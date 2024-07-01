using System;
using HP.ElementsCPS.Apps.WebUI.WebControls;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class _Default : WebControls.BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//TODO: Implement: Enable auto-redirect from the default page
#warning Not Implemented: Enable auto-redirect from the default page
			//this.AutoRedirectToDefaultStartPage();
		}

		/// <summary>
		/// Redirects users accessing the "default" page (e.g. the app root without a page specified) to an appropriate page or subfolder within the site.
		/// </summary>
		private void AutoRedirectToDefaultStartPage()
		{
			string startPageUrl = BasePage.GetStartPageUrl();
			this.Response.Redirect(startPageUrl);
		}
	}
}