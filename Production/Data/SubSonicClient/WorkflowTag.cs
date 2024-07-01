using HP.ElementsCPS.Core.Security;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the WorkflowTag class.
	/// </summary>
	public partial class WorkflowTag
	{
		/// <summary>
		/// Delete all records by WorkflowId
		/// </summary>
		public static void DestroyByWorkflowId(int WorkflowId)
		{
			Query query = WorkflowTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(WorkflowIdColumn.ColumnName, WorkflowId);
			WorkflowTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified <see cref="WorkflowTag"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void Destroy(int WorkflowId, int tagId)
		{
			Query query = WorkflowTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(WorkflowIdColumn.ColumnName, WorkflowId);
			query = query.WHERE(TagIdColumn.ColumnName, tagId);
			WorkflowTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Creates and returns a new <see cref="WorkflowTag"/> instance with the properties.
		/// </summary>
		/// <param name="WorkflowId"></param>
		/// <param name="tagId"></param>
		/// <returns>An <see cref="WorkflowTag"/> instance with the specified properties.</returns>
		public static WorkflowTag Insert(int WorkflowId, int tagId)
		{
			WorkflowTag WorkflowTag = new WorkflowTag(true);
			WorkflowTag.WorkflowId = WorkflowId;
			WorkflowTag.TagId = tagId;
			WorkflowTag.Save(SecurityManager.CurrentUserIdentityName);
			return WorkflowTag;
		}
	}
}
