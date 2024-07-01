using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Workflow_WorkflowModule table.
	/// </summary>
	public partial class WorkflowWorkflowModule
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Deletes a specified <see cref="WorkflowWorkflowModule"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void DestroyByWorkflowId(int workflowId)
		{
			Query query = WorkflowWorkflowModule.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(WorkflowIdColumn.ColumnName, workflowId);
			WorkflowWorkflowModuleController.DestroyByQuery(query);
		}

		#endregion

	}
}
