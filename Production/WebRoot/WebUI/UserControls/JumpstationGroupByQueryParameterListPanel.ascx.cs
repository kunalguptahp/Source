using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using SubSonic;


namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
    public partial class JumpstationGroupByQueryParameterListPanel : BaseListViewUserControl
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
            GridView gv = this.gvList;

            //Configure the ObjectDataSource using the current QuerySpecification
            int tenantGroupId = PersonController.GetCurrentUser().TenantGroupId;
            Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
            Parameter queryTenantGroupId = this.odsDataSource.SelectParameters[1];
            queryTenantGroupId.DefaultValue = tenantGroupId.ToString();
            querySpecificationParameter.DefaultValue = query.ToString();

            this.ddlJumpstationGroupType.DisableIf(immutableConditions.JumpstationGroupTypeId != null);
            if (query.JumpstationGroupTypeId == null)
            {
                this.ddlJumpstationGroupType.ClearSelection();
            }
            else
            {
                this.ddlJumpstationGroupType.ForceSelectedValue(query.JumpstationGroupTypeId);
                this.ddlJumpstationGroupType_SelectedIndexChanged();
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

            if (query.Paging.PageSize != null)
            {
                this.ddlItemsPerPage.ForceSelectedValue(query.Paging.PageSize);
            }

            //read the query's Paging.PageSize info
            gv.PageSize = query.Paging.PageSize ?? Convert.ToInt32(this.ddlItemsPerPage.SelectedValue);

            //read the query's Paging.PageIndex info last (since sorting resets the PageIndex to 0)
            int pageIndex = query.Paging.PageIndex ?? 0;
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
            query.JumpstationGroupTypeId = this.ddlJumpstationGroupType.SelectedValue.TryParseInt32();
            query.JumpstationGroupStatusId = this.ddlJumpstationGroupStatus.SelectedValue.TryParseInt32();
            query.QueryParameterValueIdDelimitedList = this.CreateQueryParameterValueIdDelimitedList();

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
            Global.BindTenantJumpstationGroupTypeListControl(this.ddlJumpstationGroupType, RowStatus.RowStatusId.Active,tenantId);
            this.ddlJumpstationGroupType.InsertItem(0, "", Global.GetAllListText());

            Global.BindJumpstationGroupStatusListControl(this.ddlJumpstationGroupStatus, RowStatus.RowStatusId.Active);
            this.ddlJumpstationGroupStatus.InsertItem(0, "", Global.GetAllListText());
        }

        protected override void ClearDataControls()
        {
            this.ddlJumpstationGroupType.ClearSelection();
            this.ddlJumpstationGroupStatus.ClearSelection();
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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataKey keys = gvList.DataKeys[e.Row.RowIndex];
                string keyPid = (keys.Values["ProductionId"] != null) ? keys.Values["ProductionId"].ToString() : string.Empty;
                if (!string.IsNullOrEmpty(keyPid))
                {
                    VwMapJumpstationGroup jumpstationGroup = VwMapJumpstationGroupController.FetchByID(keys.Values["Id"])[0];
                    string keyPDomain = jumpstationGroup.PublicationDomain != null ? jumpstationGroup.PublicationDomain.ToString() : string.Empty;
                    string keyQueryString = jumpstationGroup.QueryString != null ? jumpstationGroup.QueryString.ToString() : string.Empty;
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

                string keyTargetURL = (keys.Values["TargetURL"] != null) ? keys.Values["TargetURL"].ToString() : string.Empty;
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
                
                //added by fangsh
                //Add selected values in "selected values" colnum
                Label label1 = (Label)e.Row.Cells[9].FindControl("label1");
                label1.Text = "";
                JumpstationGroupQuerySpecification query = this.ConvertedQuerySpecification;
                string delimitedList = query.QueryParameterValueIdDelimitedList;
                if (delimitedList != null)
                {
                    string[] queryParameterValueList = delimitedList.Split(',');
                    List<string> newqueryParameterValueList = new List<string>();
                    foreach (string queryParameterValue in queryParameterValueList)
                    {
                        if (!String.IsNullOrEmpty(queryParameterValue))
                        {
                            newqueryParameterValueList.Add(queryParameterValue);
                        }
                    }
                    if (newqueryParameterValueList.Count > 0)
                    {
                        QueryParameterValue queryParameterFirst = QueryParameterValue.FetchByID(Convert.ToInt32(newqueryParameterValueList[0]));
                        string queryParameterName = queryParameterFirst.QueryParameter.Name.ToString().Replace(" ", "");
                        string queryParameterValue = queryParameterFirst.Name.ToString();
                        string selectedValue = string.Format("{0}:{1}", queryParameterName, queryParameterValue);
                        label1.Text = selectedValue;
                        if (newqueryParameterValueList.Count > 1)
                        {
                            for (int i = 2; i <= newqueryParameterValueList.Count; i++)
                            {
                                QueryParameterValue queryParameterNext = QueryParameterValue.FetchByID(Convert.ToInt32(newqueryParameterValueList[i - 1]));
                                string queryParameterNameNext = queryParameterNext.QueryParameter.Name.ToString();
                                string queryParameterValueNext = queryParameterNext.Name.ToString();
                                string selectedValueNext = string.Format("{0}:{1}", queryParameterNameNext, queryParameterValueNext);
                                label1.Text += ";" + "\r\n" + selectedValueNext;
                            }
                        }
                    }
                }
            }
        }

        protected void ddlJumpstationGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            JumpstationGroupQuerySpecification query = this.ConvertedQuerySpecification;
            query.QueryParameterValueIdDelimitedList = null;
            query.JumpstationGroupTypeId = this.ddlJumpstationGroupType.SelectedValue.TryParseInt32();
            this.ddlJumpstationGroupType_SelectedIndexChanged();
        }

        private void ddlJumpstationGroupType_SelectedIndexChanged()
        {
            QueryParameterJumpstationGroupTypeQuerySpecification queryParmeterJumpstationGroupTypeQuerySpecification = new QueryParameterJumpstationGroupTypeQuerySpecification() { JumpstationGroupTypeId = (ddlJumpstationGroupType.SelectedValue == "" ? 0 : Convert.ToInt32(ddlJumpstationGroupType.SelectedValue)) };
            VwMapQueryParameterJumpstationGroupTypeCollection queryParameterColl = VwMapQueryParameterJumpstationGroupTypeController.Fetch(queryParmeterJumpstationGroupTypeQuerySpecification);
            queryParameterColl.Sort("QueryParameterName", true);
            this.gvQueryParameterList.DataSource = queryParameterColl;
            //this.gvQueryParameterList.DataBind();
        }

        protected void gvQueryParameterList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlParameterValue = (DropDownList)e.Row.FindControl("ddlParameterValue");
                Global.BindQueryParameterValueListControl(ddlParameterValue, Convert.ToInt32(e.Row.Cells[0].Text), RowStatus.RowStatusId.Active);
                ddlParameterValue.InsertItem(0, "", Global.GetSelectListText());

                JumpstationGroupQuerySpecification query = this.ConvertedQuerySpecification;
                string delimitedList = query.QueryParameterValueIdDelimitedList;
                if (delimitedList != null)
                {
                    string[] queryParameterValueList = delimitedList.Split(',');
                    if ((e.Row.RowIndex <= queryParameterValueList.Length) && (queryParameterValueList[e.Row.RowIndex] != ""))
                    {
                        ddlParameterValue.ForceSelectedValue(queryParameterValueList[e.Row.RowIndex]);
                    }
                }
            }
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

        /// <summary>
        /// Create query parameter value delimited list
        /// </summary>
        /// <returns></returns>
        private string CreateQueryParameterValueIdDelimitedList()
        {
            string delimitedList = null;

            // There must be at least one selected value to save the query value parameters.
            foreach (GridViewRow row in this.gvQueryParameterList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlParameterValue = (DropDownList)row.FindControl("ddlParameterValue");
                    //if (ddlParameterValue.SelectedValue != "")
                    //{
                    delimitedList += (delimitedList == null) ? ddlParameterValue.SelectedValue : "," + ddlParameterValue.SelectedValue;
                    //}
                }
            }
            return delimitedList;
        }

        #endregion

        #endregion

    }
}