using System;
using System.Collections.Generic;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceGroupSelector Collection class.
	/// </summary>
	public partial class ConfigurationServiceGroupSelectorCollection
	{
		public List<int> GetIds()
		{
			List<int> ConfigurationServiceGroupSelectors = new List<int>(this.Count);
			foreach (ConfigurationServiceGroupSelector ConfigurationServiceGroupSelector in this)
			{
				ConfigurationServiceGroupSelectors.Add(ConfigurationServiceGroupSelector.Id);
			}
			return ConfigurationServiceGroupSelectors;
		}

		#region Convenience "ForEach" Methods

		/// <summary>
		/// Returns <c>true</c> if for all of the <see cref="ConfigurationServiceGroupSelector"/>s in this collection have no query parameter values.
		/// <c>false</c> otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsChildlessQueryParameterValue()
		{
			foreach (ConfigurationServiceGroupSelector ConfigurationServiceGroupSelector in this)
			{
				Query qry = new Query(ConfigurationServiceGroupSelectorQueryParameterValue.Schema);
				qry.AddWhere(ConfigurationServiceGroupSelectorQueryParameterValue.Columns.ConfigurationServiceGroupSelectorId, ConfigurationServiceGroupSelector.Id);
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