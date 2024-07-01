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
    public partial class JumpstationGroupListPanel : BaseListViewUserControl
    {
        const string NOT_SELECT_MESSAGE = "There is no row selected.";
        const string NOT_SAME_TYPE_MESSAGE = "When using multiple edit, all Jumpstation types must be the same.";
        const string ONLY_PUBLISHED_MESSAGE = "To replace Jumpstation(s), all selected must be published.";
       
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
            JumpstationGroupQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            JumpstationGroupQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
            GridView gv = this.Grid;

            //Configure the ObjectDataSource using the current QuerySpecification
            //string windowsId = this.Page.User.Identity.Name.ToString();
            
            //int tenantGroupId = PersonController.FetchByWindowsID(windowsId)[0].TenantGroupId;
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
            this.txtTargetURL.DisableIf(!string.IsNullOrEmpty(immutableConditions.TargetURL));
            this.txtTargetURL.Text = query.TargetURL;
            this.txtTagsFilter.DisableIf(!string.IsNullOrEmpty(immutableConditions.Tags));
            this.txtTagsFilter.Text = query.Tags;

            this.ddlJumpstationApplication.DisableIf(immutableConditions.JumpstationApplicationId != null);
            if (query.JumpstationApplicationId == null)
            {
                this.ddlJumpstationApplication.ClearSelection();
            }
            else
            {
                this.ddlJumpstationApplication.ForceSelectedValue(query.JumpstationApplicationId);
               // this.ddlJumpstationApplication.ForceSelectedValue(JumpstationApplicationController.FetchByID(query.JumpstationApplicationId)[0].Name);
            }

            this.ddlJumpstationGroupType.DisableIf(immutableConditions.JumpstationGroupTypeId != null);
            if (query.JumpstationGroupTypeId == null)
            {
                this.ddlJumpstationGroupType.ClearSelection();
            }
            else
            {
                this.ddlJumpstationGroupType.ForceSelectedValue(query.JumpstationGroupTypeId);
                //this.ddlJumpstationGroupType.ForceSelectedValue(JumpstationGroupTypeController.FetchByID(query.JumpstationGroupTypeId)[0].Name);
            }

            this.ddlJumpstationGroupStatus.DisableIf(immutableConditions.JumpstationGroupStatusId != null);
            if (query.JumpstationGroupStatusId == null)
            {
                this.ddlJumpstationGroupStatus.ClearSelection();
            }
            else
            {
                this.ddlJumpstationGroupStatus.ForceSelectedValue(query.JumpstationGroupStatusId);
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

            this.ddlBrand.DisableIf(immutableConditions.BrandQueryParameterValue != null);
            if (query.BrandQueryParameterValue == null)
            {
                this.ddlBrand.ClearSelection();
            }
            else
            {
                this.ddlBrand.ForceSelectedValue(query.BrandQueryParameterValue);
            }

            this.ddlCycle.DisableIf(immutableConditions.CycleQueryParameterValue != null);
            if (query.CycleQueryParameterValue == null)
            {
                this.ddlCycle.ClearSelection();
            }
            else
            {
                this.ddlCycle.ForceSelectedValue(query.CycleQueryParameterValue);
            }

            this.ddlLocale.DisableIf(immutableConditions.LocaleQueryParameterValue != null);
            if (query.LocaleQueryParameterValue == null)
            {
                this.ddlLocale.ClearSelection();
            }
            else
            {
                this.ddlLocale.ForceSelectedValue(query.LocaleQueryParameterValue);
            }

            this.ddlPartnerCategory.DisableIf(immutableConditions.PartnerCategoryQueryParameterValue != null);
            if (query.PartnerCategoryQueryParameterValue == null)
            {
                this.ddlPartnerCategory.ClearSelection();
            }
            else
            {
                this.ddlPartnerCategory.ForceSelectedValue(query.PartnerCategoryQueryParameterValue);
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

            this.ddlTouchpoint.DisableIf(immutableConditions.TouchpointQueryParameterValue != null);
            if (query.TouchpointQueryParameterValue == null)
            {
                this.ddlTouchpoint.ClearSelection();
            }
            else
            {
                this.ddlTouchpoint.ForceSelectedValue(query.TouchpointQueryParameterValue);
            }

            this.ddlAppClient.DisableIf(immutableConditions.AppClientId!=null);
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
            JumpstationGroupQuerySpecification query = new JumpstationGroupQuerySpecification();
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
            query.TargetURL = this.txtTargetURL.Text.TrimToNull();
            query.JumpstationApplicationId = this.ddlJumpstationApplication.SelectedValue.TryParseInt32();
            
            query.ValidationId = this.txtValidationId.Text.TryParseInt32();
            query.ProductionId = this.txtPublicationId.Text.TryParseInt32();
            query.Tags = this.txtTagsFilter.Text.TrimToNull();
            query.JumpstationGroupTypeId = this.ddlJumpstationGroupType.SelectedValue.TryParseInt32();
            
            query.JumpstationGroupStatusId = this.ddlJumpstationGroupStatus.SelectedValue.TryParseInt32();
            query.OwnerId = this.ddlOwner.SelectedValue.TryParseInt32();

            query.BrandQueryParameterValue = this.ddlBrand.SelectedValue.TrimToNull();
            query.CycleQueryParameterValue = this.ddlCycle.SelectedValue.TrimToNull();
            query.LocaleQueryParameterValue = this.ddlLocale.SelectedValue.TrimToNull();
            query.PartnerCategoryQueryParameterValue = this.ddlPartnerCategory.SelectedValue.TrimToNull();
            query.PlatformQueryParameterValue = this.ddlPlatform.SelectedValue.TrimToNull();
            query.TouchpointQueryParameterValue = this.ddlTouchpoint.SelectedValue.TrimToNull();
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
            this.EditJumpstationGroup(this.GetRowIdInt32(index));
        }

        protected override void PopulateListControls()
        {
             string windowsId = this.Page.User.Identity.Name.ToString();
            int tenantId = PersonController.GetCurrentUser().TenantGroupId;
            Global.BindJumpstationApplicationListControl(this.ddlJumpstationApplication, RowStatus.RowStatusId.Active,tenantId,true);
            this.ddlJumpstationApplication.InsertItem(0, "", Global.GetAllListText());

            Global.BindTenantJumpstationGroupTypeListControl(this.ddlJumpstationGroupType, RowStatus.RowStatusId.Active,tenantId);
            this.ddlJumpstationGroupType.InsertItem(0, "", Global.GetAllListText());

            Global.BindJumpstationGroupStatusListControl(this.ddlJumpstationGroupStatus, RowStatus.RowStatusId.Active);
            this.ddlJumpstationGroupStatus.InsertItem(0, "", Global.GetAllListText());

            Global.BindPersonListControl(this.ddlOwner, RowStatus.RowStatusId.Active);
            this.ddlOwner.InsertItem(0, "", Global.GetAllListText());

            Global.BindQueryParameterValueNameListControl(this.ddlBrand, (int)QueryParameter.QueryParameterId.Brand, RowStatus.RowStatusId.Active);
            this.ddlBrand.InsertItem(0, "", Global.GetAllListText());

            Global.BindQueryParameterValueNameListControl(this.ddlCycle, (int)QueryParameter.QueryParameterId.Cycle, RowStatus.RowStatusId.Active);
            this.ddlCycle.InsertItem(0, "", Global.GetAllListText());

            Global.BindQueryParameterValueNameListControl(this.ddlLocale, (int)QueryParameter.QueryParameterId.Locale, RowStatus.RowStatusId.Active);
            this.ddlLocale.InsertItem(0, "", Global.GetAllListText());

            Global.BindQueryParameterValueNameListControl(this.ddlPartnerCategory, (int)QueryParameter.QueryParameterId.PartnerCategory, RowStatus.RowStatusId.Active);
            this.ddlPartnerCategory.InsertItem(0, "", Global.GetAllListText());

            Global.BindQueryParameterValueNameListControl(this.ddlPlatform, (int)QueryParameter.QueryParameterId.Platform, RowStatus.RowStatusId.Active);
            this.ddlPlatform.InsertItem(0, "", Global.GetAllListText());

            Global.BindQueryParameterValueNameListControl(this.ddlTouchpoint, (int)QueryParameter.QueryParameterId.Touchpoint, RowStatus.RowStatusId.Active);
            this.ddlTouchpoint.InsertItem(0, "", Global.GetAllListText());

            Global.BindAppClientListControl(this.ddlAppClient);
            this.ddlAppClient.InsertItem(0, "", Global.GetAllListText());
        }

        protected override void ClearDataControls()
        {
            this.txtIdList.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
            this.txtTargetURL.Text = string.Empty;
            this.txtTagsFilter.Text = string.Empty;
            this.txtValidationId.Text = string.Empty;
            this.txtPublicationId.Text = string.Empty;
            this.ddlJumpstationApplication.ClearSelection();
            this.ddlJumpstationGroupType.ClearSelection();
            this.ddlJumpstationGroupStatus.ClearSelection();
            this.ddlOwner.ClearSelection();
            this.ddlBrand.ClearSelection();
            this.ddlCycle.ClearSelection();
            this.ddlLocale.ClearSelection();
            this.ddlPartnerCategory.ClearSelection();
            this.ddlPlatform.ClearSelection();
            this.ddlTouchpoint.ClearSelection();
            this.ddlAppClient.ClearSelection();
            this.txtCreatedAfter.Text = string.Empty;
            this.txtCreatedBefore.Text = string.Empty;
            this.txtCreatedBy.Text = string.Empty;
            this.txtModifiedAfter.Text = string.Empty;
            this.txtModifiedBefore.Text = string.Empty;
            this.txtModifiedBy.Text = string.Empty;
        }

        protected override void ExportData(IQuerySpecification querySpecification, string exportFormat, string filename)
        {
            JumpstationGroupQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            int startRowIndex = query.Paging.PagingStartRowIndex ?? 0;
            int totalRowCount = Math.Min(Global.ExportFileMaxRowCount, query.Paging.PageSize ?? int.MaxValue);
            //merge the Export paging settings/threshholds into the Paging settings of (a copy of) the QuerySpecification
            JumpstationGroupQuerySpecification qs = QuerySpecificationWrapper.CopyAs<JumpstationGroupQuerySpecification>(query);
            qs.Paging.SetPageIndexByStartRowIndex(totalRowCount, startRowIndex);
            //NOTE: DO NOT invoke the base class' implementation
            //IEnumerable<DataTable> dataTable = Global.YieldDataTablesForDataExport<VwMapJumpstationGroup, VwMapJumpstationGroupCollection, JumpstationGroupQuerySpecification>(querySpecification, VwMapJumpstationGroupController.Fetch);

            int tenantGroupId = PersonController.GetCurrentUser().TenantGroupId;
            List<int> appClientIds = TenantController.GetAppClientsInt(tenantGroupId);
            qs.AppClientIds = appClientIds;
            DataTable newDataTable = VwMapJumpstationGroupController.FetchToDataTable(qs);
            DataColumn sourceUrlCol = new DataColumn();
            sourceUrlCol.DataType = System.Type.GetType("System.String");
            sourceUrlCol.ColumnName = "SourceUrl";
            newDataTable.Columns.Add(sourceUrlCol);
            newDataTable.Columns["SourceUrl"].Expression = "PublicationDomain+'?'+QueryString";
         
            string fullFileName = filename.EndsWith(".xls") ? filename : (filename + ".xls");
            string strHeaderText = "Jumpstation List ";
            List<int> num0 = new List<int> { 0, 1, 2, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 21, 22, 23, 25, 27, 28, 30, 31 };
            NPOIHelper.ExportByWeb(newDataTable, strHeaderText, fullFileName, num0);

        }

        #endregion

        #endregion

        #region Properties

        #region Convenience Properties

        /// <summary>
        /// Returns the result of invoking <see cref="ConvertToExpectedType"/> on this instance's <see cref="QuerySpecification"/>.
        /// </summary>
        public JumpstationGroupQuerySpecification ConvertedQuerySpecification
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

                ButtonField colEditButton = this.gvList.Columns[0] as ButtonField;
                colEditButton.Text = "View...";
            }
        }

        #endregion

        #region ControlEvents

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            this.EditJumpstationGroup(null);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            List<int> selectedJumpstationGroupIds = this.GetSelectedJumpstationGroupIds();
            if (selectedJumpstationGroupIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                JumpstationGroupQuerySpecification jumpstationGroupQuerySpecification = new JumpstationGroupQuerySpecification() { IdList = selectedJumpstationGroupIds };
                VwMapJumpstationGroupCollection jumpstationGroups = VwMapJumpstationGroupController.Fetch(jumpstationGroupQuerySpecification);
                int groupTypeId = jumpstationGroups[0].JumpstationGroupTypeId;
                foreach (VwMapJumpstationGroup jumpstationGroup in jumpstationGroups)
                {
                    if (groupTypeId != jumpstationGroup.JumpstationGroupTypeId)
                    {
                        WebUtility.ShowAlertBox(this, NOT_SAME_TYPE_MESSAGE);
                        return;
                    }
                }
                this.EditJumpstationGroups(jumpstationGroupQuerySpecification);
            }
        }

        protected void btnMultiReplace_Click(object sender, EventArgs e)
        {
            List<int> selectedJumpstationGroupIds = this.GetSelectedJumpstationGroupIds();
            if (selectedJumpstationGroupIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                JumpstationGroupQuerySpecification jumpstationGroupQuerySpecification = new JumpstationGroupQuerySpecification() { IdList = selectedJumpstationGroupIds };
                var jumpstationGroups = VwMapJumpstationGroupController.Fetch(jumpstationGroupQuerySpecification);
                foreach (var jumpstationGroup in jumpstationGroups)
                {
                    if ((int)JumpstationGroupStateId.Published != jumpstationGroup.JumpstationGroupStatusId)
                    {
                        WebUtility.ShowAlertBox(this, ONLY_PUBLISHED_MESSAGE);
                        return;
                    }
                }

                this.EditMultiReplaceJumpstationGroups(jumpstationGroupQuerySpecification);
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
            List<int> selectedJumpstationGroupIds = this.GetSelectedJumpstationGroupIds();
            if (selectedJumpstationGroupIds.Count == 0)
            {
                WebUtility.ShowAlertBox(this, NOT_SELECT_MESSAGE);
            }
            else
            {
                JumpstationGroupQuerySpecification jumpstationGroupQuerySpecification =
                    new JumpstationGroupQuerySpecification() { IdList = selectedJumpstationGroupIds };
                this.ReportJumpstationGroups(jumpstationGroupQuerySpecification);
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

        //protected void ObjectDataSource1_Filtering(object sender, ObjectDataSourceFilteringEventArgs e)
        //{
        //    string windowsId = this.Page.User.Identity.Name.ToString();
        //    List<AppClientId>  appClientIds = PersonController.FetchByWindowsID(windowsId)[0].TenantGroup.GetAppClients();
        //    foreach (AppClientId appClient in appClientIds)
        //    {
        //        e.ParameterValues.Clear();
        //        e.ParameterValues.Add("AppClientId", appClient);
        //    }
        //   // this.odsDataSource.FilterParameters["AppClientId"].DefaultValue= "AppClientId=" +appClientIds[0];
        //}

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataKey keys = gvList.DataKeys[e.Row.RowIndex];
                string keyPid = (keys["ProductionId"] != null) ? keys["ProductionId"].ToString() : string.Empty;
                if (!string.IsNullOrEmpty(keyPid))
                {
                    string keyPDomain = (keys["PublicationDomain"] != null) ? keys["PublicationDomain"].ToString() : string.Empty;
                    string keyQueryString = (keys["QueryString"] != null) ? keys["QueryString"].ToString() : string.Empty;
                    HyperLink sourceURLHyperlink = e.Row.Cells[2].FindControl("hlSourceURL") as HyperLink;
                    string urlHyperlink = string.Format("{0}?{1}", keyPDomain, HttpUtility.HtmlDecode(keyQueryString));
                    if (urlHyperlink.Length > 70 && urlHyperlink.Length <= 140)
                    {
                        sourceURLHyperlink.Text = urlHyperlink.Substring(0, 70) + "\r\n" + urlHyperlink.Substring(70);
                    }
                    else if (urlHyperlink.Length > 140)
                    {
                        sourceURLHyperlink.Text = urlHyperlink.Substring(0, 70) + "\r\n" + urlHyperlink.Substring(70, 70) + "\r\n" + urlHyperlink.Substring(140);
                    }
                    else
                    {
                        sourceURLHyperlink.Text = urlHyperlink;
                    }
                    sourceURLHyperlink.NavigateUrl = urlHyperlink;
                }

                string keyTargetURL = (keys["TargetURL"] != null) ? keys["TargetURL"].ToString() : string.Empty;
                HyperLink targetURLHyperlink = e.Row.Cells[3].FindControl("hlTargetURL") as HyperLink;
                if (keyTargetURL.Length > 50)
                {
                    targetURLHyperlink.Text = keyTargetURL.Substring(0, 50) + "...";
                }
                else
                {
                    targetURLHyperlink.Text = keyTargetURL;
                }
                targetURLHyperlink.NavigateUrl = keyTargetURL;
            }
        }

        protected void cvTxtIdList_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Global.ValidateCommaSeparatedIdList(this.txtIdList.Text.ToString());
        }

        #endregion

        #region Methods

        #region Helper Methods

        private void EditJumpstationGroup(int? jumpstationGroupId)
        {
            this.Response.Redirect(Global.GetJumpstationGroupUpdatePageUri(jumpstationGroupId, this.QuerySpecification));
        }

        private void EditJumpstationGroups(JumpstationGroupQuerySpecification jumpstationGroupQuerySpecification)
        {
            this.Response.Redirect(Global.GetJumpstationGroupEditUpdatePageUri(jumpstationGroupQuerySpecification));
        }

        private void EditMultiReplaceJumpstationGroups(JumpstationGroupQuerySpecification jumpstationGroupQuerySpecification)
        {
            this.Response.Redirect(Global.GetJumpstationGroupMultiReplaceUpdatePageUri(jumpstationGroupQuerySpecification));
        }

        private void ReportJumpstationGroups(JumpstationGroupQuerySpecification jumpstationGroupQuerySpecification)
        {
            this.Response.Redirect(Global.GetJumpstationGroupReportPageUri(jumpstationGroupQuerySpecification));
        }

        private List<int> GetSelectedJumpstationGroupIds()
        {
            return this.gvList.GetSelectedDataKeyValues<int>();
        }

        /// <summary>
        /// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public JumpstationGroupQuerySpecification ConvertToExpectedType(IQuerySpecification original)
        {
            if (original == null)
            {
                return null;
            }

            return original as JumpstationGroupQuerySpecification ?? new JumpstationGroupQuerySpecification(original);
        }

        #endregion

        #endregion

    }
}