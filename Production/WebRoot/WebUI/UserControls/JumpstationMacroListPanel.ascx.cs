using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.WebControlEnhancements;
using HP.HPFx.Web.Utility;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class JumpstationMacroListPanel : BaseListViewUserControl
    {

        #region Constants

        const string NOT_SELECT_MESSAGE = "There is no row selected.";
		const string ONLY_PUBLISHED_MESSAGE = "To replace macros, all selected macros must be published.";

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
			JumpstationMacroQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			JumpstationMacroQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
            int tenantGroupId = PersonController.GetCurrentUser().TenantGroupId;
            Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
            querySpecificationParameter.DefaultValue = query.ToString();
            Parameter queryTenantGroupId = this.odsDataSource.SelectParameters[1];
            queryTenantGroupId.DefaultValue = tenantGroupId.ToString();

			//read the query's Conditions info
			this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
			this.txtIdList.Text = query.IdListAsString;
            this.txtName.DisableIf(!string.IsNullOrEmpty(immutableConditions.Name));
            this.txtName.Text = query.Name;
			this.txtDescription.DisableIf(!string.IsNullOrEmpty(immutableConditions.Description));
			this.txtDescription.Text = query.Description;
			this.txtValidationId.DisableIf(!(immutableConditions.ValidationId == null));
			this.txtValidationId.Text = query.ValidationId.ToString();
			this.txtPublicationId.DisableIf(!(immutableConditions.ProductionId == null));
			this.txtPublicationId.Text = query.ProductionId.ToString();

            this.ddlMacroStatus.DisableIf(immutableConditions.JumpstationMacroStatusId != null);
            if (query.JumpstationMacroStatusId == null)
            {
                this.ddlMacroStatus.ClearSelection();
            }
            else
            {
                this.ddlMacroStatus.ForceSelectedValue(query.JumpstationMacroStatusId);
            }

			this.ddlOwner.DisableIf(immutableConditions.OwnerId != null);
			if (query.OwnerId == null)
			{
				this.ddlOwner.ClearSelection();
			}
			else
			{
				this.ddlOwner.ForceSelectedValue(query.OwnerId);
			}

			this.txtCreatedAfter.DisableIf(immutableConditions.CreatedAfter != null);
			Global.InitializeFilter_DateOnly(this.txtCreatedAfter, query.CreatedAfter);

			this.txtCreatedBefore.DisableIf(immutableConditions.CreatedBefore != null);
			Global.InitializeFilter_DateOnly(this.txtCreatedBefore, query.CreatedBefore);

			this.txtCreatedBy.DisableIf(immutableConditions.CreatedBy != null);
			Global.InitializeFilter_CreatedByFilter(this.txtCreatedBy, query.CreatedBy);

			this.txtModifiedAfter.DisableIf(immutableConditions.ModifiedAfter != null);
			Global.InitializeFilter_DateOnly(this.txtModifiedAfter, query.ModifiedAfter);

			this.txtModifiedBefore.DisableIf(immutableConditions.ModifiedBefore != null);
			Global.InitializeFilter_DateOnly(this.txtModifiedBefore, query.ModifiedBefore);

			this.txtModifiedBy.DisableIf(immutableConditions.ModifiedBy != null);
			Global.InitializeFilter_ModifiedByFilter(this.txtModifiedBy, query.ModifiedBy);

			if (query.Paging.PageSize != null)
			{
				this.ddlItemsPerPage.ForceSelectedValue(query.Paging.PageSize);
			}

			//read the query's Paging.PageSize info
			gv.PageSize = query.Paging.PageSize ?? Convert.ToInt32(this.ddlItemsPerPage.SelectedValue);

			//read the query's Paging.PageIndex info last (since sorting resets the PageIndex to 0)
			int pageIndex = query.Paging.PageIndex ?? 0;
			if (gv.PageIndex != pageIndex)
			{
				gv.PageIndex = pageIndex;
			}
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			JumpstationMacroQuerySpecification query = new JumpstationMacroQuerySpecification();
			GridView gv = this.Grid;

			//NOTE: DO NOT update the sorting and paging here _from the GridView_, because that is done in the GridView's RowCommand handler
			//NOTE: However, DO preserve the existing sort order (if any) _from the previous query_ to preserve the sort order when the filter conditions are changed
			IQuerySpecification previousQuery = this.QuerySpecification;
			if ((previousQuery != null) && (previousQuery.SortBy != null) && (previousQuery.SortBy.Count > 0))
			{
				query.SortBy.ReplaceAllItems(previousQuery.SortBy); //copy the SortBy info from the previousQuery
			}

			//set the query's Conditions info
			query.IdListAsString = this.txtIdList.Text.TrimToNull();
			query.Description = this.txtDescription.Text.TrimToNull();
		    query.Name = this.txtName.Text.TrimToNull();
			query.ValidationId = this.txtValidationId.Text.TryParseInt32();
			query.ProductionId = this.txtPublicationId.Text.TryParseInt32();
			query.JumpstationMacroStatusId = this.ddlMacroStatus.SelectedValue.TryParseInt32();
			query.OwnerId = this.ddlOwner.SelectedValue.TryParseInt32();

			query.CreatedAfter = Global.ReadFilterValue_DateOnly(this.txtCreatedAfter);
			query.CreatedBefore = Global.ReadFilterValue_DateOnly(this.txtCreatedBefore);
			query.CreatedBy = Global.ReadFilterValue_WindowsIdFromCreatedByFilter(this.txtCreatedBy, true);
			query.ModifiedAfter = Global.ReadFilterValue_DateOnly(this.txtModifiedAfter);
			query.ModifiedBefore = Global.ReadFilterValue_DateOnly(this.txtModifiedBefore);
			query.ModifiedBy = Global.ReadFilterValue_WindowsIdFromModifiedByFilter(this.txtModifiedBy, true);

			query.Paging.PageSize = Convert.ToInt32(this.ddlItemsPerPage.SelectedValue);
			return query;
		}

		protected override void EditItem(int index)
		{
			this.EditJumpstationMacro(this.GetRowIdInt32(index));
		}

		protected override void PopulateListControls()
		{

            Global.BindJumpstationMacroStatusListControl(this.ddlMacroStatus, RowStatus.RowStatusId.Active);
            this.ddlMacroStatus.InsertItem(0, "", Global.GetAllListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetAllListText());
            }

		protected override void ClearDataControls()
		{
			this.txtIdList.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
		    this.txtName.Text = string.Empty;
			this.txtValidationId.Text = string.Empty;
			this.txtPublicationId.Text = string.Empty;
			this.ddlMacroStatus.ClearSelection();
			this.ddlOwner.ClearSelection();

			this.txtCreatedAfter.Text = string.Empty;
			this.txtCreatedBefore.Text = string.Empty;
			this.txtCreatedBy.Text = string.Empty;
			this.txtModifiedAfter.Text = string.Empty;
			this.txtModifiedBefore.Text = string.Empty;
			this.txtModifiedBy.Text = string.Empty;
		}

		protected override void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename)
		{
			//NOTE: DO NOT invoke the base class' implementation
            JumpstationMacroQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            query.CreatedBy = strs;
			this.ExportData(query, exportFormat, filename, Global.YieldDataTablesForDataExport<VwMapJumpstationMacro, VwMapJumpstationMacroCollection, JumpstationMacroQuerySpecification>(query, VwMapJumpstationMacroController.Fetch));
		}

		protected override void ExportData(System.Collections.Generic.IEnumerable<DataTable> dataTables, HP.HPFx.Extensions.Data.Export.ExportDataExtensions.ExportOptions exportOptions, string mimeType, string filename)
		{
			exportOptions.ExcludedColumns.AddRange(new[] { "rowstatusid", "rowstatusname" });
			base.ExportData(dataTables, exportOptions, mimeType, filename);
		}

		#endregion

		#endregion

		#region Properties

		#region Convenience Properties

		/// <summary>
		/// Returns the result of invoking <see cref="ConvertToExpectedType"/> on this instance's <see cref="QuerySpecification"/>.
		/// </summary>
		public JumpstationMacroQuerySpecification ConvertedQuerySpecification
		{
			get
			{
				return this.ConvertToExpectedType(this.QuerySpecification);
			}
		}

		#endregion

		#endregion

		#region PageEvents

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Page_LoadHelper();

			if (!SecurityManager.IsCurrentUserInRole(UserRoleId.Editor))
			{
				this.btnCreate.Visible = false;
				this.btnEdit.Visible = false;

				ButtonField colEditButton = this.gvList.Columns[0] as ButtonField;
				colEditButton.Text = "View...";
			}
		}

		#endregion

		#region ControlEvents

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			this.EditJumpstationMacro(null);
		}

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            List<int> selectedJumpstationMacroIds = this.GetSelectedJumpstationMacroIds();
            if (selectedJumpstationMacroIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                JumpstationMacroQuerySpecification jumpstationMacroQuerySpecification = new JumpstationMacroQuerySpecification() { IdList = selectedJumpstationMacroIds };
                this.EditJumpstationMacros(jumpstationMacroQuerySpecification);
            }
        }

        protected void btnMultiReplace_Click(object sender, EventArgs e)
        {
            List<int> selectedJumpstationMacroIds = this.GetSelectedJumpstationMacroIds();
            if (selectedJumpstationMacroIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                JumpstationMacroQuerySpecification jumpstationMacroQuerySpecification = new JumpstationMacroQuerySpecification() { IdList = selectedJumpstationMacroIds };
                var jumpstationMacros = VwMapJumpstationMacroController.Fetch(jumpstationMacroQuerySpecification);
                foreach (var jumpstationMacro in jumpstationMacros)
                {
                    if ((int)JumpstationMacroStateId.Published != jumpstationMacro.JumpstationMacroStatusId)
                    {
                        WebUtility.ShowAlertBox(this, ONLY_PUBLISHED_MESSAGE);
                        return;
                    }
                }

                this.EditMultiReplaceJumpstationMacros(jumpstationMacroQuerySpecification);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            const string entityName = "EntityTypes";
            string filename = Global.GenerateStandardExportFilename(entityName, this.CombinedQuerySpecification);
            this.ExportData("xls", filename, true);
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            List<int> selectedJumpstationMacroIds = this.GetSelectedJumpstationMacroIds();
            if (selectedJumpstationMacroIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                JumpstationMacroQuerySpecification jumpstationMacroQuerySpecification =
                    new JumpstationMacroQuerySpecification() { IdList = selectedJumpstationMacroIds };
                this.ReportJumpstationMacros(jumpstationMacroQuerySpecification);
            }
        }

		protected void btnFilter_Click(object sender, EventArgs e)
		{
			if (!this.Page.IsValid)
			{
				return;
			}

			this.Grid.PageIndex = 0;
			this.ApplyScreenInput();
		}

		protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
		{
			// Note - this event fires when commandname = "Edit"
			e.Cancel = true;
		}

		protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataKey keys = gvList.DataKeys[e.Row.RowIndex];
				string keyVid = (keys["ValidationId"] != null) ? keys["ValidationId"].ToString() : string.Empty;
				string keyPid = (keys["ProductionId"] != null) ? keys["ProductionId"].ToString() : string.Empty;
				string keyQueryString = (keys["QueryString"] != null) ? keys["QueryString"].ToString() : string.Empty;
			}
		}

        protected void cvTxtIdList_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Global.ValidateCommaSeparatedIdList(this.txtIdList.Text.ToString());
        }

		#endregion

		#region Methods

		#region Helper Methods

		private void EditJumpstationMacro(int? jumpstationMacroId)
		{
			this.Response.Redirect(Global.GetJumpstationMacroUpdatePageUri(jumpstationMacroId, this.QuerySpecification));
		}

		private void EditJumpstationMacros(JumpstationMacroQuerySpecification jumpstationMacroQuerySpecification)
		{
			this.Response.Redirect(Global.GetJumpstationMacroEditUpdatePageUri(jumpstationMacroQuerySpecification));
		}

        private void EditMultiReplaceJumpstationMacros(JumpstationMacroQuerySpecification jumpstationMacroQuerySpecification)
        {
            this.Response.Redirect(Global.GetJumpstationMacroMultiReplaceUpdatePageUri(jumpstationMacroQuerySpecification));
        }

        private void ReportJumpstationMacros(JumpstationMacroQuerySpecification jumpstationMacroQuerySpecification)
        {
            this.Response.Redirect(Global.GetJumpstationMacroReportPageUri(jumpstationMacroQuerySpecification));
        }
		
		private List<int> GetSelectedJumpstationMacroIds()
		{
			return this.gvList.GetSelectedDataKeyValues<int>();
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public JumpstationMacroQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as JumpstationMacroQuerySpecification ?? new JumpstationMacroQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}