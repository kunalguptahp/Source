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
	public partial class QueryParameter
	{
		/// <summary>
		/// Enumerates the primary keys of the records in the QueryParameter table.
		/// </summary>
		/// <remarks>
		/// NOTE: The IDs in the QueryParameter table MUST match the values defined in this enumeration.
		/// </remarks>
		public enum QueryParameterId
		{
			Touchpoint = 13,
			Brand = 15,
			Locale = 3,
			Cycle = 7,
			Platform = 14,
			PartnerCategory = 5,

			Language = 100,
			Country = 101,
			Release = 102,
			OSVersion = 103,
			OSBit = 104,
			OSType = 105,
			PCBrand = 106,
			PCPlatform = 107,
            PCSubBrand = 201,
            ReleaseStart = 202,
            ReleaseEnd = 203,
            ModelNumber = 204,
            ProductName = 205,
            ProductNumber = 206
		}

		#region ToString override

		public override string ToString()
		{
			return Format(this);
		}

		/// <summary>
		/// Returns a user-friendly string representation of a Query Parameter.
		/// </summary>
		/// <param name="queryParameter">The <see cref="QueryParameter"/> to format.</param>
		/// <returns></returns>
		private static string Format(QueryParameter queryParameter)
		{
			return string.Format(CultureInfo.CurrentCulture, "QueryParameter #{0} <{1}>", queryParameter.Id, queryParameter.Name);
		}

		#endregion

		#region ProxyURLType-related convenience members

		public HP.ElementsCPS.Data.SubSonicClient.ProxyURLTypeCollection GetProxyURLTypeCollection() { return QueryParameterController.GetProxyURLTypeCollection(this.Id); }

		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupTypeCollection GetConfigurationServiceGroupTypeCollection() { return QueryParameterController.GetConfigurationServiceGroupTypeCollection(this.Id); }

		public HP.ElementsCPS.Data.SubSonicClient.JumpstationGroupTypeCollection GetJumpstationGroupTypeCollection() { return QueryParameterController.GetJumpstationGroupTypeCollection(this.Id); }

        public HP.ElementsCPS.Data.SubSonicClient.WorkflowTypeCollection GetWorkflowTypeCollection() { return QueryParameterController.GetWorkflowTypeCollection(this.Id); }

		/// <summary>
		/// Returns the IDs of the ProxyURLType possessed by this instance.
		/// </summary>
		/// <returns></returns>
		public List<int> GetProxyURLType()
		{
			return this.GetProxyURLTypeCollection().GetIds();
		}

		/// <summary>
		/// Replaces all ProxyURLTypes currently associated with this query parameter with the specified set of new ProxyURLTypes.
		/// </summary>
		public void SetProxyURLTypes(List<int> newProxyURLTypeIds)
		{
			//determine which ProxyURLTypes need to be removed and which ones need to be added
			List<int> oldProxyURLTypeIds = this.GetProxyURLType();
			IEnumerable<int> enumRemovedProxyURLTypes = oldProxyURLTypeIds.Except(newProxyURLTypeIds);
			IEnumerable<int> enumAddedProxyURLTypes = newProxyURLTypeIds.Except(oldProxyURLTypeIds);

			//delete the removed ProxyURLTypes
			foreach (int proxyURLTypeId in enumRemovedProxyURLTypes)
			{
				this.RemoveProxyURLType(proxyURLTypeId);
			}

			//insert the added ProxyURLTypes
			foreach (int proxyURLTypeId in enumAddedProxyURLTypes)
			{
				this.AddProxyURLType(proxyURLTypeId);
			}
		}

		/// <summary>
		/// Deletes/removes a specific ProxyURLType for all query parameters
		/// </summary>
		/// <param name="proxyURLTypeId"></param>
		public void RemoveProxyURLType(int proxyURLTypeId)
		{
			QueryParameterProxyURLType.Destroy(this.Id, proxyURLTypeId);
		}

		/// <summary>
		/// Adds/inserts a specific ProxyURLType for each query parameter
		/// </summary>
		/// <param name="proxyURLTypeId"></param>
		public void AddProxyURLType(int proxyURLTypeId)
		{
			QueryParameterProxyURLType newProxyURLType = new QueryParameterProxyURLType(true);
			newProxyURLType.QueryParameterId = this.Id;
			newProxyURLType.ProxyURLTypeId = (int)proxyURLTypeId;
			newProxyURLType.Save(SecurityManager.CurrentUserIdentityName);
		}

		/// <summary>
		/// Deletes/removes all ProxyURLTypes.
		/// </summary>
		public void ClearProxyURLTypes()
		{
			QueryParameterProxyURLType.DestroyByQueryParameterId(this.Id);
		}

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
		/// Deletes/removes a specific ConfigurationServiceGroupType for all query parameters.
		/// </summary>
		/// <param name="configurationServiceGroupTypeId"></param>
		public void RemoveConfigurationServiceGroupType(int configurationServiceGroupTypeId)
		{
			QueryParameterConfigurationServiceGroupType.Destroy(this.Id, configurationServiceGroupTypeId);
		}

		/// <summary>
		/// Adds/inserts a specific ProxyURLType for all query parameters.
		/// </summary>
		/// <param name="configurationServiceGroupTypeId"></param>
		public void AddConfigurationServiceGroupType(int configurationServiceGroupTypeId)
		{
			QueryParameterConfigurationServiceGroupType newConfigurationServiceGroupType = new QueryParameterConfigurationServiceGroupType(true);
			newConfigurationServiceGroupType.QueryParameterId = this.Id;
			newConfigurationServiceGroupType.ConfigurationServiceGroupTypeId = (int)configurationServiceGroupTypeId;
			newConfigurationServiceGroupType.Save(SecurityManager.CurrentUserIdentityName);
		}

		/// <summary>
		/// Deletes/removes all ConfigurationServiceGroupTypes.
		/// </summary>
		public void ClearConfigurationServiceGroupTypes()
		{
			QueryParameterConfigurationServiceGroupType.DestroyByQueryParameterId(this.Id);
		}

		/// <summary>
		/// Returns the IDs of the JumpstationGroupType possessed by this instance.
		/// </summary>
		/// <returns></returns>
		public List<int> GetJumpstationGroupType()
		{
		    return this.GetJumpstationGroupTypeCollection().GetIds();
		}

		/// <summary>
		/// Replaces all JumpstationGroupTypes currently associated with this query parameter with the specified set of new JumpstationGroupTypes.
		/// </summary>
		public void SetJumpstationGroupTypes(List<int> newJumpstationGroupTypeIds)
		{
		    //determine which JumpstationGroupTypes need to be removed and which ones need to be added
		    List<int> oldJumpstationGroupTypeIds = this.GetJumpstationGroupType();
		    IEnumerable<int> enumRemovedJumpstationGroupTypes = oldJumpstationGroupTypeIds.Except(newJumpstationGroupTypeIds);
		    IEnumerable<int> enumAddedJumpstationGroupTypes = newJumpstationGroupTypeIds.Except(oldJumpstationGroupTypeIds);

		    //delete the removed for all query parameters
		    foreach (int JumpstationGroupTypeId in enumRemovedJumpstationGroupTypes)
		    {
			   this.RemoveJumpstationGroupType(JumpstationGroupTypeId);
		    }

		    //insert the added for all query parameters
		    foreach (int JumpstationGroupTypeId in enumAddedJumpstationGroupTypes)
		    {
			   this.AddJumpstationGroupType(JumpstationGroupTypeId);
		    }
		}

		/// <summary>
		/// Deletes/removes a specific JumpstationGroupType for all query parameters.
		/// </summary>
		/// <param name="JumpstationGroupTypeId"></param>
		public void RemoveJumpstationGroupType(int jumpstationGroupTypeId)
		{
		    QueryParameterJumpstationGroupType.Destroy(this.Id, jumpstationGroupTypeId);
		}

		/// <summary>
		/// Adds/inserts a specific ProxyURLType for all query parameters.
		/// </summary>
		/// <param name="JumpstationGroupTypeId"></param>
		public void AddJumpstationGroupType(int jumpstationGroupTypeId)
		{
		    QueryParameterJumpstationGroupType newJumpstationGroupType = new QueryParameterJumpstationGroupType(true);
		    newJumpstationGroupType.QueryParameterId = this.Id;
		    newJumpstationGroupType.JumpstationGroupTypeId = (int)jumpstationGroupTypeId;
		    newJumpstationGroupType.Save(SecurityManager.CurrentUserIdentityName);
		}

		/// <summary>
		/// Deletes/removes all JumpstationGroupTypes.
		/// </summary>
		public void ClearJumpstationGroupTypes()
		{
		    QueryParameterJumpstationGroupType.DestroyByQueryParameterId(this.Id);
		}

        /// <summary>
        /// Returns the IDs of the WorkflowType possessed by this instance.
        /// </summary>
        /// <returns></returns>
        public List<int> GetWorkflowType()
        {
            return this.GetWorkflowTypeCollection().GetIds();
        }

        /// <summary>
        /// Replaces all WorkflowTypes currently associated with this query parameter with the specified set of new WorkflowTypes.
        /// </summary>
        public void SetWorkflowTypes(List<int> newWorkflowTypeIds)
        {
            //determine which WorkflowTypes need to be removed and which ones need to be added
            List<int> oldWorkflowTypeIds = this.GetWorkflowType();
            IEnumerable<int> enumRemovedWorkflowTypes = oldWorkflowTypeIds.Except(newWorkflowTypeIds);
            IEnumerable<int> enumAddedWorkflowTypes = newWorkflowTypeIds.Except(oldWorkflowTypeIds);

            //delete the removed for all query parameters
            foreach (int WorkflowTypeId in enumRemovedWorkflowTypes)
            {
                this.RemoveWorkflowType(WorkflowTypeId);
            }

            //insert the added for all query parameters
            foreach (int WorkflowTypeId in enumAddedWorkflowTypes)
            {
                this.AddWorkflowType(WorkflowTypeId);
            }
        }

        /// <summary>
        /// Deletes/removes a specific WorkflowType for all query parameters.
        /// </summary>
        /// <param name="WorkflowTypeId"></param>
        public void RemoveWorkflowType(int WorkflowTypeId)
        {
            QueryParameterWorkflowType.Destroy(this.Id, WorkflowTypeId);
        }

        /// <summary>
        /// Adds/inserts a specific ProxyURLType for all query parameters.
        /// </summary>
        /// <param name="WorkflowTypeId"></param>
        public void AddWorkflowType(int WorkflowTypeId)
        {
            QueryParameterWorkflowType newWorkflowType = new QueryParameterWorkflowType(true);
            newWorkflowType.QueryParameterId = this.Id;
            newWorkflowType.WorkflowTypeId = (int)WorkflowTypeId;
            newWorkflowType.Save(SecurityManager.CurrentUserIdentityName);
        }

        /// <summary>
        /// Deletes/removes all WorkflowTypes.
        /// </summary>
        public void ClearWorkflowTypes()
        {
            QueryParameterWorkflowType.DestroyByQueryParameterId(this.Id);
        }

		#endregion

	}
}
