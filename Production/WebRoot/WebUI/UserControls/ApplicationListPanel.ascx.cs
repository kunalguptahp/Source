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
using System;
using System.Data;

namespace HP.ElementsCPS.Apps.WebUI.UserControls
{
    public partial class ApplicationListPanel : BaseListViewUserControl
    {
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
            ApplicationQuerySpecification query = this.ConvertToExpectedType(querySpecification);
            ApplicationQuerySpecification immutableConditions = this.ConvertToExpectedType(this.ImmutableQueryConditions);
            GridView gv = this.Grid;

            //Configure the ObjectDataSource using the current QuerySpecification
            Parameter querySpecificationParameter = this.odsDataSource.SelectParameters[0];
            querySpecificationParameter.DefaultValue = query.ToString();

            //read the query's Conditions info
            this.txtIdList.DisableIf(!string.IsNullOrEmpty(immutableConditions.IdListAsString));
            this.txtIdList.Text = query.IdListAsString;
            this.txtName.DisableIf(!string.IsNullOrEmpty(immutableConditions.Name));
            this.txtName.Text = query.Name;

            this.ddlStatus.DisableIf(immutableConditions.RowStateId != null);
            if (query.RowStateId == null)
            {
                this.ddlStatus.ClearSelection();
            }
            else
            {
                this.ddlStatus.ForceSelectedValue((RowStatus.RowStatusId)query.RowStateId);
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
            ApplicationQuerySpecification query = new ApplicationQuerySpecification();
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
            this.EditApplication(this.GetRowIdInt32(index));
        }

        protected override void PopulateListControls()
        {
            Global.BindRowStatusListControl(this.ddlStatus);
            this.ddlStatus.InsertItem(0, "", Global.GetAllListText());
        }

        protected override void ClearDataControls()
        {
            this.txtIdList.Text = string.Empty;
            this.txtName.Text = string.Empty;
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
            this.ExportData(querySpecification, exportFormat, filename, Global.YieldDataTablesForDataExport<VwMapApplication, VwMapApplicationCollection, ApplicationQuerySpecification>(querySpecification, VwMapApplicationController.Fetch));
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
        public ApplicationQuerySpecification ConvertedQuerySpecification
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
        }

        #endregion

        #region ControlEvents

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            this.EditApplication(null);
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

        #endregion

        #region Methods

        #region Helper Methods

        private void EditApplication(int? id)
        {
            this.Response.Redirect(Global.GetApplicationUpdatePageUri(id, this.QuerySpecification));
        }

        /// <summary>
        /// Casts or converts the specified <see cref="IQuerySpecification"/> to the <see cref="QuerySpecificationWrapper"/> (sub)type that this control prefers/expects.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public ApplicationQuerySpecification ConvertToExpectedType(IQuerySpecification original)
        {
            if (original == null)
            {
                return null;
            }

            return original as ApplicationQuerySpecification ?? new ApplicationQuerySpecification(original);
        }

        #endregion

        #endregion
    }
}