using System;
using System.Data;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using System.Configuration;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.ListControlEnhancements;
using HP.HPFx.Extensions.Web.UI.WebControls.ControlEnhancements.WebControlEnhancements;
using HP.HPFx.Web.Utility;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using System.Collections.Generic;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class WorkflowModuleListPanel : BaseListViewUserControl
	{
        const string NOT_SELECT_MESSAGE = "There is no row selected.";
        const string ONLY_PUBLISHED_MESSAGE = "To replace modules, all selected modules must be published.";

        private static string CHECK_URL_VAL;
		private static string CHECK_URL_PUB;
		static WorkflowModuleListPanel()
		{
			CHECK_URL_VAL = ConfigurationManager.AppSettings["workflowModuleCheckUrlValidation"];
			CHECK_URL_PUB = ConfigurationManager.AppSettings["workflowModuleCheckUrlPublication"];
		}

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
			WorkflowModuleQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			WorkflowModuleQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
            int tenantGroupId = PersonController.GetCurrentUser().TenantGroupId;
            Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
            querySpecificationParameter.DefaultValue = query.ToString();
            


			//read the query's Conditions info
			this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
			this.txtIdList.Text = query.IdListAsString;
			this.txtName.DisableIf(!string.IsNullOrEmpty(immutableConditions.Name));
			this.txtName.Text = query.Name;
            this.txtFilename.DisableIf(!string.IsNullOrEmpty(immutableConditions.Filename));
            this.txtFilename.Text = query.Filename;
            this.txtVersionMajor.DisableIf(!(immutableConditions.VersionMajor == null));
			this.txtVersionMajor.Text = query.VersionMajor.ToString();
			this.txtVersionMinor.DisableIf(!(immutableConditions.VersionMinor == null));
			this.txtVersionMinor.Text = query.VersionMinor.ToString();
			this.txtValidationId.DisableIf(!(immutableConditions.ValidationId == null));
			this.txtValidationId.Text = query.ValidationId.ToString();
			this.txtPublicationId.DisableIf(!(immutableConditions.ProductionId == null));
			this.txtPublicationId.Text = query.ProductionId.ToString();
			this.txtTagsFilter.DisableIf(!string.IsNullOrEmpty(immutableConditions.Tags));
			this.txtTagsFilter.Text = query.Tags;

			this.ddlModuleStatus.DisableIf(immutableConditions.WorkflowModuleStatusId != null);
			if (query.WorkflowModuleStatusId == null)
			{
				this.ddlModuleStatus.ClearSelection();
			}
			else
			{
				this.ddlModuleStatus.ForceSelectedValue(query.WorkflowModuleStatusId);
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

			this.ddlModuleCategory.DisableIf(immutableConditions.ModuleCategoryId != null);
			if (query.ModuleCategoryId == null)
			{
				this.ddlModuleCategory.ClearSelection();
			}
			else
			{
				this.ddlModuleCategory.ForceSelectedValue(query.ModuleCategoryId);
			}

			this.ddlModuleSubCategory.DisableIf(immutableConditions.ModuleSubCategoryId != null);
			if (query.ModuleSubCategoryId == null)
			{
				this.ddlModuleSubCategory.ClearSelection();
			}
			else
			{
				this.ddlModuleSubCategory.ForceSelectedValue(query.ModuleSubCategoryId);
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

			//read the query's Paging.PageSize info
			gv.PageSize = query.Paging.PageSize ?? QueryPaginationSpecification.DefaultPageSize;

			//read the query's Paging.PageIndex info last (since sorting resets the PageIndex to 0)
			int pageIndex = query.Paging.PageIndex ?? 0;
			if (gv.PageIndex != pageIndex)
			{
				gv.PageIndex = pageIndex;
			}
		}

		protected override IQuerySpecification CreateQueryFromScreen()
		{
			WorkflowModuleQuerySpecification query = new WorkflowModuleQuerySpecification();
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
			query.Name = this.txtName.Text.TrimToNull();
            query.Filename = this.txtFilename.Text.TrimToNull();
            query.VersionMajor = this.txtVersionMajor.Text.TryParseInt32();
			query.VersionMinor = this.txtVersionMinor.Text.TryParseInt32();
			query.ValidationId = this.txtValidationId.Text.TryParseInt32();
			query.ProductionId = this.txtPublicationId.Text.TryParseInt32();
			query.WorkflowModuleStatusId = this.ddlModuleStatus.SelectedValue.TryParseInt32();
			query.OwnerId = this.ddlOwner.SelectedValue.TryParseInt32();
			query.ModuleCategoryId = this.ddlModuleCategory.SelectedValue.TryParseInt32();
			query.ModuleSubCategoryId = this.ddlModuleSubCategory.SelectedValue.TryParseInt32();
			query.Tags = this.txtTagsFilter.Text.TrimToNull();

			query.CreatedAfter = Global.ReadFilterValue_DateOnly(this.txtCreatedAfter);
			query.CreatedBefore = Global.ReadFilterValue_DateOnly(this.txtCreatedBefore);
			query.CreatedBy = Global.ReadFilterValue_WindowsIdFromCreatedByFilter(this.txtCreatedBy, true);
			query.ModifiedAfter = Global.ReadFilterValue_DateOnly(this.txtModifiedAfter);
			query.ModifiedBefore = Global.ReadFilterValue_DateOnly(this.txtModifiedBefore);
			query.ModifiedBy = Global.ReadFilterValue_WindowsIdFromModifiedByFilter(this.txtModifiedBy, true);

			return query;
		}

		protected override void EditItem(int index)
		{
			this.EditModule(this.GetRowIdInt32(index));
		}

		protected override void PopulateListControls()
		{
			Global.BindWorkflowStatusListControl(this.ddlModuleStatus, RowStatus.RowStatusId.Active);
			this.ddlModuleStatus.InsertItem(0, "", Global.GetAllListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetAllListText());

			Global.BindWorkflowModuleCategoryListControl(this.ddlModuleCategory, RowStatus.RowStatusId.Active);
			this.ddlModuleCategory.InsertItem(0, "", Global.GetAllListText());

			Global.BindWorkflowModuleSubCategoryListControl(this.ddlModuleSubCategory, RowStatus.RowStatusId.Active);
			this.ddlModuleSubCategory.InsertItem(0, "", Global.GetAllListText());
		}

		protected override void ClearDataControls()
		{
			this.txtIdList.Text = string.Empty;
			this.txtName.Text = string.Empty;
            this.txtFilename.Text = string.Empty;
			this.txtVersionMajor.Text = string.Empty;
			this.txtVersionMinor.Text = string.Empty;
			this.txtValidationId.Text = string.Empty;
			this.txtPublicationId.Text = string.Empty;
			this.txtTagsFilter.Text = string.Empty;

			this.ddlOwner.ClearSelection();
			this.ddlModuleStatus.ClearSelection();
			this.ddlModuleCategory.ClearSelection();
			this.ddlModuleSubCategory.ClearSelection();

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
            WorkflowModuleQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            query.CreatedBy = strs;
			this.ExportData(query, exportFormat, filename, Global.YieldDataTablesForDataExport<VwMapWorkflowModule, VwMapWorkflowModuleCollection, WorkflowModuleQuerySpecification>(query, VwMapWorkflowModuleController.Fetch));
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
		public WorkflowModuleQuerySpecification ConvertedQuerySpecification
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
                this.btnMultiReplace.Visible = false;

                ButtonField colEditButton = this.gvList.Columns[0] as ButtonField;
                colEditButton.Text = "View...";
            }
		}

		#endregion

		#region ControlEvents

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			this.EditModule(null);
		}

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            List<int> selectedModuleIds = this.GetSelectedModuleIds();
            if (selectedModuleIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                WorkflowModuleQuerySpecification workflowModuleQuerySpecification = new WorkflowModuleQuerySpecification() { IdList = selectedModuleIds };
                this.EditModules(workflowModuleQuerySpecification);
            }
        }

        protected void btnMultiReplace_Click(object sender, EventArgs e)
        {
            List<int> selectedModuleIds = this.GetSelectedModuleIds();
            if (selectedModuleIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                WorkflowModuleQuerySpecification workflowModuleQuerySpecification = new WorkflowModuleQuerySpecification() { IdList = selectedModuleIds };
                var modules = VwMapWorkflowModuleController.Fetch(workflowModuleQuerySpecification);
                foreach (var module in modules)
                {
                    if ((int)WorkflowStateId.Published != module.WorkflowModuleStatusId)
                    {
                        WebUtility.ShowAlertBox(this, ONLY_PUBLISHED_MESSAGE);
                        return;
                    }
                }

                this.EditMultiReplaceModules(workflowModuleQuerySpecification);
            }
        }

		protected void btnExport_Click(object sender, EventArgs e)
		{
			const string entityName = "EntityTypes";
			string filename = Global.GenerateStandardExportFilename(entityName, this.CombinedQuerySpecification);
			this.ExportData("xls", filename, true);
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

                if (!string.IsNullOrEmpty(keyVid))
                {
                    HyperLink validationHyperlink = e.Row.Cells[10].FindControl("hlValidationId") as HyperLink;
                    validationHyperlink.Text = keyVid;
                    validationHyperlink.NavigateUrl = string.Format(CHECK_URL_VAL, keyVid);
                }

                if (!string.IsNullOrEmpty(keyPid))
                {
                    HyperLink publicationHyperlink = e.Row.Cells[11].FindControl("hlPublicationId") as HyperLink;
                    publicationHyperlink.Text = keyPid;
                    publicationHyperlink.NavigateUrl = string.Format(CHECK_URL_PUB, keyPid);
                }
            }
        }

        protected void cvTxtIdList_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Global.ValidateCommaSeparatedIdList(this.txtIdList.Text.ToString());
        }

		#endregion

		#region Methods

		#region Helper Methods

		private void EditModule(int? moduleId)
		{
			this.Response.Redirect(Global.GetWorkflowModuleUpdatePageUri(moduleId, this.QuerySpecification));
		}

        private void EditModules(WorkflowModuleQuerySpecification workflowModuleQuerySpecification)
        {
            this.Response.Redirect(Global.GetWorkflowModuleEditUpdatePageUri(workflowModuleQuerySpecification));
        }

        private void EditMultiReplaceModules(WorkflowModuleQuerySpecification workflowModuleQuerySpecification)
        {
            this.Response.Redirect(Global.GetWorkflowModuleMultiReplaceUpdatePageUri(workflowModuleQuerySpecification));
        }

        private List<int> GetSelectedModuleIds()
        {
            return this.gvList.GetSelectedDataKeyValues<int>();
        }

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public WorkflowModuleQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as WorkflowModuleQuerySpecification ?? new WorkflowModuleQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}