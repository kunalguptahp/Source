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
using HP.HPFx.Data.Utility;
namespace HP.ElementsCPS.Data.SubSonicClient{
	/// <summary>
	/// Strongly-typed collection for the VwMapProxyURL class.
	/// </summary>
	[Serializable]
	public partial class VwMapProxyURLCollection : ReadOnlyList<VwMapProxyURL, VwMapProxyURLCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURLCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapProxyURL);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapProxyURL class.
	/// </summary>
	[Serializable]
	public partial class VwMapProxyURLController : BaseReadOnlyRecordController<VwMapProxyURL, VwMapProxyURLCollection, VwMapProxyURLController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURLController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapProxyURL.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapProxyURL view.
	/// </summary>
	[Serializable]
	public partial class VwMapProxyURL : ReadOnlyRecord<VwMapProxyURL>, IReadOnlyRecord, IRecord
	{
		#region Schema Accessor
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
				{
					SetSQLProps();
				}
				return BaseSchema;
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("vwMapProxyURL", TableType.View, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = false;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = false;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarRowStatusName = new TableSchema.TableColumn(schema);
				colvarRowStatusName.ColumnName = "RowStatusName";
				colvarRowStatusName.DataType = DbType.String;
				colvarRowStatusName.MaxLength = 256;
				colvarRowStatusName.AutoIncrement = false;
				colvarRowStatusName.IsNullable = false;
				colvarRowStatusName.IsPrimaryKey = false;
				colvarRowStatusName.IsForeignKey = false;
				colvarRowStatusName.IsReadOnly = false;
				
				schema.Columns.Add(colvarRowStatusName);
				
				TableSchema.TableColumn colvarProxyURLStatusName = new TableSchema.TableColumn(schema);
				colvarProxyURLStatusName.ColumnName = "ProxyURLStatusName";
				colvarProxyURLStatusName.DataType = DbType.String;
				colvarProxyURLStatusName.MaxLength = 256;
				colvarProxyURLStatusName.AutoIncrement = false;
				colvarProxyURLStatusName.IsNullable = false;
				colvarProxyURLStatusName.IsPrimaryKey = false;
				colvarProxyURLStatusName.IsForeignKey = false;
				colvarProxyURLStatusName.IsReadOnly = false;
				
				schema.Columns.Add(colvarProxyURLStatusName);
				
				TableSchema.TableColumn colvarPersonName = new TableSchema.TableColumn(schema);
				colvarPersonName.ColumnName = "PersonName";
				colvarPersonName.DataType = DbType.String;
				colvarPersonName.MaxLength = 326;
				colvarPersonName.AutoIncrement = false;
				colvarPersonName.IsNullable = false;
				colvarPersonName.IsPrimaryKey = false;
				colvarPersonName.IsForeignKey = false;
				colvarPersonName.IsReadOnly = false;
				
				schema.Columns.Add(colvarPersonName);
				
				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				
				schema.Columns.Add(colvarCreatedBy);
				
				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				
				schema.Columns.Add(colvarCreatedOn);
				
				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.String;
				colvarModifiedBy.MaxLength = 50;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				
				schema.Columns.Add(colvarModifiedBy);
				
				TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(schema);
				colvarModifiedOn.ColumnName = "ModifiedOn";
				colvarModifiedOn.DataType = DbType.DateTime;
				colvarModifiedOn.MaxLength = 0;
				colvarModifiedOn.AutoIncrement = false;
				colvarModifiedOn.IsNullable = false;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				
				schema.Columns.Add(colvarModifiedOn);
				
				TableSchema.TableColumn colvarRowStatusId = new TableSchema.TableColumn(schema);
				colvarRowStatusId.ColumnName = "RowStatusId";
				colvarRowStatusId.DataType = DbType.Int32;
				colvarRowStatusId.MaxLength = 0;
				colvarRowStatusId.AutoIncrement = false;
				colvarRowStatusId.IsNullable = false;
				colvarRowStatusId.IsPrimaryKey = false;
				colvarRowStatusId.IsForeignKey = false;
				colvarRowStatusId.IsReadOnly = false;
				
				schema.Columns.Add(colvarRowStatusId);
				
				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				
				schema.Columns.Add(colvarDescription);
				
				TableSchema.TableColumn colvarProxyURLStatusId = new TableSchema.TableColumn(schema);
				colvarProxyURLStatusId.ColumnName = "ProxyURLStatusId";
				colvarProxyURLStatusId.DataType = DbType.Int32;
				colvarProxyURLStatusId.MaxLength = 0;
				colvarProxyURLStatusId.AutoIncrement = false;
				colvarProxyURLStatusId.IsNullable = false;
				colvarProxyURLStatusId.IsPrimaryKey = false;
				colvarProxyURLStatusId.IsForeignKey = false;
				colvarProxyURLStatusId.IsReadOnly = false;
				
				schema.Columns.Add(colvarProxyURLStatusId);
				
				TableSchema.TableColumn colvarProxyURLTypeId = new TableSchema.TableColumn(schema);
				colvarProxyURLTypeId.ColumnName = "ProxyURLTypeId";
				colvarProxyURLTypeId.DataType = DbType.Int32;
				colvarProxyURLTypeId.MaxLength = 0;
				colvarProxyURLTypeId.AutoIncrement = false;
				colvarProxyURLTypeId.IsNullable = false;
				colvarProxyURLTypeId.IsPrimaryKey = false;
				colvarProxyURLTypeId.IsForeignKey = false;
				colvarProxyURLTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarProxyURLTypeId);
				
				TableSchema.TableColumn colvarOwnerId = new TableSchema.TableColumn(schema);
				colvarOwnerId.ColumnName = "OwnerId";
				colvarOwnerId.DataType = DbType.Int32;
				colvarOwnerId.MaxLength = 0;
				colvarOwnerId.AutoIncrement = false;
				colvarOwnerId.IsNullable = false;
				colvarOwnerId.IsPrimaryKey = false;
				colvarOwnerId.IsForeignKey = false;
				colvarOwnerId.IsReadOnly = false;
				
				schema.Columns.Add(colvarOwnerId);
				
				TableSchema.TableColumn colvarUrl = new TableSchema.TableColumn(schema);
				colvarUrl.ColumnName = "URL";
				colvarUrl.DataType = DbType.AnsiString;
				colvarUrl.MaxLength = 512;
				colvarUrl.AutoIncrement = false;
				colvarUrl.IsNullable = true;
				colvarUrl.IsPrimaryKey = false;
				colvarUrl.IsForeignKey = false;
				colvarUrl.IsReadOnly = false;
				
				schema.Columns.Add(colvarUrl);
				
				TableSchema.TableColumn colvarProxyURLTypeName = new TableSchema.TableColumn(schema);
				colvarProxyURLTypeName.ColumnName = "ProxyURLTypeName";
				colvarProxyURLTypeName.DataType = DbType.String;
				colvarProxyURLTypeName.MaxLength = 256;
				colvarProxyURLTypeName.AutoIncrement = false;
				colvarProxyURLTypeName.IsNullable = false;
				colvarProxyURLTypeName.IsPrimaryKey = false;
				colvarProxyURLTypeName.IsForeignKey = false;
				colvarProxyURLTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarProxyURLTypeName);
				
				TableSchema.TableColumn colvarProxyURLTypeElementsKey = new TableSchema.TableColumn(schema);
				colvarProxyURLTypeElementsKey.ColumnName = "ProxyURLTypeElementsKey";
				colvarProxyURLTypeElementsKey.DataType = DbType.Int32;
				colvarProxyURLTypeElementsKey.MaxLength = 0;
				colvarProxyURLTypeElementsKey.AutoIncrement = false;
				colvarProxyURLTypeElementsKey.IsNullable = false;
				colvarProxyURLTypeElementsKey.IsPrimaryKey = false;
				colvarProxyURLTypeElementsKey.IsForeignKey = false;
				colvarProxyURLTypeElementsKey.IsReadOnly = false;
				
				schema.Columns.Add(colvarProxyURLTypeElementsKey);
				
				TableSchema.TableColumn colvarTouchpointParameterValueQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarTouchpointParameterValueQueryParameterValueId.ColumnName = "TouchpointParameterValueQueryParameterValueId";
				colvarTouchpointParameterValueQueryParameterValueId.DataType = DbType.Int32;
				colvarTouchpointParameterValueQueryParameterValueId.MaxLength = 0;
				colvarTouchpointParameterValueQueryParameterValueId.AutoIncrement = false;
				colvarTouchpointParameterValueQueryParameterValueId.IsNullable = true;
				colvarTouchpointParameterValueQueryParameterValueId.IsPrimaryKey = false;
				colvarTouchpointParameterValueQueryParameterValueId.IsForeignKey = false;
				colvarTouchpointParameterValueQueryParameterValueId.IsReadOnly = false;
				
				schema.Columns.Add(colvarTouchpointParameterValueQueryParameterValueId);
				
				TableSchema.TableColumn colvarTouchpointParameterValueQueryParameterId = new TableSchema.TableColumn(schema);
				colvarTouchpointParameterValueQueryParameterId.ColumnName = "TouchpointParameterValueQueryParameterId";
				colvarTouchpointParameterValueQueryParameterId.DataType = DbType.Int32;
				colvarTouchpointParameterValueQueryParameterId.MaxLength = 0;
				colvarTouchpointParameterValueQueryParameterId.AutoIncrement = false;
				colvarTouchpointParameterValueQueryParameterId.IsNullable = true;
				colvarTouchpointParameterValueQueryParameterId.IsPrimaryKey = false;
				colvarTouchpointParameterValueQueryParameterId.IsForeignKey = false;
				colvarTouchpointParameterValueQueryParameterId.IsReadOnly = false;
				
				schema.Columns.Add(colvarTouchpointParameterValueQueryParameterId);
				
				TableSchema.TableColumn colvarTouchpointParameterValueName = new TableSchema.TableColumn(schema);
				colvarTouchpointParameterValueName.ColumnName = "TouchpointParameterValueName";
				colvarTouchpointParameterValueName.DataType = DbType.String;
				colvarTouchpointParameterValueName.MaxLength = 256;
				colvarTouchpointParameterValueName.AutoIncrement = false;
				colvarTouchpointParameterValueName.IsNullable = true;
				colvarTouchpointParameterValueName.IsPrimaryKey = false;
				colvarTouchpointParameterValueName.IsForeignKey = false;
				colvarTouchpointParameterValueName.IsReadOnly = false;
				
				schema.Columns.Add(colvarTouchpointParameterValueName);
				
				TableSchema.TableColumn colvarBrandParameterValueQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarBrandParameterValueQueryParameterValueId.ColumnName = "BrandParameterValueQueryParameterValueId";
				colvarBrandParameterValueQueryParameterValueId.DataType = DbType.Int32;
				colvarBrandParameterValueQueryParameterValueId.MaxLength = 0;
				colvarBrandParameterValueQueryParameterValueId.AutoIncrement = false;
				colvarBrandParameterValueQueryParameterValueId.IsNullable = true;
				colvarBrandParameterValueQueryParameterValueId.IsPrimaryKey = false;
				colvarBrandParameterValueQueryParameterValueId.IsForeignKey = false;
				colvarBrandParameterValueQueryParameterValueId.IsReadOnly = false;
				
				schema.Columns.Add(colvarBrandParameterValueQueryParameterValueId);
				
				TableSchema.TableColumn colvarBrandParameterValueQueryParameterId = new TableSchema.TableColumn(schema);
				colvarBrandParameterValueQueryParameterId.ColumnName = "BrandParameterValueQueryParameterId";
				colvarBrandParameterValueQueryParameterId.DataType = DbType.Int32;
				colvarBrandParameterValueQueryParameterId.MaxLength = 0;
				colvarBrandParameterValueQueryParameterId.AutoIncrement = false;
				colvarBrandParameterValueQueryParameterId.IsNullable = true;
				colvarBrandParameterValueQueryParameterId.IsPrimaryKey = false;
				colvarBrandParameterValueQueryParameterId.IsForeignKey = false;
				colvarBrandParameterValueQueryParameterId.IsReadOnly = false;
				
				schema.Columns.Add(colvarBrandParameterValueQueryParameterId);
				
				TableSchema.TableColumn colvarBrandParameterValueName = new TableSchema.TableColumn(schema);
				colvarBrandParameterValueName.ColumnName = "BrandParameterValueName";
				colvarBrandParameterValueName.DataType = DbType.String;
				colvarBrandParameterValueName.MaxLength = 256;
				colvarBrandParameterValueName.AutoIncrement = false;
				colvarBrandParameterValueName.IsNullable = true;
				colvarBrandParameterValueName.IsPrimaryKey = false;
				colvarBrandParameterValueName.IsForeignKey = false;
				colvarBrandParameterValueName.IsReadOnly = false;
				
				schema.Columns.Add(colvarBrandParameterValueName);
				
				TableSchema.TableColumn colvarLocaleParameterValueQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarLocaleParameterValueQueryParameterValueId.ColumnName = "LocaleParameterValueQueryParameterValueId";
				colvarLocaleParameterValueQueryParameterValueId.DataType = DbType.Int32;
				colvarLocaleParameterValueQueryParameterValueId.MaxLength = 0;
				colvarLocaleParameterValueQueryParameterValueId.AutoIncrement = false;
				colvarLocaleParameterValueQueryParameterValueId.IsNullable = true;
				colvarLocaleParameterValueQueryParameterValueId.IsPrimaryKey = false;
				colvarLocaleParameterValueQueryParameterValueId.IsForeignKey = false;
				colvarLocaleParameterValueQueryParameterValueId.IsReadOnly = false;
				
				schema.Columns.Add(colvarLocaleParameterValueQueryParameterValueId);
				
				TableSchema.TableColumn colvarLocaleParameterValueQueryParameterId = new TableSchema.TableColumn(schema);
				colvarLocaleParameterValueQueryParameterId.ColumnName = "LocaleParameterValueQueryParameterId";
				colvarLocaleParameterValueQueryParameterId.DataType = DbType.Int32;
				colvarLocaleParameterValueQueryParameterId.MaxLength = 0;
				colvarLocaleParameterValueQueryParameterId.AutoIncrement = false;
				colvarLocaleParameterValueQueryParameterId.IsNullable = true;
				colvarLocaleParameterValueQueryParameterId.IsPrimaryKey = false;
				colvarLocaleParameterValueQueryParameterId.IsForeignKey = false;
				colvarLocaleParameterValueQueryParameterId.IsReadOnly = false;
				
				schema.Columns.Add(colvarLocaleParameterValueQueryParameterId);
				
				TableSchema.TableColumn colvarLocaleParameterValueName = new TableSchema.TableColumn(schema);
				colvarLocaleParameterValueName.ColumnName = "LocaleParameterValueName";
				colvarLocaleParameterValueName.DataType = DbType.String;
				colvarLocaleParameterValueName.MaxLength = 256;
				colvarLocaleParameterValueName.AutoIncrement = false;
				colvarLocaleParameterValueName.IsNullable = true;
				colvarLocaleParameterValueName.IsPrimaryKey = false;
				colvarLocaleParameterValueName.IsForeignKey = false;
				colvarLocaleParameterValueName.IsReadOnly = false;
				
				schema.Columns.Add(colvarLocaleParameterValueName);
				
				TableSchema.TableColumn colvarCycleParameterValueQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarCycleParameterValueQueryParameterValueId.ColumnName = "CycleParameterValueQueryParameterValueId";
				colvarCycleParameterValueQueryParameterValueId.DataType = DbType.Int32;
				colvarCycleParameterValueQueryParameterValueId.MaxLength = 0;
				colvarCycleParameterValueQueryParameterValueId.AutoIncrement = false;
				colvarCycleParameterValueQueryParameterValueId.IsNullable = true;
				colvarCycleParameterValueQueryParameterValueId.IsPrimaryKey = false;
				colvarCycleParameterValueQueryParameterValueId.IsForeignKey = false;
				colvarCycleParameterValueQueryParameterValueId.IsReadOnly = false;
				
				schema.Columns.Add(colvarCycleParameterValueQueryParameterValueId);
				
				TableSchema.TableColumn colvarCycleParameterValueQueryParameterId = new TableSchema.TableColumn(schema);
				colvarCycleParameterValueQueryParameterId.ColumnName = "CycleParameterValueQueryParameterId";
				colvarCycleParameterValueQueryParameterId.DataType = DbType.Int32;
				colvarCycleParameterValueQueryParameterId.MaxLength = 0;
				colvarCycleParameterValueQueryParameterId.AutoIncrement = false;
				colvarCycleParameterValueQueryParameterId.IsNullable = true;
				colvarCycleParameterValueQueryParameterId.IsPrimaryKey = false;
				colvarCycleParameterValueQueryParameterId.IsForeignKey = false;
				colvarCycleParameterValueQueryParameterId.IsReadOnly = false;
				
				schema.Columns.Add(colvarCycleParameterValueQueryParameterId);
				
				TableSchema.TableColumn colvarCycleParameterValueName = new TableSchema.TableColumn(schema);
				colvarCycleParameterValueName.ColumnName = "CycleParameterValueName";
				colvarCycleParameterValueName.DataType = DbType.String;
				colvarCycleParameterValueName.MaxLength = 256;
				colvarCycleParameterValueName.AutoIncrement = false;
				colvarCycleParameterValueName.IsNullable = true;
				colvarCycleParameterValueName.IsPrimaryKey = false;
				colvarCycleParameterValueName.IsForeignKey = false;
				colvarCycleParameterValueName.IsReadOnly = false;
				
				schema.Columns.Add(colvarCycleParameterValueName);
				
				TableSchema.TableColumn colvarPlatformParameterValueQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarPlatformParameterValueQueryParameterValueId.ColumnName = "PlatformParameterValueQueryParameterValueId";
				colvarPlatformParameterValueQueryParameterValueId.DataType = DbType.Int32;
				colvarPlatformParameterValueQueryParameterValueId.MaxLength = 0;
				colvarPlatformParameterValueQueryParameterValueId.AutoIncrement = false;
				colvarPlatformParameterValueQueryParameterValueId.IsNullable = true;
				colvarPlatformParameterValueQueryParameterValueId.IsPrimaryKey = false;
				colvarPlatformParameterValueQueryParameterValueId.IsForeignKey = false;
				colvarPlatformParameterValueQueryParameterValueId.IsReadOnly = false;
				
				schema.Columns.Add(colvarPlatformParameterValueQueryParameterValueId);
				
				TableSchema.TableColumn colvarPlatformParameterValueQueryParameterId = new TableSchema.TableColumn(schema);
				colvarPlatformParameterValueQueryParameterId.ColumnName = "PlatformParameterValueQueryParameterId";
				colvarPlatformParameterValueQueryParameterId.DataType = DbType.Int32;
				colvarPlatformParameterValueQueryParameterId.MaxLength = 0;
				colvarPlatformParameterValueQueryParameterId.AutoIncrement = false;
				colvarPlatformParameterValueQueryParameterId.IsNullable = true;
				colvarPlatformParameterValueQueryParameterId.IsPrimaryKey = false;
				colvarPlatformParameterValueQueryParameterId.IsForeignKey = false;
				colvarPlatformParameterValueQueryParameterId.IsReadOnly = false;
				
				schema.Columns.Add(colvarPlatformParameterValueQueryParameterId);
				
				TableSchema.TableColumn colvarPlatformParameterValueName = new TableSchema.TableColumn(schema);
				colvarPlatformParameterValueName.ColumnName = "PlatformParameterValueName";
				colvarPlatformParameterValueName.DataType = DbType.String;
				colvarPlatformParameterValueName.MaxLength = 256;
				colvarPlatformParameterValueName.AutoIncrement = false;
				colvarPlatformParameterValueName.IsNullable = true;
				colvarPlatformParameterValueName.IsPrimaryKey = false;
				colvarPlatformParameterValueName.IsForeignKey = false;
				colvarPlatformParameterValueName.IsReadOnly = false;
				
				schema.Columns.Add(colvarPlatformParameterValueName);
				
				TableSchema.TableColumn colvarPartnerCategoryParameterValueQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarPartnerCategoryParameterValueQueryParameterValueId.ColumnName = "PartnerCategoryParameterValueQueryParameterValueId";
				colvarPartnerCategoryParameterValueQueryParameterValueId.DataType = DbType.Int32;
				colvarPartnerCategoryParameterValueQueryParameterValueId.MaxLength = 0;
				colvarPartnerCategoryParameterValueQueryParameterValueId.AutoIncrement = false;
				colvarPartnerCategoryParameterValueQueryParameterValueId.IsNullable = true;
				colvarPartnerCategoryParameterValueQueryParameterValueId.IsPrimaryKey = false;
				colvarPartnerCategoryParameterValueQueryParameterValueId.IsForeignKey = false;
				colvarPartnerCategoryParameterValueQueryParameterValueId.IsReadOnly = false;
				
				schema.Columns.Add(colvarPartnerCategoryParameterValueQueryParameterValueId);
				
				TableSchema.TableColumn colvarPartnerCategoryParameterValueQueryParameterId = new TableSchema.TableColumn(schema);
				colvarPartnerCategoryParameterValueQueryParameterId.ColumnName = "PartnerCategoryParameterValueQueryParameterId";
				colvarPartnerCategoryParameterValueQueryParameterId.DataType = DbType.Int32;
				colvarPartnerCategoryParameterValueQueryParameterId.MaxLength = 0;
				colvarPartnerCategoryParameterValueQueryParameterId.AutoIncrement = false;
				colvarPartnerCategoryParameterValueQueryParameterId.IsNullable = true;
				colvarPartnerCategoryParameterValueQueryParameterId.IsPrimaryKey = false;
				colvarPartnerCategoryParameterValueQueryParameterId.IsForeignKey = false;
				colvarPartnerCategoryParameterValueQueryParameterId.IsReadOnly = false;
				
				schema.Columns.Add(colvarPartnerCategoryParameterValueQueryParameterId);
				
				TableSchema.TableColumn colvarPartnerCategoryParameterValueName = new TableSchema.TableColumn(schema);
				colvarPartnerCategoryParameterValueName.ColumnName = "PartnerCategoryParameterValueName";
				colvarPartnerCategoryParameterValueName.DataType = DbType.String;
				colvarPartnerCategoryParameterValueName.MaxLength = 256;
				colvarPartnerCategoryParameterValueName.AutoIncrement = false;
				colvarPartnerCategoryParameterValueName.IsNullable = true;
				colvarPartnerCategoryParameterValueName.IsPrimaryKey = false;
				colvarPartnerCategoryParameterValueName.IsForeignKey = false;
				colvarPartnerCategoryParameterValueName.IsReadOnly = false;
				
				schema.Columns.Add(colvarPartnerCategoryParameterValueName);
				
				TableSchema.TableColumn colvarTags = new TableSchema.TableColumn(schema);
				colvarTags.ColumnName = "Tags";
				colvarTags.DataType = DbType.String;
				colvarTags.MaxLength = -1;
				colvarTags.AutoIncrement = false;
				colvarTags.IsNullable = true;
				colvarTags.IsPrimaryKey = false;
				colvarTags.IsForeignKey = false;
				colvarTags.IsReadOnly = false;
				
				schema.Columns.Add(colvarTags);
				
				TableSchema.TableColumn colvarTagCount = new TableSchema.TableColumn(schema);
				colvarTagCount.ColumnName = "TagCount";
				colvarTagCount.DataType = DbType.Int32;
				colvarTagCount.MaxLength = 0;
				colvarTagCount.AutoIncrement = false;
				colvarTagCount.IsNullable = true;
				colvarTagCount.IsPrimaryKey = false;
				colvarTagCount.IsForeignKey = false;
				colvarTagCount.IsReadOnly = false;
				
				schema.Columns.Add(colvarTagCount);
				
				TableSchema.TableColumn colvarProductionId = new TableSchema.TableColumn(schema);
				colvarProductionId.ColumnName = "ProductionId";
				colvarProductionId.DataType = DbType.Int32;
				colvarProductionId.MaxLength = 0;
				colvarProductionId.AutoIncrement = false;
				colvarProductionId.IsNullable = true;
				colvarProductionId.IsPrimaryKey = false;
				colvarProductionId.IsForeignKey = false;
				colvarProductionId.IsReadOnly = false;
				
				schema.Columns.Add(colvarProductionId);
				
				TableSchema.TableColumn colvarValidationId = new TableSchema.TableColumn(schema);
				colvarValidationId.ColumnName = "ValidationId";
				colvarValidationId.DataType = DbType.Int32;
				colvarValidationId.MaxLength = 0;
				colvarValidationId.AutoIncrement = false;
				colvarValidationId.IsNullable = true;
				colvarValidationId.IsPrimaryKey = false;
				colvarValidationId.IsForeignKey = false;
				colvarValidationId.IsReadOnly = false;
				
				schema.Columns.Add(colvarValidationId);
				
				TableSchema.TableColumn colvarQueryString = new TableSchema.TableColumn(schema);
				colvarQueryString.ColumnName = "QueryString";
				colvarQueryString.DataType = DbType.AnsiString;
				colvarQueryString.MaxLength = 512;
				colvarQueryString.AutoIncrement = false;
				colvarQueryString.IsNullable = true;
				colvarQueryString.IsPrimaryKey = false;
				colvarQueryString.IsForeignKey = false;
				colvarQueryString.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryString);
				
				TableSchema.TableColumn colvarProductionDomain = new TableSchema.TableColumn(schema);
				colvarProductionDomain.ColumnName = "ProductionDomain";
				colvarProductionDomain.DataType = DbType.String;
				colvarProductionDomain.MaxLength = 256;
				colvarProductionDomain.AutoIncrement = false;
				colvarProductionDomain.IsNullable = false;
				colvarProductionDomain.IsPrimaryKey = false;
				colvarProductionDomain.IsForeignKey = false;
				colvarProductionDomain.IsReadOnly = false;
				
				schema.Columns.Add(colvarProductionDomain);
				
				TableSchema.TableColumn colvarValidationDomain = new TableSchema.TableColumn(schema);
				colvarValidationDomain.ColumnName = "ValidationDomain";
				colvarValidationDomain.DataType = DbType.String;
				colvarValidationDomain.MaxLength = 256;
				colvarValidationDomain.AutoIncrement = false;
				colvarValidationDomain.IsNullable = false;
				colvarValidationDomain.IsPrimaryKey = false;
				colvarValidationDomain.IsForeignKey = false;
				colvarValidationDomain.IsReadOnly = false;
				
				schema.Columns.Add(colvarValidationDomain);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapProxyURL",schema);
			}
		}
		#endregion
		#region Query Accessor
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Query CreateQuery()
		{
			return new Query(Schema);
		}
		#endregion
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected VwMapProxyURL(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURL() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURL(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURL(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURL(string columnName, object columnValue)
			: this(false, false)
		{
			LoadByParam(columnName, columnValue);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected static void SetSQLProps() { GetTableSchema(); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void InitSetDefaults() { SetDefaults(); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void ConstructorHelper(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if (useDatabaseDefaults)
			{
				this.ForceDefaults();
			}
			else
			{
				this.InitSetDefaults();
			}
		}
		#region Copy Constructor
		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapProxyURL"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapProxyURL(VwMapProxyURL original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapProxyURL original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.RowStatusName = original.RowStatusName;
			
			this.ProxyURLStatusName = original.ProxyURLStatusName;
			
			this.PersonName = original.PersonName;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.Description = original.Description;
			
			this.ProxyURLStatusId = original.ProxyURLStatusId;
			
			this.ProxyURLTypeId = original.ProxyURLTypeId;
			
			this.OwnerId = original.OwnerId;
			
			this.Url = original.Url;
			
			this.ProxyURLTypeName = original.ProxyURLTypeName;
			
			this.ProxyURLTypeElementsKey = original.ProxyURLTypeElementsKey;
			
			this.TouchpointParameterValueQueryParameterValueId = original.TouchpointParameterValueQueryParameterValueId;
			
			this.TouchpointParameterValueQueryParameterId = original.TouchpointParameterValueQueryParameterId;
			
			this.TouchpointParameterValueName = original.TouchpointParameterValueName;
			
			this.BrandParameterValueQueryParameterValueId = original.BrandParameterValueQueryParameterValueId;
			
			this.BrandParameterValueQueryParameterId = original.BrandParameterValueQueryParameterId;
			
			this.BrandParameterValueName = original.BrandParameterValueName;
			
			this.LocaleParameterValueQueryParameterValueId = original.LocaleParameterValueQueryParameterValueId;
			
			this.LocaleParameterValueQueryParameterId = original.LocaleParameterValueQueryParameterId;
			
			this.LocaleParameterValueName = original.LocaleParameterValueName;
			
			this.CycleParameterValueQueryParameterValueId = original.CycleParameterValueQueryParameterValueId;
			
			this.CycleParameterValueQueryParameterId = original.CycleParameterValueQueryParameterId;
			
			this.CycleParameterValueName = original.CycleParameterValueName;
			
			this.PlatformParameterValueQueryParameterValueId = original.PlatformParameterValueQueryParameterValueId;
			
			this.PlatformParameterValueQueryParameterId = original.PlatformParameterValueQueryParameterId;
			
			this.PlatformParameterValueName = original.PlatformParameterValueName;
			
			this.PartnerCategoryParameterValueQueryParameterValueId = original.PartnerCategoryParameterValueQueryParameterValueId;
			
			this.PartnerCategoryParameterValueQueryParameterId = original.PartnerCategoryParameterValueQueryParameterId;
			
			this.PartnerCategoryParameterValueName = original.PartnerCategoryParameterValueName;
			
			this.Tags = original.Tags;
			
			this.TagCount = original.TagCount;
			
			this.ProductionId = original.ProductionId;
			
			this.ValidationId = original.ValidationId;
			
			this.QueryString = original.QueryString;
			
			this.ProductionDomain = original.ProductionDomain;
			
			this.ValidationDomain = original.ValidationDomain;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapProxyURL"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapProxyURL Copy(VwMapProxyURL original)
		{
			return new VwMapProxyURL(original);
		}
		#endregion
		#endregion
		#region Properties
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		
		partial void OnIdChanging(int newValue);
		partial void OnIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get
			{
				return GetColumnValue<int>("Id");
			}
			set
			{
				this.OnIdChanging(value);
				this.OnPropertyChanging("Id", value);
				int oldValue = this.Id;
				SetColumnValue("Id", value);
				this.OnIdChanged(oldValue, value);
				this.OnPropertyChanged("Id", oldValue, value);
			}
		}
		partial void OnRowStatusNameChanging(string newValue);
		partial void OnRowStatusNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("RowStatusName")]
		[Bindable(true)]
		public string RowStatusName 
		{
			get
			{
				return GetColumnValue<string>("RowStatusName");
			}
			set
			{
				this.OnRowStatusNameChanging(value);
				this.OnPropertyChanging("RowStatusName", value);
				string oldValue = this.RowStatusName;
				SetColumnValue("RowStatusName", value);
				this.OnRowStatusNameChanged(oldValue, value);
				this.OnPropertyChanged("RowStatusName", oldValue, value);
			}
		}
		partial void OnProxyURLStatusNameChanging(string newValue);
		partial void OnProxyURLStatusNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProxyURLStatusName")]
		[Bindable(true)]
		public string ProxyURLStatusName 
		{
			get
			{
				return GetColumnValue<string>("ProxyURLStatusName");
			}
			set
			{
				this.OnProxyURLStatusNameChanging(value);
				this.OnPropertyChanging("ProxyURLStatusName", value);
				string oldValue = this.ProxyURLStatusName;
				SetColumnValue("ProxyURLStatusName", value);
				this.OnProxyURLStatusNameChanged(oldValue, value);
				this.OnPropertyChanged("ProxyURLStatusName", oldValue, value);
			}
		}
		partial void OnPersonNameChanging(string newValue);
		partial void OnPersonNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PersonName")]
		[Bindable(true)]
		public string PersonName 
		{
			get
			{
				return GetColumnValue<string>("PersonName");
			}
			set
			{
				this.OnPersonNameChanging(value);
				this.OnPropertyChanging("PersonName", value);
				string oldValue = this.PersonName;
				SetColumnValue("PersonName", value);
				this.OnPersonNameChanged(oldValue, value);
				this.OnPropertyChanged("PersonName", oldValue, value);
			}
		}
		partial void OnCreatedByChanging(string newValue);
		partial void OnCreatedByChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CreatedBy")]
		[Bindable(true)]
		public string CreatedBy 
		{
			get
			{
				return GetColumnValue<string>("CreatedBy");
			}
			set
			{
				this.OnCreatedByChanging(value);
				this.OnPropertyChanging("CreatedBy", value);
				string oldValue = this.CreatedBy;
				SetColumnValue("CreatedBy", value);
				this.OnCreatedByChanged(oldValue, value);
				this.OnPropertyChanged("CreatedBy", oldValue, value);
			}
		}
		partial void OnCreatedOnChanging(DateTime newValue);
		partial void OnCreatedOnChanged(DateTime oldValue, DateTime newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CreatedOn")]
		[Bindable(true)]
		public DateTime CreatedOn 
		{
			get
			{
				return GetColumnValue<DateTime>("CreatedOn");
			}
			set
			{
				this.OnCreatedOnChanging(value);
				this.OnPropertyChanging("CreatedOn", value);
				DateTime oldValue = this.CreatedOn;
				SetColumnValue("CreatedOn", value);
				this.OnCreatedOnChanged(oldValue, value);
				this.OnPropertyChanged("CreatedOn", oldValue, value);
			}
		}
		partial void OnModifiedByChanging(string newValue);
		partial void OnModifiedByChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ModifiedBy")]
		[Bindable(true)]
		public string ModifiedBy 
		{
			get
			{
				return GetColumnValue<string>("ModifiedBy");
			}
			set
			{
				this.OnModifiedByChanging(value);
				this.OnPropertyChanging("ModifiedBy", value);
				string oldValue = this.ModifiedBy;
				SetColumnValue("ModifiedBy", value);
				this.OnModifiedByChanged(oldValue, value);
				this.OnPropertyChanged("ModifiedBy", oldValue, value);
			}
		}
		partial void OnModifiedOnChanging(DateTime newValue);
		partial void OnModifiedOnChanged(DateTime oldValue, DateTime newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ModifiedOn")]
		[Bindable(true)]
		public DateTime ModifiedOn 
		{
			get
			{
				return GetColumnValue<DateTime>("ModifiedOn");
			}
			set
			{
				this.OnModifiedOnChanging(value);
				this.OnPropertyChanging("ModifiedOn", value);
				DateTime oldValue = this.ModifiedOn;
				SetColumnValue("ModifiedOn", value);
				this.OnModifiedOnChanged(oldValue, value);
				this.OnPropertyChanged("ModifiedOn", oldValue, value);
			}
		}
		partial void OnRowStatusIdChanging(int newValue);
		partial void OnRowStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("RowStatusId")]
		[Bindable(true)]
		public int RowStatusId 
		{
			get
			{
				return GetColumnValue<int>("RowStatusId");
			}
			set
			{
				this.OnRowStatusIdChanging(value);
				this.OnPropertyChanging("RowStatusId", value);
				int oldValue = this.RowStatusId;
				SetColumnValue("RowStatusId", value);
				this.OnRowStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("RowStatusId", oldValue, value);
			}
		}
		partial void OnDescriptionChanging(string newValue);
		partial void OnDescriptionChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Description")]
		[Bindable(true)]
		public string Description 
		{
			get
			{
				return GetColumnValue<string>("Description");
			}
			set
			{
				this.OnDescriptionChanging(value);
				this.OnPropertyChanging("Description", value);
				string oldValue = this.Description;
				SetColumnValue("Description", value);
				this.OnDescriptionChanged(oldValue, value);
				this.OnPropertyChanged("Description", oldValue, value);
			}
		}
		partial void OnProxyURLStatusIdChanging(int newValue);
		partial void OnProxyURLStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProxyURLStatusId")]
		[Bindable(true)]
		public int ProxyURLStatusId 
		{
			get
			{
				return GetColumnValue<int>("ProxyURLStatusId");
			}
			set
			{
				this.OnProxyURLStatusIdChanging(value);
				this.OnPropertyChanging("ProxyURLStatusId", value);
				int oldValue = this.ProxyURLStatusId;
				SetColumnValue("ProxyURLStatusId", value);
				this.OnProxyURLStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("ProxyURLStatusId", oldValue, value);
			}
		}
		partial void OnProxyURLTypeIdChanging(int newValue);
		partial void OnProxyURLTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProxyURLTypeId")]
		[Bindable(true)]
		public int ProxyURLTypeId 
		{
			get
			{
				return GetColumnValue<int>("ProxyURLTypeId");
			}
			set
			{
				this.OnProxyURLTypeIdChanging(value);
				this.OnPropertyChanging("ProxyURLTypeId", value);
				int oldValue = this.ProxyURLTypeId;
				SetColumnValue("ProxyURLTypeId", value);
				this.OnProxyURLTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("ProxyURLTypeId", oldValue, value);
			}
		}
		partial void OnOwnerIdChanging(int newValue);
		partial void OnOwnerIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("OwnerId")]
		[Bindable(true)]
		public int OwnerId 
		{
			get
			{
				return GetColumnValue<int>("OwnerId");
			}
			set
			{
				this.OnOwnerIdChanging(value);
				this.OnPropertyChanging("OwnerId", value);
				int oldValue = this.OwnerId;
				SetColumnValue("OwnerId", value);
				this.OnOwnerIdChanged(oldValue, value);
				this.OnPropertyChanged("OwnerId", oldValue, value);
			}
		}
		partial void OnUrlChanging(string newValue);
		partial void OnUrlChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Url")]
		[Bindable(true)]
		public string Url 
		{
			get
			{
				return GetColumnValue<string>("URL");
			}
			set
			{
				this.OnUrlChanging(value);
				this.OnPropertyChanging("Url", value);
				string oldValue = this.Url;
				SetColumnValue("URL", value);
				this.OnUrlChanged(oldValue, value);
				this.OnPropertyChanged("Url", oldValue, value);
			}
		}
		partial void OnProxyURLTypeNameChanging(string newValue);
		partial void OnProxyURLTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProxyURLTypeName")]
		[Bindable(true)]
		public string ProxyURLTypeName 
		{
			get
			{
				return GetColumnValue<string>("ProxyURLTypeName");
			}
			set
			{
				this.OnProxyURLTypeNameChanging(value);
				this.OnPropertyChanging("ProxyURLTypeName", value);
				string oldValue = this.ProxyURLTypeName;
				SetColumnValue("ProxyURLTypeName", value);
				this.OnProxyURLTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("ProxyURLTypeName", oldValue, value);
			}
		}
		partial void OnProxyURLTypeElementsKeyChanging(int newValue);
		partial void OnProxyURLTypeElementsKeyChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProxyURLTypeElementsKey")]
		[Bindable(true)]
		public int ProxyURLTypeElementsKey 
		{
			get
			{
				return GetColumnValue<int>("ProxyURLTypeElementsKey");
			}
			set
			{
				this.OnProxyURLTypeElementsKeyChanging(value);
				this.OnPropertyChanging("ProxyURLTypeElementsKey", value);
				int oldValue = this.ProxyURLTypeElementsKey;
				SetColumnValue("ProxyURLTypeElementsKey", value);
				this.OnProxyURLTypeElementsKeyChanged(oldValue, value);
				this.OnPropertyChanged("ProxyURLTypeElementsKey", oldValue, value);
			}
		}
		partial void OnTouchpointParameterValueQueryParameterValueIdChanging(int? newValue);
		partial void OnTouchpointParameterValueQueryParameterValueIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TouchpointParameterValueQueryParameterValueId")]
		[Bindable(true)]
		public int? TouchpointParameterValueQueryParameterValueId 
		{
			get
			{
				return GetColumnValue<int?>("TouchpointParameterValueQueryParameterValueId");
			}
			set
			{
				this.OnTouchpointParameterValueQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("TouchpointParameterValueQueryParameterValueId", value);
				int? oldValue = this.TouchpointParameterValueQueryParameterValueId;
				SetColumnValue("TouchpointParameterValueQueryParameterValueId", value);
				this.OnTouchpointParameterValueQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("TouchpointParameterValueQueryParameterValueId", oldValue, value);
			}
		}
		partial void OnTouchpointParameterValueQueryParameterIdChanging(int? newValue);
		partial void OnTouchpointParameterValueQueryParameterIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TouchpointParameterValueQueryParameterId")]
		[Bindable(true)]
		public int? TouchpointParameterValueQueryParameterId 
		{
			get
			{
				return GetColumnValue<int?>("TouchpointParameterValueQueryParameterId");
			}
			set
			{
				this.OnTouchpointParameterValueQueryParameterIdChanging(value);
				this.OnPropertyChanging("TouchpointParameterValueQueryParameterId", value);
				int? oldValue = this.TouchpointParameterValueQueryParameterId;
				SetColumnValue("TouchpointParameterValueQueryParameterId", value);
				this.OnTouchpointParameterValueQueryParameterIdChanged(oldValue, value);
				this.OnPropertyChanged("TouchpointParameterValueQueryParameterId", oldValue, value);
			}
		}
		partial void OnTouchpointParameterValueNameChanging(string newValue);
		partial void OnTouchpointParameterValueNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TouchpointParameterValueName")]
		[Bindable(true)]
		public string TouchpointParameterValueName 
		{
			get
			{
				return GetColumnValue<string>("TouchpointParameterValueName");
			}
			set
			{
				this.OnTouchpointParameterValueNameChanging(value);
				this.OnPropertyChanging("TouchpointParameterValueName", value);
				string oldValue = this.TouchpointParameterValueName;
				SetColumnValue("TouchpointParameterValueName", value);
				this.OnTouchpointParameterValueNameChanged(oldValue, value);
				this.OnPropertyChanged("TouchpointParameterValueName", oldValue, value);
			}
		}
		partial void OnBrandParameterValueQueryParameterValueIdChanging(int? newValue);
		partial void OnBrandParameterValueQueryParameterValueIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("BrandParameterValueQueryParameterValueId")]
		[Bindable(true)]
		public int? BrandParameterValueQueryParameterValueId 
		{
			get
			{
				return GetColumnValue<int?>("BrandParameterValueQueryParameterValueId");
			}
			set
			{
				this.OnBrandParameterValueQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("BrandParameterValueQueryParameterValueId", value);
				int? oldValue = this.BrandParameterValueQueryParameterValueId;
				SetColumnValue("BrandParameterValueQueryParameterValueId", value);
				this.OnBrandParameterValueQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("BrandParameterValueQueryParameterValueId", oldValue, value);
			}
		}
		partial void OnBrandParameterValueQueryParameterIdChanging(int? newValue);
		partial void OnBrandParameterValueQueryParameterIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("BrandParameterValueQueryParameterId")]
		[Bindable(true)]
		public int? BrandParameterValueQueryParameterId 
		{
			get
			{
				return GetColumnValue<int?>("BrandParameterValueQueryParameterId");
			}
			set
			{
				this.OnBrandParameterValueQueryParameterIdChanging(value);
				this.OnPropertyChanging("BrandParameterValueQueryParameterId", value);
				int? oldValue = this.BrandParameterValueQueryParameterId;
				SetColumnValue("BrandParameterValueQueryParameterId", value);
				this.OnBrandParameterValueQueryParameterIdChanged(oldValue, value);
				this.OnPropertyChanged("BrandParameterValueQueryParameterId", oldValue, value);
			}
		}
		partial void OnBrandParameterValueNameChanging(string newValue);
		partial void OnBrandParameterValueNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("BrandParameterValueName")]
		[Bindable(true)]
		public string BrandParameterValueName 
		{
			get
			{
				return GetColumnValue<string>("BrandParameterValueName");
			}
			set
			{
				this.OnBrandParameterValueNameChanging(value);
				this.OnPropertyChanging("BrandParameterValueName", value);
				string oldValue = this.BrandParameterValueName;
				SetColumnValue("BrandParameterValueName", value);
				this.OnBrandParameterValueNameChanged(oldValue, value);
				this.OnPropertyChanged("BrandParameterValueName", oldValue, value);
			}
		}
		partial void OnLocaleParameterValueQueryParameterValueIdChanging(int? newValue);
		partial void OnLocaleParameterValueQueryParameterValueIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("LocaleParameterValueQueryParameterValueId")]
		[Bindable(true)]
		public int? LocaleParameterValueQueryParameterValueId 
		{
			get
			{
				return GetColumnValue<int?>("LocaleParameterValueQueryParameterValueId");
			}
			set
			{
				this.OnLocaleParameterValueQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("LocaleParameterValueQueryParameterValueId", value);
				int? oldValue = this.LocaleParameterValueQueryParameterValueId;
				SetColumnValue("LocaleParameterValueQueryParameterValueId", value);
				this.OnLocaleParameterValueQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("LocaleParameterValueQueryParameterValueId", oldValue, value);
			}
		}
		partial void OnLocaleParameterValueQueryParameterIdChanging(int? newValue);
		partial void OnLocaleParameterValueQueryParameterIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("LocaleParameterValueQueryParameterId")]
		[Bindable(true)]
		public int? LocaleParameterValueQueryParameterId 
		{
			get
			{
				return GetColumnValue<int?>("LocaleParameterValueQueryParameterId");
			}
			set
			{
				this.OnLocaleParameterValueQueryParameterIdChanging(value);
				this.OnPropertyChanging("LocaleParameterValueQueryParameterId", value);
				int? oldValue = this.LocaleParameterValueQueryParameterId;
				SetColumnValue("LocaleParameterValueQueryParameterId", value);
				this.OnLocaleParameterValueQueryParameterIdChanged(oldValue, value);
				this.OnPropertyChanged("LocaleParameterValueQueryParameterId", oldValue, value);
			}
		}
		partial void OnLocaleParameterValueNameChanging(string newValue);
		partial void OnLocaleParameterValueNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("LocaleParameterValueName")]
		[Bindable(true)]
		public string LocaleParameterValueName 
		{
			get
			{
				return GetColumnValue<string>("LocaleParameterValueName");
			}
			set
			{
				this.OnLocaleParameterValueNameChanging(value);
				this.OnPropertyChanging("LocaleParameterValueName", value);
				string oldValue = this.LocaleParameterValueName;
				SetColumnValue("LocaleParameterValueName", value);
				this.OnLocaleParameterValueNameChanged(oldValue, value);
				this.OnPropertyChanged("LocaleParameterValueName", oldValue, value);
			}
		}
		partial void OnCycleParameterValueQueryParameterValueIdChanging(int? newValue);
		partial void OnCycleParameterValueQueryParameterValueIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CycleParameterValueQueryParameterValueId")]
		[Bindable(true)]
		public int? CycleParameterValueQueryParameterValueId 
		{
			get
			{
				return GetColumnValue<int?>("CycleParameterValueQueryParameterValueId");
			}
			set
			{
				this.OnCycleParameterValueQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("CycleParameterValueQueryParameterValueId", value);
				int? oldValue = this.CycleParameterValueQueryParameterValueId;
				SetColumnValue("CycleParameterValueQueryParameterValueId", value);
				this.OnCycleParameterValueQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("CycleParameterValueQueryParameterValueId", oldValue, value);
			}
		}
		partial void OnCycleParameterValueQueryParameterIdChanging(int? newValue);
		partial void OnCycleParameterValueQueryParameterIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CycleParameterValueQueryParameterId")]
		[Bindable(true)]
		public int? CycleParameterValueQueryParameterId 
		{
			get
			{
				return GetColumnValue<int?>("CycleParameterValueQueryParameterId");
			}
			set
			{
				this.OnCycleParameterValueQueryParameterIdChanging(value);
				this.OnPropertyChanging("CycleParameterValueQueryParameterId", value);
				int? oldValue = this.CycleParameterValueQueryParameterId;
				SetColumnValue("CycleParameterValueQueryParameterId", value);
				this.OnCycleParameterValueQueryParameterIdChanged(oldValue, value);
				this.OnPropertyChanged("CycleParameterValueQueryParameterId", oldValue, value);
			}
		}
		partial void OnCycleParameterValueNameChanging(string newValue);
		partial void OnCycleParameterValueNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CycleParameterValueName")]
		[Bindable(true)]
		public string CycleParameterValueName 
		{
			get
			{
				return GetColumnValue<string>("CycleParameterValueName");
			}
			set
			{
				this.OnCycleParameterValueNameChanging(value);
				this.OnPropertyChanging("CycleParameterValueName", value);
				string oldValue = this.CycleParameterValueName;
				SetColumnValue("CycleParameterValueName", value);
				this.OnCycleParameterValueNameChanged(oldValue, value);
				this.OnPropertyChanged("CycleParameterValueName", oldValue, value);
			}
		}
		partial void OnPlatformParameterValueQueryParameterValueIdChanging(int? newValue);
		partial void OnPlatformParameterValueQueryParameterValueIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PlatformParameterValueQueryParameterValueId")]
		[Bindable(true)]
		public int? PlatformParameterValueQueryParameterValueId 
		{
			get
			{
				return GetColumnValue<int?>("PlatformParameterValueQueryParameterValueId");
			}
			set
			{
				this.OnPlatformParameterValueQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("PlatformParameterValueQueryParameterValueId", value);
				int? oldValue = this.PlatformParameterValueQueryParameterValueId;
				SetColumnValue("PlatformParameterValueQueryParameterValueId", value);
				this.OnPlatformParameterValueQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("PlatformParameterValueQueryParameterValueId", oldValue, value);
			}
		}
		partial void OnPlatformParameterValueQueryParameterIdChanging(int? newValue);
		partial void OnPlatformParameterValueQueryParameterIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PlatformParameterValueQueryParameterId")]
		[Bindable(true)]
		public int? PlatformParameterValueQueryParameterId 
		{
			get
			{
				return GetColumnValue<int?>("PlatformParameterValueQueryParameterId");
			}
			set
			{
				this.OnPlatformParameterValueQueryParameterIdChanging(value);
				this.OnPropertyChanging("PlatformParameterValueQueryParameterId", value);
				int? oldValue = this.PlatformParameterValueQueryParameterId;
				SetColumnValue("PlatformParameterValueQueryParameterId", value);
				this.OnPlatformParameterValueQueryParameterIdChanged(oldValue, value);
				this.OnPropertyChanged("PlatformParameterValueQueryParameterId", oldValue, value);
			}
		}
		partial void OnPlatformParameterValueNameChanging(string newValue);
		partial void OnPlatformParameterValueNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PlatformParameterValueName")]
		[Bindable(true)]
		public string PlatformParameterValueName 
		{
			get
			{
				return GetColumnValue<string>("PlatformParameterValueName");
			}
			set
			{
				this.OnPlatformParameterValueNameChanging(value);
				this.OnPropertyChanging("PlatformParameterValueName", value);
				string oldValue = this.PlatformParameterValueName;
				SetColumnValue("PlatformParameterValueName", value);
				this.OnPlatformParameterValueNameChanged(oldValue, value);
				this.OnPropertyChanged("PlatformParameterValueName", oldValue, value);
			}
		}
		partial void OnPartnerCategoryParameterValueQueryParameterValueIdChanging(int? newValue);
		partial void OnPartnerCategoryParameterValueQueryParameterValueIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PartnerCategoryParameterValueQueryParameterValueId")]
		[Bindable(true)]
		public int? PartnerCategoryParameterValueQueryParameterValueId 
		{
			get
			{
				return GetColumnValue<int?>("PartnerCategoryParameterValueQueryParameterValueId");
			}
			set
			{
				this.OnPartnerCategoryParameterValueQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("PartnerCategoryParameterValueQueryParameterValueId", value);
				int? oldValue = this.PartnerCategoryParameterValueQueryParameterValueId;
				SetColumnValue("PartnerCategoryParameterValueQueryParameterValueId", value);
				this.OnPartnerCategoryParameterValueQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("PartnerCategoryParameterValueQueryParameterValueId", oldValue, value);
			}
		}
		partial void OnPartnerCategoryParameterValueQueryParameterIdChanging(int? newValue);
		partial void OnPartnerCategoryParameterValueQueryParameterIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PartnerCategoryParameterValueQueryParameterId")]
		[Bindable(true)]
		public int? PartnerCategoryParameterValueQueryParameterId 
		{
			get
			{
				return GetColumnValue<int?>("PartnerCategoryParameterValueQueryParameterId");
			}
			set
			{
				this.OnPartnerCategoryParameterValueQueryParameterIdChanging(value);
				this.OnPropertyChanging("PartnerCategoryParameterValueQueryParameterId", value);
				int? oldValue = this.PartnerCategoryParameterValueQueryParameterId;
				SetColumnValue("PartnerCategoryParameterValueQueryParameterId", value);
				this.OnPartnerCategoryParameterValueQueryParameterIdChanged(oldValue, value);
				this.OnPropertyChanged("PartnerCategoryParameterValueQueryParameterId", oldValue, value);
			}
		}
		partial void OnPartnerCategoryParameterValueNameChanging(string newValue);
		partial void OnPartnerCategoryParameterValueNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PartnerCategoryParameterValueName")]
		[Bindable(true)]
		public string PartnerCategoryParameterValueName 
		{
			get
			{
				return GetColumnValue<string>("PartnerCategoryParameterValueName");
			}
			set
			{
				this.OnPartnerCategoryParameterValueNameChanging(value);
				this.OnPropertyChanging("PartnerCategoryParameterValueName", value);
				string oldValue = this.PartnerCategoryParameterValueName;
				SetColumnValue("PartnerCategoryParameterValueName", value);
				this.OnPartnerCategoryParameterValueNameChanged(oldValue, value);
				this.OnPropertyChanged("PartnerCategoryParameterValueName", oldValue, value);
			}
		}
		partial void OnTagsChanging(string newValue);
		partial void OnTagsChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Tags")]
		[Bindable(true)]
		public string Tags 
		{
			get
			{
				return GetColumnValue<string>("Tags");
			}
			set
			{
				this.OnTagsChanging(value);
				this.OnPropertyChanging("Tags", value);
				string oldValue = this.Tags;
				SetColumnValue("Tags", value);
				this.OnTagsChanged(oldValue, value);
				this.OnPropertyChanged("Tags", oldValue, value);
			}
		}
		partial void OnTagCountChanging(int? newValue);
		partial void OnTagCountChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TagCount")]
		[Bindable(true)]
		public int? TagCount 
		{
			get
			{
				return GetColumnValue<int?>("TagCount");
			}
			set
			{
				this.OnTagCountChanging(value);
				this.OnPropertyChanging("TagCount", value);
				int? oldValue = this.TagCount;
				SetColumnValue("TagCount", value);
				this.OnTagCountChanged(oldValue, value);
				this.OnPropertyChanged("TagCount", oldValue, value);
			}
		}
		partial void OnProductionIdChanging(int? newValue);
		partial void OnProductionIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProductionId")]
		[Bindable(true)]
		public int? ProductionId 
		{
			get
			{
				return GetColumnValue<int?>("ProductionId");
			}
			set
			{
				this.OnProductionIdChanging(value);
				this.OnPropertyChanging("ProductionId", value);
				int? oldValue = this.ProductionId;
				SetColumnValue("ProductionId", value);
				this.OnProductionIdChanged(oldValue, value);
				this.OnPropertyChanged("ProductionId", oldValue, value);
			}
		}
		partial void OnValidationIdChanging(int? newValue);
		partial void OnValidationIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ValidationId")]
		[Bindable(true)]
		public int? ValidationId 
		{
			get
			{
				return GetColumnValue<int?>("ValidationId");
			}
			set
			{
				this.OnValidationIdChanging(value);
				this.OnPropertyChanging("ValidationId", value);
				int? oldValue = this.ValidationId;
				SetColumnValue("ValidationId", value);
				this.OnValidationIdChanged(oldValue, value);
				this.OnPropertyChanged("ValidationId", oldValue, value);
			}
		}
		partial void OnQueryStringChanging(string newValue);
		partial void OnQueryStringChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryString")]
		[Bindable(true)]
		public string QueryString 
		{
			get
			{
				return GetColumnValue<string>("QueryString");
			}
			set
			{
				this.OnQueryStringChanging(value);
				this.OnPropertyChanging("QueryString", value);
				string oldValue = this.QueryString;
				SetColumnValue("QueryString", value);
				this.OnQueryStringChanged(oldValue, value);
				this.OnPropertyChanged("QueryString", oldValue, value);
			}
		}
		partial void OnProductionDomainChanging(string newValue);
		partial void OnProductionDomainChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProductionDomain")]
		[Bindable(true)]
		public string ProductionDomain 
		{
			get
			{
				return GetColumnValue<string>("ProductionDomain");
			}
			set
			{
				this.OnProductionDomainChanging(value);
				this.OnPropertyChanging("ProductionDomain", value);
				string oldValue = this.ProductionDomain;
				SetColumnValue("ProductionDomain", value);
				this.OnProductionDomainChanged(oldValue, value);
				this.OnPropertyChanged("ProductionDomain", oldValue, value);
			}
		}
		partial void OnValidationDomainChanging(string newValue);
		partial void OnValidationDomainChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ValidationDomain")]
		[Bindable(true)]
		public string ValidationDomain 
		{
			get
			{
				return GetColumnValue<string>("ValidationDomain");
			}
			set
			{
				this.OnValidationDomainChanging(value);
				this.OnPropertyChanging("ValidationDomain", value);
				string oldValue = this.ValidationDomain;
				SetColumnValue("ValidationDomain", value);
				this.OnValidationDomainChanged(oldValue, value);
				this.OnPropertyChanged("ValidationDomain", oldValue, value);
			}
		}
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string Id = @"Id";
			
			public static string RowStatusName = @"RowStatusName";
			
			public static string ProxyURLStatusName = @"ProxyURLStatusName";
			
			public static string PersonName = @"PersonName";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string Description = @"Description";
			
			public static string ProxyURLStatusId = @"ProxyURLStatusId";
			
			public static string ProxyURLTypeId = @"ProxyURLTypeId";
			
			public static string OwnerId = @"OwnerId";
			
			public static string Url = @"URL";
			
			public static string ProxyURLTypeName = @"ProxyURLTypeName";
			
			public static string ProxyURLTypeElementsKey = @"ProxyURLTypeElementsKey";
			
			public static string TouchpointParameterValueQueryParameterValueId = @"TouchpointParameterValueQueryParameterValueId";
			
			public static string TouchpointParameterValueQueryParameterId = @"TouchpointParameterValueQueryParameterId";
			
			public static string TouchpointParameterValueName = @"TouchpointParameterValueName";
			
			public static string BrandParameterValueQueryParameterValueId = @"BrandParameterValueQueryParameterValueId";
			
			public static string BrandParameterValueQueryParameterId = @"BrandParameterValueQueryParameterId";
			
			public static string BrandParameterValueName = @"BrandParameterValueName";
			
			public static string LocaleParameterValueQueryParameterValueId = @"LocaleParameterValueQueryParameterValueId";
			
			public static string LocaleParameterValueQueryParameterId = @"LocaleParameterValueQueryParameterId";
			
			public static string LocaleParameterValueName = @"LocaleParameterValueName";
			
			public static string CycleParameterValueQueryParameterValueId = @"CycleParameterValueQueryParameterValueId";
			
			public static string CycleParameterValueQueryParameterId = @"CycleParameterValueQueryParameterId";
			
			public static string CycleParameterValueName = @"CycleParameterValueName";
			
			public static string PlatformParameterValueQueryParameterValueId = @"PlatformParameterValueQueryParameterValueId";
			
			public static string PlatformParameterValueQueryParameterId = @"PlatformParameterValueQueryParameterId";
			
			public static string PlatformParameterValueName = @"PlatformParameterValueName";
			
			public static string PartnerCategoryParameterValueQueryParameterValueId = @"PartnerCategoryParameterValueQueryParameterValueId";
			
			public static string PartnerCategoryParameterValueQueryParameterId = @"PartnerCategoryParameterValueQueryParameterId";
			
			public static string PartnerCategoryParameterValueName = @"PartnerCategoryParameterValueName";
			
			public static string Tags = @"Tags";
			
			public static string TagCount = @"TagCount";
			
			public static string ProductionId = @"ProductionId";
			
			public static string ValidationId = @"ValidationId";
			
			public static string QueryString = @"QueryString";
			
			public static string ProductionDomain = @"ProductionDomain";
			
			public static string ValidationDomain = @"ValidationDomain";
			
		}
		#endregion
		#region IAbstractRecord Members
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public new CT GetColumnValue<CT>(string columnName) {
			return base.GetColumnValue<CT>(columnName);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public object GetColumnValue(string columnName) {
			return base.GetColumnValue<object>(columnName);
		}
		#endregion
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="VwMapProxyURL"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapProxyURL#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapProxyURL"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapProxyURL"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapProxyURL"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapProxyURL instance1 = this;
			VwMapProxyURL instance2 = obj as VwMapProxyURL;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.ProxyURLStatusName == instance2.ProxyURLStatusName)
			
				&& (instance1.PersonName == instance2.PersonName)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.ProxyURLStatusId == instance2.ProxyURLStatusId)
			
				&& (instance1.ProxyURLTypeId == instance2.ProxyURLTypeId)
			
				&& (instance1.OwnerId == instance2.OwnerId)
			
				&& (instance1.Url == instance2.Url)
			
				&& (instance1.ProxyURLTypeName == instance2.ProxyURLTypeName)
			
				&& (instance1.ProxyURLTypeElementsKey == instance2.ProxyURLTypeElementsKey)
			
				&& (instance1.TouchpointParameterValueQueryParameterValueId == instance2.TouchpointParameterValueQueryParameterValueId)
			
				&& (instance1.TouchpointParameterValueQueryParameterId == instance2.TouchpointParameterValueQueryParameterId)
			
				&& (instance1.TouchpointParameterValueName == instance2.TouchpointParameterValueName)
			
				&& (instance1.BrandParameterValueQueryParameterValueId == instance2.BrandParameterValueQueryParameterValueId)
			
				&& (instance1.BrandParameterValueQueryParameterId == instance2.BrandParameterValueQueryParameterId)
			
				&& (instance1.BrandParameterValueName == instance2.BrandParameterValueName)
			
				&& (instance1.LocaleParameterValueQueryParameterValueId == instance2.LocaleParameterValueQueryParameterValueId)
			
				&& (instance1.LocaleParameterValueQueryParameterId == instance2.LocaleParameterValueQueryParameterId)
			
				&& (instance1.LocaleParameterValueName == instance2.LocaleParameterValueName)
			
				&& (instance1.CycleParameterValueQueryParameterValueId == instance2.CycleParameterValueQueryParameterValueId)
			
				&& (instance1.CycleParameterValueQueryParameterId == instance2.CycleParameterValueQueryParameterId)
			
				&& (instance1.CycleParameterValueName == instance2.CycleParameterValueName)
			
				&& (instance1.PlatformParameterValueQueryParameterValueId == instance2.PlatformParameterValueQueryParameterValueId)
			
				&& (instance1.PlatformParameterValueQueryParameterId == instance2.PlatformParameterValueQueryParameterId)
			
				&& (instance1.PlatformParameterValueName == instance2.PlatformParameterValueName)
			
				&& (instance1.PartnerCategoryParameterValueQueryParameterValueId == instance2.PartnerCategoryParameterValueQueryParameterValueId)
			
				&& (instance1.PartnerCategoryParameterValueQueryParameterId == instance2.PartnerCategoryParameterValueQueryParameterId)
			
				&& (instance1.PartnerCategoryParameterValueName == instance2.PartnerCategoryParameterValueName)
			
				&& (instance1.Tags == instance2.Tags)
			
				&& (instance1.TagCount == instance2.TagCount)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ValidationId == instance2.ValidationId)
			
				&& (instance1.QueryString == instance2.QueryString)
			
				&& (instance1.ProductionDomain == instance2.ProductionDomain)
			
				&& (instance1.ValidationDomain == instance2.ValidationDomain)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapProxyURL"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapProxyURL"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapProxyURL"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapProxyURL instance1, VwMapProxyURL instance2)
		{
			if (instance1 == null)
			{
				return (instance2 == null);
			}
			return instance1.Equals(instance2);
		}
*/
		#endregion
	}
}
