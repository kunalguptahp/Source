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
	/// Strongly-typed collection for the VwMapConfigurationServiceItemConfigurationServiceGroupType class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceItemConfigurationServiceGroupTypeCollection : ReadOnlyList<VwMapConfigurationServiceItemConfigurationServiceGroupType, VwMapConfigurationServiceItemConfigurationServiceGroupTypeCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceItemConfigurationServiceGroupTypeCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapConfigurationServiceItemConfigurationServiceGroupType);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapConfigurationServiceItemConfigurationServiceGroupType class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceItemConfigurationServiceGroupTypeController : BaseReadOnlyRecordController<VwMapConfigurationServiceItemConfigurationServiceGroupType, VwMapConfigurationServiceItemConfigurationServiceGroupTypeCollection, VwMapConfigurationServiceItemConfigurationServiceGroupTypeController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceItemConfigurationServiceGroupTypeController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapConfigurationServiceItemConfigurationServiceGroupType.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapConfigurationServiceItem_ConfigurationServiceGroupType view.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceItemConfigurationServiceGroupType : ReadOnlyRecord<VwMapConfigurationServiceItemConfigurationServiceGroupType>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapConfigurationServiceItem_ConfigurationServiceGroupType", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapConfigurationServiceItem_ConfigurationServiceGroupType",schema);
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
		protected VwMapConfigurationServiceItemConfigurationServiceGroupType(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceItemConfigurationServiceGroupType() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceItemConfigurationServiceGroupType(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceItemConfigurationServiceGroupType(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceItemConfigurationServiceGroupType(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapConfigurationServiceItemConfigurationServiceGroupType(VwMapConfigurationServiceItemConfigurationServiceGroupType original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapConfigurationServiceItemConfigurationServiceGroupType original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.ConfigurationServiceGroupTypeId = original.ConfigurationServiceGroupTypeId;
			
			this.ConfigurationServiceItemId = original.ConfigurationServiceItemId;
			
			this.ConfigurationServiceGroupTypeName = original.ConfigurationServiceGroupTypeName;
			
			this.ConfigurationServiceItemName = original.ConfigurationServiceItemName;
			
			this.RowStatusName = original.RowStatusName;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapConfigurationServiceItemConfigurationServiceGroupType Copy(VwMapConfigurationServiceItemConfigurationServiceGroupType original)
		{
			return new VwMapConfigurationServiceItemConfigurationServiceGroupType(original);
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
			
			public static string ConfigurationServiceGroupTypeId = @"ConfigurationServiceGroupTypeId";
			
			public static string ConfigurationServiceItemId = @"ConfigurationServiceItemId";
			
			public static string ConfigurationServiceGroupTypeName = @"ConfigurationServiceGroupTypeName";
			
			public static string ConfigurationServiceItemName = @"ConfigurationServiceItemName";
			
			public static string RowStatusName = @"RowStatusName";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapConfigurationServiceItemConfigurationServiceGroupType#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapConfigurationServiceItemConfigurationServiceGroupType instance1 = this;
			VwMapConfigurationServiceItemConfigurationServiceGroupType instance2 = obj as VwMapConfigurationServiceItemConfigurationServiceGroupType;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.ConfigurationServiceGroupTypeId == instance2.ConfigurationServiceGroupTypeId)
			
				&& (instance1.ConfigurationServiceItemId == instance2.ConfigurationServiceItemId)
			
				&& (instance1.ConfigurationServiceGroupTypeName == instance2.ConfigurationServiceGroupTypeName)
			
				&& (instance1.ConfigurationServiceItemName == instance2.ConfigurationServiceItemName)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapConfigurationServiceItemConfigurationServiceGroupType"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapConfigurationServiceItemConfigurationServiceGroupType instance1, VwMapConfigurationServiceItemConfigurationServiceGroupType instance2)
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
