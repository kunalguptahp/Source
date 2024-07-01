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
	/// The non-generated portion of the WorkflowController class.
	/// </summary>
	public partial class WorkflowController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static WorkflowCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			WorkflowQuerySpecification qs = new WorkflowQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			WorkflowQuerySpecification qs = new WorkflowQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static WorkflowCollection Fetch(WorkflowQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<WorkflowCollection>();
		}

		public static int FetchCount(WorkflowQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return WorkflowController.CreateQueryHelper(qs, Workflow.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(WorkflowQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{

            SqlQuery query = DB.Select().From(schema);

            if (qs.AppClientIds.Count != 0)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsIn(query, "AppClientId", qs.AppClientIds);
            }

            if (qs.AppClientId != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "AppClientId", qs.AppClientId);
            }

            if (qs.CompatibleWithWorkflowModuleId != null)
            {
                query.InnerJoin(Tables.WorkflowWorkflowModule, WorkflowWorkflowModule.Columns.WorkflowId,
                                Views.VwMapWorkflow, Workflow.Columns.Id);
                query.AndWhere(WorkflowWorkflowModule.WorkflowModuleIdColumn.ColumnName).IsEqualTo(qs.CompatibleWithWorkflowModuleId);
            }

            if (qs.WorkflowTypeId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowTypeId", qs.WorkflowTypeId);
			}

            if (qs.WorkflowApplicationId != null)
			{
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowApplicationId", qs.WorkflowApplicationId);
			}

			if (qs.WorkflowStatusId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowStatusId", qs.WorkflowStatusId);
			}

			if (qs.OwnerId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "OwnerId", qs.OwnerId);
			}

			if (qs.ValidationId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ValidationId", qs.ValidationId);
			}

			if (qs.ProductionId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ProductionId", qs.ProductionId);
			}

			if (qs.PlatformQueryParameterValue != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "PlatformQueryParameterValue", String.Format("%{0}%", qs.PlatformQueryParameterValue), true);				
			}

			if (qs.ReleaseQueryParameterValue != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "ReleaseQueryParameterValue", String.Format("%{0}%", qs.ReleaseQueryParameterValue), true);
			}

			if (qs.CountryQueryParameterValue != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "CountryQueryParameterValue", String.Format("%{0}%", qs.CountryQueryParameterValue), true);
			}

			if (qs.BrandQueryParameterValue != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "BrandQueryParameterValue", String.Format("%{0}%", qs.BrandQueryParameterValue), true);
			}

            if (qs.SubBrandQueryParameterValue != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "SubBrandQueryParameterValue", qs.SubBrandQueryParameterValue);
            }

			if (qs.ModelNumberQueryParameterValue != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "ModelNumberQueryParameterValue", String.Format("%{0}%", qs.ModelNumberQueryParameterValue), true);
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

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(WorkflowController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(WorkflowQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapWorkflowController.Fetch(qs);
			}
			else
			{
				return WorkflowController.Fetch(qs);
			}
		}

		#endregion

		#endregion

		#region Tag Accessor Methods

		/// <summary>
		/// Returns a list of the names of the Tags associated with a specified Workflow.
		/// </summary>
		/// <param name="WorkflowId"></param>
		/// <param name="rowStatusId"></param>
		/// <returns></returns>
		public static List<string> GetTagNameList(int WorkflowId, RowStatus.RowStatusId rowStatusId)
		{
			return new List<string>(WorkflowController.GetTagNames(WorkflowId, rowStatusId));
		}

		public static IEnumerable<string> GetTagNames(int WorkflowId, RowStatus.RowStatusId rowStatusId)
		{
			//TODO: Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar
#warning Review Needed: Consider whether using the SubSonic QueryCommand would be better than SqlUtility.ExecuteAsScalar

			const string sql = "SELECT [dbo].[Workflow_GetTagList](@WorkflowId, ', ', @TagRowStatusId)";

			string tagNameList = SqlUtility.ExecuteAsScalar<string>(
				SqlUtility.CreateSqlCommandForSql(
					ElementsCPSSqlUtility.CreateDefaultConnection(),
					sql,
					SqlUtility.CreateSqlParameter("@WorkflowId", SqlDbType.Int, WorkflowId),
					SqlUtility.CreateSqlParameter("@TagRowStatusId", SqlDbType.Int, RowStatus.RowStatusId.Active)));
			return Tag.ParseTagNameList(tagNameList, false);
		}
		
		#endregion

		#region GetSql_FilterWorkflowsByTagNames Methods

		/// <summary>
		/// Constructs a SQL WHERE clause that will filter a SQL query such that only Workflows that are tagged with a specified set of Tags 
		/// and are not tagged with a specified second set of Tags will be returned by the query.
		/// </summary>
		/// <param name="WorkflowIdColumnName">The name of the column (in the SQL query) that contains the <see cref="Workflow"/>s' <see cref="Workflow.Id"/> values.</param>
		/// <param name="requiredTags">A set of tags that all of the <see cref="Workflow"/>s returned by the query must possess.</param>
		/// <param name="forbiddenTags">A set of tags that none of the <see cref="Workflow"/>s returned by the query may possess.</param>
		internal static string GetSql_FilterWorkflowsByTagNames(string WorkflowIdColumnName, IEnumerable<string> requiredTags, IEnumerable<string> forbiddenTags)
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
			const string requiredTagFilteringSqlTemplate = @"EXISTS (SELECT 'x' FROM [ElementsCPSDB].[dbo].[vwMapWorkflowTag_Tag] WITH (NOLOCK) WHERE ([ElementsCPSDB].[dbo].[vwMapWorkflowTag_Tag].[WorkflowID] = {0}) AND ([ElementsCPSDB].[dbo].[vwMapWorkflowTag_Tag].[TagName] = {1}))";
			const string forbiddenTagFilteringSqlTemplate = "NOT " + requiredTagFilteringSqlTemplate;
			//NOTE: Could also use "[dbo].[Workflow_HasTag_ByTagName2]" instead of "[dbo].[Workflow_HasTag_ByTagName]"
			//const string requiredTagFilteringSqlTemplate = @"0 <> [ElementsCPSDB].[dbo].[Workflow_HasTag_ByTagName]({0}, {1}, 1)";
			//const string forbiddenTagFilteringSqlTemplate = @"0 = [ElementsCPSDB].[dbo].[Workflow_HasTag_ByTagName]({0}, {1}, 1)";

			List<string> sqlClauses = new List<string>();

			//add the WHERE clauses for the requiredTags filter clauses
			Boolean isFirstRequiredTag = true;
			if (requiredTags != null)
			{
				foreach (string tagName in requiredTags)
				{
					string tagFilteringSqlExpression = string.Format(CultureInfo.InvariantCulture,
						requiredTagFilteringSqlTemplate,
						WorkflowIdColumnName,
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
						WorkflowIdColumnName,
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
		/// Constructs a SQL WHERE clause that will filter a SQL query such that only Workflows that are tagged with a specified set of Tags 
		/// and are not tagged with a specified second set of Tags will be returned by the query.
		/// </summary>
		/// <param name="WorkflowIdColumnName">The name of the column (in the SQL query) that contains the <see cref="Workflow"/>s' <see cref="Workflow.Id"/> values.</param>
		/// <param name="tagsFilteringCriteria"></param>
		internal static string GetSql_FilterWorkflowsByTagNames(string WorkflowIdColumnName, string tagsFilteringCriteria)
		{
			const string sqlEmptyExpression = "";

			if (string.IsNullOrEmpty(tagsFilteringCriteria) || string.IsNullOrEmpty(tagsFilteringCriteria.Trim()))
			{
				return sqlEmptyExpression;
			}

			//parse the tagsFilteringCriteria
			IEnumerable<string> requiredTags;
			IEnumerable<string> forbiddenTags;
			WorkflowController.ParseTagsFilteringCriteria(tagsFilteringCriteria, out requiredTags, out forbiddenTags);

			return WorkflowController.GetSql_FilterWorkflowsByTagNames(WorkflowIdColumnName, requiredTags, forbiddenTags);
		}

		#endregion

		#region ParseTagsFilteringCriteria Method

		/// <summary>
		/// Parses a query filtering string into two sets of <see cref="Tag"/> names.
		/// </summary>
		/// <param name="tagsFilteringCriteria"></param>
		/// <param name="requiredTags">A set of tags that all of the <see cref="Workflow"/>s returned by the query must possess.</param>
		/// <param name="forbiddenTags">A set of tags that none of the <see cref="Workflow"/>s returned by the query may possess.</param>
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

		#region Other Methods

		/// <summary>
		/// Convenience method.
		/// Returns the <see cref="Workflow"/> with the specified name (if one exists).
		/// </summary>
		/// <param name="name"></param>
		public static Workflow FetchByName(string name)
		{
			SqlQuery query = DB.Select().From(Workflow.Schema);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
			ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(WorkflowController));
			Workflow instance = query.ExecuteSingle<Workflow>();
			return instance;
		}

		public static string GenerateDefaultName(int? WorkflowId)
		{
			return string.Format("New Product for Workflow #{0} created {1:U}", WorkflowId, DateTime.UtcNow);
		}

        #endregion

		#region Data Import Methods

		public static ImportUtilityForStatefulEntity<Workflow> CreateImportUtility()
		{
			ImportUtilityForStatefulEntity<Workflow> importUtilityForStatefulEntity =
				new ImportUtilityForStatefulEntity<Workflow>(Workflow.Columns.WorkflowStatusId)
					{
						NewInstanceFactory = () => new Workflow(true)
						, ModifyLifecycleStateCallback = importUtilityForStatefulEntity_SetState
					};
			return importUtilityForStatefulEntity;
		}

		private static void importUtilityForStatefulEntity_SetState(Workflow instance, object valueToSet)
		{
			if (valueToSet != null)
			{
				WorkflowStateId stateId = ((WorkflowStateId)ElementsCPSDataUtility.ParseImportValueAsInt32(valueToSet));
				if (instance.CurrentState != stateId)
				{
					instance.GoToState(stateId);
				}
			}
		}

		#endregion

	}
}
