using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using System.Configuration;
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
	public partial class WorkflowListPanel : BaseListViewUserControl
	{
		const string NOT_SELECT_MESSAGE = "There is no row selected.";
		const string NOT_SAME_TYPE_MESSAGE = "When using multiple edit, all workflow types must be the same.";
		const string ONLY_PUBLISHED_MESSAGE = "To replace workflows, all selected workflows must be published.";

		private static string CHECK_URL_VAL;
		private static string CHECK_URL_PUB;
		static WorkflowListPanel()
		{
			CHECK_URL_VAL = ConfigurationManager.AppSettings["workflowCheckUrlValidation"];
			CHECK_URL_PUB = ConfigurationManager.AppSettings["workflowCheckUrlPublication"];
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
			WorkflowQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			WorkflowQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
            int tenantGroupId = PersonController.GetCurrentUser().TenantGroupId;
            Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
            Parameter queryTenantGroupId = this.odsDataSource.SelectParameters[1];
            queryTenantGroupId.DefaultValue = tenantGroupId.ToString();
            querySpecificationParameter.DefaultValue = query.ToString();

			//read the query's Conditions info
			this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
			this.txtIdList.Text = query.IdListAsString;
			this.txtName.DisableIf(!string.IsNullOrEmpty(immutableConditions.Name));
			this.txtName.Text = query.Name;
            this.txtFilename.DisableIf(!string.IsNullOrEmpty(immutableConditions.Filename));
            this.txtFilename.Text = query.Filename;
            this.txtDescription.DisableIf(!string.IsNullOrEmpty(immutableConditions.Description));
			this.txtDescription.Text = query.Description;
			this.txtValidationId.DisableIf(!(immutableConditions.ValidationId == null));
			this.txtValidationId.Text = query.ValidationId.ToString();
			this.txtPublicationId.DisableIf(!(immutableConditions.ProductionId == null));
			this.txtPublicationId.Text = query.ProductionId.ToString();
			this.txtTagsFilter.DisableIf(!string.IsNullOrEmpty(immutableConditions.Tags));
			this.txtTagsFilter.Text = query.Tags;

			this.ddlWorkflowApplication.DisableIf(immutableConditions.WorkflowApplicationId!= null);
			if (query.WorkflowApplicationId == null)
			{
                this.ddlWorkflowApplication.ClearSelection();
			}
			else
			{
                this.ddlWorkflowApplication.ForceSelectedValue(query.WorkflowApplicationId);
			}

			this.ddlWorkflowType.DisableIf(immutableConditions.WorkflowTypeId != null);
			if (query.WorkflowTypeId == null)
			{
				this.ddlWorkflowType.ClearSelection();
			}
			else
			{
				this.ddlWorkflowType.ForceSelectedValue(query.WorkflowTypeId);
			}

			this.ddlWorkflowStatus.DisableIf(immutableConditions.WorkflowStatusId != null);
			if (query.WorkflowStatusId == null)
			{
				this.ddlWorkflowStatus.ClearSelection();
			}
			else
			{
				this.ddlWorkflowStatus.ForceSelectedValue(query.WorkflowStatusId);
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

			this.ddlRelease.DisableIf(immutableConditions.ReleaseQueryParameterValue != null);
			if (query.ReleaseQueryParameterValue == null)
			{
				this.ddlRelease.ClearSelection();
			}
			else
			{
				this.ddlRelease.ForceSelectedValue(query.ReleaseQueryParameterValue);
			}

			this.ddlCountry.DisableIf(immutableConditions.CountryQueryParameterValue != null);
			if (query.CountryQueryParameterValue == null)
			{
				this.ddlCountry.ClearSelection();
			}
			else
			{
				this.ddlCountry.ForceSelectedValue(query.CountryQueryParameterValue);
			}

			this.ddlPlatform.DisableIf(immutableConditions.PlatformQueryParameterValue != null);
			if (query.PlatformQueryParameterValue == null)
			{
				this.ddlPlatform.ClearSelection();
			}
			else
			{
				this.ddlPlatform.ForceSelectedValue(query.PlatformQueryParameterValue);
			}

			this.ddlBrand.DisableIf(immutableConditions.BrandQueryParameterValue != null);
			if (query.BrandQueryParameterValue == null)
			{
				this.ddlBrand.ClearSelection();
			}
			else
			{
				this.ddlBrand.ForceSelectedValue(query.BrandQueryParameterValue);
			}

            this.ddlSubBrand.DisableIf(immutableConditions.SubBrandQueryParameterValue != null);
            if (query.SubBrandQueryParameterValue == null)
            {
                this.ddlSubBrand.ClearSelection();
            }
            else
            {
                this.ddlSubBrand.ForceSelectedValue(query.SubBrandQueryParameterValue);
            }

			this.ddlModelNumber.DisableIf(immutableConditions.ModelNumberQueryParameterValue != null);
			if (query.ModelNumberQueryParameterValue == null)
			{
				this.ddlModelNumber.ClearSelection();
			}
			else
			{
				this.ddlModelNumber.ForceSelectedValue(query.ModelNumberQueryParameterValue);
			}

            this.ddlAppClient.DisableIf(immutableConditions.AppClientId != null);
            if (query.AppClientId == null)
            {
                this.ddlAppClient.ClearSelection();
            }
            else
            {
                this.ddlAppClient.ForceSelectedValue(AppClientController.FetchByID(query.AppClientId)[0].Name);
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
			WorkflowQuerySpecification query = new WorkflowQuerySpecification();
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
            query.Description = this.txtDescription.Text.TrimToNull();
            query.WorkflowApplicationId = this.ddlWorkflowApplication.SelectedValue.TryParseInt32();
			query.ValidationId = this.txtValidationId.Text.TryParseInt32();
			query.ProductionId = this.txtPublicationId.Text.TryParseInt32();
			query.Tags = this.txtTagsFilter.Text.TrimToNull();
			query.WorkflowTypeId = this.ddlWorkflowType.SelectedValue.TryParseInt32();
			query.WorkflowStatusId = this.ddlWorkflowStatus.SelectedValue.TryParseInt32();
			query.OwnerId = this.ddlOwner.SelectedValue.TryParseInt32();

			query.ReleaseQueryParameterValue = this.ddlRelease.SelectedValue.TrimToNull();
			query.CountryQueryParameterValue = this.ddlCountry.SelectedValue.TrimToNull();
			query.PlatformQueryParameterValue = this.ddlPlatform.SelectedValue.TrimToNull();
			query.BrandQueryParameterValue = this.ddlBrand.SelectedValue.TrimToNull();
            query.SubBrandQueryParameterValue = this.ddlSubBrand.SelectedValue.TrimToNull();
			query.ModelNumberQueryParameterValue = this.ddlModelNumber.SelectedValue.TrimToNull();
            if (this.ddlAppClient.SelectedValue.TrimToNull() != null)
            {
                query.AppClientId = AppClientController.FetchByName(this.ddlAppClient.SelectedValue).Id;
            }

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
			this.EditWorkflow(this.GetRowIdInt32(index));
		}

		protected override void PopulateListControls()
		{
            string windowsId = this.Page.User.Identity.Name.ToString();
            int tenantId = PersonController.GetCurrentUser().TenantGroupId;
            Global.BindWorkflowApplicationListControl(this.ddlWorkflowApplication, RowStatus.RowStatusId.Active,tenantId,true);
            this.ddlWorkflowApplication.InsertItem(0, "", Global.GetAllListText());
	
			Global.BindWorkflowTypeListControl(this.ddlWorkflowType, RowStatus.RowStatusId.Active);
			this.ddlWorkflowType.InsertItem(0, "", Global.GetAllListText());

			Global.BindWorkflowStatusListControl(this.ddlWorkflowStatus, RowStatus.RowStatusId.Active);
			this.ddlWorkflowStatus.InsertItem(0, "", Global.GetAllListText());

            Global.BindQueryParameterValueNameListControl(this.ddlModelNumber, (int)QueryParameter.QueryParameterId.ModelNumber, RowStatus.RowStatusId.Active);
			this.ddlModelNumber.InsertItem(0, "", Global.GetAllListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueNameListControl(this.ddlRelease, (int)QueryParameter.QueryParameterId.ReleaseEnd, RowStatus.RowStatusId.Active);
			this.ddlRelease.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueNameListControl(this.ddlCountry, (int)QueryParameter.QueryParameterId.Country, RowStatus.RowStatusId.Active);
			this.ddlCountry.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueNameListControl(this.ddlPlatform, (int)QueryParameter.QueryParameterId.PCPlatform, RowStatus.RowStatusId.Active);
			this.ddlPlatform.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueNameListControl(this.ddlBrand, (int)QueryParameter.QueryParameterId.PCBrand, RowStatus.RowStatusId.Active);
			this.ddlBrand.InsertItem(0, "", Global.GetAllListText());
        
            Global.BindQueryParameterValueNameListControl(this.ddlSubBrand, (int)QueryParameter.QueryParameterId.PCSubBrand, RowStatus.RowStatusId.Active);
            this.ddlSubBrand.InsertItem(0, "", Global.GetAllListText());

            Global.BindAppClientListControl(this.ddlAppClient);
            this.ddlAppClient.InsertItem(0, "", Global.GetAllListText());
        }

		protected override void ClearDataControls()
		{
			this.txtIdList.Text = string.Empty;
			this.txtName.Text = string.Empty;
			this.txtDescription.Text = string.Empty;
			this.txtTagsFilter.Text = string.Empty;
			this.txtValidationId.Text = string.Empty;
			this.txtPublicationId.Text = string.Empty;
            this.txtFilename.Text = string.Empty;
            this.ddlWorkflowApplication.ClearSelection();
			this.ddlWorkflowType.ClearSelection();
			this.ddlWorkflowStatus.ClearSelection();
			this.ddlOwner.ClearSelection();
			this.ddlRelease.ClearSelection();
			this.ddlCountry.ClearSelection();
			this.ddlPlatform.ClearSelection();
			this.ddlBrand.ClearSelection();
			this.ddlSubBrand.ClearSelection();
			this.ddlModelNumber.ClearSelection();
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
			//this.ExportData(querySpecification, exportFormat, filename, Global.YieldDataTablesForDataExport<VwMapWorkflow, VwMapWorkflowCollection, WorkflowQuerySpecification>(querySpecification, VwMapWorkflowController.Fetch));
            WorkflowQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            int startRowIndex = query.Paging.PagingStartRowIndex ?? 0;
            int totalRowCount = Math.Min(Global.ExportFileMaxRowCount, query.Paging.PageSize ?? int.MaxValue);
            //merge the Export paging settings/threshholds into the Paging settings of (a copy of) the QuerySpecification
            WorkflowQuerySpecification qs = QuerySpecificationWrapper.CopyAs<WorkflowQuerySpecification>(query);
            qs.Paging.SetPageIndexByStartRowIndex(totalRowCount, startRowIndex);
            int tenantGroupId = PersonController.GetCurrentUser().TenantGroupId;
            List<int> appClientIds = TenantController.GetAppClientsInt(tenantGroupId);
            qs.AppClientIds = appClientIds;
            DataTable newDataTable = VwMapWorkflowController.FetchToDataTable(qs);
           
            string fullFileName = filename.EndsWith(".xls") ? filename : (filename + ".xls");
            string strHeaderText = "Workflow List ";
            List<int> num0 = new List<int> { 0 };
            NPOIHelper.ExportByWeb(newDataTable, strHeaderText, fullFileName, num0);
            
		}

        //protected override void ExportData(System.Collections.Generic.IEnumerable<DataTable> dataTables, HP.HPFx.Extensions.Data.Export.ExportDataExtensions.ExportOptions exportOptions, string mimeType, string filename)
        //{
        //    exportOptions.ExcludedColumns.AddRange(new[] { "rowstatusid", "rowstatusname" });
        //    base.ExportData(dataTables, exportOptions, mimeType, filename);
        //}

		#endregion

		#endregion

		#region Properties

		#region Convenience Properties

		/// <summary>
		/// Returns the result of invoking <see cref="ConvertToExpectedType"/> on this instance's <see cref="QuerySpecification"/>.
		/// </summary>
		public WorkflowQuerySpecification ConvertedQuerySpecification
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
            btnExport.Attributes.Add("onclick", "return window.alert('Processing...?')"); 
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
			this.EditWorkflow(null);
		}

		protected void btnEdit_Click(object sender, EventArgs e)
		{
			List<int> selectedWorkflowIds = this.GetSelectedWorkflowIds();
			if (selectedWorkflowIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				WorkflowQuerySpecification workflowQuerySpecification = new WorkflowQuerySpecification() { IdList = selectedWorkflowIds };
				VwMapWorkflowCollection workflows = VwMapWorkflowController.Fetch(workflowQuerySpecification);
				int groupTypeId = workflows[0].WorkflowTypeId;
				foreach (VwMapWorkflow workflow in workflows)
				{
					if (groupTypeId != workflow.WorkflowTypeId)
					{
						WebUtility.ShowAlertBox(this, NOT_SAME_TYPE_MESSAGE);
						return;
					}
				}
				this.EditWorkflows(workflowQuerySpecification);
			}
		}

		protected void btnMultiReplace_Click(object sender, EventArgs e)
		{
			List<int> selectedWorkflowIds = this.GetSelectedWorkflowIds();
			if (selectedWorkflowIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				WorkflowQuerySpecification WorkflowQuerySpecification = new WorkflowQuerySpecification() { IdList = selectedWorkflowIds };
				var workflows = VwMapWorkflowController.Fetch(WorkflowQuerySpecification);
				foreach (var workflow in workflows)
				{
					if ((int)WorkflowStateId.Published != workflow.WorkflowStatusId)
					{
						WebUtility.ShowAlertBox(this, ONLY_PUBLISHED_MESSAGE);
						return;
					}
				}

				this.EditMultiReplaceWorkflows(WorkflowQuerySpecification);
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
			List<int> selectedWorkflowIds = this.GetSelectedWorkflowIds();
			if (selectedWorkflowIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				WorkflowQuerySpecification WorkflowQuerySpecification =
					new WorkflowQuerySpecification() { IdList = selectedWorkflowIds };
				this.ReportWorkflows(WorkflowQuerySpecification);
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

		private void EditWorkflow(int? workflowId)
		{
			this.Response.Redirect(Global.GetWorkflowUpdatePageUri(workflowId, this.QuerySpecification));
		}

		private void EditWorkflows(WorkflowQuerySpecification workflowQuerySpecification)
		{
			this.Response.Redirect(Global.GetWorkflowEditUpdatePageUri(workflowQuerySpecification));
		}

		private void EditMultiReplaceWorkflows(WorkflowQuerySpecification workflowQuerySpecification)
		{
			this.Response.Redirect(Global.GetWorkflowMultiReplaceUpdatePageUri(workflowQuerySpecification));
		}

		private void ReportWorkflows(WorkflowQuerySpecification workflowQuerySpecification)
		{
			this.Response.Redirect(Global.GetWorkflowReportPageUri(workflowQuerySpecification));
		}

		private List<int> GetSelectedWorkflowIds()
		{
			return this.gvList.GetSelectedDataKeyValues<int>();
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public WorkflowQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as WorkflowQuerySpecification ?? new WorkflowQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}