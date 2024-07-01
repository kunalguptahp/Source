using System;
using System.Net;
using System.Threading;
using Gallio.Framework;
using HP.HPFx.Extensions.Watin;
using HP.ElementsCPS.Apps.WebUI.Tests.Extensions.Utility.ElementsCPSWatinTestMaintainability;
using HP.ElementsCPS.Apps.WebUI.Tests.Utility;
using HP.HPFx.Diagnostics.Testing;
using HP.ElementsCPS.Data.Tests.Utility;
using MbUnit.Framework;
using WatiN.Core;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Fixtures
{
	/// <summary>
	/// This fixture verifies that all of the app's "direct access not permitted" URLs result in the expected type of server error when they are requested by the browser.
	/// </summary>
	[TestFixture][ApartmentState(ApartmentState.STA)] //[TestFixture(ApartmentState = ApartmentState.STA)] //, TimeOut = 20)]
	[DependsOn(typeof(DefaultFixture))]
	[Importance(Importance.Critical)]
	[Category(TestCategory.Kind.Functional)]
	[Category(TestCategory.Speed.Slow)]
	[Category(TestCategory.DependsOn.DB)]
	[Category(TestCategory.DependsOn.IIS)]
	[Category(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	public class UriUnavailabilitySmokeTestFixture : SharedIEInstanceTestFixture
	{

		protected override void TestFixtureSharedIEInstanceSetUp()
		{
			this.SharedIEInstance = WatinUtility.NewIE(false);
		}

		protected override void TestFixtureDataSetUp()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		#region Tests: Known Problems (e.g. "Reminder" Tests)

		/// <summary>
		/// Tests that any app pages that are known to be broken (e.g. ones not yet implemented or currently being fixed) 
		/// cause the expected type of server error when accessed.
		/// </summary>
		/// <remarks>
		/// Note that this test will always either Fail (meaning the page is no longer broken, or at least not in the expected way), 
		/// or cause a Warning (to remind the developers that the page is still broken and needs attention).
		/// </remarks>
		/// <param name="pageUrl">The app-relative URL of the page to test.</param>
		[Test]
		//[Row("Projects/ProjectList.aspx?query=invalidserializedquery")]
		public void BrokenPageSmokeTest_ApplicationRuntimeError(string pageUrl)
		{
			Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri(pageUrl);
			EnhancedIE ie = this.SharedIEInstance;
			{
				ie.GoTo(pageUri);

				//validate the page's HTTP Status code
				WatinAssert.HttpStatusCodeEquals(HttpStatusCode.InternalServerError, ie);

				//validate that the server response matches the specific type of "error" expected for this type of page
				Assert.IsTrue(WatinUtility.IsResponseType_ApplicationRuntimeError(ie), DefaultFixture.FormatFailureMessage_UnexpectedReponse(ie));
			}

			//NOTE: If we reach this point, then we have successfully verified that the known issue is still an issue, so we log a warning as a reminder.
			TestLog.Warnings.WriteLine("The following page appears to be broken: '{0}'.", pageUrl);
		}

		/// <summary>
		/// Tests that any app pages that are known to be unimplemented (e.g. ones not yet created/implemented) 
		/// cause the expected type of server error when accessed.
		/// </summary>
		/// <remarks>
		/// Note that this test will always either Fail (meaning the page is no longer broken, or at least not in the expected way), 
		/// or cause a Warning (to remind the developers that the page is still unimplemented and needs attention).
		/// </remarks>
		/// <param name="pageUrl">The app-relative URL of the page to test.</param>
		[Test]
		//[Row("Projects/ProjectDetail.aspx")]
		public void BrokenPageSmokeTest_UnimplementedPages(string pageUrl)
		{
			Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri(pageUrl);
			EnhancedIE ie = this.SharedIEInstance;
			{
				ie.GoTo(pageUri);

				//validate the page's HTTP Status code
				WatinAssert.HttpStatusCodeEquals(HttpStatusCode.NotFound, ie);

				//validate that the server response matches the specific type of "error" expected for this type of page
				Assert.IsTrue(WatinUtility.IsResponseType_ResourceNotFound(ie), DefaultFixture.FormatFailureMessage_UnexpectedReponse(ie));
			}

			//NOTE: If we reach this point, then we have successfully verified that the known issue is still an issue, so we log a warning as a reminder.
			TestLog.Warnings.WriteLine("The following page appears to be nonexistent: '{0}'.", pageUrl);
		}

		#endregion

	}
}
