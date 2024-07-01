using System;
using System.Globalization;
using System.Threading;
using HP.HPFx.Extensions.Watin;
using HP.ElementsCPS.Apps.WebUI.Tests.Extensions.Utility.ElementsCPSWatinTestMaintainability;
using HP.ElementsCPS.Apps.WebUI.Tests.Utility;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Diagnostics.Testing;
using HP.ElementsCPS.Data.Tests.Utility;
using MbUnit.Framework;
using WatiN.Core;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Fixtures.PageFixtures
{
    [Timeout(1200)]
    [TestFixture]
    [ApartmentState(ApartmentState.STA)]
    //[DependsOn(typeof(SmokeTestFixture))]
    [Importance(Importance.Default)]

    [Category(TestCategory.Optional.QuickBuilds)]
    [Category(TestCategory.Kind.Functional)]
    [Category(TestCategory.Speed.Slow)]
    [Category(TestCategory.DependsOn.DB)]
    [Category(TestCategory.DependsOn.IIS)]
    [Category(TestCategory.DependsOn.IIS + ".ElementsCPS")]
    //[Category(TestCategory.ExcludeFrom.HeavyBuilds)]
    public class ConfigurationServiceApplicationListSortingPagingFixture: SharedIEInstanceTestFixture
	{
		//protected override void TestFixtureDataSetUp()
		//{
		//	DataUtility.RestoreDB_SortingPagingData();
		//}

        [SetUp]
        public void Setup()
        {
            DataUtility.RestoreDB_SimpleData();
        }

		#region Tests: Happy Path


		/// <summary>
		/// Test Sorting for each sortable column
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that sorting continues to work properly.
		/// </remarks>
		[RowTest]
        [Row("Sort$Id", new string[] { "1", "2", "3", "4", "5" }, 5)]
        [Row("Sort$Name", new string[] { "ApplicationOfA", "ApplicationOfB", "ApplicationOfC", "ApplicationOfD", "ApplicationOfE" }, 5)]
        [Row("Sort$Version", new string[] { "1.0", "2.0", "1.1", "1.2", "3.0" }, 5)]
        [Row("Sort$ElementsKey", new string[] { "20100801", "20100802", "20100803", "20100804", "20100805" }, 5)]
		[Row("Sort$CreatedBy", new string[] { @"system\Administrator", @"americas\lasta", @"americas\mukaimur", @"system\DataAdmin", @"system\Administrator" }, 5)]
		[Row("Sort$CreatedOn", new string[] { @"1/3/2009", @"1/2/2009", @"1/1/2009", @"1/5/2009", @"1/4/2009" }, 5)]
		[Row("Sort$ModifiedBy", new string[] { @"americas\lasta", @"americas\mukaimur", @"system\DataAdmin", @"americas\mukaimur", @"system\Administrator" }, 5)]
		[Row("Sort$ModifiedOn", new string[] { @"1/4/2009", @"1/2/2009", @"1/1/2009", @"1/3/2009", @"1/5/2009" }, 5)]
		[Row("Sort$RowStatusName", new string[] { "Active", "Inactive", "Deleted", "Active", "Inactive" }, 5)]
		public void SortListTest(string sortColumn, string[] sortOrder, int itemsPerPage)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceApplicationList.aspx");
				ie.GoTo(pageUri);

				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("ApplicationOf");
				ie.FindFilterButton().Click();

				// sort column to click
				string urlSortColumn = string.Format(CultureInfo.InvariantCulture, "javascript:__doPostBack('" + Constants.ControlName_gvList + "','{0}')", sortColumn);

				// check initial order
				Array.Sort(sortOrder);
				string[] searchOrder = WatinUtility.FirstPageList(sortOrder, itemsPerPage);
				ie.Link(Find.ByUrl(urlSortColumn)).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContainsSequentially(searchOrder, ie);

				// check descending order
				Array.Reverse(sortOrder);
				searchOrder = WatinUtility.FirstPageList(sortOrder, itemsPerPage);
				ie.Link(Find.ByUrl(urlSortColumn)).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContainsSequentially(searchOrder, ie);

				// check ascending order
				Array.Reverse(sortOrder);
				searchOrder = WatinUtility.FirstPageList(sortOrder, itemsPerPage);
				ie.Link(Find.ByUrl(urlSortColumn)).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContainsSequentially(searchOrder, ie);
			}
		}
		/* 
        [Ignore("Not implemented")]
		/// <summary>
		/// Test paging for list
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that paging continues to work properly.
		/// </remarks>
		[RowTest]
        [Row(1, new string[] { "1", @"ApplicationOfA", "2", @"ApplicationOfB", "3", @"ApplicationOfC", "4", @"ApplicationOfD", "5", @"ApplicationOfE" })]
        [Row(2, new string[] { "22", @"Application22", "23", @"Application23", "24", @"Application24" })]
        public void PageListTest_Unfiltered(int pageIndex, string[] expectedPageContent)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceApplicationList.aspx");
				ie.GoTo(pageUri);

				if (pageIndex != 1)
				{
					//go to the specified pageIndex
                    string urlPageIndex = string.Format(CultureInfo.InvariantCulture, "javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(\"" + Constants.ControlName_dpListTop + "{0}\",%20\"\",%20true,%20\"\",%20\"\",%20false,%20true))", pageIndex-1);
					ie.Link(Find.ByUrl(urlPageIndex)).Click();

					//after going to the page, that page's link(s) should no longer be clickable
					ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(urlPageIndex, ie);
				}
				ElementsCPSWatinUtility.AssertPageBodyHtmlContainsSequentially(expectedPageContent, ie);
			}
		}
         */

		#endregion

		#region Tests: Known Problems & Bugs

		#endregion

		#region Tests: Robustness (Corner cases & Dummy-proofing)

		#endregion

		#region Tests: Security/Hacking

		#endregion
	}
}
