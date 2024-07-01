using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapJumpstationGroupSelectorQueryParameterValue class.
	/// </summary>
	public partial class VwMapJumpstationGroupSelectorQueryParameterValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapJumpstationGroupSelectorQueryParameterValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			JumpstationGroupSelectorQueryParameterValueQuerySpecification qs = new JumpstationGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			JumpstationGroupSelectorQueryParameterValueQuerySpecification qs = new JumpstationGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapJumpstationGroupSelectorQueryParameterValueCollection Fetch(JumpstationGroupSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapJumpstationGroupSelectorQueryParameterValueCollection>();
		}

		public static int FetchCount(JumpstationGroupSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationGroupSelectorQueryParameterValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			SqlQuery q = JumpstationGroupSelectorQueryParameterValueController.CreateQueryHelper(qs, VwMapJumpstationGroupSelectorQueryParameterValue.Schema, isCountQuery);
			return q;
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="JumpstationGroupSelectorQueryParameterValue"/>s that are related to a specified <see cref="JumpstationGroupSelector"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapJumpstationGroupSelectorQueryParameterValueCollection FetchByJumpstationGroupSelectorIdQueryParameterId(int JumpstationGroupSelectorId, int queryParameterId)
		{
			return VwMapJumpstationGroupSelectorQueryParameterValueController.Fetch(
				new JumpstationGroupSelectorQueryParameterValueQuerySpecification { JumpstationGroupSelectorId = JumpstationGroupSelectorId, QueryParameterId = queryParameterId, Paging = { PageSize = int.MaxValue } });
		}

		#endregion

		#endregion

	}
}
