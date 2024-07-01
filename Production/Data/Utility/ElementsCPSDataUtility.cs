using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;
using Microsoft.VisualBasic.FileIO;

namespace HP.ElementsCPS.Data.Utility
{
	/// <summary>
	/// Contains misc. utility methods.
	/// </summary>
	public static class ElementsCPSDataUtility
	{

		#region App-wide Constants

		public const string UrlParam_DataSourceId = "id";
		//public const string UrlParam_QuerySpecification = "query";
		//public const string UrlParam_DefaultValuesSpecification = "defaults";

		#endregion

		#region Properties

		private static Uri _WebApplicationUri;

		public static Uri WebApplicationUri
		{
			get
			{
				if (_WebApplicationUri == null)
				{
					WebApplicationUri = GetWebApplicationUri();
				}
				return _WebApplicationUri;
			}
			private set
			{
				_WebApplicationUri = value;
			}
		}

		private static Uri GetWebApplicationUri()
		{
			string appRootUrl = ConfigurationManager.AppSettings["ElementsCPS.Data.WebAppRootUrl"]; // "http://localhost/ElementsCPS/WebUI/";
			if (string.IsNullOrEmpty(appRootUrl))
			{
				//TODO: Review Needed: Should probably throw an exception here
#warning Review Needed: Should probably throw an exception here

				appRootUrl = "http://localhost/ElementsCPS/WebUI/"; //use a default URL
			}
			return new Uri(appRootUrl);
		}

		#endregion

		#region WebUI URL Generation-related Utility Methods

		#region Helper Methods

		/// <summary>
		/// Returns an absolute Uri based on either an app-relative or absolute ElementsCPS URL.
		/// </summary>
		/// <param name="pageUrl">An ElementsCPS URL (either app-relative or absolute).</param>
		/// <returns>An absolute Uri.</returns>
		public static Uri CreateElementsCPSUri(string pageUrl)
		{
			Uri baseUri = WebApplicationUri;
			return new Uri(baseUri, pageUrl);
		}

		/// <summary>
		/// Generates a complete absolute Uri for an ElementsCPS "Record Detail" page.
		/// </summary>
		/// <param name="pageUrl">The URL (either absolute or the web-app-root-relative URL path) of the page (excluding the QueryString).</param>
		/// <param name="recordId">The ID of the specific record the page should display when the URL is accessed.</param>
		/// <returns></returns>
		private static string GenerateStandardRecordDetailPageUri(string pageUrl, int? recordId)
		{
			if (recordId == null)
			{
				return null;
			}

			pageUrl = CreateElementsCPSUri(pageUrl).AbsoluteUri; //Convert the relative URL to an absolute Uri

			List<string> queryParams = new List<string>(2);
			if (recordId != null)
			{
				queryParams.Add(string.Format("{0}={1}", UrlParam_DataSourceId, recordId.Value));
			}
			if (queryParams.Count > 0)
			{
				//append the queryParams to the URL
				string newUri = string.Format("{0}?{1}", pageUrl, string.Join("&", queryParams.ToArray()));
				//if ((!WebUtility.IsUriLengthValid(pageUrl)) && (defaultValuesSpecification != null))
				//{
				//   //NOTE: If the defaultValuesSpecification makes the URL too long, then ignore it
				//   return GenerateStandardRecordDetailPageUri(pageUrl, recordId, null);
				//}
				pageUrl = newUri;
			}
			return pageUrl;
		}

		#endregion

		#region Get...DetailPageUri Methods

		public static string GetNoteDetailPageUri(int? recordId)
		{
			return GenerateStandardRecordDetailPageUri("DataAdmin/NoteDetail.aspx", recordId);
		}

		public static string GetPersonDetailPageUri(int? recordId)
		{
			return GenerateStandardRecordDetailPageUri("UserAdmin/PersonDetail.aspx", recordId);
		}

		#endregion

		#endregion

		#region QuerySpecification-related Utility Methods

		/// <summary>
		/// Returns all of the "Key" strings that are actually in use (i.e. the Key has a Value specified).
		/// </summary>
		/// <param name="qs"></param>
		/// <returns></returns>
		internal static string[] GetQueryConditionKeys(IQuerySpecification qs)
		{
			//TODO: Review Needed: Review for potential migration into HPFx
#warning Review Needed: Review for potential migration into HPFx

			Dictionary<string, object>.KeyCollection keyCollection = qs.Conditions.Keys;
			return keyCollection.ToArray();
		}

		/// <summary>
		/// Utility helper method used by various Controller classes' "CreateQuery" methods.
		/// </summary>
		/// <param name="qs"></param>
		/// <param name="validQueryConditionKeys"></param>
		internal static void ValidateQuerySpecificationConditions(IQuerySpecification qs, IEnumerable<string> validQueryConditionKeys)
		{
			string[] allSpecifiedQueryConditionKeys = ElementsCPSDataUtility.GetQueryConditionKeys(qs);
			List<string> invalidSpecifiedQueryConditionKeys = allSpecifiedQueryConditionKeys.Except(validQueryConditionKeys).ToList();
			bool invalidConditionsSpecified = invalidSpecifiedQueryConditionKeys.Count > 0;
			if (invalidConditionsSpecified)
			{
				throw new ArgumentException(string.Format("Invalid QuerySpecification condition(s). The following query conditions are unsupported by this method:\n{0}.",
					string.Join(", ", invalidSpecifiedQueryConditionKeys.ToArray())));
			}
		}

		#endregion

		#region Error Handling Utility Methods

		/// <summary>
		/// Analyzes an exception to determine whether it is "recognized" and has a corresponding user-friendly error message that can be displayed to users.
		/// </summary>
		/// <param name="ex"></param>
		/// <param name="excludeGenericMessages"></param>
		/// <returns></returns>
		public static string GetUserFriendlyExceptionMessage(Exception ex, bool excludeGenericMessages)
		{
			if (ex == null)
			{
				return null;
			}

			//if the ElementsCPSDataUtility.GetUserFriendlyExceptionMessage method first

			List<SqlException> sqlExceptions = ExceptionUtility.GetExceptionsOfType<SqlException>(ex).ToList();
			if (sqlExceptions.Count > 0)
			{
				bool isDbConstraintViolation_DuplicateKey =
					(sqlExceptions.Where(sqlEx => sqlEx.Number == 2627).Count() > 0);
				bool isDbConstraintViolation_UniqueIndex =
					(sqlExceptions.Where(sqlEx => sqlEx.Number == 2601).Count() > 0);
				bool isDbConstraintViolation_Reference =
					(sqlExceptions.Where(sqlEx => sqlEx.Number == 547).Where(sqlEx => sqlEx.Message.Contains(" statement conflicted with the REFERENCE constraint ")).Count() > 0);
				//bool isDbConstraintViolation_Reference_ByDeleteStatement =
				//   (sqlExceptions.Where(sqlEx => sqlEx.Number == 547).Where(sqlEx => sqlEx.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint ")).Count() > 0);
				bool isDbConstraintViolation_ForeignKey =
					(sqlExceptions.Where(sqlEx => sqlEx.Number == 547).Where(sqlEx => sqlEx.Message.Contains(" statement conflicted with the FOREIGN KEY constraint ")).Count() > 0);
				bool isDbConstraintViolation_Check =
					(sqlExceptions.Where(sqlEx => sqlEx.Number == 547).Where(sqlEx => sqlEx.Message.Contains(" statement conflicted with the CHECK constraint ")).Count() > 0);
				if (isDbConstraintViolation_DuplicateKey)
				{
					return "The attempted action would violate data integrity rules (invalid data duplication).";
				}
				else if (isDbConstraintViolation_UniqueIndex)
				{
					return "The attempted action would violate data integrity rules (invalid data duplication).";
				}
				//else if (isDbConstraintViolation_Reference_ByDeleteStatement)
				//{
				//   return "The item is still referenced by other data.";
				//}
				else if (isDbConstraintViolation_Reference)
				{
					return "The attempted action would violate data integrity rules (referential integrity violation).";
				}
				else if (isDbConstraintViolation_ForeignKey)
				{
					return "The attempted action would violate data integrity rules (referential integrity violation).";
				}
				else if (isDbConstraintViolation_Check)
				{
					return "The attempted action would violate data integrity rules (Check constraint violation).";
				}
				else if (!excludeGenericMessages)
				{
					return "The attempted action caused a database error.";
				}
			}

			List<SqlTypeException> sqlTypeExceptions = ExceptionUtility.GetExceptionsOfType<SqlTypeException>(ex).ToList();
			if (sqlTypeExceptions.Count > 0)
			{
				bool isDbDataTypeOverflow_DateTimeOverflow =
					(sqlTypeExceptions.Where(sqlEx => sqlEx.Message.StartsWith("SqlDateTime overflow.")).Count() > 0);
				if (isDbDataTypeOverflow_DateTimeOverflow)
				{
					return "The attempted action was incompatible with the database schema (data type overflow: invalid date or time value).";
				}
				else if (!excludeGenericMessages)
				{
					return "The attempted action was incompatible with the database schema (data type error).";
				}
			}

			//The exception was unrecognized. Therefore, no user friendly message is available.
			return null;
		}

		#endregion

		#region Data Import-related Methods

		#region Data Import Parsing and Type Conversion Methods

		#region "Special Value" Methods

		public static bool IsImportableValue(object importDataValue)
		{
			return !((importDataValue == null) || string.IsNullOrEmpty(importDataValue.ToString()));
		}

		public static bool IsImportAsNullValue(object importDataValue)
		{
			if (importDataValue == null)
			{
				return false;
			}
			return ((importDataValue == DBNull.Value) || (string.Equals(importDataValue.ToString(), "NULL", StringComparison.CurrentCulture)));
		}

		public static bool IsImportAsEmptyValue(object importDataValue)
		{
			if (importDataValue == null)
			{
				return false;
			}
			return string.Equals(importDataValue.ToString(), "EMPTY", StringComparison.CurrentCulture);
		}

		#endregion

		#region ParseImportValueAs... Methods

		public static string ParseImportValueAsString(object importDataValue)
		{
			if (!ElementsCPSDataUtility.IsImportableValue(importDataValue))
			{
				throw new ArgumentException("The specified value is not importable. Only importable values should be passed as arguments to this method.", "importDataValue");
			}
			if (ElementsCPSDataUtility.IsImportAsNullValue(importDataValue))
			{
				return null;
			}
			if (ElementsCPSDataUtility.IsImportAsEmptyValue(importDataValue))
			{
				return string.Empty;
			}
			return importDataValue.ToString();
		}

		public static bool? ParseImportValueAsNullableBoolean(object importDataValue)
		{
			if (!ElementsCPSDataUtility.IsImportableValue(importDataValue))
			{
				throw new ArgumentException("The specified value is not importable. Only importable values should be passed as arguments to this method.", "importDataValue");
			}
			if (ElementsCPSDataUtility.IsImportAsNullValue(importDataValue))
			{
				return null;
			}
			bool? value = importDataValue.ToString().TryParseBoolean();
			if (value == null)
			{
				//NOTE: if value is null, then TryParse... failed. Since importing a NULL value requires the use of an explicit string constant (handled above), any other failure to parse the value is a problem
				throw new FormatException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired type: '{0}'.", importDataValue));
			}
			return value;
		}

		public static DateTime? ParseImportValueAsNullableDateTime(object importDataValue)
		{
			if (!ElementsCPSDataUtility.IsImportableValue(importDataValue))
			{
				throw new ArgumentException("The specified value is not importable. Only importable values should be passed as arguments to this method.", "importDataValue");
			}
			if (ElementsCPSDataUtility.IsImportAsNullValue(importDataValue))
			{
				return null;
			}
			DateTime? value = importDataValue.ToString().TryParseDateTime();
			if (value == null)
			{
				//NOTE: if value is null, then TryParse... failed. Since importing a NULL value requires the use of an explicit string constant (handled above), any other failure to parse the value is a problem
				throw new FormatException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired type: '{0}'.", importDataValue));
			}
			return value;
		}

		public static int? ParseImportValueAsNullableInt32(object importDataValue)
		{
			if (!ElementsCPSDataUtility.IsImportableValue(importDataValue))
			{
				throw new ArgumentException("The specified value is not importable. Only importable values should be passed as arguments to this method.", "importDataValue");
			}
			if (ElementsCPSDataUtility.IsImportAsNullValue(importDataValue))
			{
				return null;
			}
			int? value = importDataValue.ToString().TryParseInt32();
			if (value == null)
			{
				//NOTE: if value is null, then TryParse... failed. Since importing a NULL value requires the use of an explicit string constant (handled above), any other failure to parse the value is a problem
				throw new FormatException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired type: '{0}'.", importDataValue));
			}
			return value;
		}

		public static long? ParseImportValueAsNullableInt64(object importDataValue)
		{
			if (!ElementsCPSDataUtility.IsImportableValue(importDataValue))
			{
				throw new ArgumentException("The specified value is not importable. Only importable values should be passed as arguments to this method.", "importDataValue");
			}
			if (ElementsCPSDataUtility.IsImportAsNullValue(importDataValue))
			{
				return null;
			}
			long? value = importDataValue.ToString().TryParseInt64();
			if (value == null)
			{
				//NOTE: if value is null, then TryParse... failed. Since importing a NULL value requires the use of an explicit string constant (handled above), any other failure to parse the value is a problem
				throw new FormatException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired type: '{0}'.", importDataValue));
			}
			return value;
		}

		public static decimal? ParseImportValueAsNullableDecimal(object importDataValue)
		{
			if (!ElementsCPSDataUtility.IsImportableValue(importDataValue))
			{
				throw new ArgumentException("The specified value is not importable. Only importable values should be passed as arguments to this method.", "importDataValue");
			}
			if (ElementsCPSDataUtility.IsImportAsNullValue(importDataValue))
			{
				return null;
			}
			decimal? value = importDataValue.ToString().TryParseDecimal();
			if (value == null)
			{
				//NOTE: if value is null, then TryParse... failed. Since importing a NULL value requires the use of an explicit string constant (handled above), any other failure to parse the value is a problem
				throw new FormatException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired type: '{0}'.", importDataValue));
			}
			return value;
		}

		public static double? ParseImportValueAsNullableDouble(object importDataValue)
		{
			if (!ElementsCPSDataUtility.IsImportableValue(importDataValue))
			{
				throw new ArgumentException("The specified value is not importable. Only importable values should be passed as arguments to this method.", "importDataValue");
			}
			if (ElementsCPSDataUtility.IsImportAsNullValue(importDataValue))
			{
				return null;
			}
			double? value = importDataValue.ToString().TryParseDouble();
			if (value == null)
			{
				//NOTE: if value is null, then TryParse... failed. Since importing a NULL value requires the use of an explicit string constant (handled above), any other failure to parse the value is a problem
				throw new FormatException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired type: '{0}'.", importDataValue));
			}
			return value;
		}

		public static bool ParseImportValueAsBoolean(object importDataValue)
		{
			bool? value = ElementsCPSDataUtility.ParseImportValueAsNullableBoolean(importDataValue);
			if (value == null)
			{
				throw new ArgumentException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired non-nullable type: '{0}'.", importDataValue));
			}
			return value.Value;
		}

		public static DateTime ParseImportValueAsDateTime(object importDataValue)
		{
			DateTime? value = ElementsCPSDataUtility.ParseImportValueAsNullableDateTime(importDataValue);
			if (value == null)
			{
				throw new ArgumentException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired non-nullable type: '{0}'.", importDataValue));
			}
			return value.Value;
		}

		public static int ParseImportValueAsInt32(object importDataValue)
		{
			int? value = ElementsCPSDataUtility.ParseImportValueAsNullableInt32(importDataValue);
			if (value == null)
			{
				throw new ArgumentException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired non-nullable type: '{0}'.", importDataValue));
			}
			return value.Value;
		}

		public static long ParseImportValueAsInt64(object importDataValue)
		{
			long? value = ElementsCPSDataUtility.ParseImportValueAsNullableInt64(importDataValue);
			if (value == null)
			{
				throw new ArgumentException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired non-nullable type: '{0}'.", importDataValue));
			}
			return value.Value;
		}

		public static decimal ParseImportValueAsDecimal(object importDataValue)
		{
			decimal? value = ElementsCPSDataUtility.ParseImportValueAsNullableDecimal(importDataValue);
			if (value == null)
			{
				throw new ArgumentException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired non-nullable type: '{0}'.", importDataValue));
			}
			return value.Value;
		}

		public static double ParseImportValueAsDouble(object importDataValue)
		{
			double? value = ElementsCPSDataUtility.ParseImportValueAsNullableDouble(importDataValue);
			if (value == null)
			{
				throw new ArgumentException(string.Format("Data type conversion failed. The specified value cannot be converted to the desired non-nullable type: '{0}'.", importDataValue));
			}
			return value.Value;
		}

		#endregion

		#endregion

		#region ConvertTextDataToTypedData Method and Helper Methods

		/// <summary>
		/// Creates a new <see cref="DataTable"/> with strongly-typed <see cref="DataColumn"/>s 
		/// based upon a "weakly typed" <see cref="DataTable"/> (i.e. one that contains only "string"-formatted column) 
		/// and upon a template <paramref name="rowDataObjectType"/> <see cref="Type"/> that has strongly-typed properties 
		/// which correspond to the names of the columns in the weakly typed "source" <see cref="DataTable"/>.
		/// </summary>
		/// <param name="dataTable"></param>
		/// <param name="rowDataObjectType"></param>
		/// <returns></returns>
		public static DataTable ConvertTextDataToTypedData(DataTable dataTable, Type rowDataObjectType)
		{
			//TODO: Implement: Primary code path
#warning Not Implemented: Primary code path
			throw new NotImplementedException("The invoked code path is not yet implemented.");
		}

		public static DataTable ConvertTextDataToTypedData<TRecord, TRecordCollection>(DataTable originalDataTable) 
			where TRecord : SubSonic.RecordBase<TRecord>, SubSonicClient.IRecord, new()
			where TRecordCollection : SubSonic.AbstractList<TRecord, TRecordCollection>, SubSonicClient.IRecordCollection, new()
		{
			TRecordCollection recordCollection = new TRecordCollection();
			DataTable newDataTable = recordCollection.ToDataTable();
			return ConvertTextDataToTypedData(originalDataTable, newDataTable);
		}

		private static DataTable ConvertTextDataToTypedData(DataTable originalDataTable, DataTable newDataTable)
		{
			foreach (DataRow originalRow in originalDataTable.Rows)
			{
				object[] newRowValues = new object[newDataTable.Columns.Count];
				for (int newColumnIndex = 0; newColumnIndex < newDataTable.Columns.Count; newColumnIndex++)
				{
					DataColumn newColumn = newDataTable.Columns[newColumnIndex];
					DataColumn originalColumn = originalDataTable.Columns[newColumn.ColumnName];
					object originalValue = originalRow[originalColumn];
					object newValue = ConvertOriginalValueToTypedValue(originalValue, originalColumn, newColumn);
					newRowValues[newColumnIndex] = newValue;
				}
				//foreach (DataColumn newColumn in newDataTable.Columns)
				//{
				//   DataColumn originalColumn = originalDataTable.Columns[newColumn.ColumnName];
				//}
				DataRow newRow = newDataTable.Rows.Add(newRowValues);
			}

			return newDataTable;
		}

		private static object ConvertOriginalValueToTypedValue(object originalValue, DataColumn originalColumn, DataColumn newColumn)
		{
			//TODO: Implement: Primary code path
#warning Not Implemented: Primary code path
			throw new NotImplementedException("The invoked code path is not yet implemented.");
		}

//      public static DataTable ConvertTextDataToTypedData<TRecordController>(DataTable dataTable) 
//         where TRecordController : SubSonicClient.IRecordController, new()
//      {
//         TRecordController recordController = new TRecordController();
//         recordController.GetRecordSchema()
//         //TODO: Implement: Primary code path
//#warning Not Implemented: Primary code path
//         throw new NotImplementedException("The invoked code path is not yet implemented.");
//      }

		#endregion

		#region ParseDelimitedText Method and Helper Methods

		/// <summary>
		/// Creates a <see cref="DataSet"/> populated with data loaded from a data source containing formatted text (e.g. delimited or fixed-width columns).
		/// </summary>
		/// <param name="parser">A <see cref="TextFieldParser"/> that is properly configured to read a specific formatted data source (e.g. a stream, reader, or file).</param>
		/// <param name="hasHeaderRow">Indicates whether the data's first row/line is a header row containing the names of the data fields/columns.</param>
		/// <param name="includeHeaderRowInData">Indicates whether the data in the header row (if present) should be included as a <see cref="DataRow"/> in the returned <see cref="DataTable"/>. If <paramref name="hasHeaderRow"/> is <c>false</c>, this parameter is ignored.</param>
		/// <returns>A <see cref="DataSet"/> containing a single <see cref="DataTable"/> that is populated with the data parsed from the formatted text data source.</returns>
		public static DataSet ParseDelimitedText(TextFieldParser parser, bool hasHeaderRow, bool includeHeaderRowInData)
		{
			using (parser)
			{
				try
				{
					//create a DataSet with a single DataTable
					DataSet ds = new DataSet();
					DataTable dt = ds.Tables.Add();

					//read the first line/row of data
					String[] rowValues = parser.ReadFields();

					//use the first set of row values to configure the DataTable's DataColumns
					if (hasHeaderRow)
					{
						//create a DataColumn per row value, using the row values as the names of the columns
						foreach (String rowValue in rowValues)
						{
							string columnName = rowValue;
							dt.Columns.Add(columnName, Type.GetType("System.String"));
						}
						if (includeHeaderRowInData)
						{
							dt.Rows.Add(rowValues);
						}
					}
					else
					{
						//first: create a DataColumn per row value, using generic column names
						for (int i = 0; i < rowValues.Length; i++)
						{
							string columnName = string.Format("Column{0}", i);
							dt.Columns.Add(columnName, Type.GetType("System.String"));
						}
						//second: since the first row was not a header it must be data, so we need to add a corresponding DataRow
						dt.Rows.Add(rowValues);
					}

					int columnCount = dt.Columns.Count;

					//populate with data:
					while (!parser.EndOfData)
					{
						rowValues = parser.ReadFields();
						if (rowValues.Length != columnCount)
						{
							//TODO: Implement: exception handling: number of row values per row is not constant
#warning Not Implemented: exception handling: number of row values per row is not constant
							throw new NotImplementedException("The invoked code path is not yet implemented.");
						}
						dt.Rows.Add(rowValues);
					}

					return ds;
				}
				finally
				{
					parser.Close();
				}
			}
		}

		/// <summary>
		/// Convenience overload that will automatically configure most of the <see cref="TextFieldParser"/>'s settings based upon the specified parameters.
		/// </summary>
		/// <param name="parser">A <see cref="TextFieldParser"/> that is initialized to read a specific formatted data source (e.g. a stream, reader, or file). The specified instance's settings will be automatically (re)configured based upon the other specified parameters.</param>
		/// <param name="hasHeaderRow">Indicates whether the data's first row/line is a header row containing the names of the data fields/columns.</param>
		/// <param name="includeHeaderRowInData">Indicates whether the data in the header row (if present) should be included as a <see cref="DataRow"/> in the returned <see cref="DataTable"/>. If <paramref name="hasHeaderRow"/> is <c>false</c>, this parameter is ignored.</param>
		/// <param name="hasFieldsEnclosedInQuotes">Indicates whether data values containing a column delimiter can be surrounded by double quotes to allow such values to be read as a single value.</param>
		/// <param name="delimiters">The delimiters that separate the distinct values in each data row of the formatted text.</param>
		/// <param name="commentTokens">The optional set of special row prefix(es) that indicate which lines/rows in the formatted text should be ignored/skipped while parsing the data.</param>
		/// <returns>A <see cref="DataSet"/> containing a single <see cref="DataTable"/> that is populated with the data parsed from the formatted text data source.</returns>
		public static DataSet ParseDelimitedText(TextFieldParser parser, bool hasHeaderRow, bool includeHeaderRowInData, bool hasFieldsEnclosedInQuotes, string[] delimiters, string[] commentTokens)
		{
			parser = ConfigureTextFieldParser(parser, hasFieldsEnclosedInQuotes, true, delimiters, commentTokens);
			return ParseDelimitedText(parser, hasHeaderRow, includeHeaderRowInData);
		}

		/// <summary>
		/// Convenience overload that will automatically configure most of the <see cref="TextFieldParser"/>'s settings based upon the specified parameters.
		/// </summary>
		/// <param name="parser">A <see cref="TextFieldParser"/> that is initialized to read a specific formatted data source (e.g. a stream, reader, or file). The specified instance's settings will be automatically (re)configured based upon the other specified parameters.</param>
		/// <param name="hasHeaderRow">Indicates whether the data's first row/line is a header row containing the names of the data fields/columns.</param>
		/// <param name="includeHeaderRowInData">Indicates whether the data in the header row (if present) should be included as a <see cref="DataRow"/> in the returned <see cref="DataTable"/>. If <paramref name="hasHeaderRow"/> is <c>false</c>, this parameter is ignored.</param>
		/// <param name="hasFieldsEnclosedInQuotes">Indicates whether data values containing a column delimiter can be surrounded by double quotes to allow such values to be read as a single value.</param>
		/// <param name="textFormat">Indicates which of the supported "standard data formats" matches the data format of the data source's data. E.g. "CSV", "TSV", "TAB".</param>
		/// <returns>A <see cref="DataSet"/> containing a single <see cref="DataTable"/> that is populated with the data parsed from the formatted text data source.</returns>
		public static DataSet ParseDelimitedText(TextFieldParser parser, bool hasHeaderRow, bool includeHeaderRowInData, bool hasFieldsEnclosedInQuotes, string textFormat)
		{
			string[] delimiters = GetCsvFileDelimiters(textFormat);
			string[] commentTokens = null; //GetCsvFileCommentTokens(textFormat);
			return ParseDelimitedText(parser, hasHeaderRow, includeHeaderRowInData, hasFieldsEnclosedInQuotes, delimiters, commentTokens);
		}

		/// <summary>
		/// Convenience overload that will automatically create and configure the <see cref="TextFieldParser"/> that will be used to parse the specified <see cref="Stream"/> data source.
		/// </summary>
		/// <param name="stream">A <see cref="Stream"/> connected to a specific source of formatted data.</param>
		/// <param name="hasHeaderRow">Indicates whether the data's first row/line is a header row containing the names of the data fields/columns.</param>
		/// <param name="includeHeaderRowInData">Indicates whether the data in the header row (if present) should be included as a <see cref="DataRow"/> in the returned <see cref="DataTable"/>. If <paramref name="hasHeaderRow"/> is <c>false</c>, this parameter is ignored.</param>
		/// <param name="hasFieldsEnclosedInQuotes">Indicates whether data values containing a column delimiter can be surrounded by double quotes to allow such values to be read as a single value.</param>
		/// <param name="textFormat">Indicates which of the supported "standard data formats" matches the data format of the data source's data. E.g. "CSV", "TSV", "TAB".</param>
		/// <returns>A <see cref="DataSet"/> containing a single <see cref="DataTable"/> that is populated with the data parsed from the formatted text data source.</returns>
		public static DataSet ParseDelimitedText(Stream stream, bool hasHeaderRow, bool includeHeaderRowInData, bool hasFieldsEnclosedInQuotes, string textFormat)
		{
			return ParseDelimitedText(new TextFieldParser(stream), hasHeaderRow, includeHeaderRowInData, hasFieldsEnclosedInQuotes, textFormat);
		}

		/// <summary>
		/// Convenience overload that will automatically create and configure the <see cref="TextFieldParser"/> that will be used to parse the specified <see cref="TextReader"/> data source.
		/// </summary>
		/// <param name="reader">A <see cref="TextReader"/> connected to a specific source of formatted data.</param>
		/// <param name="hasHeaderRow">Indicates whether the data's first row/line is a header row containing the names of the data fields/columns.</param>
		/// <param name="includeHeaderRowInData">Indicates whether the data in the header row (if present) should be included as a <see cref="DataRow"/> in the returned <see cref="DataTable"/>. If <paramref name="hasHeaderRow"/> is <c>false</c>, this parameter is ignored.</param>
		/// <param name="hasFieldsEnclosedInQuotes">Indicates whether data values containing a column delimiter can be surrounded by double quotes to allow such values to be read as a single value.</param>
		/// <param name="textFormat">Indicates which of the supported "standard data formats" matches the data format of the data source's data. E.g. "CSV", "TSV", "TAB".</param>
		/// <returns>A <see cref="DataSet"/> containing a single <see cref="DataTable"/> that is populated with the data parsed from the formatted text data source.</returns>
		public static DataSet ParseDelimitedText(TextReader reader, bool hasHeaderRow, bool includeHeaderRowInData, bool hasFieldsEnclosedInQuotes, string textFormat)
		{
			return ParseDelimitedText(new TextFieldParser(reader), hasHeaderRow, includeHeaderRowInData, hasFieldsEnclosedInQuotes, textFormat);
		}

		#region ParseDelimitedText Helper Methods

		/// <summary>
		/// Utility method that will configure most of a <see cref="TextFieldParser"/>'s settings based upon the specified parameters.
		/// </summary>
		/// <param name="parser"></param>
		/// <param name="hasFieldsEnclosedInQuotes"></param>
		/// <param name="trimWhiteSpace"></param>
		/// <param name="delimiters"></param>
		/// <param name="commentTokens"></param>
		/// <returns></returns>
		private static TextFieldParser ConfigureTextFieldParser(TextFieldParser parser, bool hasFieldsEnclosedInQuotes, bool trimWhiteSpace, string[] delimiters, string[] commentTokens)
		{
			parser.TextFieldType = FieldType.Delimited;
			parser.TrimWhiteSpace = trimWhiteSpace;
			parser.HasFieldsEnclosedInQuotes = hasFieldsEnclosedInQuotes;
			parser.Delimiters = delimiters;
			parser.CommentTokens = commentTokens;
			return parser;
		}

		/// <summary>
		/// Returns the correct value delimiters that correspond to the specified <paramref name="textFormat"/>.
		/// </summary>
		/// <param name="textFormat">Indicates which of the supported "standard data formats" matches the data format of the data source's data. E.g. "CSV", "TSV", "TAB".</param>
		/// <returns></returns>
		private static string[] GetCsvFileDelimiters(string textFormat)
		{
			string[] delimiters;
			switch (textFormat.ToUpperInvariant())
			{
				case "TSV":
				case "TAB":
					delimiters = new[] { "\t" };
					break;
				case "TXT":
				case "?":
				case "UNKNOWN":
					delimiters = new[] { ",", "\t" };
					break;
				case "CSV":
				default:
					delimiters = new[] { "," };
					break;
			}
			return delimiters;
		}

		#endregion

		#region ProcessOutlookCSVFile Method

		//private bool ProcessOutlookCSVFile(Stream inputStream,
		//                                    string csvType,
		//                                    out string contactList,
		//                                    out string errorInfo)
		//{
		//    contactList = String.Empty;
		//    errorInfo = String.Empty;

		//    try
		//    {
		//        DataSet contactsData = ParseDelimitedText(inputStream, true, false, true, csvType);

		//        switch (csvType)
		//        {
		//            case "Outlook":
		//                foreach (DataRow dr in contactsData.Tables[0].Rows)
		//                {
		//                    if (String.IsNullOrEmpty(dr["E-mail Address"].ToString())) { continue; }

		//                    if (contactList != String.Empty) { contactList += ";"; }
		//                    contactList += dr["Last Name"].ToString() + ", " + dr["First Name"].ToString();
		//                    contactList += "|" + dr["E-mail Address"].ToString();
		//                }
		//                break;
		//            case "OutlookExpress":
		//                foreach (DataRow dr in contactsData.Tables[0].Rows)
		//                {
		//                    if (String.IsNullOrEmpty(dr["E-mail Address"].ToString())) { continue; }

		//                    if (contactList != String.Empty) { contactList += ";"; }
		//                    try
		//                    {
		//                        contactList += dr["Last Name"].ToString() + ", " + dr["First Name"].ToString();
		//                    }
		//                    catch
		//                    {
		//                        contactList += dr["Name"].ToString();
		//                    }

		//                    contactList += "|" + dr["E-mail Address"].ToString();
		//                }
		//                break;
		//        }

		//        return true;
		//    }

		//    catch (Exception ex_process_csv)
		//    {
		//        errorInfo = ex_process_csv.Message;
		//        return false;
		//    }
		//}

		#endregion

		#endregion

		#endregion

	}
}
