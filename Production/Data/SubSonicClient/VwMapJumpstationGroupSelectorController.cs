using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapJumpstationGroupSelector class.
	/// </summary>
	public partial class VwMapJumpstationGroupSelectorController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapJumpstationGroupSelectorCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			JumpstationGroupSelectorQuerySpecification qs = new JumpstationGroupSelectorQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			JumpstationGroupSelectorQuerySpecification qs = new JumpstationGroupSelectorQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapJumpstationGroupSelectorCollection Fetch(JumpstationGroupSelectorQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapJumpstationGroupSelectorCollection>();
		}

		public static int FetchCount(JumpstationGroupSelectorQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationGroupSelectorQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationGroupSelectorController.CreateQueryHelper(qs, VwMapJumpstationGroupSelector.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="VwMapJumpstationGroupSelector"/>s that are related to a specified <see cref="JumpstationGroupSelector"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapJumpstationGroupSelectorCollection FetchByJumpstationGroupId(int jumpstationGroupId)
		{
			return VwMapJumpstationGroupSelectorController.Fetch(
				new JumpstationGroupSelectorQuerySpecification { JumpstationGroupId = jumpstationGroupId, Paging = { PageSize = int.MaxValue } });
		}

		/// <summary>
		/// Convenience method.
		/// Returns the <see cref="VwMapJumpstationGroupSelector"/> s that are related to a specified <see cref="JumpstationGroupSelector" />
		/// </summary>
		/// <param name="jumpstationGroupId"></param>
		/// <param name="name"></param>
		public static VwMapJumpstationGroupSelectorCollection FetchByJumpstationGroupIdName(int jumpstationGroupId, string name)
		{
			SqlQuery query = DB.Select().From(JumpstationGroupSelector.Schema);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "JumpstationGroupId", jumpstationGroupId);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
			ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(VwMapJumpstationGroupSelectorController));
			return query.ExecuteAsCollection<VwMapJumpstationGroupSelectorCollection>();
		}

        /// <summary>
        /// Convenience method.
        /// Returns count by JumpstationGroupId.
        /// </summary>
        /// <returns></returns>
        public static int FetchCountByJumpstationGroupId(int jumpstationGroupId)
        {
            return VwMapJumpstationGroupSelectorController.FetchCount(
                new JumpstationGroupSelectorQuerySpecification { JumpstationGroupId = jumpstationGroupId, Paging = { PageSize = int.MaxValue } });
        }

		#endregion

		#endregion

	}
}
