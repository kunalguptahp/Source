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
	/// Strongly-typed collection for the VwMapConfigurationServiceLabelValue class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelValueCollection : ReadOnlyList<VwMapConfigurationServiceLabelValue, VwMapConfigurationServiceLabelValueCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValueCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapConfigurationServiceLabelValue);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapConfigurationServiceLabelValue class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelValueController : BaseReadOnlyRecordController<VwMapConfigurationServiceLabelValue, VwMapConfigurationServiceLabelValueCollection, VwMapConfigurationServiceLabelValueController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValueController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapConfigurationServiceLabelValue.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapConfigurationServiceLabelValue view.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelValue : ReadOnlyRecord<VwMapConfigurationServiceLabelValue>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapConfigurationServiceLabelValue", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarConfigurationServiceGroupId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupId.ColumnName = "ConfigurationServiceGroupId";
				colvarConfigurationServiceGroupId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupId.MaxLength = 0;
				colvarConfigurationServiceGroupId.AutoIncrement = false;
				colvarConfigurationServiceGroupId.IsNullable = false;
				colvarConfigurationServiceGroupId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupId.IsForeignKey = false;
				colvarConfigurationServiceGroupId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupId);
				
				TableSchema.TableColumn colvarConfigurationServiceLabelId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceLabelId.ColumnName = "ConfigurationServiceLabelId";
				colvarConfigurationServiceLabelId.DataType = DbType.Int32;
				colvarConfigurationServiceLabelId.MaxLength = 0;
				colvarConfigurationServiceLabelId.AutoIncrement = false;
				colvarConfigurationServiceLabelId.IsNullable = false;
				colvarConfigurationServiceLabelId.IsPrimaryKey = false;
				colvarConfigurationServiceLabelId.IsForeignKey = false;
				colvarConfigurationServiceLabelId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceLabelId);
				
				TableSchema.TableColumn colvarConfigurationServiceLabelName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceLabelName.ColumnName = "ConfigurationServiceLabelName";
				colvarConfigurationServiceLabelName.DataType = DbType.String;
				colvarConfigurationServiceLabelName.MaxLength = 256;
				colvarConfigurationServiceLabelName.AutoIncrement = false;
				colvarConfigurationServiceLabelName.IsNullable = false;
				colvarConfigurationServiceLabelName.IsPrimaryKey = false;
				colvarConfigurationServiceLabelName.IsForeignKey = false;
				colvarConfigurationServiceLabelName.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceLabelName);
				
				TableSchema.TableColumn colvarValueX = new TableSchema.TableColumn(schema);
				colvarValueX.ColumnName = "Value";
				colvarValueX.DataType = DbType.String;
				colvarValueX.MaxLength = -1;
				colvarValueX.AutoIncrement = false;
				colvarValueX.IsNullable = true;
				colvarValueX.IsPrimaryKey = false;
				colvarValueX.IsForeignKey = false;
				colvarValueX.IsReadOnly = false;
				
				schema.Columns.Add(colvarValueX);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapConfigurationServiceLabelValue",schema);
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
		protected VwMapConfigurationServiceLabelValue(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValue() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValue(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValue(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValue(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapConfigurationServiceLabelValue"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapConfigurationServiceLabelValue(VwMapConfigurationServiceLabelValue original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapConfigurationServiceLabelValue original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.RowStatusName = original.RowStatusName;
			
			this.ConfigurationServiceGroupId = original.ConfigurationServiceGroupId;
			
			this.ConfigurationServiceLabelId = original.ConfigurationServiceLabelId;
			
			this.ConfigurationServiceLabelName = original.ConfigurationServiceLabelName;
			
			this.ValueX = original.ValueX;
			
			this.ConfigurationServiceLabelTypeId = original.ConfigurationServiceLabelTypeId;
			
			this.ConfigurationServiceItemName = original.ConfigurationServiceItemName;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapConfigurationServiceLabelValue"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapConfigurationServiceLabelValue Copy(VwMapConfigurationServiceLabelValue original)
		{
			return new VwMapConfigurationServiceLabelValue(original);
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
		partial void OnConfigurationServiceGroupIdChanging(int newValue);
		partial void OnConfigurationServiceGroupIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupId")]
		[Bindable(true)]
		public int ConfigurationServiceGroupId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceGroupId");
			}
			set
			{
				this.OnConfigurationServiceGroupIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupId", value);
				int oldValue = this.ConfigurationServiceGroupId;
				SetColumnValue("ConfigurationServiceGroupId", value);
				this.OnConfigurationServiceGroupIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceLabelIdChanging(int newValue);
		partial void OnConfigurationServiceLabelIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceLabelId")]
		[Bindable(true)]
		public int ConfigurationServiceLabelId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceLabelId");
			}
			set
			{
				this.OnConfigurationServiceLabelIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceLabelId", value);
				int oldValue = this.ConfigurationServiceLabelId;
				SetColumnValue("ConfigurationServiceLabelId", value);
				this.OnConfigurationServiceLabelIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceLabelId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceLabelNameChanging(string newValue);
		partial void OnConfigurationServiceLabelNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceLabelName")]
		[Bindable(true)]
		public string ConfigurationServiceLabelName 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceLabelName");
			}
			set
			{
				this.OnConfigurationServiceLabelNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceLabelName", value);
				string oldValue = this.ConfigurationServiceLabelName;
				SetColumnValue("ConfigurationServiceLabelName", value);
				this.OnConfigurationServiceLabelNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceLabelName", oldValue, value);
			}
		}
		partial void OnValueXChanging(string newValue);
		partial void OnValueXChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ValueX")]
		[Bindable(true)]
		public string ValueX 
		{
			get
			{
				return GetColumnValue<string>("Value");
			}
			set
			{
				this.OnValueXChanging(value);
				this.OnPropertyChanging("ValueX", value);
				string oldValue = this.ValueX;
				SetColumnValue("Value", value);
				this.OnValueXChanged(oldValue, value);
				this.OnPropertyChanged("ValueX", oldValue, value);
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
			
			public static string RowStatusName = @"RowStatusName";
			
			public static string ConfigurationServiceGroupId = @"ConfigurationServiceGroupId";
			
			public static string ConfigurationServiceLabelId = @"ConfigurationServiceLabelId";
			
			public static string ConfigurationServiceLabelName = @"ConfigurationServiceLabelName";
			
			public static string ValueX = @"Value";
			
			public static string ConfigurationServiceLabelTypeId = @"ConfigurationServiceLabelTypeId";
			
			public static string ConfigurationServiceItemName = @"ConfigurationServiceItemName";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapConfigurationServiceLabelValue"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapConfigurationServiceLabelValue#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceLabelValue"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapConfigurationServiceLabelValue"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceLabelValue"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapConfigurationServiceLabelValue instance1 = this;
			VwMapConfigurationServiceLabelValue instance2 = obj as VwMapConfigurationServiceLabelValue;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.ConfigurationServiceGroupId == instance2.ConfigurationServiceGroupId)
			
				&& (instance1.ConfigurationServiceLabelId == instance2.ConfigurationServiceLabelId)
			
				&& (instance1.ConfigurationServiceLabelName == instance2.ConfigurationServiceLabelName)
			
				&& (instance1.ValueX == instance2.ValueX)
			
				&& (instance1.ConfigurationServiceLabelTypeId == instance2.ConfigurationServiceLabelTypeId)
			
				&& (instance1.ConfigurationServiceItemName == instance2.ConfigurationServiceItemName)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapConfigurationServiceLabelValue"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapConfigurationServiceLabelValue"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapConfigurationServiceLabelValue"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapConfigurationServiceLabelValue instance1, VwMapConfigurationServiceLabelValue instance2)
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
