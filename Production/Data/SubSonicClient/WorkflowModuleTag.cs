using HP.ElementsCPS.Core.Security;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the WorkflowTag class.
	/// </summary>
	public partial class WorkflowModuleTag
	{
		/// <summary>
		/// Delete all records by WorkflowModuleId
		/// </summary>
		/// <param name="WorkflowModuleId"></param>
		public static void DestroyByWorkflowModuleId(int WorkflowModuleId)
		{
			Query query = WorkflowModuleTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(WorkflowModuleIdColumn.ColumnName, WorkflowModuleId);
			WorkflowModuleTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Deletes a specified <see cref="WorkflowModuleTag"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		/// <param name="WorkflowModuleId"></param>
		/// <param name="tagId"></param>
		public static void Destroy(int WorkflowModuleId, int tagId)
		{
			Query query = WorkflowModuleTag.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(WorkflowModuleIdColumn.ColumnName, WorkflowModuleId);
			query = query.WHERE(TagIdColumn.ColumnName, tagId);
			WorkflowModuleTagController.DestroyByQuery(query);
		}

		/// <summary>
		/// Creates and returns a new <see cref="WorkflowModuleTag"/> instance with the properties.
		/// </summary>
		/// <param name="WorkflowModuleId"></param>
		/// <param name="tagId"></param>
		/// <returns>An <see cref="WorkflowModuleTag"/> instance with the specified properties.</returns>
		public static WorkflowModuleTag Insert(int WorkflowModuleId, int tagId)
		{
			WorkflowModuleTag WorkflowModuleTag = new WorkflowModuleTag(true);
			WorkflowModuleTag.WorkflowModuleId = WorkflowModuleId;
			WorkflowModuleTag.TagId = tagId;
			WorkflowModuleTag.Save(SecurityManager.CurrentUserIdentityName);
			return WorkflowModuleTag;
		}
	}
}
