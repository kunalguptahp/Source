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
	/// Strongly-typed collection for the Tag class.
	/// </summary>
    [Serializable]
	public partial class TagCollection : ActiveList<Tag, TagCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public TagCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(Tag);
		}
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the Tag table.
	/// </summary>
	[Serializable]
	public partial class Tag : ActiveRecord<Tag>, IActiveRecord, IRecord
	{
		#region Constructors and Default Settings
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		protected Tag(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		//[Obsolete("REVIEW: Default Constructor usage. Should the ForceDBDefaults param be explicitly specified here?", true)]
		public Tag() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Tag(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Tag(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Tag(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="Tag"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private Tag(Tag original)
			: this(false, false)
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(Tag original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.Name = original.Name;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.Notes = original.Notes;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="Tag"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static Tag Copy(Tag original)
		{
			return new Tag(original);
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
				TableSchema.Table schema = new TableSchema.Table("Tag", TableType.Table, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarNotes = new TableSchema.TableColumn(schema);
				colvarNotes.ColumnName = "Notes";
				colvarNotes.DataType = DbType.String;
				colvarNotes.MaxLength = 2048;
				colvarNotes.AutoIncrement = false;
				colvarNotes.IsNullable = true;
				colvarNotes.IsPrimaryKey = false;
				colvarNotes.IsForeignKey = false;
				colvarNotes.IsReadOnly = false;
				colvarNotes.DefaultSetting = @"";
				colvarNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNotes);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("Tag",schema);
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
		partial void OnNotesChanging(string newValue);
		partial void OnNotesChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Notes")]
		[Bindable(true)]
		public string Notes 
		{
			get { return GetColumnValue<string>(Columns.Notes); }
			set
			{
				this.OnNotesChanging(value);
				this.OnPropertyChanging("Notes", value);
				string oldValue = this.Notes;
				SetColumnValue(Columns.Notes, value);
				this.OnNotesChanged(oldValue, value);
				this.OnPropertyChanged("Notes", oldValue, value);
			}
		}
		#endregion
		
		#region PrimaryKey Methods		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupTagCollection ConfigurationServiceGroupTagRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupTagCollection().Where(ConfigurationServiceGroupTag.Columns.TagId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.JumpstationGroupTagCollection JumpstationGroupTagRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.JumpstationGroupTagCollection().Where(JumpstationGroupTag.Columns.TagId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ProxyURLTagCollection ProxyURLTagRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.ProxyURLTagCollection().Where(ProxyURLTag.Columns.TagId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowTagCollection WorkflowTagRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.WorkflowTagCollection().Where(WorkflowTag.Columns.TagId, Id).Load();
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleTagCollection WorkflowModuleTagRecords()
		{
			return new HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleTagCollection().Where(WorkflowModuleTag.Columns.TagId, Id).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a RowStatus ActiveRecord object related to this Tag
		/// </summary>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.RowStatus RowStatus
		{
			get { return HP.ElementsCPS.Data.SubSonicClient.RowStatus.FetchByID(this.RowStatusId); }
			set { SetColumnValue("RowStatusId", value.Id); }
		}
		
		#endregion
		
		
		#region Many To Many Helpers
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowCollection GetWorkflowCollection() { return Tag.GetWorkflowCollection(this.Id); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static HP.ElementsCPS.Data.SubSonicClient.WorkflowCollection GetWorkflowCollection(int varId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[Workflow] INNER JOIN [Workflow_Tag] ON [Workflow].[Id] = [Workflow_Tag].[WorkflowId] WHERE [Workflow_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmd.AddParameter("@TagId", varId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			WorkflowCollection coll = new WorkflowCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveWorkflowMap(int varId, WorkflowCollection items)
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [Workflow_Tag] WHERE [Workflow_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (Workflow item in items)
			{
				WorkflowTag varWorkflowTag = new WorkflowTag(true);
				varWorkflowTag.SetColumnValue("TagId", varId);
				varWorkflowTag.SetColumnValue("WorkflowId", item.GetPrimaryKeyValue());
				varWorkflowTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveWorkflowMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [Workflow_Tag] WHERE [Workflow_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					WorkflowTag varWorkflowTag = new WorkflowTag(true);
					varWorkflowTag.SetColumnValue("TagId", varId);
					varWorkflowTag.SetColumnValue("WorkflowId", l.Value);
					varWorkflowTag.Save();
				}
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveWorkflowMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [Workflow_Tag] WHERE [Workflow_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				WorkflowTag varWorkflowTag = new WorkflowTag(true);
				varWorkflowTag.SetColumnValue("TagId", varId);
				varWorkflowTag.SetColumnValue("WorkflowId", item);
				varWorkflowTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void DeleteWorkflowMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [Workflow_Tag] WHERE [Workflow_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleCollection GetWorkflowModuleCollection() { return Tag.GetWorkflowModuleCollection(this.Id); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static HP.ElementsCPS.Data.SubSonicClient.WorkflowModuleCollection GetWorkflowModuleCollection(int varId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[WorkflowModule] INNER JOIN [WorkflowModule_Tag] ON [WorkflowModule].[Id] = [WorkflowModule_Tag].[WorkflowModuleId] WHERE [WorkflowModule_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmd.AddParameter("@TagId", varId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			WorkflowModuleCollection coll = new WorkflowModuleCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveWorkflowModuleMap(int varId, WorkflowModuleCollection items)
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [WorkflowModule_Tag] WHERE [WorkflowModule_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (WorkflowModule item in items)
			{
				WorkflowModuleTag varWorkflowModuleTag = new WorkflowModuleTag(true);
				varWorkflowModuleTag.SetColumnValue("TagId", varId);
				varWorkflowModuleTag.SetColumnValue("WorkflowModuleId", item.GetPrimaryKeyValue());
				varWorkflowModuleTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveWorkflowModuleMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [WorkflowModule_Tag] WHERE [WorkflowModule_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					WorkflowModuleTag varWorkflowModuleTag = new WorkflowModuleTag(true);
					varWorkflowModuleTag.SetColumnValue("TagId", varId);
					varWorkflowModuleTag.SetColumnValue("WorkflowModuleId", l.Value);
					varWorkflowModuleTag.Save();
				}
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveWorkflowModuleMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [WorkflowModule_Tag] WHERE [WorkflowModule_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				WorkflowModuleTag varWorkflowModuleTag = new WorkflowModuleTag(true);
				varWorkflowModuleTag.SetColumnValue("TagId", varId);
				varWorkflowModuleTag.SetColumnValue("WorkflowModuleId", item);
				varWorkflowModuleTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void DeleteWorkflowModuleMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [WorkflowModule_Tag] WHERE [WorkflowModule_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupCollection GetConfigurationServiceGroupCollection() { return Tag.GetConfigurationServiceGroupCollection(this.Id); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static HP.ElementsCPS.Data.SubSonicClient.ConfigurationServiceGroupCollection GetConfigurationServiceGroupCollection(int varId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[ConfigurationServiceGroup] INNER JOIN [ConfigurationServiceGroup_Tag] ON [ConfigurationServiceGroup].[Id] = [ConfigurationServiceGroup_Tag].[ConfigurationServiceGroupId] WHERE [ConfigurationServiceGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmd.AddParameter("@TagId", varId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			ConfigurationServiceGroupCollection coll = new ConfigurationServiceGroupCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveConfigurationServiceGroupMap(int varId, ConfigurationServiceGroupCollection items)
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [ConfigurationServiceGroup_Tag] WHERE [ConfigurationServiceGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (ConfigurationServiceGroup item in items)
			{
				ConfigurationServiceGroupTag varConfigurationServiceGroupTag = new ConfigurationServiceGroupTag(true);
				varConfigurationServiceGroupTag.SetColumnValue("TagId", varId);
				varConfigurationServiceGroupTag.SetColumnValue("ConfigurationServiceGroupId", item.GetPrimaryKeyValue());
				varConfigurationServiceGroupTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveConfigurationServiceGroupMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [ConfigurationServiceGroup_Tag] WHERE [ConfigurationServiceGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					ConfigurationServiceGroupTag varConfigurationServiceGroupTag = new ConfigurationServiceGroupTag(true);
					varConfigurationServiceGroupTag.SetColumnValue("TagId", varId);
					varConfigurationServiceGroupTag.SetColumnValue("ConfigurationServiceGroupId", l.Value);
					varConfigurationServiceGroupTag.Save();
				}
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveConfigurationServiceGroupMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [ConfigurationServiceGroup_Tag] WHERE [ConfigurationServiceGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				ConfigurationServiceGroupTag varConfigurationServiceGroupTag = new ConfigurationServiceGroupTag(true);
				varConfigurationServiceGroupTag.SetColumnValue("TagId", varId);
				varConfigurationServiceGroupTag.SetColumnValue("ConfigurationServiceGroupId", item);
				varConfigurationServiceGroupTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void DeleteConfigurationServiceGroupMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [ConfigurationServiceGroup_Tag] WHERE [ConfigurationServiceGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.ProxyURLCollection GetProxyURLCollection() { return Tag.GetProxyURLCollection(this.Id); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static HP.ElementsCPS.Data.SubSonicClient.ProxyURLCollection GetProxyURLCollection(int varId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[ProxyURL] INNER JOIN [ProxyURL_Tag] ON [ProxyURL].[Id] = [ProxyURL_Tag].[ProxyURLId] WHERE [ProxyURL_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmd.AddParameter("@TagId", varId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			ProxyURLCollection coll = new ProxyURLCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveProxyURLMap(int varId, ProxyURLCollection items)
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [ProxyURL_Tag] WHERE [ProxyURL_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (ProxyURL item in items)
			{
				ProxyURLTag varProxyURLTag = new ProxyURLTag(true);
				varProxyURLTag.SetColumnValue("TagId", varId);
				varProxyURLTag.SetColumnValue("ProxyURLId", item.GetPrimaryKeyValue());
				varProxyURLTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveProxyURLMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [ProxyURL_Tag] WHERE [ProxyURL_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					ProxyURLTag varProxyURLTag = new ProxyURLTag(true);
					varProxyURLTag.SetColumnValue("TagId", varId);
					varProxyURLTag.SetColumnValue("ProxyURLId", l.Value);
					varProxyURLTag.Save();
				}
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveProxyURLMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [ProxyURL_Tag] WHERE [ProxyURL_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				ProxyURLTag varProxyURLTag = new ProxyURLTag(true);
				varProxyURLTag.SetColumnValue("TagId", varId);
				varProxyURLTag.SetColumnValue("ProxyURLId", item);
				varProxyURLTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void DeleteProxyURLMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [ProxyURL_Tag] WHERE [ProxyURL_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public HP.ElementsCPS.Data.SubSonicClient.JumpstationGroupCollection GetJumpstationGroupCollection() { return Tag.GetJumpstationGroupCollection(this.Id); }
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static HP.ElementsCPS.Data.SubSonicClient.JumpstationGroupCollection GetJumpstationGroupCollection(int varId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[JumpstationGroup] INNER JOIN [JumpstationGroup_Tag] ON [JumpstationGroup].[Id] = [JumpstationGroup_Tag].[JumpstationGroupId] WHERE [JumpstationGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmd.AddParameter("@TagId", varId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			JumpstationGroupCollection coll = new JumpstationGroupCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveJumpstationGroupMap(int varId, JumpstationGroupCollection items)
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [JumpstationGroup_Tag] WHERE [JumpstationGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (JumpstationGroup item in items)
			{
				JumpstationGroupTag varJumpstationGroupTag = new JumpstationGroupTag(true);
				varJumpstationGroupTag.SetColumnValue("TagId", varId);
				varJumpstationGroupTag.SetColumnValue("JumpstationGroupId", item.GetPrimaryKeyValue());
				varJumpstationGroupTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveJumpstationGroupMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [JumpstationGroup_Tag] WHERE [JumpstationGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					JumpstationGroupTag varJumpstationGroupTag = new JumpstationGroupTag(true);
					varJumpstationGroupTag.SetColumnValue("TagId", varId);
					varJumpstationGroupTag.SetColumnValue("JumpstationGroupId", l.Value);
					varJumpstationGroupTag.Save();
				}
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void SaveJumpstationGroupMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [JumpstationGroup_Tag] WHERE [JumpstationGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				JumpstationGroupTag varJumpstationGroupTag = new JumpstationGroupTag(true);
				varJumpstationGroupTag.SetColumnValue("TagId", varId);
				varJumpstationGroupTag.SetColumnValue("JumpstationGroupId", item);
				varJumpstationGroupTag.Save();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static void DeleteJumpstationGroupMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [JumpstationGroup_Tag] WHERE [JumpstationGroup_Tag].[TagId] = @TagId", Tag.Schema.Provider.Name);
			cmdDel.AddParameter("@TagId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		#endregion
		
        
        
		#region Equality Methods
		
		// /// <summary>
		// /// Generates instance hashcodes based upon the instance's primary key value.
		// /// </summary>
		// /// <returns>A hash code for the current <see cref="Tag"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("Tag#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="Tag"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Tag"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="Tag"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			Tag instance1 = this;
			Tag instance2 = obj as Tag;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.Notes == instance2.Notes)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="Tag"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="Tag"/> to compare.</param>
		/// <param name="instance2">The second <see cref="Tag"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(Tag instance1, Tag instance2)
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
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[2]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[3]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[4]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[5]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn RowStatusIdColumn
		{
			get { return Schema.Columns[6]; }
		}
        
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static TableSchema.TableColumn NotesColumn
		{
			get { return Schema.Columns[7]; }
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
			 public static string Notes = @"Notes";
			
		}
		#endregion
		#region Update PK Collections
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public void SetPKValues()
		{
}
		#endregion
		#region Deep Save
	//NOTE: Code Generation: Generation disabled: DeepSave method
		#endregion
	}
}
