using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapJumpstationMacroValue class.
	/// </summary>
	public partial class VwMapJumpstationMacroValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapJumpstationMacroValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			JumpstationMacroValueQuerySpecification qs = new JumpstationMacroValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			JumpstationMacroValueQuerySpecification qs = new JumpstationMacroValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapJumpstationMacroValueCollection Fetch(JumpstationMacroValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapJumpstationMacroValueCollection>();
		}

		public static int FetchCount(JumpstationMacroValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationMacroValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationMacroValueController.CreateQueryHelper(qs, VwMapJumpstationMacroValue.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

        /// <summary>
        /// Convenience method.
        /// Returns all <see cref="VwMapJumpstationMacroValue"/>s that are related to a specified <see cref="JumpstationMacroValue"/>.
        /// </summary>
        /// <returns></returns>
        public static VwMapJumpstationMacroValueCollection FetchByJumpstationMacroId(int jumpstationMacroId)
        {
            return VwMapJumpstationMacroValueController.Fetch(
                new JumpstationMacroValueQuerySpecification { JumpstationMacroId = jumpstationMacroId, Paging = { PageSize = int.MaxValue } });
        }

		/// <summary>
		/// Convenience method.
		/// Returns the <see cref="VwMapJumpstationMacroValue"/> s that are related to a specified <see cref="JumpstationMacroValue" />
		/// </summary>
		/// <param name="jumpstationMacroId"></param>
		/// <param name="name"></param>
		public static VwMapJumpstationMacroValueCollection FetchByJumpstationMacroIdName(int jumpstationMacroId, string name)
		{
			SqlQuery query = DB.Select().From(JumpstationMacroValue.Schema);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "JumpstationMacroId", jumpstationMacroId);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "MatchName", name);
			ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(VwMapJumpstationMacroValueController));
			return query.ExecuteAsCollection<VwMapJumpstationMacroValueCollection>();
		}

		#endregion

		#endregion

	}
}
