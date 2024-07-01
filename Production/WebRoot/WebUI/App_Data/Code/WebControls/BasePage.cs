using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;

namespace HP.ElementsCPS.Apps.WebUI.WebControls
{
	public class BasePage : Page
	{
		#region LifeCycle-related

		protected override void OnLoad(EventArgs e)
		{
			LogManager.Current.Log(Severity.Debug, this, "Page Event: OnLoad.");

			base.OnLoad(e);

			this.ProcessRequestParams_PageLoadDebugging();

			//TODO: Review Needed: Review client-side-focus retention issue
#warning Review Needed: Review client-side-focus retention issue
			//if (this.IsPostBack)
			//{
			//   //since an AutoPostBack control loses focus when an AutoPostBack occurs, return focus to the control (see #1657)
			//   Global.FocusOnPostBackControl(this, true);
			//}
		}

		protected virtual void ProcessRequestParams_PageLoadDebugging()
		{
			this.ProcessRequestParams_PageLoadDebugging_Sleep();
			this.ProcessRequestParams_PageLoadDebugging_ThrowException();
		}

		private void ProcessRequestParams_PageLoadDebugging_Sleep()
		{
			int? sleepDuration = this.Request.Params["debug.pageload.sleep"].TryParseInt32();
			if (sleepDuration != null)
			{
				Thread.Sleep(sleepDuration.Value);
			}
		}

		private void ProcessRequestParams_PageLoadDebugging_ThrowException()
		{
			bool shouldThrowException = this.Request.Params["debug.pageload.throwexception"].TryParseBoolean() ?? false;
			if (shouldThrowException)
			{
				Exception ex = CreateExceptionInstance(this.Request.Params["debug.pageload.throwexception.type"]);
				throw ex ?? new Exception();
			}
		}

		private static Exception CreateExceptionInstance(string exceptionTypeName)
		{
			Type exType = (string.IsNullOrEmpty(exceptionTypeName)) ? null : Type.GetType(exceptionTypeName);
			if (exType != null)
			{
				bool exTypeIsExceptionType = typeof(Exception).IsAssignableFrom(exType);
				if (!exTypeIsExceptionType)
				{
					exType = null;
				}
			}
			return ((exType == null) ? null : ReflectionUtility.InvokeDefaultConstructor(exType)) as Exception;
		}

		#endregion

		#region Error Handling-related

		/// <summary>
		/// Overridden to be able to gracefully handle <see cref="HttpRequestValidationException"/>s,
		/// </summary>
		/// <remarks>
		/// NOTE: Since <see cref="HttpRequestValidationException"/>s are raised and thrown before <see cref="Page.OnInit"/> is invoked, 
		/// it cannot be caught and handled by the app's <see cref="HttpApplication"/>'s Application_Error event handler like a regular <see cref="Exception"/>.
		/// Instead, it is necessary to override the <see cref="TemplateControl.OnError"/> method in order to be able to successfully catch it 
		/// and handle it before it causes an HTTP Status code 500 response back to the client.
		/// See here for more details:
		/// http://blogs.msdn.com/kaevans/archive/2003/07/07/9791.aspx
		/// http://dotnet.itags.org/web-forms/129575/
		/// http://stackoverflow.com/questions/502112/handling-request-validation-silently
		/// </remarks>
		/// <param name="e"></param>
		protected override void OnError(EventArgs e)
		{
			System.Exception exc = Server.GetLastError();

			if (exc.GetBaseException() is System.Web.HttpRequestValidationException)
			{
				//NOTE: We must Response.End() before returning from this method in order to prevent the normal HTTP status code 500 response
				//that would typically be sent automatically as a response to the invalid HTTP Request.
				this.HandleHttpRequestValidationException(exc);
			}
			else
			{
				base.OnError(e);
			}
		}

		private void HandleHttpRequestValidationException(Exception exc)
		{
			////System.Diagnostics.Debug.Assert(false);
			//this.Response.Write(exc.ToString());
			//this.Response.StatusCode = 200;
			//this.Response.End();

			this.Response.Redirect("~/ApplicationError.InvalidUserInput.html", true);
		}

		#endregion

		#region Theme-related

		///// <summary>
		///// Programmatically determines the page's StyleSheetTheme.
		///// </summary>
		//public override String StyleSheetTheme
		//{
		//   get
		//   {
		//      bool useDefaultTheme = ((new Random().Next(0, 2)) == 0);
		//      if (useDefaultTheme)
		//      {
		//         return "DefaultTheme";
		//      }
		//      else
		//      {
		//         return "AlternateTheme";
		//      }
		//   }
		//}

		///// <summary>
		///// Programmatically determines the page's Theme.
		///// </summary>
		//protected void Page_PreInit(object sender, EventArgs e)
		//{
		//   switch (Request.Params["theme"])
		//   {
		//      case "Alternate":
		//         Page.Theme = "AlternateTheme";
		//         break;
		//      default:
		//         Page.Theme = "DefaultTheme";
		//         break;
		//   }
		//}

		#endregion

		#region Security-related

		/// <summary>
		/// Gets the appropriate "default" page of the site to a specific page or subfolder within the site.
		/// </summary>
		protected static string GetStartPageUrl()
		{
			if (SecurityManager.IsCurrentUserInRole(UserRoleId.RestrictedAccess))
			{
				return "~/MyInfo/";
			}

			if (SecurityManager.IsCurrentUserInRole(UserRoleId.Administrator))
			{
				return "~/SystemAdmin/";
			}

			if (SecurityManager.IsCurrentUserInRole(UserRoleId.DataAdmin))
			{
				return "~/DataAdmin/";
			}

			if (SecurityManager.IsCurrentUserInRole(UserRoleId.UserAdmin))
			{
				return "~/UserAdmin/";
			}

			//NOTE: We never expect to get to here, but just in case...

			//default for all other users
			return "~/MyInfo/";
		}

		#endregion

	}
}