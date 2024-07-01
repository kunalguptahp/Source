using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapQueryParameter class.
	/// </summary>
	public partial class VwMapJumpstationQueryParameterController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapQueryParameterCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			QueryParameterQuerySpecification qs = new QueryParameterQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			QueryParameterQuerySpecification qs = new QueryParameterQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapQueryParameterCollection Fetch(QueryParameterQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapQueryParameterCollection>();
		}

		public static int FetchCount(QueryParameterQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterController.CreateQueryHelper(qs, VwMapQueryParameter.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
