using System;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.HPFx.Web.Utility;

namespace HP.ElementsCPS.Apps.WebUI.Pages
{
	public partial class PersonDetailForMyInfo : BasePage
	{

		#region Page Event Methods

		protected virtual void Page_Load(object sender, EventArgs e)
		{
			WebUtility.DisableClientCacheing(this.Response);

			if (!this.Page.IsPostBack)
			{
				this.DataBind();
			}
		}

		#endregion

	}
}