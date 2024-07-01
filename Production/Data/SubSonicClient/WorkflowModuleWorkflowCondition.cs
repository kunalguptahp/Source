using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the WorkflowModule_WorkflowCondition table.
	/// </summary>
	public partial class WorkflowModuleWorkflowCondition
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Deletes a specified <see cref="WorkflowModuleWorkflowCondition"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void DestroyByWorkflowModuleId(int workflowModuleId)
		{
			Query query = WorkflowModuleWorkflowCondition.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(WorkflowModuleIdColumn.ColumnName, workflowModuleId);
			WorkflowModuleWorkflowConditionController.DestroyByQuery(query);
		}

		#endregion

	}
}
