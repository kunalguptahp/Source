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
	/// Strongly-typed collection for the VwMapWorkflowWorkflowModule class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowWorkflowModuleCollection : ReadOnlyList<VwMapWorkflowWorkflowModule, VwMapWorkflowWorkflowModuleCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowWorkflowModuleCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapWorkflowWorkflowModule);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapWorkflowWorkflowModule class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowWorkflowModuleController : BaseReadOnlyRecordController<VwMapWorkflowWorkflowModule, VwMapWorkflowWorkflowModuleCollection, VwMapWorkflowWorkflowModuleController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowWorkflowModuleController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapWorkflowWorkflowModule.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapWorkflow_WorkflowModule view.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowWorkflowModule : ReadOnlyRecord<VwMapWorkflowWorkflowModule>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapWorkflow_WorkflowModule", TableType.View, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarSortOrder = new TableSchema.TableColumn(schema);
				colvarSortOrder.ColumnName = "SortOrder";
				colvarSortOrder.DataType = DbType.Int32;
				colvarSortOrder.MaxLength = 0;
				colvarSortOrder.AutoIncrement = false;
				colvarSortOrder.IsNullable = false;
				colvarSortOrder.IsPrimaryKey = false;
				colvarSortOrder.IsForeignKey = false;
				colvarSortOrder.IsReadOnly = false;
				
				schema.Columns.Add(colvarSortOrder);
				
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
				
				TableSchema.TableColumn colvarWorkflowModuleId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleId.ColumnName = "WorkflowModuleId";
				colvarWorkflowModuleId.DataType = DbType.Int32;
				colvarWorkflowModuleId.MaxLength = 0;
				colvarWorkflowModuleId.AutoIncrement = false;
				colvarWorkflowModuleId.IsNullable = false;
				colvarWorkflowModuleId.IsPrimaryKey = false;
				colvarWorkflowModuleId.IsForeignKey = false;
				colvarWorkflowModuleId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleId);
				
				TableSchema.TableColumn colvarWorkflowId = new TableSchema.TableColumn(schema);
				colvarWorkflowId.ColumnName = "WorkflowId";
				colvarWorkflowId.DataType = DbType.Int32;
				colvarWorkflowId.MaxLength = 0;
				colvarWorkflowId.AutoIncrement = false;
				colvarWorkflowId.IsNullable = false;
				colvarWorkflowId.IsPrimaryKey = false;
				colvarWorkflowId.IsForeignKey = false;
				colvarWorkflowId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowId);
				
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
				
				TableSchema.TableColumn colvarWorkflowValidationId = new TableSchema.TableColumn(schema);
				colvarWorkflowValidationId.ColumnName = "WorkflowValidationId";
				colvarWorkflowValidationId.DataType = DbType.Int32;
				colvarWorkflowValidationId.MaxLength = 0;
				colvarWorkflowValidationId.AutoIncrement = false;
				colvarWorkflowValidationId.IsNullable = true;
				colvarWorkflowValidationId.IsPrimaryKey = false;
				colvarWorkflowValidationId.IsForeignKey = false;
				colvarWorkflowValidationId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowValidationId);
				
				TableSchema.TableColumn colvarWorkflowProductionId = new TableSchema.TableColumn(schema);
				colvarWorkflowProductionId.ColumnName = "WorkflowProductionId";
				colvarWorkflowProductionId.DataType = DbType.Int32;
				colvarWorkflowProductionId.MaxLength = 0;
				colvarWorkflowProductionId.AutoIncrement = false;
				colvarWorkflowProductionId.IsNullable = true;
				colvarWorkflowProductionId.IsPrimaryKey = false;
				colvarWorkflowProductionId.IsForeignKey = false;
				colvarWorkflowProductionId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowProductionId);
				
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
				
				TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
				colvarTitle.ColumnName = "Title";
				colvarTitle.DataType = DbType.String;
				colvarTitle.MaxLength = 256;
				colvarTitle.AutoIncrement = false;
				colvarTitle.IsNullable = false;
				colvarTitle.IsPrimaryKey = false;
				colvarTitle.IsForeignKey = false;
				colvarTitle.IsReadOnly = false;
				
				schema.Columns.Add(colvarTitle);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapWorkflow_WorkflowModule",schema);
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
		protected VwMapWorkflowWorkflowModule(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowWorkflowModule() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowWorkflowModule(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowWorkflowModule(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowWorkflowModule(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapWorkflowWorkflowModule"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapWorkflowWorkflowModule(VwMapWorkflowWorkflowModule original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapWorkflowWorkflowModule original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.SortOrder = original.SortOrder;
			
			this.Id = original.Id;
			
			this.Name = original.Name;
			
			this.WorkflowModuleId = original.WorkflowModuleId;
			
			this.WorkflowId = original.WorkflowId;
			
			this.VersionMajor = original.VersionMajor;
			
			this.VersionMinor = original.VersionMinor;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.CreatedBy = original.CreatedBy;
			
			this.Filename = original.Filename;
			
			this.ModuleSubCategoryName = original.ModuleSubCategoryName;
			
			this.ModuleCategoryName = original.ModuleCategoryName;
			
			this.WorkflowValidationId = original.WorkflowValidationId;
			
			this.WorkflowProductionId = original.WorkflowProductionId;
			
			this.WorkflowStatusId = original.WorkflowStatusId;
			
			this.Description = original.Description;
			
			this.Title = original.Title;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapWorkflowWorkflowModule"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapWorkflowWorkflowModule Copy(VwMapWorkflowWorkflowModule original)
		{
			return new VwMapWorkflowWorkflowModule(original);
		}
		#endregion
		#endregion
		#region Properties
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		
		partial void OnSortOrderChanging(int newValue);
		partial void OnSortOrderChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("SortOrder")]
		[Bindable(true)]
		public int SortOrder 
		{
			get
			{
				return GetColumnValue<int>("SortOrder");
			}
			set
			{
				this.OnSortOrderChanging(value);
				this.OnPropertyChanging("SortOrder", value);
				int oldValue = this.SortOrder;
				SetColumnValue("SortOrder", value);
				this.OnSortOrderChanged(oldValue, value);
				this.OnPropertyChanged("SortOrder", oldValue, value);
			}
		}
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
		partial void OnWorkflowModuleIdChanging(int newValue);
		partial void OnWorkflowModuleIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleId")]
		[Bindable(true)]
		public int WorkflowModuleId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowModuleId");
			}
			set
			{
				this.OnWorkflowModuleIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleId", value);
				int oldValue = this.WorkflowModuleId;
				SetColumnValue("WorkflowModuleId", value);
				this.OnWorkflowModuleIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleId", oldValue, value);
			}
		}
		partial void OnWorkflowIdChanging(int newValue);
		partial void OnWorkflowIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowId")]
		[Bindable(true)]
		public int WorkflowId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowId");
			}
			set
			{
				this.OnWorkflowIdChanging(value);
				this.OnPropertyChanging("WorkflowId", value);
				int oldValue = this.WorkflowId;
				SetColumnValue("WorkflowId", value);
				this.OnWorkflowIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowId", oldValue, value);
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
		partial void OnWorkflowValidationIdChanging(int? newValue);
		partial void OnWorkflowValidationIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowValidationId")]
		[Bindable(true)]
		public int? WorkflowValidationId 
		{
			get
			{
				return GetColumnValue<int?>("WorkflowValidationId");
			}
			set
			{
				this.OnWorkflowValidationIdChanging(value);
				this.OnPropertyChanging("WorkflowValidationId", value);
				int? oldValue = this.WorkflowValidationId;
				SetColumnValue("WorkflowValidationId", value);
				this.OnWorkflowValidationIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowValidationId", oldValue, value);
			}
		}
		partial void OnWorkflowProductionIdChanging(int? newValue);
		partial void OnWorkflowProductionIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowProductionId")]
		[Bindable(true)]
		public int? WorkflowProductionId 
		{
			get
			{
				return GetColumnValue<int?>("WorkflowProductionId");
			}
			set
			{
				this.OnWorkflowProductionIdChanging(value);
				this.OnPropertyChanging("WorkflowProductionId", value);
				int? oldValue = this.WorkflowProductionId;
				SetColumnValue("WorkflowProductionId", value);
				this.OnWorkflowProductionIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowProductionId", oldValue, value);
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
		partial void OnTitleChanging(string newValue);
		partial void OnTitleChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Title")]
		[Bindable(true)]
		public string Title 
		{
			get
			{
				return GetColumnValue<string>("Title");
			}
			set
			{
				this.OnTitleChanging(value);
				this.OnPropertyChanging("Title", value);
				string oldValue = this.Title;
				SetColumnValue("Title", value);
				this.OnTitleChanged(oldValue, value);
				this.OnPropertyChanged("Title", oldValue, value);
			}
		}
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string SortOrder = @"SortOrder";
			
			public static string Id = @"Id";
			
			public static string Name = @"Name";
			
			public static string WorkflowModuleId = @"WorkflowModuleId";
			
			public static string WorkflowId = @"WorkflowId";
			
			public static string VersionMajor = @"VersionMajor";
			
			public static string VersionMinor = @"VersionMinor";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string Filename = @"Filename";
			
			public static string ModuleSubCategoryName = @"ModuleSubCategoryName";
			
			public static string ModuleCategoryName = @"ModuleCategoryName";
			
			public static string WorkflowValidationId = @"WorkflowValidationId";
			
			public static string WorkflowProductionId = @"WorkflowProductionId";
			
			public static string WorkflowStatusId = @"WorkflowStatusId";
			
			public static string Description = @"Description";
			
			public static string Title = @"Title";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapWorkflowWorkflowModule"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapWorkflowWorkflowModule#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowWorkflowModule"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapWorkflowWorkflowModule"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowWorkflowModule"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapWorkflowWorkflowModule instance1 = this;
			VwMapWorkflowWorkflowModule instance2 = obj as VwMapWorkflowWorkflowModule;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.SortOrder == instance2.SortOrder)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.WorkflowModuleId == instance2.WorkflowModuleId)
			
				&& (instance1.WorkflowId == instance2.WorkflowId)
			
				&& (instance1.VersionMajor == instance2.VersionMajor)
			
				&& (instance1.VersionMinor == instance2.VersionMinor)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.Filename == instance2.Filename)
			
				&& (instance1.ModuleSubCategoryName == instance2.ModuleSubCategoryName)
			
				&& (instance1.ModuleCategoryName == instance2.ModuleCategoryName)
			
				&& (instance1.WorkflowValidationId == instance2.WorkflowValidationId)
			
				&& (instance1.WorkflowProductionId == instance2.WorkflowProductionId)
			
				&& (instance1.WorkflowStatusId == instance2.WorkflowStatusId)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.Title == instance2.Title)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapWorkflowWorkflowModule"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapWorkflowWorkflowModule"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapWorkflowWorkflowModule"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapWorkflowWorkflowModule instance1, VwMapWorkflowWorkflowModule instance2)
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
