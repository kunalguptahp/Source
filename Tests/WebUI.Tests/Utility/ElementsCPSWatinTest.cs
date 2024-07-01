using System;
using System.Net;
using HP.HPFx.Extensions.Watin;
using HP.HPFx.Utility;
using MbUnit.Framework;
using WatiN.Core;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Utility
{
	public class ElementsCPSWatinTest
	{

		#region ElementsCPS-specific utility methods

		/// <summary>
		/// Tests that the app's "directly accessible" pages can be accessed without a server error.
		/// </summary>
		/// <param name="pageUri">The absolute URL of the page to test.</param>
		internal static void TestElementsCPSPageOpensWithoutServerError(Uri pageUri)
		{
			using (IE ie = WatinUtility.NewIE())
			{
				AssertElementsCPSPageOpensWithoutServerError(ie, pageUri, true);
			}
		}

		/// <summary>
		/// Tests that the app's "directly accessible" pages can be accessed without a server error, using a specified <see cref="IE"/> instance.
		/// </summary>
		/// <remarks>
		/// This method will never dispose the <paramref name="ie"/> instance.
		/// It is the responsibility of the caller to invoke the <see cref="IDisposable.Dispose"/> method of the <paramref name="ie"/> instance.
		/// </remarks>
		/// <param name="ie">An existing <paramref name="ie"/> instance.</param>
		/// <param name="pageUri">The absolute URL of the page to test.</param>
		internal static void AssertElementsCPSPageOpensWithoutServerError(IE ie, Uri pageUri, bool assertAtHpPortalVisible)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");

			ie.GoTo(pageUri);

			//validate the page's HTTP Status code
			WatinAssert.HttpStatusCodeEquals(HttpStatusCode.OK, ie);

			//validate that the page opened successfully
			Assert.IsFalse(WatinUtility.IsResponseType_ErrorPage(ie), "The page appears to be an Error page.");

			if (assertAtHpPortalVisible)
			{
				//validate that the page's title has the appropriate app-wide prefix
				bool isTitlePrefixedProperly = ie.Title.StartsWith("CPS -");
				Assert.IsTrue(isTitlePrefixedProperly, "The page's Title is incorrect.");

				//validate that the page's KIM Portal Header is displaying correctly
				//WatinAssert.PageBodyTextContains("@hp Home", ie);
				WatinAssert.PageBodyHtmlContains("http://athp.hp.com/", ie);
				//WatinAssert.PageBodyTextContains("PeopleFinder", ie);

				//validate that the page's KIM Portal Footer is displaying correctly
				WatinAssert.PageBodyTextContains("Hewlett-Packard Development Company", ie);
				WatinAssert.PageBodyTextContains("HP Confidential", ie);
			}
		}

		#endregion

	}
}
