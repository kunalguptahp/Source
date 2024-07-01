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
	/// Strongly-typed collection for the Workflow class.
	/// </summary>
    [Serializable]
	public partial class WorkflowCollection : ActiveList<Workflow, WorkflowCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public WorkflowCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(Workflow);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the Workflow table.
	/// </summary>
	[Serializable]
	public partial class Workflow : ActiveRecord<Workflow>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected Workflow(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public Workflow() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Workflow(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Workflow(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Workflow(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="Workflow"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private Workflow(Workflow original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(Workflow original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.Name = original.Name;
			
			this.Description = original.Description;
			
			this.WorkflowStatusId = original.WorkflowStatusId;
			
			this.WorkflowTypeId = original.WorkflowTypeId;
			
			this.WorkflowApplicationId = original.WorkflowApplicationId;
			
			this.Offline = original.Offline;
			
			this.OwnerId = original.OwnerId;
			
			this.ProductionId = original.ProductionId;
			
			this.ValidationId = original.ValidationId;
			
			this.VersionMajor = original.VersionMajor;
			
			this.VersionMinor = original.VersionMinor;
			
			this.Filename = original.Filename;
			
			this.AppClientId = original.AppClientId;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="Workflow"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Workflow Copy(Workflow original)
		{
			return new Workflow(original);
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
				TableSchema.Table schema = new TableSchema.Table("Workflow", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);
				
				TableSchema.TableColumn colvarWorkflowStatusId = new TableSchema.TableColumn(schema);
				colvarWorkflowStatusId.ColumnName = "WorkflowStatusId";
				colvarWorkflowStatusId.DataType = DbType.Int32;
				colvarWorkflowStatusId.MaxLength = 0;
				colvarWorkflowStatusId.AutoIncrement = false;
				colvarWorkflowStatusId.IsNullable = false;
				colvarWorkflowStatusId.IsPrimaryKey = false;
				colvarWorkflowStatusId.IsForeignKey = true;
				colvarWorkflowStatusId.IsReadOnly = false;
				colvarWorkflowStatusId.DefaultSetting = @"";
				
					colvarWorkflowStatusId.ForeignKeyTableName = "WorkflowStatus";
				schema.Columns.Add(colvarWorkflowStatusId);
				
				TableSchema.TableColumn colvarWorkflowTypeId = new TableSchema.TableColumn(schema);
				colvarWorkflowTypeId.ColumnName = "WorkflowTypeId";
				colvarWorkflowTypeId.DataType = DbType.Int32;
				colvarWorkflowTypeId.MaxLength = 0;
				colvarWorkflowTypeId.AutoIncrement = false;
				colvarWorkflowTypeId.IsNullable = false;
				colvarWorkflowTypeId.IsPrimaryKey = false;
				colvarWorkflowTypeId.IsForeignKey = true;
				colvarWorkflowTypeId.IsReadOnly = false;
				colvarWorkflowTypeId.DefaultSetting = @"";
				
					colvarWorkflowTypeId.ForeignKeyTableName = "WorkflowType";
				schema.Columns.Add(colvarWorkflowTypeId);
				
				TableSchema.TableColumn colvarWorkflowApplicationId = new TableSchema.TableColumn(schema);
				colvarWorkflowApplicationId.ColumnName = "WorkflowApplicationId";
				colvarWorkflowApplicationId.DataType = DbType.Int32;
				colvarWorkflowApplicationId.MaxLength = 0;
				colvarWorkflowApplicationId.AutoIncrement = false;
				colvarWorkflowApplicationId.IsNullable = false;
				colvarWorkflowApplicationId.IsPrimaryKey = false;
				colvarWorkflowApplicationId.IsForeignKey = true;
				colvarWorkflowApplicationId.IsReadOnly = false;
				colvarWorkflowApplicationId.DefaultSetting = @"";
				
					colvarWorkflowApplicationId.ForeignKeyTableName = "WorkflowApplication";
				schema.Columns.Add(colvarWorkflowApplicationId);
				
				TableSchema.TableColumn colvarOffline = new TableSchema.TableColumn(schema);
				colvarOffline.ColumnName = "Offline";
				colvarOffline.DataType = DbType.Boolean;
				colvarOffline.MaxLength = 0;
				colvarOffline.AutoIncrement = false;
				colvarOffline.IsNullable = false;
				colvarOffline.IsPrimaryKey = false;
				colvarOffline.IsForeignKey = false;
				colvarOffline.IsReadOnly = false;
				colvarOffline.DefaultSetting = @"";
				colvarOffline.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOffline);
				
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
				
				TableSchema.TableColumn colvarFilename = new TableSchema.TableColumn(schema);
				colvarFilename.ColumnName = "Filename";
				colvarFilename.DataType = DbType.String;
				colvarFilename.MaxLength = 256;
				colvarFilename.AutoIncrement = false;
				colvarFilename.IsNullable = true;
				colvarFilename.IsPrimaryKey = false;
				colvarFilename.IsForeignKey = false;
				colvarFilename.IsReadOnly = false;
				colvarFilename.DefaultSetting = @"";
				colvarFilename.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFilename);
				
				TableSchema.TableColumn colvarAppClientId = new TableSchema.TableColumn(schema);
				colvarAppClientId.ColumnName = "AppClientId";
				colvarAppClientId.DataType = DbType.Int32;
				colvarAppClientId.MaxLength = 0;
				colvarAppClientId.AutoIncrement = false;
				colvarAppClientId.IsNullable = false;
				colvarAppClientId.IsPrimaryKey = false;
				colvarAppClientId.IsForeignKey = true;
				colvarAppClientId.IsReadOnly = false;
				colvarAppClientId.DefaultSetting = @"";
				
					colvarAppClientId.ForeignKeyTableName = "AppClient";
				schema.Columns.Add(colvarAppClientId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("Workflow",schema);
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
		partial void OnWorkflowStatusIdChanging(int newValue);
		partial void OnWorkflowStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowStatusId")]
		[Bindable(true)]
		public int WorkflowStatusId 
		{
			get { return GetColumnValue<int>(Columns.WorkflowStatusId); }
			set
			{
				this.OnWorkflowStatusIdChanging(value);
				this.OnPropertyChanging("WorkflowStatusId", value);
				int oldValue = this.WorkflowStatusId;
				SetColumnValue(Columns.WorkflowStatusId, value);
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
			get { return GetColumnValue<int>(Columns.WorkflowTypeId); }
			set
			{
				this.OnWorkflowTypeIdChanging(value);
				this.OnPropertyChanging("WorkflowTypeId", value);
				int oldValue = this.WorkflowTypeId;
				SetColumnValue(Columns.WorkflowTypeId, value);
				this.OnWorkflowTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowTypeId", oldValue, value);
			}
		}
		partial void OnWorkflowApplicationIdChanging(int newValue);
		partial void OnWorkflowApplicationIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowApplicationId")]
		[Bindable(true)]
		public int WorkflowApplicationId 
		{
			get { return GetColumnValue<int>(Columns.WorkflowApplicationId); }
			set
			{
				this.OnWorkflowApplicationIdChanging(value);
				this.OnPropertyChanging("WorkflowApplicationId", value);
				int oldValue = this.WorkflowApplicationId;
				SetColumnValue(Columns.WorkflowApplicationId, value);
				this.OnWorkflowApplicationIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowApplicationId", oldValue, value);
			}
		}
		partial void OnOfflineChanging(bool newValue);
		partial void OnOfflineChanged(bool oldValue, bool newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Offline")]
		[Bindable(true)]
		public bool Offline 
		{
			get { return GetColumnValue<bool>(Columns.Offline); }
			set
			{
				this.OnOfflineChanging(value);
				this.OnPropertyChanging("Offline", value);
				bool oldValue = this.Offline;
				SetColumnValue(Columns.Offline, value);
				this.OnOfflineChanged(oldValue, value);
				this.OnPropertyChanged("Offline", oldValue, value);
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
		partial void OnAppClientIdChanging(int newValue);
		partial void OnAppClientIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("AppClientId")]
		[Bindable(true)]
		public int AppClientId 
		{
			get { return GetColumnValue<int>(Columns.AppClientId); }
			set
			{
				this.OnAppClientIdChanging(value);
				this.OnPropertyChanging("AppClientId", value);
				int oldValue = this.AppClientId;
				SetColumnValue(Columns.AppClientId, value);
				this.OnAppClientIdChanged(oldValue, value);
				this.OnPropertyChanged("AppClientId", oldValue, value);
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
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowTagCollection WorkflowTagRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.WorkflowTagCollection().Where(WorkflowTag.Columns.WorkflowId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowWorkflowModuleCollection WorkflowWorkflowModuleRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.WorkflowWorkflowModuleCollection().Where(WorkflowWorkflowModule.Columns.WorkflowId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowSelectorCollection WorkflowSelectorRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.WorkflowSelectorCollection().Where(WorkflowSelector.Columns.WorkflowId, Id).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a AppClient ActiveRecord object related to this Workflow
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.AppClient AppClient
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.AppClient.FetchByID(this.AppClientId); }
			set { SetColumnValue("AppClientId", value.Id); }
		}
		
		/// <summary>
		/// Returns a RowStatus ActiveRecord object related to this Workflow
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.RowStatus RowStatus
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.RowStatus.FetchByID(this.RowStatusId); }
			set { SetColumnValue("RowStatusId", value.Id); }
		}
		
		/// <summary>
		/// Returns a Person ActiveRecord object related to this Workflow
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.Person PersonToOwnerId
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.Person.FetchByID(this.OwnerId); }
			set { SetColumnValue("OwnerId", value.Id); }
		}
		
		/// <summary>
		/// Returns a WorkflowApplication ActiveRecord object related to this Workflow
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowApplication WorkflowApplication
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.WorkflowApplication.FetchByID(this.WorkflowApplicationId); }
			set { SetColumnValue("WorkflowApplicationId", value.Id); }
		}
		
		/// <summary>
		/// Returns a WorkflowStatus ActiveRecord object related to this Workflow
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowStatus WorkflowStatus
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.WorkflowStatus.FetchByID(this.WorkflowStatusId); }
			set { SetColumnValue("WorkflowStatusId", value.Id); }
		}
		
		/// <summary>
		/// Returns a WorkflowType ActiveRecord object related to this Workflow
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowType WorkflowType
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.WorkflowType.FetchByID(this.WorkflowTypeId); }
			set { SetColumnValue("WorkflowTypeId", value.Id); }
		}
		
		#endregion
		
		
		#region Many To Many Helpers
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.TagCollection GetTagCollection() { return Workflow.GetTagCollection(this.Id); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static HP.ElementsCPS.Data.SubSonicClient.TagCollection GetTagCollection(int varId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[Tag] INNER JOIN [Workflow_Tag] ON [Tag].[Id] = [Workflow_Tag].[TagId] WHERE [Workflow_Tag].[WorkflowId] = @WorkflowId", Workflow.Schema.Provider.Name);
			cmd.AddParameter("@WorkflowId", varId, DbType.Int32);
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
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [Workflow_Tag] WHERE [Workflow_Tag].[WorkflowId] = @WorkflowId", Workflow.Schema.Provider.Name);
			cmdDel.AddParameter("@WorkflowId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (Tag item in items)
			{
				WorkflowTag varWorkflowTag = new WorkflowTag(true);
				varWorkflowTag.SetColumnValue("WorkflowId", varId);
				varWorkflowTag.SetColumnValue("TagId", item.GetPrimaryKeyValue());
				varWorkflowTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveTagMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [Workflow_Tag] WHERE [Workflow_Tag].[WorkflowId] = @WorkflowId", Workflow.Schema.Provider.Name);
			cmdDel.AddParameter("@WorkflowId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					WorkflowTag varWorkflowTag = new WorkflowTag(true);
					varWorkflowTag.SetColumnValue("WorkflowId", varId);
					varWorkflowTag.SetColumnValue("TagId", l.Value);
					varWorkflowTag.Save();
				}
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveTagMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [Workflow_Tag] WHERE [Workflow_Tag].[WorkflowId] = @WorkflowId", Workflow.Schema.Provider.Name);
			cmdDel.AddParameter("@WorkflowId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				WorkflowTag varWorkflowTag = new WorkflowTag(true);
				varWorkflowTag.SetColumnValue("WorkflowId", varId);
				varWorkflowTag.SetColumnValue("TagId", item);
				varWorkflowTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void DeleteTagMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [Workflow_Tag] WHERE [Workflow_Tag].[WorkflowId] = @WorkflowId", Workflow.Schema.Provider.Name);
			cmdDel.AddParameter("@WorkflowId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		#endregion
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="Workflow"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("Workflow#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="Workflow"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Workflow"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="Workflow"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			Workflow instance1 = this;
			Workflow instance2 = obj as Workflow;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.WorkflowStatusId == instance2.WorkflowStatusId)
			
				&& (instance1.WorkflowTypeId == instance2.WorkflowTypeId)
			
				&& (instance1.WorkflowApplicationId == instance2.WorkflowApplicationId)
			
				&& (instance1.Offline == instance2.Offline)
			
				&& (instance1.OwnerId == instance2.OwnerId)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ValidationId == instance2.ValidationId)
			
				&& (instance1.VersionMajor == instance2.VersionMajor)
			
				&& (instance1.VersionMinor == instance2.VersionMinor)
			
				&& (instance1.Filename == instance2.Filename)
			
				&& (instance1.AppClientId == instance2.AppClientId)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="Workflow"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="Workflow"/> to compare.</param>
		/// <param name="instance2">The second <see cref="Workflow"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(Workflow instance1, Workflow instance2)
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
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[1]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[2]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[3]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn RowStatusIdColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[7]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WorkflowStatusIdColumn
		{
			get { return Schema.Columns[8]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WorkflowTypeIdColumn
		{
			get { return Schema.Columns[9]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WorkflowApplicationIdColumn
		{
			get { return Schema.Columns[10]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn OfflineColumn
		{
			get { return Schema.Columns[11]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn OwnerIdColumn
		{
			get { return Schema.Columns[12]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProductionIdColumn
		{
			get { return Schema.Columns[13]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ValidationIdColumn
		{
			get { return Schema.Columns[14]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn VersionMajorColumn
		{
			get { return Schema.Columns[15]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn VersionMinorColumn
		{
			get { return Schema.Columns[16]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn FilenameColumn
		{
			get { return Schema.Columns[17]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn AppClientIdColumn
		{
			get { return Schema.Columns[18]; }
		}
        
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string CreatedBy = @"CreatedBy";
			 public static string CreatedOn = @"CreatedOn";
			 public static string ModifiedBy = @"ModifiedBy";
			 public static string ModifiedOn = @"ModifiedOn";
			 public static string RowStatusId = @"RowStatusId";
			 public static string Name = @"Name";
			 public static string Description = @"Description";
			 public static string WorkflowStatusId = @"WorkflowStatusId";
			 public static string WorkflowTypeId = @"WorkflowTypeId";
			 public static string WorkflowApplicationId = @"WorkflowApplicationId";
			 public static string Offline = @"Offline";
			 public static string OwnerId = @"OwnerId";
			 public static string ProductionId = @"ProductionId";
			 public static string ValidationId = @"ValidationId";
			 public static string VersionMajor = @"VersionMajor";
			 public static string VersionMinor = @"VersionMinor";
			 public static string Filename = @"Filename";
			 public static string AppClientId = @"AppClientId";
			
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
