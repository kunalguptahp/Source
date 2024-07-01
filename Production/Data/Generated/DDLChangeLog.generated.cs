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
	/// Strongly-typed collection for the DDLChangeLog class.
	/// </summary>
    [Serializable]
	public partial class DDLChangeLogCollection : ActiveList<DDLChangeLog, DDLChangeLogCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public DDLChangeLogCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(DDLChangeLog);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the DDLChangeLog table.
	/// </summary>
	[Serializable]
	public partial class DDLChangeLog : ActiveRecord<DDLChangeLog>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected DDLChangeLog(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public DDLChangeLog() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public DDLChangeLog(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public DDLChangeLog(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public DDLChangeLog(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="DDLChangeLog"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private DDLChangeLog(DDLChangeLog original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(DDLChangeLog original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.Inserted = original.Inserted;
			
			this.CurrentUser = original.CurrentUser;
			
			this.LoginName = original.LoginName;
			
			this.Username = original.Username;
			
			this.EventType = original.EventType;
			
			this.ObjectName = original.ObjectName;
			
			this.ObjectType = original.ObjectType;
			
			this.Tsql = original.Tsql;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="DDLChangeLog"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static DDLChangeLog Copy(DDLChangeLog original)
		{
			return new DDLChangeLog(original);
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
				TableSchema.Table schema = new TableSchema.Table("DDLChangeLog", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "ID";
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
				
				TableSchema.TableColumn colvarInserted = new TableSchema.TableColumn(schema);
				colvarInserted.ColumnName = "Inserted";
				colvarInserted.DataType = DbType.DateTime;
				colvarInserted.MaxLength = 0;
				colvarInserted.AutoIncrement = false;
				colvarInserted.IsNullable = false;
				colvarInserted.IsPrimaryKey = false;
				colvarInserted.IsForeignKey = false;
				colvarInserted.IsReadOnly = false;
				colvarInserted.DefaultSetting = @"";
				colvarInserted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInserted);
				
				TableSchema.TableColumn colvarCurrentUser = new TableSchema.TableColumn(schema);
				colvarCurrentUser.ColumnName = "CurrentUser";
				colvarCurrentUser.DataType = DbType.String;
				colvarCurrentUser.MaxLength = 50;
				colvarCurrentUser.AutoIncrement = false;
				colvarCurrentUser.IsNullable = false;
				colvarCurrentUser.IsPrimaryKey = false;
				colvarCurrentUser.IsForeignKey = false;
				colvarCurrentUser.IsReadOnly = false;
				colvarCurrentUser.DefaultSetting = @"";
				colvarCurrentUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentUser);
				
				TableSchema.TableColumn colvarLoginName = new TableSchema.TableColumn(schema);
				colvarLoginName.ColumnName = "LoginName";
				colvarLoginName.DataType = DbType.String;
				colvarLoginName.MaxLength = 50;
				colvarLoginName.AutoIncrement = false;
				colvarLoginName.IsNullable = false;
				colvarLoginName.IsPrimaryKey = false;
				colvarLoginName.IsForeignKey = false;
				colvarLoginName.IsReadOnly = false;
				colvarLoginName.DefaultSetting = @"";
				colvarLoginName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoginName);
				
				TableSchema.TableColumn colvarUsername = new TableSchema.TableColumn(schema);
				colvarUsername.ColumnName = "Username";
				colvarUsername.DataType = DbType.String;
				colvarUsername.MaxLength = 50;
				colvarUsername.AutoIncrement = false;
				colvarUsername.IsNullable = false;
				colvarUsername.IsPrimaryKey = false;
				colvarUsername.IsForeignKey = false;
				colvarUsername.IsReadOnly = false;
				colvarUsername.DefaultSetting = @"";
				colvarUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsername);
				
				TableSchema.TableColumn colvarEventType = new TableSchema.TableColumn(schema);
				colvarEventType.ColumnName = "EventType";
				colvarEventType.DataType = DbType.String;
				colvarEventType.MaxLength = 100;
				colvarEventType.AutoIncrement = false;
				colvarEventType.IsNullable = true;
				colvarEventType.IsPrimaryKey = false;
				colvarEventType.IsForeignKey = false;
				colvarEventType.IsReadOnly = false;
				colvarEventType.DefaultSetting = @"";
				colvarEventType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventType);
				
				TableSchema.TableColumn colvarObjectName = new TableSchema.TableColumn(schema);
				colvarObjectName.ColumnName = "objectName";
				colvarObjectName.DataType = DbType.String;
				colvarObjectName.MaxLength = 100;
				colvarObjectName.AutoIncrement = false;
				colvarObjectName.IsNullable = true;
				colvarObjectName.IsPrimaryKey = false;
				colvarObjectName.IsForeignKey = false;
				colvarObjectName.IsReadOnly = false;
				colvarObjectName.DefaultSetting = @"";
				colvarObjectName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarObjectName);
				
				TableSchema.TableColumn colvarObjectType = new TableSchema.TableColumn(schema);
				colvarObjectType.ColumnName = "objectType";
				colvarObjectType.DataType = DbType.String;
				colvarObjectType.MaxLength = 100;
				colvarObjectType.AutoIncrement = false;
				colvarObjectType.IsNullable = true;
				colvarObjectType.IsPrimaryKey = false;
				colvarObjectType.IsForeignKey = false;
				colvarObjectType.IsReadOnly = false;
				colvarObjectType.DefaultSetting = @"";
				colvarObjectType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarObjectType);
				
				TableSchema.TableColumn colvarTsql = new TableSchema.TableColumn(schema);
				colvarTsql.ColumnName = "tsql";
				colvarTsql.DataType = DbType.String;
				colvarTsql.MaxLength = -1;
				colvarTsql.AutoIncrement = false;
				colvarTsql.IsNullable = true;
				colvarTsql.IsPrimaryKey = false;
				colvarTsql.IsForeignKey = false;
				colvarTsql.IsReadOnly = false;
				colvarTsql.DefaultSetting = @"";
				colvarTsql.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTsql);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("DDLChangeLog",schema);
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
		partial void OnInsertedChanging(DateTime newValue);
		partial void OnInsertedChanged(DateTime oldValue, DateTime newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Inserted")]
		[Bindable(true)]
		public DateTime Inserted 
		{
			get { return GetColumnValue<DateTime>(Columns.Inserted); }
			set
			{
				this.OnInsertedChanging(value);
				this.OnPropertyChanging("Inserted", value);
				DateTime oldValue = this.Inserted;
				SetColumnValue(Columns.Inserted, value);
				this.OnInsertedChanged(oldValue, value);
				this.OnPropertyChanged("Inserted", oldValue, value);
			}
		}
		partial void OnCurrentUserChanging(string newValue);
		partial void OnCurrentUserChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CurrentUser")]
		[Bindable(true)]
		public string CurrentUser 
		{
			get { return GetColumnValue<string>(Columns.CurrentUser); }
			set
			{
				this.OnCurrentUserChanging(value);
				this.OnPropertyChanging("CurrentUser", value);
				string oldValue = this.CurrentUser;
				SetColumnValue(Columns.CurrentUser, value);
				this.OnCurrentUserChanged(oldValue, value);
				this.OnPropertyChanged("CurrentUser", oldValue, value);
			}
		}
		partial void OnLoginNameChanging(string newValue);
		partial void OnLoginNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("LoginName")]
		[Bindable(true)]
		public string LoginName 
		{
			get { return GetColumnValue<string>(Columns.LoginName); }
			set
			{
				this.OnLoginNameChanging(value);
				this.OnPropertyChanging("LoginName", value);
				string oldValue = this.LoginName;
				SetColumnValue(Columns.LoginName, value);
				this.OnLoginNameChanged(oldValue, value);
				this.OnPropertyChanged("LoginName", oldValue, value);
			}
		}
		partial void OnUsernameChanging(string newValue);
		partial void OnUsernameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Username")]
		[Bindable(true)]
		public string Username 
		{
			get { return GetColumnValue<string>(Columns.Username); }
			set
			{
				this.OnUsernameChanging(value);
				this.OnPropertyChanging("Username", value);
				string oldValue = this.Username;
				SetColumnValue(Columns.Username, value);
				this.OnUsernameChanged(oldValue, value);
				this.OnPropertyChanged("Username", oldValue, value);
			}
		}
		partial void OnEventTypeChanging(string newValue);
		partial void OnEventTypeChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("EventType")]
		[Bindable(true)]
		public string EventType 
		{
			get { return GetColumnValue<string>(Columns.EventType); }
			set
			{
				this.OnEventTypeChanging(value);
				this.OnPropertyChanging("EventType", value);
				string oldValue = this.EventType;
				SetColumnValue(Columns.EventType, value);
				this.OnEventTypeChanged(oldValue, value);
				this.OnPropertyChanged("EventType", oldValue, value);
			}
		}
		partial void OnObjectNameChanging(string newValue);
		partial void OnObjectNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ObjectName")]
		[Bindable(true)]
		public string ObjectName 
		{
			get { return GetColumnValue<string>(Columns.ObjectName); }
			set
			{
				this.OnObjectNameChanging(value);
				this.OnPropertyChanging("ObjectName", value);
				string oldValue = this.ObjectName;
				SetColumnValue(Columns.ObjectName, value);
				this.OnObjectNameChanged(oldValue, value);
				this.OnPropertyChanged("ObjectName", oldValue, value);
			}
		}
		partial void OnObjectTypeChanging(string newValue);
		partial void OnObjectTypeChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ObjectType")]
		[Bindable(true)]
		public string ObjectType 
		{
			get { return GetColumnValue<string>(Columns.ObjectType); }
			set
			{
				this.OnObjectTypeChanging(value);
				this.OnPropertyChanging("ObjectType", value);
				string oldValue = this.ObjectType;
				SetColumnValue(Columns.ObjectType, value);
				this.OnObjectTypeChanged(oldValue, value);
				this.OnPropertyChanged("ObjectType", oldValue, value);
			}
		}
		partial void OnTsqlChanging(string newValue);
		partial void OnTsqlChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Tsql")]
		[Bindable(true)]
		public string Tsql 
		{
			get { return GetColumnValue<string>(Columns.Tsql); }
			set
			{
				this.OnTsqlChanging(value);
				this.OnPropertyChanging("Tsql", value);
				string oldValue = this.Tsql;
				SetColumnValue(Columns.Tsql, value);
				this.OnTsqlChanged(oldValue, value);
				this.OnPropertyChanged("Tsql", oldValue, value);
			}
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="DDLChangeLog"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("DDLChangeLog#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="DDLChangeLog"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="DDLChangeLog"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="DDLChangeLog"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			DDLChangeLog instance1 = this;
			DDLChangeLog instance2 = obj as DDLChangeLog;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Inserted == instance2.Inserted)
			
				&& (instance1.CurrentUser == instance2.CurrentUser)
			
				&& (instance1.LoginName == instance2.LoginName)
			
				&& (instance1.Username == instance2.Username)
			
				&& (instance1.EventType == instance2.EventType)
			
				&& (instance1.ObjectName == instance2.ObjectName)
			
				&& (instance1.ObjectType == instance2.ObjectType)
			
				&& (instance1.Tsql == instance2.Tsql)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="DDLChangeLog"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="DDLChangeLog"/> to compare.</param>
		/// <param name="instance2">The second <see cref="DDLChangeLog"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(DDLChangeLog instance1, DDLChangeLog instance2)
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
		public static TableSchema.TableColumn InsertedColumn
		{
			get { return Schema.Columns[1]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CurrentUserColumn
		{
			get { return Schema.Columns[2]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn LoginNameColumn
		{
			get { return Schema.Columns[3]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn UsernameColumn
		{
			get { return Schema.Columns[4]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn EventTypeColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ObjectNameColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ObjectTypeColumn
		{
			get { return Schema.Columns[7]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn TsqlColumn
		{
			get { return Schema.Columns[8]; }
		}
        
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string Inserted = @"Inserted";
			 public static string CurrentUser = @"CurrentUser";
			 public static string LoginName = @"LoginName";
			 public static string Username = @"Username";
			 public static string EventType = @"EventType";
			 public static string ObjectName = @"objectName";
			 public static string ObjectType = @"objectType";
			 public static string Tsql = @"tsql";
			
		}
		#endregion
		#region Update PK Collections
		#endregion
		#region Deep Save
	//NOTE: Code Generation: Generation disabled: DeepSave method
		#endregion
	}
}
