using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class ProxyURLUpdate : BaseUpdatePage
	{

		#region BaseRecordUpdatePage Abstract Members

		protected override RecordDetailUserControl RecordDetailControl
		{
			get { return this.ucDetail; }
		}

		protected override void RedirectToUpdatePage(int? dataSourceId)
		{
			this.Response.Redirect(Global.GetProxyURLUpdatePageUri(dataSourceId, null));
		}

		protected override string GeneratePageTitle()
		{
			int? dataSourceId = this.DataSourceId;
			return (dataSourceId == null) ? "New Redirector" : string.Format("Update Redirector #{0}", dataSourceId.Value);
		}

		#endregion
	}
}