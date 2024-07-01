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
    public class ConfigurationServiceLabelDetailFixture : SharedIEInstanceTestFixture
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
               // Uri pageUri2 = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
                //ie.GoTo(pageUri2);
               // string project = @"ItemOfA";
               // ie.DoClickFilterAreaExpansionToggle();
                //ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
               // ie.FindFilterButton().Click();
               // ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
                
               // ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkConfigurationServiceGroupTypeList$" + "2")).Checked = true;
              //  ie.FindSaveButton().Click();


                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
                string[] domainInfo = { "QA_appconfig1", "20100801", "QA_test0909","ItemOfA", "1"};

                ie.GoTo(pageUri);
                ie.FindCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[1]);
                ie.FindDetailPageSelectListByControlName("ddlLabelType").Select(domainInfo[2]);
                ie.FindDetailPageSelectListByControlName("ddlItem").Select(domainInfo[3]);
                ie.FindDetailPageTextFieldByControlName("txtSortOrder").TypeText(domainInfo[4]);

                ie.FindSaveButton().Click();
				
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ElementsCPSWatinUtility.VerifyCreateSuccess(ie,domainInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
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
               
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
                string[] domainInfo = { "QA_appconfig2", "http://ie.configservice.hp.com", "help2", "20100802","QA_test0909","LabelValue2,LableValue1", "ItemOfD", "2", "Active" };

                ie.GoTo(pageUri);
                ie.FindCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtHelp").TypeText(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[3]);
                ie.FindDetailPageSelectListByControlName("ddlLabelType").Select(domainInfo[4]);
                ie.FindDetailPageTextFieldByControlName("txtValueList").TypeText(domainInfo[5]);
                ie.FindDetailPageSelectListByControlName("ddlItem").Select(domainInfo[6]);              
                ie.FindDetailPageTextFieldByControlName("txtSortOrder").TypeText(domainInfo[7]);   
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[8]);
                ie.CheckBox(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucDetail_chkInputRequired")).Checked = true;
                ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ElementsCPSWatinUtility.VerifyCreateSuccess(ie, domainInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
                string htmlSubString =
                         "<INPUT id=" + Constants.ClientIDPageContentAreaPrefix + "ucDetail_chkInputRequired" + " type=checkbox CHECKED";
                ElementsCPSWatinUtility.AssertPageBodyHtmlContains(htmlSubString, ie);
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

                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
				ie.GoTo(pageUri);
				string project2 = @"LabelOfA";
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project2);
				ie.FindFilterButton().Click();
                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                string[] domainInfo = { "LabelOfA_2", "http://ie.configserviceEdit.hp.com", "help3", "20100803","QA_test0909", "LabelValue2,LableValue1", "Active.ItemStatus", "1", "Inactive" };

                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtHelp").TypeText(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[3]);
                ie.FindDetailPageSelectListByControlName("ddlLabelType").Select(domainInfo[4]);
                ie.FindDetailPageTextFieldByControlName("txtValueList").TypeText(domainInfo[5]);
                ie.FindDetailPageSelectListByControlName("ddlItem").Select(domainInfo[6]);
                ie.FindDetailPageTextFieldByControlName("txtSortOrder").TypeText(domainInfo[7]);
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[8]);
                ie.CheckBox(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucDetail_chkInputRequired")).Checked = true;
                ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
                ElementsCPSWatinUtility.VerifyModifySuccess(ie, domainInfo, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
                Assert.AreEqual(ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "lblIdValue")).Text, "1");
                string htmlSubString =
                         "<INPUT id=" + Constants.ClientIDPageContentAreaPrefix + "ucDetail_chkInputRequired" + " type=checkbox CHECKED";
                ElementsCPSWatinUtility.AssertPageBodyHtmlContains(htmlSubString, ie);
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields

                ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a name.", ie);
                ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a key.", ie);
                ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter order number.", ie);


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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
				ie.GoTo(pageUri);

				// Create new button
                ie.FindCreateButton().Click();
                string[] domainInfo = { "LabelOfA_3", "http://ie.configservice.hp.com", "help04", "20100804","QA_test0909", "ItemOfA", "3", "Deleted" };
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtHelp").TypeText(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[3]);
                ie.FindDetailPageSelectListByControlName("ddlLabelType").Select(domainInfo[4]);
                ie.FindDetailPageSelectListByControlName("ddlItem").Select(domainInfo[5]);
                ie.FindDetailPageTextFieldByControlName("txtSortOrder").TypeText(domainInfo[6]);
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[7]);
				
				// Click Cancel
				ie.FindCancelButton().Click();

				// new project exists?
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("");
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
        [Row(true, @"123456")]
        [Row(true, @"12345")]
        [Row(true, @"abcdefg123")]
        [Row(true, @"123456789012345678901234567890")]
        [Row(true, @"`~!@#$%^&*()[]{}/?;:\|,<.'>")]
        [Row(true, @"测试123")]
        [Row(true, "a\"bcde")] // for "
        [Row(false, @"12")]
        [Row(false, @"1")]
        [Row(false, @"测试12")]
        [Row(false, @" a\")]
        [Row(true, @"  a   b   ")]//see if trim works
        [Row(false, @"_\_")]
        public void NameInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtName")).TypeText(name);

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

        /// <summary>
        /// Elements Key Input Validation
        /// </summary>
        /// <remarks>
        /// The reason for this test is to be sure that Elements key has proper input validation.
        /// </remarks>
        [Test]
        [Row(true, @"123456")]
        [Row(true, @"1")]
        [Row(true, @"2147483647")]
        [Row(true, @"2147483646")]
        [Row(true, @"0")]
        //[Row(false, @"2147483648")]
        //[Row(false, @"12345678901234567890")]
        [Row(true, @"-1")]
        [Row(true, @"1.0")]
        // [Row(true, @" ")]//for space
        [Row(true, @"a")]
        [Row(true, @"  1")]//see if trim works
        [Row(true, @"%%")]
        [Row(true, @"汉字")]
        [Row(true, @"1!")]
        [Row(true, @"１２")]
        public void KeyInputValidationTest(bool isValidInput, string key)
        {
            EnhancedIE ie = this.SharedIEInstance;
            {
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceLabelList.aspx");
                ie.GoTo(pageUri);

                // Create new button
                ie.FindCreateButton().Click();

                // Fill in data
                ie.FindDetailPageTextFieldByControlName(("txtElementsKey")).TypeText(key);

                // Click Save
                ie.FindSaveButton().Click();

                Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "rfvElementsKey"));


                WatinAssert.IsVisible(!isValidInput, revWindowsId);
                if (!isValidInput)
                {
                    WatinAssert.TextEquals(@"Must be an integer.", revWindowsId);
                }

            }
        }

		#endregion

		#region Tests: Security/Hacking


		#endregion



	}
}
