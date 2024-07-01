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
    /// Controller class for QueryParameter_ConfigurationServiceGroupType
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class QueryParameterConfigurationServiceGroupTypeController : BaseActiveRecordController<QueryParameterConfigurationServiceGroupType, QueryParameterConfigurationServiceGroupTypeCollection, QueryParameterConfigurationServiceGroupTypeController>
    {
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return QueryParameterConfigurationServiceGroupType.Schema;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		static QueryParameterConfigurationServiceGroupTypeController()
		{
			// Preload our schema..
			QueryParameterConfigurationServiceGroupType thisSchemaLoad = new QueryParameterConfigurationServiceGroupType(false);
		}
		#region ObjectDataSource support
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static bool Delete(int Id)
		{
			return (ActiveRecord<QueryParameterConfigurationServiceGroupType>.Delete(Id) == 1);
		}
    	
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
	}
}
