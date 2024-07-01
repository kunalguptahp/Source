using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;
using HP.ElementsCPS.Core.Security;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the Query Parameter class.
	/// </summary>
	public partial class ConfigurationServiceItem
	{

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of a Query Parameter.
		/// </summary>
		/// <param name="configurationServiceItem">The <see cref="ConfigurationServiceItem"/> to format.</param>
		/// <returns></returns>
		private static string Format(ConfigurationServiceItem configurationServiceItem)
		{
			return string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceItem #{0} <{1}>", configurationServiceItem.Id, configurationServiceItem.Name);
		}

		#endregion

        #region ConfigurationServiceGroupType-related convenience members

        public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupTypeCollection GetConfigurationServiceGroupTypeCollection() { return ConfigurationServiceItemController.GetConfigurationServiceGroupTypeCollection(this.Id); }

        /// <summary>
        /// Returns the IDs of the ConfigurationServiceGroupType possessed by this instance.
        /// </summary>
        /// <returns></returns>
        public List<int> GetConfigurationServiceGroupType()
        {
            return this.GetConfigurationServiceGroupTypeCollection().GetIds();
        }

        /// <summary>
        /// Replaces all ConfigurationServiceGroupTypes currently associated with this query parameter with the specified set of new ConfigurationServiceGroupTypes.
        /// </summary>
        public void SetConfigurationServiceGroupTypes(List<int> newConfigurationServiceGroupTypeIds)
        {
            //determine which ConfigurationServiceGroupTypes need to be removed and which ones need to be added
            List<int> oldConfigurationServiceGroupTypeIds = this.GetConfigurationServiceGroupType();
            IEnumerable<int> enumRemovedConfigurationServiceGroupTypes = oldConfigurationServiceGroupTypeIds.Except(newConfigurationServiceGroupTypeIds);
            IEnumerable<int> enumAddedConfigurationServiceGroupTypes = newConfigurationServiceGroupTypeIds.Except(oldConfigurationServiceGroupTypeIds);

            //delete the removed for all query parameters
            foreach (int configurationServiceGroupTypeId in enumRemovedConfigurationServiceGroupTypes)
            {
                this.RemoveConfigurationServiceGroupType(configurationServiceGroupTypeId);
            }

            //insert the added for all query parameters
            foreach (int ConfigurationServiceGroupTypeId in enumAddedConfigurationServiceGroupTypes)
            {
                this.AddConfigurationServiceGroupType(ConfigurationServiceGroupTypeId);
            }
        }

        /// <summary>
        /// Deletes/removes a specific ConfigurationServiceGroupType for item.
        /// </summary>
        /// <param name="configurationServiceGroupTypeId"></param>
        public void RemoveConfigurationServiceGroupType(int configurationServiceGroupTypeId)
        {
            ConfigurationServiceItemConfigurationServiceGroupType.Destroy(this.Id, configurationServiceGroupTypeId);
        }

        /// <summary>
        /// Adds/inserts a specific ConfigurationServiceGroupType for item.
        /// </summary>
        /// <param name="configurationServiceGroupTypeId"></param>
        public void AddConfigurationServiceGroupType(int configurationServiceGroupTypeId)
        {
            ConfigurationServiceItemConfigurationServiceGroupType newConfigurationServiceGroupType = new ConfigurationServiceItemConfigurationServiceGroupType(true);
            newConfigurationServiceGroupType.ConfigurationServiceItemId = this.Id;
            newConfigurationServiceGroupType.ConfigurationServiceGroupTypeId = (int)configurationServiceGroupTypeId;
            newConfigurationServiceGroupType.Save(SecurityManager.CurrentUserIdentityName);
        }

        /// <summary>
        /// Deletes/removes all ConfigurationServiceGroupTypes.
        /// </summary>
        public void ClearConfigurationServiceGroupTypes()
        {
            ConfigurationServiceItemConfigurationServiceGroupType.DestroyByConfigurationServiceItemId(this.Id);
        }
        #endregion
    }
}
