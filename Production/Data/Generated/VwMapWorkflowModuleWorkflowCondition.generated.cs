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
	/// Strongly-typed collection for the VwMapWorkflowModuleWorkflowCondition class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModuleWorkflowConditionCollection : ReadOnlyList<VwMapWorkflowModuleWorkflowCondition, VwMapWorkflowModuleWorkflowConditionCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleWorkflowConditionCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapWorkflowModuleWorkflowCondition);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapWorkflowModuleWorkflowCondition class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModuleWorkflowConditionController : BaseReadOnlyRecordController<VwMapWorkflowModuleWorkflowCondition, VwMapWorkflowModuleWorkflowConditionCollection, VwMapWorkflowModuleWorkflowConditionController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleWorkflowConditionController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapWorkflowModuleWorkflowCondition.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapWorkflowModule_WorkflowCondition view.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowModuleWorkflowCondition : ReadOnlyRecord<VwMapWorkflowModuleWorkflowCondition>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapWorkflowModule_WorkflowCondition", TableType.View, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarWorkflowModuleName = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleName.ColumnName = "WorkflowModuleName";
				colvarWorkflowModuleName.DataType = DbType.String;
				colvarWorkflowModuleName.MaxLength = 256;
				colvarWorkflowModuleName.AutoIncrement = false;
				colvarWorkflowModuleName.IsNullable = false;
				colvarWorkflowModuleName.IsPrimaryKey = false;
				colvarWorkflowModuleName.IsForeignKey = false;
				colvarWorkflowModuleName.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleName);
				
				TableSchema.TableColumn colvarWorkflowConditionId = new TableSchema.TableColumn(schema);
				colvarWorkflowConditionId.ColumnName = "WorkflowConditionId";
				colvarWorkflowConditionId.DataType = DbType.Int32;
				colvarWorkflowConditionId.MaxLength = 0;
				colvarWorkflowConditionId.AutoIncrement = false;
				colvarWorkflowConditionId.IsNullable = false;
				colvarWorkflowConditionId.IsPrimaryKey = false;
				colvarWorkflowConditionId.IsForeignKey = false;
				colvarWorkflowConditionId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowConditionId);
				
				TableSchema.TableColumn colvarWorkflowModuleId = new TableSchema.TableColumn(schema);
				colvarWorkflowModuleId.ColumnName = "WorkflowModuleId";
				colvarWorkflowModuleId.DataType = DbType.Int32;
				colvarWorkflowModuleId.MaxLength = 0;
				colvarWorkflowModuleId.AutoIncrement = false;
				colvarWorkflowModuleId.IsNullable = false;
				colvarWorkflowModuleId.IsPrimaryKey = false;
				colvarWorkflowModuleId.IsForeignKey = false;
				colvarWorkflowModuleId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowModuleId);
				
				TableSchema.TableColumn colvarWorkflowConditionName = new TableSchema.TableColumn(schema);
				colvarWorkflowConditionName.ColumnName = "WorkflowConditionName";
				colvarWorkflowConditionName.DataType = DbType.String;
				colvarWorkflowConditionName.MaxLength = 256;
				colvarWorkflowConditionName.AutoIncrement = false;
				colvarWorkflowConditionName.IsNullable = false;
				colvarWorkflowConditionName.IsPrimaryKey = false;
				colvarWorkflowConditionName.IsForeignKey = false;
				colvarWorkflowConditionName.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowConditionName);
				
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
				
				TableSchema.TableColumn colvarVersion = new TableSchema.TableColumn(schema);
				colvarVersion.ColumnName = "Version";
				colvarVersion.DataType = DbType.String;
				colvarVersion.MaxLength = 256;
				colvarVersion.AutoIncrement = false;
				colvarVersion.IsNullable = true;
				colvarVersion.IsPrimaryKey = false;
				colvarVersion.IsForeignKey = false;
				colvarVersion.IsReadOnly = false;
				
				schema.Columns.Add(colvarVersion);
				
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
				
				TableSchema.TableColumn colvarOperatorX = new TableSchema.TableColumn(schema);
				colvarOperatorX.ColumnName = "Operator";
				colvarOperatorX.DataType = DbType.AnsiString;
				colvarOperatorX.MaxLength = 2;
				colvarOperatorX.AutoIncrement = false;
				colvarOperatorX.IsNullable = false;
				colvarOperatorX.IsPrimaryKey = false;
				colvarOperatorX.IsForeignKey = false;
				colvarOperatorX.IsReadOnly = false;
				
				schema.Columns.Add(colvarOperatorX);
				
				TableSchema.TableColumn colvarValueX = new TableSchema.TableColumn(schema);
				colvarValueX.ColumnName = "Value";
				colvarValueX.DataType = DbType.String;
				colvarValueX.MaxLength = 25;
				colvarValueX.AutoIncrement = false;
				colvarValueX.IsNullable = false;
				colvarValueX.IsPrimaryKey = false;
				colvarValueX.IsForeignKey = false;
				colvarValueX.IsReadOnly = false;
				
				schema.Columns.Add(colvarValueX);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapWorkflowModule_WorkflowCondition",schema);
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
		protected VwMapWorkflowModuleWorkflowCondition(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleWorkflowCondition() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleWorkflowCondition(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleWorkflowCondition(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowModuleWorkflowCondition(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapWorkflowModuleWorkflowCondition"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapWorkflowModuleWorkflowCondition(VwMapWorkflowModuleWorkflowCondition original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapWorkflowModuleWorkflowCondition original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.WorkflowModuleName = original.WorkflowModuleName;
			
			this.WorkflowConditionId = original.WorkflowConditionId;
			
			this.WorkflowModuleId = original.WorkflowModuleId;
			
			this.WorkflowConditionName = original.WorkflowConditionName;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.Id = original.Id;
			
			this.Version = original.Version;
			
			this.Description = original.Description;
			
			this.OperatorX = original.OperatorX;
			
			this.ValueX = original.ValueX;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapWorkflowModuleWorkflowCondition"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapWorkflowModuleWorkflowCondition Copy(VwMapWorkflowModuleWorkflowCondition original)
		{
			return new VwMapWorkflowModuleWorkflowCondition(original);
		}
		#endregion
		#endregion
		#region Properties
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		
		partial void OnWorkflowModuleNameChanging(string newValue);
		partial void OnWorkflowModuleNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleName")]
		[Bindable(true)]
		public string WorkflowModuleName 
		{
			get
			{
				return GetColumnValue<string>("WorkflowModuleName");
			}
			set
			{
				this.OnWorkflowModuleNameChanging(value);
				this.OnPropertyChanging("WorkflowModuleName", value);
				string oldValue = this.WorkflowModuleName;
				SetColumnValue("WorkflowModuleName", value);
				this.OnWorkflowModuleNameChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleName", oldValue, value);
			}
		}
		partial void OnWorkflowConditionIdChanging(int newValue);
		partial void OnWorkflowConditionIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowConditionId")]
		[Bindable(true)]
		public int WorkflowConditionId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowConditionId");
			}
			set
			{
				this.OnWorkflowConditionIdChanging(value);
				this.OnPropertyChanging("WorkflowConditionId", value);
				int oldValue = this.WorkflowConditionId;
				SetColumnValue("WorkflowConditionId", value);
				this.OnWorkflowConditionIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowConditionId", oldValue, value);
			}
		}
		partial void OnWorkflowModuleIdChanging(int newValue);
		partial void OnWorkflowModuleIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowModuleId")]
		[Bindable(true)]
		public int WorkflowModuleId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowModuleId");
			}
			set
			{
				this.OnWorkflowModuleIdChanging(value);
				this.OnPropertyChanging("WorkflowModuleId", value);
				int oldValue = this.WorkflowModuleId;
				SetColumnValue("WorkflowModuleId", value);
				this.OnWorkflowModuleIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowModuleId", oldValue, value);
			}
		}
		partial void OnWorkflowConditionNameChanging(string newValue);
		partial void OnWorkflowConditionNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowConditionName")]
		[Bindable(true)]
		public string WorkflowConditionName 
		{
			get
			{
				return GetColumnValue<string>("WorkflowConditionName");
			}
			set
			{
				this.OnWorkflowConditionNameChanging(value);
				this.OnPropertyChanging("WorkflowConditionName", value);
				string oldValue = this.WorkflowConditionName;
				SetColumnValue("WorkflowConditionName", value);
				this.OnWorkflowConditionNameChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowConditionName", oldValue, value);
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
		partial void OnVersionChanging(string newValue);
		partial void OnVersionChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Version")]
		[Bindable(true)]
		public string Version 
		{
			get
			{
				return GetColumnValue<string>("Version");
			}
			set
			{
				this.OnVersionChanging(value);
				this.OnPropertyChanging("Version", value);
				string oldValue = this.Version;
				SetColumnValue("Version", value);
				this.OnVersionChanged(oldValue, value);
				this.OnPropertyChanged("Version", oldValue, value);
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
		partial void OnOperatorXChanging(string newValue);
		partial void OnOperatorXChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("OperatorX")]
		[Bindable(true)]
		public string OperatorX 
		{
			get
			{
				return GetColumnValue<string>("Operator");
			}
			set
			{
				this.OnOperatorXChanging(value);
				this.OnPropertyChanging("OperatorX", value);
				string oldValue = this.OperatorX;
				SetColumnValue("Operator", value);
				this.OnOperatorXChanged(oldValue, value);
				this.OnPropertyChanged("OperatorX", oldValue, value);
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
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string WorkflowModuleName = @"WorkflowModuleName";
			
			public static string WorkflowConditionId = @"WorkflowConditionId";
			
			public static string WorkflowModuleId = @"WorkflowModuleId";
			
			public static string WorkflowConditionName = @"WorkflowConditionName";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string Id = @"Id";
			
			public static string Version = @"Version";
			
			public static string Description = @"Description";
			
			public static string OperatorX = @"Operator";
			
			public static string ValueX = @"Value";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapWorkflowModuleWorkflowCondition"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapWorkflowModuleWorkflowCondition#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowModuleWorkflowCondition"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapWorkflowModuleWorkflowCondition"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowModuleWorkflowCondition"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapWorkflowModuleWorkflowCondition instance1 = this;
			VwMapWorkflowModuleWorkflowCondition instance2 = obj as VwMapWorkflowModuleWorkflowCondition;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.WorkflowModuleName == instance2.WorkflowModuleName)
			
				&& (instance1.WorkflowConditionId == instance2.WorkflowConditionId)
			
				&& (instance1.WorkflowModuleId == instance2.WorkflowModuleId)
			
				&& (instance1.WorkflowConditionName == instance2.WorkflowConditionName)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Version == instance2.Version)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.OperatorX == instance2.OperatorX)
			
				&& (instance1.ValueX == instance2.ValueX)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapWorkflowModuleWorkflowCondition"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapWorkflowModuleWorkflowCondition"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapWorkflowModuleWorkflowCondition"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapWorkflowModuleWorkflowCondition instance1, VwMapWorkflowModuleWorkflowCondition instance2)
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
