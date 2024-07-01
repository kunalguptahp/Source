using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.WebControlEnhancements;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class QueryParameterValueEditListUpdatePanel : BaseListViewUserControl
	{

		#region Constants

		private const string ViewStateKey_SelectionList = "SelectionList";
        private const string ViewStateKey_MinimumSelection = "MinimumSelection";
        private const string ViewStateKey_MaximumSelection = "MaximumSelection";

		#endregion

		#region Overrides

		#region Property Overrides

		protected override Label ErrorLabel
		{
			get { return this.lblError; }
		}

		protected override GridView Grid
		{
			get { return this.gvList; }
		}

		#endregion

		#region Method Overrides

		protected override void BindScreen(IQuerySpecification querySpecification)
		{
			QueryParameterValueQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            QueryParameterValueQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
            GridView gv = this.Grid;

            // default sort column
            if (query.SortBy.Count == 0)
            {
                // default to QueryValueName.
                query.SortBy.PromoteToPrimary("Name ASC");
            }

			//Configure the ObjectDataSource using the current QuerySpecification
			Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
			querySpecificationParameter.DefaultValue = query.ToString();

            this.txtValue.DisableIf(!string.IsNullOrEmpty(immutableConditions.QueryParameterValueNameCont));
            this.txtValue.Text = query.QueryParameterValueNameCont;

			//read the query's Paging.PageSize info
			gv.PageSize = query.Paging.PageSize ?? QueryPaginationSpecification.DefaultPageSize;

			//read the query's Paging.PageIndex info last (since sorting resets the PageIndex to 0)
			int pageIndex = query.Paging.PageIndex ?? 0;
			if (gv.PageIndex != pageIndex)
			{
				gv.PageIndex = pageIndex;
			}

			this.ApplyDataControlDefaultValues();
		}

		private void InitializeSelectionList()
		{
			int configurationServiceGroupSelectorId = this.GetConfigurationServiceGroupSelectorIdOfParent() ?? 0;
			if (configurationServiceGroupSelectorId == 0)
			{
				SelectionList = null;
				return;
			}

			// TODO - Check with Justin.  OK, to use ImmutableQueryCondtions.
			QueryParameterValueQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			int queryParameterId = immutableConditions.QueryParameterId ?? 0;

			List<int> selectionList = new List<int>();
			VwMapConfigurationServiceGroupSelectorQueryParameterValueCollection qpvCollection =
				VwMapConfigurationServiceGroupSelectorQueryParameterValueController.FetchByConfigurationServiceGroupSelectorIdQueryParameterId(configurationServiceGroupSelectorId, queryParameterId);
			foreach (VwMapConfigurationServiceGroupSelectorQueryParameterValue queryParameterValue in qpvCollection)
			{
				this.chkNot.Checked = queryParameterValue.Negation;
				selectionList.Add(queryParameterValue.QueryParameterValueId);
			}
			SelectionList = selectionList;

			this.DataBind();
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			QueryParameterValueQuerySpecification query = new QueryParameterValueQuerySpecification();
			GridView gv = this.Grid;

			//NOTE: DO NOT update the sorting and paging here _from the GridView_, because that is done in the GridView's RowCommand handler
			//NOTE: However, DO preserve the existing sort order (if any) _from the previous query_ to preserve the sort order when the filter conditions are changed
			IQuerySpecification previousQuery = this.QuerySpecification;
			if ((previousQuery != null) && (previousQuery.SortBy != null) && (previousQuery.SortBy.Count > 0))
			{
				query.SortBy.ReplaceAllItems(previousQuery.SortBy); //copy the SortBy info from the previousQuery
			}

            //set the query's Conditions info
            query.QueryParameterValueNameCont = this.txtValue.Text.TrimToNull();
            return query;
		}

		protected override void EditItem(int index)
		{
			throw new System.NotSupportedException();
		}

		protected override void PopulateListControls()
		{
			// Do nothing
		}

		protected override void ClearDataControls()
		{
			// Do nothing
		}

		#endregion

		#endregion

		#region Properties

		/// <summary>
		///  Selection of query parameter values
		/// </summary>
		private List<int> SelectionList
		{
			get
			{
				return this.ViewState[ViewStateKey_SelectionList] as List<int>;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove(ViewStateKey_SelectionList);
				}
				else
				{
					this.ViewState[ViewStateKey_SelectionList] = value;
				}
			}
		}

		/// <summary>
		///  visibility of persistence control (Default = false)
		/// </summary>
		public bool PersistenceControlsVisible { get; set; }

        /// <summary>
        ///  minimum selection (Default = 1)
        /// </summary>
        public int? MinimumSelection
        {
            get
            {
                return (this.ViewState[ViewStateKey_MinimumSelection] == null) ? 1 : this.ViewState[ViewStateKey_MinimumSelection] as int?;
            }
            set
            {
                if (value == null)
                {
                    this.ViewState.Remove(ViewStateKey_MinimumSelection);
                }
                else
                {
                    this.ViewState[ViewStateKey_MinimumSelection] = value;
                }
            }
        }

        /// <summary>
        ///  maximum selection (Default = 0 = all)
        /// </summary>
        public int? MaximumSelection
        {
            get
            {
                return (this.ViewState[ViewStateKey_MaximumSelection] == null) ? 0 : this.ViewState[ViewStateKey_MaximumSelection] as int?;
            }
            set
            {
                if (value == null)
                {
                    this.ViewState.Remove(ViewStateKey_MaximumSelection);
                }
                else
                {
                    this.ViewState[ViewStateKey_MaximumSelection] = value;
                }
            }
        }

		#endregion

		#region PageEvents

		protected void Page_Load(object sender, EventArgs e)
		{
			this.pnlPersistanceControls.Visible = this.PersistenceControlsVisible;
			this.Page_LoadHelper();

			if (!Page.IsPostBack)
			{
				InitializeSelectionList();
			}
		}

		#endregion

		#region ControlEvents

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            this.UpdateSelectionList();
            this.Grid.PageIndex = 0;
            this.ApplyScreenInput();
        }

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			QueryParameterValueQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			int queryParameterId = immutableConditions.QueryParameterId ?? 0;

			int? configurationServiceGroupSelectorId = GetConfigurationServiceGroupSelectorIdOfParent();
			if (configurationServiceGroupSelectorId != null)
			{
				this.SaveInput((int)configurationServiceGroupSelectorId, queryParameterId);			
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.DataBind();
		}

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<int> selectionList = (SelectionList ?? new List<int>());
                int queryParameterValueId = (int)this.gvList.DataKeys[e.Row.RowIndex].Values[1];
                if (selectionList.Contains(queryParameterValueId))
                {
                    CheckBox chkBox = (CheckBox)e.Row.FindControl("CheckBoxButton");
                    chkBox.Checked = true;
                }
            }
        }

		protected override void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.UpdateSelectionList();
			base.GridView_PageIndexChanging(sender, e);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Modifies the value or selection of all of this control's DataBound controls so that all such controls are initialized to whichever "default" value 
		/// is appropriate (for a new, non-existing record) for each such control.
		/// </summary>
		private void ApplyDataControlDefaultValues()
		{
			QueryParameterValueQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			int queryParameterId = immutableConditions.QueryParameterId ?? 0;

			this.lblQueryParameterName.Text = ElementsCPSSqlUtility.GetName("QueryParameter", queryParameterId);
            if (MaximumSelection > 0)
                this.lblHint.Text = String.Format("Note: Select up to {0} value(s).", MaximumSelection);
        }

        protected void UpdateSelectionList()
        {
            List<int> selectionList = (SelectionList ?? new List<int>());
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                if (gvList.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    int queryParameterValueId = (int)this.gvList.DataKeys[i].Values[1];

                    CheckBox chkBox = (CheckBox)gvList.Rows[i].FindControl("CheckBoxButton");
                    if (chkBox.Checked == true)
                    {
                        if (!selectionList.Contains(queryParameterValueId))
                        {
                            selectionList.Add(queryParameterValueId);
                        }
                    }
                    else
                    {
                        if (selectionList.Contains(queryParameterValueId))
                        {
                            selectionList.Remove(queryParameterValueId);
                        }
                    }
                }
            }
            SelectionList = selectionList;
        }

		#region Helper Methods

		/// <summary>
		/// Save Input
		/// </summary>
		/// <returns></returns>
		public void SaveInput(int configurationServiceGroupSelectorId, int queryParameterId)
		{
			try
			{
				// Update selection list for curent page
				this.UpdateSelectionList();

				using (TransactionScope scope = new TransactionScope())
				{
					// remove existing query parameter values
					ConfigurationServiceGroupSelectorQueryParameterValue.DestroyByConfigurationServiceGroupSelectorIdQueryParameterId(configurationServiceGroupSelectorId, queryParameterId);

					// update selected query parameter values
					List<int> selectionList = (SelectionList ?? new List<int>());
					for (int i = 0; i < selectionList.Count; i++)
					{
						ConfigurationServiceGroupSelectorQueryParameterValue saveItem = new ConfigurationServiceGroupSelectorQueryParameterValue(true);
						saveItem.ConfigurationServiceGroupSelectorId = configurationServiceGroupSelectorId;
						saveItem.QueryParameterValueId = selectionList[i];
						saveItem.Negation = this.chkNot.Checked;
						saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);						
					}

					// transaction complete
					scope.Complete();
				}
				this.DataBind();
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}
		}

        /// <summary>
        /// Check minimum number of selections
        /// </summary>
        /// <returns></returns>
        public bool IsMinimumValueSelected()
        {
            this.UpdateSelectionList();

            // must select the minimum selection
            if ((SelectionList == null ? 0 : SelectionList.Count) >= this.MinimumSelection)
            {
                return true;
            }
            else
            {
                lblError.Text = string.Format("You must select at least {0} value(s).", MinimumSelection);
                return false;
            }
        }

        /// <summary>
        /// Check maximum number of selections
        /// </summary>
        /// <returns></returns>
        public bool IsMaximumValueExceeded()
        {
            this.UpdateSelectionList();

            if ((this.MaximumSelection == null ? 0 : this.MaximumSelection) == 0)
                // maximum selection = 0 then allow user to select all
                return false;
            else
                // must not exceed the maximum selection
                if ((SelectionList == null ? 0 : SelectionList.Count) > (this.MaximumSelection ?? 1))
                {
                    lblError.Text = string.Format("You have exceeded the selection limit of {0}.", MaximumSelection);
                    return true;
                }
                else
                {
                    return false;
                }
        }

		private int? GetConfigurationServiceGroupSelectorIdOfParent()
		{
			RecordDetailUserControl parentRecordDetailUserControl = Global.GetParentRecordDetailUserControl(this);
			return (parentRecordDetailUserControl == null) ? null : parentRecordDetailUserControl.DataSourceId;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public QueryParameterValueQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as QueryParameterValueQuerySpecification ?? new QueryParameterValueQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}