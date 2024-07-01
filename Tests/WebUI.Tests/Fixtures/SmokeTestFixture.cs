using System.Threading;
using HP.ElementsCPS.Apps.WebUI.Tests.Utility;
using HP.HPFx.Diagnostics.Testing;
using HP.ElementsCPS.Data.Tests.Utility;
using MbUnit.Framework;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Fixtures
{
	/// <summary>
	/// This fixture contains miscellaneous smoke tests that don't fit into any of the other smoke test fixtures.
	/// </summary>
	[TestFixture]
	[ApartmentState(ApartmentState.STA)]
	[DependsOn(typeof(DefaultFixture))]
	[Importance(Importance.Critical)]
	[Category(TestCategory.Kind.Functional)]
	[Category(TestCategory.Speed.Slow)]
	[Category(TestCategory.DependsOn.DB)]
	[Category(TestCategory.DependsOn.IIS)]
	[Category(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	public class SmokeTestFixture : SharedIEInstanceTestFixture
	{

		protected override void TestFixtureDataSetUp()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		/// <summary>
		/// NOTE: This test is present only because there are no other tests in this fixture yet, and Gallio will fail the test run if it is included in the test suite without FINDING any tests.
		/// </summary>
		[Test]
		public void SkippedTest()
		{
			Assert.Terminate(Gallio.Model.TestOutcome.Skipped, "This test is simply a placeholder since this fixture currently has no other test methods.");
		}

	}
}
