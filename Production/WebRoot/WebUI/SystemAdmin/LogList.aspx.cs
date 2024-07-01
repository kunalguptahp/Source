using System;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class LogList : BaseListPage
	{
		#region Overrides of BaseListPage

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				//Use a default PageSize of 100 for this page instead of the app-wide default of 20.
				this.QuerySpecification.Paging.PageSize = 100;
			}
			base.Page_Load(sender, e);
		}

		protected override BaseListViewUserControl ListPanel
		{
			get { return this.logList; }
		}

		protected override string GeneratePageUrl(IQuerySpecification querySpecification)
		{
			return Global.GetLogListPageUri(querySpecification);
		}

		protected override string GeneratePageTitle()
		{
			return Global.GenerateStandardListPageTitle("Logs", this.QuerySpecification);
		}

		#endregion
	}
}