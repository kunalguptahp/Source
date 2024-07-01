using System;

namespace HP.ElementsCPS.Apps.WebUI.MasterPages
{
	public partial class ConfigurationServiceMaster : System.Web.UI.MasterPage
	{
		#region Control/Tag alias properties

		public HP.HPFx.Web.UI.Portal.PortalNavigationTree ctrlNavTree
		{
			get { return this.hpNavTree; }
		}

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
                this.ntnUsageTrackingInfo.Visible = Global.IsCFSMetaDataModificationAllowed();
                this.ntSpacer1.Visible = Global.IsCFSMetaDataModificationAllowed();
                this.ntSeparator1.Visible = Global.IsCFSMetaDataModificationAllowed();

                this.ntnReach.Visible = Global.IsREAMetaDataModificationAllowed();

			}
		}

	}
}
