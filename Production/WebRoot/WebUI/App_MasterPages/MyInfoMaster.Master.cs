using System;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Apps.WebUI.MasterPages
{
	public partial class MyInfoMaster : System.Web.UI.MasterPage
	{
		#region Control/Tag alias properties

		public HP.HPFx.Web.UI.Portal.PortalNavigationTree ctrlNavTree
		{
			get { return this.hpNavTree; }
		}

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			//this.ntiMyPersonalInfo.NavigateUrl = Global.GetMyPersonalInfoUri();

			if (!this.IsPostBack)
			{
			}
		}

	}
}