using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
    public partial class ApplicationUpdate : BaseUpdatePage
    {
        #region BaseRecordUpdatePage Abstract Members

        protected override RecordDetailUserControl RecordDetailControl
        {
            get { return this.ucDetail; }
        }

        protected override void RedirectToUpdatePage(int? dataSourceId)
        {
            this.Response.Redirect(Global.GetApplicationUpdatePageUri(dataSourceId, null));
        }

        protected override string GeneratePageTitle()
        {
            int? dataSourceId = this.DataSourceId;
            return (dataSourceId == null) ? "New Application" : string.Format("Update Application #{0}", dataSourceId.Value);
        }

        #endregion
    }
}
