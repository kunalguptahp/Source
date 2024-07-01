using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the JumpstationGroupSelector Collection class.
	/// </summary>
	public partial class JumpstationGroupSelectorCollection
	{
		public List<int> GetIds()
		{
			List<int> JumpstationGroupSelectors = new List<int>(this.Count);
			foreach (JumpstationGroupSelector JumpstationGroupSelector in this)
			{
				JumpstationGroupSelectors.Add(JumpstationGroupSelector.Id);
			}
			return JumpstationGroupSelectors;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="JumpstationGroupSelector"/>s in this collection have no query parameter values.
		/// <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsChildlessQueryParameterValue()
		{
			foreach (JumpstationGroupSelector JumpstationGroupSelector in this)
			{
				Query qry = new Query(JumpstationGroupSelectorQueryParameterValue.Schema);
				qry.AddWhere(JumpstationGroupSelectorQueryParameterValue.Columns.JumpstationGroupSelectorId, JumpstationGroupSelector.Id);
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