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
	/// Strongly-typed collection for the VwMapWorkflow class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowCollection : ReadOnlyList<VwMapWorkflow, VwMapWorkflowCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapWorkflow);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapWorkflow class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowController : BaseReadOnlyRecordController<VwMapWorkflow, VwMapWorkflowCollection, VwMapWorkflowController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapWorkflow.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapWorkflow view.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflow : ReadOnlyRecord<VwMapWorkflow>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapWorkflow", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarAppClientId = new TableSchema.TableColumn(schema);
				colvarAppClientId.ColumnName = "AppClientId";
				colvarAppClientId.DataType = DbType.Int32;
				colvarAppClientId.MaxLength = 0;
				colvarAppClientId.AutoIncrement = false;
				colvarAppClientId.IsNullable = false;
				colvarAppClientId.IsPrimaryKey = false;
				colvarAppClientId.IsForeignKey = false;
				colvarAppClientId.IsReadOnly = false;
				
				schema.Columns.Add(colvarAppClientId);
				
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
				
				TableSchema.TableColumn colvarWorkflowStatusName = new TableSchema.TableColumn(schema);
				colvarWorkflowStatusName.ColumnName = "WorkflowStatusName";
				colvarWorkflowStatusName.DataType = DbType.String;
				colvarWorkflowStatusName.MaxLength = 256;
				colvarWorkflowStatusName.AutoIncrement = false;
				colvarWorkflowStatusName.IsNullable = false;
				colvarWorkflowStatusName.IsPrimaryKey = false;
				colvarWorkflowStatusName.IsForeignKey = false;
				colvarWorkflowStatusName.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowStatusName);
				
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
				
				TableSchema.TableColumn colvarWorkflowStatusId = new TableSchema.TableColumn(schema);
				colvarWorkflowStatusId.ColumnName = "WorkflowStatusId";
				colvarWorkflowStatusId.DataType = DbType.Int32;
				colvarWorkflowStatusId.MaxLength = 0;
				colvarWorkflowStatusId.AutoIncrement = false;
				colvarWorkflowStatusId.IsNullable = false;
				colvarWorkflowStatusId.IsPrimaryKey = false;
				colvarWorkflowStatusId.IsForeignKey = false;
				colvarWorkflowStatusId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowStatusId);
				
				TableSchema.TableColumn colvarWorkflowTypeId = new TableSchema.TableColumn(schema);
				colvarWorkflowTypeId.ColumnName = "WorkflowTypeId";
				colvarWorkflowTypeId.DataType = DbType.Int32;
				colvarWorkflowTypeId.MaxLength = 0;
				colvarWorkflowTypeId.AutoIncrement = false;
				colvarWorkflowTypeId.IsNullable = false;
				colvarWorkflowTypeId.IsPrimaryKey = false;
				colvarWorkflowTypeId.IsForeignKey = false;
				colvarWorkflowTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowTypeId);
				
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
				
				TableSchema.TableColumn colvarWorkflowTypeName = new TableSchema.TableColumn(schema);
				colvarWorkflowTypeName.ColumnName = "WorkflowTypeName";
				colvarWorkflowTypeName.DataType = DbType.String;
				colvarWorkflowTypeName.MaxLength = 256;
				colvarWorkflowTypeName.AutoIncrement = false;
				colvarWorkflowTypeName.IsNullable = false;
				colvarWorkflowTypeName.IsPrimaryKey = false;
				colvarWorkflowTypeName.IsForeignKey = false;
				colvarWorkflowTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowTypeName);
				
				TableSchema.TableColumn colvarReleaseQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarReleaseQueryParameterValue.ColumnName = "ReleaseQueryParameterValue";
				colvarReleaseQueryParameterValue.DataType = DbType.String;
				colvarReleaseQueryParameterValue.MaxLength = -1;
				colvarReleaseQueryParameterValue.AutoIncrement = false;
				colvarReleaseQueryParameterValue.IsNullable = true;
				colvarReleaseQueryParameterValue.IsPrimaryKey = false;
				colvarReleaseQueryParameterValue.IsForeignKey = false;
				colvarReleaseQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarReleaseQueryParameterValue);
				
				TableSchema.TableColumn colvarCountryQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarCountryQueryParameterValue.ColumnName = "CountryQueryParameterValue";
				colvarCountryQueryParameterValue.DataType = DbType.String;
				colvarCountryQueryParameterValue.MaxLength = -1;
				colvarCountryQueryParameterValue.AutoIncrement = false;
				colvarCountryQueryParameterValue.IsNullable = true;
				colvarCountryQueryParameterValue.IsPrimaryKey = false;
				colvarCountryQueryParameterValue.IsForeignKey = false;
				colvarCountryQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarCountryQueryParameterValue);
				
				TableSchema.TableColumn colvarPlatformQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarPlatformQueryParameterValue.ColumnName = "PlatformQueryParameterValue";
				colvarPlatformQueryParameterValue.DataType = DbType.String;
				colvarPlatformQueryParameterValue.MaxLength = -1;
				colvarPlatformQueryParameterValue.AutoIncrement = false;
				colvarPlatformQueryParameterValue.IsNullable = true;
				colvarPlatformQueryParameterValue.IsPrimaryKey = false;
				colvarPlatformQueryParameterValue.IsForeignKey = false;
				colvarPlatformQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarPlatformQueryParameterValue);
				
				TableSchema.TableColumn colvarBrandQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarBrandQueryParameterValue.ColumnName = "BrandQueryParameterValue";
				colvarBrandQueryParameterValue.DataType = DbType.String;
				colvarBrandQueryParameterValue.MaxLength = -1;
				colvarBrandQueryParameterValue.AutoIncrement = false;
				colvarBrandQueryParameterValue.IsNullable = true;
				colvarBrandQueryParameterValue.IsPrimaryKey = false;
				colvarBrandQueryParameterValue.IsForeignKey = false;
				colvarBrandQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarBrandQueryParameterValue);
				
				TableSchema.TableColumn colvarModelNumberQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarModelNumberQueryParameterValue.ColumnName = "ModelNumberQueryParameterValue";
				colvarModelNumberQueryParameterValue.DataType = DbType.String;
				colvarModelNumberQueryParameterValue.MaxLength = -1;
				colvarModelNumberQueryParameterValue.AutoIncrement = false;
				colvarModelNumberQueryParameterValue.IsNullable = true;
				colvarModelNumberQueryParameterValue.IsPrimaryKey = false;
				colvarModelNumberQueryParameterValue.IsForeignKey = false;
				colvarModelNumberQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarModelNumberQueryParameterValue);
				
				TableSchema.TableColumn colvarSubBrandQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarSubBrandQueryParameterValue.ColumnName = "SubBrandQueryParameterValue";
				colvarSubBrandQueryParameterValue.DataType = DbType.String;
				colvarSubBrandQueryParameterValue.MaxLength = -1;
				colvarSubBrandQueryParameterValue.AutoIncrement = false;
				colvarSubBrandQueryParameterValue.IsNullable = true;
				colvarSubBrandQueryParameterValue.IsPrimaryKey = false;
				colvarSubBrandQueryParameterValue.IsForeignKey = false;
				colvarSubBrandQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarSubBrandQueryParameterValue);
				
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
				
				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 256;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				
				schema.Columns.Add(colvarName);
				
				TableSchema.TableColumn colvarWorkflowApplicationId = new TableSchema.TableColumn(schema);
				colvarWorkflowApplicationId.ColumnName = "WorkflowApplicationId";
				colvarWorkflowApplicationId.DataType = DbType.Int32;
				colvarWorkflowApplicationId.MaxLength = 0;
				colvarWorkflowApplicationId.AutoIncrement = false;
				colvarWorkflowApplicationId.IsNullable = false;
				colvarWorkflowApplicationId.IsPrimaryKey = false;
				colvarWorkflowApplicationId.IsForeignKey = false;
				colvarWorkflowApplicationId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowApplicationId);
				
				TableSchema.TableColumn colvarWorkflowApplicationName = new TableSchema.TableColumn(schema);
				colvarWorkflowApplicationName.ColumnName = "WorkflowApplicationName";
				colvarWorkflowApplicationName.DataType = DbType.String;
				colvarWorkflowApplicationName.MaxLength = 256;
				colvarWorkflowApplicationName.AutoIncrement = false;
				colvarWorkflowApplicationName.IsNullable = false;
				colvarWorkflowApplicationName.IsPrimaryKey = false;
				colvarWorkflowApplicationName.IsForeignKey = false;
				colvarWorkflowApplicationName.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowApplicationName);
				
				TableSchema.TableColumn colvarOffline = new TableSchema.TableColumn(schema);
				colvarOffline.ColumnName = "Offline";
				colvarOffline.DataType = DbType.Boolean;
				colvarOffline.MaxLength = 0;
				colvarOffline.AutoIncrement = false;
				colvarOffline.IsNullable = false;
				colvarOffline.IsPrimaryKey = false;
				colvarOffline.IsForeignKey = false;
				colvarOffline.IsReadOnly = false;
				
				schema.Columns.Add(colvarOffline);
				
				TableSchema.TableColumn colvarVersionMajor = new TableSchema.TableColumn(schema);
				colvarVersionMajor.ColumnName = "VersionMajor";
				colvarVersionMajor.DataType = DbType.Int32;
				colvarVersionMajor.MaxLength = 0;
				colvarVersionMajor.AutoIncrement = false;
				colvarVersionMajor.IsNullable = false;
				colvarVersionMajor.IsPrimaryKey = false;
				colvarVersionMajor.IsForeignKey = false;
				colvarVersionMajor.IsReadOnly = false;
				
				schema.Columns.Add(colvarVersionMajor);
				
				TableSchema.TableColumn colvarVersionMinor = new TableSchema.TableColumn(schema);
				colvarVersionMinor.ColumnName = "VersionMinor";
				colvarVersionMinor.DataType = DbType.Int32;
				colvarVersionMinor.MaxLength = 0;
				colvarVersionMinor.AutoIncrement = false;
				colvarVersionMinor.IsNullable = false;
				colvarVersionMinor.IsPrimaryKey = false;
				colvarVersionMinor.IsForeignKey = false;
				colvarVersionMinor.IsReadOnly = false;
				
				schema.Columns.Add(colvarVersionMinor);
				
				TableSchema.TableColumn colvarFilename = new TableSchema.TableColumn(schema);
				colvarFilename.ColumnName = "Filename";
				colvarFilename.DataType = DbType.String;
				colvarFilename.MaxLength = 256;
				colvarFilename.AutoIncrement = false;
				colvarFilename.IsNullable = true;
				colvarFilename.IsPrimaryKey = false;
				colvarFilename.IsForeignKey = false;
				colvarFilename.IsReadOnly = false;
				
				schema.Columns.Add(colvarFilename);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapWorkflow",schema);
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
		protected VwMapWorkflow(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflow() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflow(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflow(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflow(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapWorkflow"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapWorkflow(VwMapWorkflow original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapWorkflow original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.AppClientId = original.AppClientId;
			
			this.RowStatusName = original.RowStatusName;
			
			this.WorkflowStatusName = original.WorkflowStatusName;
			
			this.PersonName = original.PersonName;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.Description = original.Description;
			
			this.WorkflowStatusId = original.WorkflowStatusId;
			
			this.WorkflowTypeId = original.WorkflowTypeId;
			
			this.OwnerId = original.OwnerId;
			
			this.WorkflowTypeName = original.WorkflowTypeName;
			
			this.ReleaseQueryParameterValue = original.ReleaseQueryParameterValue;
			
			this.CountryQueryParameterValue = original.CountryQueryParameterValue;
			
			this.PlatformQueryParameterValue = original.PlatformQueryParameterValue;
			
			this.BrandQueryParameterValue = original.BrandQueryParameterValue;
			
			this.ModelNumberQueryParameterValue = original.ModelNumberQueryParameterValue;
			
			this.SubBrandQueryParameterValue = original.SubBrandQueryParameterValue;
			
			this.Tags = original.Tags;
			
			this.TagCount = original.TagCount;
			
			this.ProductionId = original.ProductionId;
			
			this.ValidationId = original.ValidationId;
			
			this.Name = original.Name;
			
			this.WorkflowApplicationId = original.WorkflowApplicationId;
			
			this.WorkflowApplicationName = original.WorkflowApplicationName;
			
			this.Offline = original.Offline;
			
			this.VersionMajor = original.VersionMajor;
			
			this.VersionMinor = original.VersionMinor;
			
			this.Filename = original.Filename;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapWorkflow"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapWorkflow Copy(VwMapWorkflow original)
		{
			return new VwMapWorkflow(original);
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
		partial void OnAppClientIdChanging(int newValue);
		partial void OnAppClientIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("AppClientId")]
		[Bindable(true)]
		public int AppClientId 
		{
			get
			{
				return GetColumnValue<int>("AppClientId");
			}
			set
			{
				this.OnAppClientIdChanging(value);
				this.OnPropertyChanging("AppClientId", value);
				int oldValue = this.AppClientId;
				SetColumnValue("AppClientId", value);
				this.OnAppClientIdChanged(oldValue, value);
				this.OnPropertyChanged("AppClientId", oldValue, value);
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
		partial void OnWorkflowStatusNameChanging(string newValue);
		partial void OnWorkflowStatusNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowStatusName")]
		[Bindable(true)]
		public string WorkflowStatusName 
		{
			get
			{
				return GetColumnValue<string>("WorkflowStatusName");
			}
			set
			{
				this.OnWorkflowStatusNameChanging(value);
				this.OnPropertyChanging("WorkflowStatusName", value);
				string oldValue = this.WorkflowStatusName;
				SetColumnValue("WorkflowStatusName", value);
				this.OnWorkflowStatusNameChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowStatusName", oldValue, value);
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
		partial void OnWorkflowStatusIdChanging(int newValue);
		partial void OnWorkflowStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowStatusId")]
		[Bindable(true)]
		public int WorkflowStatusId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowStatusId");
			}
			set
			{
				this.OnWorkflowStatusIdChanging(value);
				this.OnPropertyChanging("WorkflowStatusId", value);
				int oldValue = this.WorkflowStatusId;
				SetColumnValue("WorkflowStatusId", value);
				this.OnWorkflowStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowStatusId", oldValue, value);
			}
		}
		partial void OnWorkflowTypeIdChanging(int newValue);
		partial void OnWorkflowTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowTypeId")]
		[Bindable(true)]
		public int WorkflowTypeId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowTypeId");
			}
			set
			{
				this.OnWorkflowTypeIdChanging(value);
				this.OnPropertyChanging("WorkflowTypeId", value);
				int oldValue = this.WorkflowTypeId;
				SetColumnValue("WorkflowTypeId", value);
				this.OnWorkflowTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowTypeId", oldValue, value);
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
		partial void OnWorkflowTypeNameChanging(string newValue);
		partial void OnWorkflowTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowTypeName")]
		[Bindable(true)]
		public string WorkflowTypeName 
		{
			get
			{
				return GetColumnValue<string>("WorkflowTypeName");
			}
			set
			{
				this.OnWorkflowTypeNameChanging(value);
				this.OnPropertyChanging("WorkflowTypeName", value);
				string oldValue = this.WorkflowTypeName;
				SetColumnValue("WorkflowTypeName", value);
				this.OnWorkflowTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowTypeName", oldValue, value);
			}
		}
		partial void OnReleaseQueryParameterValueChanging(string newValue);
		partial void OnReleaseQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ReleaseQueryParameterValue")]
		[Bindable(true)]
		public string ReleaseQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("ReleaseQueryParameterValue");
			}
			set
			{
				this.OnReleaseQueryParameterValueChanging(value);
				this.OnPropertyChanging("ReleaseQueryParameterValue", value);
				string oldValue = this.ReleaseQueryParameterValue;
				SetColumnValue("ReleaseQueryParameterValue", value);
				this.OnReleaseQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("ReleaseQueryParameterValue", oldValue, value);
			}
		}
		partial void OnCountryQueryParameterValueChanging(string newValue);
		partial void OnCountryQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CountryQueryParameterValue")]
		[Bindable(true)]
		public string CountryQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("CountryQueryParameterValue");
			}
			set
			{
				this.OnCountryQueryParameterValueChanging(value);
				this.OnPropertyChanging("CountryQueryParameterValue", value);
				string oldValue = this.CountryQueryParameterValue;
				SetColumnValue("CountryQueryParameterValue", value);
				this.OnCountryQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("CountryQueryParameterValue", oldValue, value);
			}
		}
		partial void OnPlatformQueryParameterValueChanging(string newValue);
		partial void OnPlatformQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PlatformQueryParameterValue")]
		[Bindable(true)]
		public string PlatformQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("PlatformQueryParameterValue");
			}
			set
			{
				this.OnPlatformQueryParameterValueChanging(value);
				this.OnPropertyChanging("PlatformQueryParameterValue", value);
				string oldValue = this.PlatformQueryParameterValue;
				SetColumnValue("PlatformQueryParameterValue", value);
				this.OnPlatformQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("PlatformQueryParameterValue", oldValue, value);
			}
		}
		partial void OnBrandQueryParameterValueChanging(string newValue);
		partial void OnBrandQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("BrandQueryParameterValue")]
		[Bindable(true)]
		public string BrandQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("BrandQueryParameterValue");
			}
			set
			{
				this.OnBrandQueryParameterValueChanging(value);
				this.OnPropertyChanging("BrandQueryParameterValue", value);
				string oldValue = this.BrandQueryParameterValue;
				SetColumnValue("BrandQueryParameterValue", value);
				this.OnBrandQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("BrandQueryParameterValue", oldValue, value);
			}
		}
		partial void OnModelNumberQueryParameterValueChanging(string newValue);
		partial void OnModelNumberQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ModelNumberQueryParameterValue")]
		[Bindable(true)]
		public string ModelNumberQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("ModelNumberQueryParameterValue");
			}
			set
			{
				this.OnModelNumberQueryParameterValueChanging(value);
				this.OnPropertyChanging("ModelNumberQueryParameterValue", value);
				string oldValue = this.ModelNumberQueryParameterValue;
				SetColumnValue("ModelNumberQueryParameterValue", value);
				this.OnModelNumberQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("ModelNumberQueryParameterValue", oldValue, value);
			}
		}
		partial void OnSubBrandQueryParameterValueChanging(string newValue);
		partial void OnSubBrandQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("SubBrandQueryParameterValue")]
		[Bindable(true)]
		public string SubBrandQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("SubBrandQueryParameterValue");
			}
			set
			{
				this.OnSubBrandQueryParameterValueChanging(value);
				this.OnPropertyChanging("SubBrandQueryParameterValue", value);
				string oldValue = this.SubBrandQueryParameterValue;
				SetColumnValue("SubBrandQueryParameterValue", value);
				this.OnSubBrandQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("SubBrandQueryParameterValue", oldValue, value);
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
		partial void OnNameChanging(string newValue);
		partial void OnNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Name")]
		[Bindable(true)]
		public string Name 
		{
			get
			{
				return GetColumnValue<string>("Name");
			}
			set
			{
				this.OnNameChanging(value);
				this.OnPropertyChanging("Name", value);
				string oldValue = this.Name;
				SetColumnValue("Name", value);
				this.OnNameChanged(oldValue, value);
				this.OnPropertyChanged("Name", oldValue, value);
			}
		}
		partial void OnWorkflowApplicationIdChanging(int newValue);
		partial void OnWorkflowApplicationIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowApplicationId")]
		[Bindable(true)]
		public int WorkflowApplicationId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowApplicationId");
			}
			set
			{
				this.OnWorkflowApplicationIdChanging(value);
				this.OnPropertyChanging("WorkflowApplicationId", value);
				int oldValue = this.WorkflowApplicationId;
				SetColumnValue("WorkflowApplicationId", value);
				this.OnWorkflowApplicationIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowApplicationId", oldValue, value);
			}
		}
		partial void OnWorkflowApplicationNameChanging(string newValue);
		partial void OnWorkflowApplicationNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowApplicationName")]
		[Bindable(true)]
		public string WorkflowApplicationName 
		{
			get
			{
				return GetColumnValue<string>("WorkflowApplicationName");
			}
			set
			{
				this.OnWorkflowApplicationNameChanging(value);
				this.OnPropertyChanging("WorkflowApplicationName", value);
				string oldValue = this.WorkflowApplicationName;
				SetColumnValue("WorkflowApplicationName", value);
				this.OnWorkflowApplicationNameChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowApplicationName", oldValue, value);
			}
		}
		partial void OnOfflineChanging(bool newValue);
		partial void OnOfflineChanged(bool oldValue, bool newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Offline")]
		[Bindable(true)]
		public bool Offline 
		{
			get
			{
				return GetColumnValue<bool>("Offline");
			}
			set
			{
				this.OnOfflineChanging(value);
				this.OnPropertyChanging("Offline", value);
				bool oldValue = this.Offline;
				SetColumnValue("Offline", value);
				this.OnOfflineChanged(oldValue, value);
				this.OnPropertyChanged("Offline", oldValue, value);
			}
		}
		partial void OnVersionMajorChanging(int newValue);
		partial void OnVersionMajorChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("VersionMajor")]
		[Bindable(true)]
		public int VersionMajor 
		{
			get
			{
				return GetColumnValue<int>("VersionMajor");
			}
			set
			{
				this.OnVersionMajorChanging(value);
				this.OnPropertyChanging("VersionMajor", value);
				int oldValue = this.VersionMajor;
				SetColumnValue("VersionMajor", value);
				this.OnVersionMajorChanged(oldValue, value);
				this.OnPropertyChanged("VersionMajor", oldValue, value);
			}
		}
		partial void OnVersionMinorChanging(int newValue);
		partial void OnVersionMinorChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("VersionMinor")]
		[Bindable(true)]
		public int VersionMinor 
		{
			get
			{
				return GetColumnValue<int>("VersionMinor");
			}
			set
			{
				this.OnVersionMinorChanging(value);
				this.OnPropertyChanging("VersionMinor", value);
				int oldValue = this.VersionMinor;
				SetColumnValue("VersionMinor", value);
				this.OnVersionMinorChanged(oldValue, value);
				this.OnPropertyChanged("VersionMinor", oldValue, value);
			}
		}
		partial void OnFilenameChanging(string newValue);
		partial void OnFilenameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Filename")]
		[Bindable(true)]
		public string Filename 
		{
			get
			{
				return GetColumnValue<string>("Filename");
			}
			set
			{
				this.OnFilenameChanging(value);
				this.OnPropertyChanging("Filename", value);
				string oldValue = this.Filename;
				SetColumnValue("Filename", value);
				this.OnFilenameChanged(oldValue, value);
				this.OnPropertyChanged("Filename", oldValue, value);
			}
		}
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string Id = @"Id";
			
			public static string AppClientId = @"AppClientId";
			
			public static string RowStatusName = @"RowStatusName";
			
			public static string WorkflowStatusName = @"WorkflowStatusName";
			
			public static string PersonName = @"PersonName";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string Description = @"Description";
			
			public static string WorkflowStatusId = @"WorkflowStatusId";
			
			public static string WorkflowTypeId = @"WorkflowTypeId";
			
			public static string OwnerId = @"OwnerId";
			
			public static string WorkflowTypeName = @"WorkflowTypeName";
			
			public static string ReleaseQueryParameterValue = @"ReleaseQueryParameterValue";
			
			public static string CountryQueryParameterValue = @"CountryQueryParameterValue";
			
			public static string PlatformQueryParameterValue = @"PlatformQueryParameterValue";
			
			public static string BrandQueryParameterValue = @"BrandQueryParameterValue";
			
			public static string ModelNumberQueryParameterValue = @"ModelNumberQueryParameterValue";
			
			public static string SubBrandQueryParameterValue = @"SubBrandQueryParameterValue";
			
			public static string Tags = @"Tags";
			
			public static string TagCount = @"TagCount";
			
			public static string ProductionId = @"ProductionId";
			
			public static string ValidationId = @"ValidationId";
			
			public static string Name = @"Name";
			
			public static string WorkflowApplicationId = @"WorkflowApplicationId";
			
			public static string WorkflowApplicationName = @"WorkflowApplicationName";
			
			public static string Offline = @"Offline";
			
			public static string VersionMajor = @"VersionMajor";
			
			public static string VersionMinor = @"VersionMinor";
			
			public static string Filename = @"Filename";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapWorkflow"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapWorkflow#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflow"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapWorkflow"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflow"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapWorkflow instance1 = this;
			VwMapWorkflow instance2 = obj as VwMapWorkflow;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.AppClientId == instance2.AppClientId)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.WorkflowStatusName == instance2.WorkflowStatusName)
			
				&& (instance1.PersonName == instance2.PersonName)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.WorkflowStatusId == instance2.WorkflowStatusId)
			
				&& (instance1.WorkflowTypeId == instance2.WorkflowTypeId)
			
				&& (instance1.OwnerId == instance2.OwnerId)
			
				&& (instance1.WorkflowTypeName == instance2.WorkflowTypeName)
			
				&& (instance1.ReleaseQueryParameterValue == instance2.ReleaseQueryParameterValue)
			
				&& (instance1.CountryQueryParameterValue == instance2.CountryQueryParameterValue)
			
				&& (instance1.PlatformQueryParameterValue == instance2.PlatformQueryParameterValue)
			
				&& (instance1.BrandQueryParameterValue == instance2.BrandQueryParameterValue)
			
				&& (instance1.ModelNumberQueryParameterValue == instance2.ModelNumberQueryParameterValue)
			
				&& (instance1.SubBrandQueryParameterValue == instance2.SubBrandQueryParameterValue)
			
				&& (instance1.Tags == instance2.Tags)
			
				&& (instance1.TagCount == instance2.TagCount)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ValidationId == instance2.ValidationId)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.WorkflowApplicationId == instance2.WorkflowApplicationId)
			
				&& (instance1.WorkflowApplicationName == instance2.WorkflowApplicationName)
			
				&& (instance1.Offline == instance2.Offline)
			
				&& (instance1.VersionMajor == instance2.VersionMajor)
			
				&& (instance1.VersionMinor == instance2.VersionMinor)
			
				&& (instance1.Filename == instance2.Filename)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapWorkflow"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapWorkflow"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapWorkflow"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapWorkflow instance1, VwMapWorkflow instance2)
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
