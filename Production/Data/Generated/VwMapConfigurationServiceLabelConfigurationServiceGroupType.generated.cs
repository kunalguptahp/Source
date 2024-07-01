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
	/// Strongly-typed collection for the VwMapConfigurationServiceLabelConfigurationServiceGroupType class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection : ReadOnlyList<VwMapConfigurationServiceLabelConfigurationServiceGroupType, VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapConfigurationServiceLabelConfigurationServiceGroupType);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapConfigurationServiceLabelConfigurationServiceGroupType class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController : BaseReadOnlyRecordController<VwMapConfigurationServiceLabelConfigurationServiceGroupType, VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection, VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapConfigurationServiceLabelConfigurationServiceGroupType.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapConfigurationServiceLabel_ConfigurationServiceGroupType view.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelConfigurationServiceGroupType : ReadOnlyRecord<VwMapConfigurationServiceLabelConfigurationServiceGroupType>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapConfigurationServiceLabel_ConfigurationServiceGroupType", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarElementsKey = new TableSchema.TableColumn(schema);
				colvarElementsKey.ColumnName = "ElementsKey";
				colvarElementsKey.DataType = DbType.String;
				colvarElementsKey.MaxLength = 64;
				colvarElementsKey.AutoIncrement = false;
				colvarElementsKey.IsNullable = false;
				colvarElementsKey.IsPrimaryKey = false;
				colvarElementsKey.IsForeignKey = false;
				colvarElementsKey.IsReadOnly = false;
				
				schema.Columns.Add(colvarElementsKey);
				
				TableSchema.TableColumn colvarConfigurationServiceItemId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceItemId.ColumnName = "ConfigurationServiceItemId";
				colvarConfigurationServiceItemId.DataType = DbType.Int32;
				colvarConfigurationServiceItemId.MaxLength = 0;
				colvarConfigurationServiceItemId.AutoIncrement = false;
				colvarConfigurationServiceItemId.IsNullable = false;
				colvarConfigurationServiceItemId.IsPrimaryKey = false;
				colvarConfigurationServiceItemId.IsForeignKey = false;
				colvarConfigurationServiceItemId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceItemId);
				
				TableSchema.TableColumn colvarConfigurationServiceItemName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceItemName.ColumnName = "ConfigurationServiceItemName";
				colvarConfigurationServiceItemName.DataType = DbType.String;
				colvarConfigurationServiceItemName.MaxLength = 256;
				colvarConfigurationServiceItemName.AutoIncrement = false;
				colvarConfigurationServiceItemName.IsNullable = false;
				colvarConfigurationServiceItemName.IsPrimaryKey = false;
				colvarConfigurationServiceItemName.IsForeignKey = false;
				colvarConfigurationServiceItemName.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceItemName);
				
				TableSchema.TableColumn colvarConfigurationServiceGroupTypeId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupTypeId.ColumnName = "ConfigurationServiceGroupTypeId";
				colvarConfigurationServiceGroupTypeId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupTypeId.MaxLength = 0;
				colvarConfigurationServiceGroupTypeId.AutoIncrement = false;
				colvarConfigurationServiceGroupTypeId.IsNullable = false;
				colvarConfigurationServiceGroupTypeId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupTypeId.IsForeignKey = false;
				colvarConfigurationServiceGroupTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupTypeId);
				
				TableSchema.TableColumn colvarConfigurationServiceGroupTypeName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupTypeName.ColumnName = "ConfigurationServiceGroupTypeName";
				colvarConfigurationServiceGroupTypeName.DataType = DbType.String;
				colvarConfigurationServiceGroupTypeName.MaxLength = 256;
				colvarConfigurationServiceGroupTypeName.AutoIncrement = false;
				colvarConfigurationServiceGroupTypeName.IsNullable = false;
				colvarConfigurationServiceGroupTypeName.IsPrimaryKey = false;
				colvarConfigurationServiceGroupTypeName.IsForeignKey = false;
				colvarConfigurationServiceGroupTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupTypeName);
				
				TableSchema.TableColumn colvarConfigurationServiceLabelTypeId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceLabelTypeId.ColumnName = "ConfigurationServiceLabelTypeId";
				colvarConfigurationServiceLabelTypeId.DataType = DbType.Int32;
				colvarConfigurationServiceLabelTypeId.MaxLength = 0;
				colvarConfigurationServiceLabelTypeId.AutoIncrement = false;
				colvarConfigurationServiceLabelTypeId.IsNullable = false;
				colvarConfigurationServiceLabelTypeId.IsPrimaryKey = false;
				colvarConfigurationServiceLabelTypeId.IsForeignKey = false;
				colvarConfigurationServiceLabelTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceLabelTypeId);
				
				TableSchema.TableColumn colvarValueList = new TableSchema.TableColumn(schema);
				colvarValueList.ColumnName = "ValueList";
				colvarValueList.DataType = DbType.String;
				colvarValueList.MaxLength = 512;
				colvarValueList.AutoIncrement = false;
				colvarValueList.IsNullable = true;
				colvarValueList.IsPrimaryKey = false;
				colvarValueList.IsForeignKey = false;
				colvarValueList.IsReadOnly = false;
				
				schema.Columns.Add(colvarValueList);
				
				TableSchema.TableColumn colvarHelp = new TableSchema.TableColumn(schema);
				colvarHelp.ColumnName = "Help";
				colvarHelp.DataType = DbType.String;
				colvarHelp.MaxLength = 256;
				colvarHelp.AutoIncrement = false;
				colvarHelp.IsNullable = true;
				colvarHelp.IsPrimaryKey = false;
				colvarHelp.IsForeignKey = false;
				colvarHelp.IsReadOnly = false;
				
				schema.Columns.Add(colvarHelp);
				
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
				
				TableSchema.TableColumn colvarConfigurationServiceLabelTypeName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceLabelTypeName.ColumnName = "ConfigurationServiceLabelTypeName";
				colvarConfigurationServiceLabelTypeName.DataType = DbType.String;
				colvarConfigurationServiceLabelTypeName.MaxLength = 256;
				colvarConfigurationServiceLabelTypeName.AutoIncrement = false;
				colvarConfigurationServiceLabelTypeName.IsNullable = false;
				colvarConfigurationServiceLabelTypeName.IsPrimaryKey = false;
				colvarConfigurationServiceLabelTypeName.IsForeignKey = false;
				colvarConfigurationServiceLabelTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceLabelTypeName);
				
				TableSchema.TableColumn colvarInputRequired = new TableSchema.TableColumn(schema);
				colvarInputRequired.ColumnName = "InputRequired";
				colvarInputRequired.DataType = DbType.Boolean;
				colvarInputRequired.MaxLength = 0;
				colvarInputRequired.AutoIncrement = false;
				colvarInputRequired.IsNullable = false;
				colvarInputRequired.IsPrimaryKey = false;
				colvarInputRequired.IsForeignKey = false;
				colvarInputRequired.IsReadOnly = false;
				
				schema.Columns.Add(colvarInputRequired);
				
				TableSchema.TableColumn colvarConfigurationServiceItemSortOrder = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceItemSortOrder.ColumnName = "ConfigurationServiceItemSortOrder";
				colvarConfigurationServiceItemSortOrder.DataType = DbType.Int32;
				colvarConfigurationServiceItemSortOrder.MaxLength = 0;
				colvarConfigurationServiceItemSortOrder.AutoIncrement = false;
				colvarConfigurationServiceItemSortOrder.IsNullable = true;
				colvarConfigurationServiceItemSortOrder.IsPrimaryKey = false;
				colvarConfigurationServiceItemSortOrder.IsForeignKey = false;
				colvarConfigurationServiceItemSortOrder.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceItemSortOrder);
				
				TableSchema.TableColumn colvarSortOrder = new TableSchema.TableColumn(schema);
				colvarSortOrder.ColumnName = "SortOrder";
				colvarSortOrder.DataType = DbType.Int32;
				colvarSortOrder.MaxLength = 0;
				colvarSortOrder.AutoIncrement = false;
				colvarSortOrder.IsNullable = true;
				colvarSortOrder.IsPrimaryKey = false;
				colvarSortOrder.IsForeignKey = false;
				colvarSortOrder.IsReadOnly = false;
				
				schema.Columns.Add(colvarSortOrder);
				
				TableSchema.TableColumn colvarConfigurationServiceItemElementsKey = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceItemElementsKey.ColumnName = "ConfigurationServiceItemElementsKey";
				colvarConfigurationServiceItemElementsKey.DataType = DbType.String;
				colvarConfigurationServiceItemElementsKey.MaxLength = 64;
				colvarConfigurationServiceItemElementsKey.AutoIncrement = false;
				colvarConfigurationServiceItemElementsKey.IsNullable = false;
				colvarConfigurationServiceItemElementsKey.IsPrimaryKey = false;
				colvarConfigurationServiceItemElementsKey.IsForeignKey = false;
				colvarConfigurationServiceItemElementsKey.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceItemElementsKey);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapConfigurationServiceLabel_ConfigurationServiceGroupType",schema);
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
		protected VwMapConfigurationServiceLabelConfigurationServiceGroupType(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelConfigurationServiceGroupType() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelConfigurationServiceGroupType(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelConfigurationServiceGroupType(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelConfigurationServiceGroupType(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapConfigurationServiceLabelConfigurationServiceGroupType(VwMapConfigurationServiceLabelConfigurationServiceGroupType original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapConfigurationServiceLabelConfigurationServiceGroupType original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.Name = original.Name;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.RowStatusName = original.RowStatusName;
			
			this.ElementsKey = original.ElementsKey;
			
			this.ConfigurationServiceItemId = original.ConfigurationServiceItemId;
			
			this.ConfigurationServiceItemName = original.ConfigurationServiceItemName;
			
			this.ConfigurationServiceGroupTypeId = original.ConfigurationServiceGroupTypeId;
			
			this.ConfigurationServiceGroupTypeName = original.ConfigurationServiceGroupTypeName;
			
			this.ConfigurationServiceLabelTypeId = original.ConfigurationServiceLabelTypeId;
			
			this.ValueList = original.ValueList;
			
			this.Help = original.Help;
			
			this.Description = original.Description;
			
			this.ConfigurationServiceLabelTypeName = original.ConfigurationServiceLabelTypeName;
			
			this.InputRequired = original.InputRequired;
			
			this.ConfigurationServiceItemSortOrder = original.ConfigurationServiceItemSortOrder;
			
			this.SortOrder = original.SortOrder;
			
			this.ConfigurationServiceItemElementsKey = original.ConfigurationServiceItemElementsKey;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapConfigurationServiceLabelConfigurationServiceGroupType Copy(VwMapConfigurationServiceLabelConfigurationServiceGroupType original)
		{
			return new VwMapConfigurationServiceLabelConfigurationServiceGroupType(original);
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
		partial void OnElementsKeyChanging(string newValue);
		partial void OnElementsKeyChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ElementsKey")]
		[Bindable(true)]
		public string ElementsKey 
		{
			get
			{
				return GetColumnValue<string>("ElementsKey");
			}
			set
			{
				this.OnElementsKeyChanging(value);
				this.OnPropertyChanging("ElementsKey", value);
				string oldValue = this.ElementsKey;
				SetColumnValue("ElementsKey", value);
				this.OnElementsKeyChanged(oldValue, value);
				this.OnPropertyChanged("ElementsKey", oldValue, value);
			}
		}
		partial void OnConfigurationServiceItemIdChanging(int newValue);
		partial void OnConfigurationServiceItemIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceItemId")]
		[Bindable(true)]
		public int ConfigurationServiceItemId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceItemId");
			}
			set
			{
				this.OnConfigurationServiceItemIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceItemId", value);
				int oldValue = this.ConfigurationServiceItemId;
				SetColumnValue("ConfigurationServiceItemId", value);
				this.OnConfigurationServiceItemIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceItemId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceItemNameChanging(string newValue);
		partial void OnConfigurationServiceItemNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceItemName")]
		[Bindable(true)]
		public string ConfigurationServiceItemName 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceItemName");
			}
			set
			{
				this.OnConfigurationServiceItemNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceItemName", value);
				string oldValue = this.ConfigurationServiceItemName;
				SetColumnValue("ConfigurationServiceItemName", value);
				this.OnConfigurationServiceItemNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceItemName", oldValue, value);
			}
		}
		partial void OnConfigurationServiceGroupTypeIdChanging(int newValue);
		partial void OnConfigurationServiceGroupTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupTypeId")]
		[Bindable(true)]
		public int ConfigurationServiceGroupTypeId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceGroupTypeId");
			}
			set
			{
				this.OnConfigurationServiceGroupTypeIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupTypeId", value);
				int oldValue = this.ConfigurationServiceGroupTypeId;
				SetColumnValue("ConfigurationServiceGroupTypeId", value);
				this.OnConfigurationServiceGroupTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupTypeId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceGroupTypeNameChanging(string newValue);
		partial void OnConfigurationServiceGroupTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupTypeName")]
		[Bindable(true)]
		public string ConfigurationServiceGroupTypeName 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceGroupTypeName");
			}
			set
			{
				this.OnConfigurationServiceGroupTypeNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupTypeName", value);
				string oldValue = this.ConfigurationServiceGroupTypeName;
				SetColumnValue("ConfigurationServiceGroupTypeName", value);
				this.OnConfigurationServiceGroupTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupTypeName", oldValue, value);
			}
		}
		partial void OnConfigurationServiceLabelTypeIdChanging(int newValue);
		partial void OnConfigurationServiceLabelTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceLabelTypeId")]
		[Bindable(true)]
		public int ConfigurationServiceLabelTypeId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceLabelTypeId");
			}
			set
			{
				this.OnConfigurationServiceLabelTypeIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceLabelTypeId", value);
				int oldValue = this.ConfigurationServiceLabelTypeId;
				SetColumnValue("ConfigurationServiceLabelTypeId", value);
				this.OnConfigurationServiceLabelTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceLabelTypeId", oldValue, value);
			}
		}
		partial void OnValueListChanging(string newValue);
		partial void OnValueListChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ValueList")]
		[Bindable(true)]
		public string ValueList 
		{
			get
			{
				return GetColumnValue<string>("ValueList");
			}
			set
			{
				this.OnValueListChanging(value);
				this.OnPropertyChanging("ValueList", value);
				string oldValue = this.ValueList;
				SetColumnValue("ValueList", value);
				this.OnValueListChanged(oldValue, value);
				this.OnPropertyChanged("ValueList", oldValue, value);
			}
		}
		partial void OnHelpChanging(string newValue);
		partial void OnHelpChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Help")]
		[Bindable(true)]
		public string Help 
		{
			get
			{
				return GetColumnValue<string>("Help");
			}
			set
			{
				this.OnHelpChanging(value);
				this.OnPropertyChanging("Help", value);
				string oldValue = this.Help;
				SetColumnValue("Help", value);
				this.OnHelpChanged(oldValue, value);
				this.OnPropertyChanged("Help", oldValue, value);
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
		partial void OnConfigurationServiceLabelTypeNameChanging(string newValue);
		partial void OnConfigurationServiceLabelTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceLabelTypeName")]
		[Bindable(true)]
		public string ConfigurationServiceLabelTypeName 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceLabelTypeName");
			}
			set
			{
				this.OnConfigurationServiceLabelTypeNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceLabelTypeName", value);
				string oldValue = this.ConfigurationServiceLabelTypeName;
				SetColumnValue("ConfigurationServiceLabelTypeName", value);
				this.OnConfigurationServiceLabelTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceLabelTypeName", oldValue, value);
			}
		}
		partial void OnInputRequiredChanging(bool newValue);
		partial void OnInputRequiredChanged(bool oldValue, bool newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("InputRequired")]
		[Bindable(true)]
		public bool InputRequired 
		{
			get
			{
				return GetColumnValue<bool>("InputRequired");
			}
			set
			{
				this.OnInputRequiredChanging(value);
				this.OnPropertyChanging("InputRequired", value);
				bool oldValue = this.InputRequired;
				SetColumnValue("InputRequired", value);
				this.OnInputRequiredChanged(oldValue, value);
				this.OnPropertyChanged("InputRequired", oldValue, value);
			}
		}
		partial void OnConfigurationServiceItemSortOrderChanging(int? newValue);
		partial void OnConfigurationServiceItemSortOrderChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceItemSortOrder")]
		[Bindable(true)]
		public int? ConfigurationServiceItemSortOrder 
		{
			get
			{
				return GetColumnValue<int?>("ConfigurationServiceItemSortOrder");
			}
			set
			{
				this.OnConfigurationServiceItemSortOrderChanging(value);
				this.OnPropertyChanging("ConfigurationServiceItemSortOrder", value);
				int? oldValue = this.ConfigurationServiceItemSortOrder;
				SetColumnValue("ConfigurationServiceItemSortOrder", value);
				this.OnConfigurationServiceItemSortOrderChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceItemSortOrder", oldValue, value);
			}
		}
		partial void OnSortOrderChanging(int? newValue);
		partial void OnSortOrderChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("SortOrder")]
		[Bindable(true)]
		public int? SortOrder 
		{
			get
			{
				return GetColumnValue<int?>("SortOrder");
			}
			set
			{
				this.OnSortOrderChanging(value);
				this.OnPropertyChanging("SortOrder", value);
				int? oldValue = this.SortOrder;
				SetColumnValue("SortOrder", value);
				this.OnSortOrderChanged(oldValue, value);
				this.OnPropertyChanged("SortOrder", oldValue, value);
			}
		}
		partial void OnConfigurationServiceItemElementsKeyChanging(string newValue);
		partial void OnConfigurationServiceItemElementsKeyChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceItemElementsKey")]
		[Bindable(true)]
		public string ConfigurationServiceItemElementsKey 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceItemElementsKey");
			}
			set
			{
				this.OnConfigurationServiceItemElementsKeyChanging(value);
				this.OnPropertyChanging("ConfigurationServiceItemElementsKey", value);
				string oldValue = this.ConfigurationServiceItemElementsKey;
				SetColumnValue("ConfigurationServiceItemElementsKey", value);
				this.OnConfigurationServiceItemElementsKeyChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceItemElementsKey", oldValue, value);
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
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string RowStatusName = @"RowStatusName";
			
			public static string ElementsKey = @"ElementsKey";
			
			public static string ConfigurationServiceItemId = @"ConfigurationServiceItemId";
			
			public static string ConfigurationServiceItemName = @"ConfigurationServiceItemName";
			
			public static string ConfigurationServiceGroupTypeId = @"ConfigurationServiceGroupTypeId";
			
			public static string ConfigurationServiceGroupTypeName = @"ConfigurationServiceGroupTypeName";
			
			public static string ConfigurationServiceLabelTypeId = @"ConfigurationServiceLabelTypeId";
			
			public static string ValueList = @"ValueList";
			
			public static string Help = @"Help";
			
			public static string Description = @"Description";
			
			public static string ConfigurationServiceLabelTypeName = @"ConfigurationServiceLabelTypeName";
			
			public static string InputRequired = @"InputRequired";
			
			public static string ConfigurationServiceItemSortOrder = @"ConfigurationServiceItemSortOrder";
			
			public static string SortOrder = @"SortOrder";
			
			public static string ConfigurationServiceItemElementsKey = @"ConfigurationServiceItemElementsKey";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapConfigurationServiceLabelConfigurationServiceGroupType#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapConfigurationServiceLabelConfigurationServiceGroupType instance1 = this;
			VwMapConfigurationServiceLabelConfigurationServiceGroupType instance2 = obj as VwMapConfigurationServiceLabelConfigurationServiceGroupType;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.ElementsKey == instance2.ElementsKey)
			
				&& (instance1.ConfigurationServiceItemId == instance2.ConfigurationServiceItemId)
			
				&& (instance1.ConfigurationServiceItemName == instance2.ConfigurationServiceItemName)
			
				&& (instance1.ConfigurationServiceGroupTypeId == instance2.ConfigurationServiceGroupTypeId)
			
				&& (instance1.ConfigurationServiceGroupTypeName == instance2.ConfigurationServiceGroupTypeName)
			
				&& (instance1.ConfigurationServiceLabelTypeId == instance2.ConfigurationServiceLabelTypeId)
			
				&& (instance1.ValueList == instance2.ValueList)
			
				&& (instance1.Help == instance2.Help)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.ConfigurationServiceLabelTypeName == instance2.ConfigurationServiceLabelTypeName)
			
				&& (instance1.InputRequired == instance2.InputRequired)
			
				&& (instance1.ConfigurationServiceItemSortOrder == instance2.ConfigurationServiceItemSortOrder)
			
				&& (instance1.SortOrder == instance2.SortOrder)
			
				&& (instance1.ConfigurationServiceItemElementsKey == instance2.ConfigurationServiceItemElementsKey)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapConfigurationServiceLabelConfigurationServiceGroupType instance1, VwMapConfigurationServiceLabelConfigurationServiceGroupType instance2)
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
