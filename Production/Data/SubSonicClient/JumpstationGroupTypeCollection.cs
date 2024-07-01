using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the JumpstationGroupType Collection class.
	/// </summary>
	public partial class JumpstationGroupTypeCollection
	{
		public List<int> GetIds()
		{
			List<int> JumpstationGroupTypes = new List<int>(this.Count);
			foreach (JumpstationGroupType JumpstationGroupType in this)
			{
				JumpstationGroupTypes.Add(JumpstationGroupType.Id);
			}
			return JumpstationGroupTypes;
		}
	}
}
