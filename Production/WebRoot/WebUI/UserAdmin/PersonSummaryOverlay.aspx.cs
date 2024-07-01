using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class PersonSummaryOverlay : BaseRecordSummaryDetailPage
	{

		#region BaseRecordUpdatePage Abstract Members

		protected override BaseRecordSummaryDetailUserControl RecordSummaryDetailUserControl
		{
			get
			{
				return this.ucDetail;
			}
		}

		#endregion
	}
}