using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class EntityTypeList : BaseListPage
	{
		#region Overrides of BaseListPage

		protected override BaseListViewUserControl ListPanel
		{
			get { return this.ucList; }
		}

		protected override string GeneratePageUrl(IQuerySpecification querySpecification)
		{
			return Global.GetEntityTypeListPageUri(querySpecification);
		}

		protected override string GeneratePageTitle()
		{
			return Global.GenerateStandardListPageTitle("Entity Types", this.QuerySpecification);
		}

		#endregion
	}
}