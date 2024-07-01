using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.UI;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringAnalysis;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;
using HP.HPFx.Web.Utility;
using SubSonic;

namespace HP.ElementsCPS.Apps.WebUI.MasterPages
{
	public partial class SiteMaster : System.Web.UI.MasterPage
	{

		#region Control/Tag alias properties

		public ScriptManager AjaxScriptManager
		{
			get { return this.ajaxScriptManager; }
		}

		#endregion

		/// <summary>
		/// Logs "unhandled" errors that occur during any Asynchronous PostBack anywhere in the app.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ajaxScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
		{
			this.HandleAsyncPostBackError(e.Exception);
		}

		/// <summary>
		/// Utility method that uses app-specific logic to determine the most appropriate <see cref="Severity"/> with which to log 
		/// a specified <see cref="Exception"/> that has bubbled up to the <see cref="ajaxScriptManager_AsyncPostBackError"/> handler.
		/// </summary>
		private void HandleAsyncPostBackError(Exception exc)
		{
			string absoluteUri = WebUtility.GetCurrentRequestAbsoluteUri();
			string logMessage = string.Format("Unexpected error occurred during Asynchronous PostBack.\nURL: '{0}'.", absoluteUri);
			LogManager.Current.Log(this.GetAsynchronousPostBackExceptionSeverity(exc), this, logMessage, exc);

			// NOTE: The following line was adapted from this article:
			//  http://alpascual.com/blog/al/archive/2007/03/26/Code-Snip-_2200_Customizing-ScriptManager_2200_-to-detect-errors.aspx
			this.ajaxScriptManager.AsyncPostBackErrorMessage = exc.ToString();

			bool includeExceptionDetailsInResponse = Global.EnableClientAccessToExceptionDetails;
			if (includeExceptionDetailsInResponse)
			{
				const string message = 
					"Security Risk: Potentially confidential exception data has been included in the response." 
					+ " A user could potentially obtain sensitive information from this type of data that could be a security risk." 
					+ " This potentially dangerous application behavior has been explicitly enabled by the application's current runtime configuration settings."
					+ " The specific exception data that was returned is included in the content of this log event.";
				LogManager.Current.Log(Severity.Warn, this, message, exc);
			}
			else if (ExceptionUtility.IsOrContainsSqlTimeoutException(exc))
			{
				//Redirect the user to a custom error page (otherwise many users will just think the app "froze")
				this.Page.Response.Redirect("~/ApplicationError.Timeout.html");
			}
			else if (ExceptionUtility.IsOrContainsExceptionOfType(exc, typeof(TimeoutException)))
			{
				//Redirect the user to a custom error page (otherwise many users will just think the app "froze")
				this.Page.Response.Redirect("~/ApplicationError.Timeout.html");
			}
			else
			{
				//Redirect the user to a custom error page (otherwise many users will just think the app "froze")
				this.Page.Response.Redirect("~/ApplicationError.aspx");
			}
		}

		private Severity GetAsynchronousPostBackExceptionSeverity(Exception exc)
		{
			try
			{
				if (WebUtility.IsViewStateException(exc))
				{
					return Severity.Info;
				}
				else if (Global.IsHarmlessScriptResourceAxdIeBugException(exc))
				{
					//NOTE: This case downgrades the severity of a special set of "ScriptResource.axd"-related requests that are related to an MS IE bug.
					return Severity.Info;
				}
				else if (Global.IsHttpException_PageDoesNotExist(exc))
				{
					//NOTE: This case downgrades the severity of exceptions caused by users manually entering invalid URLs.
					return Severity.Info;
				}
				else if (ExceptionUtility.IsOrContainsSqlTimeoutException(exc))
				{
					//NOTE: This case downgrades the severity of "DB Timeout expired" exceptions.
					return Severity.Info;
				}
				else if (ExceptionUtility.IsOrContainsExceptionOfType(exc, typeof(TimeoutException)))
				{
					return Severity.Info;
				}
			}
			catch (Exception ex2)
			{
				LogManager.Current.Log(Severity.Warn, this, "Logging: An Unexpected exception was thrown while attempting to log a different exception.", ex2);
				//throw; //silently consume the exception (except for the log entry above)
			}
			//TODO: Review Needed: Should the final return case be Severity.Error instead of Severity.Warn (like it is in Global.GetApplicationErrorExceptionSeverity)?
#warning Review Needed: Should the final return case be Severity.Error instead of Severity.Warn (like it is in Global.GetApplicationErrorExceptionSeverity)?
			return Severity.Warn;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.EmitSubSonicClientScripts();

			if (!this.IsPostBack)
			{
				this.ValidatePageInheritance();
				this.ValidatePageNamespace();
				this.ValidatePageTitle();
			}
			else
			{
				WebUtility.DisableClientCacheing(this.Response);
			}

			if (Global.IsContentOnlyDisplayModeEnabled(this.Request))
			{
				this.SetContentOnlyDisplayModeEnabled(true);
			}
		}

		/// <summary>
		/// Toggles the "Content Only" Display Mode (i.e. should the header, footer, etc. be visible or not).
		/// </summary>
		/// <param name="enabled"></param>
		private void SetContentOnlyDisplayModeEnabled(bool enabled)
		{
			this.PortalHeaderArea.Visible = !enabled;
			this.PortalFooterArea.Visible = !enabled;
			this.PortalNavigationTreeArea.Visible = !enabled;
		}

		/// <summary>
		/// Emits client-side scripts needed by some SubSonic UI controls (e.g. Scaffold).
		/// </summary>
		private void EmitSubSonicClientScripts()
		{
			WebUIHelper.EmitClientScripts(this.Page);
		}

		/// <summary>
		/// Verify that the (Content) Page that uses this Master Page inherits from the app's BasePage class.
		/// </summary>
		/// <remarks>
		/// This method helps developers detect (during development) pages that don't have a proper inheritance hierarchy.
		/// </remarks>
		[Conditional("TEST")]
		private void ValidatePageInheritance()
		{
			Page p = this.Page;
			if (!(p is WebControls.BasePage))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "The {0} page doesn't inherit from {1}.", p.GetType(), typeof(WebControls.BasePage));
				LogManager.Current.Log(Severity.Warn, p, message);
				WebUtility.ShowAlertBox(this, message);
			}
		}

		/// <summary>
		/// Verify that the (Content) Page's codebehind class is in the correct namespace.
		/// </summary>
		/// <remarks>
		/// This method helps developers detect (during development) pages/codebehinds that don't use the correct namespace.
		/// </remarks>
		[Conditional("TEST")]
		private void ValidatePageNamespace()
		{
			const string CORRECT_PAGE_NAMESPACE = "HP.ElementsCPS.Apps.WebUI.Pages";

			Page p = this.Page;
			string codebehindNamespace = p.GetType().BaseType.Namespace;
			if ((codebehindNamespace != CORRECT_PAGE_NAMESPACE))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "The {0} page's codebehind's namespace ({1}) is incorrect.", p.GetType(), codebehindNamespace);
				LogManager.Current.Log(Severity.Warn, p, message);
				WebUtility.ShowAlertBox(this, message);
			}
		}

		/// <summary>
		/// Verify that the (Content) Page's codebehind class is in the correct namespace.
		/// </summary>
		/// <remarks>
		/// This method helps developers detect (during development) pages/codebehinds that don't use the correct namespace.
		/// </remarks>
		[Conditional("TEST")]
		private void ValidatePageTitle()
		{
			const string CORRECT_PAGE_TITLE_PREFIX = "CPS - ";

			Page p = this.Page;
			string pageTitle = p.Title;
			if ((string.IsNullOrEmpty(pageTitle)) || (!pageTitle.StartsWith(CORRECT_PAGE_TITLE_PREFIX, StringComparison.InvariantCulture)))
			{
				string message = string.Format(CultureInfo.CurrentCulture, "The {0} page's title ({1}) is incorrect.", p.GetType(), pageTitle);
				LogManager.Current.Log(Severity.Warn, p, message);
				WebUtility.ShowAlertBox(this, message);
			}
		}

		/// <summary>
		/// Convenience method.
		/// </summary>
		/// <param name="trigger">A <see cref="Control"/> within a <see cref="UpdatePanel"/> that should cause a full, regular PostBack (instead of a partial page asynchronous PostBack).</param>
		public void RegisterAsFullPostbackTrigger(Control trigger)
		{
			ajaxScriptManager.RegisterPostBackControl(trigger);
		}

	}
}
