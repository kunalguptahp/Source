using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;
using HP.ElementsCPS.Data.SubSonicClient;

namespace HP.ElementsCPS.Services
{
    [DataContract]
    public class InputConfigurationServiceGroupImport
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Description { get; set; }

        [DataMember(Order = 3)]
        public string GroupTypeName { get; set; }

        [DataMember(Order = 4)]
        public string ApplicationName { get; set; }

        [DataMember(Order = 5)]
        public string ImportMessage { get; set; }

        [DataMember(Order = 6)]
        public string ImportStatus { get; set; }

        [DataMember(Order = 7)]
        public string ImportId { get; set; }

        [DataMember(Order = 8)]
        public string PublicationId { get; set; }

        [DataMember(Order = 9)]
        public List<InputConfigurationServiceLabelValueImport> Items { get; set; }

        [DataMember(Order = 10)]
        public List<InputConfigurationServiceQueryParameterValueImport> ParaValueItems { get; set; }

        public InputConfigurationServiceGroupImport() { }

        public InputConfigurationServiceGroupImport(string name,
           string description, string groupTypeName, string applicationName,
           string message, string status, string importId, string publicationId)
        {
            Name = name;
            Description = description;
            GroupTypeName = groupTypeName;
            ApplicationName = applicationName;
            ImportMessage = message;
            ImportStatus = status;
            ImportId = importId;
            PublicationId = publicationId;
        }
    }
}
