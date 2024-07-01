using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class WorkflowModuleCategoryList : BaseListPage
	{
		#region Overrides of BaseListPage

		protected override BaseListViewUserControl ListPanel
		{
			get { return this.ucList; }
		}

		protected override string GeneratePageUrl(IQuerySpecification querySpecification)
		{
			return Global.GetWorkflowModuleCategoryListPageUri(querySpecification);
		}

		protected override string GeneratePageTitle()
		{
			return Global.GenerateStandardListPageTitle("Module Category", this.QuerySpecification);
		}

		#endregion
	}
}