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
	/// Strongly-typed collection for the VwMapQueryParameterWorkflowType class.
	/// </summary>
	[Serializable]
	public partial class VwMapQueryParameterWorkflowTypeCollection : ReadOnlyList<VwMapQueryParameterWorkflowType, VwMapQueryParameterWorkflowTypeCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapQueryParameterWorkflowTypeCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapQueryParameterWorkflowType);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapQueryParameterWorkflowType class.
	/// </summary>
	[Serializable]
	public partial class VwMapQueryParameterWorkflowTypeController : BaseReadOnlyRecordController<VwMapQueryParameterWorkflowType, VwMapQueryParameterWorkflowTypeCollection, VwMapQueryParameterWorkflowTypeController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapQueryParameterWorkflowTypeController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapQueryParameterWorkflowType.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapQueryParameter_WorkflowType view.
	/// </summary>
	[Serializable]
	public partial class VwMapQueryParameterWorkflowType : ReadOnlyRecord<VwMapQueryParameterWorkflowType>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapQueryParameter_WorkflowType", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarQueryParameterName = new TableSchema.TableColumn(schema);
				colvarQueryParameterName.ColumnName = "QueryParameterName";
				colvarQueryParameterName.DataType = DbType.String;
				colvarQueryParameterName.MaxLength = 256;
				colvarQueryParameterName.AutoIncrement = false;
				colvarQueryParameterName.IsNullable = false;
				colvarQueryParameterName.IsPrimaryKey = false;
				colvarQueryParameterName.IsForeignKey = false;
				colvarQueryParameterName.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryParameterName);
				
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
				
				TableSchema.TableColumn colvarQueryParameterId = new TableSchema.TableColumn(schema);
				colvarQueryParameterId.ColumnName = "QueryParameterId";
				colvarQueryParameterId.DataType = DbType.Int32;
				colvarQueryParameterId.MaxLength = 0;
				colvarQueryParameterId.AutoIncrement = false;
				colvarQueryParameterId.IsNullable = false;
				colvarQueryParameterId.IsPrimaryKey = false;
				colvarQueryParameterId.IsForeignKey = false;
				colvarQueryParameterId.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryParameterId);
				
				TableSchema.TableColumn colvarWorkflowTypeId = new TableSchema.TableColumn(schema);
				colvarWorkflowTypeId.ColumnName = "WorkflowTypeId";
				colvarWorkflowTypeId.DataType = DbType.Int32;
				colvarWorkflowTypeId.MaxLength = 0;
				colvarWorkflowTypeId.AutoIncrement = false;
				colvarWorkflowTypeId.IsNullable = false;
				colvarWorkflowTypeId.IsPrimaryKey = false;
				colvarWorkflowTypeId.IsForeignKey = false;
				colvarWorkflowTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowTypeId);
				
				TableSchema.TableColumn colvarQueryParameterWorkflowTypeName = new TableSchema.TableColumn(schema);
				colvarQueryParameterWorkflowTypeName.ColumnName = "QueryParameterWorkflowTypeName";
				colvarQueryParameterWorkflowTypeName.DataType = DbType.String;
				colvarQueryParameterWorkflowTypeName.MaxLength = 256;
				colvarQueryParameterWorkflowTypeName.AutoIncrement = false;
				colvarQueryParameterWorkflowTypeName.IsNullable = false;
				colvarQueryParameterWorkflowTypeName.IsPrimaryKey = false;
				colvarQueryParameterWorkflowTypeName.IsForeignKey = false;
				colvarQueryParameterWorkflowTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryParameterWorkflowTypeName);
				
				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = 512;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				
				schema.Columns.Add(colvarDescription);
				
				TableSchema.TableColumn colvarMaximumSelection = new TableSchema.TableColumn(schema);
				colvarMaximumSelection.ColumnName = "MaximumSelection";
				colvarMaximumSelection.DataType = DbType.Int32;
				colvarMaximumSelection.MaxLength = 0;
				colvarMaximumSelection.AutoIncrement = false;
				colvarMaximumSelection.IsNullable = false;
				colvarMaximumSelection.IsPrimaryKey = false;
				colvarMaximumSelection.IsForeignKey = false;
				colvarMaximumSelection.IsReadOnly = false;
				
				schema.Columns.Add(colvarMaximumSelection);
				
				TableSchema.TableColumn colvarWildcard = new TableSchema.TableColumn(schema);
				colvarWildcard.ColumnName = "Wildcard";
				colvarWildcard.DataType = DbType.Boolean;
				colvarWildcard.MaxLength = 0;
				colvarWildcard.AutoIncrement = false;
				colvarWildcard.IsNullable = false;
				colvarWildcard.IsPrimaryKey = false;
				colvarWildcard.IsForeignKey = false;
				colvarWildcard.IsReadOnly = false;
				
				schema.Columns.Add(colvarWildcard);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapQueryParameter_WorkflowType",schema);
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
		protected VwMapQueryParameterWorkflowType(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapQueryParameterWorkflowType() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapQueryParameterWorkflowType(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapQueryParameterWorkflowType(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapQueryParameterWorkflowType(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapQueryParameterWorkflowType"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapQueryParameterWorkflowType(VwMapQueryParameterWorkflowType original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapQueryParameterWorkflowType original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.QueryParameterName = original.QueryParameterName;
			
			this.RowStatusId = original.RowStatusId;
			
			this.RowStatusName = original.RowStatusName;
			
			this.Name = original.Name;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.QueryParameterId = original.QueryParameterId;
			
			this.WorkflowTypeId = original.WorkflowTypeId;
			
			this.QueryParameterWorkflowTypeName = original.QueryParameterWorkflowTypeName;
			
			this.Description = original.Description;
			
			this.MaximumSelection = original.MaximumSelection;
			
			this.Wildcard = original.Wildcard;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapQueryParameterWorkflowType"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapQueryParameterWorkflowType Copy(VwMapQueryParameterWorkflowType original)
		{
			return new VwMapQueryParameterWorkflowType(original);
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
		partial void OnQueryParameterIdChanging(int newValue);
		partial void OnQueryParameterIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryParameterId")]
		[Bindable(true)]
		public int QueryParameterId 
		{
			get
			{
				return GetColumnValue<int>("QueryParameterId");
			}
			set
			{
				this.OnQueryParameterIdChanging(value);
				this.OnPropertyChanging("QueryParameterId", value);
				int oldValue = this.QueryParameterId;
				SetColumnValue("QueryParameterId", value);
				this.OnQueryParameterIdChanged(oldValue, value);
				this.OnPropertyChanged("QueryParameterId", oldValue, value);
			}
		}
		partial void OnWorkflowTypeIdChanging(int newValue);
		partial void OnWorkflowTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowTypeId")]
		[Bindable(true)]
		public int WorkflowTypeId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowTypeId");
			}
			set
			{
				this.OnWorkflowTypeIdChanging(value);
				this.OnPropertyChanging("WorkflowTypeId", value);
				int oldValue = this.WorkflowTypeId;
				SetColumnValue("WorkflowTypeId", value);
				this.OnWorkflowTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowTypeId", oldValue, value);
			}
		}
		partial void OnQueryParameterWorkflowTypeNameChanging(string newValue);
		partial void OnQueryParameterWorkflowTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryParameterWorkflowTypeName")]
		[Bindable(true)]
		public string QueryParameterWorkflowTypeName 
		{
			get
			{
				return GetColumnValue<string>("QueryParameterWorkflowTypeName");
			}
			set
			{
				this.OnQueryParameterWorkflowTypeNameChanging(value);
				this.OnPropertyChanging("QueryParameterWorkflowTypeName", value);
				string oldValue = this.QueryParameterWorkflowTypeName;
				SetColumnValue("QueryParameterWorkflowTypeName", value);
				this.OnQueryParameterWorkflowTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("QueryParameterWorkflowTypeName", oldValue, value);
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
		partial void OnMaximumSelectionChanging(int newValue);
		partial void OnMaximumSelectionChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("MaximumSelection")]
		[Bindable(true)]
		public int MaximumSelection 
		{
			get
			{
				return GetColumnValue<int>("MaximumSelection");
			}
			set
			{
				this.OnMaximumSelectionChanging(value);
				this.OnPropertyChanging("MaximumSelection", value);
				int oldValue = this.MaximumSelection;
				SetColumnValue("MaximumSelection", value);
				this.OnMaximumSelectionChanged(oldValue, value);
				this.OnPropertyChanged("MaximumSelection", oldValue, value);
			}
		}
		partial void OnWildcardChanging(bool newValue);
		partial void OnWildcardChanged(bool oldValue, bool newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Wildcard")]
		[Bindable(true)]
		public bool Wildcard 
		{
			get
			{
				return GetColumnValue<bool>("Wildcard");
			}
			set
			{
				this.OnWildcardChanging(value);
				this.OnPropertyChanging("Wildcard", value);
				bool oldValue = this.Wildcard;
				SetColumnValue("Wildcard", value);
				this.OnWildcardChanged(oldValue, value);
				this.OnPropertyChanged("Wildcard", oldValue, value);
			}
		}
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string Id = @"Id";
			
			public static string QueryParameterName = @"QueryParameterName";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string RowStatusName = @"RowStatusName";
			
			public static string Name = @"Name";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string QueryParameterId = @"QueryParameterId";
			
			public static string WorkflowTypeId = @"WorkflowTypeId";
			
			public static string QueryParameterWorkflowTypeName = @"QueryParameterWorkflowTypeName";
			
			public static string Description = @"Description";
			
			public static string MaximumSelection = @"MaximumSelection";
			
			public static string Wildcard = @"Wildcard";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapQueryParameterWorkflowType"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapQueryParameterWorkflowType#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapQueryParameterWorkflowType"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapQueryParameterWorkflowType"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapQueryParameterWorkflowType"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapQueryParameterWorkflowType instance1 = this;
			VwMapQueryParameterWorkflowType instance2 = obj as VwMapQueryParameterWorkflowType;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.QueryParameterName == instance2.QueryParameterName)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.QueryParameterId == instance2.QueryParameterId)
			
				&& (instance1.WorkflowTypeId == instance2.WorkflowTypeId)
			
				&& (instance1.QueryParameterWorkflowTypeName == instance2.QueryParameterWorkflowTypeName)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.MaximumSelection == instance2.MaximumSelection)
			
				&& (instance1.Wildcard == instance2.Wildcard)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapQueryParameterWorkflowType"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapQueryParameterWorkflowType"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapQueryParameterWorkflowType"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapQueryParameterWorkflowType instance1, VwMapQueryParameterWorkflowType instance2)
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
