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
	/// Strongly-typed collection for the ConfigurationServiceGroupSelectorQueryParameterValue class.
	/// </summary>
    [Serializable]
	public partial class ConfigurationServiceGroupSelectorQueryParameterValueCollection : ActiveList<ConfigurationServiceGroupSelectorQueryParameterValue, ConfigurationServiceGroupSelectorQueryParameterValueCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupSelectorQueryParameterValueCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(ConfigurationServiceGroupSelectorQueryParameterValue);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the ConfigurationServiceGroupSelector_QueryParameterValue table.
	/// </summary>
	[Serializable]
	public partial class ConfigurationServiceGroupSelectorQueryParameterValue : ActiveRecord<ConfigurationServiceGroupSelectorQueryParameterValue>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected ConfigurationServiceGroupSelectorQueryParameterValue(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public ConfigurationServiceGroupSelectorQueryParameterValue() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupSelectorQueryParameterValue(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupSelectorQueryParameterValue(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public ConfigurationServiceGroupSelectorQueryParameterValue(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private ConfigurationServiceGroupSelectorQueryParameterValue(ConfigurationServiceGroupSelectorQueryParameterValue original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(ConfigurationServiceGroupSelectorQueryParameterValue original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.ConfigurationServiceGroupSelectorId = original.ConfigurationServiceGroupSelectorId;
			
			this.QueryParameterValueId = original.QueryParameterValueId;
			
			this.Negation = original.Negation;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static ConfigurationServiceGroupSelectorQueryParameterValue Copy(ConfigurationServiceGroupSelectorQueryParameterValue original)
		{
			return new ConfigurationServiceGroupSelectorQueryParameterValue(original);
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
				TableSchema.Table schema = new TableSchema.Table("ConfigurationServiceGroupSelector_QueryParameterValue", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarConfigurationServiceGroupSelectorId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupSelectorId.ColumnName = "ConfigurationServiceGroupSelectorId";
				colvarConfigurationServiceGroupSelectorId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupSelectorId.MaxLength = 0;
				colvarConfigurationServiceGroupSelectorId.AutoIncrement = false;
				colvarConfigurationServiceGroupSelectorId.IsNullable = false;
				colvarConfigurationServiceGroupSelectorId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupSelectorId.IsForeignKey = true;
				colvarConfigurationServiceGroupSelectorId.IsReadOnly = false;
				colvarConfigurationServiceGroupSelectorId.DefaultSetting = @"";
				
					colvarConfigurationServiceGroupSelectorId.ForeignKeyTableName = "ConfigurationServiceGroupSelector";
				schema.Columns.Add(colvarConfigurationServiceGroupSelectorId);
				
				TableSchema.TableColumn colvarQueryParameterValueId = new TableSchema.TableColumn(schema);
				colvarQueryParameterValueId.ColumnName = "QueryParameterValueId";
				colvarQueryParameterValueId.DataType = DbType.Int32;
				colvarQueryParameterValueId.MaxLength = 0;
				colvarQueryParameterValueId.AutoIncrement = false;
				colvarQueryParameterValueId.IsNullable = false;
				colvarQueryParameterValueId.IsPrimaryKey = false;
				colvarQueryParameterValueId.IsForeignKey = true;
				colvarQueryParameterValueId.IsReadOnly = false;
				colvarQueryParameterValueId.DefaultSetting = @"";
				
					colvarQueryParameterValueId.ForeignKeyTableName = "QueryParameterValue";
				schema.Columns.Add(colvarQueryParameterValueId);
				
				TableSchema.TableColumn colvarNegation = new TableSchema.TableColumn(schema);
				colvarNegation.ColumnName = "Negation";
				colvarNegation.DataType = DbType.Boolean;
				colvarNegation.MaxLength = 0;
				colvarNegation.AutoIncrement = false;
				colvarNegation.IsNullable = false;
				colvarNegation.IsPrimaryKey = false;
				colvarNegation.IsForeignKey = false;
				colvarNegation.IsReadOnly = false;
				colvarNegation.DefaultSetting = @"";
				colvarNegation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNegation);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("ConfigurationServiceGroupSelector_QueryParameterValue",schema);
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
		partial void OnConfigurationServiceGroupSelectorIdChanging(int newValue);
		partial void OnConfigurationServiceGroupSelectorIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupSelectorId")]
		[Bindable(true)]
		public int ConfigurationServiceGroupSelectorId 
		{
			get { return GetColumnValue<int>(Columns.ConfigurationServiceGroupSelectorId); }
			set
			{
				this.OnConfigurationServiceGroupSelectorIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupSelectorId", value);
				int oldValue = this.ConfigurationServiceGroupSelectorId;
				SetColumnValue(Columns.ConfigurationServiceGroupSelectorId, value);
				this.OnConfigurationServiceGroupSelectorIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupSelectorId", oldValue, value);
			}
		}
		partial void OnQueryParameterValueIdChanging(int newValue);
		partial void OnQueryParameterValueIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryParameterValueId")]
		[Bindable(true)]
		public int QueryParameterValueId 
		{
			get { return GetColumnValue<int>(Columns.QueryParameterValueId); }
			set
			{
				this.OnQueryParameterValueIdChanging(value);
				this.OnPropertyChanging("QueryParameterValueId", value);
				int oldValue = this.QueryParameterValueId;
				SetColumnValue(Columns.QueryParameterValueId, value);
				this.OnQueryParameterValueIdChanged(oldValue, value);
				this.OnPropertyChanged("QueryParameterValueId", oldValue, value);
			}
		}
		partial void OnNegationChanging(bool newValue);
		partial void OnNegationChanged(bool oldValue, bool newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Negation")]
		[Bindable(true)]
		public bool Negation 
		{
			get { return GetColumnValue<bool>(Columns.Negation); }
			set
			{
				this.OnNegationChanging(value);
				this.OnPropertyChanging("Negation", value);
				bool oldValue = this.Negation;
				SetColumnValue(Columns.Negation, value);
				this.OnNegationChanged(oldValue, value);
				this.OnPropertyChanged("Negation", oldValue, value);
			}
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a ConfigurationServiceGroupSelector ActiveRecord object related to this ConfigurationServiceGroupSelectorQueryParameterValue
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupSelector ConfigurationServiceGroupSelector
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupSelector.FetchByID(this.ConfigurationServiceGroupSelectorId); }
			set { SetColumnValue("ConfigurationServiceGroupSelectorId", value.Id); }
		}
		
		/// <summary>
		/// Returns a QueryParameterValue ActiveRecord object related to this ConfigurationServiceGroupSelectorQueryParameterValue
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.QueryParameterValue QueryParameterValue
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.QueryParameterValue.FetchByID(this.QueryParameterValueId); }
			set { SetColumnValue("QueryParameterValueId", value.Id); }
		}
		
		#endregion
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("ConfigurationServiceGroupSelectorQueryParameterValue#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			ConfigurationServiceGroupSelectorQueryParameterValue instance1 = this;
			ConfigurationServiceGroupSelectorQueryParameterValue instance2 = obj as ConfigurationServiceGroupSelectorQueryParameterValue;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.ConfigurationServiceGroupSelectorId == instance2.ConfigurationServiceGroupSelectorId)
			
				&& (instance1.QueryParameterValueId == instance2.QueryParameterValueId)
			
				&& (instance1.Negation == instance2.Negation)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/> to compare.</param>
		/// <param name="instance2">The second <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(ConfigurationServiceGroupSelectorQueryParameterValue instance1, ConfigurationServiceGroupSelectorQueryParameterValue instance2)
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
		public static TableSchema.TableColumn ConfigurationServiceGroupSelectorIdColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn QueryParameterValueIdColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn NegationColumn
		{
			get { return Schema.Columns[7]; }
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
			 public static string ConfigurationServiceGroupSelectorId = @"ConfigurationServiceGroupSelectorId";
			 public static string QueryParameterValueId = @"QueryParameterValueId";
			 public static string Negation = @"Negation";
			
		}
		#endregion
		#region Update PK Collections
		#endregion
		#region Deep Save
	//NOTE: Code Generation: Generation disabled: DeepSave method
		#endregion
	}
}
