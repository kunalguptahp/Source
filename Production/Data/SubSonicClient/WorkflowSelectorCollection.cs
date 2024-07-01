using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the WorkflowSelector Collection class.
	/// </summary>
	public partial class WorkflowSelectorCollection
	{
		public List<int> GetIds()
		{
			List<int> WorkflowSelectors = new List<int>(this.Count);
			foreach (WorkflowSelector WorkflowSelector in this)
			{
				WorkflowSelectors.Add(WorkflowSelector.Id);
			}
			return WorkflowSelectors;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="WorkflowSelector"/>s in this collection have no query parameter values.
		/// <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsChildlessQueryParameterValue()
		{
			foreach (WorkflowSelector WorkflowSelector in this)
			{
				Query qry = new Query(WorkflowSelectorQueryParameterValue.Schema);
				qry.AddWhere(WorkflowSelectorQueryParameterValue.Columns.WorkflowSelectorId, WorkflowSelector.Id);
				if (qry.GetCount("CreatedBy") > 0)
				{
					return false;
				}
			}
			return true;
		}

		#endregion

	}
}