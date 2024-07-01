using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceGroupType Collection class.
	/// </summary>
	public partial class ConfigurationServiceGroupTypeCollection
	{
		public List<int> GetIds()
		{
			List<int> configurationServiceGroupTypes = new List<int>(this.Count);
			foreach (ConfigurationServiceGroupType configurationServiceGroupType in this)
			{
				configurationServiceGroupTypes.Add(configurationServiceGroupType.Id);
			}
			return configurationServiceGroupTypes;
		}
	}
}
