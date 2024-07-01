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
namespace HP.ElementsCPS.Data.SubSonicClient
{
	#region Tables Struct
	public partial struct Tables
	{
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Aardvark = @"Aardvark";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string AppClient = @"AppClient";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Application = @"Application";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ApplicationRole = @"Application_Role";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceApplication = @"ConfigurationServiceApplication";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceApplicationType = @"ConfigurationServiceApplicationType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceGroup = @"ConfigurationServiceGroup";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceGroupTag = @"ConfigurationServiceGroup_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceGroupImport = @"ConfigurationServiceGroupImport";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceGroupSelector = @"ConfigurationServiceGroupSelector";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceGroupSelectorQueryParameterValue = @"ConfigurationServiceGroupSelector_QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceGroupStatus = @"ConfigurationServiceGroupStatus";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceGroupType = @"ConfigurationServiceGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceItem = @"ConfigurationServiceItem";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceItemConfigurationServiceGroupType = @"ConfigurationServiceItem_ConfigurationServiceGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceLabel = @"ConfigurationServiceLabel";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceLabelType = @"ConfigurationServiceLabelType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceLabelValue = @"ConfigurationServiceLabelValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceLabelValueImport = @"ConfigurationServiceLabelValueImport";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ConfigurationServiceQueryParameterValueImport = @"ConfigurationServiceQueryParameterValueImport";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string DDLChangeLog = @"DDLChangeLog";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string EntityType = @"EntityType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationApplication = @"JumpstationApplication";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationDomain = @"JumpstationDomain";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationGroup = @"JumpstationGroup";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationGroupTag = @"JumpstationGroup_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationGroupPivot = @"JumpstationGroupPivot";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationGroupSelector = @"JumpstationGroupSelector";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationGroupSelectorQueryParameterValue = @"JumpstationGroupSelector_QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationGroupStatus = @"JumpstationGroupStatus";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationGroupType = @"JumpstationGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationMacro = @"JumpstationMacro";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationMacroStatus = @"JumpstationMacroStatus";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string JumpstationMacroValue = @"JumpstationMacroValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Log = @"Log";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Note = @"Note";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string NoteType = @"NoteType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Person = @"Person";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string PersonRole = @"Person_Role";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ProxyURL = @"ProxyURL";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ProxyURLQueryParameterValue = @"ProxyURL_QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ProxyURLTag = @"ProxyURL_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ProxyURLDomain = @"ProxyURLDomain";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ProxyURLGroupType = @"ProxyURLGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ProxyURLStatus = @"ProxyURLStatus";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ProxyURLType = @"ProxyURLType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string PublishTemp = @"PublishTemp";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string QueryParameter = @"QueryParameter";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string QueryParameterConfigurationServiceGroupType = @"QueryParameter_ConfigurationServiceGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string QueryParameterJumpstationGroupType = @"QueryParameter_JumpstationGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string QueryParameterProxyURLType = @"QueryParameter_ProxyURLType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string QueryParameterWorkflowType = @"QueryParameter_WorkflowType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string QueryParameterValue = @"QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Role = @"Role";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string RowStatus = @"RowStatus";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Tag = @"Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Tenant = @"Tenant";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string TenantAppClient = @"Tenant_AppClient";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string Workflow = @"Workflow";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowTag = @"Workflow_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowWorkflowModule = @"Workflow_WorkflowModule";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowApplication = @"WorkflowApplication";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowApplicationType = @"WorkflowApplicationType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowCondition = @"WorkflowCondition";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowModule = @"WorkflowModule";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowModuleTag = @"WorkflowModule_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowModuleWorkflowCondition = @"WorkflowModule_WorkflowCondition";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowModuleCategory = @"WorkflowModuleCategory";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowModuleSubCategory = @"WorkflowModuleSubCategory";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowModuleVersion = @"WorkflowModuleVersion";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowSelector = @"WorkflowSelector";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowSelectorQueryParameterValue = @"WorkflowSelector_QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowStatus = @"WorkflowStatus";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowType = @"WorkflowType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string WorkflowVersion = @"WorkflowVersion";
		
	}
	#endregion
	#region Schemas
	public partial class Schemas
	{
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Aardvark{
			get { return DataService.GetSchema("Aardvark","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table AppClient{
			get { return DataService.GetSchema("AppClient","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Application{
			get { return DataService.GetSchema("Application","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ApplicationRole{
			get { return DataService.GetSchema("Application_Role","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceApplication{
			get { return DataService.GetSchema("ConfigurationServiceApplication","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceApplicationType{
			get { return DataService.GetSchema("ConfigurationServiceApplicationType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceGroup{
			get { return DataService.GetSchema("ConfigurationServiceGroup","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceGroupTag{
			get { return DataService.GetSchema("ConfigurationServiceGroup_Tag","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceGroupImport{
			get { return DataService.GetSchema("ConfigurationServiceGroupImport","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceGroupSelector{
			get { return DataService.GetSchema("ConfigurationServiceGroupSelector","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceGroupSelectorQueryParameterValue{
			get { return DataService.GetSchema("ConfigurationServiceGroupSelector_QueryParameterValue","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceGroupStatus{
			get { return DataService.GetSchema("ConfigurationServiceGroupStatus","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceGroupType{
			get { return DataService.GetSchema("ConfigurationServiceGroupType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceItem{
			get { return DataService.GetSchema("ConfigurationServiceItem","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceItemConfigurationServiceGroupType{
			get { return DataService.GetSchema("ConfigurationServiceItem_ConfigurationServiceGroupType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceLabel{
			get { return DataService.GetSchema("ConfigurationServiceLabel","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceLabelType{
			get { return DataService.GetSchema("ConfigurationServiceLabelType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceLabelValue{
			get { return DataService.GetSchema("ConfigurationServiceLabelValue","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceLabelValueImport{
			get { return DataService.GetSchema("ConfigurationServiceLabelValueImport","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ConfigurationServiceQueryParameterValueImport{
			get { return DataService.GetSchema("ConfigurationServiceQueryParameterValueImport","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table DDLChangeLog{
			get { return DataService.GetSchema("DDLChangeLog","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table EntityType{
			get { return DataService.GetSchema("EntityType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationApplication{
			get { return DataService.GetSchema("JumpstationApplication","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationDomain{
			get { return DataService.GetSchema("JumpstationDomain","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationGroup{
			get { return DataService.GetSchema("JumpstationGroup","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationGroupTag{
			get { return DataService.GetSchema("JumpstationGroup_Tag","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationGroupPivot{
			get { return DataService.GetSchema("JumpstationGroupPivot","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationGroupSelector{
			get { return DataService.GetSchema("JumpstationGroupSelector","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationGroupSelectorQueryParameterValue{
			get { return DataService.GetSchema("JumpstationGroupSelector_QueryParameterValue","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationGroupStatus{
			get { return DataService.GetSchema("JumpstationGroupStatus","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationGroupType{
			get { return DataService.GetSchema("JumpstationGroupType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationMacro{
			get { return DataService.GetSchema("JumpstationMacro","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationMacroStatus{
			get { return DataService.GetSchema("JumpstationMacroStatus","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table JumpstationMacroValue{
			get { return DataService.GetSchema("JumpstationMacroValue","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Log{
			get { return DataService.GetSchema("Log","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Note{
			get { return DataService.GetSchema("Note","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table NoteType{
			get { return DataService.GetSchema("NoteType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Person{
			get { return DataService.GetSchema("Person","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table PersonRole{
			get { return DataService.GetSchema("Person_Role","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ProxyURL{
			get { return DataService.GetSchema("ProxyURL","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ProxyURLQueryParameterValue{
			get { return DataService.GetSchema("ProxyURL_QueryParameterValue","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ProxyURLTag{
			get { return DataService.GetSchema("ProxyURL_Tag","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ProxyURLDomain{
			get { return DataService.GetSchema("ProxyURLDomain","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ProxyURLGroupType{
			get { return DataService.GetSchema("ProxyURLGroupType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ProxyURLStatus{
			get { return DataService.GetSchema("ProxyURLStatus","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table ProxyURLType{
			get { return DataService.GetSchema("ProxyURLType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table PublishTemp{
			get { return DataService.GetSchema("PublishTemp","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table QueryParameter{
			get { return DataService.GetSchema("QueryParameter","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table QueryParameterConfigurationServiceGroupType{
			get { return DataService.GetSchema("QueryParameter_ConfigurationServiceGroupType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table QueryParameterJumpstationGroupType{
			get { return DataService.GetSchema("QueryParameter_JumpstationGroupType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table QueryParameterProxyURLType{
			get { return DataService.GetSchema("QueryParameter_ProxyURLType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table QueryParameterWorkflowType{
			get { return DataService.GetSchema("QueryParameter_WorkflowType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table QueryParameterValue{
			get { return DataService.GetSchema("QueryParameterValue","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Role{
			get { return DataService.GetSchema("Role","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table RowStatus{
			get { return DataService.GetSchema("RowStatus","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Tag{
			get { return DataService.GetSchema("Tag","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Tenant{
			get { return DataService.GetSchema("Tenant","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table TenantAppClient{
			get { return DataService.GetSchema("Tenant_AppClient","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Workflow{
			get { return DataService.GetSchema("Workflow","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowTag{
			get { return DataService.GetSchema("Workflow_Tag","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowWorkflowModule{
			get { return DataService.GetSchema("Workflow_WorkflowModule","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowApplication{
			get { return DataService.GetSchema("WorkflowApplication","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowApplicationType{
			get { return DataService.GetSchema("WorkflowApplicationType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowCondition{
			get { return DataService.GetSchema("WorkflowCondition","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowModule{
			get { return DataService.GetSchema("WorkflowModule","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowModuleTag{
			get { return DataService.GetSchema("WorkflowModule_Tag","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowModuleWorkflowCondition{
			get { return DataService.GetSchema("WorkflowModule_WorkflowCondition","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowModuleCategory{
			get { return DataService.GetSchema("WorkflowModuleCategory","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowModuleSubCategory{
			get { return DataService.GetSchema("WorkflowModuleSubCategory","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowModuleVersion{
			get { return DataService.GetSchema("WorkflowModuleVersion","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowSelector{
			get { return DataService.GetSchema("WorkflowSelector","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowSelectorQueryParameterValue{
			get { return DataService.GetSchema("WorkflowSelector_QueryParameterValue","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowStatus{
			get { return DataService.GetSchema("WorkflowStatus","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowType{
			get { return DataService.GetSchema("WorkflowType","ElementsCPSDB"); }
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table WorkflowVersion{
			get { return DataService.GetSchema("WorkflowVersion","ElementsCPSDB"); }
		}
		
	}
	#endregion
	#region View Struct
	public partial struct Views
	{
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapAppClient = @"vwMapAppClient";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapApplication = @"vwMapApplication";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceApplication = @"vwMapConfigurationServiceApplication";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceApplicationType = @"vwMapConfigurationServiceApplicationType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceGroup = @"vwMapConfigurationServiceGroup";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceGroupImport = @"vwMapConfigurationServiceGroupImport";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceGroupSelector = @"vwMapConfigurationServiceGroupSelector";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceGroupSelectorQueryParameterValue = @"vwMapConfigurationServiceGroupSelector_QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceGroupTagTag = @"vwMapConfigurationServiceGroupTag_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceGroupType = @"vwMapConfigurationServiceGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceItem = @"vwMapConfigurationServiceItem";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceItemConfigurationServiceGroupType = @"vwMapConfigurationServiceItem_ConfigurationServiceGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceLabel = @"vwMapConfigurationServiceLabel";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceLabelConfigurationServiceGroupType = @"vwMapConfigurationServiceLabel_ConfigurationServiceGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceLabelValue = @"vwMapConfigurationServiceLabelValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceLabelValueImport = @"vwMapConfigurationServiceLabelValueImport";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapConfigurationServiceQueryParameterValueImport = @"vwMapConfigurationServiceQueryParameterValueImport";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapEntityType = @"vwMapEntityType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationApplication = @"vwMapJumpstationApplication";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationDomain = @"vwMapJumpstationDomain";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationGroup = @"vwMapJumpstationGroup";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationGroupCalcOnFly = @"vwMapJumpstationGroupCalcOnFly";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationGroupSelector = @"vwMapJumpstationGroupSelector";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationGroupSelectorQueryParameterValue = @"vwMapJumpstationGroupSelector_QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationGroupTagTag = @"vwMapJumpstationGroupTag_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationGroupType = @"vwMapJumpstationGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationMacro = @"vwMapJumpstationMacro";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapJumpstationMacroValue = @"vwMapJumpstationMacroValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapNote = @"vwMapNote";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapNoteType = @"vwMapNoteType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapPerson = @"vwMapPerson";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapPersonRole = @"vwMapPerson_Role";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapProxyURL = @"vwMapProxyURL";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapProxyURLQueryParameterValue = @"vwMapProxyURL_QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapProxyURLDomain = @"vwMapProxyURLDomain";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapProxyURLGroupType = @"vwMapProxyURLGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapProxyURLTagTag = @"vwMapProxyURLTag_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapProxyURLType = @"vwMapProxyURLType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapQueryParameter = @"vwMapQueryParameter";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapQueryParameterConfigurationServiceGroupType = @"vwMapQueryParameter_ConfigurationServiceGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapQueryParameterJumpstationGroupType = @"vwMapQueryParameter_JumpstationGroupType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapQueryParameterProxyURLType = @"vwMapQueryParameter_ProxyURLType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapQueryParameterWorkflowType = @"vwMapQueryParameter_WorkflowType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapQueryParameterValue = @"vwMapQueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapTenant = @"vwMapTenant";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflow = @"vwMapWorkflow";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowWorkflowModule = @"vwMapWorkflow_WorkflowModule";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowApplication = @"vwMapWorkflowApplication";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowApplicationType = @"vwMapWorkflowApplicationType";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowCondition = @"vwMapWorkflowCondition";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowModule = @"vwMapWorkflowModule";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowModuleWorkflowCondition = @"vwMapWorkflowModule_WorkflowCondition";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowModuleCategory = @"vwMapWorkflowModuleCategory";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowModuleSubCategory = @"vwMapWorkflowModuleSubCategory";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowModuleTagTag = @"vwMapWorkflowModuleTag_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowModuleVersion = @"vwMapWorkflowModuleVersion";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowSelector = @"vwMapWorkflowSelector";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowSelectorQueryParameterValue = @"vwMapWorkflowSelector_QueryParameterValue";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowTagTag = @"vwMapWorkflowTag_Tag";
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string VwMapWorkflowType = @"vwMapWorkflowType";
		
	}
	#endregion
	#region Query Factories
	public static partial class DB
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static DataProvider _provider = DataService.Providers["ElementsCPSDB"];
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		static ISubSonicRepository _repository;
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static ISubSonicRepository Repository {
			get
			{
				if (_repository == null)
					return new SubSonicRepository(_provider);
				return _repository; 
			}
			set { _repository = value; }
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Select SelectAllColumnsFrom<T>() where T : RecordBase<T>, new()
		{
			return Repository.SelectAllColumnsFrom<T>();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Select Select()
		{
			return Repository.Select();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Select Select(params string[] columns)
		{
			return Repository.Select(columns);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Select Select(params Aggregate[] aggregates)
		{
			return Repository.Select(aggregates);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Update Update<T>() where T : RecordBase<T>, new()
		{
			return Repository.Update<T>();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Insert Insert()
		{
			return Repository.Insert();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Delete Delete()
		{
			return Repository.Delete();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static InlineQuery Query()
		{
			return Repository.Query();
		}
		
	}
	#endregion
}
namespace Generated
{
	#region Databases
	public partial struct Databases 
	{
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static string ElementsCPSDB = @"ElementsCPSDB";
		
	}
	#endregion
}