using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class ConfigurationServiceGroupSelectorUpdate : BaseUpdatePage
	{

		#region BaseRecordUpdatePage Abstract Members

		protected override RecordDetailUserControl RecordDetailControl
		{
			get { return this.ucDetail; }
		}

		protected override void RedirectToUpdatePage(int? dataSourceId)
		{
			this.Response.Redirect(Global.GetConfigurationServiceGroupSelectorUpdatePageUri(dataSourceId, null));
		}

		protected override string GeneratePageTitle()
		{
			int? dataSourceId = this.DataSourceId;
			return (dataSourceId == null) ? "New Configuration Service Selector Group" : string.Format("Update Configuration Service Selector Group #{0}", dataSourceId.Value);
		}

		#endregion
	}
}