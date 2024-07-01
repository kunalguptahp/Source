using System.Data;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Utility;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the QueryParameterConfigurationServiceGroupType table.
	/// </summary>
	public partial class QueryParameterConfigurationServiceGroupType
	{
        /// <summary>
        /// Deletes a specified QueryParameterConfigurationServiceGroupType record (whether the table supports logical/soft deletes or not).
        /// </summary>
        public void Destroy()
        {
            Query query = QueryParameterConfigurationServiceGroupType.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(IdColumn.ColumnName, this.Id);
            QueryParameterConfigurationServiceGroupTypeController.DestroyByQuery(query);
        }

		/// <summary>
		/// Delete all records by queryParameterId
		/// </summary>
		public static void DestroyByQueryParameterId(int queryParameterId)
		{
			Query query = QueryParameterConfigurationServiceGroupType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(QueryParameterIdColumn.ColumnName, queryParameterId);
			QueryParameterConfigurationServiceGroupTypeController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified QueryParameterConfigurationServiceGroupType record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int queryParameterId, int proxyURLTypeId)
		{
			Query query = QueryParameterConfigurationServiceGroupType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(QueryParameterIdColumn.ColumnName, queryParameterId);
			query = query.WHERE(ConfigurationServiceGroupTypeIdColumn.ColumnName, proxyURLTypeId);
			QueryParameterConfigurationServiceGroupTypeController.DestroyByQuery(query);
		}

        #region IsDataModificationAllowed Method

        /// <summary>
        /// Determines whether a user can update QueryParameterConfigurationServiceGroupType record if there are no Configuration Service Groups using the record.
        /// </summary>
        /// <returns><c>true</c> if modification is allowed.</returns>
        public bool IsDataModificationAllowed()
        {
            const string sql = "SELECT COUNT(*) FROM (SELECT TOP (1) ConfigurationServiceGroup.Id FROM ConfigurationServiceGroup INNER JOIN ConfigurationServiceGroupSelector (NOLOCK) ON ConfigurationServiceGroup.Id = ConfigurationServiceGroupSelector.ConfigurationServiceGroupId " +
                               "INNER JOIN ConfigurationServiceGroupSelector_QueryParameterValue (NOLOCK) ON ConfigurationServiceGroupSelector.Id = ConfigurationServiceGroupSelector_QueryParameterValue.ConfigurationServiceGroupSelectorId " +
                               "INNER JOIN QueryParameterValue (NOLOCK) ON ConfigurationServiceGroupSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id " +
                               "WHERE ConfigurationServiceGroup.ConfigurationServiceGroupTypeId = @ConfigurationServiceGroupTypeId AND QueryParameterValue.QueryParameterId = @QueryParameterId) aa";
            int queryValueFound = SqlUtility.ExecuteAsScalar<int>(
                SqlUtility.CreateSqlCommandForSql(
                    ElementsCPSSqlUtility.CreateDefaultConnection(),
                    sql,
                    SqlUtility.CreateSqlParameter("@ConfigurationServiceGroupTypeId", SqlDbType.Int, this.ConfigurationServiceGroupTypeId),
                    SqlUtility.CreateSqlParameter("@QueryParameterId", SqlDbType.Int, this.QueryParameterId)));
            return (queryValueFound == 0);
        }

        #endregion

    }
}
