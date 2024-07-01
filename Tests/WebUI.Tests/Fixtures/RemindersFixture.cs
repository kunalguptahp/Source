using System;
using System.Globalization;
using System.IO;
using HP.ElementsCPS.Apps.WebUI.Tests.Extensions.Utility.ElementsCPSWatinTestMaintainability;
using HP.ElementsCPS.Apps.WebUI.Tests.Utility;
using HP.HPFx.Diagnostics.Testing;
using HP.HPFx.Extensions.Watin;
using MbUnit.Framework;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Fixtures
{
	[TestFixture]
	[Importance(Importance.Critical)]
	[Category(TestCategory.Kind.Unit)]
	[Category(TestCategory.Speed.Fast)]
	public class RemindersFixture
	{

		//NOTE: Action Required: Please add/remove/update ALL appropriate Smoke Test fixtures (plus other fixtures if needed) before you change the constants below
		private const int NumberOfAspxFiles = 127;
		//NOTE: Action Required: Please add/remove/update ALL appropriate Smoke Test fixtures (plus other fixtures if needed) before you change the constants above

		/// <summary>
		/// Raises any warnings not related to any existing test, such as warnings about missing tests.
		/// </summary>
		[Test]
		public void LogWarnings()
		{
		}

		[Test]
		public void VerifyIE6PlusIsInstalledLocally()
		{
			Version ieVersion = WatinUtility.GetIEVersionFromRegistry();
			Assert.IsNotNull(ieVersion);
			Assert.IsTrue(ieVersion.Major >= 6, string.Format(CultureInfo.CurrentCulture, "Invalid IE version: {0}.", ieVersion.ToString()));
		}

		/// <summary>
		/// Verifies that the web app contains exactly as many ASPX files as are hardcoded in the test.
		/// </summary>
		/// <remarks>
		/// The reason for this test is that if anyone adds or removes an ASPX file, this test will fail.
		/// The failure of this test will require the developer to fix this test, 
		/// which should hopefully remind the developer to add/update other tests for the new/removed page(s) at the same time.
		/// </remarks>
		[Test]
		public void VerifyWebPageCount()
		{
			const int expectedFileCount = NumberOfAspxFiles;
			const string searchPattern = "*.aspx";
			string searchPath = ElementsCPSWatinUtility.GetElementsCPSWebRootPath();
			int actualFileCount = Directory.GetFiles(searchPath, searchPattern, SearchOption.AllDirectories).Length;
			Assert.AreEqual(expectedFileCount, actualFileCount);
		}

		/// <summary>
		/// This test is intended to try to resolve the intermittent "IE Timeout" errors which have been failing some of the CI builds.
		/// </summary>
		[Test]
		//[Ignore("Temporarily disabled to determine whether it is failing the CI build.")]
		public void InitializeIIS()
		{
			//HACK: The following method call is intended to try to resolve the intermittent "IE Timeout" errors which have been failing some of the CI builds.
			try
			{
				ElementsCPSWatinUtility.InitializeIIS();
			}
			catch
			{
				//ignore exception
			}
		}

	}
}
