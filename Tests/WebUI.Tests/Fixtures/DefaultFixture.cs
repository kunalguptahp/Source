using System;
using System.Globalization;
using System.Threading;
using Gallio.Framework;
using HP.ElementsCPS.Apps.WebUI.Tests.Extensions.Utility.ElementsCPSWatinTestMaintainability;
using HP.ElementsCPS.Apps.WebUI.Tests.Utility;
using HP.ElementsCPS.Apps.WebUI.WebControls;
using HP.ElementsCPS.Data.Tests.Utility;
using HP.HPFx.Diagnostics.Testing;
using MbUnit.Framework;
using WatiN.Core;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Fixtures
{
	[TestFixture]
	[ApartmentState(ApartmentState.STA)]
	[Importance(Importance.Default)]
	[DependsOn(typeof(RemindersFixture))]
	[Category(TestCategory.Kind.Functional)]
	[Category(TestCategory.Speed.Slow)]
	[Category(TestCategory.DependsOn.DB)]
	[Category(TestCategory.DependsOn.IIS)]
	[Category(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	public class DefaultFixture : SharedIEInstanceTestFixture
	{

		protected override void TestFixtureDataSetUp()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		#region Assembly Tests

		[Test]
		public void AssemblyTest_SmokeTest()
		{
			OldReflectionAssert.HasConstructor(typeof(BasePage));
		}

		#endregion

		/// <summary>
		/// Tests that the app's "directly accessible" pages can be accessed without a server error.
		/// </summary>
		/// <param name="pageUrl">The app-relative URL of the page to test.</param>
		[Test]
		[Row("")]
		//[Row("Default.aspx")]
		public void BasicPageSmokeTest(string pageUrl)
		{
			Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri(pageUrl);
			try
			{
				ElementsCPSWatinTest.AssertElementsCPSPageOpensWithoutServerError(this.SharedIEInstance, pageUri, true);
			}
			catch (WatiN.Core.Exceptions.TimeoutException ex)
			{
				//for an initial timeout exception, we log the event in the Warnings log, but we DO NOT fail the test.
				TestLog.Warnings.WriteLine("NOTE: A TimeoutException occurred, but the test's functionality will be executed again (once) after a short delay to give IIS time to initialize.");
				TestLog.Warnings.WriteException(ex);
				Thread.Sleep(new TimeSpan(0, 2, 0));//wait for 2 minutes

				//re-run the test
				ElementsCPSWatinTest.AssertElementsCPSPageOpensWithoutServerError(this.SharedIEInstance, pageUri, true);
			}
		}

		#region Helper Methods

		/// <summary>
		/// Utility method for constructing a useful assertion failure message used by several of the test methods in this class.
		/// </summary>
		/// <param name="ie"></param>
		internal static string FormatFailureMessage_UnexpectedReponse(IE ie)
		{
			return string.Format(CultureInfo.CurrentCulture, "The web server's HTTP Response is different than expected. Page Title='{0}'.", ie.Title);
		}

		#endregion

	}
}
