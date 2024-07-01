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
	/// The non-generated portion of the QueryParameterConfigurationServiceGroupTypeController class.
	/// </summary>
	public partial class QueryParameterConfigurationServiceGroupTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static QueryParameterConfigurationServiceGroupTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			QueryParameterConfigurationServiceGroupTypeQuerySpecification qs = new QueryParameterConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			QueryParameterConfigurationServiceGroupTypeQuerySpecification qs = new QueryParameterConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static QueryParameterConfigurationServiceGroupTypeCollection Fetch(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<QueryParameterConfigurationServiceGroupTypeCollection>();
		}

		public static int FetchCount(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterConfigurationServiceGroupTypeController.CreateQueryHelper(qs, QueryParameterConfigurationServiceGroupType.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
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
            if (qs.QueryParameterName != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "QueryParameterName", qs.QueryParameterName);
            }
			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (qs.ConfigurationServiceGroupTypeId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ConfigurationServiceGroupTypeId", qs.ConfigurationServiceGroupTypeId);				
			}

            // omit "*" wildcard
            if (qs.Wildcard != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Wildcard", qs.Wildcard);
            }

            if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(QueryParameterConfigurationServiceGroupTypeController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapQueryParameterConfigurationServiceGroupTypeController.Fetch(qs);
			}
			else
			{
				return QueryParameterConfigurationServiceGroupTypeController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
