using System;
using System.Web;
using HP.HPFx.Utility;

namespace HP.ElementsCPS.Data.Utility
{
	/// <summary>
	/// Contains utility methods related to SiteMinder.
	/// </summary>
	public static class SiteMinderUtility
	{

		//TODO: Refactoring: Move to HPFx
#warning Refactoring: Move to HPFx

		#region Constants

		private const string SITEMINDER_HEADER_KEY_ISAUTHENTICATED = "SM_AUTHENTIC";
		private const string SITEMINDER_HEADER_KEY_ISAUTHORIZED = "SM_AUTHORIZED";
		private const string SITEMINDER_HEADER_KEY_USERDN = "SM_USERDN";
		private const string SITEMINDER_HEADER_KEY_UNIVERSALID = "SM_UNIVERSALID";

		#endregion

		#region Methods

		#region SiteMinder Request Header-related Methods

		public static bool IsAuthenticated(HttpRequest request)
		{
			return "YES".Equals(SiteMinderUtility.GetRequestHeaderValue(request, SITEMINDER_HEADER_KEY_ISAUTHENTICATED), StringComparison.CurrentCultureIgnoreCase);
		}

		public static bool IsAuthorized(HttpRequest request)
		{
			return "YES".Equals(SiteMinderUtility.GetRequestHeaderValue(request, SITEMINDER_HEADER_KEY_ISAUTHORIZED), StringComparison.CurrentCultureIgnoreCase);
		}

		public static string GetUserDN(HttpRequest request)
		{
			return SiteMinderUtility.GetRequestHeaderValue(request, SITEMINDER_HEADER_KEY_USERDN);
		}

		public static string GetUserUniversalId(HttpRequest request)
		{
			return SiteMinderUtility.GetRequestHeaderValue(request, SITEMINDER_HEADER_KEY_UNIVERSALID);
		}

		#endregion

		#region Helper Methods

		internal static string GetRequestHeaderValue(HttpRequest request, string headerKey)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(request, "request");
			ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(headerKey, "headerKey");
			return (request.Headers == null) ? null : request.Headers[headerKey];
		}

		#endregion

		#endregion

	}
}
