using HP.ElementsCPS.Core.Security;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ProxyURLTag class.
	/// </summary>
	public partial class ProxyURLTag
	{
		/// <summary>
		/// Delete all records by ProxyURLId
		/// </summary>
		public static void DestroyByProxyURLId(int ProxyURLId)
		{
			Query query = ProxyURLTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ProxyURLIdColumn.ColumnName, ProxyURLId);
			ProxyURLTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified <see cref="ProxyURLTag"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int ProxyURLId, int tagId)
		{
			Query query = ProxyURLTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(ProxyURLIdColumn.ColumnName, ProxyURLId);
			query = query.WHERE(TagIdColumn.ColumnName, tagId);
			ProxyURLTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Creates and returns a new <see cref="ProxyURLTag"/> instance with the properties.
		/// </summary>
		/// <param name="ProxyURLId"></param>
		/// <param name="tagId"></param>
		/// <returns>An <see cref="ProxyURLTag"/> instance with the specified properties.</returns>
		public static ProxyURLTag Insert(int ProxyURLId, int tagId)
		{
			ProxyURLTag ProxyURLTag = new ProxyURLTag(true);
			ProxyURLTag.ProxyURLId = ProxyURLId;
			ProxyURLTag.TagId = tagId;
			ProxyURLTag.Save(SecurityManager.CurrentUserIdentityName);
			return ProxyURLTag;
		}
	}
}
