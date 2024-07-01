using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the QueryParameterWorkflowTypeController class.
	/// </summary>
	public partial class QueryParameterWorkflowTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static QueryParameterWorkflowTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			QueryParameterWorkflowTypeQuerySpecification qs = new QueryParameterWorkflowTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			QueryParameterWorkflowTypeQuerySpecification qs = new QueryParameterWorkflowTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static QueryParameterWorkflowTypeCollection Fetch(QueryParameterWorkflowTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<QueryParameterWorkflowTypeCollection>();
		}

		public static int FetchCount(QueryParameterWorkflowTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterWorkflowTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterWorkflowTypeController.CreateQueryHelper(qs, QueryParameterWorkflowType.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(QueryParameterWorkflowTypeQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
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

			if (qs.RowStatusId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "RowStatusId", qs.RowStatusId);
			}
			
			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (qs.WorkflowTypeId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowTypeId", qs.WorkflowTypeId);				
			}

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(QueryParameterWorkflowTypeController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(QueryParameterWorkflowTypeQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapQueryParameterWorkflowTypeController.Fetch(qs);
			}
			else
			{
				return QueryParameterWorkflowTypeController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
