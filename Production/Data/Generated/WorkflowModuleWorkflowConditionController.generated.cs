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
    /// Controller class for WorkflowModule_WorkflowCondition
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class WorkflowModuleWorkflowConditionController : BaseActiveRecordController<WorkflowModuleWorkflowCondition, WorkflowModuleWorkflowConditionCollection, WorkflowModuleWorkflowConditionController>
    {
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return WorkflowModuleWorkflowCondition.Schema;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		static WorkflowModuleWorkflowConditionController()
		{
			// Preload our schema..
			WorkflowModuleWorkflowCondition thisSchemaLoad = new WorkflowModuleWorkflowCondition(false);
		}
		#region ObjectDataSource support
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static bool Delete(int Id)
		{
			return (ActiveRecord<WorkflowModuleWorkflowCondition>.Delete(Id) == 1);
		}
    	
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
	}
}
