using System.Globalization;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the WorkflowSelector_QueryParameterValue table.
	/// </summary>
	public partial class WorkflowSelectorQueryParameterValue
	{

		#region Deletion/Destruction Methods

		/// <summary>
		/// Delete all records by WorkflowSelectorId and QueryParameterId
		/// </summary>
		public static void DestroyByWorkflowSelectorIdQueryParameterId(int WorkflowSelectorId, int queryParameterId)
		{
			SubSonic.StoredProcedure sp = StoredProcedures.DeleteWorkflowSelectorQueryParameterValueByWorkflowSelectorIdQueryParameterId(WorkflowSelectorId, queryParameterId);
			sp.ExecuteScalar();
		}

		/// <summary>
		/// Deletes a specified <see cref="WorkflowSelectorQueryParameterValue"/> record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public static void DestroyByWorkflowSelectorId(int WorkflowSelectorId)
		{
			Query query = WorkflowSelectorQueryParameterValue.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(WorkflowSelectorIdColumn.ColumnName, WorkflowSelectorId);
			WorkflowSelectorQueryParameterValueController.DestroyByQuery(query);
		}

		#endregion

	}
}
