using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class NoteTypeList : BaseListPage
	{
		#region Overrides of BaseListPage

		protected override BaseListViewUserControl ListPanel
		{
			get { return this.ucList; }
		}

		protected override string GeneratePageUrl(IQuerySpecification querySpecification)
		{
			return Global.GetNoteTypeListPageUri(querySpecification);
		}

		protected override string GeneratePageTitle()
		{
			return Global.GenerateStandardListPageTitle("Note Types", this.QuerySpecification);
		}

		#endregion
	}
}