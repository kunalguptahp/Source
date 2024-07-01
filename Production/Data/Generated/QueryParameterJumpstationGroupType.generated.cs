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
	/// Strongly-typed collection for the QueryParameterJumpstationGroupType class.
	/// </summary>
    [Serializable]
	public partial class QueryParameterJumpstationGroupTypeCollection : ActiveList<QueryParameterJumpstationGroupType, QueryParameterJumpstationGroupTypeCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public QueryParameterJumpstationGroupTypeCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(QueryParameterJumpstationGroupType);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the QueryParameter_JumpstationGroupType table.
	/// </summary>
	[Serializable]
	public partial class QueryParameterJumpstationGroupType : ActiveRecord<QueryParameterJumpstationGroupType>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected QueryParameterJumpstationGroupType(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public QueryParameterJumpstationGroupType() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public QueryParameterJumpstationGroupType(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public QueryParameterJumpstationGroupType(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public QueryParameterJumpstationGroupType(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="QueryParameterJumpstationGroupType"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private QueryParameterJumpstationGroupType(QueryParameterJumpstationGroupType original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(QueryParameterJumpstationGroupType original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.JumpstationGroupTypeId = original.JumpstationGroupTypeId;
			
			this.QueryParameterId = original.QueryParameterId;
			
			this.Name = original.Name;
			
			this.Description = original.Description;
			
			this.MaximumSelection = original.MaximumSelection;
			
			this.Wildcard = original.Wildcard;
			
			this.SortOrder = original.SortOrder;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="QueryParameterJumpstationGroupType"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static QueryParameterJumpstationGroupType Copy(QueryParameterJumpstationGroupType original)
		{
			return new QueryParameterJumpstationGroupType(original);
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
				TableSchema.Table schema = new TableSchema.Table("QueryParameter_JumpstationGroupType", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarJumpstationGroupTypeId = new TableSchema.TableColumn(schema);
				colvarJumpstationGroupTypeId.ColumnName = "JumpstationGroupTypeId";
				colvarJumpstationGroupTypeId.DataType = DbType.Int32;
				colvarJumpstationGroupTypeId.MaxLength = 0;
				colvarJumpstationGroupTypeId.AutoIncrement = false;
				colvarJumpstationGroupTypeId.IsNullable = false;
				colvarJumpstationGroupTypeId.IsPrimaryKey = false;
				colvarJumpstationGroupTypeId.IsForeignKey = true;
				colvarJumpstationGroupTypeId.IsReadOnly = false;
				colvarJumpstationGroupTypeId.DefaultSetting = @"";
				
					colvarJumpstationGroupTypeId.ForeignKeyTableName = "JumpstationGroupType";
				schema.Columns.Add(colvarJumpstationGroupTypeId);
				
				TableSchema.TableColumn colvarQueryParameterId = new TableSchema.TableColumn(schema);
				colvarQueryParameterId.ColumnName = "QueryParameterId";
				colvarQueryParameterId.DataType = DbType.Int32;
				colvarQueryParameterId.MaxLength = 0;
				colvarQueryParameterId.AutoIncrement = false;
				colvarQueryParameterId.IsNullable = false;
				colvarQueryParameterId.IsPrimaryKey = false;
				colvarQueryParameterId.IsForeignKey = true;
				colvarQueryParameterId.IsReadOnly = false;
				colvarQueryParameterId.DefaultSetting = @"";
				
					colvarQueryParameterId.ForeignKeyTableName = "QueryParameter";
				schema.Columns.Add(colvarQueryParameterId);
				
				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 256;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);
				
				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = 512;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
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
				colvarMaximumSelection.DefaultSetting = @"";
				colvarMaximumSelection.ForeignKeyTableName = "";
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
				colvarWildcard.DefaultSetting = @"";
				colvarWildcard.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWildcard);
				
				TableSchema.TableColumn colvarSortOrder = new TableSchema.TableColumn(schema);
				colvarSortOrder.ColumnName = "SortOrder";
				colvarSortOrder.DataType = DbType.Int32;
				colvarSortOrder.MaxLength = 0;
				colvarSortOrder.AutoIncrement = false;
				colvarSortOrder.IsNullable = true;
				colvarSortOrder.IsPrimaryKey = false;
				colvarSortOrder.IsForeignKey = false;
				colvarSortOrder.IsReadOnly = false;
				colvarSortOrder.DefaultSetting = @"";
				colvarSortOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSortOrder);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("QueryParameter_JumpstationGroupType",schema);
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
		partial void OnJumpstationGroupTypeIdChanging(int newValue);
		partial void OnJumpstationGroupTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationGroupTypeId")]
		[Bindable(true)]
		public int JumpstationGroupTypeId 
		{
			get { return GetColumnValue<int>(Columns.JumpstationGroupTypeId); }
			set
			{
				this.OnJumpstationGroupTypeIdChanging(value);
				this.OnPropertyChanging("JumpstationGroupTypeId", value);
				int oldValue = this.JumpstationGroupTypeId;
				SetColumnValue(Columns.JumpstationGroupTypeId, value);
				this.OnJumpstationGroupTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationGroupTypeId", oldValue, value);
			}
		}
		partial void OnQueryParameterIdChanging(int newValue);
		partial void OnQueryParameterIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryParameterId")]
		[Bindable(true)]
		public int QueryParameterId 
		{
			get { return GetColumnValue<int>(Columns.QueryParameterId); }
			set
			{
				this.OnQueryParameterIdChanging(value);
				this.OnPropertyChanging("QueryParameterId", value);
				int oldValue = this.QueryParameterId;
				SetColumnValue(Columns.QueryParameterId, value);
				this.OnQueryParameterIdChanged(oldValue, value);
				this.OnPropertyChanged("QueryParameterId", oldValue, value);
			}
		}
		partial void OnNameChanging(string newValue);
		partial void OnNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Name")]
		[Bindable(true)]
		public string Name 
		{
			get { return GetColumnValue<string>(Columns.Name); }
			set
			{
				this.OnNameChanging(value);
				this.OnPropertyChanging("Name", value);
				string oldValue = this.Name;
				SetColumnValue(Columns.Name, value);
				this.OnNameChanged(oldValue, value);
				this.OnPropertyChanged("Name", oldValue, value);
			}
		}
		partial void OnDescriptionChanging(string newValue);
		partial void OnDescriptionChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Description")]
		[Bindable(true)]
		public string Description 
		{
			get { return GetColumnValue<string>(Columns.Description); }
			set
			{
				this.OnDescriptionChanging(value);
				this.OnPropertyChanging("Description", value);
				string oldValue = this.Description;
				SetColumnValue(Columns.Description, value);
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
			get { return GetColumnValue<int>(Columns.MaximumSelection); }
			set
			{
				this.OnMaximumSelectionChanging(value);
				this.OnPropertyChanging("MaximumSelection", value);
				int oldValue = this.MaximumSelection;
				SetColumnValue(Columns.MaximumSelection, value);
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
			get { return GetColumnValue<bool>(Columns.Wildcard); }
			set
			{
				this.OnWildcardChanging(value);
				this.OnPropertyChanging("Wildcard", value);
				bool oldValue = this.Wildcard;
				SetColumnValue(Columns.Wildcard, value);
				this.OnWildcardChanged(oldValue, value);
				this.OnPropertyChanged("Wildcard", oldValue, value);
			}
		}
		partial void OnSortOrderChanging(int? newValue);
		partial void OnSortOrderChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("SortOrder")]
		[Bindable(true)]
		public int? SortOrder 
		{
			get { return GetColumnValue<int?>(Columns.SortOrder); }
			set
			{
				this.OnSortOrderChanging(value);
				this.OnPropertyChanging("SortOrder", value);
				int? oldValue = this.SortOrder;
				SetColumnValue(Columns.SortOrder, value);
				this.OnSortOrderChanged(oldValue, value);
				this.OnPropertyChanged("SortOrder", oldValue, value);
			}
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a JumpstationGroupType ActiveRecord object related to this QueryParameterJumpstationGroupType
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.JumpstationGroupType JumpstationGroupType
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.JumpstationGroupType.FetchByID(this.JumpstationGroupTypeId); }
			set { SetColumnValue("JumpstationGroupTypeId", value.Id); }
		}
		
		/// <summary>
		/// Returns a QueryParameter ActiveRecord object related to this QueryParameterJumpstationGroupType
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.QueryParameter QueryParameter
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.QueryParameter.FetchByID(this.QueryParameterId); }
			set { SetColumnValue("QueryParameterId", value.Id); }
		}
		
		#endregion
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="QueryParameterJumpstationGroupType"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("QueryParameterJumpstationGroupType#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="QueryParameterJumpstationGroupType"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="QueryParameterJumpstationGroupType"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="QueryParameterJumpstationGroupType"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			QueryParameterJumpstationGroupType instance1 = this;
			QueryParameterJumpstationGroupType instance2 = obj as QueryParameterJumpstationGroupType;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.JumpstationGroupTypeId == instance2.JumpstationGroupTypeId)
			
				&& (instance1.QueryParameterId == instance2.QueryParameterId)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.MaximumSelection == instance2.MaximumSelection)
			
				&& (instance1.Wildcard == instance2.Wildcard)
			
				&& (instance1.SortOrder == instance2.SortOrder)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="QueryParameterJumpstationGroupType"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="QueryParameterJumpstationGroupType"/> to compare.</param>
		/// <param name="instance2">The second <see cref="QueryParameterJumpstationGroupType"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(QueryParameterJumpstationGroupType instance1, QueryParameterJumpstationGroupType instance2)
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
		public static TableSchema.TableColumn JumpstationGroupTypeIdColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn QueryParameterIdColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[7]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[8]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn MaximumSelectionColumn
		{
			get { return Schema.Columns[9]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn WildcardColumn
		{
			get { return Schema.Columns[10]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn SortOrderColumn
		{
			get { return Schema.Columns[11]; }
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
			 public static string JumpstationGroupTypeId = @"JumpstationGroupTypeId";
			 public static string QueryParameterId = @"QueryParameterId";
			 public static string Name = @"Name";
			 public static string Description = @"Description";
			 public static string MaximumSelection = @"MaximumSelection";
			 public static string Wildcard = @"Wildcard";
			 public static string SortOrder = @"SortOrder";
			
		}
		#endregion
		#region Update PK Collections
		#endregion
		#region Deep Save
	//NOTE: Code Generation: Generation disabled: DeepSave method
		#endregion
	}
}
