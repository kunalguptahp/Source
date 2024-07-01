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
    /// Controller class for Person_Role
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PersonRoleController : BaseActiveRecordController<PersonRole, PersonRoleCollection, PersonRoleController>
    {
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return PersonRole.Schema;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		static PersonRoleController()
		{
			// Preload our schema..
			PersonRole thisSchemaLoad = new PersonRole(false);
		}
		#region ObjectDataSource support
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public bool Delete(int PersonId,int RoleId)
		{
			Query qry = new Query(this.GetRecordSchema());
			qry.QueryType = QueryType.Delete;
			qry.AddWhere("PersonId", PersonId).AND("RoleId", RoleId);
			qry.Execute();
			return (true);
		}
		
    	
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
	}
}
