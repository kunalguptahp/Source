using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the WorkflowType Collection class.
	/// </summary>
	public partial class WorkflowTypeCollection
	{
		public List<int> GetIds()
		{
			List<int> WorkflowTypes = new List<int>(this.Count);
			foreach (WorkflowType WorkflowType in this)
			{
				WorkflowTypes.Add(WorkflowType.Id);
			}
			return WorkflowTypes;
		}
	}
}
