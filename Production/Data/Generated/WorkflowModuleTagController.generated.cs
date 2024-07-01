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
    /// Controller class for WorkflowModule_Tag
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class WorkflowModuleTagController : BaseActiveRecordController<WorkflowModuleTag, WorkflowModuleTagCollection, WorkflowModuleTagController>
    {
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return WorkflowModuleTag.Schema;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		static WorkflowModuleTagController()
		{
			// Preload our schema..
			WorkflowModuleTag thisSchemaLoad = new WorkflowModuleTag(false);
		}
		#region ObjectDataSource support
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public bool Delete(int WorkflowModuleId,int TagId)
		{
			Query qry = new Query(this.GetRecordSchema());
			qry.QueryType = QueryType.Delete;
			qry.AddWhere("WorkflowModuleId", WorkflowModuleId).AND("TagId", TagId);
			qry.Execute();
			return (true);
		}
		
    	
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
	}
}
