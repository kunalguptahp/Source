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
	/// Strongly-typed collection for the VwMapProxyURLTagTag class.
	/// </summary>
	[Serializable]
	public partial class VwMapProxyURLTagTagCollection : ReadOnlyList<VwMapProxyURLTagTag, VwMapProxyURLTagTagCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURLTagTagCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapProxyURLTagTag);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapProxyURLTagTag class.
	/// </summary>
	[Serializable]
	public partial class VwMapProxyURLTagTagController : BaseReadOnlyRecordController<VwMapProxyURLTagTag, VwMapProxyURLTagTagCollection, VwMapProxyURLTagTagController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURLTagTagController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapProxyURLTagTag.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapProxyURLTag_Tag view.
	/// </summary>
	[Serializable]
	public partial class VwMapProxyURLTagTag : ReadOnlyRecord<VwMapProxyURLTagTag>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapProxyURLTag_Tag", TableType.View, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarProxyURLId = new TableSchema.TableColumn(schema);
				colvarProxyURLId.ColumnName = "ProxyURLId";
				colvarProxyURLId.DataType = DbType.Int32;
				colvarProxyURLId.MaxLength = 0;
				colvarProxyURLId.AutoIncrement = false;
				colvarProxyURLId.IsNullable = false;
				colvarProxyURLId.IsPrimaryKey = false;
				colvarProxyURLId.IsForeignKey = false;
				colvarProxyURLId.IsReadOnly = false;
				
				schema.Columns.Add(colvarProxyURLId);
				
				TableSchema.TableColumn colvarTagId = new TableSchema.TableColumn(schema);
				colvarTagId.ColumnName = "TagId";
				colvarTagId.DataType = DbType.Int32;
				colvarTagId.MaxLength = 0;
				colvarTagId.AutoIncrement = false;
				colvarTagId.IsNullable = false;
				colvarTagId.IsPrimaryKey = false;
				colvarTagId.IsForeignKey = false;
				colvarTagId.IsReadOnly = false;
				
				schema.Columns.Add(colvarTagId);
				
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
				
				TableSchema.TableColumn colvarTagName = new TableSchema.TableColumn(schema);
				colvarTagName.ColumnName = "TagName";
				colvarTagName.DataType = DbType.String;
				colvarTagName.MaxLength = 256;
				colvarTagName.AutoIncrement = false;
				colvarTagName.IsNullable = false;
				colvarTagName.IsPrimaryKey = false;
				colvarTagName.IsForeignKey = false;
				colvarTagName.IsReadOnly = false;
				
				schema.Columns.Add(colvarTagName);
				
				TableSchema.TableColumn colvarTagRowStatusId = new TableSchema.TableColumn(schema);
				colvarTagRowStatusId.ColumnName = "TagRowStatusId";
				colvarTagRowStatusId.DataType = DbType.Int32;
				colvarTagRowStatusId.MaxLength = 0;
				colvarTagRowStatusId.AutoIncrement = false;
				colvarTagRowStatusId.IsNullable = false;
				colvarTagRowStatusId.IsPrimaryKey = false;
				colvarTagRowStatusId.IsForeignKey = false;
				colvarTagRowStatusId.IsReadOnly = false;
				
				schema.Columns.Add(colvarTagRowStatusId);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapProxyURLTag_Tag",schema);
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
		protected VwMapProxyURLTagTag(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURLTagTag() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURLTagTag(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURLTagTag(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapProxyURLTagTag(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapProxyURLTagTag"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapProxyURLTagTag(VwMapProxyURLTagTag original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapProxyURLTagTag original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.ProxyURLId = original.ProxyURLId;
			
			this.TagId = original.TagId;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.TagName = original.TagName;
			
			this.TagRowStatusId = original.TagRowStatusId;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapProxyURLTagTag"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapProxyURLTagTag Copy(VwMapProxyURLTagTag original)
		{
			return new VwMapProxyURLTagTag(original);
		}
		#endregion
		#endregion
		#region Properties
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		
		partial void OnProxyURLIdChanging(int newValue);
		partial void OnProxyURLIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProxyURLId")]
		[Bindable(true)]
		public int ProxyURLId 
		{
			get
			{
				return GetColumnValue<int>("ProxyURLId");
			}
			set
			{
				this.OnProxyURLIdChanging(value);
				this.OnPropertyChanging("ProxyURLId", value);
				int oldValue = this.ProxyURLId;
				SetColumnValue("ProxyURLId", value);
				this.OnProxyURLIdChanged(oldValue, value);
				this.OnPropertyChanged("ProxyURLId", oldValue, value);
			}
		}
		partial void OnTagIdChanging(int newValue);
		partial void OnTagIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TagId")]
		[Bindable(true)]
		public int TagId 
		{
			get
			{
				return GetColumnValue<int>("TagId");
			}
			set
			{
				this.OnTagIdChanging(value);
				this.OnPropertyChanging("TagId", value);
				int oldValue = this.TagId;
				SetColumnValue("TagId", value);
				this.OnTagIdChanged(oldValue, value);
				this.OnPropertyChanged("TagId", oldValue, value);
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
		partial void OnTagNameChanging(string newValue);
		partial void OnTagNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TagName")]
		[Bindable(true)]
		public string TagName 
		{
			get
			{
				return GetColumnValue<string>("TagName");
			}
			set
			{
				this.OnTagNameChanging(value);
				this.OnPropertyChanging("TagName", value);
				string oldValue = this.TagName;
				SetColumnValue("TagName", value);
				this.OnTagNameChanged(oldValue, value);
				this.OnPropertyChanged("TagName", oldValue, value);
			}
		}
		partial void OnTagRowStatusIdChanging(int newValue);
		partial void OnTagRowStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TagRowStatusId")]
		[Bindable(true)]
		public int TagRowStatusId 
		{
			get
			{
				return GetColumnValue<int>("TagRowStatusId");
			}
			set
			{
				this.OnTagRowStatusIdChanging(value);
				this.OnPropertyChanging("TagRowStatusId", value);
				int oldValue = this.TagRowStatusId;
				SetColumnValue("TagRowStatusId", value);
				this.OnTagRowStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("TagRowStatusId", oldValue, value);
			}
		}
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string ProxyURLId = @"ProxyURLId";
			
			public static string TagId = @"TagId";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string TagName = @"TagName";
			
			public static string TagRowStatusId = @"TagRowStatusId";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapProxyURLTagTag"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapProxyURLTagTag#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapProxyURLTagTag"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapProxyURLTagTag"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapProxyURLTagTag"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapProxyURLTagTag instance1 = this;
			VwMapProxyURLTagTag instance2 = obj as VwMapProxyURLTagTag;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.ProxyURLId == instance2.ProxyURLId)
			
				&& (instance1.TagId == instance2.TagId)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.TagName == instance2.TagName)
			
				&& (instance1.TagRowStatusId == instance2.TagRowStatusId)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapProxyURLTagTag"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapProxyURLTagTag"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapProxyURLTagTag"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapProxyURLTagTag instance1, VwMapProxyURLTagTag instance2)
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
