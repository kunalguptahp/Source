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
    /// Controller class for ProxyURL_Tag
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ProxyURLTagController : BaseActiveRecordController<ProxyURLTag, ProxyURLTagCollection, ProxyURLTagController>
    {
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return ProxyURLTag.Schema;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		static ProxyURLTagController()
		{
			// Preload our schema..
			ProxyURLTag thisSchemaLoad = new ProxyURLTag(false);
		}
		#region ObjectDataSource support
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public bool Delete(int ProxyURLId,int TagId)
		{
			Query qry = new Query(this.GetRecordSchema());
			qry.QueryType = QueryType.Delete;
			qry.AddWhere("ProxyURLId", ProxyURLId).AND("TagId", TagId);
			qry.Execute();
			return (true);
		}
		
    	
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
	}
}
