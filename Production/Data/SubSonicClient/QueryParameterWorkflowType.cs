using System.Data;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Utility;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the QueryParameterWorkflowType table.
	/// </summary>
	public partial class QueryParameterWorkflowType
	{
        /// <summary>
        /// Deletes a specified QueryParameterWorkflowType record (whether the table supports logical/soft deletes or not).
        /// </summary>
        public void Destroy()
        {
            Query query = QueryParameterWorkflowType.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(IdColumn.ColumnName, this.Id);
            QueryParameterWorkflowTypeController.DestroyByQuery(query);
        }
        
        /// <summary>
		/// Delete all records by queryParameterId
		/// </summary>
		public static void DestroyByQueryParameterId(int queryParameterId)
		{
			Query query = QueryParameterWorkflowType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(QueryParameterIdColumn.ColumnName, queryParameterId);
			QueryParameterWorkflowTypeController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified QueryParameterWorkflowType record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int queryParameterId, int proxyURLTypeId)
		{
			Query query = QueryParameterWorkflowType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(QueryParameterIdColumn.ColumnName, queryParameterId);
			query = query.WHERE(WorkflowTypeIdColumn.ColumnName, proxyURLTypeId);
			QueryParameterWorkflowTypeController.DestroyByQuery(query);
		}

        #region IsDataModificationAllowed Method

        /// <summary>
        /// Determines whether a user can update QueryParameterWorkflowType record if there are no Workflows using the record.
        /// </summary>
        /// <returns><c>true</c> if modification is allowed.</returns>
        public bool IsDataModificationAllowed()
        {
            const string sql = "SELECT COUNT(*) FROM (SELECT TOP (1) Workflow.Id FROM Workflow INNER JOIN WorkflowSelector (NOLOCK) ON Workflow.Id = WorkflowSelector.WorkflowId " +
                               "INNER JOIN WorkflowSelector_QueryParameterValue (NOLOCK) ON WorkflowSelector.Id = WorkflowSelector_QueryParameterValue.WorkflowSelectorId " +
                               "INNER JOIN QueryParameterValue (NOLOCK) ON WorkflowSelector_QueryParameterValue.QueryParameterValueId = QueryParameterValue.Id " +
                               "WHERE Workflow.WorkflowTypeId = @WorkflowTypeId AND QueryParameterValue.QueryParameterId = @QueryParameterId) aa";
            int queryValueFound = SqlUtility.ExecuteAsScalar<int>(
                SqlUtility.CreateSqlCommandForSql(
                    ElementsCPSSqlUtility.CreateDefaultConnection(),
                    sql,
                    SqlUtility.CreateSqlParameter("@WorkflowTypeId", SqlDbType.Int, this.WorkflowTypeId),
                    SqlUtility.CreateSqlParameter("@QueryParameterId", SqlDbType.Int, this.QueryParameterId)));
            return (queryValueFound == 0);
        }

        #endregion

    }
}
