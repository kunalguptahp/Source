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
	/// Strongly-typed collection for the VwMapJumpstationGroup class.
	/// </summary>
	[Serializable]
	public partial class VwMapJumpstationGroupCollection : ReadOnlyList<VwMapJumpstationGroup, VwMapJumpstationGroupCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapJumpstationGroupCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapJumpstationGroup);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapJumpstationGroup class.
	/// </summary>
	[Serializable]
	public partial class VwMapJumpstationGroupController : BaseReadOnlyRecordController<VwMapJumpstationGroup, VwMapJumpstationGroupCollection, VwMapJumpstationGroupController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapJumpstationGroupController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapJumpstationGroup.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapJumpstationGroup view.
	/// </summary>
	[Serializable]
	public partial class VwMapJumpstationGroup : ReadOnlyRecord<VwMapJumpstationGroup>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapJumpstationGroup", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarAppClientId = new TableSchema.TableColumn(schema);
				colvarAppClientId.ColumnName = "AppClientId";
				colvarAppClientId.DataType = DbType.Int32;
				colvarAppClientId.MaxLength = 0;
				colvarAppClientId.AutoIncrement = false;
				colvarAppClientId.IsNullable = false;
				colvarAppClientId.IsPrimaryKey = false;
				colvarAppClientId.IsForeignKey = false;
				colvarAppClientId.IsReadOnly = false;
				
				schema.Columns.Add(colvarAppClientId);
				
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
				
				TableSchema.TableColumn colvarJumpstationGroupStatusName = new TableSchema.TableColumn(schema);
				colvarJumpstationGroupStatusName.ColumnName = "JumpstationGroupStatusName";
				colvarJumpstationGroupStatusName.DataType = DbType.String;
				colvarJumpstationGroupStatusName.MaxLength = 256;
				colvarJumpstationGroupStatusName.AutoIncrement = false;
				colvarJumpstationGroupStatusName.IsNullable = false;
				colvarJumpstationGroupStatusName.IsPrimaryKey = false;
				colvarJumpstationGroupStatusName.IsForeignKey = false;
				colvarJumpstationGroupStatusName.IsReadOnly = false;
				
				schema.Columns.Add(colvarJumpstationGroupStatusName);
				
				TableSchema.TableColumn colvarPersonName = new TableSchema.TableColumn(schema);
				colvarPersonName.ColumnName = "PersonName";
				colvarPersonName.DataType = DbType.String;
				colvarPersonName.MaxLength = 326;
				colvarPersonName.AutoIncrement = false;
				colvarPersonName.IsNullable = false;
				colvarPersonName.IsPrimaryKey = false;
				colvarPersonName.IsForeignKey = false;
				colvarPersonName.IsReadOnly = false;
				
				schema.Columns.Add(colvarPersonName);
				
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
				
				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				
				schema.Columns.Add(colvarDescription);
				
				TableSchema.TableColumn colvarJumpstationGroupStatusId = new TableSchema.TableColumn(schema);
				colvarJumpstationGroupStatusId.ColumnName = "JumpstationGroupStatusId";
				colvarJumpstationGroupStatusId.DataType = DbType.Int32;
				colvarJumpstationGroupStatusId.MaxLength = 0;
				colvarJumpstationGroupStatusId.AutoIncrement = false;
				colvarJumpstationGroupStatusId.IsNullable = false;
				colvarJumpstationGroupStatusId.IsPrimaryKey = false;
				colvarJumpstationGroupStatusId.IsForeignKey = false;
				colvarJumpstationGroupStatusId.IsReadOnly = false;
				
				schema.Columns.Add(colvarJumpstationGroupStatusId);
				
				TableSchema.TableColumn colvarJumpstationGroupTypeId = new TableSchema.TableColumn(schema);
				colvarJumpstationGroupTypeId.ColumnName = "JumpstationGroupTypeId";
				colvarJumpstationGroupTypeId.DataType = DbType.Int32;
				colvarJumpstationGroupTypeId.MaxLength = 0;
				colvarJumpstationGroupTypeId.AutoIncrement = false;
				colvarJumpstationGroupTypeId.IsNullable = false;
				colvarJumpstationGroupTypeId.IsPrimaryKey = false;
				colvarJumpstationGroupTypeId.IsForeignKey = false;
				colvarJumpstationGroupTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarJumpstationGroupTypeId);
				
				TableSchema.TableColumn colvarOwnerId = new TableSchema.TableColumn(schema);
				colvarOwnerId.ColumnName = "OwnerId";
				colvarOwnerId.DataType = DbType.Int32;
				colvarOwnerId.MaxLength = 0;
				colvarOwnerId.AutoIncrement = false;
				colvarOwnerId.IsNullable = false;
				colvarOwnerId.IsPrimaryKey = false;
				colvarOwnerId.IsForeignKey = false;
				colvarOwnerId.IsReadOnly = false;
				
				schema.Columns.Add(colvarOwnerId);
				
				TableSchema.TableColumn colvarJumpstationGroupTypeName = new TableSchema.TableColumn(schema);
				colvarJumpstationGroupTypeName.ColumnName = "JumpstationGroupTypeName";
				colvarJumpstationGroupTypeName.DataType = DbType.String;
				colvarJumpstationGroupTypeName.MaxLength = 256;
				colvarJumpstationGroupTypeName.AutoIncrement = false;
				colvarJumpstationGroupTypeName.IsNullable = false;
				colvarJumpstationGroupTypeName.IsPrimaryKey = false;
				colvarJumpstationGroupTypeName.IsForeignKey = false;
				colvarJumpstationGroupTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarJumpstationGroupTypeName);
				
				TableSchema.TableColumn colvarBrandQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarBrandQueryParameterValue.ColumnName = "BrandQueryParameterValue";
				colvarBrandQueryParameterValue.DataType = DbType.String;
				colvarBrandQueryParameterValue.MaxLength = 256;
				colvarBrandQueryParameterValue.AutoIncrement = false;
				colvarBrandQueryParameterValue.IsNullable = true;
				colvarBrandQueryParameterValue.IsPrimaryKey = false;
				colvarBrandQueryParameterValue.IsForeignKey = false;
				colvarBrandQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarBrandQueryParameterValue);
				
				TableSchema.TableColumn colvarCycleQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarCycleQueryParameterValue.ColumnName = "CycleQueryParameterValue";
				colvarCycleQueryParameterValue.DataType = DbType.String;
				colvarCycleQueryParameterValue.MaxLength = 256;
				colvarCycleQueryParameterValue.AutoIncrement = false;
				colvarCycleQueryParameterValue.IsNullable = true;
				colvarCycleQueryParameterValue.IsPrimaryKey = false;
				colvarCycleQueryParameterValue.IsForeignKey = false;
				colvarCycleQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarCycleQueryParameterValue);
				
				TableSchema.TableColumn colvarLocaleQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarLocaleQueryParameterValue.ColumnName = "LocaleQueryParameterValue";
				colvarLocaleQueryParameterValue.DataType = DbType.String;
				colvarLocaleQueryParameterValue.MaxLength = 256;
				colvarLocaleQueryParameterValue.AutoIncrement = false;
				colvarLocaleQueryParameterValue.IsNullable = true;
				colvarLocaleQueryParameterValue.IsPrimaryKey = false;
				colvarLocaleQueryParameterValue.IsForeignKey = false;
				colvarLocaleQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarLocaleQueryParameterValue);
				
				TableSchema.TableColumn colvarTouchpointQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarTouchpointQueryParameterValue.ColumnName = "TouchpointQueryParameterValue";
				colvarTouchpointQueryParameterValue.DataType = DbType.String;
				colvarTouchpointQueryParameterValue.MaxLength = 256;
				colvarTouchpointQueryParameterValue.AutoIncrement = false;
				colvarTouchpointQueryParameterValue.IsNullable = true;
				colvarTouchpointQueryParameterValue.IsPrimaryKey = false;
				colvarTouchpointQueryParameterValue.IsForeignKey = false;
				colvarTouchpointQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarTouchpointQueryParameterValue);
				
				TableSchema.TableColumn colvarPartnerCategoryQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarPartnerCategoryQueryParameterValue.ColumnName = "PartnerCategoryQueryParameterValue";
				colvarPartnerCategoryQueryParameterValue.DataType = DbType.String;
				colvarPartnerCategoryQueryParameterValue.MaxLength = 256;
				colvarPartnerCategoryQueryParameterValue.AutoIncrement = false;
				colvarPartnerCategoryQueryParameterValue.IsNullable = true;
				colvarPartnerCategoryQueryParameterValue.IsPrimaryKey = false;
				colvarPartnerCategoryQueryParameterValue.IsForeignKey = false;
				colvarPartnerCategoryQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarPartnerCategoryQueryParameterValue);
				
				TableSchema.TableColumn colvarPlatformQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarPlatformQueryParameterValue.ColumnName = "PlatformQueryParameterValue";
				colvarPlatformQueryParameterValue.DataType = DbType.String;
				colvarPlatformQueryParameterValue.MaxLength = 256;
				colvarPlatformQueryParameterValue.AutoIncrement = false;
				colvarPlatformQueryParameterValue.IsNullable = true;
				colvarPlatformQueryParameterValue.IsPrimaryKey = false;
				colvarPlatformQueryParameterValue.IsForeignKey = false;
				colvarPlatformQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarPlatformQueryParameterValue);
				
				TableSchema.TableColumn colvarTags = new TableSchema.TableColumn(schema);
				colvarTags.ColumnName = "Tags";
				colvarTags.DataType = DbType.String;
				colvarTags.MaxLength = -1;
				colvarTags.AutoIncrement = false;
				colvarTags.IsNullable = true;
				colvarTags.IsPrimaryKey = false;
				colvarTags.IsForeignKey = false;
				colvarTags.IsReadOnly = false;
				
				schema.Columns.Add(colvarTags);
				
				TableSchema.TableColumn colvarQueryString = new TableSchema.TableColumn(schema);
				colvarQueryString.ColumnName = "QueryString";
				colvarQueryString.DataType = DbType.AnsiString;
				colvarQueryString.MaxLength = 1024;
				colvarQueryString.AutoIncrement = false;
				colvarQueryString.IsNullable = true;
				colvarQueryString.IsPrimaryKey = false;
				colvarQueryString.IsForeignKey = false;
				colvarQueryString.IsReadOnly = false;
				
				schema.Columns.Add(colvarQueryString);
				
				TableSchema.TableColumn colvarTagCount = new TableSchema.TableColumn(schema);
				colvarTagCount.ColumnName = "TagCount";
				colvarTagCount.DataType = DbType.Int32;
				colvarTagCount.MaxLength = 0;
				colvarTagCount.AutoIncrement = false;
				colvarTagCount.IsNullable = true;
				colvarTagCount.IsPrimaryKey = false;
				colvarTagCount.IsForeignKey = false;
				colvarTagCount.IsReadOnly = false;
				
				schema.Columns.Add(colvarTagCount);
				
				TableSchema.TableColumn colvarProductionId = new TableSchema.TableColumn(schema);
				colvarProductionId.ColumnName = "ProductionId";
				colvarProductionId.DataType = DbType.Int32;
				colvarProductionId.MaxLength = 0;
				colvarProductionId.AutoIncrement = false;
				colvarProductionId.IsNullable = true;
				colvarProductionId.IsPrimaryKey = false;
				colvarProductionId.IsForeignKey = false;
				colvarProductionId.IsReadOnly = false;
				
				schema.Columns.Add(colvarProductionId);
				
				TableSchema.TableColumn colvarValidationId = new TableSchema.TableColumn(schema);
				colvarValidationId.ColumnName = "ValidationId";
				colvarValidationId.DataType = DbType.Int32;
				colvarValidationId.MaxLength = 0;
				colvarValidationId.AutoIncrement = false;
				colvarValidationId.IsNullable = true;
				colvarValidationId.IsPrimaryKey = false;
				colvarValidationId.IsForeignKey = false;
				colvarValidationId.IsReadOnly = false;
				
				schema.Columns.Add(colvarValidationId);
				
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
				
				TableSchema.TableColumn colvarJumpstationApplicationId = new TableSchema.TableColumn(schema);
				colvarJumpstationApplicationId.ColumnName = "JumpstationApplicationId";
				colvarJumpstationApplicationId.DataType = DbType.Int32;
				colvarJumpstationApplicationId.MaxLength = 0;
				colvarJumpstationApplicationId.AutoIncrement = false;
				colvarJumpstationApplicationId.IsNullable = false;
				colvarJumpstationApplicationId.IsPrimaryKey = false;
				colvarJumpstationApplicationId.IsForeignKey = false;
				colvarJumpstationApplicationId.IsReadOnly = false;
				
				schema.Columns.Add(colvarJumpstationApplicationId);
				
				TableSchema.TableColumn colvarJumpstationApplicationName = new TableSchema.TableColumn(schema);
				colvarJumpstationApplicationName.ColumnName = "JumpstationApplicationName";
				colvarJumpstationApplicationName.DataType = DbType.String;
				colvarJumpstationApplicationName.MaxLength = 256;
				colvarJumpstationApplicationName.AutoIncrement = false;
				colvarJumpstationApplicationName.IsNullable = false;
				colvarJumpstationApplicationName.IsPrimaryKey = false;
				colvarJumpstationApplicationName.IsForeignKey = false;
				colvarJumpstationApplicationName.IsReadOnly = false;
				
				schema.Columns.Add(colvarJumpstationApplicationName);
				
				TableSchema.TableColumn colvarTargetURL = new TableSchema.TableColumn(schema);
				colvarTargetURL.ColumnName = "TargetURL";
				colvarTargetURL.DataType = DbType.String;
				colvarTargetURL.MaxLength = -1;
				colvarTargetURL.AutoIncrement = false;
				colvarTargetURL.IsNullable = true;
				colvarTargetURL.IsPrimaryKey = false;
				colvarTargetURL.IsForeignKey = false;
				colvarTargetURL.IsReadOnly = false;
				
				schema.Columns.Add(colvarTargetURL);
				
				TableSchema.TableColumn colvarOrder = new TableSchema.TableColumn(schema);
				colvarOrder.ColumnName = "Order";
				colvarOrder.DataType = DbType.Int32;
				colvarOrder.MaxLength = 0;
				colvarOrder.AutoIncrement = false;
				colvarOrder.IsNullable = true;
				colvarOrder.IsPrimaryKey = false;
				colvarOrder.IsForeignKey = false;
				colvarOrder.IsReadOnly = false;
				
				schema.Columns.Add(colvarOrder);
				
				TableSchema.TableColumn colvarPublicationDomain = new TableSchema.TableColumn(schema);
				colvarPublicationDomain.ColumnName = "PublicationDomain";
				colvarPublicationDomain.DataType = DbType.String;
				colvarPublicationDomain.MaxLength = 256;
				colvarPublicationDomain.AutoIncrement = false;
				colvarPublicationDomain.IsNullable = false;
				colvarPublicationDomain.IsPrimaryKey = false;
				colvarPublicationDomain.IsForeignKey = false;
				colvarPublicationDomain.IsReadOnly = false;
				
				schema.Columns.Add(colvarPublicationDomain);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapJumpstationGroup",schema);
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
		protected VwMapJumpstationGroup(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapJumpstationGroup() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapJumpstationGroup(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapJumpstationGroup(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapJumpstationGroup(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapJumpstationGroup"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapJumpstationGroup(VwMapJumpstationGroup original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapJumpstationGroup original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.AppClientId = original.AppClientId;
			
			this.RowStatusName = original.RowStatusName;
			
			this.JumpstationGroupStatusName = original.JumpstationGroupStatusName;
			
			this.PersonName = original.PersonName;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.Description = original.Description;
			
			this.JumpstationGroupStatusId = original.JumpstationGroupStatusId;
			
			this.JumpstationGroupTypeId = original.JumpstationGroupTypeId;
			
			this.OwnerId = original.OwnerId;
			
			this.JumpstationGroupTypeName = original.JumpstationGroupTypeName;
			
			this.BrandQueryParameterValue = original.BrandQueryParameterValue;
			
			this.CycleQueryParameterValue = original.CycleQueryParameterValue;
			
			this.LocaleQueryParameterValue = original.LocaleQueryParameterValue;
			
			this.TouchpointQueryParameterValue = original.TouchpointQueryParameterValue;
			
			this.PartnerCategoryQueryParameterValue = original.PartnerCategoryQueryParameterValue;
			
			this.PlatformQueryParameterValue = original.PlatformQueryParameterValue;
			
			this.Tags = original.Tags;
			
			this.QueryString = original.QueryString;
			
			this.TagCount = original.TagCount;
			
			this.ProductionId = original.ProductionId;
			
			this.ValidationId = original.ValidationId;
			
			this.Name = original.Name;
			
			this.JumpstationApplicationId = original.JumpstationApplicationId;
			
			this.JumpstationApplicationName = original.JumpstationApplicationName;
			
			this.TargetURL = original.TargetURL;
			
			this.Order = original.Order;
			
			this.PublicationDomain = original.PublicationDomain;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapJumpstationGroup"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapJumpstationGroup Copy(VwMapJumpstationGroup original)
		{
			return new VwMapJumpstationGroup(original);
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
		partial void OnAppClientIdChanging(int newValue);
		partial void OnAppClientIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("AppClientId")]
		[Bindable(true)]
		public int AppClientId 
		{
			get
			{
				return GetColumnValue<int>("AppClientId");
			}
			set
			{
				this.OnAppClientIdChanging(value);
				this.OnPropertyChanging("AppClientId", value);
				int oldValue = this.AppClientId;
				SetColumnValue("AppClientId", value);
				this.OnAppClientIdChanged(oldValue, value);
				this.OnPropertyChanged("AppClientId", oldValue, value);
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
		partial void OnJumpstationGroupStatusNameChanging(string newValue);
		partial void OnJumpstationGroupStatusNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationGroupStatusName")]
		[Bindable(true)]
		public string JumpstationGroupStatusName 
		{
			get
			{
				return GetColumnValue<string>("JumpstationGroupStatusName");
			}
			set
			{
				this.OnJumpstationGroupStatusNameChanging(value);
				this.OnPropertyChanging("JumpstationGroupStatusName", value);
				string oldValue = this.JumpstationGroupStatusName;
				SetColumnValue("JumpstationGroupStatusName", value);
				this.OnJumpstationGroupStatusNameChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationGroupStatusName", oldValue, value);
			}
		}
		partial void OnPersonNameChanging(string newValue);
		partial void OnPersonNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PersonName")]
		[Bindable(true)]
		public string PersonName 
		{
			get
			{
				return GetColumnValue<string>("PersonName");
			}
			set
			{
				this.OnPersonNameChanging(value);
				this.OnPropertyChanging("PersonName", value);
				string oldValue = this.PersonName;
				SetColumnValue("PersonName", value);
				this.OnPersonNameChanged(oldValue, value);
				this.OnPropertyChanged("PersonName", oldValue, value);
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
		partial void OnJumpstationGroupStatusIdChanging(int newValue);
		partial void OnJumpstationGroupStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationGroupStatusId")]
		[Bindable(true)]
		public int JumpstationGroupStatusId 
		{
			get
			{
				return GetColumnValue<int>("JumpstationGroupStatusId");
			}
			set
			{
				this.OnJumpstationGroupStatusIdChanging(value);
				this.OnPropertyChanging("JumpstationGroupStatusId", value);
				int oldValue = this.JumpstationGroupStatusId;
				SetColumnValue("JumpstationGroupStatusId", value);
				this.OnJumpstationGroupStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationGroupStatusId", oldValue, value);
			}
		}
		partial void OnJumpstationGroupTypeIdChanging(int newValue);
		partial void OnJumpstationGroupTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationGroupTypeId")]
		[Bindable(true)]
		public int JumpstationGroupTypeId 
		{
			get
			{
				return GetColumnValue<int>("JumpstationGroupTypeId");
			}
			set
			{
				this.OnJumpstationGroupTypeIdChanging(value);
				this.OnPropertyChanging("JumpstationGroupTypeId", value);
				int oldValue = this.JumpstationGroupTypeId;
				SetColumnValue("JumpstationGroupTypeId", value);
				this.OnJumpstationGroupTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationGroupTypeId", oldValue, value);
			}
		}
		partial void OnOwnerIdChanging(int newValue);
		partial void OnOwnerIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("OwnerId")]
		[Bindable(true)]
		public int OwnerId 
		{
			get
			{
				return GetColumnValue<int>("OwnerId");
			}
			set
			{
				this.OnOwnerIdChanging(value);
				this.OnPropertyChanging("OwnerId", value);
				int oldValue = this.OwnerId;
				SetColumnValue("OwnerId", value);
				this.OnOwnerIdChanged(oldValue, value);
				this.OnPropertyChanged("OwnerId", oldValue, value);
			}
		}
		partial void OnJumpstationGroupTypeNameChanging(string newValue);
		partial void OnJumpstationGroupTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationGroupTypeName")]
		[Bindable(true)]
		public string JumpstationGroupTypeName 
		{
			get
			{
				return GetColumnValue<string>("JumpstationGroupTypeName");
			}
			set
			{
				this.OnJumpstationGroupTypeNameChanging(value);
				this.OnPropertyChanging("JumpstationGroupTypeName", value);
				string oldValue = this.JumpstationGroupTypeName;
				SetColumnValue("JumpstationGroupTypeName", value);
				this.OnJumpstationGroupTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationGroupTypeName", oldValue, value);
			}
		}
		partial void OnBrandQueryParameterValueChanging(string newValue);
		partial void OnBrandQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("BrandQueryParameterValue")]
		[Bindable(true)]
		public string BrandQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("BrandQueryParameterValue");
			}
			set
			{
				this.OnBrandQueryParameterValueChanging(value);
				this.OnPropertyChanging("BrandQueryParameterValue", value);
				string oldValue = this.BrandQueryParameterValue;
				SetColumnValue("BrandQueryParameterValue", value);
				this.OnBrandQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("BrandQueryParameterValue", oldValue, value);
			}
		}
		partial void OnCycleQueryParameterValueChanging(string newValue);
		partial void OnCycleQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CycleQueryParameterValue")]
		[Bindable(true)]
		public string CycleQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("CycleQueryParameterValue");
			}
			set
			{
				this.OnCycleQueryParameterValueChanging(value);
				this.OnPropertyChanging("CycleQueryParameterValue", value);
				string oldValue = this.CycleQueryParameterValue;
				SetColumnValue("CycleQueryParameterValue", value);
				this.OnCycleQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("CycleQueryParameterValue", oldValue, value);
			}
		}
		partial void OnLocaleQueryParameterValueChanging(string newValue);
		partial void OnLocaleQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("LocaleQueryParameterValue")]
		[Bindable(true)]
		public string LocaleQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("LocaleQueryParameterValue");
			}
			set
			{
				this.OnLocaleQueryParameterValueChanging(value);
				this.OnPropertyChanging("LocaleQueryParameterValue", value);
				string oldValue = this.LocaleQueryParameterValue;
				SetColumnValue("LocaleQueryParameterValue", value);
				this.OnLocaleQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("LocaleQueryParameterValue", oldValue, value);
			}
		}
		partial void OnTouchpointQueryParameterValueChanging(string newValue);
		partial void OnTouchpointQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TouchpointQueryParameterValue")]
		[Bindable(true)]
		public string TouchpointQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("TouchpointQueryParameterValue");
			}
			set
			{
				this.OnTouchpointQueryParameterValueChanging(value);
				this.OnPropertyChanging("TouchpointQueryParameterValue", value);
				string oldValue = this.TouchpointQueryParameterValue;
				SetColumnValue("TouchpointQueryParameterValue", value);
				this.OnTouchpointQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("TouchpointQueryParameterValue", oldValue, value);
			}
		}
		partial void OnPartnerCategoryQueryParameterValueChanging(string newValue);
		partial void OnPartnerCategoryQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PartnerCategoryQueryParameterValue")]
		[Bindable(true)]
		public string PartnerCategoryQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("PartnerCategoryQueryParameterValue");
			}
			set
			{
				this.OnPartnerCategoryQueryParameterValueChanging(value);
				this.OnPropertyChanging("PartnerCategoryQueryParameterValue", value);
				string oldValue = this.PartnerCategoryQueryParameterValue;
				SetColumnValue("PartnerCategoryQueryParameterValue", value);
				this.OnPartnerCategoryQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("PartnerCategoryQueryParameterValue", oldValue, value);
			}
		}
		partial void OnPlatformQueryParameterValueChanging(string newValue);
		partial void OnPlatformQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PlatformQueryParameterValue")]
		[Bindable(true)]
		public string PlatformQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("PlatformQueryParameterValue");
			}
			set
			{
				this.OnPlatformQueryParameterValueChanging(value);
				this.OnPropertyChanging("PlatformQueryParameterValue", value);
				string oldValue = this.PlatformQueryParameterValue;
				SetColumnValue("PlatformQueryParameterValue", value);
				this.OnPlatformQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("PlatformQueryParameterValue", oldValue, value);
			}
		}
		partial void OnTagsChanging(string newValue);
		partial void OnTagsChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Tags")]
		[Bindable(true)]
		public string Tags 
		{
			get
			{
				return GetColumnValue<string>("Tags");
			}
			set
			{
				this.OnTagsChanging(value);
				this.OnPropertyChanging("Tags", value);
				string oldValue = this.Tags;
				SetColumnValue("Tags", value);
				this.OnTagsChanged(oldValue, value);
				this.OnPropertyChanged("Tags", oldValue, value);
			}
		}
		partial void OnQueryStringChanging(string newValue);
		partial void OnQueryStringChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("QueryString")]
		[Bindable(true)]
		public string QueryString 
		{
			get
			{
				return GetColumnValue<string>("QueryString");
			}
			set
			{
				this.OnQueryStringChanging(value);
				this.OnPropertyChanging("QueryString", value);
				string oldValue = this.QueryString;
				SetColumnValue("QueryString", value);
				this.OnQueryStringChanged(oldValue, value);
				this.OnPropertyChanged("QueryString", oldValue, value);
			}
		}
		partial void OnTagCountChanging(int? newValue);
		partial void OnTagCountChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TagCount")]
		[Bindable(true)]
		public int? TagCount 
		{
			get
			{
				return GetColumnValue<int?>("TagCount");
			}
			set
			{
				this.OnTagCountChanging(value);
				this.OnPropertyChanging("TagCount", value);
				int? oldValue = this.TagCount;
				SetColumnValue("TagCount", value);
				this.OnTagCountChanged(oldValue, value);
				this.OnPropertyChanged("TagCount", oldValue, value);
			}
		}
		partial void OnProductionIdChanging(int? newValue);
		partial void OnProductionIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ProductionId")]
		[Bindable(true)]
		public int? ProductionId 
		{
			get
			{
				return GetColumnValue<int?>("ProductionId");
			}
			set
			{
				this.OnProductionIdChanging(value);
				this.OnPropertyChanging("ProductionId", value);
				int? oldValue = this.ProductionId;
				SetColumnValue("ProductionId", value);
				this.OnProductionIdChanged(oldValue, value);
				this.OnPropertyChanged("ProductionId", oldValue, value);
			}
		}
		partial void OnValidationIdChanging(int? newValue);
		partial void OnValidationIdChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ValidationId")]
		[Bindable(true)]
		public int? ValidationId 
		{
			get
			{
				return GetColumnValue<int?>("ValidationId");
			}
			set
			{
				this.OnValidationIdChanging(value);
				this.OnPropertyChanging("ValidationId", value);
				int? oldValue = this.ValidationId;
				SetColumnValue("ValidationId", value);
				this.OnValidationIdChanged(oldValue, value);
				this.OnPropertyChanged("ValidationId", oldValue, value);
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
		partial void OnJumpstationApplicationIdChanging(int newValue);
		partial void OnJumpstationApplicationIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationApplicationId")]
		[Bindable(true)]
		public int JumpstationApplicationId 
		{
			get
			{
				return GetColumnValue<int>("JumpstationApplicationId");
			}
			set
			{
				this.OnJumpstationApplicationIdChanging(value);
				this.OnPropertyChanging("JumpstationApplicationId", value);
				int oldValue = this.JumpstationApplicationId;
				SetColumnValue("JumpstationApplicationId", value);
				this.OnJumpstationApplicationIdChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationApplicationId", oldValue, value);
			}
		}
		partial void OnJumpstationApplicationNameChanging(string newValue);
		partial void OnJumpstationApplicationNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("JumpstationApplicationName")]
		[Bindable(true)]
		public string JumpstationApplicationName 
		{
			get
			{
				return GetColumnValue<string>("JumpstationApplicationName");
			}
			set
			{
				this.OnJumpstationApplicationNameChanging(value);
				this.OnPropertyChanging("JumpstationApplicationName", value);
				string oldValue = this.JumpstationApplicationName;
				SetColumnValue("JumpstationApplicationName", value);
				this.OnJumpstationApplicationNameChanged(oldValue, value);
				this.OnPropertyChanged("JumpstationApplicationName", oldValue, value);
			}
		}
		partial void OnTargetURLChanging(string newValue);
		partial void OnTargetURLChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("TargetURL")]
		[Bindable(true)]
		public string TargetURL 
		{
			get
			{
				return GetColumnValue<string>("TargetURL");
			}
			set
			{
				this.OnTargetURLChanging(value);
				this.OnPropertyChanging("TargetURL", value);
				string oldValue = this.TargetURL;
				SetColumnValue("TargetURL", value);
				this.OnTargetURLChanged(oldValue, value);
				this.OnPropertyChanged("TargetURL", oldValue, value);
			}
		}
		partial void OnOrderChanging(int? newValue);
		partial void OnOrderChanged(int? oldValue, int? newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("Order")]
		[Bindable(true)]
		public int? Order 
		{
			get
			{
				return GetColumnValue<int?>("Order");
			}
			set
			{
				this.OnOrderChanging(value);
				this.OnPropertyChanging("Order", value);
				int? oldValue = this.Order;
				SetColumnValue("Order", value);
				this.OnOrderChanged(oldValue, value);
				this.OnPropertyChanged("Order", oldValue, value);
			}
		}
		partial void OnPublicationDomainChanging(string newValue);
		partial void OnPublicationDomainChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PublicationDomain")]
		[Bindable(true)]
		public string PublicationDomain 
		{
			get
			{
				return GetColumnValue<string>("PublicationDomain");
			}
			set
			{
				this.OnPublicationDomainChanging(value);
				this.OnPropertyChanging("PublicationDomain", value);
				string oldValue = this.PublicationDomain;
				SetColumnValue("PublicationDomain", value);
				this.OnPublicationDomainChanged(oldValue, value);
				this.OnPropertyChanged("PublicationDomain", oldValue, value);
			}
		}
		#endregion
		#region Columns Struct
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public struct Columns
		{
			
			public static string Id = @"Id";
			
			public static string AppClientId = @"AppClientId";
			
			public static string RowStatusName = @"RowStatusName";
			
			public static string JumpstationGroupStatusName = @"JumpstationGroupStatusName";
			
			public static string PersonName = @"PersonName";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string Description = @"Description";
			
			public static string JumpstationGroupStatusId = @"JumpstationGroupStatusId";
			
			public static string JumpstationGroupTypeId = @"JumpstationGroupTypeId";
			
			public static string OwnerId = @"OwnerId";
			
			public static string JumpstationGroupTypeName = @"JumpstationGroupTypeName";
			
			public static string BrandQueryParameterValue = @"BrandQueryParameterValue";
			
			public static string CycleQueryParameterValue = @"CycleQueryParameterValue";
			
			public static string LocaleQueryParameterValue = @"LocaleQueryParameterValue";
			
			public static string TouchpointQueryParameterValue = @"TouchpointQueryParameterValue";
			
			public static string PartnerCategoryQueryParameterValue = @"PartnerCategoryQueryParameterValue";
			
			public static string PlatformQueryParameterValue = @"PlatformQueryParameterValue";
			
			public static string Tags = @"Tags";
			
			public static string QueryString = @"QueryString";
			
			public static string TagCount = @"TagCount";
			
			public static string ProductionId = @"ProductionId";
			
			public static string ValidationId = @"ValidationId";
			
			public static string Name = @"Name";
			
			public static string JumpstationApplicationId = @"JumpstationApplicationId";
			
			public static string JumpstationApplicationName = @"JumpstationApplicationName";
			
			public static string TargetURL = @"TargetURL";
			
			public static string Order = @"Order";
			
			public static string PublicationDomain = @"PublicationDomain";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapJumpstationGroup"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapJumpstationGroup#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapJumpstationGroup"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapJumpstationGroup"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapJumpstationGroup"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapJumpstationGroup instance1 = this;
			VwMapJumpstationGroup instance2 = obj as VwMapJumpstationGroup;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.AppClientId == instance2.AppClientId)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.JumpstationGroupStatusName == instance2.JumpstationGroupStatusName)
			
				&& (instance1.PersonName == instance2.PersonName)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.JumpstationGroupStatusId == instance2.JumpstationGroupStatusId)
			
				&& (instance1.JumpstationGroupTypeId == instance2.JumpstationGroupTypeId)
			
				&& (instance1.OwnerId == instance2.OwnerId)
			
				&& (instance1.JumpstationGroupTypeName == instance2.JumpstationGroupTypeName)
			
				&& (instance1.BrandQueryParameterValue == instance2.BrandQueryParameterValue)
			
				&& (instance1.CycleQueryParameterValue == instance2.CycleQueryParameterValue)
			
				&& (instance1.LocaleQueryParameterValue == instance2.LocaleQueryParameterValue)
			
				&& (instance1.TouchpointQueryParameterValue == instance2.TouchpointQueryParameterValue)
			
				&& (instance1.PartnerCategoryQueryParameterValue == instance2.PartnerCategoryQueryParameterValue)
			
				&& (instance1.PlatformQueryParameterValue == instance2.PlatformQueryParameterValue)
			
				&& (instance1.Tags == instance2.Tags)
			
				&& (instance1.QueryString == instance2.QueryString)
			
				&& (instance1.TagCount == instance2.TagCount)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ValidationId == instance2.ValidationId)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.JumpstationApplicationId == instance2.JumpstationApplicationId)
			
				&& (instance1.JumpstationApplicationName == instance2.JumpstationApplicationName)
			
				&& (instance1.TargetURL == instance2.TargetURL)
			
				&& (instance1.Order == instance2.Order)
			
				&& (instance1.PublicationDomain == instance2.PublicationDomain)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapJumpstationGroup"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapJumpstationGroup"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapJumpstationGroup"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapJumpstationGroup instance1, VwMapJumpstationGroup instance2)
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
