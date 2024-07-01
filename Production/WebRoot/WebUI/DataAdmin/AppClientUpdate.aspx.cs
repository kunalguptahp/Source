using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
    public partial class AppClientUpdate : BaseUpdatePage
    {
        #region BaseRecordUpdatePage Abstract Members

        protected override RecordDetailUserControl RecordDetailControl
        {
            get { return this.ucDetail; }
        }

        protected override void RedirectToUpdatePage(int? dataSourceId)
        {
            this.Response.Redirect(Global.GetAppClientUpdatePageUri(dataSourceId, null));
        }

        protected override string GeneratePageTitle()
        {
            int? dataSourceId = this.DataSourceId;
            return (dataSourceId == null) ? "New AppClient" : string.Format("Update AppClient #{0}", dataSourceId.Value);
        }

        #endregion
    }
}
