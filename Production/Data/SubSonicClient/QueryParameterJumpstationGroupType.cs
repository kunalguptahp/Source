using SubSonic;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Utility;
using System.Data;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the QueryParameterJumpstationGroupType table.
	/// </summary>
	public partial class QueryParameterJumpstationGroupType
    {
        #region "Destroy Method"

        /// <summary>
        /// Deletes a specified QueryParameterJumpstationGroupType record (whether the table supports logical/soft deletes or not).
        /// </summary>
        public void Destroy()
        {
            Query query = QueryParameterJumpstationGroupType.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(IdColumn.ColumnName, this.Id);
            QueryParameterJumpstationGroupTypeController.DestroyByQuery(query);
        }

		/// <summary>
		/// Delete all records by queryParameterId
		/// </summary>
		public static void DestroyByQueryParameterId(int queryParameterId)
		{
			Query query = QueryParameterJumpstationGroupType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(QueryParameterIdColumn.ColumnName, queryParameterId);
			QueryParameterJumpstationGroupTypeController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified QueryParameterJumpstationGroupType record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int queryParameterId, int jumpstationGroupTypeId)
		{
			Query query = QueryParameterJumpstationGroupType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(QueryParameterIdColumn.ColumnName, queryParameterId);
			query = query.WHERE(JumpstationGroupTypeIdColumn.ColumnName, jumpstationGroupTypeId);
			QueryParameterJumpstationGroupTypeController.DestroyByQuery(query);
        }

        #endregion

        #region IsDataModificationAllowed Method

        /// <summary>
        /// Determines whether a user can update QueryParameterJumpstationGroupType record if there are no Jumpstations using the record.
        /// </summary>
        /// <returns><c>true</c> if modification is allowed.</returns>
        public bool IsDataModificationAllowed()
        {
            const string sql = "SELECT COUNT(*) FROM (SELECT TOP (1) JumpstationGroup.Id FROM JumpstationGroup INNER JOIN JumpstationGroupSelector (NOLOCK) ON JumpstationGroup.Id = JumpstationGroupSelector.JumpstationGroupId " +
                               "INNER JOIN JumpstationGroupSelector_QueryParameterValue (NOLOCK) ON JumpstationGroupSelector.Id = JumpstationGroupSelector_QueryParameterValue.JumpstationGroupSelectorId " +
                               "INNER JOIN QueryParameterValue (NOLOCK) ON JumpstationGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id " +
                               "WHERE JumpstationGroup.JumpstationGroupTypeId = @JumpstationGroupTypeId AND QueryParameterValue.QueryParameterId = @QueryParameterId) aa";
            int queryValueFound = SqlUtility.ExecuteAsScalar<int>(
                SqlUtility.CreateSqlCommandForSql(
                    ElementsCPSSqlUtility.CreateDefaultConnection(),
                    sql,
                    SqlUtility.CreateSqlParameter("@JumpstationGroupTypeId", SqlDbType.Int, this.JumpstationGroupTypeId),
                    SqlUtility.CreateSqlParameter("@QueryParameterId", SqlDbType.Int, this.QueryParameterId)));
            return (queryValueFound == 0);
        }

        #endregion
	}
}
