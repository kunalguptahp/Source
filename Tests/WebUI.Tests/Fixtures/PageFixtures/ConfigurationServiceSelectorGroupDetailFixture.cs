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
    public class ConfigurationServiceSelectorGroupDetailFixture : SharedIEInstanceTestFixture
	{
		[SetUp]
		public void Setup()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		#region Tests: Happy Path

		/// <summary>
		/// Add new project with only mandatory fields
		/// </summary>
		[Test]
		public void AddNewWithOnlyMandatoryTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                string[] domainInfo = { "NewSelectorGroup" };

                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.FindSelectorGroupCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindSaveButton().Click();

                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText(domainInfo[0]);
                ie.FindSelectorGroupFilterButton().Click();
                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                ElementsCPSWatinUtility.VerifyCreateSuccess(ie, domainInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
			}
		}

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
                string[] domainInfo = { "QA_GroupSelector", "Desc1" };

                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.FindSelectorGroupCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "repQueryParameter$ctl00$ucQueryParameterValue$chkNot")).Checked = true;
                ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "repQueryParameter$ctl01$ucQueryParameterValue$gvList$ctl02$CheckBoxButton")).Checked = true;
                ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "repQueryParameter$ctl02$ucQueryParameterValue$gvList$ctl02$CheckBoxButton")).Checked = true;
                ie.FindSaveButton().Click();

                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText(domainInfo[0]);
                ie.FindSelectorGroupFilterButton().Click();
                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                ElementsCPSWatinUtility.VerifyCreateSuccess(ie, domainInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
			}
		}

		/// <summary>
		/// Test edit
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure edit record continues to work.fail,don't know why
		/// </remarks>
		[Test]
		public void EditTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                string[] domainInfo = { "QA_GroupSelector2", "Desc2" };

                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                string project = @"GroupSelectorOfA";
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText(project);
                ie.FindSelectorGroupFilterButton().Click();


                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "repQueryParameter$ctl00$ucQueryParameterValue$chkNot")).Checked = true;
                ie.FindSaveButton().Click();

                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText(domainInfo[0]);
                ie.FindSelectorGroupFilterButton().Click();

                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                ElementsCPSWatinUtility.VerifyModifySuccess(ie, domainInfo, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
                Assert.AreEqual(ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "lblIdValue")).Text, "1");
    
            
            }
		}




		/// <summary>
		/// Test for required fields
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure the required field validators work
		/// </remarks>
		[Test]
		public void RequiredFieldValidatorTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

                // Create new button
                ie.FindSelectorGroupCreateButton().Click();

                // Click Save
                ie.FindSaveButton().Click();

                // check required fields

                ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a name.", ie);		

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
                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

                // Create new button
                ie.FindSelectorGroupCreateButton().Click();

                string[] domainInfo = { "QA_GroupSelector3", "Desc3" };
                ie.FindDetailPageTextFieldByControlName(("txtName")).TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);


                // Click Cancel
                ie.FindCancelButton().Click();

                //verify the new project exists
                ie.GoTo(pageUri);
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

                ie.DoClickFilterSelectorGroupAreaExpansionToggle();
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText(domainInfo[0]);
                ie.FindSelectorGroupFilterButton().Click();

                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceGroupSelectorList$txtName").TypeText("");
                ie.FindSelectorGroupFilterButton().Click();
                ElementsCPSWatinUtility.AssertPageBodyTextExcludes(domainInfo[0], ie);
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
		public void NameInputValidationTest(bool isValidInput, string name)
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


		#endregion

		#region Tests: Security/Hacking


		#endregion
	}
}
