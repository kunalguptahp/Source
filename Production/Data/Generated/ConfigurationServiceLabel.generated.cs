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
	/// Strongly-typed collection for the ConfigurationServiceLabel class.
	/// </summary>
    [Serializable]
	public partial class ConfigurationServiceLabelCollection : ActiveList<ConfigurationServiceLabel, ConfigurationServiceLabelCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceLabelCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(ConfigurationServiceLabel);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceLabel table.
	/// </summary>
	[Serializable]
	public partial class ConfigurationServiceLabel : ActiveRecord<ConfigurationServiceLabel>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected ConfigurationServiceLabel(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public ConfigurationServiceLabel() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceLabel(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceLabel(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceLabel(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="ConfigurationServiceLabel"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private ConfigurationServiceLabel(ConfigurationServiceLabel original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(ConfigurationServiceLabel original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.Name = original.Name;
			
			this.ElementsKey = original.ElementsKey;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.ConfigurationServiceItemId = original.ConfigurationServiceItemId;
			
			this.ConfigurationServiceLabelTypeId = original.ConfigurationServiceLabelTypeId;
			
			this.Description = original.Description;
			
			this.Help = original.Help;
			
			this.ValueList = original.ValueList;
			
			this.InputRequired = original.InputRequired;
			
			this.SortOrder = original.SortOrder;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="ConfigurationServiceLabel"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static ConfigurationServiceLabel Copy(ConfigurationServiceLabel original)
		{
			return new ConfigurationServiceLabel(original);
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
				TableSchema.Table schema = new TableSchema.Table("ConfigurationServiceLabel", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarElementsKey = new TableSchema.TableColumn(schema);
				colvarElementsKey.ColumnName = "ElementsKey";
				colvarElementsKey.DataType = DbType.String;
				colvarElementsKey.MaxLength = 64;
				colvarElementsKey.AutoIncrement = false;
				colvarElementsKey.IsNullable = false;
				colvarElementsKey.IsPrimaryKey = false;
				colvarElementsKey.IsForeignKey = false;
				colvarElementsKey.IsReadOnly = false;
				colvarElementsKey.DefaultSetting = @"";
				colvarElementsKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarElementsKey);
				
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
				
				TableSchema.TableColumn colvarConfigurationServiceItemId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceItemId.ColumnName = "ConfigurationServiceItemId";
				colvarConfigurationServiceItemId.DataType = DbType.Int32;
				colvarConfigurationServiceItemId.MaxLength = 0;
				colvarConfigurationServiceItemId.AutoIncrement = false;
				colvarConfigurationServiceItemId.IsNullable = false;
				colvarConfigurationServiceItemId.IsPrimaryKey = false;
				colvarConfigurationServiceItemId.IsForeignKey = true;
				colvarConfigurationServiceItemId.IsReadOnly = false;
				colvarConfigurationServiceItemId.DefaultSetting = @"";
				
					colvarConfigurationServiceItemId.ForeignKeyTableName = "ConfigurationServiceItem";
				schema.Columns.Add(colvarConfigurationServiceItemId);
				
				TableSchema.TableColumn colvarConfigurationServiceLabelTypeId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceLabelTypeId.ColumnName = "ConfigurationServiceLabelTypeId";
				colvarConfigurationServiceLabelTypeId.DataType = DbType.Int32;
				colvarConfigurationServiceLabelTypeId.MaxLength = 0;
				colvarConfigurationServiceLabelTypeId.AutoIncrement = false;
				colvarConfigurationServiceLabelTypeId.IsNullable = false;
				colvarConfigurationServiceLabelTypeId.IsPrimaryKey = false;
				colvarConfigurationServiceLabelTypeId.IsForeignKey = true;
				colvarConfigurationServiceLabelTypeId.IsReadOnly = false;
				colvarConfigurationServiceLabelTypeId.DefaultSetting = @"";
				
					colvarConfigurationServiceLabelTypeId.ForeignKeyTableName = "ConfigurationServiceLabelType";
				schema.Columns.Add(colvarConfigurationServiceLabelTypeId);
				
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
				
				TableSchema.TableColumn colvarHelp = new TableSchema.TableColumn(schema);
				colvarHelp.ColumnName = "Help";
				colvarHelp.DataType = DbType.String;
				colvarHelp.MaxLength = 256;
				colvarHelp.AutoIncrement = false;
				colvarHelp.IsNullable = true;
				colvarHelp.IsPrimaryKey = false;
				colvarHelp.IsForeignKey = false;
				colvarHelp.IsReadOnly = false;
				colvarHelp.DefaultSetting = @"";
				colvarHelp.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHelp);
				
				TableSchema.TableColumn colvarValueList = new TableSchema.TableColumn(schema);
				colvarValueList.ColumnName = "ValueList";
				colvarValueList.DataType = DbType.String;
				colvarValueList.MaxLength = 512;
				colvarValueList.AutoIncrement = false;
				colvarValueList.IsNullable = true;
				colvarValueList.IsPrimaryKey = false;
				colvarValueList.IsForeignKey = false;
				colvarValueList.IsReadOnly = false;
				colvarValueList.DefaultSetting = @"";
				colvarValueList.ForeignKeyTableName = "";
				schema.Columns.Add(colvarValueList);
				
				TableSchema.TableColumn colvarInputRequired = new TableSchema.TableColumn(schema);
				colvarInputRequired.ColumnName = "InputRequired";
				colvarInputRequired.DataType = DbType.Boolean;
				colvarInputRequired.MaxLength = 0;
				colvarInputRequired.AutoIncrement = false;
				colvarInputRequired.IsNullable = false;
				colvarInputRequired.IsPrimaryKey = false;
				colvarInputRequired.IsForeignKey = false;
				colvarInputRequired.IsReadOnly = false;
				colvarInputRequired.DefaultSetting = @"";
				colvarInputRequired.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInputRequired);
				
				TableSchema.TableColumn colvarSortOrder = new TableSchema.TableColumn(schema);
				colvarSortOrder.ColumnName = "SortOrder";
				colvarSortOrder.DataType = DbType.Int32;
				colvarSortOrder.MaxLength = 0;
				colvarSortOrder.AutoIncrement = false;
				colvarSortOrder.IsNullable = true;
				colvarSortOrder.IsPrimaryKey = false;
				colvarSortOrder.IsForeignKey = false;
				colvarSortOrder.IsReadOnly = false;
				colvarSortOrder.DefaultSetting = @"";
				colvarSortOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSortOrder);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("ConfigurationServiceLabel",schema);
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
		partial void OnElementsKeyChanging(string newValue);
		partial void OnElementsKeyChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ElementsKey")]
		[Bindable(true)]
		public string ElementsKey 
		{
			get { return GetColumnValue<string>(Columns.ElementsKey); }
			set
			{
				this.OnElementsKeyChanging(value);
				this.OnPropertyChanging("ElementsKey", value);
				string oldValue = this.ElementsKey;
				SetColumnValue(Columns.ElementsKey, value);
				this.OnElementsKeyChanged(oldValue, value);
				this.OnPropertyChanged("ElementsKey", oldValue, value);
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
		partial void OnConfigurationServiceItemIdChanging(int newValue);
		partial void OnConfigurationServiceItemIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceItemId")]
		[Bindable(true)]
		public int ConfigurationServiceItemId 
		{
			get { return GetColumnValue<int>(Columns.ConfigurationServiceItemId); }
			set
			{
				this.OnConfigurationServiceItemIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceItemId", value);
				int oldValue = this.ConfigurationServiceItemId;
				SetColumnValue(Columns.ConfigurationServiceItemId, value);
				this.OnConfigurationServiceItemIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceItemId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceLabelTypeIdChanging(int newValue);
		partial void OnConfigurationServiceLabelTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceLabelTypeId")]
		[Bindable(true)]
		public int ConfigurationServiceLabelTypeId 
		{
			get { return GetColumnValue<int>(Columns.ConfigurationServiceLabelTypeId); }
			set
			{
				this.OnConfigurationServiceLabelTypeIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceLabelTypeId", value);
				int oldValue = this.ConfigurationServiceLabelTypeId;
				SetColumnValue(Columns.ConfigurationServiceLabelTypeId, value);
				this.OnConfigurationServiceLabelTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceLabelTypeId", oldValue, value);
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
		partial void OnHelpChanging(string newValue);
		partial void OnHelpChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Help")]
		[Bindable(true)]
		public string Help 
		{
			get { return GetColumnValue<string>(Columns.Help); }
			set
			{
				this.OnHelpChanging(value);
				this.OnPropertyChanging("Help", value);
				string oldValue = this.Help;
				SetColumnValue(Columns.Help, value);
				this.OnHelpChanged(oldValue, value);
				this.OnPropertyChanged("Help", oldValue, value);
			}
		}
		partial void OnValueListChanging(string newValue);
		partial void OnValueListChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ValueList")]
		[Bindable(true)]
		public string ValueList 
		{
			get { return GetColumnValue<string>(Columns.ValueList); }
			set
			{
				this.OnValueListChanging(value);
				this.OnPropertyChanging("ValueList", value);
				string oldValue = this.ValueList;
				SetColumnValue(Columns.ValueList, value);
				this.OnValueListChanged(oldValue, value);
				this.OnPropertyChanged("ValueList", oldValue, value);
			}
		}
		partial void OnInputRequiredChanging(bool newValue);
		partial void OnInputRequiredChanged(bool oldValue, bool newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("InputRequired")]
		[Bindable(true)]
		public bool InputRequired 
		{
			get { return GetColumnValue<bool>(Columns.InputRequired); }
			set
			{
				this.OnInputRequiredChanging(value);
				this.OnPropertyChanging("InputRequired", value);
				bool oldValue = this.InputRequired;
				SetColumnValue(Columns.InputRequired, value);
				this.OnInputRequiredChanged(oldValue, value);
				this.OnPropertyChanged("InputRequired", oldValue, value);
			}
		}
		partial void OnSortOrderChanging(int? newValue);
		partial void OnSortOrderChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("SortOrder")]
		[Bindable(true)]
		public int? SortOrder 
		{
			get { return GetColumnValue<int?>(Columns.SortOrder); }
			set
			{
				this.OnSortOrderChanging(value);
				this.OnPropertyChanging("SortOrder", value);
				int? oldValue = this.SortOrder;
				SetColumnValue(Columns.SortOrder, value);
				this.OnSortOrderChanged(oldValue, value);
				this.OnPropertyChanged("SortOrder", oldValue, value);
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
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceLabelValueCollection ConfigurationServiceLabelValueRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceLabelValueCollection().Where(ConfigurationServiceLabelValue.Columns.ConfigurationServiceLabelId, Id).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a ConfigurationServiceItem ActiveRecord object related to this ConfigurationServiceLabel
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceItem ConfigurationServiceItem
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceItem.FetchByID(this.ConfigurationServiceItemId); }
			set { SetColumnValue("ConfigurationServiceItemId", value.Id); }
		}
		
		/// <summary>
		/// Returns a ConfigurationServiceLabelType ActiveRecord object related to this ConfigurationServiceLabel
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceLabelType ConfigurationServiceLabelType
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceLabelType.FetchByID(this.ConfigurationServiceLabelTypeId); }
			set { SetColumnValue("ConfigurationServiceLabelTypeId", value.Id); }
		}
		
		/// <summary>
		/// Returns a RowStatus ActiveRecord object related to this ConfigurationServiceLabel
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
		// /// <returns>A hash code for the current <see cref="ConfigurationServiceLabel"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("ConfigurationServiceLabel#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="ConfigurationServiceLabel"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="ConfigurationServiceLabel"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="ConfigurationServiceLabel"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			ConfigurationServiceLabel instance1 = this;
			ConfigurationServiceLabel instance2 = obj as ConfigurationServiceLabel;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.ElementsKey == instance2.ElementsKey)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.ConfigurationServiceItemId == instance2.ConfigurationServiceItemId)
			
				&& (instance1.ConfigurationServiceLabelTypeId == instance2.ConfigurationServiceLabelTypeId)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.Help == instance2.Help)
			
				&& (instance1.ValueList == instance2.ValueList)
			
				&& (instance1.InputRequired == instance2.InputRequired)
			
				&& (instance1.SortOrder == instance2.SortOrder)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="ConfigurationServiceLabel"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="ConfigurationServiceLabel"/> to compare.</param>
		/// <param name="instance2">The second <see cref="ConfigurationServiceLabel"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(ConfigurationServiceLabel instance1, ConfigurationServiceLabel instance2)
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
		public static TableSchema.TableColumn ElementsKeyColumn
		{
			get { return Schema.Columns[2]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[3]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn RowStatusIdColumn
		{
			get { return Schema.Columns[7]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ConfigurationServiceItemIdColumn
		{
			get { return Schema.Columns[8]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ConfigurationServiceLabelTypeIdColumn
		{
			get { return Schema.Columns[9]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[10]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn HelpColumn
		{
			get { return Schema.Columns[11]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ValueListColumn
		{
			get { return Schema.Columns[12]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn InputRequiredColumn
		{
			get { return Schema.Columns[13]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn SortOrderColumn
		{
			get { return Schema.Columns[14]; }
		}
        
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string Name = @"Name";
			 public static string ElementsKey = @"ElementsKey";
			 public static string CreatedBy = @"CreatedBy";
			 public static string CreatedOn = @"CreatedOn";
			 public static string ModifiedBy = @"ModifiedBy";
			 public static string ModifiedOn = @"ModifiedOn";
			 public static string RowStatusId = @"RowStatusId";
			 public static string ConfigurationServiceItemId = @"ConfigurationServiceItemId";
			 public static string ConfigurationServiceLabelTypeId = @"ConfigurationServiceLabelTypeId";
			 public static string Description = @"Description";
			 public static string Help = @"Help";
			 public static string ValueList = @"ValueList";
			 public static string InputRequired = @"InputRequired";
			 public static string SortOrder = @"SortOrder";
			
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
