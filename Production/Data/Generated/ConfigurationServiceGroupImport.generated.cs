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
	/// Strongly-typed collection for the ConfigurationServiceGroupImport class.
	/// </summary>
    [Serializable]
	public partial class ConfigurationServiceGroupImportCollection : ActiveList<ConfigurationServiceGroupImport, ConfigurationServiceGroupImportCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupImportCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(ConfigurationServiceGroupImport);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceGroupImport table.
	/// </summary>
	[Serializable]
	public partial class ConfigurationServiceGroupImport : ActiveRecord<ConfigurationServiceGroupImport>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected ConfigurationServiceGroupImport(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public ConfigurationServiceGroupImport() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupImport(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupImport(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupImport(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="ConfigurationServiceGroupImport"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private ConfigurationServiceGroupImport(ConfigurationServiceGroupImport original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(ConfigurationServiceGroupImport original)
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
			
			this.ConfigurationServiceGroupTypeName = original.ConfigurationServiceGroupTypeName;
			
			this.ConfigurationServiceApplicationName = original.ConfigurationServiceApplicationName;
			
			this.ProductionId = original.ProductionId;
			
			this.ImportMessage = original.ImportMessage;
			
			this.ImportStatus = original.ImportStatus;
			
			this.ConfigurationServiceGroupId = original.ConfigurationServiceGroupId;
			
			this.ImportId = original.ImportId;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="ConfigurationServiceGroupImport"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static ConfigurationServiceGroupImport Copy(ConfigurationServiceGroupImport original)
		{
			return new ConfigurationServiceGroupImport(original);
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
				TableSchema.Table schema = new TableSchema.Table("ConfigurationServiceGroupImport", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarConfigurationServiceGroupTypeName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupTypeName.ColumnName = "ConfigurationServiceGroupTypeName";
				colvarConfigurationServiceGroupTypeName.DataType = DbType.String;
				colvarConfigurationServiceGroupTypeName.MaxLength = 256;
				colvarConfigurationServiceGroupTypeName.AutoIncrement = false;
				colvarConfigurationServiceGroupTypeName.IsNullable = false;
				colvarConfigurationServiceGroupTypeName.IsPrimaryKey = false;
				colvarConfigurationServiceGroupTypeName.IsForeignKey = false;
				colvarConfigurationServiceGroupTypeName.IsReadOnly = false;
				colvarConfigurationServiceGroupTypeName.DefaultSetting = @"";
				colvarConfigurationServiceGroupTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarConfigurationServiceGroupTypeName);
				
				TableSchema.TableColumn colvarConfigurationServiceApplicationName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceApplicationName.ColumnName = "ConfigurationServiceApplicationName";
				colvarConfigurationServiceApplicationName.DataType = DbType.String;
				colvarConfigurationServiceApplicationName.MaxLength = 256;
				colvarConfigurationServiceApplicationName.AutoIncrement = false;
				colvarConfigurationServiceApplicationName.IsNullable = false;
				colvarConfigurationServiceApplicationName.IsPrimaryKey = false;
				colvarConfigurationServiceApplicationName.IsForeignKey = false;
				colvarConfigurationServiceApplicationName.IsReadOnly = false;
				colvarConfigurationServiceApplicationName.DefaultSetting = @"";
				colvarConfigurationServiceApplicationName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarConfigurationServiceApplicationName);
				
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
				
				TableSchema.TableColumn colvarImportMessage = new TableSchema.TableColumn(schema);
				colvarImportMessage.ColumnName = "ImportMessage";
				colvarImportMessage.DataType = DbType.String;
				colvarImportMessage.MaxLength = 256;
				colvarImportMessage.AutoIncrement = false;
				colvarImportMessage.IsNullable = true;
				colvarImportMessage.IsPrimaryKey = false;
				colvarImportMessage.IsForeignKey = false;
				colvarImportMessage.IsReadOnly = false;
				colvarImportMessage.DefaultSetting = @"";
				colvarImportMessage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarImportMessage);
				
				TableSchema.TableColumn colvarImportStatus = new TableSchema.TableColumn(schema);
				colvarImportStatus.ColumnName = "ImportStatus";
				colvarImportStatus.DataType = DbType.String;
				colvarImportStatus.MaxLength = 50;
				colvarImportStatus.AutoIncrement = false;
				colvarImportStatus.IsNullable = true;
				colvarImportStatus.IsPrimaryKey = false;
				colvarImportStatus.IsForeignKey = false;
				colvarImportStatus.IsReadOnly = false;
				colvarImportStatus.DefaultSetting = @"";
				colvarImportStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarImportStatus);
				
				TableSchema.TableColumn colvarConfigurationServiceGroupId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupId.ColumnName = "ConfigurationServiceGroupId";
				colvarConfigurationServiceGroupId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupId.MaxLength = 0;
				colvarConfigurationServiceGroupId.AutoIncrement = false;
				colvarConfigurationServiceGroupId.IsNullable = true;
				colvarConfigurationServiceGroupId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupId.IsForeignKey = false;
				colvarConfigurationServiceGroupId.IsReadOnly = false;
				colvarConfigurationServiceGroupId.DefaultSetting = @"";
				colvarConfigurationServiceGroupId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarConfigurationServiceGroupId);
				
				TableSchema.TableColumn colvarImportId = new TableSchema.TableColumn(schema);
				colvarImportId.ColumnName = "ImportId";
				colvarImportId.DataType = DbType.String;
				colvarImportId.MaxLength = 50;
				colvarImportId.AutoIncrement = false;
				colvarImportId.IsNullable = true;
				colvarImportId.IsPrimaryKey = false;
				colvarImportId.IsForeignKey = false;
				colvarImportId.IsReadOnly = false;
				colvarImportId.DefaultSetting = @"";
				colvarImportId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarImportId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("ConfigurationServiceGroupImport",schema);
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
		partial void OnConfigurationServiceGroupTypeNameChanging(string newValue);
		partial void OnConfigurationServiceGroupTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupTypeName")]
		[Bindable(true)]
		public string ConfigurationServiceGroupTypeName 
		{
			get { return GetColumnValue<string>(Columns.ConfigurationServiceGroupTypeName); }
			set
			{
				this.OnConfigurationServiceGroupTypeNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupTypeName", value);
				string oldValue = this.ConfigurationServiceGroupTypeName;
				SetColumnValue(Columns.ConfigurationServiceGroupTypeName, value);
				this.OnConfigurationServiceGroupTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupTypeName", oldValue, value);
			}
		}
		partial void OnConfigurationServiceApplicationNameChanging(string newValue);
		partial void OnConfigurationServiceApplicationNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceApplicationName")]
		[Bindable(true)]
		public string ConfigurationServiceApplicationName 
		{
			get { return GetColumnValue<string>(Columns.ConfigurationServiceApplicationName); }
			set
			{
				this.OnConfigurationServiceApplicationNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceApplicationName", value);
				string oldValue = this.ConfigurationServiceApplicationName;
				SetColumnValue(Columns.ConfigurationServiceApplicationName, value);
				this.OnConfigurationServiceApplicationNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceApplicationName", oldValue, value);
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
		partial void OnImportMessageChanging(string newValue);
		partial void OnImportMessageChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ImportMessage")]
		[Bindable(true)]
		public string ImportMessage 
		{
			get { return GetColumnValue<string>(Columns.ImportMessage); }
			set
			{
				this.OnImportMessageChanging(value);
				this.OnPropertyChanging("ImportMessage", value);
				string oldValue = this.ImportMessage;
				SetColumnValue(Columns.ImportMessage, value);
				this.OnImportMessageChanged(oldValue, value);
				this.OnPropertyChanged("ImportMessage", oldValue, value);
			}
		}
		partial void OnImportStatusChanging(string newValue);
		partial void OnImportStatusChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ImportStatus")]
		[Bindable(true)]
		public string ImportStatus 
		{
			get { return GetColumnValue<string>(Columns.ImportStatus); }
			set
			{
				this.OnImportStatusChanging(value);
				this.OnPropertyChanging("ImportStatus", value);
				string oldValue = this.ImportStatus;
				SetColumnValue(Columns.ImportStatus, value);
				this.OnImportStatusChanged(oldValue, value);
				this.OnPropertyChanged("ImportStatus", oldValue, value);
			}
		}
		partial void OnConfigurationServiceGroupIdChanging(int? newValue);
		partial void OnConfigurationServiceGroupIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupId")]
		[Bindable(true)]
		public int? ConfigurationServiceGroupId 
		{
			get { return GetColumnValue<int?>(Columns.ConfigurationServiceGroupId); }
			set
			{
				this.OnConfigurationServiceGroupIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupId", value);
				int? oldValue = this.ConfigurationServiceGroupId;
				SetColumnValue(Columns.ConfigurationServiceGroupId, value);
				this.OnConfigurationServiceGroupIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupId", oldValue, value);
			}
		}
		partial void OnImportIdChanging(string newValue);
		partial void OnImportIdChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ImportId")]
		[Bindable(true)]
		public string ImportId 
		{
			get { return GetColumnValue<string>(Columns.ImportId); }
			set
			{
				this.OnImportIdChanging(value);
				this.OnPropertyChanging("ImportId", value);
				string oldValue = this.ImportId;
				SetColumnValue(Columns.ImportId, value);
				this.OnImportIdChanged(oldValue, value);
				this.OnPropertyChanged("ImportId", oldValue, value);
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
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceLabelValueImportCollection ConfigurationServiceLabelValueImportRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceLabelValueImportCollection().Where(ConfigurationServiceLabelValueImport.Columns.ConfigurationServiceGroupImportId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceQueryParameterValueImportCollection ConfigurationServiceQueryParameterValueImportRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceQueryParameterValueImportCollection().Where(ConfigurationServiceQueryParameterValueImport.Columns.ConfigurationServiceGroupImportId, Id).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a RowStatus ActiveRecord object related to this ConfigurationServiceGroupImport
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.RowStatus RowStatus
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.RowStatus.FetchByID(this.RowStatusId); }
			set { SetColumnValue("RowStatusId", value.Id); }
		}
		
		#endregion
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="ConfigurationServiceGroupImport"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("ConfigurationServiceGroupImport#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="ConfigurationServiceGroupImport"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="ConfigurationServiceGroupImport"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="ConfigurationServiceGroupImport"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			ConfigurationServiceGroupImport instance1 = this;
			ConfigurationServiceGroupImport instance2 = obj as ConfigurationServiceGroupImport;
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
			
				&& (instance1.ConfigurationServiceGroupTypeName == instance2.ConfigurationServiceGroupTypeName)
			
				&& (instance1.ConfigurationServiceApplicationName == instance2.ConfigurationServiceApplicationName)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ImportMessage == instance2.ImportMessage)
			
				&& (instance1.ImportStatus == instance2.ImportStatus)
			
				&& (instance1.ConfigurationServiceGroupId == instance2.ConfigurationServiceGroupId)
			
				&& (instance1.ImportId == instance2.ImportId)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="ConfigurationServiceGroupImport"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="ConfigurationServiceGroupImport"/> to compare.</param>
		/// <param name="instance2">The second <see cref="ConfigurationServiceGroupImport"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(ConfigurationServiceGroupImport instance1, ConfigurationServiceGroupImport instance2)
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
		public static TableSchema.TableColumn ConfigurationServiceGroupTypeNameColumn
		{
			get { return Schema.Columns[8]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ConfigurationServiceApplicationNameColumn
		{
			get { return Schema.Columns[9]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProductionIdColumn
		{
			get { return Schema.Columns[10]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ImportMessageColumn
		{
			get { return Schema.Columns[11]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ImportStatusColumn
		{
			get { return Schema.Columns[12]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ConfigurationServiceGroupIdColumn
		{
			get { return Schema.Columns[13]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ImportIdColumn
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
			 public static string ConfigurationServiceGroupTypeName = @"ConfigurationServiceGroupTypeName";
			 public static string ConfigurationServiceApplicationName = @"ConfigurationServiceApplicationName";
			 public static string ProductionId = @"ProductionId";
			 public static string ImportMessage = @"ImportMessage";
			 public static string ImportStatus = @"ImportStatus";
			 public static string ConfigurationServiceGroupId = @"ConfigurationServiceGroupId";
			 public static string ImportId = @"ImportId";
			
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
