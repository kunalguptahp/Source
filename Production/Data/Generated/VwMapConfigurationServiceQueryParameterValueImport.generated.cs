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
	/// Strongly-typed collection for the VwMapConfigurationServiceQueryParameterValueImport class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceQueryParameterValueImportCollection : ReadOnlyList<VwMapConfigurationServiceQueryParameterValueImport, VwMapConfigurationServiceQueryParameterValueImportCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceQueryParameterValueImportCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapConfigurationServiceQueryParameterValueImport);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapConfigurationServiceQueryParameterValueImport class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceQueryParameterValueImportController : BaseReadOnlyRecordController<VwMapConfigurationServiceQueryParameterValueImport, VwMapConfigurationServiceQueryParameterValueImportCollection, VwMapConfigurationServiceQueryParameterValueImportController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceQueryParameterValueImportController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapConfigurationServiceQueryParameterValueImport.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapConfigurationServiceQueryParameterValueImport view.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceQueryParameterValueImport : ReadOnlyRecord<VwMapConfigurationServiceQueryParameterValueImport>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapConfigurationServiceQueryParameterValueImport", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarQueryParameterName = new TableSchema.TableColumn(schema);
				colvarQueryParameterName.ColumnName = "QueryParameterName";
				colvarQueryParameterName.DataType = DbType.String;
				colvarQueryParameterName.MaxLength = 64;
				colvarQueryParameterName.AutoIncrement = false;
				colvarQueryParameterName.IsNullable = false;
				colvarQueryParameterName.IsPrimaryKey = false;
				colvarQueryParameterName.IsForeignKey = false;
				colvarQueryParameterName.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryParameterName);
				
				TableSchema.TableColumn colvarQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarQueryParameterValue.ColumnName = "QueryParameterValue";
				colvarQueryParameterValue.DataType = DbType.String;
				colvarQueryParameterValue.MaxLength = 256;
				colvarQueryParameterValue.AutoIncrement = false;
				colvarQueryParameterValue.IsNullable = false;
				colvarQueryParameterValue.IsPrimaryKey = false;
				colvarQueryParameterValue.IsForeignKey = false;
				colvarQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryParameterValue);
				
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
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapConfigurationServiceQueryParameterValueImport",schema);
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
		protected VwMapConfigurationServiceQueryParameterValueImport(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceQueryParameterValueImport() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceQueryParameterValueImport(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceQueryParameterValueImport(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceQueryParameterValueImport(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapConfigurationServiceQueryParameterValueImport"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapConfigurationServiceQueryParameterValueImport(VwMapConfigurationServiceQueryParameterValueImport original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapConfigurationServiceQueryParameterValueImport original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.RowStatusId = original.RowStatusId;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.QueryParameterName = original.QueryParameterName;
			
			this.QueryParameterValue = original.QueryParameterValue;
			
			this.ConfigurationServiceGroupImportId = original.ConfigurationServiceGroupImportId;
			
			this.Name = original.Name;
			
			this.ConfigurationServiceGroupImportName = original.ConfigurationServiceGroupImportName;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapConfigurationServiceQueryParameterValueImport"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapConfigurationServiceQueryParameterValueImport Copy(VwMapConfigurationServiceQueryParameterValueImport original)
		{
			return new VwMapConfigurationServiceQueryParameterValueImport(original);
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
		partial void OnQueryParameterNameChanging(string newValue);
		partial void OnQueryParameterNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryParameterName")]
		[Bindable(true)]
		public string QueryParameterName 
		{
			get
			{
				return GetColumnValue<string>("QueryParameterName");
			}
			set
			{
				this.OnQueryParameterNameChanging(value);
				this.OnPropertyChanging("QueryParameterName", value);
				string oldValue = this.QueryParameterName;
				SetColumnValue("QueryParameterName", value);
				this.OnQueryParameterNameChanged(oldValue, value);
				this.OnPropertyChanged("QueryParameterName", oldValue, value);
			}
		}
		partial void OnQueryParameterValueChanging(string newValue);
		partial void OnQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryParameterValue")]
		[Bindable(true)]
		public string QueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("QueryParameterValue");
			}
			set
			{
				this.OnQueryParameterValueChanging(value);
				this.OnPropertyChanging("QueryParameterValue", value);
				string oldValue = this.QueryParameterValue;
				SetColumnValue("QueryParameterValue", value);
				this.OnQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("QueryParameterValue", oldValue, value);
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
			
			public static string Id = @"Id";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string QueryParameterName = @"QueryParameterName";
			
			public static string QueryParameterValue = @"QueryParameterValue";
			
			public static string ConfigurationServiceGroupImportId = @"ConfigurationServiceGroupImportId";
			
			public static string Name = @"Name";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapConfigurationServiceQueryParameterValueImport"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapConfigurationServiceQueryParameterValueImport#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceQueryParameterValueImport"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapConfigurationServiceQueryParameterValueImport"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceQueryParameterValueImport"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapConfigurationServiceQueryParameterValueImport instance1 = this;
			VwMapConfigurationServiceQueryParameterValueImport instance2 = obj as VwMapConfigurationServiceQueryParameterValueImport;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.QueryParameterName == instance2.QueryParameterName)
			
				&& (instance1.QueryParameterValue == instance2.QueryParameterValue)
			
				&& (instance1.ConfigurationServiceGroupImportId == instance2.ConfigurationServiceGroupImportId)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.ConfigurationServiceGroupImportName == instance2.ConfigurationServiceGroupImportName)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapConfigurationServiceQueryParameterValueImport"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapConfigurationServiceQueryParameterValueImport"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapConfigurationServiceQueryParameterValueImport"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapConfigurationServiceQueryParameterValueImport instance1, VwMapConfigurationServiceQueryParameterValueImport instance2)
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
