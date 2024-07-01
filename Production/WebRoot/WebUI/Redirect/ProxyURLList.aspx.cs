using System;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Data.Query;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class ProxyURLList : BaseListPage
	{
		//#region PageEvents

		//protected override void Page_Load(object sender, EventArgs e)
		//{
		//    if (!Page.IsPostBack)
		//    {
		//        this.UpdateChildListQueryConditions();
		//    }
		//    base.Page_Load(sender, e);
		//}

		//#endregion

		#region Overrides of BaseListPage

		protected override BaseListViewUserControl ListPanel
		{
			get { return this.ucList; }
		}

		protected override string GeneratePageUrl(IQuerySpecification querySpecification)
		{
			return Global.GetProxyURLListPageUri(querySpecification);
		}

		protected override string GeneratePageTitle()
		{
			return Global.GenerateStandardListPageTitle("Redirector", this.QuerySpecification);
		}

		#endregion


		//#region Methods

		//private void UpdateChildListQueryConditions()
		//{
		//    this.UpdateTaskListForManufacturingQueryConditions();
		//}

		//private void UpdateTaskListForManufacturingQueryConditions()
		//{
		//    string s = System.Web.HttpUtility.UrlDecode(Request["query"]);
		//    if (string.IsNullOrEmpty(s))
		//    {
		//        Person currUser = PersonController.GetCurrentUser();
		//        this.ucList.QuerySpecification = new ProxyURLQuerySpecification() { OwnerId = currUser.Id };
		//        this.ucList.DataBind();				
		//    }
		//}

		//#endregion

	}
}