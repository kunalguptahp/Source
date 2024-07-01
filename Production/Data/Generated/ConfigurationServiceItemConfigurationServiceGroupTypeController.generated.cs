using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
using HP.HPFx.Utility;
namespace HP.ElementsCPS.Data.SubSonicClient
{
    /// <summary>
    /// Controller class for ConfigurationServiceItem_ConfigurationServiceGroupType
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ConfigurationServiceItemConfigurationServiceGroupTypeController : BaseActiveRecordController<ConfigurationServiceItemConfigurationServiceGroupType, ConfigurationServiceItemConfigurationServiceGroupTypeCollection, ConfigurationServiceItemConfigurationServiceGroupTypeController>
    {
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return ConfigurationServiceItemConfigurationServiceGroupType.Schema;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		static ConfigurationServiceItemConfigurationServiceGroupTypeController()
		{
			// Preload our schema..
			ConfigurationServiceItemConfigurationServiceGroupType thisSchemaLoad = new ConfigurationServiceItemConfigurationServiceGroupType(false);
		}
		#region ObjectDataSource support
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static bool Delete(int Id)
		{
			return (ActiveRecord<ConfigurationServiceItemConfigurationServiceGroupType>.Delete(Id) == 1);
		}
    	
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
	}
}