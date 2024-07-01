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
	/// Strongly-typed collection for the VwMapWorkflowModuleVersion class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModuleVersionCollection : ReadOnlyList<VwMapWorkflowModuleVersion, VwMapWorkflowModuleVersionCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleVersionCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapWorkflowModuleVersion);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapWorkflowModuleVersion class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModuleVersionController : BaseReadOnlyRecordController<VwMapWorkflowModuleVersion, VwMapWorkflowModuleVersionCollection, VwMapWorkflowModuleVersionController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleVersionController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapWorkflowModuleVersion.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapWorkflowModuleVersion view.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModuleVersion : ReadOnlyRecord<VwMapWorkflowModuleVersion>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapWorkflowModuleVersion", TableType.View, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
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
				
				TableSchema.TableColumn colvarVersionMajor = new TableSchema.TableColumn(schema);
				colvarVersionMajor.ColumnName = "VersionMajor";
				colvarVersionMajor.DataType = DbType.Int32;
				colvarVersionMajor.MaxLength = 0;
				colvarVersionMajor.AutoIncrement = false;
				colvarVersionMajor.IsNullable = false;
				colvarVersionMajor.IsPrimaryKey = false;
				colvarVersionMajor.IsForeignKey = false;
				colvarVersionMajor.IsReadOnly = false;
				
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
				
				schema.Columns.Add(colvarVersionMinor);
				
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
				
				TableSchema.TableColumn colvarWorkflowModuleCategoryId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleCategoryId.ColumnName = "WorkflowModuleCategoryId";
				colvarWorkflowModuleCategoryId.DataType = DbType.Int32;
				colvarWorkflowModuleCategoryId.MaxLength = 0;
				colvarWorkflowModuleCategoryId.AutoIncrement = false;
				colvarWorkflowModuleCategoryId.IsNullable = false;
				colvarWorkflowModuleCategoryId.IsPrimaryKey = false;
				colvarWorkflowModuleCategoryId.IsForeignKey = false;
				colvarWorkflowModuleCategoryId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleCategoryId);
				
				TableSchema.TableColumn colvarWorkflowModuleSubCategoryId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleSubCategoryId.ColumnName = "WorkflowModuleSubCategoryId";
				colvarWorkflowModuleSubCategoryId.DataType = DbType.Int32;
				colvarWorkflowModuleSubCategoryId.MaxLength = 0;
				colvarWorkflowModuleSubCategoryId.AutoIncrement = false;
				colvarWorkflowModuleSubCategoryId.IsNullable = false;
				colvarWorkflowModuleSubCategoryId.IsPrimaryKey = false;
				colvarWorkflowModuleSubCategoryId.IsForeignKey = false;
				colvarWorkflowModuleSubCategoryId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleSubCategoryId);
				
				TableSchema.TableColumn colvarWorkflowModuleSubCategoryName = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleSubCategoryName.ColumnName = "WorkflowModuleSubCategoryName";
				colvarWorkflowModuleSubCategoryName.DataType = DbType.String;
				colvarWorkflowModuleSubCategoryName.MaxLength = 256;
				colvarWorkflowModuleSubCategoryName.AutoIncrement = false;
				colvarWorkflowModuleSubCategoryName.IsNullable = false;
				colvarWorkflowModuleSubCategoryName.IsPrimaryKey = false;
				colvarWorkflowModuleSubCategoryName.IsForeignKey = false;
				colvarWorkflowModuleSubCategoryName.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleSubCategoryName);
				
				TableSchema.TableColumn colvarWorkflowModuleCategoryName = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleCategoryName.ColumnName = "WorkflowModuleCategoryName";
				colvarWorkflowModuleCategoryName.DataType = DbType.String;
				colvarWorkflowModuleCategoryName.MaxLength = 256;
				colvarWorkflowModuleCategoryName.AutoIncrement = false;
				colvarWorkflowModuleCategoryName.IsNullable = false;
				colvarWorkflowModuleCategoryName.IsPrimaryKey = false;
				colvarWorkflowModuleCategoryName.IsForeignKey = false;
				colvarWorkflowModuleCategoryName.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleCategoryName);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapWorkflowModuleVersion",schema);
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
		protected VwMapWorkflowModuleVersion(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleVersion() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleVersion(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleVersion(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleVersion(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapWorkflowModuleVersion"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapWorkflowModuleVersion(VwMapWorkflowModuleVersion original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapWorkflowModuleVersion original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Name = original.Name;
			
			this.VersionMajor = original.VersionMajor;
			
			this.VersionMinor = original.VersionMinor;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.WorkflowModuleCategoryId = original.WorkflowModuleCategoryId;
			
			this.WorkflowModuleSubCategoryId = original.WorkflowModuleSubCategoryId;
			
			this.WorkflowModuleSubCategoryName = original.WorkflowModuleSubCategoryName;
			
			this.WorkflowModuleCategoryName = original.WorkflowModuleCategoryName;
			
			this.Id = original.Id;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapWorkflowModuleVersion"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapWorkflowModuleVersion Copy(VwMapWorkflowModuleVersion original)
		{
			return new VwMapWorkflowModuleVersion(original);
		}
		#endregion
		#endregion
		#region Properties
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		
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
		partial void OnVersionMajorChanging(int newValue);
		partial void OnVersionMajorChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("VersionMajor")]
		[Bindable(true)]
		public int VersionMajor 
		{
			get
			{
				return GetColumnValue<int>("VersionMajor");
			}
			set
			{
				this.OnVersionMajorChanging(value);
				this.OnPropertyChanging("VersionMajor", value);
				int oldValue = this.VersionMajor;
				SetColumnValue("VersionMajor", value);
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
			get
			{
				return GetColumnValue<int>("VersionMinor");
			}
			set
			{
				this.OnVersionMinorChanging(value);
				this.OnPropertyChanging("VersionMinor", value);
				int oldValue = this.VersionMinor;
				SetColumnValue("VersionMinor", value);
				this.OnVersionMinorChanged(oldValue, value);
				this.OnPropertyChanged("VersionMinor", oldValue, value);
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
		partial void OnWorkflowModuleCategoryIdChanging(int newValue);
		partial void OnWorkflowModuleCategoryIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleCategoryId")]
		[Bindable(true)]
		public int WorkflowModuleCategoryId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowModuleCategoryId");
			}
			set
			{
				this.OnWorkflowModuleCategoryIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleCategoryId", value);
				int oldValue = this.WorkflowModuleCategoryId;
				SetColumnValue("WorkflowModuleCategoryId", value);
				this.OnWorkflowModuleCategoryIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleCategoryId", oldValue, value);
			}
		}
		partial void OnWorkflowModuleSubCategoryIdChanging(int newValue);
		partial void OnWorkflowModuleSubCategoryIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleSubCategoryId")]
		[Bindable(true)]
		public int WorkflowModuleSubCategoryId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowModuleSubCategoryId");
			}
			set
			{
				this.OnWorkflowModuleSubCategoryIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleSubCategoryId", value);
				int oldValue = this.WorkflowModuleSubCategoryId;
				SetColumnValue("WorkflowModuleSubCategoryId", value);
				this.OnWorkflowModuleSubCategoryIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleSubCategoryId", oldValue, value);
			}
		}
		partial void OnWorkflowModuleSubCategoryNameChanging(string newValue);
		partial void OnWorkflowModuleSubCategoryNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleSubCategoryName")]
		[Bindable(true)]
		public string WorkflowModuleSubCategoryName 
		{
			get
			{
				return GetColumnValue<string>("WorkflowModuleSubCategoryName");
			}
			set
			{
				this.OnWorkflowModuleSubCategoryNameChanging(value);
				this.OnPropertyChanging("WorkflowModuleSubCategoryName", value);
				string oldValue = this.WorkflowModuleSubCategoryName;
				SetColumnValue("WorkflowModuleSubCategoryName", value);
				this.OnWorkflowModuleSubCategoryNameChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleSubCategoryName", oldValue, value);
			}
		}
		partial void OnWorkflowModuleCategoryNameChanging(string newValue);
		partial void OnWorkflowModuleCategoryNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleCategoryName")]
		[Bindable(true)]
		public string WorkflowModuleCategoryName 
		{
			get
			{
				return GetColumnValue<string>("WorkflowModuleCategoryName");
			}
			set
			{
				this.OnWorkflowModuleCategoryNameChanging(value);
				this.OnPropertyChanging("WorkflowModuleCategoryName", value);
				string oldValue = this.WorkflowModuleCategoryName;
				SetColumnValue("WorkflowModuleCategoryName", value);
				this.OnWorkflowModuleCategoryNameChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleCategoryName", oldValue, value);
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
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string Name = @"Name";
			
			public static string VersionMajor = @"VersionMajor";
			
			public static string VersionMinor = @"VersionMinor";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string WorkflowModuleCategoryId = @"WorkflowModuleCategoryId";
			
			public static string WorkflowModuleSubCategoryId = @"WorkflowModuleSubCategoryId";
			
			public static string WorkflowModuleSubCategoryName = @"WorkflowModuleSubCategoryName";
			
			public static string WorkflowModuleCategoryName = @"WorkflowModuleCategoryName";
			
			public static string Id = @"Id";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapWorkflowModuleVersion"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapWorkflowModuleVersion#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowModuleVersion"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapWorkflowModuleVersion"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowModuleVersion"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapWorkflowModuleVersion instance1 = this;
			VwMapWorkflowModuleVersion instance2 = obj as VwMapWorkflowModuleVersion;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.VersionMajor == instance2.VersionMajor)
			
				&& (instance1.VersionMinor == instance2.VersionMinor)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.WorkflowModuleCategoryId == instance2.WorkflowModuleCategoryId)
			
				&& (instance1.WorkflowModuleSubCategoryId == instance2.WorkflowModuleSubCategoryId)
			
				&& (instance1.WorkflowModuleSubCategoryName == instance2.WorkflowModuleSubCategoryName)
			
				&& (instance1.WorkflowModuleCategoryName == instance2.WorkflowModuleCategoryName)
			
				&& (instance1.Id == instance2.Id)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapWorkflowModuleVersion"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapWorkflowModuleVersion"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapWorkflowModuleVersion"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapWorkflowModuleVersion instance1, VwMapWorkflowModuleVersion instance2)
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
