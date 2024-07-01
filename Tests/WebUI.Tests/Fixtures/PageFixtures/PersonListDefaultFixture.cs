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
	[Importance(TestImportance.Default)]
	[FixtureCategory(TestCategory.Optional.QuickBuilds)]
	[FixtureCategory(TestCategory.Kind.Functional)]
	[FixtureCategory(TestCategory.Speed.Slow)]
	[FixtureCategory(TestCategory.DependsOn.DB)]
	[FixtureCategory(TestCategory.DependsOn.IIS)]
	[FixtureCategory(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	///[FixtureCategory("Special.HeavyBuildTesting")]
	public class PersonListDefaultFixture : SharedIEInstanceTestFixture
	{

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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("person.");

				// Active status test
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Active");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.deleted@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.deleted@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Inactive status test
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Inactive");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.deleted@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Deleted status test
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Deleted");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.inactive@hp.com", ie);
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);

				string[] prefixProjectGroup = { @"system\system", @"americas\websterj", @"americas\mukaimur", @"americas\scott_trimber", @"americas\bryantc" };
				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("1");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyTextContains(prefixProjectGroup[0], ie);
				//ElementsCPSWatinUtility.AssertPageBodyTextExcludes(prefixProjectGroup[1], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("2,3,4");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyTextContains(prefixProjectGroup[2], ie);
				ElementsCPSWatinUtility.AssertPageBodyTextContains(prefixProjectGroup[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyTextContains(prefixProjectGroup[1], ie);
				//ElementsCPSWatinUtility.AssertPageBodyTextExcludes(prefixProjectGroup[0], ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("02");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyTextContains(prefixProjectGroup[1], ie);
				//ElementsCPSWatinUtility.AssertPageBodyTextExcludes(prefixProjectGroup[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("  ");
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText(@"system\system");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyTextContains(prefixProjectGroup[0], ie);
				//ElementsCPSWatinUtility.AssertPageBodyTextExcludes(prefixProjectGroup[0], ie);
			}
		}
		[Ignore("Not implemented")]
		/// <summary>
		/// TO test
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that role filtering continues to work properly.
		/// </remarks>
		[Test]
		public void ClearTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// single windowsId test
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText(@"system");
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("person.AccountLocked@hp.com");
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("Role");
				ie.FindListPageTextFieldByControlName(("txtFirstName")).TypeText("LockedOut");
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Active");
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("AccountLocked");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.AccountLocked@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindButtonByText(Constants.ButtonText_Clear).Click();
				WatinUtility.Equals(ie.FindListPageTextFieldByControlName(("txtWindowsId")).Text, "");
				WatinUtility.Equals(ie.FindListPageSelectListByControlName(("ddlStatus")).SelectedOption.ToString(), "- All -");
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.AccountLocked@hp.com", ie);
			}
		}

		/// <summary>
		/// Test Email filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that email filtering continues to work properly.
		/// </remarks>
		[Test]
		public void EmailFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// single email test
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("person.active@hp.com");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("justin.webster@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// emails "starting with" test
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("p");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("justin.webster@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("%de");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.deleted@hp.com", ie);
				//ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.ProductDevelopmentManager@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("justin.webster@hp.com", ie);

				ie.DoClickFilterAreaExpansionToggle();
				// nothing found test
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("nothing");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no users found", ie);
			}
		}

		/// <summary>
		/// Test Last Name filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that last name filtering continues to work properly.
		/// </remarks>
		[Test]
		public void LastNameFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// single last name test
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("Webster");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("justin.webster@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// last name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("p");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("justin.webster@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("%c");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("ElementsCPS.Support@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("firsta.lasta@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("justin.webster@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// nothing found test
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("nothing");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no users found", ie);
			}
		}

		/// <summary>
		/// Test First Name filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that first name filtering continues to work properly.
		/// </remarks>
		[Test]
		public void FirstNameFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// single first name test
				ie.FindListPageTextFieldByControlName(("txtFirstName")).TypeText("Justin");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("justin.webster@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// first name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtFirstName")).TypeText("f");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("firsta.lasta@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("firstb.lastb@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("firstc.lastc@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("firstd.lastd@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("firste.laste@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("justin.webster@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtFirstName")).TypeText("%u");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("justin.webster@hp.com", ie);
				//ElementsCPSWatinUtility.AssertPageBodyHtmlContains("jingyuan.xu@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("jiongl@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// nothing found test
				ie.FindListPageTextFieldByControlName(("txtFirstName")).TypeText("nothing");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no users found", ie);
			}
		}

		/// <summary>
		/// Test windowsId filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that windowsId filtering continues to work properly.
		/// </remarks>
		[Test]
		public void WindowsIdFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// single windowsId test
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText(@"americas\lasta");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("firsta.lasta@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("firstb.lastb@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// windowsId "starting with" test
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText("americas");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("justin.webster@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("robert.mukai@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.active@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("ElementsCPS.Support@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("jiongl@hp.com", ie);
				ie.DoClickFilterAreaExpansionToggle();
				// nothing found test
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText("websterj");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no users found", ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText(@"%websterj");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("justin.webster@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("firstb.lastb@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.inactive@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("person.deleted@hp.com", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("robert.mukai@hp.com", ie);

			}
		}

		/// <summary>
		/// Test all fields filtering
		/// </summary>        
		[Test]
		public void AllFieldsFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// single windowsId test
				ie.FindListPageTextFieldByControlName(("txtWindowsId")).TypeText(@"system");
				ie.FindListPageTextFieldByControlName(("txtEmail")).TypeText("person.AccountLocked@hp.com");
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("Role");
				ie.FindListPageTextFieldByControlName(("txtFirstName")).TypeText("LockedOut");
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Active");
				ie.FindListPageSelectListByControlName(("ddlRole")).Select("AccountLocked");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("person.AccountLocked@hp.com", ie);
			}
		}

		/// <summary>
		/// Test Role filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that role filtering continues to work properly.
		/// </remarks>
		[RowTest]
		[Row("Administrator", new string[] { "person.Administrator@hp.com" }, "person.DataAdmin@hp.com")]
		[Row("AccountLocked", new string[] { "person.AccountLocked@hp.com" }, "person.Administrator@hp.com")]
		//[Row("Restricted Access", new string[] { "person.RestrictedAccess@hp.com" }, "person.Administrator@hp.com")]
		//[Row("Everyone", new string[] { "person.Everyone@hp.com" }, "person.Administrator@hp.com")]
		[Row("DataAdmin", new string[] { "person.DataAdmin@hp.com" }, "person.Administrator@hp.com")]
		[Row("UserAdmin", new string[] { "person.UserAdmin@hp.com" }, "person.Administrator@hp.com")]
		[Row("- All -", new string[] { "person.Administrator@hp.com", "person.DataAdmin@hp.com", "person.UserAdmin@hp.com" }, "nonexist@hp.com")]
		//[Row("Technical Support", new string[] { "person.TechnicalSupport@hp.com" }, "person.Administrator@hp.com")]
		//[Row("Project Manager", new string[] { "person.ProjectManager@hp.com" }, "person.Administrator@hp.com")]
		//[Row("Product Development Manager", new string[] { "person.ProductDevelopmentManager@hp.com" }, "person.Administrator@hp.com")]
		//[Row("ODM", new string[] { "person.ODM@hp.com" }, "person.Administrator@hp.com")]
		//[Row("EnableOptionalFeatures", new string[] { "person.EnableOptionalFeatures@hp.com" }, "person.Administrator@hp.com")]
		public void RoleFilterListTest(string roleName, string[] personWithRole, string personWithoutRole)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				//filter the page by last name (to reduce the list data down to the Single-Role-Person test records)
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("Role");
				ie.FindListPageSelectListByControlName(("ddlRole")).Select(roleName);
				ie.FindFilterButton().Click();

				//verify that the page contains only the matching person after filtering
				ElementsCPSWatinUtility.AssertPageBodyTextContains(personWithRole, ie);
				ElementsCPSWatinUtility.AssertPageBodyTextExcludes(personWithoutRole, ie);
			}
		}

		/// <summary>
		/// Test that filtering down to 0 matching rows shows an informative label.
		/// </summary>
		[Test]
		public void FilterListTest_NoMatchingRows()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				//verify that the page contains both people before filtering
				const string msgNoUsers = "There are no users found";
				ElementsCPSWatinUtility.AssertPageBodyTextExcludes(msgNoUsers, ie);

				//filter the page so that no records match
				ie.FindListPageTextFieldByControlName(("txtLastName")).TypeText("NonexistentLastName");
				ie.FindFilterButton().Click();

				//verify that the page contains only the matching person after filtering
				ElementsCPSWatinUtility.AssertPageBodyTextContains(msgNoUsers, ie);
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("UserAdmin/PersonList.aspx");
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
