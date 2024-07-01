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
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                string[] domainInfo = { "QA_appconfig1", "Active.ApplicationStatus", "GroupTypeOfA", "test_qa1", "test_qa2", "test_qa3", "test_qa4", "Ye, Wei (asiapacific\\mahai)"};

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceApplication").Select(domainInfo[1]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceGroupType").Select(domainInfo[2]); 
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$LabelOfE1").TypeText(domainInfo[3]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$LabelOfD1").TypeText(domainInfo[4]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$LabelOfA1").TypeText(domainInfo[5]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$Deleted.LabelStatus1").TypeText(domainInfo[6]);
                ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[7]);     
				ie.FindSaveButton().Click();
				
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
                //Console.WriteLine(ie.Buttons.Length);
                //ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
                string[] domainInfo = { "QA_appconfig2", "Active.ApplicationStatus", "GroupTypeOfA", "test_qa1", "test_qa2", "test_qa3", "test_qa4", "Ye, Wei (asiapacific\\mahai)", "Desc2", "Tags2"};

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceApplication").Select(domainInfo[1]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceGroupType").Select(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$LabelOfE1").TypeText(domainInfo[3]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$LabelOfD1").TypeText(domainInfo[4]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$LabelOfA1").TypeText(domainInfo[5]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$Deleted.LabelStatus1").TypeText(domainInfo[6]);
                ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[7]);  
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[8]);
                ie.FindDetailPageTextFieldByControlName("txtTags").TypeText(domainInfo[9]);
                ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

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
				ie.GoTo(pageUri);
				string project = @"GroupOfA";
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
				ie.FindFilterButton().Click();

				string[] domainInfo = { "NewConfigurationServiceGroup", "Active.ApplicationStatus", "Active.GroupTypeStatus", "value1", "Ye, Wei (asiapacific\\mahai)", "Modified","Desc2","Tags2" };

                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceApplication").Select(domainInfo[1]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceGroupType").Select(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$Label106").TypeText(domainInfo[3]);
                ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[4]);
              //  ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceGroupStatus").Select(domainInfo[5]);
                ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[6]);
                ie.FindDetailPageTextFieldByControlName("txtTags").TypeText(domainInfo[7]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
			
                ie.Buttons.Filter(Find.ByValue(Constants.ButtonText_EditDotDotDot))[1].Click();

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

				// Create new button
				ie.FindCreateButton().Click();

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields

				ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a name.", ie);
                ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please select a Group type.", ie);				

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
             
                string[] domainInfo = { "NewConfigurationServiceGroup_Cancel", "Active.ApplicationStatus", "Active.GroupTypeStatus", "value", "Ye, Wei (asiapacific\\mahai)"};

                ie.GoTo(pageUri);
                ie.FindCreateButton().Click();
                ie.FindDetailPageTextFieldByControlName("txtName").TypeText(domainInfo[0]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceApplication").Select(domainInfo[1]);
                ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceGroupType").Select(domainInfo[2]);
                ie.FindDetailPageTextFieldByControlName("ucConfigurationServiceLabelValue$Label106").TypeText(domainInfo[3]);
                ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[4]);
                //ie.FindDetailPageSelectListByControlName("ddlConfigurationServiceGroupStatus").Select(domainInfo[5]);
           

				// Click Cancel
				ie.FindCancelButton().Click();

				//verify the new project exists
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
		[Row(true, @" a\b ")]
        [Row(true, @"sa\b ")]//see if trim works
        [Row(true, @" a\bc")]
        [Row(true, @"1a\b")]
        [Row(true, @"六一二三")]
        [Row(true, @"六一二")]
		public void NameInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
                Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("ConfigurationService/ConfigurationServiceGroupList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtName")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

                Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "rfvName"));

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
