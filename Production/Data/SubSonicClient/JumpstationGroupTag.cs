using HP.ElementsCPS.Core.Security;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the JumpstationGroupTag class.
	/// </summary>
	public partial class JumpstationGroupTag
	{
		/// <summary>
		/// Delete all records by JumpstationGroupId
		/// </summary>
		public static void DestroyByJumpstationGroupId(int JumpstationGroupId)
		{
			Query query = JumpstationGroupTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(JumpstationGroupIdColumn.ColumnName, JumpstationGroupId);
			JumpstationGroupTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified <see cref="JumpstationGroupTag"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int JumpstationGroupId, int tagId)
		{
			Query query = JumpstationGroupTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(JumpstationGroupIdColumn.ColumnName, JumpstationGroupId);
			query = query.WHERE(TagIdColumn.ColumnName, tagId);
			JumpstationGroupTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Creates and returns a new <see cref="JumpstationGroupTag"/> instance with the properties.
		/// </summary>
		/// <param name="JumpstationGroupId"></param>
		/// <param name="tagId"></param>
		/// <returns>An <see cref="JumpstationGroupTag"/> instance with the specified properties.</returns>
		public static JumpstationGroupTag Insert(int JumpstationGroupId, int tagId)
		{
			JumpstationGroupTag JumpstationGroupTag = new JumpstationGroupTag(true);
			JumpstationGroupTag.JumpstationGroupId = JumpstationGroupId;
			JumpstationGroupTag.TagId = tagId;
			JumpstationGroupTag.Save(SecurityManager.CurrentUserIdentityName);
			return JumpstationGroupTag;
		}
	}
}
