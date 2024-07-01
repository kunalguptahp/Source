using System;
using System.Diagnostics;
using System.Globalization;
using System.Web.UI;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Security;
using HP.HPFx.Web.UI.Portal;

namespace HP.ElementsCPS.Apps.WebUI.MasterPages
{
	public partial class DefaultOverlayMaster : System.Web.UI.MasterPage
	{
		#region Control/Tag alias properties

		public UpdatePanel AjaxContentAreaUpdatePanel
		{
			get { return this.ajaxContentAreaUpdatePanel; }
		}

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
			}
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

	}
}
