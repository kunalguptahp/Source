using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class JumpstationMacroValueUpdate : BaseUpdatePage
	{

		#region BaseRecordUpdatePage Abstract Members

	    protected override RecordDetailUserControl RecordDetailControl
	    {
		   get { return this.ucDetail; }
	    }

		protected override void RedirectToUpdatePage(int? dataSourceId)
		{
			this.Response.Redirect(Global.GetJumpstationMacroValueUpdatePageUri(dataSourceId, null));
		}

		protected override string GeneratePageTitle()
		{
			int? dataSourceId = this.DataSourceId;
			return (dataSourceId == null) ? "New Macro Value" : string.Format("Update Macro Value #{0}", dataSourceId.Value);
		}

		#endregion

    }
}