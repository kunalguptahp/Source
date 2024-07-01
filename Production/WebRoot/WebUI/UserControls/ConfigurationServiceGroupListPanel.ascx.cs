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
	public partial class ConfigurationServiceGroupListPanel : BaseListViewUserControl
	{

        #region Constants

        const string NOT_SELECT_MESSAGE = "There is no row selected.";
		const string NOT_SAME_TYPE_MESSAGE = "When using multiple edit, all configuration service group types must be the same.";
		const string ONLY_PUBLISHED_MESSAGE = "To replace configuration service groups, all selected groups must be published.";

		private static string CHECK_URL_VAL;
		private static string CHECK_URL_PUB;
		static ConfigurationServiceGroupListPanel()
		{
			CHECK_URL_VAL = ConfigurationManager.AppSettings["configurationServiceCheckUrlValidation"];
			CHECK_URL_PUB = ConfigurationManager.AppSettings["configurationServiceCheckUrlPublication"];
        }

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
			ConfigurationServiceGroupQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			ConfigurationServiceGroupQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
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
			this.txtDescription.DisableIf(!string.IsNullOrEmpty(immutableConditions.Description));
			this.txtDescription.Text = query.Description;
			this.txtValidationId.DisableIf(!(immutableConditions.ValidationId == null));
			this.txtValidationId.Text = query.ValidationId.ToString();
			this.txtPublicationId.DisableIf(!(immutableConditions.ProductionId == null));
			this.txtPublicationId.Text = query.ProductionId.ToString();
			this.txtTagsFilter.DisableIf(!string.IsNullOrEmpty(immutableConditions.Tags));
			this.txtTagsFilter.Text = query.Tags;
			this.txtPublisher.DisableIf(!string.IsNullOrEmpty(immutableConditions.PublisherLabelValue));
			this.txtPublisher.Text = query.PublisherLabelValue;

			this.ddlConfigurationServiceApplication.DisableIf(immutableConditions.ConfigurationServiceApplicationId != null);
			if (query.ConfigurationServiceApplicationId == null)
			{
				this.ddlConfigurationServiceApplication.ClearSelection();
			}
			else
			{
				this.ddlConfigurationServiceApplication.ForceSelectedValue(query.ConfigurationServiceApplicationId);
			}

			this.ddlConfigurationServiceGroupType.DisableIf(immutableConditions.ConfigurationServiceGroupTypeId != null);
			if (query.ConfigurationServiceGroupTypeId == null)
			{
				this.ddlConfigurationServiceGroupType.ClearSelection();
			}
			else
			{
				this.ddlConfigurationServiceGroupType.ForceSelectedValue(query.ConfigurationServiceGroupTypeId);
			}

			this.ddlConfigurationServiceGroupStatus.DisableIf(immutableConditions.ConfigurationServiceGroupStatusId != null);
			if (query.ConfigurationServiceGroupStatusId == null)
			{
				this.ddlConfigurationServiceGroupStatus.ClearSelection();
			}
			else
			{
				this.ddlConfigurationServiceGroupStatus.ForceSelectedValue(query.ConfigurationServiceGroupStatusId);
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

			this.ddlInstallerType.DisableIf(immutableConditions.InstallerTypeLabelValue != null);
			if (query.InstallerTypeLabelValue == null)
			{
				this.ddlInstallerType.ClearSelection();
			}
			else
			{
				this.ddlInstallerType.ForceSelectedValue(query.InstallerTypeLabelValue);
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
			ConfigurationServiceGroupQuerySpecification query = new ConfigurationServiceGroupQuerySpecification();
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
			query.Description = this.txtDescription.Text.TrimToNull();
			query.ConfigurationServiceApplicationId = this.ddlConfigurationServiceApplication.SelectedValue.TryParseInt32();
			query.ValidationId = this.txtValidationId.Text.TryParseInt32();
			query.ProductionId = this.txtPublicationId.Text.TryParseInt32();
			query.Tags = this.txtTagsFilter.Text.TrimToNull();
			query.ConfigurationServiceGroupTypeId = this.ddlConfigurationServiceGroupType.SelectedValue.TryParseInt32();
			query.ConfigurationServiceGroupStatusId = this.ddlConfigurationServiceGroupStatus.SelectedValue.TryParseInt32();
			query.OwnerId = this.ddlOwner.SelectedValue.TryParseInt32();

			query.ReleaseQueryParameterValue = this.ddlRelease.SelectedValue.TrimToNull();
			query.CountryQueryParameterValue = this.ddlCountry.SelectedValue.TrimToNull();
			query.PlatformQueryParameterValue = this.ddlPlatform.SelectedValue.TrimToNull();
			query.BrandQueryParameterValue = this.ddlBrand.SelectedValue.TrimToNull();
			query.PublisherLabelValue = this.txtPublisher.Text.TrimToNull();
			query.InstallerTypeLabelValue = this.ddlInstallerType.SelectedValue.TrimToNull();

			query.CreatedAfter = Global.ReadFilterValue_DateOnly(this.txtCreatedAfter);
			query.CreatedBefore = Global.ReadFilterValue_DateOnly(this.txtCreatedBefore);
			query.CreatedBy = Global.ReadFilterValue_WindowsIdFromCreatedByFilter(this.txtCreatedBy, true);
			query.ModifiedAfter = Global.ReadFilterValue_DateOnly(this.txtModifiedAfter);
			query.ModifiedBefore = Global.ReadFilterValue_DateOnly(this.txtModifiedBefore);
			query.ModifiedBy = Global.ReadFilterValue_WindowsIdFromModifiedByFilter(this.txtModifiedBy, true);

            if (this.ddlAppClient.SelectedValue.TrimToNull() != null)
            {
                query.AppClientId = AppClientController.FetchByName(this.ddlAppClient.SelectedValue).Id;
            }
			query.Paging.PageSize = Convert.ToInt32(this.ddlItemsPerPage.SelectedValue);
			return query;
		}

		protected override void EditItem(int index)
		{
			this.EditConfigurationServiceGroup(this.GetRowIdInt32(index));
		}

		protected override void PopulateListControls()
		{
            string windowsId = this.Page.User.Identity.Name.ToString();
            int tenantId = PersonController.GetCurrentUser().TenantGroupId;
			Global.BindConfigurationServiceApplicationListControl(this.ddlConfigurationServiceApplication, RowStatus.RowStatusId.Active,tenantId ,true);
			this.ddlConfigurationServiceApplication.InsertItem(0, "", Global.GetAllListText());
	
			Global.BindTenantConfigurationServiceGroupTypeListControl(this.ddlConfigurationServiceGroupType, RowStatus.RowStatusId.Active,tenantId);
			this.ddlConfigurationServiceGroupType.InsertItem(0, "", Global.GetAllListText());

			Global.BindConfigurationServiceGroupStatusListControl(this.ddlConfigurationServiceGroupStatus, RowStatus.RowStatusId.Active);
			this.ddlConfigurationServiceGroupStatus.InsertItem(0, "", Global.GetAllListText());

			Global.BindConfigurationServiceLabelValueListControl(this.ddlInstallerType, (int)ConfigurationServiceLabelId.InstallerType, RowStatus.RowStatusId.Active);
			this.ddlInstallerType.InsertItem(0, "", Global.GetAllListText());

			Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
			this.ddlOwner.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueNameListControl(this.ddlRelease, (int)QueryParameter.QueryParameterId.Release, RowStatus.RowStatusId.Active);
			this.ddlRelease.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueNameListControl(this.ddlCountry, (int)QueryParameter.QueryParameterId.Country, RowStatus.RowStatusId.Active);
			this.ddlCountry.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueNameListControl(this.ddlPlatform, (int)QueryParameter.QueryParameterId.PCPlatform, RowStatus.RowStatusId.Active);
			this.ddlPlatform.InsertItem(0, "", Global.GetAllListText());

			Global.BindQueryParameterValueNameListControl(this.ddlBrand, (int)QueryParameter.QueryParameterId.PCBrand, RowStatus.RowStatusId.Active);
			this.ddlBrand.InsertItem(0, "", Global.GetAllListText());

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
			this.ddlConfigurationServiceApplication.ClearSelection();
			this.ddlConfigurationServiceGroupType.ClearSelection();
			this.ddlConfigurationServiceGroupStatus.ClearSelection();
			this.ddlOwner.ClearSelection();
			this.ddlRelease.ClearSelection();
			this.ddlCountry.ClearSelection();
			this.ddlPlatform.ClearSelection();
			this.ddlBrand.ClearSelection();
			this.ddlInstallerType.ClearSelection();
            this.ddlAppClient.ClearSelection();
			this.txtPublisher.Text = string.Empty;
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
			//this.ExportData(querySpecification, exportFormat, filename, Global.YieldDataTablesForDataExport<VwMapConfigurationServiceGroup, VwMapConfigurationServiceGroupCollection, ConfigurationServiceGroupQuerySpecification>(querySpecification, VwMapConfigurationServiceGroupController.Fetch));
            ConfigurationServiceGroupQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            int startRowIndex = query.Paging.PagingStartRowIndex ?? 0;
            int totalRowCount = Math.Min(Global.ExportFileMaxRowCount, query.Paging.PageSize ?? int.MaxValue);
            //merge the Export paging settings/threshholds into the Paging settings of (a copy of) the QuerySpecification
            ConfigurationServiceGroupQuerySpecification qs = QuerySpecificationWrapper.CopyAs<ConfigurationServiceGroupQuerySpecification>(query);
            qs.Paging.SetPageIndexByStartRowIndex(totalRowCount, startRowIndex);

            int tenantGroupId = PersonController.GetCurrentUser().TenantGroupId;
            List<int> appClientIds = TenantController.GetAppClientsInt(tenantGroupId);
            qs.AppClientIds = appClientIds;
            DataTable newDataTable = VwMapConfigurationServiceGroupController.FetchToDataTable(qs);
            
            string fullFileName = filename.EndsWith(".xls") ? filename : (filename + ".xls");
            string strHeaderText = "Configuration Service Group List ";
            List<int> num0 = new List<int> { 0};
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
		public ConfigurationServiceGroupQuerySpecification ConvertedQuerySpecification
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
				this.btnCopy.Visible = false;

				ButtonField colEditButton = this.gvList.Columns[0] as ButtonField;
				colEditButton.Text = "View...";
			}
		}

		#endregion

		#region ControlEvents

		protected void btnCreate_Click(object sender, EventArgs e)
		{
			this.EditConfigurationServiceGroup(null);
		}

		protected void btnEdit_Click(object sender, EventArgs e)
		{
			List<int> selectedConfigurationServiceGroupIds = this.GetSelectedConfigurationServiceGroupIds();
			if (selectedConfigurationServiceGroupIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification = new ConfigurationServiceGroupQuerySpecification() { IdList = selectedConfigurationServiceGroupIds };
				VwMapConfigurationServiceGroupCollection configurationServiceGroups = VwMapConfigurationServiceGroupController.Fetch(configurationServiceGroupQuerySpecification);
				int groupTypeId = configurationServiceGroups[0].ConfigurationServiceGroupTypeId;
				foreach (VwMapConfigurationServiceGroup configurationServiceGroup in configurationServiceGroups)
				{
					if (groupTypeId != configurationServiceGroup.ConfigurationServiceGroupTypeId)
					{
						WebUtility.ShowAlertBox(this, NOT_SAME_TYPE_MESSAGE);
						return;
					}
				}
				this.EditConfigurationServiceGroups(configurationServiceGroupQuerySpecification);
			}
		}

		protected void btnCopy_Click(object sender, EventArgs e)
		{
			List<int> selectedConfigurationServiceGroupIds = this.GetSelectedConfigurationServiceGroupIds();
			if (selectedConfigurationServiceGroupIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification = new ConfigurationServiceGroupQuerySpecification() { IdList = selectedConfigurationServiceGroupIds };
				this.CopyConfigurationServiceGroups(configurationServiceGroupQuerySpecification);
			}
		}

		protected void btnMultiReplace_Click(object sender, EventArgs e)
		{
			List<int> selectedConfigurationServiceGroupIds = this.GetSelectedConfigurationServiceGroupIds();
			if (selectedConfigurationServiceGroupIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification = new ConfigurationServiceGroupQuerySpecification() { IdList = selectedConfigurationServiceGroupIds };
				var proxyUrls = VwMapConfigurationServiceGroupController.Fetch(configurationServiceGroupQuerySpecification);
				foreach (var proxyUrl in proxyUrls)
				{
					if ((int)ConfigurationServiceGroupStateId.Published != proxyUrl.ConfigurationServiceGroupStatusId)
					{
						WebUtility.ShowAlertBox(this, ONLY_PUBLISHED_MESSAGE);
						return;
					}
				}

				this.EditMultiReplaceConfigurationServiceGroups(configurationServiceGroupQuerySpecification);
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
			List<int> selectedConfigurationServiceGroupIds = this.GetSelectedConfigurationServiceGroupIds();
			if (selectedConfigurationServiceGroupIds.Count == 0)
			{
				WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
			}
			else
			{
				ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification =
					new ConfigurationServiceGroupQuerySpecification() { IdList = selectedConfigurationServiceGroupIds };
				this.ReportConfigurationServiceGroups(configurationServiceGroupQuerySpecification);
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

		private void EditConfigurationServiceGroup(int? configurationServiceGroupId)
		{
			this.Response.Redirect(Global.GetConfigurationServiceGroupUpdatePageUri(configurationServiceGroupId, this.QuerySpecification));
		}

		private void EditConfigurationServiceGroups(ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification)
		{
			this.Response.Redirect(Global.GetConfigurationServiceGroupEditUpdatePageUri(configurationServiceGroupQuerySpecification));
		}

		private void CopyConfigurationServiceGroups(ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification)
		{
			this.Response.Redirect(Global.GetConfigurationServiceGroupCopyPageUri(configurationServiceGroupQuerySpecification));
		}

		private void EditMultiReplaceConfigurationServiceGroups(ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification)
		{
			this.Response.Redirect(Global.GetConfigurationServiceGroupMultiReplaceUpdatePageUri(configurationServiceGroupQuerySpecification));
		}

		private void ReportConfigurationServiceGroups(ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification)
		{
			this.Response.Redirect(Global.GetConfigurationServiceGroupReportPageUri(configurationServiceGroupQuerySpecification));
		}

		private List<int> GetSelectedConfigurationServiceGroupIds()
		{
			return this.gvList.GetSelectedDataKeyValues<int>();
		}

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public ConfigurationServiceGroupQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

			return original as ConfigurationServiceGroupQuerySpecification ?? new ConfigurationServiceGroupQuerySpecification(original);
		}

		#endregion

		#endregion

	}
}