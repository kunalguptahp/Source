using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapConfigurationServiceGroupCollection class.
	/// </summary>
	public partial class VwMapConfigurationServiceGroupCollection
	{

		/// <summary>
		/// Gets the <see cref="ConfigurationServiceGroup"/>s corresponding to the items in this <see cref="VwMapConfigurationServiceGroupCollection"/>.
		/// </summary>
		/// <returns></returns>
		public ConfigurationServiceGroupCollection GetConfigurationServiceGroups()
		{
			return ConfigurationServiceGroupController.FetchByIDs(this.GetConfigurationServiceGroupIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="ConfigurationServiceGroup"/>s corresponding to the items in this <see cref="VwMapConfigurationServiceGroupCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetConfigurationServiceGroupIds()
		{
			List<int> ConfigurationServiceGroupIds = new List<int>(this.Count);
			foreach (VwMapConfigurationServiceGroup vwMapConfigurationServiceGroup in this)
			{
				ConfigurationServiceGroupIds.Add(vwMapConfigurationServiceGroup.Id);
			}
			return ConfigurationServiceGroupIds;
		}

	}
}
