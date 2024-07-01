using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Services
{
    [ServiceContract]
    public interface IConfigurationServiceGroupImportService
    {
        [OperationContract(Name="Save")]
        int SubmitConfigurationServiceGroupImport(InputConfigurationServiceGroupImport csi);
    }
}

