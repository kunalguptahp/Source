using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapWorkflowCollection class.
	/// </summary>
	public partial class VwMapWorkflowCollection
	{
		/// <summary>
		/// Gets the <see cref="Workflow"/>s corresponding to the items in this <see cref="VwMapWorkflowCollection"/>.
		/// </summary>
		/// <returns></returns>
		public WorkflowCollection GetWorkflows()
		{
			return WorkflowController.FetchByIDs(this.GetWorkflowIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="Workflow"/>s corresponding to the items in this <see cref="VwMapWorkflowCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetWorkflowIds()
		{
			List<int> workflowIds = new List<int>(this.Count);
			foreach (VwMapWorkflow vwMapWorkflow in this)
			{
				workflowIds.Add(vwMapWorkflow.Id);
			}
			return workflowIds;
		}
	}
}
