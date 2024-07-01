using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapWorkflowModuleCollection class.
	/// </summary>
	public partial class VwMapWorkflowModuleCollection
	{

		/// <summary>
		/// Gets the <see cref="WorkflowModule"/>s corresponding to the items in this <see cref="VwMapWorkflowModuleCollection"/>.
		/// </summary>
		/// <returns></returns>
		public WorkflowModuleCollection GetWorkflowModules()
		{
			return WorkflowModuleController.FetchByIDs(this.GetWorkflowModuleIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="WorkflowModule"/>s corresponding to the items in this <see cref="VwMapWorkflowModuleCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetWorkflowModuleIds()
		{
			List<int> workflowModuleIds = new List<int>(this.Count);
			foreach (VwMapWorkflowModule vwMapWorkflowModule in this)
			{
				workflowModuleIds.Add(vwMapWorkflowModule.Id);
			}
			return workflowModuleIds;
		}

	}
}
