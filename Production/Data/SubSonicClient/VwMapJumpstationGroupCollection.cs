using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapJumpstationGroupCollection class.
	/// </summary>
	public partial class VwMapJumpstationGroupCollection
	{

		/// <summary>
		/// Gets the <see cref="JumpstationGroup"/>s corresponding to the items in this <see cref="VwMapJumpstationGroupCollection"/>.
		/// </summary>
		/// <returns></returns>
		public JumpstationGroupCollection GetJumpstationGroups()
		{
			return JumpstationGroupController.FetchByIDs(this.GetJumpstationGroupIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="JumpstationGroup"/>s corresponding to the items in this <see cref="VwMapJumpstationGroupCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetJumpstationGroupIds()
		{
			List<int> JumpstationGroupIds = new List<int>(this.Count);
			foreach (VwMapJumpstationGroup vwMapJumpstationGroup in this)
			{
				JumpstationGroupIds.Add(vwMapJumpstationGroup.Id);
			}
			return JumpstationGroupIds;
		}

	}
}
