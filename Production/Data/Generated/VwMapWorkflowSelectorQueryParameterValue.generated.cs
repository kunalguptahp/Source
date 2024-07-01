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
	/// Strongly-typed collection for the VwMapWorkflowSelectorQueryParameterValue class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowSelectorQueryParameterValueCollection : ReadOnlyList<VwMapWorkflowSelectorQueryParameterValue, VwMapWorkflowSelectorQueryParameterValueCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowSelectorQueryParameterValueCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapWorkflowSelectorQueryParameterValue);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapWorkflowSelectorQueryParameterValue class.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowSelectorQueryParameterValueController : BaseReadOnlyRecordController<VwMapWorkflowSelectorQueryParameterValue, VwMapWorkflowSelectorQueryParameterValueCollection, VwMapWorkflowSelectorQueryParameterValueController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowSelectorQueryParameterValueController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapWorkflowSelectorQueryParameterValue.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapWorkflowSelector_QueryParameterValue view.
	/// </summary>
	[Serializable]
	public partial class VwMapWorkflowSelectorQueryParameterValue : ReadOnlyRecord<VwMapWorkflowSelectorQueryParameterValue>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapWorkflowSelector_QueryParameterValue", TableType.View, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
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
				
				TableSchema.TableColumn colvarQueryParameterValueName = new TableSchema.TableColumn(schema);
				colvarQueryParameterValueName.ColumnName = "QueryParameterValueName";
				colvarQueryParameterValueName.DataType = DbType.String;
				colvarQueryParameterValueName.MaxLength = 256;
				colvarQueryParameterValueName.AutoIncrement = false;
				colvarQueryParameterValueName.IsNullable = false;
				colvarQueryParameterValueName.IsPrimaryKey = false;
				colvarQueryParameterValueName.IsForeignKey = false;
				colvarQueryParameterValueName.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryParameterValueName);
				
				TableSchema.TableColumn colvarNegation = new TableSchema.TableColumn(schema);
				colvarNegation.ColumnName = "Negation";
				colvarNegation.DataType = DbType.Boolean;
				colvarNegation.MaxLength = 0;
				colvarNegation.AutoIncrement = false;
				colvarNegation.IsNullable = false;
				colvarNegation.IsPrimaryKey = false;
				colvarNegation.IsForeignKey = false;
				colvarNegation.IsReadOnly = false;
				
				schema.Columns.Add(colvarNegation);
				
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
				
				TableSchema.TableColumn colvarQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarQueryParameterValueId.ColumnName = "QueryParameterValueId";
				colvarQueryParameterValueId.DataType = DbType.Int32;
				colvarQueryParameterValueId.MaxLength = 0;
				colvarQueryParameterValueId.AutoIncrement = false;
				colvarQueryParameterValueId.IsNullable = false;
				colvarQueryParameterValueId.IsPrimaryKey = false;
				colvarQueryParameterValueId.IsForeignKey = false;
				colvarQueryParameterValueId.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryParameterValueId);
				
				TableSchema.TableColumn colvarWorkflowSelectorQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarWorkflowSelectorQueryParameterValueId.ColumnName = "WorkflowSelectorQueryParameterValueId";
				colvarWorkflowSelectorQueryParameterValueId.DataType = DbType.Int32;
				colvarWorkflowSelectorQueryParameterValueId.MaxLength = 0;
				colvarWorkflowSelectorQueryParameterValueId.AutoIncrement = false;
				colvarWorkflowSelectorQueryParameterValueId.IsNullable = false;
				colvarWorkflowSelectorQueryParameterValueId.IsPrimaryKey = false;
				colvarWorkflowSelectorQueryParameterValueId.IsForeignKey = false;
				colvarWorkflowSelectorQueryParameterValueId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowSelectorQueryParameterValueId);
				
				TableSchema.TableColumn colvarWorkflowSelectorId = new TableSchema.TableColumn(schema);
				colvarWorkflowSelectorId.ColumnName = "WorkflowSelectorId";
				colvarWorkflowSelectorId.DataType = DbType.Int32;
				colvarWorkflowSelectorId.MaxLength = 0;
				colvarWorkflowSelectorId.AutoIncrement = false;
				colvarWorkflowSelectorId.IsNullable = false;
				colvarWorkflowSelectorId.IsPrimaryKey = false;
				colvarWorkflowSelectorId.IsForeignKey = false;
				colvarWorkflowSelectorId.IsReadOnly = false;
				
				schema.Columns.Add(colvarWorkflowSelectorId);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapWorkflowSelector_QueryParameterValue",schema);
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
		protected VwMapWorkflowSelectorQueryParameterValue(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowSelectorQueryParameterValue() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowSelectorQueryParameterValue(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowSelectorQueryParameterValue(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapWorkflowSelectorQueryParameterValue(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapWorkflowSelectorQueryParameterValue"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapWorkflowSelectorQueryParameterValue(VwMapWorkflowSelectorQueryParameterValue original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapWorkflowSelectorQueryParameterValue original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.QueryParameterName = original.QueryParameterName;
			
			this.QueryParameterValueName = original.QueryParameterValueName;
			
			this.Negation = original.Negation;
			
			this.QueryParameterId = original.QueryParameterId;
			
			this.QueryParameterValueId = original.QueryParameterValueId;
			
			this.WorkflowSelectorQueryParameterValueId = original.WorkflowSelectorQueryParameterValueId;
			
			this.WorkflowSelectorId = original.WorkflowSelectorId;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.CreatedBy = original.CreatedBy;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapWorkflowSelectorQueryParameterValue"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapWorkflowSelectorQueryParameterValue Copy(VwMapWorkflowSelectorQueryParameterValue original)
		{
			return new VwMapWorkflowSelectorQueryParameterValue(original);
		}
		#endregion
		#endregion
		#region Properties
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		
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
		partial void OnQueryParameterValueNameChanging(string newValue);
		partial void OnQueryParameterValueNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryParameterValueName")]
		[Bindable(true)]
		public string QueryParameterValueName 
		{
			get
			{
				return GetColumnValue<string>("QueryParameterValueName");
			}
			set
			{
				this.OnQueryParameterValueNameChanging(value);
				this.OnPropertyChanging("QueryParameterValueName", value);
				string oldValue = this.QueryParameterValueName;
				SetColumnValue("QueryParameterValueName", value);
				this.OnQueryParameterValueNameChanged(oldValue, value);
				this.OnPropertyChanged("QueryParameterValueName", oldValue, value);
			}
		}
		partial void OnNegationChanging(bool newValue);
		partial void OnNegationChanged(bool oldValue, bool newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Negation")]
		[Bindable(true)]
		public bool Negation 
		{
			get
			{
				return GetColumnValue<bool>("Negation");
			}
			set
			{
				this.OnNegationChanging(value);
				this.OnPropertyChanging("Negation", value);
				bool oldValue = this.Negation;
				SetColumnValue("Negation", value);
				this.OnNegationChanged(oldValue, value);
				this.OnPropertyChanged("Negation", oldValue, value);
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
		partial void OnQueryParameterValueIdChanging(int newValue);
		partial void OnQueryParameterValueIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryParameterValueId")]
		[Bindable(true)]
		public int QueryParameterValueId 
		{
			get
			{
				return GetColumnValue<int>("QueryParameterValueId");
			}
			set
			{
				this.OnQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("QueryParameterValueId", value);
				int oldValue = this.QueryParameterValueId;
				SetColumnValue("QueryParameterValueId", value);
				this.OnQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("QueryParameterValueId", oldValue, value);
			}
		}
		partial void OnWorkflowSelectorQueryParameterValueIdChanging(int newValue);
		partial void OnWorkflowSelectorQueryParameterValueIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowSelectorQueryParameterValueId")]
		[Bindable(true)]
		public int WorkflowSelectorQueryParameterValueId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowSelectorQueryParameterValueId");
			}
			set
			{
				this.OnWorkflowSelectorQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("WorkflowSelectorQueryParameterValueId", value);
				int oldValue = this.WorkflowSelectorQueryParameterValueId;
				SetColumnValue("WorkflowSelectorQueryParameterValueId", value);
				this.OnWorkflowSelectorQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowSelectorQueryParameterValueId", oldValue, value);
			}
		}
		partial void OnWorkflowSelectorIdChanging(int newValue);
		partial void OnWorkflowSelectorIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkflowSelectorId")]
		[Bindable(true)]
		public int WorkflowSelectorId 
		{
			get
			{
				return GetColumnValue<int>("WorkflowSelectorId");
			}
			set
			{
				this.OnWorkflowSelectorIdChanging(value);
				this.OnPropertyChanging("WorkflowSelectorId", value);
				int oldValue = this.WorkflowSelectorId;
				SetColumnValue("WorkflowSelectorId", value);
				this.OnWorkflowSelectorIdChanged(oldValue, value);
				this.OnPropertyChanged("WorkflowSelectorId", oldValue, value);
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
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string QueryParameterName = @"QueryParameterName";
			
			public static string QueryParameterValueName = @"QueryParameterValueName";
			
			public static string Negation = @"Negation";
			
			public static string QueryParameterId = @"QueryParameterId";
			
			public static string QueryParameterValueId = @"QueryParameterValueId";
			
			public static string WorkflowSelectorQueryParameterValueId = @"WorkflowSelectorQueryParameterValueId";
			
			public static string WorkflowSelectorId = @"WorkflowSelectorId";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string CreatedBy = @"CreatedBy";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapWorkflowSelectorQueryParameterValue"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapWorkflowSelectorQueryParameterValue#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowSelectorQueryParameterValue"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapWorkflowSelectorQueryParameterValue"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapWorkflowSelectorQueryParameterValue"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapWorkflowSelectorQueryParameterValue instance1 = this;
			VwMapWorkflowSelectorQueryParameterValue instance2 = obj as VwMapWorkflowSelectorQueryParameterValue;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.QueryParameterName == instance2.QueryParameterName)
			
				&& (instance1.QueryParameterValueName == instance2.QueryParameterValueName)
			
				&& (instance1.Negation == instance2.Negation)
			
				&& (instance1.QueryParameterId == instance2.QueryParameterId)
			
				&& (instance1.QueryParameterValueId == instance2.QueryParameterValueId)
			
				&& (instance1.WorkflowSelectorQueryParameterValueId == instance2.WorkflowSelectorQueryParameterValueId)
			
				&& (instance1.WorkflowSelectorId == instance2.WorkflowSelectorId)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapWorkflowSelectorQueryParameterValue"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapWorkflowSelectorQueryParameterValue"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapWorkflowSelectorQueryParameterValue"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapWorkflowSelectorQueryParameterValue instance1, VwMapWorkflowSelectorQueryParameterValue instance2)
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
