using System.ComponentModel;
using System.Data;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapQueryParameterProxyURLType class.
	/// </summary>
	public partial class VwMapQueryParameterProxyURLTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapQueryParameterProxyURLTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			QueryParameterProxyURLTypeQuerySpecification qs = new QueryParameterProxyURLTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			QueryParameterProxyURLTypeQuerySpecification qs = new QueryParameterProxyURLTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapQueryParameterProxyURLTypeCollection Fetch(QueryParameterProxyURLTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapQueryParameterProxyURLTypeCollection>();
		}

		public static int FetchCount(QueryParameterProxyURLTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterProxyURLTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterProxyURLTypeController.CreateQueryHelper(qs, VwMapQueryParameterProxyURLType.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		public static VwMapQueryParameterProxyURLTypeCollection GetDistinctQueryParameterProxyURLTypeCollection()
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT DISTINCT QueryParameterId, QueryParameterName FROM vwMapQueryParameter_ProxyURLType ORDER BY QueryParameterName", QueryParameterProxyURLType.Schema.Provider.Name);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			VwMapQueryParameterProxyURLTypeCollection coll = new VwMapQueryParameterProxyURLTypeCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}

		#endregion

		#endregion

	}
}
