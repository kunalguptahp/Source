using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class PersonUpdate : BaseUpdatePage
	{

		#region BaseRecordUpdatePage Abstract Members

		protected override RecordDetailUserControl RecordDetailControl
		{
			get { return this.ucDetail; }
		}

		protected override void RedirectToUpdatePage(int? dataSourceId)
		{
			this.Response.Redirect(Global.GetPersonUpdatePageUri(dataSourceId, null));
		}

		protected override string GeneratePageTitle()
		{
			int? dataSourceId = this.DataSourceId;
			return (dataSourceId == null) ? "New User" : string.Format("Update User #{0}", dataSourceId.Value);
		}

		#endregion
	}
}