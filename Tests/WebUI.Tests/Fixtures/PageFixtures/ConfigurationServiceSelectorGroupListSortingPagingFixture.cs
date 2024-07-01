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
    [Importance(Importance.Default)]
    [Category(TestCategory.Optional.QuickBuilds)]
    [Category(TestCategory.Kind.Functional)]
    [Category(TestCategory.Speed.Slow)]
    [Category(TestCategory.DependsOn.DB)]
    [Category(TestCategory.DependsOn.IIS)]
    [Category(TestCategory.DependsOn.IIS + ".ElementsCPS")]
    public class ConfigurationServiceSelectorGroupListSortingPagingFixture : SharedIEInstanceTestFixture
	{
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
        [Row("Sort$Name", new string[] { "GroupSelectorOfA", "GroupSelectorOfB", "GroupSelectorOfC", "GroupSelectorOfD", "GroupSelectorOfE" }, 5)]
		[Row("Sort$CreatedBy", new string[] { @"system\Administrator", @"americas\lasta", @"americas\mukaimur", @"system\DataAdmin", @"system\Administrator" }, 5)]
		[Row("Sort$CreatedOn", new string[] { @"1/3/2009", @"1/2/2009", @"1/1/2009", @"1/5/2009", @"1/4/2009" }, 5)]
		[Row("Sort$ModifiedBy", new string[] { @"americas\lasta", @"americas\mukaimur", @"system\DataAdmin", @"americas\mukaimur", @"system\Administrator" }, 5)]
		[Row("Sort$ModifiedOn", new string[] { @"1/4/2009", @"1/2/2009", @"1/1/2009", @"1/3/2009", @"1/5/2009" }, 5)]
		[Row("Sort$RowStatusName", new string[] { "Active", "Inactive", "Deleted", "Active", "Inactive" }, 5)]
		public void SortListTest(string sortColumn, string[] sortOrder, int itemsPerPage)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
				ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText("GroupSelectorOf");
                ie.FindSelectorGroupFilterButton().Click();

				// sort column to click
                string urlSortColumn = string.Format(CultureInfo.InvariantCulture, "javascript:__doPostBack('" + Constants.ControlName_gvList2 + "','{0}')", sortColumn);

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
		[Row(1, new string[] { "1", @"LocalizedProduct_LocalizedProductComponentOfA", "2", @"LocalizedProduct_LocalizedProductComponentOfB", "3", @"LocalizedProduct_LocalizedProductComponentOfC", "4", @"LocalizedProduct_LocalizedProductComponentOfD", "5", @"LocalizedProduct_LocalizedProductComponentOfE" })]
		[Row(2, new string[] { "6", @"Active.LocalizedProduct_LocalizedProductComponentStatus", "7", @"Inactive.LocalizedProduct_LocalizedProductComponentStatus", "8", "Deleted.LocalizedProduct_LocalizedProductComponentStatus" })]
		public void PageListTest_Unfiltered(int pageIndex, string[] expectedPageContent)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/DomainList.aspx");
				ie.GoTo(pageUri);

				if (pageIndex != 1)
				{
					//go to the specified pageIndex
					string urlPageIndex = string.Format(CultureInfo.InvariantCulture, "javascript:__doPostBack('" + Constants.ControlName_gvList + "','{0}')", string.Format(CultureInfo.InvariantCulture, "Page${0}", pageIndex));
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
