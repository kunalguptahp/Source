
namespace HP.ElementsCPS.Apps.WebUI.Tests.Utility
{
	/// <summary>
	/// Contains various constants and other values that are referenced by many fixtures and tests.
	/// </summary>
	public static class Constants
	{
		#region ID-related Members

		public const string ControlNamePageContentAreaPrefix = "ctl00$ctl00$ctl00$PageContentArea$PageContentArea$PageContentArea$";
		public const string ControlNamePrefix2 = ControlNamePageContentAreaPrefix + "ajaxProjectDetailTabs$ajaxProjectDetailTabBasicInfo$";
		public const string ControlNameListPrefix = ControlNamePageContentAreaPrefix + "ucList$";
		public const string ControlNameDetailPrefix = ControlNamePageContentAreaPrefix + "ucDetail$";

		//public const string ControlName_btnClose = Constants.ControlNameListPrefix + "btnClose";
		public const string ControlName_btnCreate = Constants.ControlNameListPrefix + "btnCreate";
		public const string ControlName_btnFilter = Constants.ControlNameListPrefix + "btnFilter";
		public const string ControlName_btnCancel = Constants.ControlNameDetailPrefix + "btnCancel";
		public const string ControlName_btnSave = Constants.ControlNameDetailPrefix + "btnSave";
		public const string ControlName_btnSaveAsNew = Constants.ControlNameDetailPrefix + "btnSaveAsNew";
		public const string ControlName_gvList = Constants.ControlNameListPrefix + "gvList";
		public const string ControlName_gvList_Row1 = Constants.ControlName_gvList + "$ctl03";
		public const string ControlName_gvList_Row1_EditButton = Constants.ControlName_gvList_Row1 + "$ctl02";
		public const string ControlName_gvList_Row1_EditButton_ListProject = Constants.ControlName_gvList_Row1 + "$ctl00";
		public const string ControlName_gvList_Header_SelectAllCheckBox = Constants.ControlName_gvList + "$HeaderButton";
		public const string ControlName_gvList_Row2 = Constants.ControlName_gvList + "$ctl05";
		public const string ControlName_gvList_Row2_EditButton = Constants.ControlName_gvList_Row1 + "$ctl02";

		public const string ControlIdListPrefix = ClientIDPageContentAreaPrefix + "ucList_";
		public const string ClientIDPageContentAreaPrefix = "ctl00_ctl00_ctl00_PageContentArea_PageContentArea_PageContentArea_";
		public const string ControlIdDetailPrefix = ClientIDPageContentAreaPrefix + "ucDetail_";
		public const string ClientIdDetailPanelPrefix = ClientIDPageContentAreaPrefix + "ucDetail_";
		public const string ButtonText_EditDotDotDot = "Edit...";
		public const string ButtonText_Delete = "Delete";
		public const string ButtonText_Clear = "Clear";
		public const string PopupPanelIdListPrefix = "ctl00_ctl00_PageContentArea_PageContentArea_ucList_";
		public const string QueryParameterValueEditUpdatePanel = "ucQueryParameterValueEditUpdatePanel_gvList_";
		public const string MultiEditWithGvList = "ucQueryParameterValueMultiUpdate_gvList_";
		public const string MultiEdit = "ucQueryParameterValueMultiUpdate_";
		public const string MultiReplace="ucProxyURLDescriptionMultiReplaceUpdate_gvList_";
		public const string ControlIdDetailPrefix2 = "ctl00_ctl00_PageContentArea_PageContentArea_ucDetail_";
		public const string ControlIdListPrefix2 = "ctl00_ctl00_PageContentArea_PageContentArea_ucList_";

		#endregion
	}
}
