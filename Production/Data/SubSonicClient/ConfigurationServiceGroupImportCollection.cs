using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceGroupImport Collection class.
	/// </summary>
	public partial class ConfigurationServiceGroupImportCollection
	{
		public List<int> GetIds()
		{
			List<int> ConfigurationServiceGroupImports = new List<int>(this.Count);
            foreach (ConfigurationServiceGroupImport ConfigurationServiceGroupImport in this)
			{
                ConfigurationServiceGroupImports.Add(ConfigurationServiceGroupImport.Id);
			}
            return ConfigurationServiceGroupImports;
		}
	}
}