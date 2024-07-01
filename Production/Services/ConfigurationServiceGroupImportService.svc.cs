using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Diagnostics.Logging;
using HP.ElementsCPS.Core;
using HP.ElementsCPS.Core.Security;
using System.Transactions;
using HP.ElementsCPS.Data.SubSonicClient;
using SubSonic;
using System.Globalization;

namespace HP.ElementsCPS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, ConcurrencyMode = ConcurrencyMode.Single)]
    public class ConfigurationServiceGroupImportService : IConfigurationServiceGroupImportService
    {
        #region IConfigurationServiceGroupImportService Members
        
        public int SubmitConfigurationServiceGroupImport(InputConfigurationServiceGroupImport csi)
        {
            string ntAccount = "system\\system";

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    ConfigurationServiceGroupImport saveItem = null;
                    if (csi.ImportId != null)
                    {
                        saveItem = ConfigurationServiceGroupImport.FetchByImportId(csi.ImportId);
                    }
                    if (saveItem == null)
                        saveItem = new ConfigurationServiceGroupImport(true);

                    saveItem.Name = csi.Name;
                    saveItem.Description = csi.Description;
                    saveItem.ConfigurationServiceGroupTypeName = csi.GroupTypeName;
                    saveItem.ConfigurationServiceApplicationName = csi.ApplicationName;

                    switch (saveItem.ImportStatus)
                    {
                        case "Imported":
                            switch (csi.ImportStatus)
                            {
                                case "Added":
                                    saveItem.ImportStatus = "Readded";
                                    break;
                                    
                                default:
                                    saveItem.ImportStatus = csi.ImportStatus;
                                    break;
                            }
                            break;

                        default:
                            saveItem.ImportStatus = csi.ImportStatus;
                            break;
                    }
                    saveItem.ImportMessage = csi.ImportMessage;
                    saveItem.ImportId = csi.ImportId;

                    if (csi.PublicationId != null)
                    {
                        // fill in the configuration service group id
                        int pubId;
                        if (Int32.TryParse(csi.PublicationId, out pubId))
                        {
                            ConfigurationServiceGroup configurationServiceGroup = ConfigurationServiceGroupController.FetchByProductionId(pubId);
                            saveItem.ConfigurationServiceGroupId = configurationServiceGroup.Id;
                            saveItem.ProductionId = pubId;
                        }
                        else
                        {
                            saveItem.ConfigurationServiceGroupId = null;
                            saveItem.ProductionId = null;
                        }
                    }
                    
                    saveItem.RowStatusId = (int)RowStatus.RowStatusId.Active;
                    saveItem.Save(ntAccount);

                    if (csi.Items != null)
                    {
                        // clear any existing label values 
                        saveItem.ClearConfigurationServiceLabelValueImport();
                        foreach (var c in csi.Items)
                        {
                            ConfigurationServiceLabelValueImport saveItemChild = new ConfigurationServiceLabelValueImport(true);
                            saveItemChild.ItemName = c.ItemName;
                            saveItemChild.LabelName = c.LabelName;
                            saveItemChild.LabelValue = c.LabelValue;
                            saveItemChild.ConfigurationServiceGroupImportId = saveItem.Id;
                            saveItemChild.RowStatusId = (int)RowStatus.RowStatusId.Active;
                            saveItemChild.Save(ntAccount);
                        }
                    }

                    // insert QueryParameterValueImport items
                    if (csi.ParaValueItems != null)
                    {
                        // clear any existing query parameter values 
                        saveItem.ClearConfigurationServiceQueryParameterValueImport();
                        foreach (var c in csi.ParaValueItems)
                        {
                            ConfigurationServiceQueryParameterValueImport saveItemChild = new ConfigurationServiceQueryParameterValueImport(true);
                            saveItemChild.QueryParameterName = c.QueryParameterName;
                            saveItemChild.QueryParameterValue = c.QueryParameterValue;
                            saveItemChild.ConfigurationServiceGroupImportId = saveItem.Id;
                            saveItemChild.RowStatusId = (int)RowStatus.RowStatusId.Active;
                            saveItemChild.Save(ntAccount);
                        }
                    }
                    scope.Complete();
                    return 0;
                }
				catch (Exception ex)
				{
					string message = "Unable to add ConfigurationServiceGroupImport from wcf service.";
					LogManager.Current.Log(Severity.Error, this, message, ex);
					throw;
                }
            }
        }

        #endregion

    }
}

