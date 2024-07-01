using System.Web;
using HP.HPFx.Extensions.Text.StringManipulation;

namespace HP.ElementsCPS.Data.Utility
{
	/// <summary>
	/// Contains utility methods related to SupplierPortal.
	/// </summary>
	public static class SupplierPortalUtility
	{

		//TODO: Refactoring: Move to HPFx
#warning Refactoring: Move to HPFx

		#region Constants

		private const string SUPPLIERPORTAL_HEADER_KEY_USEREMAIL = "SP_EMAIL";
		private const string SUPPLIERPORTAL_HEADER_KEY_FULLNAME = "SP_CN";
		private const string SUPPLIERPORTAL_HEADER_KEY_SURNAME = "SP_SN";
		private const string SUPPLIERPORTAL_HEADER_KEY_GIVENNAME = "SP_GIVENNAME";
		private const string SUPPLIERPORTAL_HEADER_KEY_EMPLOYEENUMBER = "SP_EMPLOYEENUMBER";
		private const string SUPPLIERPORTAL_HEADER_KEY_USERID = "SP_USERID";

		#endregion

		#region Methods

		#region SupplierPortal Request Header-related Methods

		public static bool IsAuthenticated(HttpRequest request)
		{
			return (!string.IsNullOrEmpty(SupplierPortalUtility.GetSupplierPortalUserId(request)));
		}

		public static string GetUserEmail(HttpRequest request)
		{
			return SiteMinderUtility.GetRequestHeaderValue(request, SUPPLIERPORTAL_HEADER_KEY_USEREMAIL);
		}

		public static string GetUserFullName(HttpRequest request)
		{
			return SiteMinderUtility.GetRequestHeaderValue(request, SUPPLIERPORTAL_HEADER_KEY_FULLNAME);
		}

		public static string GetUserSurname(HttpRequest request)
		{
			return SiteMinderUtility.GetRequestHeaderValue(request, SUPPLIERPORTAL_HEADER_KEY_SURNAME);
		}

		public static string GetUserGivenName(HttpRequest request)
		{
			return SiteMinderUtility.GetRequestHeaderValue(request, SUPPLIERPORTAL_HEADER_KEY_GIVENNAME);
		}

		public static int? GetEmployeeNumber(HttpRequest request)
		{
			return SiteMinderUtility.GetRequestHeaderValue(request, SUPPLIERPORTAL_HEADER_KEY_EMPLOYEENUMBER).TryParseInt32();
		}

		public static string GetSupplierPortalUserId(HttpRequest request)
		{
			return SiteMinderUtility.GetRequestHeaderValue(request, SUPPLIERPORTAL_HEADER_KEY_USERID);
		}

		#endregion

		#endregion

	}
}
