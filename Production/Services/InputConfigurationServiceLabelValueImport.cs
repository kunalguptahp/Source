using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace HP.ElementsCPS.Services
{
    [DataContract]
    public class InputConfigurationServiceLabelValueImport
    {
        [DataMember(Order = 1)]
        public string ItemName 
        { get; set; }

        [DataMember(Order = 2)]
        public string LabelName 
        { get; set; }

        [DataMember(Order = 3)]
        public string LabelValue 
        { get; set; }

        //public InputConfigurationServiceLabelValueImport(){}

        //public InputConfigurationServiceLabelValueImport(int rowStatusID, string itemName,
        //    string labelName, string labelValue, string configurationServiceGroupImportID)
        //{
        //    ItemName = itemName;
        //    LabelName = labelName;
        //    LabelValue = labelValue;
        //}
    }
}
