using System;
using System.Configuration;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Core.Security;
using System.Collections.Generic;

namespace HP.ElementsCPS.Apps.WebUI.MasterPages
{
    public partial class DataAdminMaster : System.Web.UI.MasterPage
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
                this.ntnRedirectInfo.Visible = Global.IsJSDataModificationAllowed();
                this.ntSpacer1.Visible = Global.IsJSDataModificationAllowed();
                this.ntSeparator1.Visible = Global.IsJSDataModificationAllowed();

                this.ntnConfigurationServiceInfo.Visible = Global.IsCFSDataModificationAllowed();
                this.PortalNavigationSpacer1.Visible = Global.IsCFSDataModificationAllowed();
                this.PortalNavigationSeparator2.Visible = Global.IsCFSDataModificationAllowed();

                this.ntnJumpstation.Visible = Global.IsJSDataModificationAllowed();
                this.ntSpacer2.Visible = Global.IsJSDataModificationAllowed();
                this.ntSeparator2.Visible = Global.IsJSDataModificationAllowed();

                this.ntWorkflowInfo.Visible = Global.IsREADataModificationAllowed();
                this.ntSpacer3.Visible = Global.IsREADataModificationAllowed();
                this.ntSeparator3.Visible = Global.IsREADataModificationAllowed();

                this.ntnCPSInfo.Visible = Global.IsSysDataModificationAllowed();
                this.ntSpacer4.Visible = Global.IsSysDataModificationAllowed();
                this.ntSeparator4.Visible = Global.IsSysDataModificationAllowed();
                this.ntTenantInfo.Visible = Global.IsSysDataModificationAllowed();
                this.ntSpacer5.Visible = Global.IsSysDataModificationAllowed();
                this.ntSeparator5.Visible = Global.IsSysDataModificationAllowed();
                this.ntnSystem.Visible = Global.IsSysDataModificationAllowed();
              
            }

        }
    }
}