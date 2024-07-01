using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the WorkflowModuleController class.
	/// </summary>
	public partial class WorkflowModuleController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static WorkflowModuleCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			WorkflowModuleQuerySpecification qs = new WorkflowModuleQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			WorkflowModuleQuerySpecification qs = new WorkflowModuleQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static WorkflowModuleCollection Fetch(WorkflowModuleQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<WorkflowModuleCollection>();
		}

		public static int FetchCount(WorkflowModuleQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowModuleQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return WorkflowModuleController.CreateQueryHelper(qs, WorkflowModule.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(WorkflowModuleQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

            if (qs.CreatedBy != null)
            {
                string[] strs = qs.CreatedBy.Split(',');
                List<string> strlist = new List<string>();
                foreach (string str in strs)
                {
                    strlist.Add(str);
                }
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsIn(query, "CreatedBy", strlist);
            }

			if (qs.WorkflowModuleStatusId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowModuleStatusId", qs.WorkflowModuleStatusId);
			}

			if (qs.OwnerId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "OwnerId", qs.OwnerId);
			}

			if (qs.VersionMajor != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "VersionMajor", qs.VersionMajor);
			}

			if (qs.VersionMinor != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "VersionMinor", qs.VersionMinor);
			}

			if (qs.ValidationId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ValidationId", qs.ValidationId);
			}

			if (qs.ProductionId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ProductionId", qs.ProductionId);
			}

			if (qs.ModuleCategoryId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowModuleCategoryId", qs.ModuleCategoryId);
			}

			if (qs.ModuleSubCategoryId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowModuleSubCategoryId", qs.ModuleSubCategoryId);
			}

            if (qs.Filename != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereContains(query, "Filename", qs.Filename);
            }

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(WorkflowModuleController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(WorkflowModuleQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapWorkflowModuleController.Fetch(qs);
			}
			else
			{
				return WorkflowModuleController.Fetch(qs);
			}
		}

		#endregion

		#endregion

		#region Tag Accessor Methods

		/// <summary>
		/// Returns a list of the names of the Tags associated with a specified Workflow Module.
		/// </summary>
		/// <param name="WorkflowModuleId"></param>
		/// <param name="rowStatusId"></param>
		/// <returns></returns>
		public static List<string> GetTagNameList(int WorkflowModuleId, RowStatus.RowStatusId rowStatusId)
		{
			return new List<string>(WorkflowModuleController.GetTagNames(WorkflowModuleId, rowStatusId));
		}

		public static IEnumerable<string> GetTagNames(int WorkflowModuleId, RowStatus.RowStatusId rowStatusId)
		{
			//TODO: Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar
#warning Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar

			const string sql = "SELECT [dbo].[WorkflowModule_GetTagList](@WorkflowModuleId, ', ', @TagRowStatusId)";

			string tagNameList = SqlUtility.ExecuteAsScalar<string>(
				SqlUtility.CreateSqlCommandForSql(
					ElementsCPSSqlUtility.CreateDefaultConnection(),
					sql,
					SqlUtility.CreateSqlParameter("@WorkflowModuleId", SqlDbType.Int, WorkflowModuleId),
					SqlUtility.CreateSqlParameter("@TagRowStatusId", SqlDbType.Int, RowStatus.RowStatusId.Active)));
			return Tag.ParseTagNameList(tagNameList, false);
		}

		#endregion

		#region GetSql_FilterWorkflowModulesByTagNames Methods

		/// <summary>
		/// Constructs a SQL WHERE clause that will filter a SQL query such that only Workflow Modules that are tagged with a specified set of Tags 
		/// and are not tagged with a specified second set of Tags will be returned by the query.
		/// </summary>
		/// <param name="WorkflowModuleIdColumnName">The name of the column (in the SQL query) that contains the <see cref="WorkflowModule"/>s' <see cref="WorkflowModule.Id"/> values.</param>
		/// <param name="requiredTags">A set of tags that all of the <see cref="WorkflowModule"/>s returned by the query must possess.</param>
		/// <param name="forbiddenTags">A set of tags that none of the <see cref="WorkflowModule"/>s returned by the query may possess.</param>
		internal static string GetSql_FilterWorkflowModulesByTagNames(string WorkflowModuleIdColumnName, IEnumerable<string> requiredTags, IEnumerable<string> forbiddenTags)
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
			const string requiredTagFilteringSqlTemplate = @"EXISTS (SELECT 'x' FROM [ElementsCPSDB].[dbo].[vwMapWorkflowModuleTag_Tag] WITH (NOLOCK) WHERE ([ElementsCPSDB].[dbo].[vwMapWorkflowModuleTag_Tag].[WorkflowModuleId] = {0}) AND ([ElementsCPSDB].[dbo].[vwMapWorkflowModuleTag_Tag].[TagName] = {1}))";
			const string forbiddenTagFilteringSqlTemplate = "NOT " + requiredTagFilteringSqlTemplate;
			//NOTE: Could also use "[dbo].[WorkflowModule_HasTag_ByTagName2]" instead of "[dbo].[WorkflowModule_HasTag_ByTagName]"
			//const string requiredTagFilteringSqlTemplate = @"0 <> [ElementsCPSDB].[dbo].[WorkflowModule_HasTag_ByTagName]({0}, {1}, 1)";
			//const string forbiddenTagFilteringSqlTemplate = @"0 = [ElementsCPSDB].[dbo].[WorkflowModule_HasTag_ByTagName]({0}, {1}, 1)";

			List<string> sqlClauses = new List<string>();

			//add the WHERE clauses for the requiredTags filter clauses
			Boolean isFirstRequiredTag = true;
			if (requiredTags != null)
			{
				foreach (string tagName in requiredTags)
				{
					string tagFilteringSqlExpression = string.Format(CultureInfo.InvariantCulture,
						requiredTagFilteringSqlTemplate,
						WorkflowModuleIdColumnName,
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
						WorkflowModuleIdColumnName,
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
		/// Constructs a SQL WHERE clause that will filter a SQL query such that only Workflow Modules that are tagged with a specified set of Tags 
		/// and are not tagged with a specified second set of Tags will be returned by the query.
		/// </summary>
		/// <param name="WorkflowModuleIdColumnName">The name of the column (in the SQL query) that contains the <see cref="WorkflowModule"/>s' <see cref="WorkflowModule.Id"/> values.</param>
		/// <param name="tagsFilteringCriteria"></param>
		internal static string GetSql_FilterWorkflowModulesByTagNames(string WorkflowModuleIdColumnName, string tagsFilteringCriteria)
		{
			const string sqlEmptyExpression = "";

			if (string.IsNullOrEmpty(tagsFilteringCriteria) || string.IsNullOrEmpty(tagsFilteringCriteria.Trim()))
			{
				return sqlEmptyExpression;
			}

			//parse the tagsFilteringCriteria
			IEnumerable<string> requiredTags;
			IEnumerable<string> forbiddenTags;
			WorkflowModuleController.ParseTagsFilteringCriteria(tagsFilteringCriteria, out requiredTags, out forbiddenTags);

			return WorkflowModuleController.GetSql_FilterWorkflowModulesByTagNames(WorkflowModuleIdColumnName, requiredTags, forbiddenTags);
		}

		#endregion

		#region ParseTagsFilteringCriteria Method

		/// <summary>
		/// Parses a query filtering string into two sets of <see cref="Tag"/> names.
		/// </summary>
		/// <param name="tagsFilteringCriteria"></param>
		/// <param name="requiredTags">A set of tags that all of the <see cref="WorkflowModule"/>s returned by the query must possess.</param>
		/// <param name="forbiddenTags">A set of tags that none of the <see cref="WorkflowModule"/>s returned by the query may possess.</param>
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
	}
}
