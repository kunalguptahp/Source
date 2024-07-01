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
    public class ConfigurationServiceItemDetailFixture : SharedIEInstanceTestFixture
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

                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
                string[] domainInfo = { "QA_appconfig1", "20100801", "1"};

                ie.GoTo(pageUri);
                ie.FindCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);  
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtSortOrder").TypeText(domainInfo[2]);

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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
                string[] domainInfo = { "QA_appconfig2", "http://ie.configservice.hp.com", "20100802", "Active", "2" };

                ie.GoTo(pageUri);
                ie.FindCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[2]);
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[3]);
                ie.FindDetailPageTextFieldByControlName("txtSortOrder").TypeText(domainInfo[4]);

                for (int num = 0; num < 4; num++)
                {
                    ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkConfigurationServiceGroupTypeList$" + num.ToString())).Checked = true;
                }
                ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ElementsCPSWatinUtility.VerifyCreateSuccess(ie, domainInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
                for (int num = 0; num < 4; num++)
                {
                    string htmlSubString =
                         "<INPUT id=" + Constants.ClientIDPageContentAreaPrefix + "ucDetail_chkConfigurationServiceGroupTypeList_" + num.ToString() + " type=checkbox CHECKED";
                    ElementsCPSWatinUtility.AssertPageBodyHtmlContains(htmlSubString, ie);
                }
				
			}
		}

		/// <summary>
		/// Test edit
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure edit record continues to work.
		/// </remarks>
		[Test]
		public void EditTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
                ie.GoTo(pageUri);
                string project = @"ItemOfA";
                ie.DoClickFilterAreaExpansionToggle();
                ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
                ie.FindFilterButton().Click();

                string[] domainInfo = { "ItemOfA_2", "http://ie.configserviceEdit.hp.com", "20100803", "Inactive", "3" };

                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[2]);
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[3]);
                ie.FindDetailPageTextFieldByControlName("txtSortOrder").TypeText(domainInfo[4]);
                ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
                ie.GoTo(pageUri);

                // Create new button
                ie.FindCreateButton().Click();
                string[] domainInfo = { "GroupTypeOfA_3", "http://ie.configservice.hp.com", "20100804", "Inactive", "2" };
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[2]);
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[3]);
                ie.FindDetailPageTextFieldByControlName("txtSortOrder").TypeText(domainInfo[4]);
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

       /// [Ignore("Not implemented")]
		#region Tests: Robustness (Corner cases & Dummy-proofing)
		/// <summary>
		/// Name Input Validation
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that Email has proper input validation.
		/// </remarks>
		[Test]
		[Row(true, @"123")]
		[Row(true, @"1234")]
		[Row(true, @"abcdefg")]
        [Row(true, @"123456789012345678901234567890")]
        [Row(true, @"  a b ")]//see if trim works
		[Row(true, @"_\_")]
		[Row(true, @"`~!@#$%^&*()[]{}/?;:\|,<.'>")]
        [Row(true, @"测试1")]
        [Row(true, "a\"bcde")] // for "
        [Row(false, @"12")]
        [Row(false, @"1")]
		[Row(false, @"测试")]
		[Row(false, @" a\")]
		public void NameInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtName")).TypeText(name);
                //ie.FindDetailPageTextFieldByControlName(("txtElementsKey")).TypeText(9);

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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceItemList.aspx");
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
                    WatinAssert.TextEquals(@"Please enter a key.", revWindowsId);
                }

            }
        }



		#endregion

		#region Tests: Security/Hacking


		#endregion
	}
}
