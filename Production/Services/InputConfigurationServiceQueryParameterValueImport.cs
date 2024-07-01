using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace HP.ElementsCPS.Services
{
    [DataContract]
    public class InputConfigurationServiceQueryParameterValueImport
    {
        [DataMember]
        public string QueryParameterName
        { get; set; }

        [DataMember]
        public string QueryParameterValue
        { get; set; }
    }
}
