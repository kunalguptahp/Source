using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the QueryParameterValueController class.
	/// </summary>
	public partial class QueryParameterValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static QueryParameterValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			QueryParameterValueQuerySpecification qs = new QueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			QueryParameterValueQuerySpecification qs = new QueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static QueryParameterValueCollection Fetch(QueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<QueryParameterValueCollection>();
		}

		public static int FetchCount(QueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterValueController.CreateQueryHelper(qs, QueryParameterValue.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(QueryParameterValueQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			if (qs.QueryParameterId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "QueryParameterId", qs.QueryParameterId);				
			}

			if (qs.QueryParameterValueName != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", qs.QueryParameterValueName);
			}

            if (qs.QueryParameterValueNameCont != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereContains(query, "Name", qs.QueryParameterValueNameCont);
            }

            // omit "*" wildcard
            if (!qs.Wildcard)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsNotEqualTo(query, "Name", "*");                
            }

			if (qs.RowStatusId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "RowStatusId", qs.RowStatusId);
			}

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(QueryParameterValueController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(QueryParameterValueQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapQueryParameterValueController.Fetch(qs);
			}
			else
			{
				return QueryParameterValueController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
