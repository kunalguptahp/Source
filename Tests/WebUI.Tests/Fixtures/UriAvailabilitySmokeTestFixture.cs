using System;
using System.Threading;
using Gallio.Model;
using HP.ElementsCPS.Apps.WebUI.Tests.Extensions.Utility.ElementsCPSWatinTestMaintainability;
using HP.ElementsCPS.Apps.WebUI.Tests.Utility;
using HP.HPFx.Diagnostics.Testing;
using HP.ElementsCPS.Data.Tests.Utility;
using MbUnit.Framework;

namespace HP.ElementsCPS.Apps.WebUI.Tests.Fixtures
{
	/// <summary>
	/// This fixture verifies that all of the app's valid "direct access" URLs are able to be loaded by the browser without any server errors.
	/// </summary>
	[TestFixture][ApartmentState(ApartmentState.STA)] //[TestFixture(ApartmentState = ApartmentState.STA)] //, TimeOut = 20)]
	[DependsOn(typeof(DefaultFixture))]
	[Importance(Importance.Critical)]
	[Category(TestCategory.Kind.Functional)]
	[Category(TestCategory.Speed.VerySlow)]
	[Category(TestCategory.DependsOn.DB)]
	[Category(TestCategory.DependsOn.IIS)]
	[Category(TestCategory.DependsOn.IIS + ".ElementsCPS")]
	public class UriAvailabilitySmokeTestFixture : SharedIEInstanceTestFixture
	{
		protected override void TestFixtureDataSetUp()
		{
			DataUtility.RestoreDB_SimpleData();
		}

		#region Tests: Happy Path

		/// <summary>
		/// Tests that the app's directories/folders can be accessed/browsed (i.e. without specifying a page/file name) without a server error.
		/// </summary>
		/// <param name="pageUrl">The app-relative URL of the page to test.</param>
		/// <param name="assertAtHpPortalVisible"></param>
        [Test]
		[Row("", true)]
		[Row("DataAdmin/", true)]
		[Row("MyInfo/", true)]
		[Row("Registration/", true)]
		[Row("SystemAdmin/", true)]
		[Row("UserAdmin/", true)]
		public void BrowsableFolderSmokeTest(string pageUrl, bool assertAtHpPortalVisible)
		{
			Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri(pageUrl);
			ElementsCPSWatinTest.AssertElementsCPSPageOpensWithoutServerError(this.SharedIEInstance, pageUri, assertAtHpPortalVisible);
		}

		/// <summary>
		/// Tests that the app's "directly accessible" pages can be accessed without a server error.
		/// </summary>
		/// <param name="pageUrl">The app-relative URL of the page to test.</param>
		/// <param name="assertAtHpPortalVisible"></param>
	    ///[Ignore("Disabled for now since these are mostly redundant to the WebPageSmokeTest_ContentOnlyDisplayMode tests, and are also slower.")]
		[Test]
		[Row("Default.aspx", true)]
		[Row("ApplicationError.aspx", true)]
		[Row("ConfigurationService/ConfigurationServiceDefault.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupCopy.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupEditUpdate.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupImportList.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupImportUpdate.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupList.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupMultiReplaceUpdate.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupReport.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupUpdate.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupSelectorCopy.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupSelectorUpdate.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceGroupUpdate.aspx", true)]
        [Row("ConfigurationService/ConfigurationServiceLabelValueImportUpdate.aspx", true)]
        [Row("ConfigurationService/WorkflowList.aspx", true)]
        [Row("ConfigurationService/WorkflowMultiReplaceUpdate.aspx", true)]
        [Row("ConfigurationService/WorkflowReport.aspx", true)]
        [Row("ConfigurationService/WorkflowSelectorCopy.aspx", true)]
        [Row("ConfigurationService/WorkflowSelectorUpdate.aspx", true)]
        [Row("ConfigurationService/WorkflowUpdate.aspx", true)]
        [Row("ConfigurationService/WorkflowVersionReport.aspx", true)]
        [Row("DataAdmin/ConfigurationServiceApplicationList.aspx", true)]
        [Row("DataAdmin/ConfigurationServiceApplicationTypeList.aspx", true)]
        [Row("DataAdmin/ConfigurationServiceApplicationTypeUpdate.aspx", true)]
        [Row("DataAdmin/ConfigurationServiceApplicationUpdate.aspx", true)]
        [Row("DataAdmin/ConfigurationServiceApplicationList.aspx", true)]
        [Row("DataAdmin/ConfigurationServiceGroupTypeList.aspx", true)]
		[Row("DataAdmin/ConfigurationServiceGroupTypeUpdate.aspx", true)]
		[Row("DataAdmin/ConfigurationServiceItemList.aspx", true)]
		[Row("DataAdmin/ConfigurationServiceItemUpdate.aspx", true)]
		[Row("DataAdmin/ConfigurationServiceLabelList.aspx", true)]
		[Row("DataAdmin/ConfigurationServiceLabelUpdate.aspx", true)]
        [Row("DataAdmin/DataAdminDefault.aspx", true)]
        [Row("DataAdmin/EntityTypeList.aspx", true)]
        [Row("DataAdmin/EntityTypeUpdate.aspx", true)]
        [Row("DataAdmin/EntityTypeUpdate.aspx?id=1", true)]
        [Row("DataAdmin/JumpstationApplicationList.aspx", true)]
        [Row("DataAdmin/JumpstationApplicationUpdate.aspx", true)]
        [Row("DataAdmin/JumpstationGroupTypeList.aspx", true)]
        [Row("DataAdmin/JumpstationGroupTypeUpdate.aspx", true)]
        [Row("DataAdmin/JumpstationMacroList.aspx", true)]
        [Row("DataAdmin/JumpstationMacroUpdate.aspx", true)]
        [Row("DataAdmin/NoteList.aspx", true)]
        [Row("DataAdmin/NoteUpdate.aspx", true)]
        [Row("DataAdmin/NoteUpdate.aspx?id=1", true)]
        [Row("DataAdmin/NoteTypeList.aspx", true)]
        [Row("DataAdmin/NoteTypeUpdate.aspx", true)]
        [Row("DataAdmin/ProxyURLDomainList.aspx", true)]
        [Row("DataAdmin/ProxyURLDomainUpdate.aspx", true)]
        [Row("DataAdmin/ProxyURLGroupTypeList.aspx", true)]
        [Row("DataAdmin/ProxyURLGroupTypeUpdate.aspx", true)]
        [Row("DataAdmin/ProxyURLTypeList.aspx", true)]
        [Row("DataAdmin/ProxyURLTypeUpdate.aspx", true)]
        [Row("DataAdmin/QueryParameterConfigurationServiceGroupTypeList.aspx", true)]
        [Row("DataAdmin/QueryParameterConfigurationServiceGroupTypeUpdate.aspx", true)]
        [Row("DataAdmin/QueryParameterList.aspx", true)]
        [Row("DataAdmin/QueryParameterProxyURLTypeList.aspx", true)]
        [Row("DataAdmin/QueryParameterProxyURLTypeUpdate.aspx", true)]
        [Row("DataAdmin/QueryParameterUpdate.aspx", true)]
        [Row("DataAdmin/QueryParameterValueList.aspx", true)]
        [Row("DataAdmin/QueryParameterValueUpdate.aspx", true)]
        [Row("DataAdmin/QueryParameterWorkflowTypeList.aspx", true)]
        [Row("DataAdmin/QueryParameterWorkflowTypeUpdate.aspx", true)]
        [Row("DataAdmin/WorkflowApplicationList.aspx", true)]
        [Row("DataAdmin/WorkflowApplicationTypeList.aspx", true)]
        [Row("DataAdmin/WorkflowApplicationTypeUpdate.aspx", true)]
        [Row("DataAdmin/WorkflowApplicationUpdate.aspx", true)]
        [Row("DataAdmin/WorkflowConditionList.aspx", true)]
        [Row("DataAdmin/WorkflowConditionUpdate.aspx", true)]
        [Row("DataAdmin/WorkflowModuleCategoryList.aspx", true)]
        [Row("DataAdmin/WorkflowModuleCategoryUpdate.aspx", true)]
        [Row("DataAdmin/WorkflowModuleList.aspx", true)]
        [Row("DataAdmin/WorkflowModuleSubCategoryList.aspx", true)]
        [Row("DataAdmin/WorkflowModuleSubCategoryUpdate.aspx", true)]
        [Row("DataAdmin/WorkflowModuleUpdate.aspx", true)]
        [Row("DataAdmin/WorkflowTypeList.aspx", true)]
        [Row("DataAdmin/WorkflowTypeUpdate.aspx", true)]
		[Row("Jumpstation/JumpstationDefault.aspx", true)]
        //[Row("Jumpstation/JumpstationGroupCopy.aspx", true)]
        //[Row("Jumpstation/JumpstationGroupEditUpdate.aspx", true)]
        [Row("Jumpstation/JumpstationGroupList.aspx", true)]
        [Row("Jumpstation/JumpstationGroupByQueryParameterList.aspx", true)]
        [Row("Jumpstation/JumpstationGroupMultiReplaceUpdate.aspx", true)]
        //[Row("Jumpstation/JumpstationGroupReport.aspx", true)]
        [Row("Jumpstation/JumpstationGroupSelectorCopy.aspx", true)]
        [Row("Jumpstation/JumpstationGroupSelectorUpdate.aspx", true)]
        [Row("Jumpstation/JumpstationGroupUpdate.aspx", true)]
		[Row("MyInfo/MyInfoDefault.aspx", true)]
		[Row("MyInfo/PersonDetailForMyInfo.aspx", true)]		
        [Row("Redirect/ProxyURLCopy.aspx", true)]
		[Row("Redirect/ProxyURLEditUpdate.aspx", true)]
		[Row("Redirect/ProxyURLList.aspx", true)]
		[Row("Redirect/ProxyURLMultiEditUpdate.aspx", true)]
		[Row("Redirect/ProxyURLMultiReplaceUpdate.aspx", true)]
		[Row("Redirect/ProxyURLUpdate.aspx", true)]
		[Row("Redirect/RedirectDefault.aspx", true)]
		[Row("Registration/Register.aspx", true)]
		[Row("SystemAdmin/DataImport.aspx", true)]
		[Row("SystemAdmin/DevScaffold.aspx", true)]
		[Row("SystemAdmin/DevTestPage.aspx", true)]
		[Row("SystemAdmin/LogDetail.aspx", true)]
        [Row("SystemAdmin/SystemAdminProcessConsole.aspx", true)]
		[Row("SystemAdmin/SystemAdminFileDownload.aspx", true)]
		[Row("SystemAdmin/SystemAdminFileUpload.aspx", true)]
		[Row("SystemAdmin/LogList.aspx", true)]
		[Row("SystemAdmin/SystemAdminDashboard.aspx", true)]
		[Row("SystemAdmin/SystemAdminDefault.aspx", true)]
		[Row("SystemAdmin/SystemLog.aspx", true)]
		[Row("SystemAdmin/ViewLog.aspx", true)]
		[Row("UserAdmin/PersonList.aspx", true)]
		[Row("UserAdmin/PersonDetail.aspx", true)]
		[Row("UserAdmin/PersonUpdate.aspx", true)]
		[Row("UserAdmin/UserAdminDefault.aspx", true)]
		public void WebPageSmokeTest(string pageUrl, bool assertAtHpPortalVisible)
		{
			//Assert.TerminateSilently(TestOutcome.Skipped, "Disabled for now since these are mostly redundant to the WebPageSmokeTest_ContentOnlyDisplayMode tests, and are also slower.");
			Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri(pageUrl);
			ElementsCPSWatinTest.AssertElementsCPSPageOpensWithoutServerError(this.SharedIEInstance, pageUri, assertAtHpPortalVisible);
		}

		/// <summary>
		/// Tests that the app's "directly accessible" pages can be accessed without a server error.
		/// </summary>
		/// <param name="pageUrl">The app-relative URL of the page to test.</param>
		/// <param name="assertAtHpPortalVisible"></param>
		//[Ignore("Disabled for now since these are mostly redundant to the WebPageSmokeTest tests, and would increase build times without adding much value.")]
		[Test]
		[Row("Default.aspx?co=y", false)]
		//[Row("ApplicationError.aspx?co=y", false)]
		//[Row("ConfigurationService/ConfigurationServiceDefault.aspx?co=y", false)]
		//[Row("ConfigurationService/ConfigurationServiceGroupList.aspx?co=y", false)]
		//[Row("ConfigurationService/ConfigurationServiceGroupUpdate.aspx?co=y", false)]
		//[Row("ConfigurationService/ConfigurationServiceGroupEditUpdate.aspx?co=y", false)]
		//[Row("ConfigurationService/ConfigurationServiceGroupCopy.aspx?co=y", false)]
		//[Row("ConfigurationService/ConfigurationServiceGroupMultiReplaceUpdate.aspx", true)]
		//[Row("ConfigurationService/ConfigurationServiceGroupReport.aspx?co=y", false)]
		//[Row("ConfigurationService/ConfigurationServiceGroupSelectorCopy.aspx?co=y", false)]
		//[Row("DataAdmin/ConfigurationServiceApplicationList.aspx?co=y", false)]
		//[Row("DataAdmin/ConfigurationServiceApplicationUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/ConfigurationServiceGroupTypeList.aspx?co=y", false)]
		//[Row("DataAdmin/ConfigurationServiceGroupTypeUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/ConfigurationServiceItemList.aspx?co=y", false)]
		//[Row("DataAdmin/ConfigurationServiceItemUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/ConfigurationServiceLabelList.aspx?co=y", false)]
		//[Row("DataAdmin/ConfigurationServiceLabelUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/DataAdminDefault.aspx?co=y", false)]
		//[Row("DataAdmin/EntityTypeList.aspx?co=y", false)]
		//[Row("DataAdmin/EntityTypeUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/EntityTypeUpdate.aspx?co=y&id=1", false)]
		//[Row("DataAdmin/NoteList.aspx?co=y", false)]
		//[Row("DataAdmin/NoteUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/NoteUpdate.aspx?co=y&id=1", false)]
		//[Row("DataAdmin/NoteTypeList.aspx?co=y", false)]
		//[Row("DataAdmin/NoteTypeUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/ProxyURLDomainList.aspx?co=y", false)]
		//[Row("DataAdmin/ProxyURLDomainUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/ProxyURLGroupTypeList.aspx?co=y", false)]
		//[Row("DataAdmin/ProxyURLGroupTypeUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/ProxyURLTypeList.aspx?co=y", false)]
		//[Row("DataAdmin/ProxyURLTypeUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/QueryParameterList.aspx?co=y", false)]
		//[Row("DataAdmin/QueryParameterUpdate.aspx?co=y", false)]
		//[Row("DataAdmin/QueryParameterValueList.aspx?co=y", false)]
		//[Row("DataAdmin/QueryParameterValueUpdate.aspx?co=y", false)]
		////[Row("MyInfo/MyInfoDefault.aspx?co=y", false)]
		////[Row("MyInfo/PersonDetailForMyInfo.aspx?co=y", false)]
		//[Row("Redirect/ProxyURLCopy.aspx?co=y", false)]
		//[Row("Redirect/ProxyURLEditUpdate.aspx?co=y", false)]
		//[Row("Redirect/ProxyURLList.aspx?co=y", false)]
		//[Row("Redirect/ProxyURLMultiEditUpdate.aspx?co=y", false)]
		//[Row("Redirect/ProxyURLMultiReplaceUpdate.aspx?co=y", false)]
		//[Row("Redirect/ProxyURLUpdate.aspx?co=y", false)]
		//[Row("Redirect/RedirectDefault.aspx?co=y", false)]
		////[Row("Registration/Register.aspx?co=y", false)]
		//[Row("SystemAdmin/DataImport.aspx?co=y", false)]
		//[Row("SystemAdmin/DevScaffold.aspx?co=y", false)]
		//[Row("SystemAdmin/DevTestPage.aspx?co=y", false)]
		//[Row("SystemAdmin/LogDetail.aspx?co=y", false)]
		//[Row("SystemAdmin/LogList.aspx?co=y", false)]
		//[Row("SystemAdmin/SystemAdminDashboard.aspx?co=y", false)]
		//[Row("SystemAdmin/SystemAdminDefault.aspx?co=y", false)]
		//[Row("SystemAdmin/SystemLog.aspx?co=y", false)]
		//[Row("SystemAdmin/ViewLog.aspx?co=y", false)]
		//[Row("UserAdmin/PersonList.aspx?co=y", false)]
		//[Row("UserAdmin/PersonDetail.aspx?co=y", false)]
		//[Row("UserAdmin/PersonUpdate.aspx?co=y", false)]
		//[Row("UserAdmin/UserAdminDefault.aspx?co=y", false)]
		//[Disabled("Disabled for now since these are mostly redundant to the WebPageSmokeTest tests, and would increase build times without adding much value.")]
		//[Ignore("Disabled for now since these are mostly redundant to the WebPageSmokeTest tests, and would increase build times without adding much value.")]
		//[Pending("Disabled for now since these are mostly redundant to the WebPageSmokeTest tests, and would increase build times without adding much value.")]
		[Explicit("Disabled for now since these are mostly redundant to the WebPageSmokeTest tests, and would increase build times without adding much value.")]
		public void WebPageSmokeTest_ContentOnlyDisplayMode(string pageUrl, bool assertAtHpPortalVisible)
		{
			//Assert.TerminateSilently(TestOutcome.Skipped, "Disabled for now since these are mostly redundant to the WebPageSmokeTest tests, and would increase build times without adding much value.");
			Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri(pageUrl);
			ElementsCPSWatinTest.AssertElementsCPSPageOpensWithoutServerError(this.SharedIEInstance, pageUri, assertAtHpPortalVisible);
		}

		/// <summary>
		/// Tests that the app's "popup" pages can be accessed without a server error.
		/// </summary>
		/// <param name="pageUrl">The app-relative URL of the page to test.</param>
        [Test]
		[Row("DataAdmin/NoteSummariesOverlay.aspx")]
		[Row("DataAdmin/NoteSummaryOverlay.aspx?id=0")]
		[Row("DataAdmin/NoteSummaryOverlay.aspx?id=1")]
		[Row("UserAdmin/PersonSummaryOverlay.aspx")]
		[Row("UserAdmin/PersonSummaryOverlay.aspx?id=0")]
		[Row("UserAdmin/PersonSummaryOverlay.aspx?id=1")]
		public void OverlayPageSmokeTest(string pageUrl)
		{
			Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri(pageUrl);
			ElementsCPSWatinTest.AssertElementsCPSPageOpensWithoutServerError(this.SharedIEInstance, pageUri, false);
		}

		/// <summary>
		/// Tests that the app's "special" pages can be accessed without a server error.
		/// </summary>
		/// <param name="pageUrl">The app-relative URL of the page to test.</param>
        [Test]
		//[Row("ApplicationError.aspx")]
		[Row("AccessForbidden.html")]
		public void SpecialPageSmokeTest(string pageUrl)
		{
			Uri pageUri = ElementsCPSWatinUtility.CreateElementsCPSUri(pageUrl);
			ElementsCPSWatinTest.AssertElementsCPSPageOpensWithoutServerError(this.SharedIEInstance, pageUri, false);
		}

		#endregion

	}
}
