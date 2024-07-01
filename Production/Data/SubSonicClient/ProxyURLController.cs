using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.ElementsCPS.Data.Utility.ImportUtility;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ProxyURLController class.
	/// </summary>
	public partial class ProxyURLController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ProxyURLCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ProxyURLQuerySpecification qs = new ProxyURLQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ProxyURLQuerySpecification qs = new ProxyURLQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ProxyURLCollection Fetch(ProxyURLQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ProxyURLCollection>();
		}

		public static int FetchCount(ProxyURLQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ProxyURLQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ProxyURLController.CreateQueryHelper(qs, ProxyURL.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ProxyURLQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

            //if (qs.CompatibleWithQueryParameterId != null)
            //{
            //    query.InnerJoin(ProxyURLQueryParameterValue.ProxyURLIdColumn, ProxyURL.IdColumn);
            //    query.AndWhere(ProxyURLQueryParameterValue.IdColumn).IsEqualTo(qs.CompatibleWithQueryParameterId);
            //}

			if (qs.ProxyURL != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereContains(query, "URL", qs.ProxyURL);
			}

			if (qs.ProxyURLTypeId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ProxyURLTypeId", qs.ProxyURLTypeId);
			}

			if (qs.ProxyURLStatusId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ProxyURLStatusId", qs.ProxyURLStatusId);
			}

			if (qs.OwnerId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "OwnerId", qs.OwnerId);
			}

			if (qs.TouchpointParameterValueQueryParameterValueId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "TouchpointParameterValueQueryParameterValueId", qs.TouchpointParameterValueQueryParameterValueId);
			}

			if (qs.BrandParameterValueQueryParameterValueId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "BrandParameterValueQueryParameterValueId", qs.BrandParameterValueQueryParameterValueId);
			}

			if (qs.LocaleParameterValueQueryParameterValueId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "LocaleParameterValueQueryParameterValueId", qs.LocaleParameterValueQueryParameterValueId);
			}

			if (qs.CycleParameterValueQueryParameterValueId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "CycleParameterValueQueryParameterValueId", qs.CycleParameterValueQueryParameterValueId);
			}

			if (qs.PlatformParameterValueQueryParameterValueId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "PlatformParameterValueQueryParameterValueId", qs.PlatformParameterValueQueryParameterValueId);
			}

			if (qs.PartnerCategoryParameterValueQueryParameterValueId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "PartnerCategoryParameterValueQueryParameterValueId", qs.PartnerCategoryParameterValueQueryParameterValueId);
			}

			if (qs.ValidationId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ValidationId", qs.ValidationId);
			}

			if (qs.ProductionId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ProductionId", qs.ProductionId);
			}

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ProxyURLController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ProxyURLQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapProxyURLController.Fetch(qs);
			}
			else
			{
				return ProxyURLController.Fetch(qs);
			}
		}

		#endregion

		#endregion

		#region Tag Accessor Methods

		/// <summary>
		/// Returns a list of the names of the Tags associated with a specified ProxyURL.
		/// </summary>
		/// <param name="ProxyURLId"></param>
		/// <param name="rowStatusId"></param>
		/// <returns></returns>
		public static List<string> GetTagNameList(int ProxyURLId, RowStatus.RowStatusId rowStatusId)
		{
			return new List<string>(ProxyURLController.GetTagNames(ProxyURLId, rowStatusId));
		}

		public static IEnumerable<string> GetTagNames(int ProxyURLId, RowStatus.RowStatusId rowStatusId)
		{
			//TODO: Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar
#warning Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar

			const string sql = "SELECT [dbo].[ProxyURL_GetTagList](@ProxyURLId, ', ', @TagRowStatusId)";

			string tagNameList = SqlUtility.ExecuteAsScalar<string>(
				SqlUtility.CreateSqlCommandForSql(
					ElementsCPSSqlUtility.CreateDefaultConnection(),
					sql,
					SqlUtility.CreateSqlParameter("@ProxyURLId", SqlDbType.Int, ProxyURLId),
					SqlUtility.CreateSqlParameter("@TagRowStatusId", SqlDbType.Int, RowStatus.RowStatusId.Active)));
			return Tag.ParseTagNameList(tagNameList, false);
		}
		
		#endregion

		#region GetSql_FilterProxyURLsByTagNames Methods

		/// <summary>
		/// Constructs a SQL WHERE clause that will filter a SQL query such that only ProxyURLs that are tagged with a specified set of Tags 
		/// and are not tagged with a specified second set of Tags will be returned by the query.
		/// </summary>
		/// <param name="ProxyURLIdColumnName">The name of the column (in the SQL query) that contains the <see cref="ProxyURL"/>s' <see cref="ProxyURL.Id"/> values.</param>
		/// <param name="requiredTags">A set of tags that all of the <see cref="ProxyURL"/>s returned by the query must possess.</param>
		/// <param name="forbiddenTags">A set of tags that none of the <see cref="ProxyURL"/>s returned by the query may possess.</param>
		internal static string GetSql_FilterProxyURLsByTagNames(string ProxyURLIdColumnName, IEnumerable<string> requiredTags, IEnumerable<string> forbiddenTags)
		{
			//check for certain conditions that allow for performance optimizations (i.e. short-circuits)
			if ((requiredTags == null) && (forbiddenTags == null))
			{
				return SqlUtility.SqlExpressionAlwaysTrue;
			}

			if ((requiredTags != null) && (forbiddenTags != null))
			{
				//if there is any overlap between the 2 sets, then the filter condition is a logical contradiction (i.e. always false)
				int countOfIntersectingClauses = requiredTags.Intersect(forbiddenTags, StringComparer.CurrentCultureIgnoreCase).Count();
				if (countOfIntersectingClauses > 0)
				{
					//ensure that the query will return nothing, but more quickly than if we allowed actual tag filtering
					return SqlUtility.SqlExpressionAlwaysFalse;
				}
			}

			if (requiredTags != null)
			{
				//if the requiredTags set contains any invalid Tag names, then the filter condition is a logical contradiction (i.e. always false)
				int countOfInvalidWithTagNames = requiredTags.Count(tagName => !Tag.IsValidName(tagName));
				if (countOfInvalidWithTagNames > 0)
				{
					//ensure that the query will return nothing, but more quickly than if we allowed actual tag filtering
					return SqlUtility.SqlExpressionAlwaysFalse;
				}
			}

			//NOTE: According to Tom Stoudt, it is better to have SQL for the WHERE clauses completely "inlined" rather than calling any UDFs/SPs/etc.
			const string requiredTagFilteringSqlTemplate = @"EXISTS (SELECT 'x' FROM [ElementsCPSDB].[dbo].[vwMapProxyURLTag_Tag] WITH (NOLOCK) WHERE ([ElementsCPSDB].[dbo].[vwMapProxyURLTag_Tag].[ProxyURLID] = {0}) AND ([ElementsCPSDB].[dbo].[vwMapProxyURLTag_Tag].[TagName] = {1}))";
			const string forbiddenTagFilteringSqlTemplate = "NOT " + requiredTagFilteringSqlTemplate;
			//NOTE: Could also use "[dbo].[ProxyURL_HasTag_ByTagName2]" instead of "[dbo].[ProxyURL_HasTag_ByTagName]"
			//const string requiredTagFilteringSqlTemplate = @"0 <> [ElementsCPSDB].[dbo].[ProxyURL_HasTag_ByTagName]({0}, {1}, 1)";
			//const string forbiddenTagFilteringSqlTemplate = @"0 = [ElementsCPSDB].[dbo].[ProxyURL_HasTag_ByTagName]({0}, {1}, 1)";

			List<string> sqlClauses = new List<string>();

			//add the WHERE clauses for the requiredTags filter clauses
			Boolean isFirstRequiredTag = true;
			if (requiredTags != null)
			{
				foreach (string tagName in requiredTags)
				{
					string tagFilteringSqlExpression = string.Format(CultureInfo.InvariantCulture,
						requiredTagFilteringSqlTemplate,
						ProxyURLIdColumnName,
						SqlUtility.CreateSqlStringLiteral(tagName));

					if (isFirstRequiredTag)
					{
						//TODO: Refactoring: Performance Optimization: Since Tag use is sparse, we should do a JOIN for the 1st required tag, and only use a WHERE clause for the rest
#warning Refactoring: Performance Optimization: Since Tag use is sparse, we should do a JOIN for the 1st required tag, and only use a WHERE clause for the rest

						sqlClauses.Add(tagFilteringSqlExpression);
					}
					else
					{
						sqlClauses.Add(tagFilteringSqlExpression);
					}
					isFirstRequiredTag = false;
				}
			}

			//add the WHERE clauses for the forbiddenTags filter clauses
			if (forbiddenTags != null)
			{
				foreach (string tagName in forbiddenTags)
				{
					if (!Tag.IsValidName(tagName))
					{
						//any invalid tag names in the WithoutTag set can be safely ignored, because each of those will result in a filter condition that is a logical tautology (i.e. always true)
						continue;
					}

					string tagFilteringSqlExpression = string.Format(CultureInfo.InvariantCulture,
						forbiddenTagFilteringSqlTemplate,
						ProxyURLIdColumnName,
						SqlUtility.CreateSqlStringLiteral(tagName));
					sqlClauses.Add(tagFilteringSqlExpression);
				}
			}

			//return (sqlClauses.Count == 0) ? "" : string.Format(CultureInfo.InvariantCulture, "({0})", string.Join(" AND ", sqlClauses.ToArray()));
			if (sqlClauses.Count > 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "( {0} )", string.Join(" AND ", sqlClauses.ToArray()));
			}

			return SqlUtility.SqlExpressionAlwaysTrue;
		}

		/// <summary>
		/// Constructs a SQL WHERE clause that will filter a SQL query such that only ProxyURLs that are tagged with a specified set of Tags 
		/// and are not tagged with a specified second set of Tags will be returned by the query.
		/// </summary>
		/// <param name="ProxyURLIdColumnName">The name of the column (in the SQL query) that contains the <see cref="ProxyURL"/>s' <see cref="ProxyURL.Id"/> values.</param>
		/// <param name="tagsFilteringCriteria"></param>
		internal static string GetSql_FilterProxyURLsByTagNames(string ProxyURLIdColumnName, string tagsFilteringCriteria)
		{
			const string sqlEmptyExpression = "";

			if (string.IsNullOrEmpty(tagsFilteringCriteria) || string.IsNullOrEmpty(tagsFilteringCriteria.Trim()))
			{
				return sqlEmptyExpression;
			}

			//parse the tagsFilteringCriteria
			IEnumerable<string> requiredTags;
			IEnumerable<string> forbiddenTags;
			ProxyURLController.ParseTagsFilteringCriteria(tagsFilteringCriteria, out requiredTags, out forbiddenTags);

			return ProxyURLController.GetSql_FilterProxyURLsByTagNames(ProxyURLIdColumnName, requiredTags, forbiddenTags);
		}

		#endregion

		#region ParseTagsFilteringCriteria Method

		/// <summary>
		/// Parses a query filtering string into two sets of <see cref="Tag"/> names.
		/// </summary>
		/// <param name="tagsFilteringCriteria"></param>
		/// <param name="requiredTags">A set of tags that all of the <see cref="ProxyURL"/>s returned by the query must possess.</param>
		/// <param name="forbiddenTags">A set of tags that none of the <see cref="ProxyURL"/>s returned by the query may possess.</param>
		internal static void ParseTagsFilteringCriteria(string tagsFilteringCriteria, out IEnumerable<string> requiredTags, out IEnumerable<string> forbiddenTags)
		{
			//split the criteria by the allowed clause delimiters (ignoring consecutive delimiters)
			char[] delimiters = ", \t".ToCharArray();
			List<string> allTags = new List<string>(tagsFilteringCriteria.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));

			//separate all of the clauses that have a leading "-" char as "forbidden Tag" conditions
			forbiddenTags = allTags.FindAll(s => s.StartsWith("-"));

			//treat all of the remaining clauses (i.e. that don't have a leading "-" char) as "required Tag" conditions
			requiredTags = new List<string>(allTags.Except(forbiddenTags));

			//remove all leading "-" chars from the forbiddenTags list, now that we have separated the 2 types
			forbiddenTags = new List<string>(forbiddenTags.YieldTrimStart("-".ToCharArray()));

			//remove any leading "+" chars from the requiredTags list, since they are implicit and therefore unnecessary
			requiredTags = new List<string>(requiredTags.YieldTrimStart("+".ToCharArray()));

			//remove any duplicates that exist in either set
			forbiddenTags = forbiddenTags.RemoveDuplicates(StringComparer.CurrentCultureIgnoreCase);
			requiredTags = requiredTags.RemoveDuplicates(StringComparer.CurrentCultureIgnoreCase);
		}

		#endregion

		#region Publish Validation

		public static bool IsQueryParameterValuesDuplicated(int proxyURLId)
		{
			//TODO: Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar
			#warning Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar

			const string sql = "SELECT [dbo].[ProxyURL_HasDuplicateQueryParameterValues](@ProxyURLId)";

			int proxyURLDuplicateCount = SqlUtility.ExecuteAsScalar<int>(
				SqlUtility.CreateSqlCommandForSql(
					ElementsCPSSqlUtility.CreateDefaultConnection(),
					sql,
					SqlUtility.CreateSqlParameter("@ProxyURLId", SqlDbType.Int, proxyURLId)));
			return (proxyURLDuplicateCount > 0);
		}

		public static bool IsQueryParameterValuesDuplicated(int proxyURLId, string queryParameterValueIdDelimitedList)
		{
			//TODO: Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar
			#warning Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar

			if (queryParameterValueIdDelimitedList == null)
			{
				return false;
			}

			const string sql = "SELECT [dbo].[ProxyURL_HasDuplicateQueryParameterValuesDelimitedList](@ProxyURLId, @ProxyURLQueryParameterValueIdList)";

			int proxyURLDuplicateCount = SqlUtility.ExecuteAsScalar<int>(
				SqlUtility.CreateSqlCommandForSql(
					ElementsCPSSqlUtility.CreateDefaultConnection(),
					sql,
					SqlUtility.CreateSqlParameter("@ProxyURLId", SqlDbType.Int, proxyURLId),
					SqlUtility.CreateSqlParameter("@ProxyURLQueryParameterValueIdList", SqlDbType.VarChar,
					                              queryParameterValueIdDelimitedList)));

			return (proxyURLDuplicateCount > 0);
		}

		#endregion

		#region Data Import Methods

		public static ImportUtilityForStatefulEntity<ProxyURL> CreateImportUtility()
		{
			ImportUtilityForStatefulEntity<ProxyURL> importUtilityForStatefulEntity =
				new ImportUtilityForStatefulEntity<ProxyURL>(ProxyURL.Columns.ProxyURLStatusId)
					{
						NewInstanceFactory = () => new ProxyURL(true)
						, ModifyLifecycleStateCallback = importUtilityForStatefulEntity_SetState
					};
			return importUtilityForStatefulEntity;
		}

		private static void importUtilityForStatefulEntity_SetState(ProxyURL instance, object valueToSet)
		{
			if (valueToSet != null)
			{
				ProxyURLStateId stateId = ((ProxyURLStateId)ElementsCPSDataUtility.ParseImportValueAsInt32(valueToSet));
				if (instance.CurrentState != stateId)
				{
					instance.GoToState(stateId);
				}
			}
		}

		#endregion

	}
}
