using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class ConfigurationServiceGroupUpdate : BaseUpdatePage
	{

		#region BaseRecordUpdatePage Abstract Members

		protected override RecordDetailUserControl RecordDetailControl
		{
			get { return this.ucDetail; }
		}

		protected override void RedirectToUpdatePage(int? dataSourceId)
		{
			this.Response.Redirect(Global.GetConfigurationServiceGroupUpdatePageUri(dataSourceId, null));
		}

		protected override string GeneratePageTitle()
		{
			int? dataSourceId = this.DataSourceId;
			return (dataSourceId == null) ? "New Configuration Service Group" : string.Format("Update Configuration Service Group #{0}", dataSourceId.Value);
		}

		#endregion
	}
}