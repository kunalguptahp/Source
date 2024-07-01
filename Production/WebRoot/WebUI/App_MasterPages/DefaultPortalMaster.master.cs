using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Security;
using HP.HPFx.Utility;
using HP.HPFx.Web.UI.Portal;

namespace HP.ElementsCPS.Apps.WebUI.MasterPages
{
    public partial class DefaultPortalMaster : System.Web.UI.MasterPage
    {
        #region Control/Tag alias properties

        public UpdatePanel AjaxContentAreaUpdatePanel
        {
            get { return this.ajaxContentAreaUpdatePanel; }
        }

        public Label PageContentAreaTitle
        {
            get { return this.lblPageContentAreaTitle; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenu Menu
        {
            get { return this.hpMenu; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenuItem ctrlMenuItem_Home
        {
            get { return this.hpMenuItem_Home; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenuItem ctrlMenuItem_MyInfo
        {
            get { return this.hpMenuItem_MyInfo; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenuItem ctrlMenuItem_Redirect
        {
            get { return this.hpMenuItem_Redirect; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenuItem ctrlMenuItem_ConfigurationService
        {
            get { return this.hpMenuItem_ConfigurationService; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenuItem ctrlMenuItem_Jumpstation
        {
            get { return this.hpMenuItem_Jumpstation; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenuItem ctrlMenuItem_Admin_DataAdmin
        {
            get { return this.hpMenuItem_Admin_DataAdmin; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenuItem ctrlMenuItem_Admin_SystemAdmin
        {
            get { return this.hpMenuItem_Admin_SystemAdmin; }
        }

        public HP.HPFx.Web.UI.Portal.PortalMenuItem ctrlMenuItem_Admin_UserAdmin
        {
            get { return this.hpMenuItem_Admin_UserAdmin; }
        }

        public HP.HPFx.Web.UI.Portal.PortalFooter ctrlFooter
        {
            get { return this.hpFooter; }
        }

        public HP.HPFx.Web.UI.Portal.PortalHeader ctrlHeader
        {
            get { return this.hpHeader; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.CONDITIONAL_UseLocalNavLibHeaderScript();

                this.InitializeGreetingMessage();

                this.InitializeSelectedMenuItem();
 
            }

            this.InitializeWarningMessage();
        }

        private void InitializeGreetingMessage()
        {
            //Initialize the NavBar title based on the selected item(s)
            string greetingMessage = "You are not logged in.";
            if (SimpleSecurityManager.CurrentUserIdentity != null)
            {
                string userName = SimpleSecurityManager.CurrentUserIdentity.Name;
                if (!string.IsNullOrEmpty(userName))
                {
                    greetingMessage = string.Format(CultureInfo.CurrentCulture, "Hello {0}.", userName);
                }
            }
            greetingMessage += string.Format(CultureInfo.CurrentCulture, " The time is : {0}", DateTime.UtcNow);
            this.hpHeader.GreetingMessage = greetingMessage;
        }

        private void InitializeWarningMessage()
        {
            try
            {
                this.hpHeader.WarningMessage = this.GetWarningMessage();
            }
            catch (Exception ex1)
            {
                //if the exception is a SQL deadlock, then try again
                if (ExceptionUtility.IsOrContainsSqlDeadlockException(ex1))
                {
                    LogManager.Current.Log(Severity.Info, this, string.Format("A DB deadlock (SqlException.Number={0}) has occurred (1st try). Will retry.", 1205), ex1);
                    Thread.Sleep(10); //pause for 10ms
                    try
                    {
                        this.hpHeader.WarningMessage = this.GetWarningMessage();
                    }
                    catch (Exception ex2)
                    {
                        //if the exception is another SQL deadlock, then just clear the message (for now), and hope it works without a deadlock later
                        if (ExceptionUtility.IsOrContainsSqlDeadlockException(ex2))
                        {
                            LogManager.Current.Log(Severity.Info, this, string.Format("A DB deadlock (SqlException.Number={0}) has occurred (2nd try). Will not retry.", 1205), ex2);
                            this.hpHeader.WarningMessage = "";
                        }
                        else
                        {
                            LogManager.Current.Log(Severity.Warn, this, "An unexpected exception occurred while attempting to generate the System Alert Message (2nd try).", ex1);
                            throw;
                        }
                    }
                }
                else
                {
                    LogManager.Current.Log(Severity.Warn, this, "An unexpected exception occurred while attempting to generate the System Alert Message (1st try).", ex1);
                    throw;
                }
            }
        }

        private string GetWarningMessage()
        {
            //TODO: Implement: Primary code path
#warning Not Implemented: Primary code path
            return ""; //throw new NotImplementedException("The invoked code path is not yet implemented.");
        }

        /// <summary>
        /// Initializes the Selected property of the appropriate MenuItem(s) based on the current Request's Context.
        /// </summary>
        private void InitializeSelectedMenuItem()
        {
            const string PATHFRAGMENT_Redirect = "~/Redirect/";
            const string PATHFRAGMENT_ConfigurationService = "~/ConfigurationService/";
            const string PATHFRAGMENT_Jumpstation = "~/Jumpstation/";
            const string PATHFRAGMENT_DataAdmin = "~/DataAdmin/";
            const string PATHFRAGMENT_MyInfo = "~/MyInfo/";
            const string PATHFRAGMENT_SystemAdmin = "~/SystemAdmin/";
            const string PATHFRAGMENT_UserAdmin = "~/UserAdmin/";
            const string PATHFRAGMENT_Home = "~/Default.aspx";

            //determine which PortalMenuItem the current request is most closely associated with
            PortalMenuItem currentMenuItem;

            string urlPath = this.Request.Url.LocalPath;
            if (string.IsNullOrEmpty(urlPath))
            {
                //this should never happen, but just in case
                currentMenuItem = null;
            }
            else if (urlPath.IndexOf(PATHFRAGMENT_MyInfo, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                currentMenuItem = this.hpMenuItem_MyInfo;
            }
            else if (urlPath.IndexOf(PATHFRAGMENT_Redirect, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                currentMenuItem = this.hpMenuItem_Redirect;
            }
            else if (urlPath.IndexOf(PATHFRAGMENT_ConfigurationService, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                currentMenuItem = this.hpMenuItem_ConfigurationService;
            }
            else if (urlPath.IndexOf(PATHFRAGMENT_Jumpstation, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                currentMenuItem = this.hpMenuItem_Jumpstation;
            }
            else if (urlPath.IndexOf(PATHFRAGMENT_DataAdmin, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                currentMenuItem = this.hpMenuItem_Admin_DataAdmin;
            }
            else if (urlPath.IndexOf(PATHFRAGMENT_SystemAdmin, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                currentMenuItem = this.hpMenuItem_Admin_SystemAdmin;
            }
            else if (urlPath.IndexOf(PATHFRAGMENT_UserAdmin, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                currentMenuItem = this.hpMenuItem_Admin_UserAdmin;
            }
            else if (urlPath.IndexOf(PATHFRAGMENT_Home, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                currentMenuItem = this.hpMenuItem_Home;
            }
            else
            {
                currentMenuItem = null;
            }

            //Select the PortalMenuItem the current request is most closely associated with
            if (currentMenuItem == null)
            {
                this.UnselectAllMenus();
            }
            else
            {
                currentMenuItem.Select(true);
            }
        }

        [Conditional("OFFLINE")]
        private void CONDITIONAL_UseLocalNavLibHeaderScript()
        {
            this.hpHeader.PortalScriptUrl = "/ElementsCPS/scripts/KimNavLib/header.js";
        }

        protected void hpHeader_PreRender(object sender, EventArgs e)
        {
            this.InitializeHeaderTitle();

            this.SecureNavBar();
        }

        /// <summary>
        /// Initializes the NavBar title based on the selected item(s).
        /// </summary>
        protected virtual void InitializeHeaderTitle()
        {
            string title = this.GetDefaultTitle();
            if (title != null)
            {
                this.SetHeader_Title(title);
            }
        }

        #region Header-related Methods

        public void SetHeader_Title(string title)
        {
            this.hpHeader.Title = string.Format(CultureInfo.InvariantCulture, "<div class='PortalHeaderTitle'>{0}</div>", title);
        }

        public string ContentAreaTitle
        {
            get
            {
                return (this.lblPageContentAreaTitle == null) ? "" : this.lblPageContentAreaTitle.Text;
            }
            set
            {
                this.lblPageContentAreaTitle.Text = value ?? "";
            }
        }

        private string GetDefaultTitle()
        {
            if (this.hpMenuItem_Redirect.Selected)
            {
                return "CPS Redirect";
            }
            else if (this.hpMenuItem_ConfigurationService.Selected)
            {
                return "CPS Configuration Service";
            }
            else if (this.hpMenuItem_Jumpstation.Selected)
            {
                return "CPS Jumpstation";
            }
            else if (this.hpMenuItem_MyInfo.Selected)
            {
                return "CPS - My Info";
            }
            else if (this.hpMenuItem_Admin_DataAdmin.Selected)
            {
                return "CPS Reference Data Management";
            }
            else if (this.hpMenuItem_Admin_SystemAdmin.Selected)
            {
                return "CPS System Admin UI";
            }
            else if (this.hpMenuItem_Admin_UserAdmin.Selected)
            {
                return "CPS User Management";
            }
            else
            {
                return "CPS";
            }
        }

        private void SecureNavBar()
        {
            if (SecurityManager.IsCurrentUserInRole(UserRoleId.RestrictedAccess)
                || SecurityManager.IsCurrentUserInRole(UserRoleId.AccountLocked))
            {
                this.hpMenuItem_ConfigurationService.Visible = false;
                this.hpMenuItem_Jumpstation.Visible = false;
                this.hpMenuItem_Redirect.Visible = false;
                this.hpMenuItem_Admin_DataAdmin.Visible = false;
                this.hpMenuItem_Admin_SystemAdmin.Visible = false;
                this.hpMenuItem_Admin_UserAdmin.Visible = false;
                this.hpMenuItem_MyInfo.Visible = true;
                return;
            }

            if (!SecurityManager.IsCurrentUserInRole(UserRoleId.Everyone))
            {
                this.hpMenuItem_MyInfo.Visible = false;
            }

            if (!(SecurityManager.IsCurrentUserInRole(UserRoleId.Editor)
                    || SecurityManager.IsCurrentUserInRole(UserRoleId.Coordinator)
                    || SecurityManager.IsCurrentUserInRole(UserRoleId.TechSupport)
                    || SecurityManager.IsCurrentUserInRole(UserRoleId.Viewer)
                    || SecurityManager.IsCurrentUserInRole(UserRoleId.Validator)))
            {
                this.hpMenuItem_ConfigurationService.Visible = false;
                this.hpMenuItem_Redirect.Visible = false;
                this.hpMenuItem_Jumpstation.Visible = false;
                this.hpMenuItem_MyInfo.Visible = false;
            }

            if (!SecurityManager.IsCurrentUserInRole(UserRoleId.DataAdmin) && !SecurityManager.IsCurrentUserInRole(UserRoleId.SysDataAdmin))
            {
                this.hpMenuItem_Admin_DataAdmin.Visible = false;
            }

            if (!SecurityManager.IsCurrentUserInRole(UserRoleId.UserAdmin))
            {
                this.hpMenuItem_Admin_UserAdmin.Visible = false;
            }

            if (!SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator))
            {
                this.hpMenuItem_Admin_SystemAdmin.Visible = false;
            }
        }

        #endregion

        #region Navigation-related Methods

        public void UnselectAllMenus()
        {
            this.hpMenuItem_Admin_DataAdmin.Selected = false;
            this.hpMenuItem_Admin_SystemAdmin.Selected = false;
            this.hpMenuItem_Admin_UserAdmin.Selected = false;
            this.hpMenuItem_Home.Selected = false;
            this.hpMenuItem_MyInfo.Selected = false;
            this.hpMenuItem_Redirect.Selected = false;
            this.hpMenuItem_ConfigurationService.Selected = false;
            this.hpMenuItem_Jumpstation.Selected = false;
        }

        #endregion

     

       
    }
}
