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
	/// Strongly-typed collection for the ConfigurationServiceGroup class.
	/// </summary>
    [Serializable]
	public partial class ConfigurationServiceGroupCollection : ActiveList<ConfigurationServiceGroup, ConfigurationServiceGroupCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(ConfigurationServiceGroup);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceGroup table.
	/// </summary>
	[Serializable]
	public partial class ConfigurationServiceGroup : ActiveRecord<ConfigurationServiceGroup>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected ConfigurationServiceGroup(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public ConfigurationServiceGroup() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroup(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroup(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroup(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="ConfigurationServiceGroup"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private ConfigurationServiceGroup(ConfigurationServiceGroup original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(ConfigurationServiceGroup original)
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
			
			this.ConfigurationServiceGroupStatusId = original.ConfigurationServiceGroupStatusId;
			
			this.ConfigurationServiceGroupTypeId = original.ConfigurationServiceGroupTypeId;
			
			this.ConfigurationServiceApplicationId = original.ConfigurationServiceApplicationId;
			
			this.OwnerId = original.OwnerId;
			
			this.ProductionId = original.ProductionId;
			
			this.ValidationId = original.ValidationId;
			
			this.AppClientId = original.AppClientId;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="ConfigurationServiceGroup"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static ConfigurationServiceGroup Copy(ConfigurationServiceGroup original)
		{
			return new ConfigurationServiceGroup(original);
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
				TableSchema.Table schema = new TableSchema.Table("ConfigurationServiceGroup", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarConfigurationServiceGroupStatusId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupStatusId.ColumnName = "ConfigurationServiceGroupStatusId";
				colvarConfigurationServiceGroupStatusId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupStatusId.MaxLength = 0;
				colvarConfigurationServiceGroupStatusId.AutoIncrement = false;
				colvarConfigurationServiceGroupStatusId.IsNullable = false;
				colvarConfigurationServiceGroupStatusId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupStatusId.IsForeignKey = true;
				colvarConfigurationServiceGroupStatusId.IsReadOnly = false;
				colvarConfigurationServiceGroupStatusId.DefaultSetting = @"";
				
					colvarConfigurationServiceGroupStatusId.ForeignKeyTableName = "ConfigurationServiceGroupStatus";
				schema.Columns.Add(colvarConfigurationServiceGroupStatusId);
				
				TableSchema.TableColumn colvarConfigurationServiceGroupTypeId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupTypeId.ColumnName = "ConfigurationServiceGroupTypeId";
				colvarConfigurationServiceGroupTypeId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupTypeId.MaxLength = 0;
				colvarConfigurationServiceGroupTypeId.AutoIncrement = false;
				colvarConfigurationServiceGroupTypeId.IsNullable = false;
				colvarConfigurationServiceGroupTypeId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupTypeId.IsForeignKey = true;
				colvarConfigurationServiceGroupTypeId.IsReadOnly = false;
				colvarConfigurationServiceGroupTypeId.DefaultSetting = @"";
				
					colvarConfigurationServiceGroupTypeId.ForeignKeyTableName = "ConfigurationServiceGroupType";
				schema.Columns.Add(colvarConfigurationServiceGroupTypeId);
				
				TableSchema.TableColumn colvarConfigurationServiceApplicationId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceApplicationId.ColumnName = "ConfigurationServiceApplicationId";
				colvarConfigurationServiceApplicationId.DataType = DbType.Int32;
				colvarConfigurationServiceApplicationId.MaxLength = 0;
				colvarConfigurationServiceApplicationId.AutoIncrement = false;
				colvarConfigurationServiceApplicationId.IsNullable = false;
				colvarConfigurationServiceApplicationId.IsPrimaryKey = false;
				colvarConfigurationServiceApplicationId.IsForeignKey = true;
				colvarConfigurationServiceApplicationId.IsReadOnly = false;
				colvarConfigurationServiceApplicationId.DefaultSetting = @"";
				
					colvarConfigurationServiceApplicationId.ForeignKeyTableName = "ConfigurationServiceApplication";
				schema.Columns.Add(colvarConfigurationServiceApplicationId);
				
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
				DataService.Providers["ElementsCPSDB"].AddSchema("ConfigurationServiceGroup",schema);
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
		partial void OnConfigurationServiceGroupStatusIdChanging(int newValue);
		partial void OnConfigurationServiceGroupStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupStatusId")]
		[Bindable(true)]
		public int ConfigurationServiceGroupStatusId 
		{
			get { return GetColumnValue<int>(Columns.ConfigurationServiceGroupStatusId); }
			set
			{
				this.OnConfigurationServiceGroupStatusIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupStatusId", value);
				int oldValue = this.ConfigurationServiceGroupStatusId;
				SetColumnValue(Columns.ConfigurationServiceGroupStatusId, value);
				this.OnConfigurationServiceGroupStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupStatusId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceGroupTypeIdChanging(int newValue);
		partial void OnConfigurationServiceGroupTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupTypeId")]
		[Bindable(true)]
		public int ConfigurationServiceGroupTypeId 
		{
			get { return GetColumnValue<int>(Columns.ConfigurationServiceGroupTypeId); }
			set
			{
				this.OnConfigurationServiceGroupTypeIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupTypeId", value);
				int oldValue = this.ConfigurationServiceGroupTypeId;
				SetColumnValue(Columns.ConfigurationServiceGroupTypeId, value);
				this.OnConfigurationServiceGroupTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupTypeId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceApplicationIdChanging(int newValue);
		partial void OnConfigurationServiceApplicationIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceApplicationId")]
		[Bindable(true)]
		public int ConfigurationServiceApplicationId 
		{
			get { return GetColumnValue<int>(Columns.ConfigurationServiceApplicationId); }
			set
			{
				this.OnConfigurationServiceApplicationIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceApplicationId", value);
				int oldValue = this.ConfigurationServiceApplicationId;
				SetColumnValue(Columns.ConfigurationServiceApplicationId, value);
				this.OnConfigurationServiceApplicationIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceApplicationId", oldValue, value);
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
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupTagCollection ConfigurationServiceGroupTagRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupTagCollection().Where(ConfigurationServiceGroupTag.Columns.ConfigurationServiceGroupId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupSelectorCollection ConfigurationServiceGroupSelectorRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupSelectorCollection().Where(ConfigurationServiceGroupSelector.Columns.ConfigurationServiceGroupId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceLabelValueCollection ConfigurationServiceLabelValueRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceLabelValueCollection().Where(ConfigurationServiceLabelValue.Columns.ConfigurationServiceGroupId, Id).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a AppClient ActiveRecord object related to this ConfigurationServiceGroup
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.AppClient AppClient
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.AppClient.FetchByID(this.AppClientId); }
			set { SetColumnValue("AppClientId", value.Id); }
		}
		
		/// <summary>
		/// Returns a ConfigurationServiceApplication ActiveRecord object related to this ConfigurationServiceGroup
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceApplication ConfigurationServiceApplication
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceApplication.FetchByID(this.ConfigurationServiceApplicationId); }
			set { SetColumnValue("ConfigurationServiceApplicationId", value.Id); }
		}
		
		/// <summary>
		/// Returns a ConfigurationServiceGroupStatus ActiveRecord object related to this ConfigurationServiceGroup
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupStatus ConfigurationServiceGroupStatus
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupStatus.FetchByID(this.ConfigurationServiceGroupStatusId); }
			set { SetColumnValue("ConfigurationServiceGroupStatusId", value.Id); }
		}
		
		/// <summary>
		/// Returns a ConfigurationServiceGroupType ActiveRecord object related to this ConfigurationServiceGroup
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupType ConfigurationServiceGroupType
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupType.FetchByID(this.ConfigurationServiceGroupTypeId); }
			set { SetColumnValue("ConfigurationServiceGroupTypeId", value.Id); }
		}
		
		/// <summary>
		/// Returns a RowStatus ActiveRecord object related to this ConfigurationServiceGroup
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.RowStatus RowStatus
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.RowStatus.FetchByID(this.RowStatusId); }
			set { SetColumnValue("RowStatusId", value.Id); }
		}
		
		/// <summary>
		/// Returns a Person ActiveRecord object related to this ConfigurationServiceGroup
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.Person PersonToOwnerId
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.Person.FetchByID(this.OwnerId); }
			set { SetColumnValue("OwnerId", value.Id); }
		}
		
		#endregion
		
		
		#region Many To Many Helpers
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.TagCollection GetTagCollection() { return ConfigurationServiceGroup.GetTagCollection(this.Id); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static HP.ElementsCPS.Data.SubSonicClient.TagCollection GetTagCollection(int varId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[Tag] INNER JOIN [ConfigurationServiceGroup_Tag] ON [Tag].[Id] = [ConfigurationServiceGroup_Tag].[TagId] WHERE [ConfigurationServiceGroup_Tag].[ConfigurationServiceGroupId] = @ConfigurationServiceGroupId", ConfigurationServiceGroup.Schema.Provider.Name);
			cmd.AddParameter("@ConfigurationServiceGroupId", varId, DbType.Int32);
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
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [ConfigurationServiceGroup_Tag] WHERE [ConfigurationServiceGroup_Tag].[ConfigurationServiceGroupId] = @ConfigurationServiceGroupId", ConfigurationServiceGroup.Schema.Provider.Name);
			cmdDel.AddParameter("@ConfigurationServiceGroupId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (Tag item in items)
			{
				ConfigurationServiceGroupTag varConfigurationServiceGroupTag = new ConfigurationServiceGroupTag(true);
				varConfigurationServiceGroupTag.SetColumnValue("ConfigurationServiceGroupId", varId);
				varConfigurationServiceGroupTag.SetColumnValue("TagId", item.GetPrimaryKeyValue());
				varConfigurationServiceGroupTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveTagMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [ConfigurationServiceGroup_Tag] WHERE [ConfigurationServiceGroup_Tag].[ConfigurationServiceGroupId] = @ConfigurationServiceGroupId", ConfigurationServiceGroup.Schema.Provider.Name);
			cmdDel.AddParameter("@ConfigurationServiceGroupId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					ConfigurationServiceGroupTag varConfigurationServiceGroupTag = new ConfigurationServiceGroupTag(true);
					varConfigurationServiceGroupTag.SetColumnValue("ConfigurationServiceGroupId", varId);
					varConfigurationServiceGroupTag.SetColumnValue("TagId", l.Value);
					varConfigurationServiceGroupTag.Save();
				}
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveTagMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [ConfigurationServiceGroup_Tag] WHERE [ConfigurationServiceGroup_Tag].[ConfigurationServiceGroupId] = @ConfigurationServiceGroupId", ConfigurationServiceGroup.Schema.Provider.Name);
			cmdDel.AddParameter("@ConfigurationServiceGroupId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				ConfigurationServiceGroupTag varConfigurationServiceGroupTag = new ConfigurationServiceGroupTag(true);
				varConfigurationServiceGroupTag.SetColumnValue("ConfigurationServiceGroupId", varId);
				varConfigurationServiceGroupTag.SetColumnValue("TagId", item);
				varConfigurationServiceGroupTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void DeleteTagMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [ConfigurationServiceGroup_Tag] WHERE [ConfigurationServiceGroup_Tag].[ConfigurationServiceGroupId] = @ConfigurationServiceGroupId", ConfigurationServiceGroup.Schema.Provider.Name);
			cmdDel.AddParameter("@ConfigurationServiceGroupId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		#endregion
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="ConfigurationServiceGroup"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("ConfigurationServiceGroup#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="ConfigurationServiceGroup"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="ConfigurationServiceGroup"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="ConfigurationServiceGroup"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			ConfigurationServiceGroup instance1 = this;
			ConfigurationServiceGroup instance2 = obj as ConfigurationServiceGroup;
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
			
				&& (instance1.ConfigurationServiceGroupStatusId == instance2.ConfigurationServiceGroupStatusId)
			
				&& (instance1.ConfigurationServiceGroupTypeId == instance2.ConfigurationServiceGroupTypeId)
			
				&& (instance1.ConfigurationServiceApplicationId == instance2.ConfigurationServiceApplicationId)
			
				&& (instance1.OwnerId == instance2.OwnerId)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ValidationId == instance2.ValidationId)
			
				&& (instance1.AppClientId == instance2.AppClientId)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="ConfigurationServiceGroup"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="ConfigurationServiceGroup"/> to compare.</param>
		/// <param name="instance2">The second <see cref="ConfigurationServiceGroup"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(ConfigurationServiceGroup instance1, ConfigurationServiceGroup instance2)
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
		public static TableSchema.TableColumn ConfigurationServiceGroupStatusIdColumn
		{
			get { return Schema.Columns[8]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ConfigurationServiceGroupTypeIdColumn
		{
			get { return Schema.Columns[9]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ConfigurationServiceApplicationIdColumn
		{
			get { return Schema.Columns[10]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn OwnerIdColumn
		{
			get { return Schema.Columns[11]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProductionIdColumn
		{
			get { return Schema.Columns[12]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ValidationIdColumn
		{
			get { return Schema.Columns[13]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn AppClientIdColumn
		{
			get { return Schema.Columns[14]; }
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
			 public static string ConfigurationServiceGroupStatusId = @"ConfigurationServiceGroupStatusId";
			 public static string ConfigurationServiceGroupTypeId = @"ConfigurationServiceGroupTypeId";
			 public static string ConfigurationServiceApplicationId = @"ConfigurationServiceApplicationId";
			 public static string OwnerId = @"OwnerId";
			 public static string ProductionId = @"ProductionId";
			 public static string ValidationId = @"ValidationId";
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
