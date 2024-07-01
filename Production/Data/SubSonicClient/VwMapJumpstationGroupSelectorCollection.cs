using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapJumpstationGroupSelectorCollection class.
	/// </summary>
	public partial class VwMapJumpstationGroupSelectorCollection
	{

		/// <summary>
		/// Gets the <see cref="JumpstationGroupSelector"/>s corresponding to the items in this <see cref="VwMapJumpstationGroupSelectorCollection"/>.
		/// </summary>
		/// <returns></returns>
		public JumpstationGroupSelectorCollection GetJumpstationGroupSelectors()
		{
			return JumpstationGroupSelectorController.FetchByIDs(this.GetJumpstationGroupSelectorIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="JumpstationGroupSelector"/>s corresponding to the items in this <see cref="VwMapJumpstationGroupSelectorCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetJumpstationGroupSelectorIds()
		{
			List<int> JumpstationGroupSelectorIds = new List<int>(this.Count);
			foreach (VwMapJumpstationGroupSelector vwMapJumpstationGroupSelector in this)
			{
				JumpstationGroupSelectorIds.Add(vwMapJumpstationGroupSelector.Id);
			}
			return JumpstationGroupSelectorIds;
		}

	}
}
