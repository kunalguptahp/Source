using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the ProxyURL_QueryParameterValue table.
	/// </summary>
	public partial class ProxyURLQueryParameterValue
	{
		/// <summary>
		/// Deletes a specified ProxyURLQueryParameterValue records by ProxyURLId (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void DestroyByProxyURLId(int proxyURLId)
		{
			Query query = ProxyURLQueryParameterValue.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ProxyURLIdColumn.ColumnName, proxyURLId);
			ProxyURLQueryParameterValueController.DestroyByQuery(query);
		}
	}
}
