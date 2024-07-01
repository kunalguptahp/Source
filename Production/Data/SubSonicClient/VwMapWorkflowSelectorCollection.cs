using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapWorkflowSelectorCollection class.
	/// </summary>
	public partial class VwMapWorkflowSelectorCollection
	{

		/// <summary>
		/// Gets the <see cref="WorkflowSelector"/>s corresponding to the items in this <see cref="VwMapWorkflowSelectorCollection"/>.
		/// </summary>
		/// <returns></returns>
		public WorkflowSelectorCollection GetWorkflowSelectors()
		{
			return WorkflowSelectorController.FetchByIDs(this.GetWorkflowSelectorIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="WorkflowSelector"/>s corresponding to the items in this <see cref="VwMapWorkflowSelectorCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetWorkflowSelectorIds()
		{
			List<int> WorkflowSelectorIds = new List<int>(this.Count);
			foreach (VwMapWorkflowSelector vwMapWorkflowSelector in this)
			{
				WorkflowSelectorIds.Add(vwMapWorkflowSelector.Id);
			}
			return WorkflowSelectorIds;
		}

	}
}
