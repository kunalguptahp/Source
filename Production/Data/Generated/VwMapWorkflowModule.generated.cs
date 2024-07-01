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
	/// Strongly-typed collection for the VwMapWorkflowModule class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModuleCollection : ReadOnlyList<VwMapWorkflowModule, VwMapWorkflowModuleCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapWorkflowModule);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapWorkflowModule class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModuleController : BaseReadOnlyRecordController<VwMapWorkflowModule, VwMapWorkflowModuleCollection, VwMapWorkflowModuleController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapWorkflowModule.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapWorkflowModule view.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModule : ReadOnlyRecord<VwMapWorkflowModule>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapWorkflowModule", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = 512;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				
				schema.Columns.Add(colvarDescription);
				
				TableSchema.TableColumn colvarFilename = new TableSchema.TableColumn(schema);
				colvarFilename.ColumnName = "Filename";
				colvarFilename.DataType = DbType.String;
				colvarFilename.MaxLength = 256;
				colvarFilename.AutoIncrement = false;
				colvarFilename.IsNullable = false;
				colvarFilename.IsPrimaryKey = false;
				colvarFilename.IsForeignKey = false;
				colvarFilename.IsReadOnly = false;
				
				schema.Columns.Add(colvarFilename);
				
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
				
				TableSchema.TableColumn colvarWorkflowModuleStatusId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleStatusId.ColumnName = "WorkflowModuleStatusId";
				colvarWorkflowModuleStatusId.DataType = DbType.Int32;
				colvarWorkflowModuleStatusId.MaxLength = 0;
				colvarWorkflowModuleStatusId.AutoIncrement = false;
				colvarWorkflowModuleStatusId.IsNullable = false;
				colvarWorkflowModuleStatusId.IsPrimaryKey = false;
				colvarWorkflowModuleStatusId.IsForeignKey = false;
				colvarWorkflowModuleStatusId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleStatusId);
				
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
				
				TableSchema.TableColumn colvarWorkflowModuleCategoryId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleCategoryId.ColumnName = "WorkflowModuleCategoryId";
				colvarWorkflowModuleCategoryId.DataType = DbType.Int32;
				colvarWorkflowModuleCategoryId.MaxLength = 0;
				colvarWorkflowModuleCategoryId.AutoIncrement = false;
				colvarWorkflowModuleCategoryId.IsNullable = false;
				colvarWorkflowModuleCategoryId.IsPrimaryKey = false;
				colvarWorkflowModuleCategoryId.IsForeignKey = false;
				colvarWorkflowModuleCategoryId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleCategoryId);
				
				TableSchema.TableColumn colvarWorkflowModuleSubCategoryId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleSubCategoryId.ColumnName = "WorkflowModuleSubCategoryId";
				colvarWorkflowModuleSubCategoryId.DataType = DbType.Int32;
				colvarWorkflowModuleSubCategoryId.MaxLength = 0;
				colvarWorkflowModuleSubCategoryId.AutoIncrement = false;
				colvarWorkflowModuleSubCategoryId.IsNullable = false;
				colvarWorkflowModuleSubCategoryId.IsPrimaryKey = false;
				colvarWorkflowModuleSubCategoryId.IsForeignKey = false;
				colvarWorkflowModuleSubCategoryId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleSubCategoryId);
				
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
				
				TableSchema.TableColumn colvarModuleSubCategoryName = new TableSchema.TableColumn(schema);
				colvarModuleSubCategoryName.ColumnName = "ModuleSubCategoryName";
				colvarModuleSubCategoryName.DataType = DbType.String;
				colvarModuleSubCategoryName.MaxLength = 256;
				colvarModuleSubCategoryName.AutoIncrement = false;
				colvarModuleSubCategoryName.IsNullable = false;
				colvarModuleSubCategoryName.IsPrimaryKey = false;
				colvarModuleSubCategoryName.IsForeignKey = false;
				colvarModuleSubCategoryName.IsReadOnly = false;
				
				schema.Columns.Add(colvarModuleSubCategoryName);
				
				TableSchema.TableColumn colvarModuleCategoryName = new TableSchema.TableColumn(schema);
				colvarModuleCategoryName.ColumnName = "ModuleCategoryName";
				colvarModuleCategoryName.DataType = DbType.String;
				colvarModuleCategoryName.MaxLength = 256;
				colvarModuleCategoryName.AutoIncrement = false;
				colvarModuleCategoryName.IsNullable = false;
				colvarModuleCategoryName.IsPrimaryKey = false;
				colvarModuleCategoryName.IsForeignKey = false;
				colvarModuleCategoryName.IsReadOnly = false;
				
				schema.Columns.Add(colvarModuleCategoryName);
				
				TableSchema.TableColumn colvarWorkflowModuleStatusName = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleStatusName.ColumnName = "WorkflowModuleStatusName";
				colvarWorkflowModuleStatusName.DataType = DbType.String;
				colvarWorkflowModuleStatusName.MaxLength = 256;
				colvarWorkflowModuleStatusName.AutoIncrement = false;
				colvarWorkflowModuleStatusName.IsNullable = false;
				colvarWorkflowModuleStatusName.IsPrimaryKey = false;
				colvarWorkflowModuleStatusName.IsForeignKey = false;
				colvarWorkflowModuleStatusName.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleStatusName);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapWorkflowModule",schema);
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
		protected VwMapWorkflowModule(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModule() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModule(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModule(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModule(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapWorkflowModule"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapWorkflowModule(VwMapWorkflowModule original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapWorkflowModule original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.Name = original.Name;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.VersionMajor = original.VersionMajor;
			
			this.VersionMinor = original.VersionMinor;
			
			this.RowStatusId = original.RowStatusId;
			
			this.RowStatusName = original.RowStatusName;
			
			this.Description = original.Description;
			
			this.Filename = original.Filename;
			
			this.ProductionId = original.ProductionId;
			
			this.ValidationId = original.ValidationId;
			
			this.WorkflowModuleStatusId = original.WorkflowModuleStatusId;
			
			this.OwnerId = original.OwnerId;
			
			this.WorkflowModuleCategoryId = original.WorkflowModuleCategoryId;
			
			this.WorkflowModuleSubCategoryId = original.WorkflowModuleSubCategoryId;
			
			this.Tags = original.Tags;
			
			this.TagCount = original.TagCount;
			
			this.ModuleSubCategoryName = original.ModuleSubCategoryName;
			
			this.ModuleCategoryName = original.ModuleCategoryName;
			
			this.WorkflowModuleStatusName = original.WorkflowModuleStatusName;
			
			this.PersonName = original.PersonName;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapWorkflowModule"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapWorkflowModule Copy(VwMapWorkflowModule original)
		{
			return new VwMapWorkflowModule(original);
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
		partial void OnWorkflowModuleStatusIdChanging(int newValue);
		partial void OnWorkflowModuleStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleStatusId")]
		[Bindable(true)]
		public int WorkflowModuleStatusId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowModuleStatusId");
			}
			set
			{
				this.OnWorkflowModuleStatusIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleStatusId", value);
				int oldValue = this.WorkflowModuleStatusId;
				SetColumnValue("WorkflowModuleStatusId", value);
				this.OnWorkflowModuleStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleStatusId", oldValue, value);
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
		partial void OnWorkflowModuleCategoryIdChanging(int newValue);
		partial void OnWorkflowModuleCategoryIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleCategoryId")]
		[Bindable(true)]
		public int WorkflowModuleCategoryId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowModuleCategoryId");
			}
			set
			{
				this.OnWorkflowModuleCategoryIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleCategoryId", value);
				int oldValue = this.WorkflowModuleCategoryId;
				SetColumnValue("WorkflowModuleCategoryId", value);
				this.OnWorkflowModuleCategoryIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleCategoryId", oldValue, value);
			}
		}
		partial void OnWorkflowModuleSubCategoryIdChanging(int newValue);
		partial void OnWorkflowModuleSubCategoryIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleSubCategoryId")]
		[Bindable(true)]
		public int WorkflowModuleSubCategoryId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowModuleSubCategoryId");
			}
			set
			{
				this.OnWorkflowModuleSubCategoryIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleSubCategoryId", value);
				int oldValue = this.WorkflowModuleSubCategoryId;
				SetColumnValue("WorkflowModuleSubCategoryId", value);
				this.OnWorkflowModuleSubCategoryIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleSubCategoryId", oldValue, value);
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
		partial void OnModuleSubCategoryNameChanging(string newValue);
		partial void OnModuleSubCategoryNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ModuleSubCategoryName")]
		[Bindable(true)]
		public string ModuleSubCategoryName 
		{
			get
			{
				return GetColumnValue<string>("ModuleSubCategoryName");
			}
			set
			{
				this.OnModuleSubCategoryNameChanging(value);
				this.OnPropertyChanging("ModuleSubCategoryName", value);
				string oldValue = this.ModuleSubCategoryName;
				SetColumnValue("ModuleSubCategoryName", value);
				this.OnModuleSubCategoryNameChanged(oldValue, value);
				this.OnPropertyChanged("ModuleSubCategoryName", oldValue, value);
			}
		}
		partial void OnModuleCategoryNameChanging(string newValue);
		partial void OnModuleCategoryNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ModuleCategoryName")]
		[Bindable(true)]
		public string ModuleCategoryName 
		{
			get
			{
				return GetColumnValue<string>("ModuleCategoryName");
			}
			set
			{
				this.OnModuleCategoryNameChanging(value);
				this.OnPropertyChanging("ModuleCategoryName", value);
				string oldValue = this.ModuleCategoryName;
				SetColumnValue("ModuleCategoryName", value);
				this.OnModuleCategoryNameChanged(oldValue, value);
				this.OnPropertyChanged("ModuleCategoryName", oldValue, value);
			}
		}
		partial void OnWorkflowModuleStatusNameChanging(string newValue);
		partial void OnWorkflowModuleStatusNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleStatusName")]
		[Bindable(true)]
		public string WorkflowModuleStatusName 
		{
			get
			{
				return GetColumnValue<string>("WorkflowModuleStatusName");
			}
			set
			{
				this.OnWorkflowModuleStatusNameChanging(value);
				this.OnPropertyChanging("WorkflowModuleStatusName", value);
				string oldValue = this.WorkflowModuleStatusName;
				SetColumnValue("WorkflowModuleStatusName", value);
				this.OnWorkflowModuleStatusNameChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleStatusName", oldValue, value);
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
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string Id = @"Id";
			
			public static string Name = @"Name";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string VersionMajor = @"VersionMajor";
			
			public static string VersionMinor = @"VersionMinor";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string RowStatusName = @"RowStatusName";
			
			public static string Description = @"Description";
			
			public static string Filename = @"Filename";
			
			public static string ProductionId = @"ProductionId";
			
			public static string ValidationId = @"ValidationId";
			
			public static string WorkflowModuleStatusId = @"WorkflowModuleStatusId";
			
			public static string OwnerId = @"OwnerId";
			
			public static string WorkflowModuleCategoryId = @"WorkflowModuleCategoryId";
			
			public static string WorkflowModuleSubCategoryId = @"WorkflowModuleSubCategoryId";
			
			public static string Tags = @"Tags";
			
			public static string TagCount = @"TagCount";
			
			public static string ModuleSubCategoryName = @"ModuleSubCategoryName";
			
			public static string ModuleCategoryName = @"ModuleCategoryName";
			
			public static string WorkflowModuleStatusName = @"WorkflowModuleStatusName";
			
			public static string PersonName = @"PersonName";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapWorkflowModule"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapWorkflowModule#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowModule"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapWorkflowModule"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowModule"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapWorkflowModule instance1 = this;
			VwMapWorkflowModule instance2 = obj as VwMapWorkflowModule;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.VersionMajor == instance2.VersionMajor)
			
				&& (instance1.VersionMinor == instance2.VersionMinor)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.Filename == instance2.Filename)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ValidationId == instance2.ValidationId)
			
				&& (instance1.WorkflowModuleStatusId == instance2.WorkflowModuleStatusId)
			
				&& (instance1.OwnerId == instance2.OwnerId)
			
				&& (instance1.WorkflowModuleCategoryId == instance2.WorkflowModuleCategoryId)
			
				&& (instance1.WorkflowModuleSubCategoryId == instance2.WorkflowModuleSubCategoryId)
			
				&& (instance1.Tags == instance2.Tags)
			
				&& (instance1.TagCount == instance2.TagCount)
			
				&& (instance1.ModuleSubCategoryName == instance2.ModuleSubCategoryName)
			
				&& (instance1.ModuleCategoryName == instance2.ModuleCategoryName)
			
				&& (instance1.WorkflowModuleStatusName == instance2.WorkflowModuleStatusName)
			
				&& (instance1.PersonName == instance2.PersonName)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapWorkflowModule"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapWorkflowModule"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapWorkflowModule"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapWorkflowModule instance1, VwMapWorkflowModule instance2)
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
