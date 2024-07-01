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
	/// Strongly-typed collection for the JumpstationGroupPivot class.
	/// </summary>
    [Serializable]
	public partial class JumpstationGroupPivotCollection : ActiveList<JumpstationGroupPivot, JumpstationGroupPivotCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public JumpstationGroupPivotCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(JumpstationGroupPivot);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the JumpstationGroupPivot table.
	/// </summary>
	[Serializable]
	public partial class JumpstationGroupPivot : ActiveRecord<JumpstationGroupPivot>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected JumpstationGroupPivot(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public JumpstationGroupPivot() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public JumpstationGroupPivot(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public JumpstationGroupPivot(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public JumpstationGroupPivot(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="JumpstationGroupPivot"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private JumpstationGroupPivot(JumpstationGroupPivot original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(JumpstationGroupPivot original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.JumpstationGroupId = original.JumpstationGroupId;
			
			this.Brand = original.Brand;
			
			this.Cycle = original.Cycle;
			
			this.Locale = original.Locale;
			
			this.Touchpoint = original.Touchpoint;
			
			this.PartnerCategory = original.PartnerCategory;
			
			this.Platform = original.Platform;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="JumpstationGroupPivot"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static JumpstationGroupPivot Copy(JumpstationGroupPivot original)
		{
			return new JumpstationGroupPivot(original);
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
				TableSchema.Table schema = new TableSchema.Table("JumpstationGroupPivot", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarJumpstationGroupId = new TableSchema.TableColumn(schema);
				colvarJumpstationGroupId.ColumnName = "JumpstationGroupId";
				colvarJumpstationGroupId.DataType = DbType.Int32;
				colvarJumpstationGroupId.MaxLength = 0;
				colvarJumpstationGroupId.AutoIncrement = false;
				colvarJumpstationGroupId.IsNullable = false;
				colvarJumpstationGroupId.IsPrimaryKey = true;
				colvarJumpstationGroupId.IsForeignKey = false;
				colvarJumpstationGroupId.IsReadOnly = false;
				colvarJumpstationGroupId.DefaultSetting = @"";
				colvarJumpstationGroupId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarJumpstationGroupId);
				
				TableSchema.TableColumn colvarBrand = new TableSchema.TableColumn(schema);
				colvarBrand.ColumnName = "Brand";
				colvarBrand.DataType = DbType.String;
				colvarBrand.MaxLength = 256;
				colvarBrand.AutoIncrement = false;
				colvarBrand.IsNullable = true;
				colvarBrand.IsPrimaryKey = false;
				colvarBrand.IsForeignKey = false;
				colvarBrand.IsReadOnly = false;
				colvarBrand.DefaultSetting = @"";
				colvarBrand.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBrand);
				
				TableSchema.TableColumn colvarCycle = new TableSchema.TableColumn(schema);
				colvarCycle.ColumnName = "Cycle";
				colvarCycle.DataType = DbType.String;
				colvarCycle.MaxLength = 256;
				colvarCycle.AutoIncrement = false;
				colvarCycle.IsNullable = true;
				colvarCycle.IsPrimaryKey = false;
				colvarCycle.IsForeignKey = false;
				colvarCycle.IsReadOnly = false;
				colvarCycle.DefaultSetting = @"";
				colvarCycle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCycle);
				
				TableSchema.TableColumn colvarLocale = new TableSchema.TableColumn(schema);
				colvarLocale.ColumnName = "Locale";
				colvarLocale.DataType = DbType.String;
				colvarLocale.MaxLength = 256;
				colvarLocale.AutoIncrement = false;
				colvarLocale.IsNullable = true;
				colvarLocale.IsPrimaryKey = false;
				colvarLocale.IsForeignKey = false;
				colvarLocale.IsReadOnly = false;
				colvarLocale.DefaultSetting = @"";
				colvarLocale.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocale);
				
				TableSchema.TableColumn colvarTouchpoint = new TableSchema.TableColumn(schema);
				colvarTouchpoint.ColumnName = "Touchpoint";
				colvarTouchpoint.DataType = DbType.String;
				colvarTouchpoint.MaxLength = 256;
				colvarTouchpoint.AutoIncrement = false;
				colvarTouchpoint.IsNullable = true;
				colvarTouchpoint.IsPrimaryKey = false;
				colvarTouchpoint.IsForeignKey = false;
				colvarTouchpoint.IsReadOnly = false;
				colvarTouchpoint.DefaultSetting = @"";
				colvarTouchpoint.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTouchpoint);
				
				TableSchema.TableColumn colvarPartnerCategory = new TableSchema.TableColumn(schema);
				colvarPartnerCategory.ColumnName = "PartnerCategory";
				colvarPartnerCategory.DataType = DbType.String;
				colvarPartnerCategory.MaxLength = 256;
				colvarPartnerCategory.AutoIncrement = false;
				colvarPartnerCategory.IsNullable = true;
				colvarPartnerCategory.IsPrimaryKey = false;
				colvarPartnerCategory.IsForeignKey = false;
				colvarPartnerCategory.IsReadOnly = false;
				colvarPartnerCategory.DefaultSetting = @"";
				colvarPartnerCategory.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPartnerCategory);
				
				TableSchema.TableColumn colvarPlatform = new TableSchema.TableColumn(schema);
				colvarPlatform.ColumnName = "Platform";
				colvarPlatform.DataType = DbType.String;
				colvarPlatform.MaxLength = 256;
				colvarPlatform.AutoIncrement = false;
				colvarPlatform.IsNullable = true;
				colvarPlatform.IsPrimaryKey = false;
				colvarPlatform.IsForeignKey = false;
				colvarPlatform.IsReadOnly = false;
				colvarPlatform.DefaultSetting = @"";
				colvarPlatform.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPlatform);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("JumpstationGroupPivot",schema);
			}
		}
		#endregion
		
		#region Props
		partial void OnPropertyChanging(string propertyName, object newValue);
		partial void OnPropertyChanged(string propertyName, object oldValue, object newValue);
		partial void OnJumpstationGroupIdChanging(int newValue);
		partial void OnJumpstationGroupIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationGroupId")]
		[Bindable(true)]
		public int JumpstationGroupId 
		{
			get { return GetColumnValue<int>(Columns.JumpstationGroupId); }
			set
			{
				this.OnJumpstationGroupIdChanging(value);
				this.OnPropertyChanging("JumpstationGroupId", value);
				int oldValue = this.JumpstationGroupId;
				SetColumnValue(Columns.JumpstationGroupId, value);
				this.OnJumpstationGroupIdChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationGroupId", oldValue, value);
			}
		}
		partial void OnBrandChanging(string newValue);
		partial void OnBrandChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Brand")]
		[Bindable(true)]
		public string Brand 
		{
			get { return GetColumnValue<string>(Columns.Brand); }
			set
			{
				this.OnBrandChanging(value);
				this.OnPropertyChanging("Brand", value);
				string oldValue = this.Brand;
				SetColumnValue(Columns.Brand, value);
				this.OnBrandChanged(oldValue, value);
				this.OnPropertyChanged("Brand", oldValue, value);
			}
		}
		partial void OnCycleChanging(string newValue);
		partial void OnCycleChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Cycle")]
		[Bindable(true)]
		public string Cycle 
		{
			get { return GetColumnValue<string>(Columns.Cycle); }
			set
			{
				this.OnCycleChanging(value);
				this.OnPropertyChanging("Cycle", value);
				string oldValue = this.Cycle;
				SetColumnValue(Columns.Cycle, value);
				this.OnCycleChanged(oldValue, value);
				this.OnPropertyChanged("Cycle", oldValue, value);
			}
		}
		partial void OnLocaleChanging(string newValue);
		partial void OnLocaleChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Locale")]
		[Bindable(true)]
		public string Locale 
		{
			get { return GetColumnValue<string>(Columns.Locale); }
			set
			{
				this.OnLocaleChanging(value);
				this.OnPropertyChanging("Locale", value);
				string oldValue = this.Locale;
				SetColumnValue(Columns.Locale, value);
				this.OnLocaleChanged(oldValue, value);
				this.OnPropertyChanged("Locale", oldValue, value);
			}
		}
		partial void OnTouchpointChanging(string newValue);
		partial void OnTouchpointChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Touchpoint")]
		[Bindable(true)]
		public string Touchpoint 
		{
			get { return GetColumnValue<string>(Columns.Touchpoint); }
			set
			{
				this.OnTouchpointChanging(value);
				this.OnPropertyChanging("Touchpoint", value);
				string oldValue = this.Touchpoint;
				SetColumnValue(Columns.Touchpoint, value);
				this.OnTouchpointChanged(oldValue, value);
				this.OnPropertyChanged("Touchpoint", oldValue, value);
			}
		}
		partial void OnPartnerCategoryChanging(string newValue);
		partial void OnPartnerCategoryChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PartnerCategory")]
		[Bindable(true)]
		public string PartnerCategory 
		{
			get { return GetColumnValue<string>(Columns.PartnerCategory); }
			set
			{
				this.OnPartnerCategoryChanging(value);
				this.OnPropertyChanging("PartnerCategory", value);
				string oldValue = this.PartnerCategory;
				SetColumnValue(Columns.PartnerCategory, value);
				this.OnPartnerCategoryChanged(oldValue, value);
				this.OnPropertyChanged("PartnerCategory", oldValue, value);
			}
		}
		partial void OnPlatformChanging(string newValue);
		partial void OnPlatformChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Platform")]
		[Bindable(true)]
		public string Platform 
		{
			get { return GetColumnValue<string>(Columns.Platform); }
			set
			{
				this.OnPlatformChanging(value);
				this.OnPropertyChanging("Platform", value);
				string oldValue = this.Platform;
				SetColumnValue(Columns.Platform, value);
				this.OnPlatformChanged(oldValue, value);
				this.OnPropertyChanged("Platform", oldValue, value);
			}
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="JumpstationGroupPivot"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("JumpstationGroupPivot#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="JumpstationGroupPivot"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="JumpstationGroupPivot"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="JumpstationGroupPivot"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			JumpstationGroupPivot instance1 = this;
			JumpstationGroupPivot instance2 = obj as JumpstationGroupPivot;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.JumpstationGroupId == instance2.JumpstationGroupId)
			
				&& (instance1.Brand == instance2.Brand)
			
				&& (instance1.Cycle == instance2.Cycle)
			
				&& (instance1.Locale == instance2.Locale)
			
				&& (instance1.Touchpoint == instance2.Touchpoint)
			
				&& (instance1.PartnerCategory == instance2.PartnerCategory)
			
				&& (instance1.Platform == instance2.Platform)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="JumpstationGroupPivot"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="JumpstationGroupPivot"/> to compare.</param>
		/// <param name="instance2">The second <see cref="JumpstationGroupPivot"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(JumpstationGroupPivot instance1, JumpstationGroupPivot instance2)
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
		public static TableSchema.TableColumn JumpstationGroupIdColumn
		{
			get { return Schema.Columns[0]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn BrandColumn
		{
			get { return Schema.Columns[1]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CycleColumn
		{
			get { return Schema.Columns[2]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn LocaleColumn
		{
			get { return Schema.Columns[3]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn TouchpointColumn
		{
			get { return Schema.Columns[4]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn PartnerCategoryColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn PlatformColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			 public static string JumpstationGroupId = @"JumpstationGroupId";
			 public static string Brand = @"Brand";
			 public static string Cycle = @"Cycle";
			 public static string Locale = @"Locale";
			 public static string Touchpoint = @"Touchpoint";
			 public static string PartnerCategory = @"PartnerCategory";
			 public static string Platform = @"Platform";
			
		}
		#endregion
		#region Update PK Collections
		#endregion
		#region Deep Save
	//NOTE: Code Generation: Generation disabled: DeepSave method
		#endregion
	}
}
