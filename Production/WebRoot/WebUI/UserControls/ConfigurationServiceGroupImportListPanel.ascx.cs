using System;
using System.Data;
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
using System.Collections.Generic;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
	public partial class ConfigurationServiceGroupImportListPanel : BaseListViewUserControl
	{
        #region Constants

        const string NOT_SELECT_MESSAGE = "There is no row selected.";
        const string NOT_SELECT_GROUPIDONLY_MESSAGE = "You may select rows with a group id only.";
        const string NOT_SAME_TYPE_MESSAGE = "When using multiple edit, all configuration service group types must be the same.";

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
			ConfigurationServiceGroupImportQuerySpecification query = this.ConvertToExpectedType(querySpecification);
			ConfigurationServiceGroupImportQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
			GridView gv = this.Grid;

			//Configure the ObjectDataSource using the current QuerySpecification
			Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
			querySpecificationParameter.DefaultValue = query.ToString();

			//read the query's Conditions info
			this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
			this.txtIdList.Text = query.IdListAsString;
			this.txtName.DisableIf(!string.IsNullOrEmpty(immutableConditions.Name));
			this.txtName.Text = query.Name;
            this.txtDescription.DisableIf(!string.IsNullOrEmpty(immutableConditions.Description));
            this.txtDescription.Text = query.Description;
            this.txtImportStatus.DisableIf(!string.IsNullOrEmpty(immutableConditions.ImportStatus));
            this.txtImportMessage.DisableIf(!string.IsNullOrEmpty(immutableConditions.ImportMessage));
            this.txtImportMessage.Text = query.ImportMessage;
            this.txtImportStatus.Text = query.ImportStatus;
            this.txtLabelValue.DisableIf(!string.IsNullOrEmpty(immutableConditions.LabelValue));
            this.txtLabelValue.Text = query.LabelValue;

			this.ddlStatus.DisableIf(immutableConditions.RowStateId != null);
			if (query.RowStateId == null)
			{
				this.ddlStatus.ClearSelection();
			}
			else
			{
				this.ddlStatus.ForceSelectedValue((RowStatus.RowStatusId)query.RowStateId);
			}

            this.ddlApplication.DisableIf(immutableConditions.ConfigurationServiceApplicationName != null);
            if (query.ConfigurationServiceApplicationName == null)
            {
                this.ddlApplication.ClearSelection();
            }
            else
            {
                this.ddlApplication.ForceSelectedValue(query.ConfigurationServiceApplicationName);
            }

            this.ddlGroupType.DisableIf(immutableConditions.ConfigurationServiceGroupTypeName != null);
            if (query.ConfigurationServiceGroupTypeName == null)
            {
                this.ddlGroupType.ClearSelection();
            }
            else
            {
                this.ddlGroupType.ForceSelectedValue(query.ConfigurationServiceGroupTypeName);
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
            ConfigurationServiceGroupImportQuerySpecification query = new ConfigurationServiceGroupImportQuerySpecification();
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
            query.ConfigurationServiceApplicationName = this.ddlApplication.SelectedValue.TrimToNull();
            query.ConfigurationServiceGroupTypeName = this.ddlGroupType.SelectedValue.TrimToNull();
            query.ImportStatus = this.txtImportStatus.Text.TrimToNull();
            query.ImportMessage = this.txtImportMessage.Text.TrimToNull();
            query.LabelValue = this.txtLabelValue.Text.TrimToNull();
            query.RowStateId = this.ddlStatus.SelectedValue.TryParseInt32();
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
			this.EditConfigurationServiceGroupImport(this.GetRowIdInt32(index));
		}

		protected override void PopulateListControls()
		{
            Global.BindConfigurationServiceApplicationElementsKeyListControl(this.ddlApplication, RowStatus.RowStatusId.Active);
            this.ddlApplication.InsertItem(0, "", Global.GetAllListText());

            Global.BindConfigurationServiceGroupTypeNameListControl(this.ddlGroupType, RowStatus.RowStatusId.Active);
            this.ddlGroupType.InsertItem(0, "", Global.GetAllListText());

            Global.BindRowStatusListControl(this.ddlStatus);
			this.ddlStatus.InsertItem(0, "", Global.GetAllListText());
		}

		protected override void ClearDataControls()
		{
			this.txtIdList.Text = string.Empty;
			this.txtName.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
            this.txtImportMessage.Text = string.Empty;
            this.txtImportStatus.Text = string.Empty;
            this.txtLabelValue.Text = string.Empty;
            this.ddlGroupType.ClearSelection();
            this.ddlApplication.ClearSelection();
            this.ddlStatus.ClearSelection();

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
            this.ExportData(querySpecification, exportFormat, filename, Global.YieldDataTablesForDataExport<VwMapConfigurationServiceGroupImport, VwMapConfigurationServiceGroupImportCollection, ConfigurationServiceGroupImportQuerySpecification>(querySpecification, VwMapConfigurationServiceGroupImportController.Fetch));
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
        public ConfigurationServiceGroupImportQuerySpecification ConvertedQuerySpecification
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
                this.btnGroupEdit.Visible = false;

                ButtonField colEditButton = this.gvList.Columns[0] as ButtonField;
                colEditButton.Text = "View...";
            }
		}

		#endregion

		#region ControlEvents

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            this.EditConfigurationServiceGroupImport(null);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            List<int> selectedConfigurationServiceGroupImportIds = this.GetSelectedGroupImportIds();
            if (selectedConfigurationServiceGroupImportIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                ConfigurationServiceGroupImportQuerySpecification configurationServiceGroupImportQuerySpecification = new ConfigurationServiceGroupImportQuerySpecification() { IdList = selectedConfigurationServiceGroupImportIds };
                this.EditConfigurationServiceGroupImports(configurationServiceGroupImportQuerySpecification);
            }
        }

        protected void btnGroupEdit_Click(object sender, EventArgs e)
        {
            List<int> selectedConfigurationServiceGroupIds = this.GetSelectedGroupIds();
            if (selectedConfigurationServiceGroupIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_GROUPIDONLY_MESSAGE);
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
                string keyConfigurationServiceGroupId = (keys["ConfigurationServiceGroupId"] != null) ? keys["ConfigurationServiceGroupId"].ToString() : string.Empty;

                if (!string.IsNullOrEmpty(keyConfigurationServiceGroupId))
                {
                    HyperLink configurationServiceGroupHyperlink = e.Row.Cells[6].FindControl("hlConfigurationServiceGroupId") as HyperLink;
                    configurationServiceGroupHyperlink.Text = keyConfigurationServiceGroupId;
                    configurationServiceGroupHyperlink.NavigateUrl = string.Format("/ConfigurationService/ConfigurationServiceGroupUpdate.aspx?id={0}", keyConfigurationServiceGroupId);
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

		private void EditConfigurationServiceGroupImport(int? groupImportId)
		{
			this.Response.Redirect(Global.GetConfigurationServiceGroupImportUpdatePageUri(groupImportId, this.QuerySpecification));
		}

        private void EditConfigurationServiceGroupImports(ConfigurationServiceGroupImportQuerySpecification configurationServiceGroupImportQuerySpecification)
        {
            this.Response.Redirect(Global.GetConfigurationServiceGroupImportEditUpdatePageUri(configurationServiceGroupImportQuerySpecification));
        }

        private void EditConfigurationServiceGroups(ConfigurationServiceGroupQuerySpecification configurationServiceGroupQuerySpecification)
        {
            this.Response.Redirect(Global.GetConfigurationServiceGroupEditUpdatePageUri(configurationServiceGroupQuerySpecification));
        }

		/// <summary>
		/// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
        public ConfigurationServiceGroupImportQuerySpecification ConvertToExpectedType(IQuerySpecification original)
		{
			if (original == null)
			{
				return null;
			}

            return original as ConfigurationServiceGroupImportQuerySpecification ?? new ConfigurationServiceGroupImportQuerySpecification(original);
		}

        private List<int> GetSelectedGroupImportIds()
        {
            return this.gvList.GetSelectedDataKeyValues<int>();
        }

        private List<int> GetSelectedGroupIds()
        {
            List<int> selectedGroupIds = new List<int>();
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                // Retrieve the reference to the checkbox
                CheckBox cb = (CheckBox)gvList.Rows[i].FindControl("CheckBoxButton");
                if (cb == null)
                {
                    return selectedGroupIds;
                }
                if (cb.Checked)
                {
                    DataKey keys = gvList.DataKeys[i];
                    if (keys["ConfigurationServiceGroupId"] != null)
                    {
                        selectedGroupIds.Add((int)keys["ConfigurationServiceGroupId"]);
                    }
                    else
                    {   // user must select with group id only.
                        selectedGroupIds.Clear();
                        return selectedGroupIds;
                    }
                }
            }
            return selectedGroupIds;        
        }
        
        #endregion

		#endregion

	}
}