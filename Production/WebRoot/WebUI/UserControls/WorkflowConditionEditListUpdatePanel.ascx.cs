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
	public partial class WorkflowConditionEditListUpdatePanel : BaseListViewUserControl
	{
		#region Constants

		private const string ViewStateKey_SelectionList = "SelectionList";

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
			WorkflowConditionQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			WorkflowConditionQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);

			GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
            int tenantGroupId = PersonController.GetCurrentUser().TenantGroupId;
            Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
            querySpecificationParameter.DefaultValue = query.ToString();
           

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
			int workflowModuleId = this.GetWorkflowModuleIdOfParent() ?? 0;
			if (workflowModuleId == 0)
			{
				SelectionList = null;
				return;
			}

			List<int> selectionList = new List<int>();
			VwMapWorkflowModuleWorkflowConditionCollection wcCollection =
				VwMapWorkflowModuleWorkflowConditionController.FetchByWorkflowModuleId(workflowModuleId);
			foreach (VwMapWorkflowModuleWorkflowCondition workflowModCond in wcCollection)
			{
				selectionList.Add(workflowModCond.WorkflowConditionId);
			}
			SelectionList = selectionList;

			this.DataBind();
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			WorkflowConditionQuerySpecification query = new WorkflowConditionQuerySpecification();
			GridView gv = this.Grid;

			//NOTE: DO NOT update the sorting and paging here _from the GridView_, because that is done in the GridView's RowCommand handler
			//NOTE: However, DO preserve the existing sort order (if any) _from the previous query_ to preserve the sort order when the filter conditions are changed
			IQuerySpecification previousQuery = this.QuerySpecification;
			if ((previousQuery != null) && (previousQuery.SortBy != null) && (previousQuery.SortBy.Count > 0))
			{
				query.SortBy.ReplaceAllItems(previousQuery.SortBy); //copy the SortBy info from the previousQuery
			}
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
		///  Selection of workflow conditions
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

		#region Convenience Properties


		#endregion

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

		protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				List<int> selectionList = (SelectionList ?? new List<int>());
				int conditionId = (int)this.gvList.DataKeys[e.Row.RowIndex].Values[0];
				if (selectionList.Contains(conditionId))
				{
					CheckBox chkBox = (CheckBox)e.Row.FindControl("CheckBoxButton");
					chkBox.Checked = true;
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			int? workflowModuleId = GetWorkflowModuleIdOfParent();
			if (workflowModuleId != null)
			{
				this.SaveInput((int)workflowModuleId, false);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			this.DataBind();
		}

		protected void UpdateSelectionList()
		{
			List<int> selectionList = (SelectionList ?? new List<int>());
			for (int i = 0; i < gvList.Rows.Count; i++)
			{
				if (gvList.Rows[i].RowType == DataControlRowType.DataRow)
				{
					int workflowConditionId = (int)this.gvList.DataKeys[i].Values[0];

					CheckBox chkBox = (CheckBox)gvList.Rows[i].FindControl("CheckBoxButton");
					if (chkBox.Checked == true)
					{
						if (!selectionList.Contains(workflowConditionId))
						{
							selectionList.Add(workflowConditionId);
						}
					}
					else
					{
						if (selectionList.Contains(workflowConditionId))
						{
							selectionList.Remove(workflowConditionId);
						}
					}
				}
			}
			SelectionList = selectionList;
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
		}

		#region Helper Methods

		/// <summary>
		/// Save Input
		/// </summary>
		/// <returns></returns>
		public void SaveInput(int workflowModuleId, bool multiEditFlag)
		{
			try
			{
                // update the selection list
                this.UpdateSelectionList();

                // for editing multiple modules only update if user selected a condition. If no selection is made, just ignore.
                if ((multiEditFlag == false) || (this.SelectionCount() > 0))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        // remove existing workflow conditions
                        WorkflowModuleWorkflowCondition.DestroyByWorkflowModuleId(workflowModuleId);

                        // update selected workflow conditions
                        List<int> selectionList = (SelectionList ?? new List<int>());
                        for (int i = 0; i < selectionList.Count; i++)
                        {
                            WorkflowModuleWorkflowCondition saveItem = new WorkflowModuleWorkflowCondition(true);
                            saveItem.WorkflowModuleId = workflowModuleId;
                            saveItem.WorkflowConditionId = selectionList[i];
                            saveItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                        }

                        // transaction complete
                        scope.Complete();
                    }
                }
                this.DataBind();
			}
			catch (SqlException ex)
			{
				LogManager.Current.Log(Severity.Error, this, "Unrecognized Exception", ex);
				throw;
			}
		}

        private int SelectionCount()
        {
            return (SelectionList == null ? 0 : SelectionList.Count);
        }

		private int? GetWorkflowModuleIdOfParent()
		{
			RecordDetailUserControl parentRecordDetailUserControl = Global.GetParentRecordDetailUserControl(this);
			return (parentRecordDetailUserControl == null) ? null : parentRecordDetailUserControl.DataSourceId;
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public WorkflowConditionQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as WorkflowConditionQuerySpecification ?? new WorkflowConditionQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}