using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapConfigurationServiceGroupSelectorCollection class.
	/// </summary>
	public partial class VwMapConfigurationServiceGroupSelectorCollection
	{

		/// <summary>
		/// Gets the <see cref="ConfigurationServiceGroupSelector"/>s corresponding to the items in this <see cref="VwMapConfigurationServiceGroupSelectorCollection"/>.
		/// </summary>
		/// <returns></returns>
		public ConfigurationServiceGroupSelectorCollection GetConfigurationServiceGroupSelectors()
		{
			return ConfigurationServiceGroupSelectorController.FetchByIDs(this.GetConfigurationServiceGroupSelectorIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="ConfigurationServiceGroupSelector"/>s corresponding to the items in this <see cref="VwMapConfigurationServiceGroupSelectorCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetConfigurationServiceGroupSelectorIds()
		{
			List<int> ConfigurationServiceGroupSelectorIds = new List<int>(this.Count);
			foreach (VwMapConfigurationServiceGroupSelector vwMapConfigurationServiceGroupSelector in this)
			{
				ConfigurationServiceGroupSelectorIds.Add(vwMapConfigurationServiceGroupSelector.Id);
			}
			return ConfigurationServiceGroupSelectorIds;
		}

	}
}
