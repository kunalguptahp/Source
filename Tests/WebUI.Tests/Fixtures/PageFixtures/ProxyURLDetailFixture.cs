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
	public class ProxyURLDetailFixture : SharedIEInstanceTestFixture
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				string[] domainInfo = { "http://NewProxyURL.com", "Active.ProxyURLTypeStatus", @"lastd, firstd (americas\lastd)" };

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtURL").TypeText(domainInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlProxyURLType").Select(domainInfo[1]);
				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[2]);
				ie.FindSaveButton().Click();
				
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtProxyURL").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);

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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				string[] domainInfo = { "http://NewProxyURL.com", "ProxyURLTypeOfA", @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew", "TouchpointOfB", "LocaleOfB", "PlatformOfB" };

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtURL").TypeText(domainInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlProxyURLType").Select(domainInfo[1]);
				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[3]);
				ie.FindDetailPageTextFieldByControlName("txtTags").TypeText(domainInfo[4]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl03_ddlParameterValue").Select(domainInfo[5]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl04_ddlParameterValue").Select(domainInfo[6]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl05_ddlParameterValue").Select(domainInfo[7]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtProxyURL").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);

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
		public void EditSingleRecordTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				string project = @"ProxyURLOfA";
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText(project);
				ie.FindFilterButton().Click();

				string[] domainInfo = { "http://NewProxyURL.com", "ProxyURLTypeOfA", "PlatformOfA", @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew", "TouchpointOfB", "LocaleOfB", "PlatformOfB" };
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.FindDetailPageTextFieldByControlName("txtURL").TypeText(domainInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlProxyURLType").Select(domainInfo[1]);
				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[3]);
				ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[4]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[5]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl03_ddlParameterValue").Select(domainInfo[6]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl04_ddlParameterValue").Select(domainInfo[7]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl05_ddlParameterValue").Select(domainInfo[8]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtProxyURL").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);
				ElementsCPSWatinUtility.VerifyModifySuccess(ie, domainInfo, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				Assert.AreEqual(ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "lblIdValue")).Text, "1");
			}
		}

		[Test]
		public void ValidateDuplicateParameterTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				
				string[] domainInfo = { "http://NewProxyURL.com", "ProxyURLTypeOfA", "PlatformOfA", @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew", "TouchpointOfB", "LocaleOfA", "PlatformOfA" };
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;

				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl03_ddlParameterValue").Select(domainInfo[6]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl04_ddlParameterValue").Select(domainInfo[7]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl05_ddlParameterValue").Select(domainInfo[8]);
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnReadyForValidation")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnValidate")).Click();				


				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("You have saved a Redirector that is already validated or published with the same parameter values.",ie);
				
			}
		}
		[Test]
		public void EditMultipleRecordTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl10_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnEdit")).Click();
				
				string[] domainInfo = { "http://NewProxyURL.com", "ProxyURLTypeOfA",  @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew", "TouchpointOfB", "LocaleOfB", "PlatformOfB","Abandoned" };

				ie.FindDetailPageTextFieldByControlName("txtURL").TypeText(domainInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[3]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[4]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToRemove").TypeText("TagOfB TagOfA NoneExist TagOfD");
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl03_ddlParameterValue").Select(domainInfo[5]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl04_ddlParameterValue").Select(domainInfo[6]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl05_ddlParameterValue").Select(domainInfo[7]);
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnAbandon")).Click();
				ie.FindSaveButton().Click();

				string[] firRecord = { "NewTag", "SecondNew", "TagOfC" };
				string[] secRecord = { "NewTag", "SecondNew" };

				ie.GoTo(pageUri);				
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("New");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);

				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(firRecord, ie);
				ElementsCPSWatinUtility.VerifyModifySuccess(ie, domainInfo, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl10_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnEdit")).Click(); 
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(secRecord, ie);
				ElementsCPSWatinUtility.VerifyModifySuccess(ie, domainInfo, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
			}
		}

		[Test]
		public void MultipleNewRecordTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				string[] domainInfo = { "http://NewProxyURL.com", "ProxyURLTypeOfA", "PlatformOfA", @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew", "TouchpointOfB", "LocaleOfB", "PlatformOfA", "http://SecondNewProxyURL.com", "TouchpointOfB", "LocaleOfA", "PlatformOfA" };

				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnMultiCreate")).Click();
				ie.FindDetailPageSelectListByControlName("ddlProxyURLType").Select(domainInfo[1]);

				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + Constants.MultiEdit + "btnInsert")).Click();
				ie.FindDetailPageTextFieldByControlId(Constants.MultiEditWithGvList + "ctl03_" + "txtTargetUrl").TypeText(domainInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[3]);
				ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[4]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[5]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl03_ddlTouchpoint").Select(domainInfo[6]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl03_ddlLocale").Select(domainInfo[7]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl03_ddlPlatform").Select(domainInfo[8]);
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + Constants.MultiEdit + "btnInsert")).Click();

				ie.FindDetailPageTextFieldByControlId(Constants.MultiEditWithGvList + "ctl04_" + "txtTargetUrl").TypeText(domainInfo[9]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl04_ddlTouchpoint").Select(domainInfo[10]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl04_ddlLocale").Select(domainInfo[11]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl04_ddlPlatform").Select(domainInfo[12]);
				ie.FindSaveButton().Click();
				WatinAssert.PageBodyHtmlContains(domainInfo[0], ie);
				WatinAssert.PageBodyHtmlContains(domainInfo[9], ie);
				
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Click Save
				ie.FindSaveButton().Click();

				// check required fields
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(@"Please enter a target URL.", ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(@"Please select a Redirector type.", ie);
				

			}
		}

		[Test]
		public void ParameterRequiredFieldValidatorTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				string[] domainInfo = { "http://NewProxyURL.com", "ProxyURLTypeOfA", "PlatformOfA", @"lastd, firstd (americas\lastd)", "New", "NewTag", "NewA", "SecondNewC", "SecondNewE" };

				ie.FindDetailPageTextFieldByControlName("txtURL").TypeText(domainInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlProxyURLType").Select(domainInfo[1]);
				// Click Save
				ie.FindSaveButton().Click();
				
				// check required fields
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(@"Please select parameter value(s).", ie);
				
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
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();
				string[] domainInfo = { "http://NewProxyURL.com", "ProxyURLTypeOfA", "PlatformOfA", @"lastd, firstd (americas\lastd)" };

				ie.FindDetailPageTextFieldByControlName("txtURL").TypeText(domainInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlProxyURLType").Select(domainInfo[1]);
				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[3]);
				
				// Click Cancel
				ie.FindCancelButton().Click();

				// new project exists?
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtProxyURL")).TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName(("txtProxyURL")).TypeText("");
				ElementsCPSWatinUtility.AssertPageBodyTextExcludes(domainInfo[0], ie);
			}
		}

		[Test]
		public void MultiReplaceUnPublishedTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				WatiN.Core.DialogHandlers.AlertDialogHandler dh = new WatiN.Core.DialogHandlers.AlertDialogHandler();

				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.AddDialogHandler(dh);//增加一个控制句柄

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl04_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnMultiReplace")).ClickNoWait();
				dh.WaitUntilExists();
				string mess = dh.Message;
				dh.OKButton.Click();//点击这个窗口的OK按钮
				ie.WaitForComplete();
				ie.RemoveDialogHandler(dh);

				Assert.AreEqual("To replace redirector URLs, all selected redirectors must be published.", mess);
			}
		}

		[Test]
		public void MultiReplaceTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl06_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl09_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnMultiReplace")).Click();

				string[] domainInfo = { @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew" };
				string[] firRecord = { "http://firstNew.com", "NewA", "NewTag", "SecondNew", "TagOfD","Modified" };
				string[] secRecord = { "http://secondNew.com", "NewB", "NewTag", "SecondNew", "Modified" };

				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[0]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToRemove").TypeText("TagOfB TagOfA NoneExisting");
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl03_txtTargetUrl").TypeText(firRecord[0]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl03_txtDescription").TypeText(firRecord[1]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl04_txtTargetUrl").TypeText(secRecord[0]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl04_txtDescription").TypeText(secRecord[1]);
				ie.FindSaveButton().Click();              
			                                                                                                               
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl12_CheckBoxButton")).Checked = true;
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(firRecord, ie);

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl13_CheckBoxButton")).Checked = true;
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(secRecord, ie);

			}
		}

		[Test]
		public void MultiReplaceAndValidateTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl06_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl09_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnMultiReplace")).Click();

				string[] domainInfo = { @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew" };
				string[] firRecord = { "http://firstNew.com", "NewA", "NewTag", "SecondNew", "TagOfD", "Modified" };
				string[] secRecord = { "http://secondNew.com", "NewB", "NewTag", "SecondNew", "Modified" };

				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[0]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToRemove").TypeText("TagOfB TagOfA NoneExisting");
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl03_txtTargetUrl").TypeText(firRecord[0]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl03_txtDescription").TypeText(firRecord[1]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl04_txtTargetUrl").TypeText(secRecord[0]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl04_txtDescription").TypeText(secRecord[1]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl12_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl04_ddlParameterValue").Select("BrandOfA");
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnReadyForValidation")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnValidate")).Click();				

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl12_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Validated", ie);
				//check other's state are right
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl09_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Published (Production Only)", ie);
				Assert.AreEqual(0, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnUnPublish")).Enabled);
				Assert.AreEqual(0, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnPublish")).Enabled);

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl06_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Published (Production Only)", ie);
				Assert.AreEqual(0, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnUnPublish")).Enabled);
				Assert.AreEqual(0, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnPublish")).Enabled);

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Validated", ie);

			}
		}

		[Test]
		public void MultiReplaceAndReworkTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl06_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl09_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnMultiReplace")).Click();

				string[] domainInfo = { @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew" };
				string[] firRecord = { "http://firstNew.com", "NewA", "NewTag", "SecondNew", "TagOfD", "Modified" };
				string[] secRecord = { "http://secondNew.com", "NewB", "NewTag", "SecondNew", "Modified" };

				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[0]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToRemove").TypeText("TagOfB TagOfA NoneExisting");

				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl03_txtTargetUrl").TypeText(firRecord[0]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl03_txtDescription").TypeText(firRecord[1]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl04_txtTargetUrl").TypeText(secRecord[0]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl04_txtDescription").TypeText(secRecord[1]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl12_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl04_ddlParameterValue").Select("BrandOfA");
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnReadyForValidation")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnValidate")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnRework")).Click();

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl12_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Modified", ie);
				//check other's state are right
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl09_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Published", ie);
				Assert.AreEqual(0, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnUnPublish")).Enabled);
				Assert.AreEqual(0, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnPublish")).Enabled);

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl06_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Published", ie);
				Assert.AreEqual(0, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnUnPublish")).Enabled);
				Assert.AreEqual(0, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnPublish")).Enabled);

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Validated", ie);

			}
		}


		[Test]
		public void MultiReplaceAndPublishTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl06_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl09_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnMultiReplace")).Click();

				string[] domainInfo = { @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew" };
				string[] firRecord = { "http://firstNew.com", "NewA", "NewTag", "SecondNew", "TagOfD", "Modified" };
				string[] secRecord = { "http://secondNew.com", "NewB", "NewTag", "SecondNew", "Modified" };

				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[0]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToRemove").TypeText("TagOfB TagOfA NoneExisting");

				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl03_txtTargetUrl").TypeText(firRecord[0]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl03_txtDescription").TypeText(firRecord[1]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl04_txtTargetUrl").TypeText(secRecord[0]);
				ie.FindDetailPageTextFieldByControlId(Constants.MultiReplace + "ctl04_txtDescription").TypeText(secRecord[1]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdDetailPrefix + "gvList_ctl12_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();

				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl04_ddlParameterValue").Select("BrandOfA");
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnReadyForValidation")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnValidate")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnPublish")).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Published", ie);

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdDetailPrefix + "gvList_ctl06_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Cancelled", ie);
				Assert.AreEqual(1, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnRework")).Enabled);
				Assert.AreEqual(1, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnPublish")).Enabled);

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdDetailPrefix + "gvList_ctl09_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Cancelled", ie);

				Assert.AreEqual(1, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnRework")).Enabled);
				Assert.AreEqual(1, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnPublish")).Enabled);
				//check other's state are right
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdDetailPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Validated", ie);

			}
		}

		[Test]
		public void MultiEditDiffRedirectorTypeTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				WatiN.Core.DialogHandlers.AlertDialogHandler dh = new WatiN.Core.DialogHandlers.AlertDialogHandler();

				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.AddDialogHandler(dh);//增加一个控制句柄

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl04_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnEdit")).ClickNoWait();
				dh.WaitUntilExists();
				string mess = dh.Message;
				dh.OKButton.Click();//点击这个窗口的OK按钮
				ie.WaitForComplete();
				ie.RemoveDialogHandler(dh);
				           
				Assert.AreEqual("When using Multi edit, all redirector should have same type.", mess);
			}
		}

		[Test]
		public void MultiEditSameRedirectorTypeTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl10_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnMultiEdit")).Click();

				string[] domainInfo = { @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew"};
				string[] firRecord = { "TouchpointOfB", "LocaleOfB", "PlatformOfB", "NewTag", "SecondNew", "TagOfC" };
				string[] secRecord = { "TouchpointOfA", "LocaleOfA", "PlatformOfA", "NewTag", "SecondNew" };

				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[0]);
				ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[1]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToAdd").TypeText(domainInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtTagsToRemove").TypeText("TagOfB TagOfA NoneExisting TagOfD");
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl03_ddlTouchpoint").Select(firRecord[0]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl03_ddlLocale").Select(firRecord[1]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl03_ddlPlatform").Select(firRecord[2]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl04_ddlTouchpoint").Select(secRecord[0]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl04_ddlLocale").Select(secRecord[1]);
				ie.FindDetailPageSelectListByControlId(Constants.MultiEditWithGvList + "ctl04_ddlPlatform").Select(secRecord[2]);
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtProxyURL").TypeText("http://ProxyURLOfA.com");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);
				//ie.Link(Find.ByUrl("http://localhost/ElementsCPS/WebUI/Redirect/ProxyURLUpdate.aspx?id=1")).Click();
				ElementsCPSWatinUtility.VerifyModifySuccess(ie, firRecord, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());

				ie.GoTo(pageUri);
				//ie.Link(Find.ByUrl("http://localhost/ElementsCPS/WebUI/Redirect/ProxyURLUpdate.aspx?id=8")).Click();

				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtProxyURL").TypeText("http://ProxyURLOfH.com");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);
				ElementsCPSWatinUtility.VerifyModifySuccess(ie, secRecord, ElementsCPSWatinUtility.Modifier, DateTime.Now.ToUniversalTime().ToUniversalTime());
				
			}
		}

		


		[RowTest]
		[Row("Abandoned", "btnAbandon", "gvList_ctl03_CheckBoxButton", new string[] { "btnReadyForValidation", "btnRework" })]
		[Row("Ready For Validation", "btnReadyForValidation", "gvList_ctl03_CheckBoxButton", new string[] { "btnAbandon", "btnRework", "btnValidate" })]
		[Row("Validated", "btnValidate", "gvList_ctl04_CheckBoxButton", new string[] { "btnAbandon", "btnRework", "btnValidate" })]
		[Row("Modified", "btnRework", "gvList_ctl04_CheckBoxButton", new string[] { "btnAbandon", "btnReadyForValidation" })]
		[Row("Published", "btnPublish", "gvList_ctl05_CheckBoxButton", new string[] { "btnPublish", "btnUnPublish" })]
		[Row("Cancelled", "btnUnPublish", "gvList_ctl06_CheckBoxButton", new string[] { "btnPublish", "btnRework" })]
		public void StatusValidationTest(string changedStatus, string buttonId, string recordCheckBoxToBeUsed,string[] buttonsEnabled)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + recordCheckBoxToBeUsed)).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnEdit")).Click();
				// Fill in data
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + buttonId)).Click();

				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(changedStatus, ie);
				foreach (string button in buttonsEnabled)
				{
					Assert.AreEqual(true, ie.Button(Find.ById(Constants.ControlIdDetailPrefix + button)).Enabled);
				}
			}
		}
		[RowTest]
		[Row("btnPublish", "gvList_ctl09_CheckBoxButton")]
		//[Row("btnValidate", "gvList_ctl08_CheckBoxButton")]
		public void UniqueValidationTest(string buttonId,string recordCheckBoxId)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + recordCheckBoxId)).Click();

				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + buttonId)).Click();
				//Thread.Sleep(5000);
				//WatinAssert.IsVisible(true, ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "cvProxyURLPublishable")));
				WatinAssert.PageBodyTextContains("This redirector is not ready to be published. Please make sure that all offer data is complete and valid.", ie);
				
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
		[Row(true, @"0_1i3")]
		[Row(true, @"0_1i3 0_1i3")]
		[Row(true, @"l23rt h23tt tdsf3")]//space delimited
		[Row(false, @"l23rt h23t tdsf3")]
		[Row(true, @" o8123 ")]
		[Row(false, @" abcd")]
		[Row(false, @"cdea ")]
		[Row(true, @"er;drop table RowStatus")]
		public void TagsInputValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtTags")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

				Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "revTxtTagsMinTagNameLength"));

				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				if (!isValidInput)
				{
					WatinAssert.TextEquals(@"Tag names shorter than 5 characters are not allowed.", revWindowsId);
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
		[Row(false, @"l23rt j六ab9d tdsf3")]
		[Row(false, @"jtghfhgf`~!@#$%^&*0123456789()[]{}/?;:\<|,.'>")]
		[Row(false, @"j六ab9d")]
		[Row(false, @"j*ab8d")]
		[Row(false, "a\"4neee")] // for "
		public void TagsInputCharacterValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtTags")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

				Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "revTxtTagsInvalidTagNameChars"));

				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				if (!isValidInput)
				{
					WatinAssert.TextEquals(@"Tag names must contain only letters, numbers, and underscores.", revWindowsId);
				}
			}
		}

		[RowTest]
		[Row(false, @"dfsfd _dabcd")]
		[Row(false, @"_dabcd")]
		[Row(false, @"2fabcd")]
		[Row(false, @"六fabcd")]
		[Row(false, @"*fabcd")]
		public void TagsInputBeginWithValidationTest(bool isValidInput, string name)
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);

				// Create new button
				ie.FindCreateButton().Click();

				// Fill in data
				ie.FindDetailPageTextFieldByControlName(("txtTags")).TypeText(name);

				// Click Save
				ie.FindSaveButton().Click();

				Span revWindowsId = ie.Span(Find.ById(Constants.ControlIdDetailPrefix + "revTxtTagsTagNameFirstChar"));

				WatinAssert.IsVisible(!isValidInput, revWindowsId);
				if (!isValidInput)
				{
					WatinAssert.TextEquals(@"Tag names must begin with a letter.", revWindowsId);
				}
			}
		}

		#endregion

		#region Tests: Security/Hacking

		[Test]
		public void DuplicateParameterTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/ProxyURLTypeList.aspx");
				string[] RedirectorTypeInfo = { "DuplicateRedirector", @"itg-search.rssx.hp.com", "redirect.compaq.ac.legacy", "1" };

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtName").TypeText(RedirectorTypeInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlDomain").Select(RedirectorTypeInfo[1]);
				ie.FindDetailPageSelectListByControlName("ddlGroupType").Select(RedirectorTypeInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtElementsKey").TypeText(RedirectorTypeInfo[3]);
				ie.FindSaveButton().Click();

				pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/QueryParameterList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("Touchpoint");
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkProxyURLTypeList$3")).Checked = true;
				ie.FindSaveButton().Click();

				pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/QueryParameterList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("Locale");
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkProxyURLTypeList$3")).Checked = true;
				ie.FindSaveButton().Click();

				pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("DataAdmin/QueryParameterList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtName")).TypeText("Platform");
				ie.FindFilterButton().Click();
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.CheckBox(Find.ByName(Constants.ControlNameDetailPrefix + "chkProxyURLTypeList$3")).Checked = true;
				ie.FindSaveButton().Click();

				pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				string[] domainInfo = { "http://NewProxyURL.com", RedirectorTypeInfo[0], @"lastd, firstd (americas\lastd)", "New", "NewTag, SecondNew", "TouchpointOfB", "LocaleOfA", "PlatformOfA" };

				ie.GoTo(pageUri);
				ie.FindCreateButton().Click();
				ie.FindDetailPageTextFieldByControlName("txtURL").TypeText(domainInfo[0]);
				ie.FindDetailPageSelectListByControlName("ddlProxyURLType").Select(domainInfo[1]);
				ie.FindDetailPageSelectListByControlName("ddlOwner").Select(domainInfo[2]);
				ie.FindDetailPageTextFieldByControlName("txtDescription").TypeText(domainInfo[3]);
				ie.FindDetailPageTextFieldByControlName("txtTags").TypeText(domainInfo[4]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl03_ddlParameterValue").Select(domainInfo[5]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl04_ddlParameterValue").Select(domainInfo[6]);
				ie.FindDetailPageSelectListByControlId(Constants.QueryParameterValueEditUpdatePanel + "ctl05_ddlParameterValue").Select(domainInfo[7]);
				ie.FindSaveButton().Click();

				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlName("txtProxyURL").TypeText(domainInfo[0]);
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnReadyForValidation")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnValidate")).Click();				

			}
		}

		[Test]
		public void DeleteTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlName(("txtDescription")).TypeText("ProxyURLOfA");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickSecondEditButton(ie);
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnAbandon")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnDelete")).Click();

				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Deleted", ie);
				ie.GoTo(pageUri);
				ElementsCPSWatinUtility.AssertPageBodyHtmlExcludes("ProxyURLOfA", ie);
			}
		}

		[Test]
		public void DeleteInEditPageTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageSelectListByControlId(("ddlProxyURLType")).Select("ProxyURLTypeOfA");
				ie.FindListPageSelectListByControlId(("ddlProxyURLStatus")).Select("Modified");
				ie.FindFilterButton().Click();
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_HeaderButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnAbandon")).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnDelete")).Click();

				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("Deleted", ie);
				string[] result={"Success Delete","Success Delete"};
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(result, ie);
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageSelectListByControlId(("ddlProxyURLType")).Select("ProxyURLTypeOfA");
				ie.FindListPageSelectListByControlId(("ddlProxyURLStatus")).Select("Modified");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no Redirections found", ie);
			}
		}

		[Test]
		public void DeleteDifferentStatusInEditPageTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnAbandon")).Click();

				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl05_CheckBoxButton")).Checked = true;
				ie.FindButtonByText(Constants.ButtonText_EditDotDotDot).Click();
				ie.Button(Find.ById(Constants.ControlIdDetailPrefix + "btnDelete")).Click();


				string[] result = { "ProxyURLOfA", "Success Delete", "ProxyURLOfC", "Unable to Delete: Redirector must be 'Abandoned'." };
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(result, ie);
				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				// filter by emails starting with "person."
				ie.FindListPageTextFieldByControlId(("txtIdList")).TypeText("1");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains("There are no Redirections found", ie);
			}
		}

		[Test]
		public void CopyTest()
		{
			EnhancedIE ie = this.SharedIEInstance;
			{
				Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri("Redirect/ProxyURLList.aspx");
				ie.GoTo(pageUri);
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl03_CheckBoxButton")).Checked = true;
				ie.CheckBox(Find.ById(Constants.ControlIdListPrefix + "gvList_ctl06_CheckBoxButton")).Checked = true;
				ie.Button(Find.ById(Constants.ControlIdListPrefix + "btnCopy")).Click();

				ie.TextField(Find.ById(Constants.ControlIdDetailPrefix + "txtTagsToAdd")).TypeText("TagOfF");
				ie.TextField(Find.ById(Constants.ControlIdDetailPrefix + "txtTagsToRemove")).TypeText("TagOfC,TagOfD");
				ie.FindSaveButton().Click();

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();				
				ie.FindListPageTextFieldByControlId(("txtProxyURL")).TypeText(@"http://ProxyURLOfA.com");
				ie.FindListPageSelectListByControlId(("ddlProxyURLStatus")).Select("Modified");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickThirdEditButton(ie);
				string[] result={"TagOfA","TagOfE","TagOfF"};
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(result, ie);

				ie.GoTo(pageUri);
				ie.DoClickFilterAreaExpansionToggle();
				ie.FindListPageTextFieldByControlId(("txtProxyURL")).TypeText(@"http://ProxyURLOfD.com");
				ie.FindFilterButton().Click();
				ElementsCPSWatinUtility.ClickThirdEditButton(ie);				
				result = new string[]{"TagOfF" };
				ElementsCPSWatinUtility.AssertPageBodyHtmlContains(result, ie);
			}
		}
		#endregion
	}
}
