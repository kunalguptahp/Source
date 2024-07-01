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
    public class ConfigurationServiceApplicationDetailFixture : SharedIEInstanceTestFixture
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceApplicationList.aspx");
                string[] domainInfo = { "QA_TestConfig1", "20100801"};

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[1]);
                //ie.FindDetailPageTextFieldByControlName("txtVersion").TypeText(domainInfo[1]);
               // ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[2]);
               // ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[3]);
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceApplicationList.aspx");
                string[] domainInfo = { "QA_TestConfig2", "2", "QA_AddNewWithAllFieldsTest", "20100801", "Active" };

                ie.GoTo(pageUri);
                ie.FindCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtVersion").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[3]);
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[4]);
                ie.FindSaveButton().Click();

                ie.GoTo(pageUri);
                ie.DoClickFilterAreaExpansionToggle();
                ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindFilterButton().Click();
                ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

                ElementsCPSWatinUtility.VerifyCreateSuccess(ie, domainInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
			
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceApplicationList.aspx");
				ie.GoTo(pageUri);
                string project = @"ApplicationOfA";
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
				ie.FindFilterButton().Click();

                string[] domainInfo = { "ApplicationOfA", "1", "QA_TestEditFunction", "20100801", "Active" };

				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtVersion").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[3]);
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[4]);
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceApplicationList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields

				ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a name.", ie);
				ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a key.", ie);
				//ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a production domain.", ie);

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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceApplicationList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();
                string[] domainInfo = { "QA_TestConfig3", "3", "QA_TestCancelFunction", "20100801", "Active" };
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageTextFieldByControlName("txtVersion").TypeText(domainInfo[1]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(domainInfo[3]);
                ie.FindDetailPageSelectListByControlName("ddlStatus").Select(domainInfo[4]);
				
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
		/// The reason for this test is to be sure that Name has proper input validation.
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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ConfigurationServiceApplicationList.aspx");
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


		#endregion

		#region Tests: Security/Hacking


		#endregion
	}
}
