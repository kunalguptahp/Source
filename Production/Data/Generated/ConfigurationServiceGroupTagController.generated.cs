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
    /// Controller class for ConfigurationServiceGroup_Tag
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ConfigurationServiceGroupTagController : BaseActiveRecordController<ConfigurationServiceGroupTag, ConfigurationServiceGroupTagCollection, ConfigurationServiceGroupTagController>
    {
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return ConfigurationServiceGroupTag.Schema;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		static ConfigurationServiceGroupTagController()
		{
			// Preload our schema..
			ConfigurationServiceGroupTag thisSchemaLoad = new ConfigurationServiceGroupTag(false);
		}
		#region ObjectDataSource support
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public bool Delete(int ConfigurationServiceGroupId,int TagId)
		{
			Query qry = new Query(this.GetRecordSchema());
			qry.QueryType = QueryType.Delete;
			qry.AddWhere("ConfigurationServiceGroupId", ConfigurationServiceGroupId).AND("TagId", TagId);
			qry.Execute();
			return (true);
		}
		
    	
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
	}
}
