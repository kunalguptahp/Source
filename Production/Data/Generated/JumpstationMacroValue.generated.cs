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
namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the JumpstationMacroValue class.
	/// </summary>
    [Serializable]
	public partial class JumpstationMacroValueCollection : ActiveList<JumpstationMacroValue, JumpstationMacroValueCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public JumpstationMacroValueCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(JumpstationMacroValue);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the JumpstationMacroValue table.
	/// </summary>
	[Serializable]
	public partial class JumpstationMacroValue : ActiveRecord<JumpstationMacroValue>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected JumpstationMacroValue(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public JumpstationMacroValue() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public JumpstationMacroValue(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public JumpstationMacroValue(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public JumpstationMacroValue(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="JumpstationMacroValue"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private JumpstationMacroValue(JumpstationMacroValue original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(JumpstationMacroValue original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.JumpstationMacroId = original.JumpstationMacroId;
			
			this.MatchName = original.MatchName;
			
			this.ResultValue = original.ResultValue;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="JumpstationMacroValue"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static JumpstationMacroValue Copy(JumpstationMacroValue original)
		{
			return new JumpstationMacroValue(original);
		}
		#endregion
		#endregion
		
		#region Schema and Query Accessor
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Query CreateQuery() { return new Query(Schema); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("JumpstationMacroValue", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
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
				colvarCreatedBy.DefaultSetting = @"";
				colvarCreatedBy.ForeignKeyTableName = "";
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
				colvarCreatedOn.DefaultSetting = @"";
				colvarCreatedOn.ForeignKeyTableName = "";
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
				colvarModifiedBy.DefaultSetting = @"";
				colvarModifiedBy.ForeignKeyTableName = "";
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
				colvarModifiedOn.DefaultSetting = @"";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);
				
				TableSchema.TableColumn colvarRowStatusId = new TableSchema.TableColumn(schema);
				colvarRowStatusId.ColumnName = "RowStatusId";
				colvarRowStatusId.DataType = DbType.Int32;
				colvarRowStatusId.MaxLength = 0;
				colvarRowStatusId.AutoIncrement = false;
				colvarRowStatusId.IsNullable = false;
				colvarRowStatusId.IsPrimaryKey = false;
				colvarRowStatusId.IsForeignKey = true;
				colvarRowStatusId.IsReadOnly = false;
				colvarRowStatusId.DefaultSetting = @"";
				
					colvarRowStatusId.ForeignKeyTableName = "RowStatus";
				schema.Columns.Add(colvarRowStatusId);
				
				TableSchema.TableColumn colvarJumpstationMacroId = new TableSchema.TableColumn(schema);
				colvarJumpstationMacroId.ColumnName = "JumpstationMacroId";
				colvarJumpstationMacroId.DataType = DbType.Int32;
				colvarJumpstationMacroId.MaxLength = 0;
				colvarJumpstationMacroId.AutoIncrement = false;
				colvarJumpstationMacroId.IsNullable = false;
				colvarJumpstationMacroId.IsPrimaryKey = false;
				colvarJumpstationMacroId.IsForeignKey = true;
				colvarJumpstationMacroId.IsReadOnly = false;
				colvarJumpstationMacroId.DefaultSetting = @"";
				
					colvarJumpstationMacroId.ForeignKeyTableName = "JumpstationMacro";
				schema.Columns.Add(colvarJumpstationMacroId);
				
				TableSchema.TableColumn colvarMatchName = new TableSchema.TableColumn(schema);
				colvarMatchName.ColumnName = "MatchName";
				colvarMatchName.DataType = DbType.String;
				colvarMatchName.MaxLength = 256;
				colvarMatchName.AutoIncrement = false;
				colvarMatchName.IsNullable = true;
				colvarMatchName.IsPrimaryKey = false;
				colvarMatchName.IsForeignKey = false;
				colvarMatchName.IsReadOnly = false;
				colvarMatchName.DefaultSetting = @"";
				colvarMatchName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMatchName);
				
				TableSchema.TableColumn colvarResultValue = new TableSchema.TableColumn(schema);
				colvarResultValue.ColumnName = "ResultValue";
				colvarResultValue.DataType = DbType.String;
				colvarResultValue.MaxLength = 256;
				colvarResultValue.AutoIncrement = false;
				colvarResultValue.IsNullable = false;
				colvarResultValue.IsPrimaryKey = false;
				colvarResultValue.IsForeignKey = false;
				colvarResultValue.IsReadOnly = false;
				colvarResultValue.DefaultSetting = @"";
				colvarResultValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResultValue);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("JumpstationMacroValue",schema);
			}
		}
		#endregion
		
		#region Props
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		partial void OnIdChanging(int newValue);
		partial void OnIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set
			{
				this.OnIdChanging(value);
				this.OnPropertyChanging("Id", value);
				int oldValue = this.Id;
				SetColumnValue(Columns.Id, value);
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
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set
			{
				this.OnCreatedByChanging(value);
				this.OnPropertyChanging("CreatedBy", value);
				string oldValue = this.CreatedBy;
				SetColumnValue(Columns.CreatedBy, value);
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
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set
			{
				this.OnCreatedOnChanging(value);
				this.OnPropertyChanging("CreatedOn", value);
				DateTime oldValue = this.CreatedOn;
				SetColumnValue(Columns.CreatedOn, value);
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
			get { return GetColumnValue<string>(Columns.ModifiedBy); }
			set
			{
				this.OnModifiedByChanging(value);
				this.OnPropertyChanging("ModifiedBy", value);
				string oldValue = this.ModifiedBy;
				SetColumnValue(Columns.ModifiedBy, value);
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
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set
			{
				this.OnModifiedOnChanging(value);
				this.OnPropertyChanging("ModifiedOn", value);
				DateTime oldValue = this.ModifiedOn;
				SetColumnValue(Columns.ModifiedOn, value);
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
			get { return GetColumnValue<int>(Columns.RowStatusId); }
			set
			{
				this.OnRowStatusIdChanging(value);
				this.OnPropertyChanging("RowStatusId", value);
				int oldValue = this.RowStatusId;
				SetColumnValue(Columns.RowStatusId, value);
				this.OnRowStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("RowStatusId", oldValue, value);
			}
		}
		partial void OnJumpstationMacroIdChanging(int newValue);
		partial void OnJumpstationMacroIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationMacroId")]
		[Bindable(true)]
		public int JumpstationMacroId 
		{
			get { return GetColumnValue<int>(Columns.JumpstationMacroId); }
			set
			{
				this.OnJumpstationMacroIdChanging(value);
				this.OnPropertyChanging("JumpstationMacroId", value);
				int oldValue = this.JumpstationMacroId;
				SetColumnValue(Columns.JumpstationMacroId, value);
				this.OnJumpstationMacroIdChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationMacroId", oldValue, value);
			}
		}
		partial void OnMatchNameChanging(string newValue);
		partial void OnMatchNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("MatchName")]
		[Bindable(true)]
		public string MatchName 
		{
			get { return GetColumnValue<string>(Columns.MatchName); }
			set
			{
				this.OnMatchNameChanging(value);
				this.OnPropertyChanging("MatchName", value);
				string oldValue = this.MatchName;
				SetColumnValue(Columns.MatchName, value);
				this.OnMatchNameChanged(oldValue, value);
				this.OnPropertyChanged("MatchName", oldValue, value);
			}
		}
		partial void OnResultValueChanging(string newValue);
		partial void OnResultValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ResultValue")]
		[Bindable(true)]
		public string ResultValue 
		{
			get { return GetColumnValue<string>(Columns.ResultValue); }
			set
			{
				this.OnResultValueChanging(value);
				this.OnPropertyChanging("ResultValue", value);
				string oldValue = this.ResultValue;
				SetColumnValue(Columns.ResultValue, value);
				this.OnResultValueChanged(oldValue, value);
				this.OnPropertyChanged("ResultValue", oldValue, value);
			}
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a JumpstationMacro ActiveRecord object related to this JumpstationMacroValue
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.JumpstationMacro JumpstationMacro
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.JumpstationMacro.FetchByID(this.JumpstationMacroId); }
			set { SetColumnValue("JumpstationMacroId", value.Id); }
		}
		
		/// <summary>
		/// Returns a RowStatus ActiveRecord object related to this JumpstationMacroValue
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.RowStatus RowStatus
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.RowStatus.FetchByID(this.RowStatusId); }
			set { SetColumnValue("RowStatusId", value.Id); }
		}
		
		#endregion
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="JumpstationMacroValue"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("JumpstationMacroValue#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="JumpstationMacroValue"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="JumpstationMacroValue"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="JumpstationMacroValue"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			JumpstationMacroValue instance1 = this;
			JumpstationMacroValue instance2 = obj as JumpstationMacroValue;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.JumpstationMacroId == instance2.JumpstationMacroId)
			
				&& (instance1.MatchName == instance2.MatchName)
			
				&& (instance1.ResultValue == instance2.ResultValue)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="JumpstationMacroValue"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="JumpstationMacroValue"/> to compare.</param>
		/// <param name="instance2">The second <see cref="JumpstationMacroValue"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(JumpstationMacroValue instance1, JumpstationMacroValue instance2)
		{
			if (instance1 == null)
			{
				return (instance2 == null);
			}
			return instance1.Equals(instance2);
		}
*/
		#endregion
		#region ObjectDataSource support
		
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Insert method
	//NOTE: Code Generation: Generation disabled: ObjectDataSource-compatible Update method
		#endregion
        
		#region Typed Columns
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn IdColumn
		{
			get { return Schema.Columns[0]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[1]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[2]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[3]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn RowStatusIdColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn JumpstationMacroIdColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn MatchNameColumn
		{
			get { return Schema.Columns[7]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ResultValueColumn
		{
			get { return Schema.Columns[8]; }
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
			 public static string JumpstationMacroId = @"JumpstationMacroId";
			 public static string MatchName = @"MatchName";
			 public static string ResultValue = @"ResultValue";
			
		}
		#endregion
		#region Update PK Collections
		#endregion
		#region Deep Save
	//NOTE: Code Generation: Generation disabled: DeepSave method
		#endregion
	}
}
