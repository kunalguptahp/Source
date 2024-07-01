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
    public class ConfigurationServiceLabelListDefaultFixture : SharedIEInstanceTestFixture
	{

		string[] expectedStringsPrefixes = { "LabelOfA", "LabelOfB", "LabelOfC", "LabelOfD", "LabelOfE" };
		string[] expectedStringsSuffixes = { "Active.LabelStatus", "Inactive.LabelStatus", "Deleted.LabelStatus" };

		protected override void TestFixtureDataSetUp()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		#region Tests: Happy Path

		/// <summary>
		/// Test status filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that status filtering continues to work properly.
		/// </remarks>
		[Test]
		public void StatusFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
               
                string[] ItemInfo = { "ItemOfA", "ItemOfD", "Active.ItemStatus", "Item9" };

                for (int num = 0; num < 4; num++)
                {
                    Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
                    ie.GoTo(pageUri);
                    string project = ItemInfo[num];
                    ie.DoClickFilterAreaExpansionToggle();
                    ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
                    ie.FindFilterButton().Click();
                    ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                    ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkConfigurationServiceGroupTypeList$" + "0")).Checked = true;
                    ie.FindSaveButton().Click();
                }

                Uri pageUri2 = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
				ie.GoTo(pageUri2);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("%LabelStatus");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Active");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Inactive status test
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Inactive");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Deleted status test
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Deleted");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes[2], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[1], ie);
			}
		}
        //[Ignore("Not implemented")]

        [Test]
        public void ItemFilterListTest()
        {
            EnhancedIE ie = this.SharedIEInstance;
            {
                string[] ItemInfo = { "ItemOfA", "ItemOfD", "Active.ItemStatus", "Item9" };

                for (int num = 0; num < 4; num++)
                {
                    Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
                    ie.GoTo(pageUri);
                    string project = ItemInfo[num];
                    ie.DoClickFilterAreaExpansionToggle();
                    ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
                    ie.FindFilterButton().Click();
                    ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                    ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkConfigurationServiceGroupTypeList$" + "0")).Checked = true;
                    ie.FindSaveButton().Click();
                }

                Uri pageUri2 = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
                ie.GoTo(pageUri2);
                ie.DoClickFilterAreaExpansionToggle();
                // filter by emails starting with "person."
                ie.FindListPageTextFieldByControlName(("txtName")).TypeText("%LabelStatus");


                // Active status test
                ie.FindListPageSelectListByControlName(("ddlItem")).Select("ItemOfD");
                ie.FindFilterButton().Click();
                ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes[0], ie);
                ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[1], ie);
                ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[2], ie);
                ie.DoClickFilterAreaExpansionToggle();
                // Active status test
                ie.FindListPageSelectListByControlName(("ddlItem")).Select("- All -");
                ie.FindFilterButton().Click();
                ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes, ie);
                ie.DoClickFilterAreaExpansionToggle();
                // Inactive status test
                ie.FindListPageSelectListByControlName(("ddlItem")).Select("Active.ItemStatus");
                ie.FindFilterButton().Click();
                ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes[1], ie);
                ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[0], ie);
                ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[2], ie);
                ie.DoClickFilterAreaExpansionToggle();
                // Deleted status test
                ie.FindListPageSelectListByControlName(("ddlItem")).Select("Item9");
                ie.FindFilterButton().Click();
                ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes[2], ie);
                ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[0], ie);
                ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[1], ie);
            }
        }


		/// <summary>
		/// Test Name filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that name filtering continues to work properly.
		/// </remarks>
		[Test]
		public void IDsFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                string[] ItemInfo = { "ItemOfA", "ItemOfD", "Active.ItemStatus", "Item9" };

                for (int num = 0; num < 4; num++)
                {
                    Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
                    ie.GoTo(pageUri);
                    string project = ItemInfo[num];
                    ie.DoClickFilterAreaExpansionToggle();
                    ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
                    ie.FindFilterButton().Click();
                    ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                    ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkConfigurationServiceGroupTypeList$" + "0")).Checked = true;
                    ie.FindSaveButton().Click();
                }

                Uri pageUri2 = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
                ie.GoTo(pageUri2);

				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("1");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("2,3,4");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[2], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("02");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText(" ");
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("%LabelStatus");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes, ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes, ie);
			
			
			}
		}




		/// <summary>
		/// Test Name filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that name filtering continues to work properly.
		/// </remarks>

		[Test]
		public void NameFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
                ie.GoTo(pageUri);
				
				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(expectedStringsPrefixes[0]);
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("LabelOf");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("%LabelStatus");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes, ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// nothing found test
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("nothing");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no Labels found", ie);
			}
		}


		

	
		/// <summary>
		/// Filter with all fields
		/// </summary>
		[Test]
		public void AllFieldsFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
               
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
                ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("1");
                ie.FindListPageSelectListByControlName(("ddlItem")).Select("ItemOfA");
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("LabelOfA");
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Active");
				ie.FindListPageTextFieldByControlName(("txtCreatedBy")).TypeText(@"system\Administrator");
				ie.FindListPageTextFieldByControlName(("txtModifiedBy")).TypeText(@"americas\lasta");
				ie.FindListPageTextFieldByControlName(("txtCreatedAfter")).TypeText(@"01/02/09");
				ie.FindListPageTextFieldByControlName(("txtModifiedAfter")).TypeText(@"01/02/09");
				ie.FindListPageTextFieldByControlName(("txtCreatedBefore")).TypeText(@"01/04/09");
				ie.FindListPageTextFieldByControlName(("txtModifiedBefore")).TypeText(@"01/05/09");

				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
			}
		}




		#endregion

		#region Tests: Known Problems & Bugs

		#endregion

		#region Tests: Robustness (Corner cases & Dummy-proofing)
		/// <summary>
		/// Name Input Validation
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that Email has proper input validation.
		/// </remarks>
		[RowTest]
		[Row(true, @",2,3")]
		[Row(true, @",2,3,")]
		[Row(true, @",112,3,")]//id=112 doesn't exist 
		[Row(true, @"1,2,3")]
		[Row(true, @"2")]
		[Row(true, @"0")]
		[Row(true, @"0000000000000000")]
		[Row(true, @" 2 ")]
		[Row(true, @" 2")]
		[Row(true, @"2 ")]
		[Row(true, @"01")]
		[Row(true, @"000000001")]
		[Row(true, @"999999998")]
		[Row(false, @"六")]
		[Row(false, @"六一")]
		[Row(false, @"六2")]
		[Row(false, @"a_\b_")]
		[Row(false, @"2ab0c")]
		[Row(false, @"-1")]
		[Row(false, @"-0.4")]
		[Row(false, @"`~!@#$%^&*()[-_]{}/?;:\|,.'>")]
		[Row(false, "\"")] // for "
		[Row(false, @"<")] 
		[Row(true, @" ")]
		[Row(true, @"999999999")]
		[Row(true, @"9999999999")]
		[Row(false, "-99999999")]
		[Row(false, @"-9999999999")]
		public void IDsInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// Fill in data
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText(name);

				// check required fields
				Span revWindowsId = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucList_revTxtIdList"));

				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				if (!isValidInput)
				{
					WatinAssert.TextEquals(@"Must be a comma-separated list of integers.", revWindowsId);
				}
			}
		}

		#endregion

		#region Tests: Security/Hacking

		#endregion
	}
}
