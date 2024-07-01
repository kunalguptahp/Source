using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the QueryParameterProxyURLType table.
	/// </summary>
	public partial class QueryParameterProxyURLType
	{
        /// <summary>
        /// Deletes a specified QueryParameterProxyURLType record (whether the table supports logical/soft deletes or not).
        /// </summary>
        public void Destroy()
        {
            Query query = QueryParameterProxyURLType.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(IdColumn.ColumnName, this.Id);
            QueryParameterProxyURLTypeController.DestroyByQuery(query);
        }

		/// <summary>
		/// Delete all records by PersonId
		/// </summary>
		public static void DestroyByQueryParameterId(int queryParameterId)
		{
			Query query = QueryParameterProxyURLType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(QueryParameterIdColumn.ColumnName, queryParameterId);
			QueryParameterProxyURLTypeController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified QueryParameterProxyURLType record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int queryParameterId, int proxyURLTypeId)
		{
			Query query = QueryParameterProxyURLType.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(QueryParameterIdColumn.ColumnName, queryParameterId);
			query = query.WHERE(ProxyURLTypeIdColumn.ColumnName, proxyURLTypeId);
			QueryParameterProxyURLTypeController.DestroyByQuery(query);
		}
	}
}
