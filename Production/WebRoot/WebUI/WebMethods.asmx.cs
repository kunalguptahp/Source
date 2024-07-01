using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Compilation;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using HP.ElementsCPS.Apps.WebUI.Pages;
using HP.ElementsCPS.Apps.WebUI.UserControls;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;

namespace HP.ElementsCPS.Apps.WebUI
{
	/// <summary>
	/// This web service contains various WebMethods used by the ElementsCPS application.
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class WebMethods : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		#region AJAX AutoCompleteExtender WebMethods

		/// <summary>
		/// Used by an AJAX AutoCompleteExtender to provide a list of suggestions for existing Person names.
		/// </summary>
		/// <param name="prefixText">The user input to construct the completion list from.</param>
		/// <param name="count">The maximum number of items to return.</param>
		/// <returns>A list of suggested "autocomplete" items for the specified input/prefix.</returns>
		[WebMethod]
		[System.Web.Script.Services.ScriptMethod]
		public string[] GetPersonNameCompletionList(string prefixText, int count)
		{
			if (count == 0)
			{
				//return new string[0];
				count = 10;
			}

			if (string.IsNullOrEmpty(prefixText.TrimUnlessNull()))
			{
				//short-circuit the lookup if the string is null, empty, or pure whitespace
				return new string[0];
			}

			List<string> items = PersonController.GetDistinctColumnValues(Person.Columns.Name, prefixText, (int)RowStatus.RowStatusId.Active, 0, count);
			return ConvertToAutoCompleteItemsArray(items);
		}


		/// <summary>
		/// Used by an AJAX AutoCompleteExtender to provide a list of suggestions for existing Tag names.
		/// </summary>
		/// <param name="prefixText">The user input to construct the completion list from.</param>
		/// <param name="count">The maximum number of items to return.</param>
		/// <returns>A list of suggested "autocomplete" items for the specified input/prefix.</returns>
		[WebMethod]
		[System.Web.Script.Services.ScriptMethod]
		public string[] GetTagNameCompletionList(string prefixText, int count)
		{
			if (count == 0)
			{
				//return new string[0];
				count = 10;
			}

			if (string.IsNullOrEmpty(prefixText.TrimUnlessNull()))
			{
				//short-circuit the lookup if the string is null, empty, or pure whitespace
				return new string[0];
			}

			List<string> items = TagController.GetDistinctColumnValues(Tag.Columns.Name, prefixText, (int)RowStatus.RowStatusId.Active, 0, count);
			return items.ToArray();
		}

		/// <summary>
		/// Used by an AJAX AutoCompleteExtender to provide a list of suggestions for existing Tag names.
		/// </summary>
		/// <param name="prefixText">The user input to construct the completion list from.</param>
		/// <param name="count">The maximum number of items to return.</param>
		/// <returns>A list of suggested "autocomplete" items for the specified input/prefix.</returns>
		[WebMethod]
		public string[] GetTagFilterCompletionList(string prefixText, int count)
		{
			if (count == 0)
			{
				//return new string[0];
				count = 10;
			}

			if (string.IsNullOrEmpty(prefixText.TrimUnlessNull()))
			{
				//short-circuit the lookup if the string is null, empty, or pure whitespace
				return new string[0];
			}

			if (prefixText.StartsWith("-") || prefixText.StartsWith("+"))
			{
				string prefixTextPrefix = prefixText.Substring(0, 1);
				prefixText = prefixText.Substring(prefixTextPrefix.Length);
				List<string> items = TagController.GetDistinctColumnValues(Tag.Columns.Name, prefixText, (int)RowStatus.RowStatusId.Active, 0, count);
				List<string> prefixedItems = new List<string>(items.Count);
				items.ForEach(unprefixedItem => prefixedItems.Add(prefixTextPrefix + unprefixedItem));
				return prefixedItems.ToArray();
			}
			else
			{
				List<string> items = TagController.GetDistinctColumnValues(Tag.Columns.Name, prefixText, (int)RowStatus.RowStatusId.Active, 0, count);
				return items.ToArray();

				//SqlQuery query = TagController.NewDistinctColumnValuesQuery(Tag.Columns.Name, prefixText, (int)RowStatus.RowStatusId.Active);
				//SubSonicUtility.SetPaging(query, count, 0);
				//DataSet dataSet = query.ExecuteDataSet();
				//return SqlUtility.CreateListOfColumnValues<string>(dataSet.Tables[0], 1).ToArray();
			}
		}
	
		#endregion

		#region AJAX CascadingDropDown WebMethods

		#region GetCascadingDropDownItems... Methods

		[WebMethod]
		public CascadingDropDownNameValue[] GetCascadingDropDownItems(string knownCategoryValues, string category, string contextKey)
		{
			return GetCascadingDropDownItemsHelper(knownCategoryValues, category, contextKey).ToArray();
		}

		[WebMethod]
		public CascadingDropDownNameValue[] GetCascadingDropDownItemsWithAllItem(string knownCategoryValues, string category, string contextKey)
		{
			List<CascadingDropDownNameValue> values = GetCascadingDropDownItemsHelper(knownCategoryValues, category, contextKey);
			values.Insert(0, new CascadingDropDownNameValue(Global.GetAllListText(), ""));
			return values.ToArray();
		}

		[WebMethod]
		public CascadingDropDownNameValue[] GetCascadingDropDownItemsWithNoneItem(string knownCategoryValues, string category, string contextKey)
		{
			List<CascadingDropDownNameValue> values = GetCascadingDropDownItemsHelper(knownCategoryValues, category, contextKey);
			values.Insert(0, new CascadingDropDownNameValue(Global.GetNoneListText(), ""));
			return values.ToArray();
		}

		[WebMethod]
		public CascadingDropDownNameValue[] GetCascadingDropDownItemsWithSelectItem(string knownCategoryValues, string category, string contextKey)
		{
			List<CascadingDropDownNameValue> values = GetCascadingDropDownItemsHelper(knownCategoryValues, category, contextKey);
			values.Insert(0, new CascadingDropDownNameValue(Global.GetSelectListText(), ""));
			return values.ToArray();
		}

		#endregion

		#region Helper Methods

		private static List<CascadingDropDownNameValue> GetCascadingDropDownItemsHelper(string knownCategoryValues, string category, string contextKey)
		{
			StringDictionary currentlySelectedValues = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);

			List<CascadingDropDownNameValue> newDropDownItemNameValues = null;

			//parse the category and contextKey
			//NOTE: I append the extra semicolons so the array will always have a Length of 5+ (so that I don't have to check the Length since some tokens are optional)
			string[] categoryArgs = (category + ";;;;").Split(";".ToCharArray(), StringSplitOptions.None);
			string tableName = categoryArgs[0].TrimToNull();
			string valueColumnName = categoryArgs[1].TrimToNull() ?? "Id";
			string textColumnName = categoryArgs[2].TrimToNull() ?? "Name";
			string filterColumnName = contextKey.TrimToNull();

			//use the parsed values to build the results data
			newDropDownItemNameValues = GetCascadingDropDownItemsHelper_StandardSingleColumnEqualityFiltering(tableName, valueColumnName, textColumnName, filterColumnName, currentlySelectedValues);

			newDropDownItemNameValues = newDropDownItemNameValues ?? (new List<CascadingDropDownNameValue>()); //handle null

			////If we want to sort explicitly, do it here
			//newDropDownItemNameValues.Sort(new CascadingDropDownNameValueComparer());

			return newDropDownItemNameValues;
		}

		private static List<CascadingDropDownNameValue> GetCascadingDropDownItemsHelper_StandardSingleColumnEqualityFiltering(string fromTable, string valueColumnName, string textColumnName, string filterColumnName, StringDictionary currentlySelectedValues)
		{
			int? currentlySelectedValue = ParseKnownCategoryValuesAsInt32(currentlySelectedValues);
			string whereClauseSql = null;
			if (!string.IsNullOrEmpty(filterColumnName))
			{
				whereClauseSql = currentlySelectedValue == null ? string.Format("({0} IS NULL)", filterColumnName) : string.Format("({0} = {1})", filterColumnName, currentlySelectedValue);
			}
			DataTable dataSource = ElementsCPSSqlUtility.GetListControlDataSource_Id_Name(fromTable, whereClauseSql);

			List<CascadingDropDownNameValue> newDropDownItemNameValues = new List<CascadingDropDownNameValue>();
			foreach (DataRow row in dataSource.Rows)
			{
				newDropDownItemNameValues.Add(new CascadingDropDownNameValue(row[textColumnName].ToString(), row[valueColumnName].ToString()));
			}
			return newDropDownItemNameValues;
		}

		private static int? ParseKnownCategoryValuesAsInt32(StringDictionary knownCategoryValues)
		{
			const string keyName = "undefined"; //NOTE: JW: I don't know why this has to be hardcoded, but it apparently does
			return (!knownCategoryValues.ContainsKey(keyName)) ? null : knownCategoryValues[keyName].TryParseInt32();
		}

		#endregion

		#endregion

		#region AJAX DynamicContent WebMethods

		[WebMethod]
		public string GetDynamicContentEchoContextKey(string contextKey)
		{
			return contextKey;
		}

		#region ...SummaryOverlay DynamicContent Methods

		[WebMethod]
		public string GetDynamicContentPersonSummary(string contextKey)
		{
			try
			{
				int? personId = contextKey.TryParseInt32();

				//Construct the appropriate URL to be displayed within the returned IFrame
				string pageUrl = Global.GetPersonSummaryOverlayPageUri(personId);

				//Construct the IFrame tag that will be returned
				return string.Format(GetDynamicContentIframeHtml(pageUrl));
			}
			catch (Exception ex)
			{
				LogManager.Current.Log(Severity.Warn, this, "", ex);
				return ""; //return ex.ToString();
			}
		}

		#endregion

		#region Person-related DynamicContent Methods

		#endregion

		#region Note-related DynamicContent Methods

		//[WebMethod]
		private string GetDynamicContentNoteSummariesList_Generic(int entityTypeId, int? entityId)
		{
			try
			{
				//Construct the appropriate URL to be displayed within the returned IFrame
				NoteQuerySpecification qs = null;
				if (entityId != null)
				{
					qs = new NoteQuerySpecification();
					//qs.Paging.PageSize = int.MaxValue;
					qs.EntityTypeId = entityTypeId;
					qs.EntityId = entityId;
				}

				string pageUrl = Global.GetNoteSummariesOverlayPageUri(qs);

				//Construct the IFrame tag that will be returned
				return string.Format(GetDynamicContentIframeHtml(pageUrl));
			}
			catch (Exception ex)
			{
				LogManager.Current.Log(Severity.Warn, this, "", ex);
				return ""; //return ex.ToString();
			}
		}

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_DDLChangeLog(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(13, contextKey.TryParseInt32()); }

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_EntityType(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(14, contextKey.TryParseInt32()); }

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_Log(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(26, contextKey.TryParseInt32()); }

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_Note(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(27, contextKey.TryParseInt32()); }

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_NoteType(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(28, contextKey.TryParseInt32()); }

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_Person(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(30, contextKey.TryParseInt32()); }

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_PersonRole(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(31, contextKey.TryParseInt32()); }

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_Role(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(56, contextKey.TryParseInt32()); }

		[WebMethod]
		public string GetDynamicContentNoteSummariesList_RowStatus(string contextKey) { return this.GetDynamicContentNoteSummariesList_Generic(57, contextKey.TryParseInt32()); }

		#endregion

		#endregion

		#region Utility Methods

		/// <summary>
		/// Converts a set of strings into an array containing one "AutoCompleteItem" for each string in the original set.
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		public static string[] ConvertToAutoCompleteItemsArray(List<string> items)
		{
			//TODO: Review Needed: Move this to HPFx?
#warning Review Needed: Move this to HPFx?

			if (items != null)
			{
				string[] array = items.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = AutoCompleteExtender.CreateAutoCompleteItem(array[i], array[i]);
				}
				return array;
			}
			return new string[0];
		}

		private static string GetDynamicContentIframeHtml(string pageUrl)
		{
			return string.Format("<iframe class='AjaxDynamicContentIframe' src='{0}' />", HttpUtility.HtmlAttributeEncode(pageUrl));
		}

		#endregion

		#region Label-related DynamicContent Methods

		[WebMethod]
		public string GetDynamicContentLabelHelp(string contextKey)
		{
			try
			{
				string helpString = "Please contact the CPS Configuration Service Administrator for help.";

				int? labelId = contextKey.TryParseInt32();
				if (labelId != null)
				{
					ConfigurationServiceLabel label = ConfigurationServiceLabel.FetchByID(labelId);
					if (!String.IsNullOrEmpty(label.Help))
						helpString = label.Help;
				}
				//Construct the IFrame tag that will be returned
				return string.Format(helpString);
			}
			catch (Exception ex)
			{
				LogManager.Current.Log(Severity.Warn, this, "", ex);
				return ""; //return ex.ToString();
			}
		}

		#endregion

	}
}
