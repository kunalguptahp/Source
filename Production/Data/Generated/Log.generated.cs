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
	/// Strongly-typed collection for the Log class.
	/// </summary>
    [Serializable]
	public partial class LogCollection : ActiveList<Log, LogCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public LogCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(Log);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the Log table.
	/// </summary>
	[Serializable]
	public partial class Log : ActiveRecord<Log>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected Log(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public Log() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Log(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Log(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Log(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="Log"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private Log(Log original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(Log original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.CreatedAt = original.CreatedAt;
			
			this.DateX = original.DateX;
			
			this.UtcDate = original.UtcDate;
			
			this.Severity = original.Severity;
			
			this.UserIdentity = original.UserIdentity;
			
			this.UserName = original.UserName;
			
			this.UserWebIdentity = original.UserWebIdentity;
			
			this.Logger = original.Logger;
			
			this.Location = original.Location;
			
			this.WebSessionId = original.WebSessionId;
			
			this.ProcessThread = original.ProcessThread;
			
			this.MachineName = original.MachineName;
			
			this.ProcessorCount = original.ProcessorCount;
			
			this.OSVersion = original.OSVersion;
			
			this.ClrVersion = original.ClrVersion;
			
			this.AllocatedMemory = original.AllocatedMemory;
			
			this.WorkingMemory = original.WorkingMemory;
			
			this.ProcessUser = original.ProcessUser;
			
			this.ProcessUserInteractive = original.ProcessUserInteractive;
			
			this.ProcessUptime = original.ProcessUptime;
			
			this.Message = original.Message;
			
			this.Exception = original.Exception;
			
			this.StackTrace = original.StackTrace;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="Log"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Log Copy(Log original)
		{
			return new Log(original);
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
				TableSchema.Table schema = new TableSchema.Table("Log", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Int64;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarCreatedAt = new TableSchema.TableColumn(schema);
				colvarCreatedAt.ColumnName = "CreatedAt";
				colvarCreatedAt.DataType = DbType.DateTime;
				colvarCreatedAt.MaxLength = 0;
				colvarCreatedAt.AutoIncrement = false;
				colvarCreatedAt.IsNullable = false;
				colvarCreatedAt.IsPrimaryKey = false;
				colvarCreatedAt.IsForeignKey = false;
				colvarCreatedAt.IsReadOnly = false;
				colvarCreatedAt.DefaultSetting = @"";
				colvarCreatedAt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedAt);
				
				TableSchema.TableColumn colvarDateX = new TableSchema.TableColumn(schema);
				colvarDateX.ColumnName = "Date";
				colvarDateX.DataType = DbType.DateTime;
				colvarDateX.MaxLength = 0;
				colvarDateX.AutoIncrement = false;
				colvarDateX.IsNullable = true;
				colvarDateX.IsPrimaryKey = false;
				colvarDateX.IsForeignKey = false;
				colvarDateX.IsReadOnly = false;
				colvarDateX.DefaultSetting = @"";
				colvarDateX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDateX);
				
				TableSchema.TableColumn colvarUtcDate = new TableSchema.TableColumn(schema);
				colvarUtcDate.ColumnName = "UtcDate";
				colvarUtcDate.DataType = DbType.DateTime;
				colvarUtcDate.MaxLength = 0;
				colvarUtcDate.AutoIncrement = false;
				colvarUtcDate.IsNullable = true;
				colvarUtcDate.IsPrimaryKey = false;
				colvarUtcDate.IsForeignKey = false;
				colvarUtcDate.IsReadOnly = false;
				colvarUtcDate.DefaultSetting = @"";
				colvarUtcDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUtcDate);
				
				TableSchema.TableColumn colvarSeverity = new TableSchema.TableColumn(schema);
				colvarSeverity.ColumnName = "Severity";
				colvarSeverity.DataType = DbType.AnsiString;
				colvarSeverity.MaxLength = 8;
				colvarSeverity.AutoIncrement = false;
				colvarSeverity.IsNullable = true;
				colvarSeverity.IsPrimaryKey = false;
				colvarSeverity.IsForeignKey = false;
				colvarSeverity.IsReadOnly = false;
				colvarSeverity.DefaultSetting = @"";
				colvarSeverity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeverity);
				
				TableSchema.TableColumn colvarUserIdentity = new TableSchema.TableColumn(schema);
				colvarUserIdentity.ColumnName = "UserIdentity";
				colvarUserIdentity.DataType = DbType.String;
				colvarUserIdentity.MaxLength = 128;
				colvarUserIdentity.AutoIncrement = false;
				colvarUserIdentity.IsNullable = true;
				colvarUserIdentity.IsPrimaryKey = false;
				colvarUserIdentity.IsForeignKey = false;
				colvarUserIdentity.IsReadOnly = false;
				colvarUserIdentity.DefaultSetting = @"";
				colvarUserIdentity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserIdentity);
				
				TableSchema.TableColumn colvarUserName = new TableSchema.TableColumn(schema);
				colvarUserName.ColumnName = "UserName";
				colvarUserName.DataType = DbType.String;
				colvarUserName.MaxLength = 128;
				colvarUserName.AutoIncrement = false;
				colvarUserName.IsNullable = true;
				colvarUserName.IsPrimaryKey = false;
				colvarUserName.IsForeignKey = false;
				colvarUserName.IsReadOnly = false;
				colvarUserName.DefaultSetting = @"";
				colvarUserName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserName);
				
				TableSchema.TableColumn colvarUserWebIdentity = new TableSchema.TableColumn(schema);
				colvarUserWebIdentity.ColumnName = "UserWebIdentity";
				colvarUserWebIdentity.DataType = DbType.String;
				colvarUserWebIdentity.MaxLength = 128;
				colvarUserWebIdentity.AutoIncrement = false;
				colvarUserWebIdentity.IsNullable = true;
				colvarUserWebIdentity.IsPrimaryKey = false;
				colvarUserWebIdentity.IsForeignKey = false;
				colvarUserWebIdentity.IsReadOnly = false;
				colvarUserWebIdentity.DefaultSetting = @"";
				colvarUserWebIdentity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserWebIdentity);
				
				TableSchema.TableColumn colvarLogger = new TableSchema.TableColumn(schema);
				colvarLogger.ColumnName = "Logger";
				colvarLogger.DataType = DbType.String;
				colvarLogger.MaxLength = 512;
				colvarLogger.AutoIncrement = false;
				colvarLogger.IsNullable = true;
				colvarLogger.IsPrimaryKey = false;
				colvarLogger.IsForeignKey = false;
				colvarLogger.IsReadOnly = false;
				colvarLogger.DefaultSetting = @"";
				colvarLogger.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLogger);
				
				TableSchema.TableColumn colvarLocation = new TableSchema.TableColumn(schema);
				colvarLocation.ColumnName = "Location";
				colvarLocation.DataType = DbType.String;
				colvarLocation.MaxLength = 512;
				colvarLocation.AutoIncrement = false;
				colvarLocation.IsNullable = true;
				colvarLocation.IsPrimaryKey = false;
				colvarLocation.IsForeignKey = false;
				colvarLocation.IsReadOnly = false;
				colvarLocation.DefaultSetting = @"";
				colvarLocation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocation);
				
				TableSchema.TableColumn colvarWebSessionId = new TableSchema.TableColumn(schema);
				colvarWebSessionId.ColumnName = "WebSessionId";
				colvarWebSessionId.DataType = DbType.AnsiString;
				colvarWebSessionId.MaxLength = 64;
				colvarWebSessionId.AutoIncrement = false;
				colvarWebSessionId.IsNullable = true;
				colvarWebSessionId.IsPrimaryKey = false;
				colvarWebSessionId.IsForeignKey = false;
				colvarWebSessionId.IsReadOnly = false;
				colvarWebSessionId.DefaultSetting = @"";
				colvarWebSessionId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWebSessionId);
				
				TableSchema.TableColumn colvarProcessThread = new TableSchema.TableColumn(schema);
				colvarProcessThread.ColumnName = "ProcessThread";
				colvarProcessThread.DataType = DbType.Int32;
				colvarProcessThread.MaxLength = 0;
				colvarProcessThread.AutoIncrement = false;
				colvarProcessThread.IsNullable = true;
				colvarProcessThread.IsPrimaryKey = false;
				colvarProcessThread.IsForeignKey = false;
				colvarProcessThread.IsReadOnly = false;
				colvarProcessThread.DefaultSetting = @"";
				colvarProcessThread.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessThread);
				
				TableSchema.TableColumn colvarMachineName = new TableSchema.TableColumn(schema);
				colvarMachineName.ColumnName = "MachineName";
				colvarMachineName.DataType = DbType.AnsiString;
				colvarMachineName.MaxLength = 64;
				colvarMachineName.AutoIncrement = false;
				colvarMachineName.IsNullable = true;
				colvarMachineName.IsPrimaryKey = false;
				colvarMachineName.IsForeignKey = false;
				colvarMachineName.IsReadOnly = false;
				colvarMachineName.DefaultSetting = @"";
				colvarMachineName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMachineName);
				
				TableSchema.TableColumn colvarProcessorCount = new TableSchema.TableColumn(schema);
				colvarProcessorCount.ColumnName = "ProcessorCount";
				colvarProcessorCount.DataType = DbType.Int16;
				colvarProcessorCount.MaxLength = 0;
				colvarProcessorCount.AutoIncrement = false;
				colvarProcessorCount.IsNullable = true;
				colvarProcessorCount.IsPrimaryKey = false;
				colvarProcessorCount.IsForeignKey = false;
				colvarProcessorCount.IsReadOnly = false;
				colvarProcessorCount.DefaultSetting = @"";
				colvarProcessorCount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessorCount);
				
				TableSchema.TableColumn colvarOSVersion = new TableSchema.TableColumn(schema);
				colvarOSVersion.ColumnName = "OSVersion";
				colvarOSVersion.DataType = DbType.AnsiString;
				colvarOSVersion.MaxLength = 64;
				colvarOSVersion.AutoIncrement = false;
				colvarOSVersion.IsNullable = true;
				colvarOSVersion.IsPrimaryKey = false;
				colvarOSVersion.IsForeignKey = false;
				colvarOSVersion.IsReadOnly = false;
				colvarOSVersion.DefaultSetting = @"";
				colvarOSVersion.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOSVersion);
				
				TableSchema.TableColumn colvarClrVersion = new TableSchema.TableColumn(schema);
				colvarClrVersion.ColumnName = "ClrVersion";
				colvarClrVersion.DataType = DbType.AnsiString;
				colvarClrVersion.MaxLength = 32;
				colvarClrVersion.AutoIncrement = false;
				colvarClrVersion.IsNullable = true;
				colvarClrVersion.IsPrimaryKey = false;
				colvarClrVersion.IsForeignKey = false;
				colvarClrVersion.IsReadOnly = false;
				colvarClrVersion.DefaultSetting = @"";
				colvarClrVersion.ForeignKeyTableName = "";
				schema.Columns.Add(colvarClrVersion);
				
				TableSchema.TableColumn colvarAllocatedMemory = new TableSchema.TableColumn(schema);
				colvarAllocatedMemory.ColumnName = "AllocatedMemory";
				colvarAllocatedMemory.DataType = DbType.Int32;
				colvarAllocatedMemory.MaxLength = 0;
				colvarAllocatedMemory.AutoIncrement = false;
				colvarAllocatedMemory.IsNullable = true;
				colvarAllocatedMemory.IsPrimaryKey = false;
				colvarAllocatedMemory.IsForeignKey = false;
				colvarAllocatedMemory.IsReadOnly = false;
				colvarAllocatedMemory.DefaultSetting = @"";
				colvarAllocatedMemory.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAllocatedMemory);
				
				TableSchema.TableColumn colvarWorkingMemory = new TableSchema.TableColumn(schema);
				colvarWorkingMemory.ColumnName = "WorkingMemory";
				colvarWorkingMemory.DataType = DbType.Int32;
				colvarWorkingMemory.MaxLength = 0;
				colvarWorkingMemory.AutoIncrement = false;
				colvarWorkingMemory.IsNullable = true;
				colvarWorkingMemory.IsPrimaryKey = false;
				colvarWorkingMemory.IsForeignKey = false;
				colvarWorkingMemory.IsReadOnly = false;
				colvarWorkingMemory.DefaultSetting = @"";
				colvarWorkingMemory.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWorkingMemory);
				
				TableSchema.TableColumn colvarProcessUser = new TableSchema.TableColumn(schema);
				colvarProcessUser.ColumnName = "ProcessUser";
				colvarProcessUser.DataType = DbType.String;
				colvarProcessUser.MaxLength = 128;
				colvarProcessUser.AutoIncrement = false;
				colvarProcessUser.IsNullable = true;
				colvarProcessUser.IsPrimaryKey = false;
				colvarProcessUser.IsForeignKey = false;
				colvarProcessUser.IsReadOnly = false;
				colvarProcessUser.DefaultSetting = @"";
				colvarProcessUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessUser);
				
				TableSchema.TableColumn colvarProcessUserInteractive = new TableSchema.TableColumn(schema);
				colvarProcessUserInteractive.ColumnName = "ProcessUserInteractive";
				colvarProcessUserInteractive.DataType = DbType.Boolean;
				colvarProcessUserInteractive.MaxLength = 0;
				colvarProcessUserInteractive.AutoIncrement = false;
				colvarProcessUserInteractive.IsNullable = true;
				colvarProcessUserInteractive.IsPrimaryKey = false;
				colvarProcessUserInteractive.IsForeignKey = false;
				colvarProcessUserInteractive.IsReadOnly = false;
				colvarProcessUserInteractive.DefaultSetting = @"";
				colvarProcessUserInteractive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessUserInteractive);
				
				TableSchema.TableColumn colvarProcessUptime = new TableSchema.TableColumn(schema);
				colvarProcessUptime.ColumnName = "ProcessUptime";
				colvarProcessUptime.DataType = DbType.Int64;
				colvarProcessUptime.MaxLength = 0;
				colvarProcessUptime.AutoIncrement = false;
				colvarProcessUptime.IsNullable = true;
				colvarProcessUptime.IsPrimaryKey = false;
				colvarProcessUptime.IsForeignKey = false;
				colvarProcessUptime.IsReadOnly = false;
				colvarProcessUptime.DefaultSetting = @"";
				colvarProcessUptime.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessUptime);
				
				TableSchema.TableColumn colvarMessage = new TableSchema.TableColumn(schema);
				colvarMessage.ColumnName = "Message";
				colvarMessage.DataType = DbType.String;
				colvarMessage.MaxLength = -1;
				colvarMessage.AutoIncrement = false;
				colvarMessage.IsNullable = true;
				colvarMessage.IsPrimaryKey = false;
				colvarMessage.IsForeignKey = false;
				colvarMessage.IsReadOnly = false;
				colvarMessage.DefaultSetting = @"";
				colvarMessage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessage);
				
				TableSchema.TableColumn colvarException = new TableSchema.TableColumn(schema);
				colvarException.ColumnName = "Exception";
				colvarException.DataType = DbType.String;
				colvarException.MaxLength = -1;
				colvarException.AutoIncrement = false;
				colvarException.IsNullable = true;
				colvarException.IsPrimaryKey = false;
				colvarException.IsForeignKey = false;
				colvarException.IsReadOnly = false;
				colvarException.DefaultSetting = @"";
				colvarException.ForeignKeyTableName = "";
				schema.Columns.Add(colvarException);
				
				TableSchema.TableColumn colvarStackTrace = new TableSchema.TableColumn(schema);
				colvarStackTrace.ColumnName = "StackTrace";
				colvarStackTrace.DataType = DbType.String;
				colvarStackTrace.MaxLength = -1;
				colvarStackTrace.AutoIncrement = false;
				colvarStackTrace.IsNullable = true;
				colvarStackTrace.IsPrimaryKey = false;
				colvarStackTrace.IsForeignKey = false;
				colvarStackTrace.IsReadOnly = false;
				colvarStackTrace.DefaultSetting = @"";
				colvarStackTrace.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStackTrace);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("Log",schema);
			}
		}
		#endregion
		
		#region Props
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		partial void OnIdChanging(long newValue);
		partial void OnIdChanged(long oldValue, long newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Id")]
		[Bindable(true)]
		public long Id 
		{
			get { return GetColumnValue<long>(Columns.Id); }
			set
			{
				this.OnIdChanging(value);
				this.OnPropertyChanging("Id", value);
				long oldValue = this.Id;
				SetColumnValue(Columns.Id, value);
				this.OnIdChanged(oldValue, value);
				this.OnPropertyChanged("Id", oldValue, value);
			}
		}
		partial void OnCreatedAtChanging(DateTime newValue);
		partial void OnCreatedAtChanged(DateTime oldValue, DateTime newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CreatedAt")]
		[Bindable(true)]
		public DateTime CreatedAt 
		{
			get { return GetColumnValue<DateTime>(Columns.CreatedAt); }
			set
			{
				this.OnCreatedAtChanging(value);
				this.OnPropertyChanging("CreatedAt", value);
				DateTime oldValue = this.CreatedAt;
				SetColumnValue(Columns.CreatedAt, value);
				this.OnCreatedAtChanged(oldValue, value);
				this.OnPropertyChanged("CreatedAt", oldValue, value);
			}
		}
		partial void OnDateXChanging(DateTime? newValue);
		partial void OnDateXChanged(DateTime? oldValue, DateTime? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("DateX")]
		[Bindable(true)]
		public DateTime? DateX 
		{
			get { return GetColumnValue<DateTime?>(Columns.DateX); }
			set
			{
				this.OnDateXChanging(value);
				this.OnPropertyChanging("DateX", value);
				DateTime? oldValue = this.DateX;
				SetColumnValue(Columns.DateX, value);
				this.OnDateXChanged(oldValue, value);
				this.OnPropertyChanged("DateX", oldValue, value);
			}
		}
		partial void OnUtcDateChanging(DateTime? newValue);
		partial void OnUtcDateChanged(DateTime? oldValue, DateTime? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("UtcDate")]
		[Bindable(true)]
		public DateTime? UtcDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.UtcDate); }
			set
			{
				this.OnUtcDateChanging(value);
				this.OnPropertyChanging("UtcDate", value);
				DateTime? oldValue = this.UtcDate;
				SetColumnValue(Columns.UtcDate, value);
				this.OnUtcDateChanged(oldValue, value);
				this.OnPropertyChanged("UtcDate", oldValue, value);
			}
		}
		partial void OnSeverityChanging(string newValue);
		partial void OnSeverityChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Severity")]
		[Bindable(true)]
		public string Severity 
		{
			get { return GetColumnValue<string>(Columns.Severity); }
			set
			{
				this.OnSeverityChanging(value);
				this.OnPropertyChanging("Severity", value);
				string oldValue = this.Severity;
				SetColumnValue(Columns.Severity, value);
				this.OnSeverityChanged(oldValue, value);
				this.OnPropertyChanged("Severity", oldValue, value);
			}
		}
		partial void OnUserIdentityChanging(string newValue);
		partial void OnUserIdentityChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("UserIdentity")]
		[Bindable(true)]
		public string UserIdentity 
		{
			get { return GetColumnValue<string>(Columns.UserIdentity); }
			set
			{
				this.OnUserIdentityChanging(value);
				this.OnPropertyChanging("UserIdentity", value);
				string oldValue = this.UserIdentity;
				SetColumnValue(Columns.UserIdentity, value);
				this.OnUserIdentityChanged(oldValue, value);
				this.OnPropertyChanged("UserIdentity", oldValue, value);
			}
		}
		partial void OnUserNameChanging(string newValue);
		partial void OnUserNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("UserName")]
		[Bindable(true)]
		public string UserName 
		{
			get { return GetColumnValue<string>(Columns.UserName); }
			set
			{
				this.OnUserNameChanging(value);
				this.OnPropertyChanging("UserName", value);
				string oldValue = this.UserName;
				SetColumnValue(Columns.UserName, value);
				this.OnUserNameChanged(oldValue, value);
				this.OnPropertyChanged("UserName", oldValue, value);
			}
		}
		partial void OnUserWebIdentityChanging(string newValue);
		partial void OnUserWebIdentityChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("UserWebIdentity")]
		[Bindable(true)]
		public string UserWebIdentity 
		{
			get { return GetColumnValue<string>(Columns.UserWebIdentity); }
			set
			{
				this.OnUserWebIdentityChanging(value);
				this.OnPropertyChanging("UserWebIdentity", value);
				string oldValue = this.UserWebIdentity;
				SetColumnValue(Columns.UserWebIdentity, value);
				this.OnUserWebIdentityChanged(oldValue, value);
				this.OnPropertyChanged("UserWebIdentity", oldValue, value);
			}
		}
		partial void OnLoggerChanging(string newValue);
		partial void OnLoggerChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Logger")]
		[Bindable(true)]
		public string Logger 
		{
			get { return GetColumnValue<string>(Columns.Logger); }
			set
			{
				this.OnLoggerChanging(value);
				this.OnPropertyChanging("Logger", value);
				string oldValue = this.Logger;
				SetColumnValue(Columns.Logger, value);
				this.OnLoggerChanged(oldValue, value);
				this.OnPropertyChanged("Logger", oldValue, value);
			}
		}
		partial void OnLocationChanging(string newValue);
		partial void OnLocationChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Location")]
		[Bindable(true)]
		public string Location 
		{
			get { return GetColumnValue<string>(Columns.Location); }
			set
			{
				this.OnLocationChanging(value);
				this.OnPropertyChanging("Location", value);
				string oldValue = this.Location;
				SetColumnValue(Columns.Location, value);
				this.OnLocationChanged(oldValue, value);
				this.OnPropertyChanged("Location", oldValue, value);
			}
		}
		partial void OnWebSessionIdChanging(string newValue);
		partial void OnWebSessionIdChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WebSessionId")]
		[Bindable(true)]
		public string WebSessionId 
		{
			get { return GetColumnValue<string>(Columns.WebSessionId); }
			set
			{
				this.OnWebSessionIdChanging(value);
				this.OnPropertyChanging("WebSessionId", value);
				string oldValue = this.WebSessionId;
				SetColumnValue(Columns.WebSessionId, value);
				this.OnWebSessionIdChanged(oldValue, value);
				this.OnPropertyChanged("WebSessionId", oldValue, value);
			}
		}
		partial void OnProcessThreadChanging(int? newValue);
		partial void OnProcessThreadChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProcessThread")]
		[Bindable(true)]
		public int? ProcessThread 
		{
			get { return GetColumnValue<int?>(Columns.ProcessThread); }
			set
			{
				this.OnProcessThreadChanging(value);
				this.OnPropertyChanging("ProcessThread", value);
				int? oldValue = this.ProcessThread;
				SetColumnValue(Columns.ProcessThread, value);
				this.OnProcessThreadChanged(oldValue, value);
				this.OnPropertyChanged("ProcessThread", oldValue, value);
			}
		}
		partial void OnMachineNameChanging(string newValue);
		partial void OnMachineNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("MachineName")]
		[Bindable(true)]
		public string MachineName 
		{
			get { return GetColumnValue<string>(Columns.MachineName); }
			set
			{
				this.OnMachineNameChanging(value);
				this.OnPropertyChanging("MachineName", value);
				string oldValue = this.MachineName;
				SetColumnValue(Columns.MachineName, value);
				this.OnMachineNameChanged(oldValue, value);
				this.OnPropertyChanged("MachineName", oldValue, value);
			}
		}
		partial void OnProcessorCountChanging(short? newValue);
		partial void OnProcessorCountChanged(short? oldValue, short? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProcessorCount")]
		[Bindable(true)]
		public short? ProcessorCount 
		{
			get { return GetColumnValue<short?>(Columns.ProcessorCount); }
			set
			{
				this.OnProcessorCountChanging(value);
				this.OnPropertyChanging("ProcessorCount", value);
				short? oldValue = this.ProcessorCount;
				SetColumnValue(Columns.ProcessorCount, value);
				this.OnProcessorCountChanged(oldValue, value);
				this.OnPropertyChanged("ProcessorCount", oldValue, value);
			}
		}
		partial void OnOSVersionChanging(string newValue);
		partial void OnOSVersionChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("OSVersion")]
		[Bindable(true)]
		public string OSVersion 
		{
			get { return GetColumnValue<string>(Columns.OSVersion); }
			set
			{
				this.OnOSVersionChanging(value);
				this.OnPropertyChanging("OSVersion", value);
				string oldValue = this.OSVersion;
				SetColumnValue(Columns.OSVersion, value);
				this.OnOSVersionChanged(oldValue, value);
				this.OnPropertyChanged("OSVersion", oldValue, value);
			}
		}
		partial void OnClrVersionChanging(string newValue);
		partial void OnClrVersionChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ClrVersion")]
		[Bindable(true)]
		public string ClrVersion 
		{
			get { return GetColumnValue<string>(Columns.ClrVersion); }
			set
			{
				this.OnClrVersionChanging(value);
				this.OnPropertyChanging("ClrVersion", value);
				string oldValue = this.ClrVersion;
				SetColumnValue(Columns.ClrVersion, value);
				this.OnClrVersionChanged(oldValue, value);
				this.OnPropertyChanged("ClrVersion", oldValue, value);
			}
		}
		partial void OnAllocatedMemoryChanging(int? newValue);
		partial void OnAllocatedMemoryChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("AllocatedMemory")]
		[Bindable(true)]
		public int? AllocatedMemory 
		{
			get { return GetColumnValue<int?>(Columns.AllocatedMemory); }
			set
			{
				this.OnAllocatedMemoryChanging(value);
				this.OnPropertyChanging("AllocatedMemory", value);
				int? oldValue = this.AllocatedMemory;
				SetColumnValue(Columns.AllocatedMemory, value);
				this.OnAllocatedMemoryChanged(oldValue, value);
				this.OnPropertyChanged("AllocatedMemory", oldValue, value);
			}
		}
		partial void OnWorkingMemoryChanging(int? newValue);
		partial void OnWorkingMemoryChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("WorkingMemory")]
		[Bindable(true)]
		public int? WorkingMemory 
		{
			get { return GetColumnValue<int?>(Columns.WorkingMemory); }
			set
			{
				this.OnWorkingMemoryChanging(value);
				this.OnPropertyChanging("WorkingMemory", value);
				int? oldValue = this.WorkingMemory;
				SetColumnValue(Columns.WorkingMemory, value);
				this.OnWorkingMemoryChanged(oldValue, value);
				this.OnPropertyChanged("WorkingMemory", oldValue, value);
			}
		}
		partial void OnProcessUserChanging(string newValue);
		partial void OnProcessUserChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProcessUser")]
		[Bindable(true)]
		public string ProcessUser 
		{
			get { return GetColumnValue<string>(Columns.ProcessUser); }
			set
			{
				this.OnProcessUserChanging(value);
				this.OnPropertyChanging("ProcessUser", value);
				string oldValue = this.ProcessUser;
				SetColumnValue(Columns.ProcessUser, value);
				this.OnProcessUserChanged(oldValue, value);
				this.OnPropertyChanged("ProcessUser", oldValue, value);
			}
		}
		partial void OnProcessUserInteractiveChanging(bool? newValue);
		partial void OnProcessUserInteractiveChanged(bool? oldValue, bool? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProcessUserInteractive")]
		[Bindable(true)]
		public bool? ProcessUserInteractive 
		{
			get { return GetColumnValue<bool?>(Columns.ProcessUserInteractive); }
			set
			{
				this.OnProcessUserInteractiveChanging(value);
				this.OnPropertyChanging("ProcessUserInteractive", value);
				bool? oldValue = this.ProcessUserInteractive;
				SetColumnValue(Columns.ProcessUserInteractive, value);
				this.OnProcessUserInteractiveChanged(oldValue, value);
				this.OnPropertyChanged("ProcessUserInteractive", oldValue, value);
			}
		}
		partial void OnProcessUptimeChanging(long? newValue);
		partial void OnProcessUptimeChanged(long? oldValue, long? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProcessUptime")]
		[Bindable(true)]
		public long? ProcessUptime 
		{
			get { return GetColumnValue<long?>(Columns.ProcessUptime); }
			set
			{
				this.OnProcessUptimeChanging(value);
				this.OnPropertyChanging("ProcessUptime", value);
				long? oldValue = this.ProcessUptime;
				SetColumnValue(Columns.ProcessUptime, value);
				this.OnProcessUptimeChanged(oldValue, value);
				this.OnPropertyChanged("ProcessUptime", oldValue, value);
			}
		}
		partial void OnMessageChanging(string newValue);
		partial void OnMessageChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Message")]
		[Bindable(true)]
		public string Message 
		{
			get { return GetColumnValue<string>(Columns.Message); }
			set
			{
				this.OnMessageChanging(value);
				this.OnPropertyChanging("Message", value);
				string oldValue = this.Message;
				SetColumnValue(Columns.Message, value);
				this.OnMessageChanged(oldValue, value);
				this.OnPropertyChanged("Message", oldValue, value);
			}
		}
		partial void OnExceptionChanging(string newValue);
		partial void OnExceptionChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Exception")]
		[Bindable(true)]
		public string Exception 
		{
			get { return GetColumnValue<string>(Columns.Exception); }
			set
			{
				this.OnExceptionChanging(value);
				this.OnPropertyChanging("Exception", value);
				string oldValue = this.Exception;
				SetColumnValue(Columns.Exception, value);
				this.OnExceptionChanged(oldValue, value);
				this.OnPropertyChanged("Exception", oldValue, value);
			}
		}
		partial void OnStackTraceChanging(string newValue);
		partial void OnStackTraceChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("StackTrace")]
		[Bindable(true)]
		public string StackTrace 
		{
			get { return GetColumnValue<string>(Columns.StackTrace); }
			set
			{
				this.OnStackTraceChanging(value);
				this.OnPropertyChanging("StackTrace", value);
				string oldValue = this.StackTrace;
				SetColumnValue(Columns.StackTrace, value);
				this.OnStackTraceChanged(oldValue, value);
				this.OnPropertyChanged("StackTrace", oldValue, value);
			}
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="Log"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("Log#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="Log"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Log"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="Log"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			Log instance1 = this;
			Log instance2 = obj as Log;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedAt == instance2.CreatedAt)
			
				&& (instance1.DateX == instance2.DateX)
			
				&& (instance1.UtcDate == instance2.UtcDate)
			
				&& (instance1.Severity == instance2.Severity)
			
				&& (instance1.UserIdentity == instance2.UserIdentity)
			
				&& (instance1.UserName == instance2.UserName)
			
				&& (instance1.UserWebIdentity == instance2.UserWebIdentity)
			
				&& (instance1.Logger == instance2.Logger)
			
				&& (instance1.Location == instance2.Location)
			
				&& (instance1.WebSessionId == instance2.WebSessionId)
			
				&& (instance1.ProcessThread == instance2.ProcessThread)
			
				&& (instance1.MachineName == instance2.MachineName)
			
				&& (instance1.ProcessorCount == instance2.ProcessorCount)
			
				&& (instance1.OSVersion == instance2.OSVersion)
			
				&& (instance1.ClrVersion == instance2.ClrVersion)
			
				&& (instance1.AllocatedMemory == instance2.AllocatedMemory)
			
				&& (instance1.WorkingMemory == instance2.WorkingMemory)
			
				&& (instance1.ProcessUser == instance2.ProcessUser)
			
				&& (instance1.ProcessUserInteractive == instance2.ProcessUserInteractive)
			
				&& (instance1.ProcessUptime == instance2.ProcessUptime)
			
				&& (instance1.Message == instance2.Message)
			
				&& (instance1.Exception == instance2.Exception)
			
				&& (instance1.StackTrace == instance2.StackTrace)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="Log"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="Log"/> to compare.</param>
		/// <param name="instance2">The second <see cref="Log"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(Log instance1, Log instance2)
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
		public static TableSchema.TableColumn CreatedAtColumn
		{
			get { return Schema.Columns[1]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn DateXColumn
		{
			get { return Schema.Columns[2]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn UtcDateColumn
		{
			get { return Schema.Columns[3]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn SeverityColumn
		{
			get { return Schema.Columns[4]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn UserIdentityColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn UserNameColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn UserWebIdentityColumn
		{
			get { return Schema.Columns[7]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn LoggerColumn
		{
			get { return Schema.Columns[8]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn LocationColumn
		{
			get { return Schema.Columns[9]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WebSessionIdColumn
		{
			get { return Schema.Columns[10]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProcessThreadColumn
		{
			get { return Schema.Columns[11]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn MachineNameColumn
		{
			get { return Schema.Columns[12]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProcessorCountColumn
		{
			get { return Schema.Columns[13]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn OSVersionColumn
		{
			get { return Schema.Columns[14]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ClrVersionColumn
		{
			get { return Schema.Columns[15]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn AllocatedMemoryColumn
		{
			get { return Schema.Columns[16]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WorkingMemoryColumn
		{
			get { return Schema.Columns[17]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProcessUserColumn
		{
			get { return Schema.Columns[18]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProcessUserInteractiveColumn
		{
			get { return Schema.Columns[19]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ProcessUptimeColumn
		{
			get { return Schema.Columns[20]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn MessageColumn
		{
			get { return Schema.Columns[21]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ExceptionColumn
		{
			get { return Schema.Columns[22]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn StackTraceColumn
		{
			get { return Schema.Columns[23]; }
		}
        
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string CreatedAt = @"CreatedAt";
			 public static string DateX = @"Date";
			 public static string UtcDate = @"UtcDate";
			 public static string Severity = @"Severity";
			 public static string UserIdentity = @"UserIdentity";
			 public static string UserName = @"UserName";
			 public static string UserWebIdentity = @"UserWebIdentity";
			 public static string Logger = @"Logger";
			 public static string Location = @"Location";
			 public static string WebSessionId = @"WebSessionId";
			 public static string ProcessThread = @"ProcessThread";
			 public static string MachineName = @"MachineName";
			 public static string ProcessorCount = @"ProcessorCount";
			 public static string OSVersion = @"OSVersion";
			 public static string ClrVersion = @"ClrVersion";
			 public static string AllocatedMemory = @"AllocatedMemory";
			 public static string WorkingMemory = @"WorkingMemory";
			 public static string ProcessUser = @"ProcessUser";
			 public static string ProcessUserInteractive = @"ProcessUserInteractive";
			 public static string ProcessUptime = @"ProcessUptime";
			 public static string Message = @"Message";
			 public static string Exception = @"Exception";
			 public static string StackTrace = @"StackTrace";
			
		}
		#endregion
		#region Update PK Collections
		#endregion
		#region Deep Save
	//NOTE: Code Generation: Generation disabled: DeepSave method
		#endregion
	}
}
