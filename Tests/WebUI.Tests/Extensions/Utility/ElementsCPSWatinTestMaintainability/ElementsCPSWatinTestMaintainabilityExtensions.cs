using HP.ElementsCPS.Apps.WebUI.Tests.Utility;
using HP.HPFx.Extensions.Watin.Utility.WatinExpressiveness;
using HP.HPFx.Utility;
using WatiN.Core;


namespace HP.ElementsCPS.Apps.WebUI.Tests.Extensions.Utility.ElementsCPSWatinTestMaintainability
{
	/// <summary>
	/// This class contains extension methods that can be used within WatiN test methods to make the test suite's code more maintainable.
	/// </summary>
	/// <remarks>
	/// The primary purpose of this class is to provide extension and utility methods which can be used to make the application's WatiN test suite more maintainable by:
	/// 1. making it easier to implement tests using a higher proportion of declarative coding style;
	/// 2. decreasing the amount of unnecessary code duplication in test code;
	/// 3. making the test suite's code more readable and understandable;
	/// 4. enabling a certain degree of simulated polymorphism (by using extension methods instead of inheritance) without the drawbacks of true inheritance.
	/// </remarks>
	public static class ElementsCPSWatinTestMaintainabilityExtensions
	{

		#region IE.Find... methods

		public static Button FindCancelButton(this IE ie)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			return ie.Button(Find.ByName(Constants.ControlName_btnCancel));
		}

		public static Button FindCreateButton(this IE ie)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			return ie.Button(Find.ByName(Constants.ControlName_btnCreate));
		}		

		public static Button FindFilterButton(this IE ie)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			return ie.Button(Find.ByName(Constants.ControlName_btnFilter));
		}

		public static Button FindSaveButton(this IE ie)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			return ie.Button(Find.ByName(Constants.ControlName_btnSave));
		}

		public static Button FindSubmitButton(this IE ie)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			return ie.Button(Find.ByName(Constants.ControlNameDetailPrefix+"btnSubmit"));
		}

		public static Button FindSaveAsNewButton(this IE ie)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			return ie.Button(Find.ByName(Constants.ControlName_btnSaveAsNew));
		}

		public static Span FindFilterAreaExpansionToggle(this IE ie)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			return ie.Span(Find.ById(Constants.ClientIDPageContentAreaPrefix + "ucList_lblFilterAreaHeaderPrompt"));
		}

		public static Button FindButtonByText(this IE ie, string buttonText)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(buttonText, "buttonText");
			return ie.Button(Find.ByValue(buttonText));
		}


		#region Find...ByControlName methods

		public static SelectList FindSelectListByControlName(this IE ie, string controlClientName)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(controlClientName, "controlClientName");
			return ie.SelectList(Find.ByName(controlClientName));
		}

		public static TextField FindTextFieldByControlName(this IE ie, string controlClientName)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(controlClientName, "controlClientName");
			return ie.TextField(Find.ByName(controlClientName));
		}

		public static Link FindLinkByControlId(this IE ie, string controlClientId)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			ExceptionUtility.ArgumentNullEx_ThrowIfNullOrEmpty(controlClientId, "controlClientId");
			return ie.Link(Find.ById(controlClientId));
		}

		#endregion

		#region Find...Page...ByControlName methods

		public static SelectList FindDetailPageSelectListByControlName(this IE ie, string controlName)
		{
			return FindSelectListByControlName(ie, Constants.ControlNameDetailPrefix + controlName);
		}

		public static TextField FindDetailPageTextFieldByControlName(this IE ie, string controlName)
		{
			return FindTextFieldByControlName(ie, Constants.ControlNameDetailPrefix + controlName);
		}

		public static SelectList FindListPageSelectListByControlName(this IE ie, string controlName)
		{
			return FindSelectListByControlName(ie, Constants.ControlNameListPrefix + controlName);
		}

		public static TextField FindListPageTextFieldByControlName(this IE ie, string controlName)
		{
			return FindTextFieldByControlName(ie, Constants.ControlNameListPrefix + controlName);
		}

		#endregion

		#region Find...Link...ByControlId methods 

		public static Link FindDetailPageLinkByControlId(this IE ie, string controlId)
		{
			return FindLinkByControlId(ie, Constants.ClientIdDetailPanelPrefix + controlId);
		}

		public static Link FindListPageLinkByControlId(this IE ie, string controlId)
		{
			return FindLinkByControlId(ie, Constants.ControlIdListPrefix + controlId);
		}		

		#endregion

		#endregion

		#region IE.Do... methods

		public static void DoClickFilterAreaExpansionToggle(this IE ie)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(ie, "ie");
			ie.FindFilterAreaExpansionToggle().Click();
		}

		#endregion

	}
}