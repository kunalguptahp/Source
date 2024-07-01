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

	[TestFixture(ApartmentState = ApartmentState.STA, TimeOut = 90)]
	[Importance(TestImportance.Default)]
	[FixtureCategory(TestCategory.Optional.QuickBuilds)]
	[FixtureCategory(TestCategory.Optional.HeavyBuilds)]
	[FixtureCategory(TestCategory.Optional.DailyBuilds)]
	[FixtureCategory(TestCategory.Kind.Functional)]
	[FixtureCategory(TestCategory.Speed.Slow)]
	[FixtureCategory(TestCategory.DependsOn.DB)]
	[FixtureCategory(TestCategory.DependsOn.IIS)]
	[FixtureCategory(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	//[FixtureCategory("Special.HeavyBuildTesting")]
	public class PersonListSortingPagingFixture : SharedIEInstanceTestFixture
	{

		protected override void TestFixtureDataSetUp()
		{
			DataUtility.RestoreDB_SortingPagingData();
		}

		#region Tests: Happy Path


		/// <summary>
		/// Test Sorting for each sortable column
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that sorting continues to work properly.
		/// </remarks>
		[RowTest]
		[Row("Sort$Id", new string[] { @"americas\lasta", @"americas\lastb", @"americas\lastc", @"americas\lastd", @"americas\laste" }, 5)]
		[Row("Sort$WindowsId", new string[] { @"americas\laste", @"americas\lastd", @"americas\lastc", @"americas\lastb", @"americas\lasta" }, 5)]
		[Row("Sort$LastName", new string[] { @"lastb", @"lastc", @"lastc", @"lasta", @"lastd" }, 5)]
		[Row("Sort$FirstName", new string[] { @"firsta", @"firsta", @"firstb", @"firstc", @"firstd" }, 5)]
		[Row("Sort$Email", new string[] { @"firstc.lastc@hp.com", @"firstd.lastd@hp.com", @"firsta.lasta@hp.com", @"firstb.lastb@hp.com", @"firstf.lastf@hp.com", @"firste.laste@hp.com"  }, 6)]
		[Row("Sort$CreatedBy", new string[] { @"americas\lastd", @"americas\lastb", @"americas\lastc", @"americas\lasta", @"americas\lasta" }, 5)]
		[Row("Sort$CreatedOn", new string[] { @"1/2/2008", @"1/3/2008", @"1/4/2008", @"1/2/2008", @"1/1/2008" }, 5)]
		[Row("Sort$ModifiedBy", new string[] { @"americas\lastd", @"americas\lastb", @"americas\lasta", @"americas\lastc", @"americas\lastd" }, 5)]
		[Row("Sort$ModifiedOn", new string[] { @"1/3/2008", @"1/4/2008", @"1/3/2008", @"1/1/2008", @"1/2/2008" }, 5)]
		[Row("Sort$RowStatusName", new string[] { "Active", "Inactive", "Deleted", "Active", "Inactive" }, 5)]
		public void SortListTest(string sortColumn, string[] sortOrder, int itemsPerPage)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("last");
				ie.FindFilterButton().Click();

				// sort column to click
				string urlSortColumn = string.Format(CultureInfo.InvariantCulture, "javascript:__doPostBack('" + Constants.ControlNameListPrefix + "gvList','{0}')", sortColumn);

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
		[Ignore("Not implemented")]
		/// <summary>
		/// Multiple column sort test
		/// </summary>
		/// <param name="sortColumn"></param>
		/// <param name="sortOrder"></param>
		/// <param name="itemsPerPage"></param>
		[RowTest]
		[Row(new string[] { "Sort$Id", "Sort$LastName" }, new string[] { @"americas\lastd", @"americas\lasta", @"americas\lastb", @"americas\lastc", @"americas\laste" })]
		[Row(new string[] { "Sort$CreatedBy", "Sort$CreatedOn", "Sort$FirstName", "Sort$Email" }, new string[] { @"americas\lastc", @"americas\lastd", @"americas\lasta", @"americas\lastb", @"americas\laste" })]
		public void MultiColumnSortListTest(string[] sortColumns, string[] expectedResult)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("last");
				ie.FindFilterButton().Click();

				string urlSortColumn;
				// sort column to click
				foreach (string column in sortColumns)
				{
					urlSortColumn = string.Format(CultureInfo.InvariantCulture, "javascript:__doPostBack('" + Constants.ControlNameListPrefix + "gvList','{0}')", column);
					ie.Link(Find.ByUrl(urlSortColumn)).Click();
				}
				ElementsCPSWatinUtility.AssertPageBodyHtmlContainsSequentially(expectedResult, ie);
			}
		}
		[Ignore("Not implemented")]
		/// <summary>
		/// Test paging for list
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that paging continues to work properly.
		/// </remarks>
		[RowTest]
		[Row(1, new string[] { "1", @"system\system", "2", @"americas\websterj", "3", @"americas\mukaimur", "4", @"asiapacific\dengz"})]
		[Row(2, new string[] { "21", @"americas\laste" })]
		public void PageListTest_Unfiltered(int pageIndex, string[] expectedPageContent)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				if (pageIndex != 1)
				{
					//go to the specified pageIndex
					string urlPageIndex = string.Format(CultureInfo.InvariantCulture, "javascript:__doPostBack('" + Constants.ControlNameListPrefix + "gvList','{0}')", string.Format(CultureInfo.InvariantCulture, "Page${0}", pageIndex));
					ie.Link(Find.ByUrl(urlPageIndex)).Click();

					//after going to the page, that page's link(s) should no longer be clickable
					ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(urlPageIndex, ie);
				}
				ElementsCPSWatinUtility.AssertPageBodyHtmlContainsSequentially(expectedPageContent, ie);
			}
		}

		#endregion

		#region Tests: Known Problems & Bugs

		#endregion

		#region Tests: Robustness (Corner cases & Dummy-proofing)

		#endregion

		#region Tests: Security/Hacking

		#endregion


	}
}
