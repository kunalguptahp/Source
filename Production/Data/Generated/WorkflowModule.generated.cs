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
	/// Strongly-typed collection for the WorkflowModule class.
	/// </summary>
    [Serializable]
	public partial class WorkflowModuleCollection : ActiveList<WorkflowModule, WorkflowModuleCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public WorkflowModuleCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(WorkflowModule);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the WorkflowModule table.
	/// </summary>
	[Serializable]
	public partial class WorkflowModule : ActiveRecord<WorkflowModule>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected WorkflowModule(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public WorkflowModule() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public WorkflowModule(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public WorkflowModule(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public WorkflowModule(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="WorkflowModule"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private WorkflowModule(WorkflowModule original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(WorkflowModule original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.Name = original.Name;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.VersionMajor = original.VersionMajor;
			
			this.VersionMinor = original.VersionMinor;
			
			this.Description = original.Description;
			
			this.Filename = original.Filename;
			
			this.ProductionId = original.ProductionId;
			
			this.ValidationId = original.ValidationId;
			
			this.WorkflowModuleStatusId = original.WorkflowModuleStatusId;
			
			this.OwnerId = original.OwnerId;
			
			this.WorkflowModuleCategoryId = original.WorkflowModuleCategoryId;
			
			this.WorkflowModuleSubCategoryId = original.WorkflowModuleSubCategoryId;
			
			this.Title = original.Title;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="WorkflowModule"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static WorkflowModule Copy(WorkflowModule original)
		{
			return new WorkflowModule(original);
		}
		#endregion
		#endregion
		
		#region Schema and Query Accessor
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Query CreateQuery() { return new Query(Schema); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("WorkflowModule", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
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
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
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
				colvarCreatedBy.DefaultSetting = @"";
				colvarCreatedBy.ForeignKeyTableName = "";
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
				colvarCreatedOn.DefaultSetting = @"";
				colvarCreatedOn.ForeignKeyTableName = "";
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
				colvarModifiedBy.DefaultSetting = @"";
				colvarModifiedBy.ForeignKeyTableName = "";
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
				colvarModifiedOn.DefaultSetting = @"";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);
				
				TableSchema.TableColumn colvarRowStatusId = new TableSchema.TableColumn(schema);
				colvarRowStatusId.ColumnName = "RowStatusId";
				colvarRowStatusId.DataType = DbType.Int32;
				colvarRowStatusId.MaxLength = 0;
				colvarRowStatusId.AutoIncrement = false;
				colvarRowStatusId.IsNullable = false;
				colvarRowStatusId.IsPrimaryKey = false;
				colvarRowStatusId.IsForeignKey = true;
				colvarRowStatusId.IsReadOnly = false;
				colvarRowStatusId.DefaultSetting = @"";
				
					colvarRowStatusId.ForeignKeyTableName = "RowStatus";
				schema.Columns.Add(colvarRowStatusId);
				
				TableSchema.TableColumn colvarVersionMajor = new TableSchema.TableColumn(schema);
				colvarVersionMajor.ColumnName = "VersionMajor";
				colvarVersionMajor.DataType = DbType.Int32;
				colvarVersionMajor.MaxLength = 0;
				colvarVersionMajor.AutoIncrement = false;
				colvarVersionMajor.IsNullable = false;
				colvarVersionMajor.IsPrimaryKey = false;
				colvarVersionMajor.IsForeignKey = false;
				colvarVersionMajor.IsReadOnly = false;
				colvarVersionMajor.DefaultSetting = @"";
				colvarVersionMajor.ForeignKeyTableName = "";
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
				colvarVersionMinor.DefaultSetting = @"";
				colvarVersionMinor.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVersionMinor);
				
				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = 512;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
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
				colvarFilename.DefaultSetting = @"";
				colvarFilename.ForeignKeyTableName = "";
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
				colvarProductionId.DefaultSetting = @"";
				colvarProductionId.ForeignKeyTableName = "";
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
				colvarValidationId.DefaultSetting = @"";
				colvarValidationId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarValidationId);
				
				TableSchema.TableColumn colvarWorkflowModuleStatusId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleStatusId.ColumnName = "WorkflowModuleStatusId";
				colvarWorkflowModuleStatusId.DataType = DbType.Int32;
				colvarWorkflowModuleStatusId.MaxLength = 0;
				colvarWorkflowModuleStatusId.AutoIncrement = false;
				colvarWorkflowModuleStatusId.IsNullable = false;
				colvarWorkflowModuleStatusId.IsPrimaryKey = false;
				colvarWorkflowModuleStatusId.IsForeignKey = true;
				colvarWorkflowModuleStatusId.IsReadOnly = false;
				colvarWorkflowModuleStatusId.DefaultSetting = @"";
				
					colvarWorkflowModuleStatusId.ForeignKeyTableName = "WorkflowStatus";
				schema.Columns.Add(colvarWorkflowModuleStatusId);
				
				TableSchema.TableColumn colvarOwnerId = new TableSchema.TableColumn(schema);
				colvarOwnerId.ColumnName = "OwnerId";
				colvarOwnerId.DataType = DbType.Int32;
				colvarOwnerId.MaxLength = 0;
				colvarOwnerId.AutoIncrement = false;
				colvarOwnerId.IsNullable = false;
				colvarOwnerId.IsPrimaryKey = false;
				colvarOwnerId.IsForeignKey = true;
				colvarOwnerId.IsReadOnly = false;
				colvarOwnerId.DefaultSetting = @"";
				
					colvarOwnerId.ForeignKeyTableName = "Person";
				schema.Columns.Add(colvarOwnerId);
				
				TableSchema.TableColumn colvarWorkflowModuleCategoryId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleCategoryId.ColumnName = "WorkflowModuleCategoryId";
				colvarWorkflowModuleCategoryId.DataType = DbType.Int32;
				colvarWorkflowModuleCategoryId.MaxLength = 0;
				colvarWorkflowModuleCategoryId.AutoIncrement = false;
				colvarWorkflowModuleCategoryId.IsNullable = false;
				colvarWorkflowModuleCategoryId.IsPrimaryKey = false;
				colvarWorkflowModuleCategoryId.IsForeignKey = true;
				colvarWorkflowModuleCategoryId.IsReadOnly = false;
				colvarWorkflowModuleCategoryId.DefaultSetting = @"";
				
					colvarWorkflowModuleCategoryId.ForeignKeyTableName = "WorkflowModuleCategory";
				schema.Columns.Add(colvarWorkflowModuleCategoryId);
				
				TableSchema.TableColumn colvarWorkflowModuleSubCategoryId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleSubCategoryId.ColumnName = "WorkflowModuleSubCategoryId";
				colvarWorkflowModuleSubCategoryId.DataType = DbType.Int32;
				colvarWorkflowModuleSubCategoryId.MaxLength = 0;
				colvarWorkflowModuleSubCategoryId.AutoIncrement = false;
				colvarWorkflowModuleSubCategoryId.IsNullable = false;
				colvarWorkflowModuleSubCategoryId.IsPrimaryKey = false;
				colvarWorkflowModuleSubCategoryId.IsForeignKey = true;
				colvarWorkflowModuleSubCategoryId.IsReadOnly = false;
				colvarWorkflowModuleSubCategoryId.DefaultSetting = @"";
				
					colvarWorkflowModuleSubCategoryId.ForeignKeyTableName = "WorkflowModuleSubCategory";
				schema.Columns.Add(colvarWorkflowModuleSubCategoryId);
				
				TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
				colvarTitle.ColumnName = "Title";
				colvarTitle.DataType = DbType.String;
				colvarTitle.MaxLength = 256;
				colvarTitle.AutoIncrement = false;
				colvarTitle.IsNullable = false;
				colvarTitle.IsPrimaryKey = false;
				colvarTitle.IsForeignKey = false;
				colvarTitle.IsReadOnly = false;
				colvarTitle.DefaultSetting = @"";
				colvarTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTitle);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("WorkflowModule",schema);
			}
		}
		#endregion
		
		#region Props
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		partial void OnIdChanging(int newValue);
		partial void OnIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set
			{
				this.OnIdChanging(value);
				this.OnPropertyChanging("Id", value);
				int oldValue = this.Id;
				SetColumnValue(Columns.Id, value);
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
			get { return GetColumnValue<string>(Columns.Name); }
			set
			{
				this.OnNameChanging(value);
				this.OnPropertyChanging("Name", value);
				string oldValue = this.Name;
				SetColumnValue(Columns.Name, value);
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
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set
			{
				this.OnCreatedByChanging(value);
				this.OnPropertyChanging("CreatedBy", value);
				string oldValue = this.CreatedBy;
				SetColumnValue(Columns.CreatedBy, value);
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
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set
			{
				this.OnCreatedOnChanging(value);
				this.OnPropertyChanging("CreatedOn", value);
				DateTime oldValue = this.CreatedOn;
				SetColumnValue(Columns.CreatedOn, value);
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
			get { return GetColumnValue<string>(Columns.ModifiedBy); }
			set
			{
				this.OnModifiedByChanging(value);
				this.OnPropertyChanging("ModifiedBy", value);
				string oldValue = this.ModifiedBy;
				SetColumnValue(Columns.ModifiedBy, value);
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
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set
			{
				this.OnModifiedOnChanging(value);
				this.OnPropertyChanging("ModifiedOn", value);
				DateTime oldValue = this.ModifiedOn;
				SetColumnValue(Columns.ModifiedOn, value);
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
			get { return GetColumnValue<int>(Columns.RowStatusId); }
			set
			{
				this.OnRowStatusIdChanging(value);
				this.OnPropertyChanging("RowStatusId", value);
				int oldValue = this.RowStatusId;
				SetColumnValue(Columns.RowStatusId, value);
				this.OnRowStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("RowStatusId", oldValue, value);
			}
		}
		partial void OnVersionMajorChanging(int newValue);
		partial void OnVersionMajorChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("VersionMajor")]
		[Bindable(true)]
		public int VersionMajor 
		{
			get { return GetColumnValue<int>(Columns.VersionMajor); }
			set
			{
				this.OnVersionMajorChanging(value);
				this.OnPropertyChanging("VersionMajor", value);
				int oldValue = this.VersionMajor;
				SetColumnValue(Columns.VersionMajor, value);
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
			get { return GetColumnValue<int>(Columns.VersionMinor); }
			set
			{
				this.OnVersionMinorChanging(value);
				this.OnPropertyChanging("VersionMinor", value);
				int oldValue = this.VersionMinor;
				SetColumnValue(Columns.VersionMinor, value);
				this.OnVersionMinorChanged(oldValue, value);
				this.OnPropertyChanged("VersionMinor", oldValue, value);
			}
		}
		partial void OnDescriptionChanging(string newValue);
		partial void OnDescriptionChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Description")]
		[Bindable(true)]
		public string Description 
		{
			get { return GetColumnValue<string>(Columns.Description); }
			set
			{
				this.OnDescriptionChanging(value);
				this.OnPropertyChanging("Description", value);
				string oldValue = this.Description;
				SetColumnValue(Columns.Description, value);
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
			get { return GetColumnValue<string>(Columns.Filename); }
			set
			{
				this.OnFilenameChanging(value);
				this.OnPropertyChanging("Filename", value);
				string oldValue = this.Filename;
				SetColumnValue(Columns.Filename, value);
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
			get { return GetColumnValue<int?>(Columns.ProductionId); }
			set
			{
				this.OnProductionIdChanging(value);
				this.OnPropertyChanging("ProductionId", value);
				int? oldValue = this.ProductionId;
				SetColumnValue(Columns.ProductionId, value);
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
			get { return GetColumnValue<int?>(Columns.ValidationId); }
			set
			{
				this.OnValidationIdChanging(value);
				this.OnPropertyChanging("ValidationId", value);
				int? oldValue = this.ValidationId;
				SetColumnValue(Columns.ValidationId, value);
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
			get { return GetColumnValue<int>(Columns.WorkflowModuleStatusId); }
			set
			{
				this.OnWorkflowModuleStatusIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleStatusId", value);
				int oldValue = this.WorkflowModuleStatusId;
				SetColumnValue(Columns.WorkflowModuleStatusId, value);
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
			get { return GetColumnValue<int>(Columns.OwnerId); }
			set
			{
				this.OnOwnerIdChanging(value);
				this.OnPropertyChanging("OwnerId", value);
				int oldValue = this.OwnerId;
				SetColumnValue(Columns.OwnerId, value);
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
			get { return GetColumnValue<int>(Columns.WorkflowModuleCategoryId); }
			set
			{
				this.OnWorkflowModuleCategoryIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleCategoryId", value);
				int oldValue = this.WorkflowModuleCategoryId;
				SetColumnValue(Columns.WorkflowModuleCategoryId, value);
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
			get { return GetColumnValue<int>(Columns.WorkflowModuleSubCategoryId); }
			set
			{
				this.OnWorkflowModuleSubCategoryIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleSubCategoryId", value);
				int oldValue = this.WorkflowModuleSubCategoryId;
				SetColumnValue(Columns.WorkflowModuleSubCategoryId, value);
				this.OnWorkflowModuleSubCategoryIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleSubCategoryId", oldValue, value);
			}
		}
		partial void OnTitleChanging(string newValue);
		partial void OnTitleChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Title")]
		[Bindable(true)]
		public string Title 
		{
			get { return GetColumnValue<string>(Columns.Title); }
			set
			{
				this.OnTitleChanging(value);
				this.OnPropertyChanging("Title", value);
				string oldValue = this.Title;
				SetColumnValue(Columns.Title, value);
				this.OnTitleChanged(oldValue, value);
				this.OnPropertyChanged("Title", oldValue, value);
			}
		}
		#endregion
		
		#region PrimaryKey Methods		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowWorkflowModuleCollection WorkflowWorkflowModuleRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.WorkflowWorkflowModuleCollection().Where(WorkflowWorkflowModule.Columns.WorkflowModuleId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleTagCollection WorkflowModuleTagRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleTagCollection().Where(WorkflowModuleTag.Columns.WorkflowModuleId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleWorkflowConditionCollection WorkflowModuleWorkflowConditionRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleWorkflowConditionCollection().Where(WorkflowModuleWorkflowCondition.Columns.WorkflowModuleId, Id).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a RowStatus ActiveRecord object related to this WorkflowModule
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.RowStatus RowStatus
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.RowStatus.FetchByID(this.RowStatusId); }
			set { SetColumnValue("RowStatusId", value.Id); }
		}
		
		/// <summary>
		/// Returns a Person ActiveRecord object related to this WorkflowModule
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.Person PersonToOwnerId
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.Person.FetchByID(this.OwnerId); }
			set { SetColumnValue("OwnerId", value.Id); }
		}
		
		/// <summary>
		/// Returns a WorkflowModuleCategory ActiveRecord object related to this WorkflowModule
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleCategory WorkflowModuleCategory
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleCategory.FetchByID(this.WorkflowModuleCategoryId); }
			set { SetColumnValue("WorkflowModuleCategoryId", value.Id); }
		}
		
		/// <summary>
		/// Returns a WorkflowModuleSubCategory ActiveRecord object related to this WorkflowModule
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleSubCategory WorkflowModuleSubCategory
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleSubCategory.FetchByID(this.WorkflowModuleSubCategoryId); }
			set { SetColumnValue("WorkflowModuleSubCategoryId", value.Id); }
		}
		
		/// <summary>
		/// Returns a WorkflowStatus ActiveRecord object related to this WorkflowModule
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowStatus WorkflowStatusToWorkflowModuleStatusId
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.WorkflowStatus.FetchByID(this.WorkflowModuleStatusId); }
			set { SetColumnValue("WorkflowModuleStatusId", value.Id); }
		}
		
		#endregion
		
		
		#region Many To Many Helpers
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.TagCollection GetTagCollection() { return WorkflowModule.GetTagCollection(this.Id); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static HP.ElementsCPS.Data.SubSonicClient.TagCollection GetTagCollection(int varId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[Tag] INNER JOIN [WorkflowModule_Tag] ON [Tag].[Id] = [WorkflowModule_Tag].[TagId] WHERE [WorkflowModule_Tag].[WorkflowModuleId] = @WorkflowModuleId", WorkflowModule.Schema.Provider.Name);
			cmd.AddParameter("@WorkflowModuleId", varId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			TagCollection coll = new TagCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveTagMap(int varId, TagCollection items)
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [WorkflowModule_Tag] WHERE [WorkflowModule_Tag].[WorkflowModuleId] = @WorkflowModuleId", WorkflowModule.Schema.Provider.Name);
			cmdDel.AddParameter("@WorkflowModuleId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (Tag item in items)
			{
				WorkflowModuleTag varWorkflowModuleTag = new WorkflowModuleTag(true);
				varWorkflowModuleTag.SetColumnValue("WorkflowModuleId", varId);
				varWorkflowModuleTag.SetColumnValue("TagId", item.GetPrimaryKeyValue());
				varWorkflowModuleTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveTagMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [WorkflowModule_Tag] WHERE [WorkflowModule_Tag].[WorkflowModuleId] = @WorkflowModuleId", WorkflowModule.Schema.Provider.Name);
			cmdDel.AddParameter("@WorkflowModuleId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					WorkflowModuleTag varWorkflowModuleTag = new WorkflowModuleTag(true);
					varWorkflowModuleTag.SetColumnValue("WorkflowModuleId", varId);
					varWorkflowModuleTag.SetColumnValue("TagId", l.Value);
					varWorkflowModuleTag.Save();
				}
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveTagMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [WorkflowModule_Tag] WHERE [WorkflowModule_Tag].[WorkflowModuleId] = @WorkflowModuleId", WorkflowModule.Schema.Provider.Name);
			cmdDel.AddParameter("@WorkflowModuleId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				WorkflowModuleTag varWorkflowModuleTag = new WorkflowModuleTag(true);
				varWorkflowModuleTag.SetColumnValue("WorkflowModuleId", varId);
				varWorkflowModuleTag.SetColumnValue("TagId", item);
				varWorkflowModuleTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void DeleteTagMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [WorkflowModule_Tag] WHERE [WorkflowModule_Tag].[WorkflowModuleId] = @WorkflowModuleId", WorkflowModule.Schema.Provider.Name);
			cmdDel.AddParameter("@WorkflowModuleId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		#endregion
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="WorkflowModule"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("WorkflowModule#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="WorkflowModule"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="WorkflowModule"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="WorkflowModule"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			WorkflowModule instance1 = this;
			WorkflowModule instance2 = obj as WorkflowModule;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.VersionMajor == instance2.VersionMajor)
			
				&& (instance1.VersionMinor == instance2.VersionMinor)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.Filename == instance2.Filename)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ValidationId == instance2.ValidationId)
			
				&& (instance1.WorkflowModuleStatusId == instance2.WorkflowModuleStatusId)
			
				&& (instance1.OwnerId == instance2.OwnerId)
			
				&& (instance1.WorkflowModuleCategoryId == instance2.WorkflowModuleCategoryId)
			
				&& (instance1.WorkflowModuleSubCategoryId == instance2.WorkflowModuleSubCategoryId)
			
				&& (instance1.Title == instance2.Title)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="WorkflowModule"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="WorkflowModule"/> to compare.</param>
		/// <param name="instance2">The second <see cref="WorkflowModule"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(WorkflowModule instance1, WorkflowModule instance2)
		{
			if (instance1 == null)
			{
				return (instance2 == null);
			}
			return instance1.Equals(instance2);
		}
*/
		#endregion
		#region ObjectDataSource support
		
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
        
		#region Typed Columns
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn IdColumn
		{
			get { return Schema.Columns[0]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[2]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[3]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[4]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn RowStatusIdColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn VersionMajorColumn
		{
			get { return Schema.Columns[7]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn VersionMinorColumn
		{
			get { return Schema.Columns[8]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[9]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn FilenameColumn
		{
			get { return Schema.Columns[10]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProductionIdColumn
		{
			get { return Schema.Columns[11]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ValidationIdColumn
		{
			get { return Schema.Columns[12]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WorkflowModuleStatusIdColumn
		{
			get { return Schema.Columns[13]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn OwnerIdColumn
		{
			get { return Schema.Columns[14]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WorkflowModuleCategoryIdColumn
		{
			get { return Schema.Columns[15]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WorkflowModuleSubCategoryIdColumn
		{
			get { return Schema.Columns[16]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn TitleColumn
		{
			get { return Schema.Columns[17]; }
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
			 public static string RowStatusId = @"RowStatusId";
			 public static string VersionMajor = @"VersionMajor";
			 public static string VersionMinor = @"VersionMinor";
			 public static string Description = @"Description";
			 public static string Filename = @"Filename";
			 public static string ProductionId = @"ProductionId";
			 public static string ValidationId = @"ValidationId";
			 public static string WorkflowModuleStatusId = @"WorkflowModuleStatusId";
			 public static string OwnerId = @"OwnerId";
			 public static string WorkflowModuleCategoryId = @"WorkflowModuleCategoryId";
			 public static string WorkflowModuleSubCategoryId = @"WorkflowModuleSubCategoryId";
			 public static string Title = @"Title";
			
		}
		#endregion
		#region Update PK Collections
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public void SetPKValues()
		{
}
		#endregion
		#region Deep Save
	//NOTE: Code Generation: Generation disabled: DeepSave method
		#endregion
	}
}
