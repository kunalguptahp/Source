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
    [Timeout(1500)]
    [TestFixture]
    [ApartmentState(ApartmentState.STA)]
    [Importance(Importance.Default)]
    [Category(TestCategory.Optional.QuickBuilds)]
    [Category(TestCategory.Kind.Functional)]
    [Category(TestCategory.Speed.Slow)]
    [Category(TestCategory.DependsOn.DB)]
    [Category(TestCategory.DependsOn.IIS)]
    [Category(TestCategory.DependsOn.IIS + ".ElementsCPS")]
    public class ConfigurationServiceSelectorGroupListDefaultFixture : SharedIEInstanceTestFixture
	{

        string[] expectedStringsPrefixes = { "GroupSelectorOfA", "GroupSelectorOfB", "GroupSelectorOfC", "GroupSelectorOfD", "GroupSelectorOfE" };
        string[] expectedStringsSuffixes = { "Active.GroupSelectorStatus", "Inactive.GroupSelectorStatus", "Deleted.GroupSelectorStatus" };

        [SetUp]
        public void Setup()
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
				ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();

				// filter by emails starting with "person."
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText("%GroupSelectorStatus");


				// Active status test
                ie.FindDetailPageSelectListByControlName("ucConfigurationServiceGroupSelectorList$ddlStatus").Select("Active");
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[2], ie);
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
				// Active status test
                ie.FindDetailPageSelectListByControlName("ucConfigurationServiceGroupSelectorList$ddlStatus").Select("- All -");
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes, ie);
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
				// Inactive status test
                ie.FindDetailPageSelectListByControlName("ucConfigurationServiceGroupSelectorList$ddlStatus").Select("Inactive");
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes[2], ie);
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
				// Deleted status test
                ie.FindDetailPageSelectListByControlName("ucConfigurationServiceGroupSelectorList$ddlStatus").Select("Deleted");
                ie.FindSelectorGroupFilterButton().Click();
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
				ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
				// single name test
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtIdList").TypeText("1");
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes, ie);
               
				// name "starting with" test
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtIdList").TypeText("2,3,4");
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[2], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes, ie);
               
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtIdList").TypeText("02");
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsSuffixes, ie);
               
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtIdList").TypeText(" ");
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText("%GroupSelectorStatus");
                ie.FindSelectorGroupFilterButton().Click();
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
				ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
				// single name test
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText(expectedStringsPrefixes[0]);
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
          
				// name "starting with" test
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText("GroupSelectorOf");
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
              
				// name "starting with" test
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText("%GroupSelectorStatus");
                ie.FindSelectorGroupFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsSuffixes, ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes, ie);
            
				// nothing found test
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText("nothing");
                ie.FindSelectorGroupFilterButton().Click();
                ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no selector groups found", ie);
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
				ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
				// single name test
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtIdList").TypeText("1");
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText("GroupSelectorOfA");
                ie.FindDetailPageSelectListByControlName("ucConfigurationServiceGroupSelectorList$ddlStatus").Select("Active");
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtCreatedBy").TypeText(@"Role, Administrator (system\Administrator)");
				ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtModifiedBy").TypeText(@"lasta, firsta (americas\lasta)");
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtCreatedAfter").TypeText(@"01/02/09");
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtModifiedAfter").TypeText(@"01/02/09");
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtCreatedBefore").TypeText(@"01/04/09");
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtModifiedBefore").TypeText(@"01/05/09");

                ie.FindSelectorGroupFilterButton().Click();
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
        [Row(false, "-99999999")]
        [Row(false, @"-9999999999")]
        public void IDsInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
				ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
				// Fill in data
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtIdList").TypeText(name);

				// check required fields
                Span revWindowsId = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucDetail_ucConfigurationServiceGroupSelectorList_revTxtIdList"));

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
