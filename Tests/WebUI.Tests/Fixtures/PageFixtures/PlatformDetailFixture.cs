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
	//[DependsOn(typeof(SmokeTestFixture))]
	[Importance(TestImportance.Default)]
	[FixtureCategory(TestCategory.Optional.QuickBuilds)]
	[FixtureCategory(TestCategory.Kind.Functional)]
	[FixtureCategory(TestCategory.Speed.Slow)]
	[FixtureCategory(TestCategory.DependsOn.DB)]
	[FixtureCategory(TestCategory.DependsOn.IIS)]
	[FixtureCategory(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	//[FixtureCategory(TestCategory.ExcludeFrom.HeavyBuilds)]
	public class PlatformDetailFixture : SharedIEInstanceTestFixture
	{
		[SetUp("Restore the DB to the SimpleData state.")]
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("SystemAdmin/PlatformList.aspx");
				string[] platformInfo = { "NewPlatform", "http://dev.hp.com", "http://dev.hp.com" };

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(platformInfo[0]);
				ie.FindDetailPageTextFieldByControlName("txtValidationPlatform").TypeText(platformInfo[1]);
				ie.FindDetailPageTextFieldByControlName("txtProductionPlatform").TypeText(platformInfo[2]);
				ie.FindSaveButton().Click();
				
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(platformInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ElementsCPSWatinUtility.VerifyCreateSuccess(ie,platformInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("SystemAdmin/PlatformList.aspx");
				string[] platformInfo = { "NewPlatform", "New", "Inactive", "http://dev.hp.com", "http://dev.hp.com" };

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(platformInfo[0]);
				ie.FindDetailPageTextFieldByControlName("txtComment").TypeText(platformInfo[1]);
				ie.FindDetailPageSelectListByControlName("ddlStatus").Select(platformInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtValidationPlatform").TypeText(platformInfo[3]);
				ie.FindDetailPageTextFieldByControlName("txtProductionPlatform").TypeText(platformInfo[4]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(platformInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ElementsCPSWatinUtility.VerifyCreateSuccess(ie, platformInfo, ElementsCPSWatinUtility.Creator, DateTime.Now.ToUniversalTime().ToUniversalTime(), ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("SystemAdmin/PlatformList.aspx");
				ie.GoTo(pageUri);
				string project = @"PlatformOfA";
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(project);
				ie.FindFilterButton().Click();

				string[] platformInfo = { "NewPlatform", "New", "Inactive", "http://dev.hp.com", "http://dev.hp.com" };

				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(platformInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlStatus").Select(platformInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtComment").TypeText(platformInfo[1]);
				ie.FindDetailPageTextFieldByControlName("txtValidationPlatform").TypeText(platformInfo[3]);

				ie.FindDetailPageTextFieldByControlName("txtProductionPlatform").TypeText(platformInfo[4]); 
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtName").TypeText(platformInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.VerifyModifySuccess(ie, platformInfo, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("SystemAdmin/PlatformList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Click Save
				ie.FindSaveButton().Click();
				
				// check required fields
				ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a name.", ie);

				ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a Validation URL to publish to.", ie);
				ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a Production URL to publish to.", ie);

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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("SystemAdmin/PlatformList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();
				string[] platformInfo = { "NewPlatform", "http://dev.hp.com", "http://dev.hp.com" };
				ie.FindDetailPageTextFieldByControlName(("txtName")).TypeText(platformInfo[0]);
				ie.FindDetailPageTextFieldByControlName("txtValidationPlatform").TypeText(platformInfo[1]);
				ie.FindDetailPageTextFieldByControlName("txtProductionPlatform").TypeText(platformInfo[2]); 
				
				// Click Cancel
				ie.FindCancelButton().Click();

				// new project exists?
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText(platformInfo[0]);
				ie.FindFilterButton().Click();
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("");
				ElementsCPSWatinUtility.AssertPageBodyTextExcludes(platformInfo[0], ie);
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
		[Row(true, @"六一二")]
		[Row(true, @"六二三五")]
		[Row(true, @"六12")]
		[Row(true, @"ab_")]
		[Row(true, @" ab_ ")]
		[Row(true, @"0123456789")]
		[Row(true, @"`~!@#$%^&*()[]{}/?;:\|,<.'>")]
		[Row(true, "a\"bcde")] // for "
		[Row(false, @" a\ ")]
		[Row(false, @"sb ")]//see if trim works
		[Row(false, @" bc")]
		[Row(false, @"1\")]
		[Row(false, @"六一")]
		//[Row(false, @" ")]
		public void NameInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("SystemAdmin/PlatformList.aspx");
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
		/// Name Input Validation
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that Email has proper input validation.
		/// </remarks>
		[RowTest]
		[Row(true, @"六")]
		[Row(true, @"六一")]
		[Row(true, @"六2")]
		[Row(true, @"2")]
		[Row(true, @" 2 ")]
		[Row(true, @" 2")]
		[Row(true, @"2 ")]
		[Row(true, @" a_1\b_ ")]
		[Row(true, @"0123456789")]
		[Row(true, @"`~!@#$%^&*()[－＿-_]{}/?;:\|,.'><")]
		[Row(true, "\"")] // for "
		//[Row(false, @" ")]
		public void PublishValidationURLInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("SystemAdmin/PlatformList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtValidationPlatform")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

				//Span revWindowsId = ie.Span(Find.ById(Constants.ClientIdDetailPanelPrefix + "revNameMinLength"));

				//WatinAssert.IsVisible(!isValidInput, revWindowsId);
				//if (!isValidInput)
				//{
				//    WatinAssert.TextEquals(@"Name is too short.", revWindowsId);
				//}
			}
		}

		[RowTest]
		[Row(true, @"六")]
		[Row(true, @"六一")]
		[Row(true, @"六2")]
		[Row(true, @"2")]
		[Row(true, @" 2 ")]
		[Row(true, @" 2")]
		[Row(true, @"2 ")]
		[Row(true, @" a_1\b_ ")]
		[Row(true, @"0123456789")]
		[Row(true, @"`~!@#$%^&*()[－＿-_]{}/?;:\|,.'><")]
		[Row(true, "\"")] // for "
		//[Row(false, @" ")]
		public void PublishProductionURLInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("SystemAdmin/PlatformList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtProductionPlatform")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

				//Span revWindowsId = ie.Span(Find.ById(Constants.ClientIdDetailPanelPrefix + "revNameMinLength"));

				//WatinAssert.IsVisible(!isValidInput, revWindowsId);
				//if (!isValidInput)
				//{
				//    WatinAssert.TextEquals(@"Name is too short.", revWindowsId);
				//}
			}
		}
		#endregion

		#region Tests: Security/Hacking


		#endregion
	}
}
