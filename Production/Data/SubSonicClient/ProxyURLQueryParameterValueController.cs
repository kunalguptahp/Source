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
	/// The non-generated portion of the ProxyURLQueryParameterValueController class.
	/// </summary>
	public partial class ProxyURLQueryParameterValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ProxyURLQueryParameterValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ProxyURLQueryParameterValueQuerySpecification qs = new ProxyURLQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ProxyURLQueryParameterValueQuerySpecification qs = new ProxyURLQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ProxyURLQueryParameterValueCollection Fetch(ProxyURLQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ProxyURLQueryParameterValueCollection>();
		}

		public static int FetchCount(ProxyURLQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ProxyURLQueryParameterValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ProxyURLQueryParameterValueController.CreateQueryHelper(qs, ProxyURLQueryParameterValue.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ProxyURLQueryParameterValueQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			if (qs.ProxyURLId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ProxyURLId", qs.ProxyURLId);				
			}

			if (qs.QueryParameterId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "QueryParameterId", qs.QueryParameterId);				
			}

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ProxyURLQueryParameterValueController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ProxyURLQueryParameterValueQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapProxyURLQueryParameterValueController.Fetch(qs);
			}
			else
			{
				return ProxyURLQueryParameterValueController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
