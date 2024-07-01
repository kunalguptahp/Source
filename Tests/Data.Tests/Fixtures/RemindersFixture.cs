using System.IO;
using HP.ElementsCPS.Data.Tests.Utility;
using HP.HPFx.Diagnostics.Testing;
using MbUnit.Framework;

namespace HP.ElementsCPS.Data.Tests.Fixtures
{
	[TestFixture]
	[Importance(Importance.Critical)]
	[Category(TestCategory.Kind.Unit)]
	[Category(TestCategory.Speed.Fast)]
	public class RemindersFixture
	{
		//NOTE: Action Required: Please add/remove/update ALL appropriate TypeFixture Providers and Smoke Tests before you change the constants below
		private const int NumberOfTablesIncludedInProject = 67;
		private const int NumberOfTablesExcludedFromProject = 2;
		private const int NumberOfViewsIncludedInProject = 56;
		private const int NumberOfViewsExcludedFromProject = 0;
		private const int NumberOfMiscFiles = 2;
		//NOTE: Action Required: Please add/remove/update ALL appropriate TypeFixture Providers and Smoke Tests before you change the constants above
		private const int NumberOfTables = (NumberOfTablesIncludedInProject + NumberOfTablesExcludedFromProject);
		private const int NumberOfViews = (NumberOfViewsIncludedInProject + NumberOfViewsExcludedFromProject);

		[SetUp] //[SetUp("Restore the DB to the SimpleData state.")]
		public void Setup()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		[TearDown]
		public void TearDown()
		{
			//DataUtility.SetTestUserRoles(UserRoleId.Administrator);
		}

		/// <summary>
		/// Raises any warnings not related to any existing test, such as warnings about missing tests.
		/// </summary>
		[Test]
		public void LogWarnings()
		{
		}

		#region Tests

		/// <summary>
		/// This test asserts that there are exact number of generated DAL files, 
		/// which will cause a test failure when new files are added without also updating the autotests.
		/// </summary>
		[Test]
		public void ReminderToAddTypeFixtureProvidersForAllGeneratedFiles()
		{
            //const int expectedFileCount = (NumberOfTables * 2) + NumberOfViews + NumberOfMiscFiles;
            //const string searchPattern = "*.generated.cs";
            //string searchPath = Path.Combine(DataUtility.GetElementsCPSSourceRootPath(), @"Production\Data\Generated\");
            //int actualFileCount = Directory.GetFiles(searchPath, searchPattern, SearchOption.AllDirectories).Length;
            //Assert.AreEqual(expectedFileCount, actualFileCount);
		}

		/// <summary>
		/// This test asserts that there are exact number of generated files for the app's Views, 
		/// which will cause a test failure when new Views are added without also updating the autotests.
		/// </summary>
		[Test]
		public void ReminderToAddTypeFixtureProvidersForGeneratedViewFiles()
		{
			//NOTE: Important: Please add/remove all appropriate TypeFixture Providers before you change this constant
            //const int expectedFileCount = NumberOfViews;
            //const string searchPattern = "Vw*.generated.cs";
            //string searchPath = Path.Combine(DataUtility.GetElementsCPSSourceRootPath(), @"Production\Data\Generated\");
            //int actualFileCount = Directory.GetFiles(searchPath, searchPattern, SearchOption.AllDirectories).Length;
            //Assert.AreEqual(expectedFileCount, actualFileCount);
		}

		#endregion

	}
}
