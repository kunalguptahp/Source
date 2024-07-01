using System;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class ProxyURLMultiReplaceUpdate : BaseQuerySpecificationPage
	{
		#region Overrides of BaseListPage

		protected override BaseQuerySpecificationEditDataUserControl DetailPanel
		{
			get { return this.ucDetail; }
		}

		protected override string GeneratePageUrl(IQuerySpecification querySpecification)
		{
			return Global.GetProxyURLMultiReplaceUpdatePageUri(querySpecification);
		}

		protected override string GeneratePageTitle()
		{
			return "Multi Replace Redirector";
			//return Global.GenerateStandardListPageTitle("Multi Edit", this.QuerySpecification);
		}

		#endregion
	}
}