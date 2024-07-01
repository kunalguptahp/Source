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
	[DependsOn(typeof(SmokeTestFixture))]
	[Importance(TestImportance.Default)]
	[FixtureCategory(TestCategory.Optional.QuickBuilds)]
	[FixtureCategory(TestCategory.Kind.Functional)]
	[FixtureCategory(TestCategory.Speed.Slow)]
	[FixtureCategory(TestCategory.DependsOn.DB)]
	[FixtureCategory(TestCategory.DependsOn.IIS)]
	[FixtureCategory(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	///[FixtureCategory("Special.HeavyBuildTesting")]
	public class PersonDetailFixture : SharedIEInstanceTestFixture
	{
		[SetUp("Restore the DB to the SimpleData state.")]
		public void Setup()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		#region Tests: Happy Path

		/// <summary>
		/// Test add new with only mandatory fields
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure add new record continues to work.
		/// </remarks>
		[Test]
		public void AddNewWithOnlyMandatoryTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();
				string[] personInfo = { @"AMERICAS\newperson", "New", "Person", "new.person@hp.com" };
				// Add new
				ie.FindDetailPageTextFieldByControlName(("txtWindowsId")).TypeText(personInfo[0]);
				ie.FindDetailPageTextFieldByControlName(("txtFirstName")).TypeText(personInfo[1]);
				ie.FindDetailPageTextFieldByControlName(("txtLastName")).TypeText(personInfo[2]);
				ie.FindDetailPageTextFieldByControlName(("txtEmail")).TypeText(personInfo[3]);

				// Click Save
				ie.FindSaveButton().Click();

				// new person exists?
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("new.person@hp.com");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyTextContains(personInfo, ie);
				//ElementsCPSWatinUtility.CheckCreatedAndModified(ie, @"asiapacific\liujion", @"asiapacific\liujion", DateTime.Now.ToUniversalTime().ToString(), DateTime.Now.ToUniversalTime().ToString());
			}
		}

		/// <summary>
		/// Test add new with all fields
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure add new record continues to work.
		/// </remarks>
		[Test]
		public void AddNewWithAllFieldsTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();
				string[] personInfo = { @"AMERICAS\newperson", "New", "Middle", "Person", "new.person@hp.com", "Active" };
				// Add new
				ie.FindDetailPageTextFieldByControlName(("txtWindowsId")).TypeText(personInfo[0]);
				ie.FindDetailPageTextFieldByControlName(("txtFirstName")).TypeText(personInfo[1]);
				ie.FindDetailPageTextFieldByControlName(("txtMiddleName")).TypeText(personInfo[2]);
				ie.FindDetailPageTextFieldByControlName(("txtLastName")).TypeText(personInfo[3]);
				ie.FindDetailPageTextFieldByControlName(("txtEmail")).TypeText(personInfo[4]);
				ie.SelectList(Find.ByName(Constants.ControlNameDetailPrefix + "ddlStatus")).Select(personInfo[5]);
				for (int num = 0; num < 8; num++)
				{
					ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkRoleList$" + num.ToString())).Checked = true;
				}

				// Click Save
				ie.FindSaveButton().Click();

				// new person exists?
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("new.person@hp.com");
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(personInfo, ie);
				//ElementsCPSWatinUtility.CheckCreatedAndModified(ie, @"asiapacific\liujion", @"asiapacific\liujion", DateTime.Now.ToUniversalTime().ToString(), DateTime.Now.ToUniversalTime().ToString());

				for (int num = 0; num < 8; num++)
				{
					string htmlSubString =
						 "<INPUT id=" + Constants.ClientIDPageContentAreaPrefix + "ucDetail_chkRoleList_" + num.ToString() + " type=checkbox CHECKED";
					//ElementsCPSWatinUtility.AssertPageBodyHtmlContains(htmlSubString, ie);
					ElementsCPSWatinUtility.AssertPageBodyHtmlContains(htmlSubString, ie);
				}
			}
		}

		/// <summary>
		/// Check off roles and test
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure checking off roles continue to work.
		/// </remarks>
		[Test]
		public void CheckOffRoleTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Click Edit
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				for (int num = 0; num < 8; num++)
				{
					ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkRoleList$" + num.ToString())).Checked = false;
				}
				// Click Save
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				// Click Edit
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				for (int num = 0; num < 8; num++)
				{
					string htmlSubString =
						 "<INPUT id=" + Constants.ClientIDPageContentAreaPrefix + "ucDetail_chkRoleList_" + num.ToString() + " type=checkbox name=" + Constants.ControlNameDetailPrefix + "chkRoleList$" + num.ToString() + ">";
					ElementsCPSWatinUtility.AssertPageBodyHtmlContains(htmlSubString, ie);
				}
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				string project = @"system\system";
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText(project);
				ie.FindFilterButton().Click();

				// Click Edit
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				string[] personInfo = { @"AMERICAS\newperson", "newperson", "The", "Clown", "newperson@hp.com", "Active" };
				// Edit
				ie.FindDetailPageTextFieldByControlName(("txtWindowsId")).TypeText(personInfo[0]);
				ie.FindDetailPageTextFieldByControlName(("txtFirstName")).TypeText(personInfo[1]);
				ie.FindDetailPageTextFieldByControlName(("txtMiddleName")).TypeText(personInfo[2]);
				ie.FindDetailPageTextFieldByControlName(("txtLastName")).TypeText(personInfo[3]);
				ie.FindDetailPageTextFieldByControlName(("txtEmail")).TypeText(personInfo[4]);
				ie.SelectList(Find.ByName(Constants.ControlNameDetailPrefix + "ddlStatus")).Select(personInfo[5]);
				for (int num = 0; num < 8; num++)
				{
					ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkRoleList$" + num.ToString())).Checked = true;
				}

				// Click Save
				ie.FindSaveButton().Click();
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// Check for new values
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText(personInfo[0]);
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(personInfo, ie);
				//ElementsCPSWatinUtility.CheckModified(ie, @"asiapacific\liujion", DateTime.Now.ToUniversalTime().ToString());
				for (int num = 0; num < 8; num++)
				{
					string htmlSubString =
						 "<INPUT id=" + Constants.ClientIDPageContentAreaPrefix + "ucDetail_chkRoleList_" + num.ToString() + " type=checkbox CHECKED";
					ElementsCPSWatinUtility.AssertPageBodyHtmlContains(htmlSubString, ie);
				}
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields
				ElementsCPSWatinUtility.AssertPageBodyTextContains(@"Please enter a windows Id (e.g. AMERICAS\smith).", ie);
				ElementsCPSWatinUtility.AssertPageBodyTextContains("Please enter a first name.", ie);
				ElementsCPSWatinUtility.AssertPageBodyTextContains("Please enter a last name.", ie);
				ElementsCPSWatinUtility.AssertPageBodyTextContains("Please enter an email (e.g. john.smith@hp.com).", ie);
			}
		}

		/// <summary>
		/// Test for unique constraint
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure you can't enter a duplidate
		/// </remarks>
		[Test]
		public void UniqueConstraintTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Add duplicate
				ie.FindDetailPageTextFieldByControlName(("txtWindowsId")).TypeText(@"AMERICAS\mukaimur");
				ie.FindDetailPageTextFieldByControlName(("txtFirstName")).TypeText("Duplicate");
				ie.FindDetailPageTextFieldByControlName(("txtLastName")).TypeText("Mukai");
				ie.FindDetailPageTextFieldByControlName(("txtEmail")).TypeText("robert.mukai@hp.com");

				// Click Save
				ie.FindSaveButton().Click();

				// check for valid error
				ElementsCPSWatinUtility.AssertPageIsDisplayingUniqueConstraintViolationMessage(ie);
				// Click Cancel
				ie.FindCancelButton().Click();
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// Filter last name Mukai and make there is no "Duplicate"
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("Mukai");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyTextExcludes("Duplicate", ie);
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				string[] personInfo = { @"AMERICAS\bozo", "Bozo", "The", "Clown", "bozo.clown@hp.com", "Active" };
				// Edit
				ie.FindDetailPageTextFieldByControlName(("txtWindowsId")).TypeText(personInfo[0]);
				ie.FindDetailPageTextFieldByControlName(("txtFirstName")).TypeText(personInfo[1]);
				ie.FindDetailPageTextFieldByControlName(("txtMiddleName")).TypeText(personInfo[2]);
				ie.FindDetailPageTextFieldByControlName(("txtLastName")).TypeText(personInfo[3]);
				ie.FindDetailPageTextFieldByControlName(("txtEmail")).TypeText(personInfo[4]);
				ie.SelectList(Find.ByName(Constants.ControlNameDetailPrefix + "ddlStatus")).Select(personInfo[5]);
				//josh add
				//ie.WaitForComplete(5);
				WatinAssert.IsVisible(true, ie.FindCancelButton());

				// Click Cancel button
				ie.FindCancelButton().Click();
				ie.GoTo(pageUri);
				Assert.IsFalse(ie.FindCancelButton().Exists, "Unexpected Existence of Cancel button.");
				ie.DoClickFilterAreaExpansionToggle();
				// single email test
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText(personInfo[4]);
				ie.FindFilterButton().Click();
				//clean the mail textbox so that watinassert won't find "bozo.clown@hp.com" here
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("");
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(personInfo[4], ie);
			}
		}

		#endregion

		#region Tests: Known Problems & Bugs

		#endregion

		#region Tests: Robustness (Corner cases & Dummy-proofing)
		/// <summary>
		/// Email Address Input Validation
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that Email has proper input validation.
		/// </remarks>
		[RowTest]
		[Row("a@hp.com", false)]
		[Row("zhipe12ng.luo@hp.com.cn", false)]
		[Row("zhipeng@hp.com; luo@hp.com", true)]
		[Row("zhipeng.luo/hp.com", true)]
		[Row("zhipeng.luo@hp.com ", true)]
		[Row(" zhipeng.luo@hp.com", true)]
		[Row(" zhipeng.luo@hp.com ", true)]
		[Row("zhipeng.luo@hp.com ", true)]
		[Row("zhip中eng.luo@hp.com ", true)]
		[Row("zhipeng.luo@h重p.com ", true)]
		[Row("@hp.com ", true)]
		[Row("zhipeng.luo@ ", true)]
		[Row("aadd", true)]
		public void EmailAddressInputValidationTest(string emailAddress, bool errorEmailAddress)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.TextField(Find.ByName(Constants.ControlNamePrefix1 + "ucDetail$txtFirstName")).TypeText("Bozo");
				ie.TextField(Find.ByName(Constants.ControlNamePrefix1 + "ucDetail$txtMiddleName")).TypeText("The");
				ie.TextField(Find.ByName(Constants.ControlNamePrefix1 + "ucDetail$txtLastName")).TypeText("Clown");
				ie.TextField(Find.ByName(Constants.ControlNamePrefix1 + "ucDetail$txtEmail")).TypeText(emailAddress);

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields
				Span revEmail = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucDetail_revEmail"));
				//josh add
				//ie.WaitForComplete(5);
				WatinAssert.IsVisible(errorEmailAddress, revEmail);
				WatinAssert.TextEquals("Please enter a valid email (e.g. john.smith@hp.com)", revEmail);
			}
		}

		/// <summary>
		/// Email Address Input Validation
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that Email has proper input validation.
		/// </remarks>
		[RowTest]
		[Row(true, @"a\b")]
		[Row(true, @" a\b")]
		[Row(true, @"a\b ")]
		[Row(true, @"a_\b_")]
		[Row(true, @"a1\b2")]
		[Row(true, @"a_1\b_2")]
		[Row(true, @" a\b ")]
		[Row(true, @"MACHINE\ACCOUNT")]
		[Row(true, @"AMERICAS\username")]
		[Row(true, @"ASIAPACIFIC\username")]
		[Row(true, @"EMEA\username")]
		[Row(true, @"americas\username")]
		[Row(true, @"asiapacific\username")]
		[Row(true, @"emea\username")]
		[Row(true, @"emea\user_name")]
		[Row(false, @"_\_")]
		[Row(false, @"1a\b")]
		[Row(false, @"a\2b")]
		[Row(false, @"1a\2b")]
		[Row(false, @"1\2")]
		[Row(false, @"username")]
		[Row(false, @"\username")]
		[Row(false, @"emea\")]
		[Row(false, @"emea\user\name")]
		[Row(false, @"emea\user name")]
		[Row(false, @"em ea\user")]
		[Row(false, @"zhipeng@hp.com")]
		[Row(false, @"emea\s可b")]
		[Row(false, @"em可ea\s")]
		[Row(false, @"em中ea\s文s")]
		public void WindowsIdInputValidationTest(bool isValidInput, string windowsId)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				//ie.TextField(Find.ByName(Constants.ControlNamePrefix1 + "ucDetail$txtFirstName")).TypeText("Bozo");
				//ie.TextField(Find.ByName(Constants.ControlNamePrefix1 + "ucDetail$txtMiddleName")).TypeText("The");
				//ie.TextField(Find.ByName(Constants.ControlNamePrefix1 + "ucDetail$txtLastName")).TypeText("Clown");
				ie.TextField(Find.ByName(Constants.ControlNamePrefix1 + "ucDetail$txtWindowsId")).TypeText(windowsId);

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields
				Span revWindowsId = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucDetail_revWindowsId"));
				//josh add
				//ie.WaitForComplete(5);
				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				WatinAssert.TextEquals(@"Please enter a valid windows Id (e.g. AMERICAS\smith)", revWindowsId);
			}
		}
		/// <summary>
		/// First name Input Validation
		/// </summary>        
		[RowTest]
		[Row(true, @"六")]
		[Row(true, @"六一")]
		[Row(true, @"六2")]
		[Row(true, @"2")]
		[Row(true, @" 2 ")]
		[Row(true, @" 2")]
		[Row(true, @"2 ")]
		[Row(true, @"a_\b_")]
		[Row(true, @"a_\b_ ")]
		[Row(true, @" a_\b_")]
		[Row(true, @" a_\b_ ")]
		[Row(true, @"a_1\b_")]
		[Row(true, @"0123456789")]
		[Row(true, @"`~!@#$%^&*()[]{}/?;:\|,.'>")]
		[Row(true, "\"")] // for "
		//[Row(false, @"<")] 
		[Row(false, @" ")]
		public void FirstNameInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtFirstName")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields
				Span span = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucDetail_rfvFirstName"));
				//josh add
				//ie.WaitForComplete(5);
				WatinAssert.IsVisible(!isValidInput, span);
				WatinAssert.TextEquals(@"Please enter a first name.", span);
			}
		}
		/// <summary>
		/// First name Input Validation
		/// </summary>        
		[RowTest]
		[Row(true, @"六")]
		[Row(true, @"六一")]
		[Row(true, @"六2")]
		[Row(true, @"2")]
		[Row(true, @" 2 ")]
		[Row(true, @" 2")]
		[Row(true, @"2 ")]
		[Row(true, @"a_\b_")]
		[Row(true, @"a_\b_ ")]
		[Row(true, @" a_\b_")]
		[Row(true, @" a_\b_ ")]
		[Row(true, @"a_1\b_")]
		[Row(true, @"0123456789")]
		[Row(true, @"`~!@#$%^&*()[－＿-_]{}/?;:\|,.'><")]
		[Row(true, "\"")] // for "
		[Row(false, @" ")]
		public void LastNameInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtLastName")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields
				Span span = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucDetail_rfvLastName"));
				//josh add
				//ie.WaitForComplete(5);
				WatinAssert.IsVisible(!isValidInput, span);
				WatinAssert.TextEquals(@"Please enter a last name.", span);
			}
		}

		#endregion

		#region Tests: Security/Hacking

		#endregion

	}
}
