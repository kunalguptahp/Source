using System;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class JumpstationGroupMultiReplaceUpdate : BaseQuerySpecificationPage
	{

		#region Overrides of BaseListPage

		protected override BaseQuerySpecificationEditDataUserControl DetailPanel
		{
			get { return this.ucDetail; }
		}

		protected override string GeneratePageUrl(IQuerySpecification querySpecification)
		{
			return Global.GetJumpstationGroupMultiReplaceUpdatePageUri(querySpecification);
		}

		protected override string GeneratePageTitle()
		{
			return "Multi Replace Jumpstation";
		}

		#endregion

	}
}