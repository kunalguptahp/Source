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
	public class ProxyURLListDefaultFixture : SharedIEInstanceTestFixture
	{

		string[] expectedStringsPrefixes = { "ProxyURLOfA", "ProxyURLOfB", "ProxyURLOfC", "ProxyURLOfD", "ProxyURLOfF", "ProxyURLOfG", "ProxyURLOfH", "ProxyURLOfI"};
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlProxyURLStatus")).Select("Modified");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlProxyURLStatus")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Inactive status test
				ie.FindListPageSelectListByControlName(("ddlProxyURLStatus")).Select("Ready For Validation");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				
			}
		}

		[Test]
		public void ProxyURLTypeFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlProxyURLType")).Select("ProxyURLTypeOfA");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlProxyURLType")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Inactive status test
				ie.FindListPageSelectListByControlName(("ddlProxyURLType")).Select("ProxyURLTypeOfB");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);

			}
		}

		[Test]
		public void OwnerFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlOwner")).Select(@"ServiceAccount, ElementsCPS (system\system)");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlOwner")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Inactive status test
				ie.FindListPageSelectListByControlName(("ddlOwner")).Select(@"Trimber, Scott L (americas\scott_trimber)");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);

			}
		}

		
		[Ignore]
		[Test]
		public void PlatformFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlPlatform")).Select(@"PlatformOfA");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlPlatform")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Inactive status test
				ie.FindListPageSelectListByControlName(("ddlPlatform")).Select(@"PlatformOfD");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);

			}
		}
		[Test]
		public void TouchpointFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlTouchpoint")).Select(@"TouchpointOfA");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlTouchpoint")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);

			}
		}
		[Test]
		public void LocaleFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlLocale")).Select(@"LocaleOfA");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlLocale")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);

			}
		}
		[Test]
		public void PartnerCategoryFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlPartnerCategory")).Select(@"PartnerCategoryOfA");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlPartnerCategory")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);

			}
		}
		[Test]
		public void BrandFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlBrand")).Select(@"BrandOfB");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[6], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlBrand")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);

			}
		}
		[Test]
		public void CycleFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");


				// Active status test
				ie.FindListPageSelectListByControlName(("ddlCycle")).Select(@"CycleOfA");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[7], ie); 
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// Active status test
				ie.FindListPageSelectListByControlName(("ddlCycle")).Select("- All -");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);

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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("1");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("2,3,4");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[2], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[0], ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("02");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText(" ");
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
			
			
			}
		}

		[Test]
		public void ValidationFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtValidationProxyURLId")).TypeText("1");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[2], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtValidationProxyURLId")).TypeText("111");
				ie.FindFilterButton().Click();				
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no Redirections found", ie);


			}
		}

		[Test]
		public void PublishFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtElementsProxyURLId")).TypeText("2");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[3], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtElementsProxyURLId")).TypeText("111");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no Redirections found", ie);


			}
		}

		/// <summary>
		/// Test Name filtering
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that name filtering continues to work properly.
		/// </remarks>
		[Test]
		public void DescriptionFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				
				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText(expectedStringsPrefixes[0]);
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("%A");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// nothing found test
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("nothing");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no Redirections found", ie);
			}
		}

		[Test]
		public void TargetURLFilterListTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtProxyURL")).TypeText("http://ProxyURLOfA.com");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[2], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtProxyURL")).TypeText("http://Proxy");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes, ie);
				ie.DoClickFilterAreaExpansionToggle();
				// name "starting with" test
				ie.FindListPageTextFieldByControlName(("txtProxyURL")).TypeText("%A");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(expectedStringsPrefixes[0], ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes(expectedStringsPrefixes[1], ie);
				ie.DoClickFilterAreaExpansionToggle();
				// nothing found test
				ie.FindListPageTextFieldByControlName(("txtProxyURL")).TypeText("nothing");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no Redirections found", ie);
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// single name test
				ie.FindListPageTextFieldByControlName(("txtIdList")).TypeText("1");
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");
				ie.FindListPageSelectListByControlName(("ddlStatus")).Select("Modified");
				ie.FindListPageTextFieldByControlName(("txtCreatedBy")).TypeText(@"Role, Administrator (system\Administrator)");
				ie.FindListPageTextFieldByControlName(("txtModifiedBy")).TypeText(@"lasta, firsta (americas\lasta)");
				ie.FindListPageTextFieldByControlName(("txtCreatedAfter")).TypeText(@"01/02/09");
				ie.FindListPageTextFieldByControlName(("txtModifiedAfter")).TypeText(@"01/02/09");
				ie.FindListPageTextFieldByControlName(("txtCreatedBefore")).TypeText(@"01/04/09");
				ie.FindListPageTextFieldByControlName(("txtModifiedBefore")).TypeText(@"01/05/09");

				ie.FindFilterButton().Click();
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
		[Row(true, @"9999999999")]
		[Row(false, "-99999999")]
		[Row(false, @"-9999999999")]
		public void IDsInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
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

		/// <summary>
		/// TO do
		/// </summary>
		/// <remarks>
		/// The reason for this test is to be sure that role filtering continues to work properly.
		/// </remarks>
		[RowTest]
		[Row(@"01/04/09", @"01/05/09", new string[] { "ProxyURLOfE" }, "ProxyURLOfA")]
		[Row(@"01/01/00", @"12/31/9999", new string[] { "ProxyURLOfA", "ProxyURLOfB", "ProxyURLOfC", "ProxyURLOfD", "ProxyURLOfE" }, "Active.ProxyURLStatus")]
		[Row(@"01/05/09", @"", new string[] { "ProxyURLOfD" }, "ProxyURLOfE")]
		[Row(@"", @"01/03/09", new string[] { "ProxyURLOfB", "ProxyURLOfC" }, "ProxyURLOfE")]
		public void CreatedBeforeAndAfterPositiveFilterListTest(string createdAfter, string createdBefore, string[] validRecord, string invalidRecord)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				//filter the page
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOf");
				ie.FindListPageTextFieldByControlName(("txtCreatedAfter")).TypeText(createdAfter);
				ie.FindListPageTextFieldByControlName(("txtCreatedBefore")).TypeText(createdBefore);
				ie.FindFilterButton().Click();

				//verify that the page contains only the matching person after filtering

				ElementsCPSWatinUtility.AssertPageBodyTextContains(validRecord, ie);
				ElementsCPSWatinUtility.AssertPageBodyTextExcludes(invalidRecord, ie);
			}
		}
		[RowTest]
		[Row("02/11/2009", "02/11/2009", false)]
		[Row("02/11/2009", "02/10/2009", false)]//date
		[Row("02/11/2009", "01/22/2009", false)]//month
		[Row("02/11/2009", "12/21/2008", false)]//year
		public void CreatedBeforeAndAfterNegativeFilterListTest(string assignedAt, string completedAt, bool isValid)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtCreatedAfter")).TypeText(assignedAt);
				ie.FindListPageTextFieldByControlName(("txtCreatedBefore")).TypeText(completedAt);

				Span revWindowsId = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucList_cvCreatedOnRange"));
				WatinAssert.IsVisible(!isValid, revWindowsId);
				if (!isValid)
				{
					WatinAssert.TextEquals("'After' date must be earlier than 'Before' date.", revWindowsId);
					
				}
			}
		}

		#endregion
		#region Tests: Known Problems & Bugs

		#endregion

		#region Tests: Robustness (Corner cases & Dummy-proofing)



		[RowTest]
		[Row(true, @"01/01/0000")]
		[Row(true, @" 12/31/9999 ")]
		[Row(true, @"2/29/2000")]//闰年
		[Row(false, @"2/29/1900")]//平年
		//date
		[Row(false, @"0.4/12/07")]
		[Row(false, @"-4/12/07")]
		[Row(false, @"0/12/07")]
		[Row(false, @"32/12/07")]
		[Row(false, @"004/11/07")]//three digits
		[Row(true, @"6/8/06")]//only one digit
		[Row(false, @"1 2/02/08")]
		//month
		[Row(false, @"4/0.1/07")]
		[Row(false, @"4/-12/07")]
		[Row(false, @"4/0/07")]
		[Row(false, @"4/13/07")]
		[Row(false, @"4/003/07")]
		[Row(true, @"4/8/07")]
		[Row(false, @"12/0 2/08")]
		//year
		[Row(false, @"4/1/0.7")]
		[Row(false, @"4/12/-07")]
		[Row(false, @"4/10/0")]
		[Row(false, @"4/11/10000")]
		[Row(false, @"4/10/00007")]
		[Row(true, @"4/8/117")]
		[Row(true, @"4/8/17")]
		[Row(false, @"4/8/7")]
		[Row(false, @"12/02/0 8")]
		//special character
		[Row(false, @"`<~!@#$%^&*()-[]{}/?;:\|,.'>")]
		[Row(false, "a\"/cd/ef")]
		[Row(false, @"一二/三四/五六")]
		//lack one 
		[Row(false, @"12/07")]
		[Row(false, @"12")]
		public void CreatedAfterValidationTest(bool isValidInput, string time)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// Fill in data
				ie.FindListPageTextFieldByControlName(("txtCreatedAfter")).TypeText(time);

				// check required fields
				Span revWindowsId = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucList_cvCreatedAfter"));

				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				if (!isValidInput)
				{

					WatinAssert.TextEquals(@"Please enter a valid date (e.g. mm/dd/yy).", revWindowsId);
				}
			}
		}

		[RowTest]
		[Row(true, @"01/01/0000")]
		[Row(true, @" 12/31/9999 ")]
		[Row(true, @"2/29/2000")]//闰年
		[Row(false, @"2/29/1900")]//平年
		//date
		[Row(false, @"0.4/12/07")]
		[Row(false, @"-4/12/07")]
		[Row(false, @"0/12/07")]
		[Row(false, @"32/12/07")]
		[Row(false, @"004/11/07")]//three digits
		[Row(true, @"6/8/06")]//only one digit
		[Row(false, @"1 2/02/08")]
		//month
		[Row(false, @"4/0.1/07")]
		[Row(false, @"4/-12/07")]
		[Row(false, @"4/0/07")]
		[Row(false, @"4/13/07")]
		[Row(false, @"4/003/07")]
		[Row(true, @"4/8/07")]
		[Row(false, @"12/0 2/08")]
		//year
		[Row(false, @"4/1/0.7")]
		[Row(false, @"4/12/-07")]
		[Row(false, @"4/10/0")]
		[Row(false, @"4/11/10000")]
		[Row(false, @"4/10/00007")]
		[Row(true, @"4/8/117")]
		[Row(true, @"4/8/17")]
		[Row(false, @"4/8/7")]
		[Row(false, @"12/02/0 8")]
		//special character
		[Row(false, @"`<~!@#$%^&*()-[]{}/?;:\|,.'>")]
		[Row(false, "a\"/cd/ef")]
		[Row(false, @"一二/三四/五六")]
		//lack one 
		[Row(false, @"12/07")]
		[Row(false, @"12")]
		public void CreatedBeforeValidationTest(bool isValidInput, string time)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// Fill in data
				ie.FindListPageTextFieldByControlName(("txtCreatedBefore")).TypeText(time);

				// check required fields
				Span revWindowsId = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucList_cvCreatedBefore"));

				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				if (!isValidInput)
				{
					WatinAssert.TextEquals(@"Please enter a valid date (e.g. mm/dd/yy).", revWindowsId);
				}
			}
		}
		[RowTest]
		[Row(true, @"2")]
		[Row(true, @"0")]
		[Row(true, @"0000000000000000")]
		[Row(true, @" 2 ")]
		[Row(true, @"01")]
		[Row(true, @"000000001")]
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
		[Row(true, @"9999999999")]
		[Row(false, @"-9999999999")]
		public void ValidationIDInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// Fill in data
				ie.FindListPageTextFieldByControlName(("txtValidationProxyURLId")).TypeText(name);

				// check required fields
				Span revWindowsId = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucList_revValidationProxyURLId"));

				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				if (!isValidInput)
				{
					WatinAssert.TextEquals(@"Must be an integer.", revWindowsId);
				}
			}
		}

		[RowTest]
		[Row(true, @"2")]
		[Row(true, @"0")]
		[Row(true, @"0000000000000000")]
		[Row(true, @" 2 ")]
		[Row(true, @"01")]
		[Row(true, @"000000001")]
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
		[Row(true, @"9999999999")]
		[Row(false, @"-9999999999")]
		public void PublishIDInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// Fill in data
				ie.FindListPageTextFieldByControlName(("txtElementsProxyURLId")).TypeText(name);

				// check required fields
				Span revWindowsId = ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucList_revElementsProxyURLId"));

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
