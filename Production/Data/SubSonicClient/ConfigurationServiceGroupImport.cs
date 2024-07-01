using System;
using System.Globalization;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Diagnostics.Logging;
using SubSonic;
using HP.ElementsCPS.Data.Utility;
using System.Collections.Specialized;
using System.Collections.Generic;
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceGroupImport table.
	/// </summary>
	public partial class ConfigurationServiceGroupImport
	{

		#region Log Methods

		public void Log(Severity severity, string message)
		{
			int? ConfigurationServiceGroupImportId = (this.IsNew ? null : (int?)this.Id);
			Log(ConfigurationServiceGroupImportId, severity, this, message);
		}

		public void Log(Severity severity, string message, Exception ex)
		{
			int? ConfigurationServiceGroupImportId = (this.IsNew ? null : (int?)this.Id);
			Log(ConfigurationServiceGroupImportId, severity, this, message, ex);
		}

		internal static void Log(int? ConfigurationServiceGroupImportId, Severity severity, object source, string message)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroupImport History: #{0}: {1}.", ConfigurationServiceGroupImportId, message);
			LogManager.Current.Log(severity, source, logMessage);
		}

		internal static void Log(int? ConfigurationServiceGroupImportId, Severity severity, object source, string message, Exception ex)
		{
			string logMessage = string.Format(CultureInfo.CurrentCulture, "ConfigurationServiceGroupImport History: #{0}: {1}.", ConfigurationServiceGroupImportId, message);
			LogManager.Current.Log(severity, source, logMessage, ex);
		}

		#endregion

		#region db Methods

        /// <summary>
        /// Import the ConfigurationServiceGroupImport to ConfigurationServiceGroup
        /// </summary>
        public void Import()
        {
            using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
            {
                ConfigurationServiceGroup configurationServiceGroup = new ConfigurationServiceGroup(true);
                configurationServiceGroup.Name = this.Name;
                configurationServiceGroup.Description = this.Description;

                VwMapConfigurationServiceApplication configurationServiceApplication = VwMapConfigurationServiceApplicationController.FetchByElementsKey(this.ConfigurationServiceApplicationName);
                if (configurationServiceApplication != null)
                    configurationServiceGroup.ConfigurationServiceApplicationId = configurationServiceApplication.Id;

                VwMapConfigurationServiceGroupType configurationServiceGroupType = VwMapConfigurationServiceGroupTypeController.FetchByElementsKeyGroupTypeName(this.ConfigurationServiceApplicationName, this.ConfigurationServiceGroupTypeName);
                if (configurationServiceGroupType != null)
                    configurationServiceGroup.ConfigurationServiceGroupTypeId = configurationServiceGroupType.Id;

                //assign the current user as the Owner of the newConfigurationServiceGroup
                configurationServiceGroup.OwnerId = PersonController.GetCurrentUser().Id;

                // add it to configurationServiceGroup
                configurationServiceGroup.ConfigurationServiceGroupState = ConfigurationServiceGroupStateId.Modified;
                configurationServiceGroup.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                //import Configuration Service Label Value
                foreach (VwMapConfigurationServiceLabelConfigurationServiceGroupType configurationServiceLabelConfigurationServiceGroupType in VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController.FetchByConfigurationServiceGroupTypeId(configurationServiceGroup.ConfigurationServiceGroupTypeId))
                {
                    ConfigurationServiceLabelValue labelValue = new ConfigurationServiceLabelValue();
                    labelValue.ConfigurationServiceGroupId = configurationServiceGroup.Id;
                    labelValue.ConfigurationServiceLabelId = configurationServiceLabelConfigurationServiceGroupType.Id;
                    labelValue.ValueX = "n/a";
                    foreach (ConfigurationServiceLabelValueImport configurationServiceLabelValueImport in this.ConfigurationServiceLabelValueImportRecords())
                    {
                        if ((configurationServiceLabelConfigurationServiceGroupType.ConfigurationServiceItemElementsKey == configurationServiceLabelValueImport.ItemName) &&
                            (configurationServiceLabelConfigurationServiceGroupType.ElementsKey == configurationServiceLabelValueImport.LabelName))
                        {
                            labelValue.ValueX = configurationServiceLabelValueImport.LabelValue;
                        }
                    }
                    labelValue.RowStatusId = (int)RowStatus.RowStatusId.Active;
                    labelValue.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                }

                //import Query Parameter Value
                NameValueCollection importQueryParameterValueCollection = new NameValueCollection();
                foreach (ConfigurationServiceQueryParameterValueImport configurationServiceQueryParameterValueImport in this.ConfigurationServiceQueryParameterValueImportRecords())
                {
                    importQueryParameterValueCollection.Add(configurationServiceQueryParameterValueImport.QueryParameterName, configurationServiceQueryParameterValueImport.QueryParameterValue);
                }

                if (importQueryParameterValueCollection.Count > 0)
                {
                    //query parameter values to store
                    bool createGroupSelector = true;
                    List<int> queryParameterValueList = new List<int>();
                    foreach (VwMapQueryParameterConfigurationServiceGroupType queryParameterConfigurationServiceGroupType in VwMapQueryParameterConfigurationServiceGroupTypeController.FetchByConfigurationServiceGroupTypeId(configurationServiceGroup.ConfigurationServiceGroupTypeId, (int)RowStatus.RowStatusId.Active))
                    {
                        bool isWildcardAllowed = queryParameterConfigurationServiceGroupType.Wildcard;
                        int maximumQueryParameterValue = queryParameterConfigurationServiceGroupType.MaximumSelection;

                        int queryParameterValueAdded = 0;
                        foreach (string queryParameterValue in importQueryParameterValueCollection.GetValues(queryParameterConfigurationServiceGroupType.ElementsKey))
                        {
                            // check to see how many query parameters can be defined (0 = unlimited)
                            if ((maximumQueryParameterValue == 0) || (queryParameterValueAdded < maximumQueryParameterValue))
                            {
                                // only allow wildcard if query parameter allows it
                                if ((queryParameterValue != "*") || (isWildcardAllowed))
                                {
                                    VwMapQueryParameterValueCollection queryParameterValueCollection = VwMapQueryParameterValueController.FetchByQueryParameterIdName(queryParameterConfigurationServiceGroupType.QueryParameterId, queryParameterValue);
                                    // just assign the first one if the are multiple
                                    if (queryParameterValueCollection.Count > 0)
                                    {
                                        // no duplicate
                                        if (queryParameterValueList.Find(item => item == queryParameterValueCollection[0].Id) != queryParameterValueCollection[0].Id)
                                        {
                                            // add query parameter value to import list
                                            queryParameterValueList.Add(queryParameterValueCollection[0].Id);

                                            // only import up to maximum allowed
                                            queryParameterValueAdded = queryParameterValueAdded + 1;
                                        }
                                    }
                                }
                            }
                        }

                        if (queryParameterValueAdded == 0)
                        {
                            // each query parameter must have at least one query parameter value
                            this.ImportMessage = "Import did not create group selector because of insufficient or inaccurate data. \r\n" + this.ImportMessage;
                            createGroupSelector = false;
                            break;
                        }
                    }

                    // import the selector group and query parameter values.
                    if (createGroupSelector == true)
                    {
                        // create default selector group
                        ConfigurationServiceGroupSelector saveConfigurationServiceGroupSelector = new ConfigurationServiceGroupSelector(true);
                        saveConfigurationServiceGroupSelector.Name = "Import Selector";
                        saveConfigurationServiceGroupSelector.ConfigurationServiceGroupId = configurationServiceGroup.Id;
                        saveConfigurationServiceGroupSelector.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);

                        // create the query default selector group/parameter value map
                        foreach (int queryParameterValueId in queryParameterValueList)
                        {
                            //Save query parameter value for the group selector
                            ConfigurationServiceGroupSelectorQueryParameterValue saveQueryParameterValueItem = new ConfigurationServiceGroupSelectorQueryParameterValue(true);
                            saveQueryParameterValueItem.QueryParameterValueId = queryParameterValueId;
                            saveQueryParameterValueItem.ConfigurationServiceGroupSelectorId = saveConfigurationServiceGroupSelector.Id;
                            saveQueryParameterValueItem.Negation = false;
                            saveQueryParameterValueItem.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                        }
                    }
                }

                // update the status to imported
                this.ConfigurationServiceGroupId = configurationServiceGroup.Id;
                this.ImportStatus = "Imported";
                this.Save(Core.Security.SecurityManager.CurrentUserIdentity.Name);
                scope.Complete(); // transaction complete
            }
        }

		/// <summary>
		/// Delete the ConfigurationServiceGroupImport
		/// </summary>
		public void Delete()
		{
				try
				{
					this.ClearConfigurationServiceLabelValueImport();
                    this.ClearConfigurationServiceQueryParameterValueImport();
					this.Destroy();
				}
				catch (Exception ex)
				{
					string message = string.Format(CultureInfo.CurrentCulture, "Unable to delete ConfigurationServiceGroupImportId #{0}.", this.Id);
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
				}
		}

		/// <summary>
		/// Deletes a specified ConfigurationServiceGroupImport record (whether the table supports logical/soft deletes or not).
		/// </summary>
		public void Destroy()
		{
			Query query = ConfigurationServiceGroupImport.CreateQuery();
			query.QueryType = QueryType.Delete;
			query = query.WHERE(IdColumn.ColumnName, this.Id);
			ConfigurationServiceGroupImportController.DestroyByQuery(query);
		}

		#region ConfigurationServiceLabelValueImport-related convenience members

		/// <summary>
		/// Removes all associated configuration service Group label values
		/// </summary>
		public void ClearConfigurationServiceLabelValueImport()
		{
			ConfigurationServiceLabelValueImport.DestroyByConfigurationServiceGroupImportId(this.Id);
		}

		#endregion

        #region ConfigurationServiceQueryParameterValueImport-related convenience members

        /// <summary>
        /// Removes all associated query parameter values
        /// </summary>
        public void ClearConfigurationServiceQueryParameterValueImport()
        {
            ConfigurationServiceQueryParameterValueImport.DestroyByConfigurationServiceGroupImportId(this.Id);
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Convenience method.
        /// Returns the <see cref="ConfigurationServiceGroupImport"/>.
        /// </summary>
        /// <param name="importId"></param>
        public static ConfigurationServiceGroupImport FetchByImportId(string importId)
        {
            SqlQuery query = DB.Select().From(ConfigurationServiceGroupImport.Schema);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ImportId", importId);
            ConfigurationServiceGroupImport instance = query.ExecuteSingle<ConfigurationServiceGroupImport>();
            return instance;
        }

        #endregion

        #endregion

    }
}
