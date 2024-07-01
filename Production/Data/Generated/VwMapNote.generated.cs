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
	/// Strongly-typed collection for the VwMapNote class.
	/// </summary>
	[Serializable]
	public partial class VwMapNoteCollection : ReadOnlyList<VwMapNote, VwMapNoteCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapNoteCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapNote);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapNote class.
	/// </summary>
	[Serializable]
	public partial class VwMapNoteController : BaseReadOnlyRecordController<VwMapNote, VwMapNoteCollection, VwMapNoteController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapNoteController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapNote.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapNote view.
	/// </summary>
	[Serializable]
	public partial class VwMapNote : ReadOnlyRecord<VwMapNote>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapNote", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarComment = new TableSchema.TableColumn(schema);
				colvarComment.ColumnName = "Comment";
				colvarComment.DataType = DbType.String;
				colvarComment.MaxLength = -1;
				colvarComment.AutoIncrement = false;
				colvarComment.IsNullable = true;
				colvarComment.IsPrimaryKey = false;
				colvarComment.IsForeignKey = false;
				colvarComment.IsReadOnly = false;
				
				schema.Columns.Add(colvarComment);
				
				TableSchema.TableColumn colvarEntityTypeId = new TableSchema.TableColumn(schema);
				colvarEntityTypeId.ColumnName = "EntityTypeId";
				colvarEntityTypeId.DataType = DbType.Int32;
				colvarEntityTypeId.MaxLength = 0;
				colvarEntityTypeId.AutoIncrement = false;
				colvarEntityTypeId.IsNullable = false;
				colvarEntityTypeId.IsPrimaryKey = false;
				colvarEntityTypeId.IsForeignKey = false;
				colvarEntityTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarEntityTypeId);
				
				TableSchema.TableColumn colvarEntityTypeName = new TableSchema.TableColumn(schema);
				colvarEntityTypeName.ColumnName = "EntityTypeName";
				colvarEntityTypeName.DataType = DbType.String;
				colvarEntityTypeName.MaxLength = 256;
				colvarEntityTypeName.AutoIncrement = false;
				colvarEntityTypeName.IsNullable = false;
				colvarEntityTypeName.IsPrimaryKey = false;
				colvarEntityTypeName.IsForeignKey = false;
				colvarEntityTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarEntityTypeName);
				
				TableSchema.TableColumn colvarEntityId = new TableSchema.TableColumn(schema);
				colvarEntityId.ColumnName = "EntityId";
				colvarEntityId.DataType = DbType.Int32;
				colvarEntityId.MaxLength = 0;
				colvarEntityId.AutoIncrement = false;
				colvarEntityId.IsNullable = false;
				colvarEntityId.IsPrimaryKey = false;
				colvarEntityId.IsForeignKey = false;
				colvarEntityId.IsReadOnly = false;
				
				schema.Columns.Add(colvarEntityId);
				
				TableSchema.TableColumn colvarNoteTypeId = new TableSchema.TableColumn(schema);
				colvarNoteTypeId.ColumnName = "NoteTypeId";
				colvarNoteTypeId.DataType = DbType.Int32;
				colvarNoteTypeId.MaxLength = 0;
				colvarNoteTypeId.AutoIncrement = false;
				colvarNoteTypeId.IsNullable = true;
				colvarNoteTypeId.IsPrimaryKey = false;
				colvarNoteTypeId.IsForeignKey = false;
				colvarNoteTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarNoteTypeId);
				
				TableSchema.TableColumn colvarNoteTypeName = new TableSchema.TableColumn(schema);
				colvarNoteTypeName.ColumnName = "NoteTypeName";
				colvarNoteTypeName.DataType = DbType.String;
				colvarNoteTypeName.MaxLength = 256;
				colvarNoteTypeName.AutoIncrement = false;
				colvarNoteTypeName.IsNullable = true;
				colvarNoteTypeName.IsPrimaryKey = false;
				colvarNoteTypeName.IsForeignKey = false;
				colvarNoteTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarNoteTypeName);
				
				TableSchema.TableColumn colvarNoteCount = new TableSchema.TableColumn(schema);
				colvarNoteCount.ColumnName = "NoteCount";
				colvarNoteCount.DataType = DbType.Int32;
				colvarNoteCount.MaxLength = 0;
				colvarNoteCount.AutoIncrement = false;
				colvarNoteCount.IsNullable = true;
				colvarNoteCount.IsPrimaryKey = false;
				colvarNoteCount.IsForeignKey = false;
				colvarNoteCount.IsReadOnly = false;
				
				schema.Columns.Add(colvarNoteCount);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapNote",schema);
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
		protected VwMapNote(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapNote() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapNote(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapNote(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapNote(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapNote"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapNote(VwMapNote original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapNote original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.Name = original.Name;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.RowStatusName = original.RowStatusName;
			
			this.Comment = original.Comment;
			
			this.EntityTypeId = original.EntityTypeId;
			
			this.EntityTypeName = original.EntityTypeName;
			
			this.EntityId = original.EntityId;
			
			this.NoteTypeId = original.NoteTypeId;
			
			this.NoteTypeName = original.NoteTypeName;
			
			this.NoteCount = original.NoteCount;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapNote"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapNote Copy(VwMapNote original)
		{
			return new VwMapNote(original);
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
		partial void OnCommentChanging(string newValue);
		partial void OnCommentChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Comment")]
		[Bindable(true)]
		public string Comment 
		{
			get
			{
				return GetColumnValue<string>("Comment");
			}
			set
			{
				this.OnCommentChanging(value);
				this.OnPropertyChanging("Comment", value);
				string oldValue = this.Comment;
				SetColumnValue("Comment", value);
				this.OnCommentChanged(oldValue, value);
				this.OnPropertyChanged("Comment", oldValue, value);
			}
		}
		partial void OnEntityTypeIdChanging(int newValue);
		partial void OnEntityTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("EntityTypeId")]
		[Bindable(true)]
		public int EntityTypeId 
		{
			get
			{
				return GetColumnValue<int>("EntityTypeId");
			}
			set
			{
				this.OnEntityTypeIdChanging(value);
				this.OnPropertyChanging("EntityTypeId", value);
				int oldValue = this.EntityTypeId;
				SetColumnValue("EntityTypeId", value);
				this.OnEntityTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("EntityTypeId", oldValue, value);
			}
		}
		partial void OnEntityTypeNameChanging(string newValue);
		partial void OnEntityTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("EntityTypeName")]
		[Bindable(true)]
		public string EntityTypeName 
		{
			get
			{
				return GetColumnValue<string>("EntityTypeName");
			}
			set
			{
				this.OnEntityTypeNameChanging(value);
				this.OnPropertyChanging("EntityTypeName", value);
				string oldValue = this.EntityTypeName;
				SetColumnValue("EntityTypeName", value);
				this.OnEntityTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("EntityTypeName", oldValue, value);
			}
		}
		partial void OnEntityIdChanging(int newValue);
		partial void OnEntityIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("EntityId")]
		[Bindable(true)]
		public int EntityId 
		{
			get
			{
				return GetColumnValue<int>("EntityId");
			}
			set
			{
				this.OnEntityIdChanging(value);
				this.OnPropertyChanging("EntityId", value);
				int oldValue = this.EntityId;
				SetColumnValue("EntityId", value);
				this.OnEntityIdChanged(oldValue, value);
				this.OnPropertyChanged("EntityId", oldValue, value);
			}
		}
		partial void OnNoteTypeIdChanging(int? newValue);
		partial void OnNoteTypeIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("NoteTypeId")]
		[Bindable(true)]
		public int? NoteTypeId 
		{
			get
			{
				return GetColumnValue<int?>("NoteTypeId");
			}
			set
			{
				this.OnNoteTypeIdChanging(value);
				this.OnPropertyChanging("NoteTypeId", value);
				int? oldValue = this.NoteTypeId;
				SetColumnValue("NoteTypeId", value);
				this.OnNoteTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("NoteTypeId", oldValue, value);
			}
		}
		partial void OnNoteTypeNameChanging(string newValue);
		partial void OnNoteTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("NoteTypeName")]
		[Bindable(true)]
		public string NoteTypeName 
		{
			get
			{
				return GetColumnValue<string>("NoteTypeName");
			}
			set
			{
				this.OnNoteTypeNameChanging(value);
				this.OnPropertyChanging("NoteTypeName", value);
				string oldValue = this.NoteTypeName;
				SetColumnValue("NoteTypeName", value);
				this.OnNoteTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("NoteTypeName", oldValue, value);
			}
		}
		partial void OnNoteCountChanging(int? newValue);
		partial void OnNoteCountChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("NoteCount")]
		[Bindable(true)]
		public int? NoteCount 
		{
			get
			{
				return GetColumnValue<int?>("NoteCount");
			}
			set
			{
				this.OnNoteCountChanging(value);
				this.OnPropertyChanging("NoteCount", value);
				int? oldValue = this.NoteCount;
				SetColumnValue("NoteCount", value);
				this.OnNoteCountChanged(oldValue, value);
				this.OnPropertyChanged("NoteCount", oldValue, value);
			}
		}
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string Id = @"Id";
			
			public static string Name = @"Name";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string RowStatusName = @"RowStatusName";
			
			public static string Comment = @"Comment";
			
			public static string EntityTypeId = @"EntityTypeId";
			
			public static string EntityTypeName = @"EntityTypeName";
			
			public static string EntityId = @"EntityId";
			
			public static string NoteTypeId = @"NoteTypeId";
			
			public static string NoteTypeName = @"NoteTypeName";
			
			public static string NoteCount = @"NoteCount";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapNote"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapNote#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapNote"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapNote"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapNote"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapNote instance1 = this;
			VwMapNote instance2 = obj as VwMapNote;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.Comment == instance2.Comment)
			
				&& (instance1.EntityTypeId == instance2.EntityTypeId)
			
				&& (instance1.EntityTypeName == instance2.EntityTypeName)
			
				&& (instance1.EntityId == instance2.EntityId)
			
				&& (instance1.NoteTypeId == instance2.NoteTypeId)
			
				&& (instance1.NoteTypeName == instance2.NoteTypeName)
			
				&& (instance1.NoteCount == instance2.NoteCount)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapNote"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapNote"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapNote"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapNote instance1, VwMapNote instance2)
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
