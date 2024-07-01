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
    public class ConfigurationServiceGroupEditDetailFixture : SharedIEInstanceTestFixture
	{
		[SetUp]
		public void Setup()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		#region Tests: Happy Path

        /// <summary>
        /// Add new project with all fields
        /// </summary>
        /// <remarks>
        /// The reason for this test is to be sure add new record continues to work.
        /// </remarks>
		[Test]
        public void AddNewWithAllFieldsTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                string[] domainInfo = { "NewGroupEdit", "Desc", "Application9", "lasta, firsta (americas\\lasta)", "addtags" };
                string[] domainInfo2 = { "removetags" };
                ie.GoTo(pageUri);
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl12$CheckBoxButton")).Checked = true;
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl13$CheckBoxButton")).Checked = true;
                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceApplication").Select(domainInfo[2]);
                ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[3]);
                ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[4]);
                ie.FindDetailPageTextFieldByControlName("txtTagsToRemove").TypeText(domainInfo2[0]);

                ie.FindSaveButton().Click();

                ie.GoTo(pageUri);
                ie.DoClickFilterAreaExpansionToggle();
                ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindFilterButton().Click();
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ElementsCPSWatinUtility.VerifyCreateSuccess(ie,domainInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
                ElementsCPSWatinUtility.AssertPageBodyTextExcludes(domainInfo2[0], ie);

                ie.GoTo(pageUri);
                ie.DoClickFilterAreaExpansionToggle();
                ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindFilterButton().Click();
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[2].Click();
                ElementsCPSWatinUtility.VerifyCreateSuccess(ie, domainInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
                ElementsCPSWatinUtility.AssertPageBodyTextExcludes(domainInfo2[0], ie);
			}
		}



		/// <summary>
		/// Test cancel button
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that cancel button to work properly. 
		/// </remarks>
		[Test]
		public void CancelButtonTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                string[] domainInfo = { "NewGroupEdit3", "Desc3", "Application9", "lasta, firsta (americas\\lasta)", "addtagstest"};

                ie.GoTo(pageUri);
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl12$CheckBoxButton")).Checked = true;
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl13$CheckBoxButton")).Checked = true;
                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();              
                
                ie.FindDetailPageTextFieldByControlName(("txtName")).TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);


                // Click Cancel
                ie.FindCancelButton().Click();

                //verify the new project exists
                ie.GoTo(pageUri);
                ie.DoClickFilterAreaExpansionToggle();
                ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindFilterButton().Click();

                ie.GoTo(pageUri);
                ie.DoClickFilterAreaExpansionToggle();
                ie.FindListPageTextFieldByControlName("txtName").TypeText("");
                ie.FindFilterButton().Click();
                ElementsCPSWatinUtility.AssertPageBodyTextExcludes(domainInfo[0], ie);
			}
		}



        /// <summary>
        /// Test cancel button
        /// </summary>
        /// <remarks>
        /// The reason for this test is to be sure that cancel button to work properly. 
        /// </remarks>
        [Test]
        public void ReportButtonTest()
        {
            EnhancedIE ie = this.SharedIEInstance;
            {
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                string[] domainInfo = { "NewGroupEdit", "Desc", "Application9", "lasta, firsta (americas\\lasta)"};
                ie.GoTo(pageUri);
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl12$CheckBoxButton")).Checked = true;
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl13$CheckBoxButton")).Checked = true;

                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceApplication").Select(domainInfo[2]);
                ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[3]);           
 
                ie.FindSaveButton().Click();

                ie.GoTo(pageUri);      
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl12$CheckBoxButton")).Checked = true;
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl13$CheckBoxButton")).Checked = true;
                // Click report button
                ie.FindReportButton().Click();

                //verify the content          

                ElementsCPSWatinUtility.AssertPageBodyHtmlContains(domainInfo, ie);
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
      /*  
        [Test]
       [Row(true, @"")]
        [Row(true, @" ")]
        [Row(true, @"abc")]
        [Row(true, @"abcde")]
        [Row(true, @"0123456789")]
        [Row(true, @"1 2")]
        [Row(true, @"`~!@#$%^&*()[]{}/?;:\|,<.'>")]
        [Row(true, "a\"bcde")] // for "
        [Row(true, @" a\b ")]
        [Row(false, @"ab")]
        [Row(false, @"a")]
        [Row(false, @"1")]
        [Row(false, @"12 ")]
        [Row(false, @" 12")]
        [Row(false, @" 12 ")]       
        public void NameInputValidationTest(bool isValidInput, string name)
        {
            EnhancedIE ie = this.SharedIEInstance;
            {
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                ie.GoTo(pageUri);
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl12$CheckBoxButton")).Checked = true;
                ie.CheckBox(Find.ByName(Constants.ControlName_gvList + "$ctl13$CheckBoxButton")).Checked = true;
                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
 
                // Fill in data
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(name);
                ie.FindDetailPageTextFieldByControlName("txtTagsToRemove").TypeText("");
               
                // Click Save
                ie.FindSaveButton().Click();

                Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "revNameMinLength"));
        

                WatinAssert.IsVisible(!isValidInput, revWindowsId);
                if (!isValidInput)
                {
                    WatinAssert.TextEquals(@"Name is too short.", revWindowsId);
                }
                
            }
        }


        /*
        /// <summary>
        /// Add Tags Input Validation
        /// </summary>
        /// <remarks>
        /// The reason for this test is to be sure that Email has proper input validation.
        /// </remarks>
        [Test]
        [Row(true, @"六一二三四")]
        [Row(true, @"六一二三四五")]
        [Row(true, @"六一12二")]
        [Row(true, @"a_\b_")]
        [Row(true, @"a_\b_ ")]
        [Row(true, @" a_\b_")]
        [Row(true, @" a_\b_ ")]
        [Row(true, @"a_1\b_")]
        [Row(true, @"0123456789")]
        [Row(true, @"`~!@#$%^&*()[]{}/?;:\|,<.'>")]
        [Row(true, "a\"bcde")] // for "
        [Row(false, @" a\b ")]
        [Row(false, @"sa\b ")]//see if trim works
        [Row(false, @" a\bc")]
        [Row(false, @"1a\b")]
        [Row(false, @"六一二三")]
        [Row(false, @"六一二")]
        public void AddTagsInputValidationTest(bool isValidInput, string name)
        {
            EnhancedIE ie = this.SharedIEInstance;
            {
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

                // Create new button
                ie.FindSelectorGroupCreateButton().Click();

                // Fill in data
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(name);

                // Click Save
                ie.FindSaveButton().Click();

               // Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "rfvName"));
                Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "revNameMinLength"));
               // Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "cvNameUnique"));

                WatinAssert.IsVisible(!isValidInput, revWindowsId);
                if (!isValidInput)
                {
                    WatinAssert.TextEquals(@"Name is too short.", revWindowsId);
                }
            }
        }


        /// <summary>
        /// Remove Tags Input Validation
        /// </summary>
        /// <remarks>
        /// The reason for this test is to be sure that Email has proper input validation.
        /// </remarks>
        [Test]
        [Row(true, @"六一二三四")]
        [Row(true, @"六一二三四五")]
        [Row(true, @"六一12二")]
        [Row(true, @"a_\b_")]
        [Row(true, @"a_\b_ ")]
        [Row(true, @" a_\b_")]
        [Row(true, @" a_\b_ ")]
        [Row(true, @"a_1\b_")]
        [Row(true, @"0123456789")]
        [Row(true, @"`~!@#$%^&*()[]{}/?;:\|,<.'>")]
        [Row(true, "a\"bcde")] // for "
        [Row(false, @" a\b ")]
        [Row(false, @"sa\b ")]//see if trim works
        [Row(false, @" a\bc")]
        [Row(false, @"1a\b")]
        [Row(false, @"六一二三")]
        [Row(false, @"六一二")]
        public void RemoveTagsInputValidationTest(bool isValidInput, string name)
        {
            EnhancedIE ie = this.SharedIEInstance;
            {
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

                // Create new button
                ie.FindSelectorGroupCreateButton().Click();

                // Fill in data
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(name);

                // Click Save
                ie.FindSaveButton().Click();

                // Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "rfvName"));
                Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "revNameMinLength"));
                // Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "cvNameUnique"));

                WatinAssert.IsVisible(!isValidInput, revWindowsId);
                if (!isValidInput)
                {
                    WatinAssert.TextEquals(@"Name is too short.", revWindowsId);
                }
            }
        }
        */

		#endregion

		#region Tests: Security/Hacking


		#endregion
	}
}
