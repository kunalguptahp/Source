using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapJumpstationMacroCollection class.
	/// </summary>
	public partial class VwMapJumpstationMacroCollection
	{

		/// <summary>
		/// Gets the <see cref="JumpstationMacro"/>s corresponding to the items in this <see cref="VwMapJumpstationMacroCollection"/>.
		/// </summary>
		/// <returns></returns>
		public JumpstationMacroCollection GetJumpstationMacros()
		{
			return JumpstationMacroController.FetchByIDs(this.GetJumpstationMacroIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="JumpstationMacro"/>s corresponding to the items in this <see cref="VwMapJumpstationMacroCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetJumpstationMacroIds()
		{
			List<int> jumpstationMacroIds = new List<int>(this.Count);
			foreach (VwMapJumpstationMacro vwMapJumpstationMacro in this)
			{
				jumpstationMacroIds.Add(vwMapJumpstationMacro.Id);
			}
			return jumpstationMacroIds;
		}

	}
}
