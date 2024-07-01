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
	/// Strongly-typed collection for the VwMapConfigurationServiceLabelValueImport class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelValueImportCollection : ReadOnlyList<VwMapConfigurationServiceLabelValueImport, VwMapConfigurationServiceLabelValueImportCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValueImportCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapConfigurationServiceLabelValueImport);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapConfigurationServiceLabelValueImport class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelValueImportController : BaseReadOnlyRecordController<VwMapConfigurationServiceLabelValueImport, VwMapConfigurationServiceLabelValueImportCollection, VwMapConfigurationServiceLabelValueImportController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValueImportController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapConfigurationServiceLabelValueImport.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapConfigurationServiceLabelValueImport view.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceLabelValueImport : ReadOnlyRecord<VwMapConfigurationServiceLabelValueImport>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapConfigurationServiceLabelValueImport", TableType.View, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarLabelName = new TableSchema.TableColumn(schema);
				colvarLabelName.ColumnName = "LabelName";
				colvarLabelName.DataType = DbType.String;
				colvarLabelName.MaxLength = 256;
				colvarLabelName.AutoIncrement = false;
				colvarLabelName.IsNullable = false;
				colvarLabelName.IsPrimaryKey = false;
				colvarLabelName.IsForeignKey = false;
				colvarLabelName.IsReadOnly = false;
				
				schema.Columns.Add(colvarLabelName);
				
				TableSchema.TableColumn colvarItemName = new TableSchema.TableColumn(schema);
				colvarItemName.ColumnName = "ItemName";
				colvarItemName.DataType = DbType.String;
				colvarItemName.MaxLength = 256;
				colvarItemName.AutoIncrement = false;
				colvarItemName.IsNullable = false;
				colvarItemName.IsPrimaryKey = false;
				colvarItemName.IsForeignKey = false;
				colvarItemName.IsReadOnly = false;
				
				schema.Columns.Add(colvarItemName);
				
				TableSchema.TableColumn colvarLabelValue = new TableSchema.TableColumn(schema);
				colvarLabelValue.ColumnName = "LabelValue";
				colvarLabelValue.DataType = DbType.String;
				colvarLabelValue.MaxLength = 512;
				colvarLabelValue.AutoIncrement = false;
				colvarLabelValue.IsNullable = false;
				colvarLabelValue.IsPrimaryKey = false;
				colvarLabelValue.IsForeignKey = false;
				colvarLabelValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarLabelValue);
				
				TableSchema.TableColumn colvarConfigurationServiceGroupImportId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupImportId.ColumnName = "ConfigurationServiceGroupImportId";
				colvarConfigurationServiceGroupImportId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupImportId.MaxLength = 0;
				colvarConfigurationServiceGroupImportId.AutoIncrement = false;
				colvarConfigurationServiceGroupImportId.IsNullable = false;
				colvarConfigurationServiceGroupImportId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupImportId.IsForeignKey = false;
				colvarConfigurationServiceGroupImportId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupImportId);
				
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
				
				TableSchema.TableColumn colvarConfigurationServiceGroupImportName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupImportName.ColumnName = "ConfigurationServiceGroupImportName";
				colvarConfigurationServiceGroupImportName.DataType = DbType.String;
				colvarConfigurationServiceGroupImportName.MaxLength = 256;
				colvarConfigurationServiceGroupImportName.AutoIncrement = false;
				colvarConfigurationServiceGroupImportName.IsNullable = false;
				colvarConfigurationServiceGroupImportName.IsPrimaryKey = false;
				colvarConfigurationServiceGroupImportName.IsForeignKey = false;
				colvarConfigurationServiceGroupImportName.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupImportName);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapConfigurationServiceLabelValueImport",schema);
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
		protected VwMapConfigurationServiceLabelValueImport(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValueImport() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValueImport(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValueImport(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceLabelValueImport(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapConfigurationServiceLabelValueImport"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapConfigurationServiceLabelValueImport(VwMapConfigurationServiceLabelValueImport original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapConfigurationServiceLabelValueImport original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.LabelName = original.LabelName;
			
			this.ItemName = original.ItemName;
			
			this.LabelValue = original.LabelValue;
			
			this.ConfigurationServiceGroupImportId = original.ConfigurationServiceGroupImportId;
			
			this.Name = original.Name;
			
			this.Id = original.Id;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.ConfigurationServiceGroupImportName = original.ConfigurationServiceGroupImportName;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapConfigurationServiceLabelValueImport"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapConfigurationServiceLabelValueImport Copy(VwMapConfigurationServiceLabelValueImport original)
		{
			return new VwMapConfigurationServiceLabelValueImport(original);
		}
		#endregion
		#endregion
		#region Properties
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		
		partial void OnLabelNameChanging(string newValue);
		partial void OnLabelNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("LabelName")]
		[Bindable(true)]
		public string LabelName 
		{
			get
			{
				return GetColumnValue<string>("LabelName");
			}
			set
			{
				this.OnLabelNameChanging(value);
				this.OnPropertyChanging("LabelName", value);
				string oldValue = this.LabelName;
				SetColumnValue("LabelName", value);
				this.OnLabelNameChanged(oldValue, value);
				this.OnPropertyChanged("LabelName", oldValue, value);
			}
		}
		partial void OnItemNameChanging(string newValue);
		partial void OnItemNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ItemName")]
		[Bindable(true)]
		public string ItemName 
		{
			get
			{
				return GetColumnValue<string>("ItemName");
			}
			set
			{
				this.OnItemNameChanging(value);
				this.OnPropertyChanging("ItemName", value);
				string oldValue = this.ItemName;
				SetColumnValue("ItemName", value);
				this.OnItemNameChanged(oldValue, value);
				this.OnPropertyChanged("ItemName", oldValue, value);
			}
		}
		partial void OnLabelValueChanging(string newValue);
		partial void OnLabelValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("LabelValue")]
		[Bindable(true)]
		public string LabelValue 
		{
			get
			{
				return GetColumnValue<string>("LabelValue");
			}
			set
			{
				this.OnLabelValueChanging(value);
				this.OnPropertyChanging("LabelValue", value);
				string oldValue = this.LabelValue;
				SetColumnValue("LabelValue", value);
				this.OnLabelValueChanged(oldValue, value);
				this.OnPropertyChanged("LabelValue", oldValue, value);
			}
		}
		partial void OnConfigurationServiceGroupImportIdChanging(int newValue);
		partial void OnConfigurationServiceGroupImportIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupImportId")]
		[Bindable(true)]
		public int ConfigurationServiceGroupImportId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceGroupImportId");
			}
			set
			{
				this.OnConfigurationServiceGroupImportIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupImportId", value);
				int oldValue = this.ConfigurationServiceGroupImportId;
				SetColumnValue("ConfigurationServiceGroupImportId", value);
				this.OnConfigurationServiceGroupImportIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupImportId", oldValue, value);
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
		partial void OnConfigurationServiceGroupImportNameChanging(string newValue);
		partial void OnConfigurationServiceGroupImportNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupImportName")]
		[Bindable(true)]
		public string ConfigurationServiceGroupImportName 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceGroupImportName");
			}
			set
			{
				this.OnConfigurationServiceGroupImportNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupImportName", value);
				string oldValue = this.ConfigurationServiceGroupImportName;
				SetColumnValue("ConfigurationServiceGroupImportName", value);
				this.OnConfigurationServiceGroupImportNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupImportName", oldValue, value);
			}
		}
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string LabelName = @"LabelName";
			
			public static string ItemName = @"ItemName";
			
			public static string LabelValue = @"LabelValue";
			
			public static string ConfigurationServiceGroupImportId = @"ConfigurationServiceGroupImportId";
			
			public static string Name = @"Name";
			
			public static string Id = @"Id";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string ConfigurationServiceGroupImportName = @"ConfigurationServiceGroupImportName";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapConfigurationServiceLabelValueImport"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapConfigurationServiceLabelValueImport#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceLabelValueImport"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapConfigurationServiceLabelValueImport"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceLabelValueImport"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapConfigurationServiceLabelValueImport instance1 = this;
			VwMapConfigurationServiceLabelValueImport instance2 = obj as VwMapConfigurationServiceLabelValueImport;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.LabelName == instance2.LabelName)
			
				&& (instance1.ItemName == instance2.ItemName)
			
				&& (instance1.LabelValue == instance2.LabelValue)
			
				&& (instance1.ConfigurationServiceGroupImportId == instance2.ConfigurationServiceGroupImportId)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.ConfigurationServiceGroupImportName == instance2.ConfigurationServiceGroupImportName)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapConfigurationServiceLabelValueImport"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapConfigurationServiceLabelValueImport"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapConfigurationServiceLabelValueImport"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapConfigurationServiceLabelValueImport instance1, VwMapConfigurationServiceLabelValueImport instance2)
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
