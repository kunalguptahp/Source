using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapConfigurationServiceGroupImportCollection class.
	/// </summary>
	public partial class VwMapConfigurationServiceGroupImportCollection
	{

		/// <summary>
		/// Gets the <see cref="ConfigurationServiceGroupImport"/>s corresponding to the items in this <see cref="VwMapConfigurationServiceGroupImportCollection"/>.
		/// </summary>
		/// <returns></returns>
		public ConfigurationServiceGroupImportCollection GetConfigurationServiceGroupImports()
		{
			return ConfigurationServiceGroupImportController.FetchByIDs(this.GetConfigurationServiceGroupImportIds());
		}

		/// <summary>
		/// Gets a list of the IDs of the <see cref="ConfigurationServiceGroupImport"/>s corresponding to the items in this <see cref="VwMapConfigurationServiceGroupImportCollection"/>.
		/// </summary>
		/// <returns></returns>
		public List<int> GetConfigurationServiceGroupImportIds()
		{
			List<int> ConfigurationServiceGroupImportIds = new List<int>(this.Count);
			foreach (VwMapConfigurationServiceGroupImport vwMapConfigurationServiceGroupImport in this)
			{
				ConfigurationServiceGroupImportIds.Add(vwMapConfigurationServiceGroupImport.Id);
			}
			return ConfigurationServiceGroupImportIds;
		}

	}
}
