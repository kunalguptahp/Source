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
namespace HP.ElementsCPS.Data.SubSonicClient{
	public partial class StoredProcedures{
		
		/// <summary>
		/// Creates an object wrapper for the debug_DDLChangeLog_GetAsHtml Procedure
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static StoredProcedure DebugDDLChangeLogGetAsHtml()
		{
			SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("debug_DDLChangeLog_GetAsHtml", DataService.GetInstance("ElementsCPSDB"), "");
			
			return sp;
		}
		
		/// <summary>
		/// Creates an object wrapper for the uspDeleteConfigurationServiceGroupSelectorQueryParameterValueByConfigurationServiceGroupSelectorIdQueryParameterId Procedure
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static StoredProcedure DeleteConfigurationServiceGroupSelectorQueryParameterValueByConfigurationServiceGroupSelectorIdQueryParameterId(int? configurationServiceGroupSelectorId, int? queryParameterId)
		{
			SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("uspDeleteConfigurationServiceGroupSelectorQueryParameterValueByConfigurationServiceGroupSelectorIdQueryParameterId", DataService.GetInstance("ElementsCPSDB"), "dbo");
			
			sp.Command.AddParameter("@configurationServiceGroupSelectorId", configurationServiceGroupSelectorId, DbType.Int32, 0, 10);
			
			sp.Command.AddParameter("@queryParameterId", queryParameterId, DbType.Int32, 0, 10);
			
			return sp;
		}
		
		/// <summary>
		/// Creates an object wrapper for the uspDeleteJumpstationGroupSelectorQueryParameterValueByJumpstationGroupSelectorIdQueryParameterId Procedure
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static StoredProcedure DeleteJumpstationGroupSelectorQueryParameterValueByJumpstationGroupSelectorIdQueryParameterId(int? JumpstationGroupSelectorId, int? queryParameterId)
		{
			SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("uspDeleteJumpstationGroupSelectorQueryParameterValueByJumpstationGroupSelectorIdQueryParameterId", DataService.GetInstance("ElementsCPSDB"), "dbo");
			
			sp.Command.AddParameter("@JumpstationGroupSelectorId", JumpstationGroupSelectorId, DbType.Int32, 0, 10);
			
			sp.Command.AddParameter("@queryParameterId", queryParameterId, DbType.Int32, 0, 10);
			
			return sp;
		}
		
		/// <summary>
		/// Creates an object wrapper for the uspDeleteWorkflowSelectorQueryParameterValueByWorkflowSelectorIdQueryParameterId Procedure
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static StoredProcedure DeleteWorkflowSelectorQueryParameterValueByWorkflowSelectorIdQueryParameterId(int? WorkflowSelectorId, int? queryParameterId)
		{
			SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("uspDeleteWorkflowSelectorQueryParameterValueByWorkflowSelectorIdQueryParameterId", DataService.GetInstance("ElementsCPSDB"), "dbo");
			
			sp.Command.AddParameter("@WorkflowSelectorId", WorkflowSelectorId, DbType.Int32, 0, 10);
			
			sp.Command.AddParameter("@queryParameterId", queryParameterId, DbType.Int32, 0, 10);
			
			return sp;
		}
		
		/// <summary>
		/// Creates an object wrapper for the uspGetJumpstationByQueryParameterValue Procedure
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static StoredProcedure GetJumpstationByQueryParameterValue(int? isCountQuery, int? rowCount, int? startRow, int? rowsPerPage, int? statusId, int? jumpstationTypeId, string queryParameterValueIdDelimitedList)
		{
			SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("uspGetJumpstationByQueryParameterValue", DataService.GetInstance("ElementsCPSDB"), "dbo");
			
			sp.Command.AddParameter("@isCountQuery", isCountQuery, DbType.Int32, 0, 10);
			
			sp.Command.AddOutputParameter("@rowCount", DbType.Int32, 0, 10);
			
			sp.Command.AddParameter("@startRow", startRow, DbType.Int32, 0, 10);
			
			sp.Command.AddParameter("@rowsPerPage", rowsPerPage, DbType.Int32, 0, 10);
			
			sp.Command.AddParameter("@statusId", statusId, DbType.Int32, 0, 10);
			
			sp.Command.AddParameter("@jumpstationTypeId", jumpstationTypeId, DbType.Int32, 0, 10);
			
			sp.Command.AddParameter("@queryParameterValueIdDelimitedList", queryParameterValueIdDelimitedList, DbType.AnsiString, null, null);
			
			return sp;
		}
		
	}
	
}
