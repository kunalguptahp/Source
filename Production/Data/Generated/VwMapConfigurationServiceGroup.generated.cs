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
	/// Strongly-typed collection for the VwMapConfigurationServiceGroup class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceGroupCollection : ReadOnlyList<VwMapConfigurationServiceGroup, VwMapConfigurationServiceGroupCollection>, IRecordCollection
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceGroupCollection() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public Type GetRecordType()
		{
			return typeof(VwMapConfigurationServiceGroup);
		}
	}
	/// <summary>
	/// Strongly-typed Controller class for the VwMapConfigurationServiceGroup class.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceGroupController : BaseReadOnlyRecordController<VwMapConfigurationServiceGroup, VwMapConfigurationServiceGroupCollection, VwMapConfigurationServiceGroupController>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceGroupController() {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public override TableSchema.Table GetRecordSchema()
		{
			return VwMapConfigurationServiceGroup.Schema;
		}
	}
	/// <summary>
	/// This is  Read-only wrapper class for the vwMapConfigurationServiceGroup view.
	/// </summary>
	[Serializable]
	public partial class VwMapConfigurationServiceGroup : ReadOnlyRecord<VwMapConfigurationServiceGroup>, IReadOnlyRecord, IRecord
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
				TableSchema.Table schema = new TableSchema.Table("vwMapConfigurationServiceGroup", TableType.View, DataService.GetInstance("ElementsCPSDB"));
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
				
				TableSchema.TableColumn colvarConfigurationServiceGroupStatusName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupStatusName.ColumnName = "ConfigurationServiceGroupStatusName";
				colvarConfigurationServiceGroupStatusName.DataType = DbType.String;
				colvarConfigurationServiceGroupStatusName.MaxLength = 256;
				colvarConfigurationServiceGroupStatusName.AutoIncrement = false;
				colvarConfigurationServiceGroupStatusName.IsNullable = false;
				colvarConfigurationServiceGroupStatusName.IsPrimaryKey = false;
				colvarConfigurationServiceGroupStatusName.IsForeignKey = false;
				colvarConfigurationServiceGroupStatusName.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupStatusName);
				
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
				
				TableSchema.TableColumn colvarConfigurationServiceGroupStatusId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupStatusId.ColumnName = "ConfigurationServiceGroupStatusId";
				colvarConfigurationServiceGroupStatusId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupStatusId.MaxLength = 0;
				colvarConfigurationServiceGroupStatusId.AutoIncrement = false;
				colvarConfigurationServiceGroupStatusId.IsNullable = false;
				colvarConfigurationServiceGroupStatusId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupStatusId.IsForeignKey = false;
				colvarConfigurationServiceGroupStatusId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupStatusId);
				
				TableSchema.TableColumn colvarConfigurationServiceGroupTypeId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupTypeId.ColumnName = "ConfigurationServiceGroupTypeId";
				colvarConfigurationServiceGroupTypeId.DataType = DbType.Int32;
				colvarConfigurationServiceGroupTypeId.MaxLength = 0;
				colvarConfigurationServiceGroupTypeId.AutoIncrement = false;
				colvarConfigurationServiceGroupTypeId.IsNullable = false;
				colvarConfigurationServiceGroupTypeId.IsPrimaryKey = false;
				colvarConfigurationServiceGroupTypeId.IsForeignKey = false;
				colvarConfigurationServiceGroupTypeId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupTypeId);
				
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
				
				TableSchema.TableColumn colvarConfigurationServiceGroupTypeName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceGroupTypeName.ColumnName = "ConfigurationServiceGroupTypeName";
				colvarConfigurationServiceGroupTypeName.DataType = DbType.String;
				colvarConfigurationServiceGroupTypeName.MaxLength = 256;
				colvarConfigurationServiceGroupTypeName.AutoIncrement = false;
				colvarConfigurationServiceGroupTypeName.IsNullable = false;
				colvarConfigurationServiceGroupTypeName.IsPrimaryKey = false;
				colvarConfigurationServiceGroupTypeName.IsForeignKey = false;
				colvarConfigurationServiceGroupTypeName.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceGroupTypeName);
				
				TableSchema.TableColumn colvarReleaseQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarReleaseQueryParameterValue.ColumnName = "ReleaseQueryParameterValue";
				colvarReleaseQueryParameterValue.DataType = DbType.String;
				colvarReleaseQueryParameterValue.MaxLength = -1;
				colvarReleaseQueryParameterValue.AutoIncrement = false;
				colvarReleaseQueryParameterValue.IsNullable = true;
				colvarReleaseQueryParameterValue.IsPrimaryKey = false;
				colvarReleaseQueryParameterValue.IsForeignKey = false;
				colvarReleaseQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarReleaseQueryParameterValue);
				
				TableSchema.TableColumn colvarCountryQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarCountryQueryParameterValue.ColumnName = "CountryQueryParameterValue";
				colvarCountryQueryParameterValue.DataType = DbType.String;
				colvarCountryQueryParameterValue.MaxLength = -1;
				colvarCountryQueryParameterValue.AutoIncrement = false;
				colvarCountryQueryParameterValue.IsNullable = true;
				colvarCountryQueryParameterValue.IsPrimaryKey = false;
				colvarCountryQueryParameterValue.IsForeignKey = false;
				colvarCountryQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarCountryQueryParameterValue);
				
				TableSchema.TableColumn colvarPlatformQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarPlatformQueryParameterValue.ColumnName = "PlatformQueryParameterValue";
				colvarPlatformQueryParameterValue.DataType = DbType.String;
				colvarPlatformQueryParameterValue.MaxLength = -1;
				colvarPlatformQueryParameterValue.AutoIncrement = false;
				colvarPlatformQueryParameterValue.IsNullable = true;
				colvarPlatformQueryParameterValue.IsPrimaryKey = false;
				colvarPlatformQueryParameterValue.IsForeignKey = false;
				colvarPlatformQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarPlatformQueryParameterValue);
				
				TableSchema.TableColumn colvarBrandQueryParameterValue = new TableSchema.TableColumn(schema);
				colvarBrandQueryParameterValue.ColumnName = "BrandQueryParameterValue";
				colvarBrandQueryParameterValue.DataType = DbType.String;
				colvarBrandQueryParameterValue.MaxLength = -1;
				colvarBrandQueryParameterValue.AutoIncrement = false;
				colvarBrandQueryParameterValue.IsNullable = true;
				colvarBrandQueryParameterValue.IsPrimaryKey = false;
				colvarBrandQueryParameterValue.IsForeignKey = false;
				colvarBrandQueryParameterValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarBrandQueryParameterValue);
				
				TableSchema.TableColumn colvarPublisherLabelValue = new TableSchema.TableColumn(schema);
				colvarPublisherLabelValue.ColumnName = "PublisherLabelValue";
				colvarPublisherLabelValue.DataType = DbType.String;
				colvarPublisherLabelValue.MaxLength = -1;
				colvarPublisherLabelValue.AutoIncrement = false;
				colvarPublisherLabelValue.IsNullable = true;
				colvarPublisherLabelValue.IsPrimaryKey = false;
				colvarPublisherLabelValue.IsForeignKey = false;
				colvarPublisherLabelValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarPublisherLabelValue);
				
				TableSchema.TableColumn colvarInstallerTypeLabelValue = new TableSchema.TableColumn(schema);
				colvarInstallerTypeLabelValue.ColumnName = "InstallerTypeLabelValue";
				colvarInstallerTypeLabelValue.DataType = DbType.String;
				colvarInstallerTypeLabelValue.MaxLength = -1;
				colvarInstallerTypeLabelValue.AutoIncrement = false;
				colvarInstallerTypeLabelValue.IsNullable = true;
				colvarInstallerTypeLabelValue.IsPrimaryKey = false;
				colvarInstallerTypeLabelValue.IsForeignKey = false;
				colvarInstallerTypeLabelValue.IsReadOnly = false;
				
				schema.Columns.Add(colvarInstallerTypeLabelValue);
				
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
				
				TableSchema.TableColumn colvarConfigurationServiceApplicationId = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceApplicationId.ColumnName = "ConfigurationServiceApplicationId";
				colvarConfigurationServiceApplicationId.DataType = DbType.Int32;
				colvarConfigurationServiceApplicationId.MaxLength = 0;
				colvarConfigurationServiceApplicationId.AutoIncrement = false;
				colvarConfigurationServiceApplicationId.IsNullable = false;
				colvarConfigurationServiceApplicationId.IsPrimaryKey = false;
				colvarConfigurationServiceApplicationId.IsForeignKey = false;
				colvarConfigurationServiceApplicationId.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceApplicationId);
				
				TableSchema.TableColumn colvarConfigurationServiceApplicationName = new TableSchema.TableColumn(schema);
				colvarConfigurationServiceApplicationName.ColumnName = "ConfigurationServiceApplicationName";
				colvarConfigurationServiceApplicationName.DataType = DbType.String;
				colvarConfigurationServiceApplicationName.MaxLength = 256;
				colvarConfigurationServiceApplicationName.AutoIncrement = false;
				colvarConfigurationServiceApplicationName.IsNullable = false;
				colvarConfigurationServiceApplicationName.IsPrimaryKey = false;
				colvarConfigurationServiceApplicationName.IsForeignKey = false;
				colvarConfigurationServiceApplicationName.IsReadOnly = false;
				
				schema.Columns.Add(colvarConfigurationServiceApplicationName);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ElementsCPSDB"].AddSchema("vwMapConfigurationServiceGroup",schema);
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
		protected VwMapConfigurationServiceGroup(bool useDatabaseDefaults, bool markNew)
		{
			this.ConstructorHelper(useDatabaseDefaults);
			if (markNew)
			{
				MarkNew();
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceGroup() : this(false) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceGroup(bool useDatabaseDefaults) : this(useDatabaseDefaults, true) {}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceGroup(object keyID)
			: this(false, false)
		{
			LoadByKey(keyID);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public VwMapConfigurationServiceGroup(string columnName, object columnValue)
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
		/// <param name="original">The <see cref="VwMapConfigurationServiceGroup"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private VwMapConfigurationServiceGroup(VwMapConfigurationServiceGroup original)
			: this()
		{
			this.CopyConstuctorHelper(original);
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		private void CopyConstuctorHelper(VwMapConfigurationServiceGroup original)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(original, "original");
			
			this.Id = original.Id;
			
			this.AppClientId = original.AppClientId;
			
			this.RowStatusName = original.RowStatusName;
			
			this.ConfigurationServiceGroupStatusName = original.ConfigurationServiceGroupStatusName;
			
			this.PersonName = original.PersonName;
			
			this.CreatedBy = original.CreatedBy;
			
			this.CreatedOn = original.CreatedOn;
			
			this.ModifiedBy = original.ModifiedBy;
			
			this.ModifiedOn = original.ModifiedOn;
			
			this.RowStatusId = original.RowStatusId;
			
			this.Description = original.Description;
			
			this.ConfigurationServiceGroupStatusId = original.ConfigurationServiceGroupStatusId;
			
			this.ConfigurationServiceGroupTypeId = original.ConfigurationServiceGroupTypeId;
			
			this.OwnerId = original.OwnerId;
			
			this.ConfigurationServiceGroupTypeName = original.ConfigurationServiceGroupTypeName;
			
			this.ReleaseQueryParameterValue = original.ReleaseQueryParameterValue;
			
			this.CountryQueryParameterValue = original.CountryQueryParameterValue;
			
			this.PlatformQueryParameterValue = original.PlatformQueryParameterValue;
			
			this.BrandQueryParameterValue = original.BrandQueryParameterValue;
			
			this.PublisherLabelValue = original.PublisherLabelValue;
			
			this.InstallerTypeLabelValue = original.InstallerTypeLabelValue;
			
			this.Tags = original.Tags;
			
			this.TagCount = original.TagCount;
			
			this.ProductionId = original.ProductionId;
			
			this.ValidationId = original.ValidationId;
			
			this.Name = original.Name;
			
			this.ConfigurationServiceApplicationId = original.ConfigurationServiceApplicationId;
			
			this.ConfigurationServiceApplicationName = original.ConfigurationServiceApplicationName;
			
		}
		/// <summary>
		/// Factory method alternative to the Copy constructor.
		/// </summary>
		/// <param name="original">The <see cref="VwMapConfigurationServiceGroup"/> instance to duplicate.</param>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static VwMapConfigurationServiceGroup Copy(VwMapConfigurationServiceGroup original)
		{
			return new VwMapConfigurationServiceGroup(original);
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
		partial void OnConfigurationServiceGroupStatusNameChanging(string newValue);
		partial void OnConfigurationServiceGroupStatusNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupStatusName")]
		[Bindable(true)]
		public string ConfigurationServiceGroupStatusName 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceGroupStatusName");
			}
			set
			{
				this.OnConfigurationServiceGroupStatusNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupStatusName", value);
				string oldValue = this.ConfigurationServiceGroupStatusName;
				SetColumnValue("ConfigurationServiceGroupStatusName", value);
				this.OnConfigurationServiceGroupStatusNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupStatusName", oldValue, value);
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
		partial void OnConfigurationServiceGroupStatusIdChanging(int newValue);
		partial void OnConfigurationServiceGroupStatusIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupStatusId")]
		[Bindable(true)]
		public int ConfigurationServiceGroupStatusId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceGroupStatusId");
			}
			set
			{
				this.OnConfigurationServiceGroupStatusIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupStatusId", value);
				int oldValue = this.ConfigurationServiceGroupStatusId;
				SetColumnValue("ConfigurationServiceGroupStatusId", value);
				this.OnConfigurationServiceGroupStatusIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupStatusId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceGroupTypeIdChanging(int newValue);
		partial void OnConfigurationServiceGroupTypeIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupTypeId")]
		[Bindable(true)]
		public int ConfigurationServiceGroupTypeId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceGroupTypeId");
			}
			set
			{
				this.OnConfigurationServiceGroupTypeIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupTypeId", value);
				int oldValue = this.ConfigurationServiceGroupTypeId;
				SetColumnValue("ConfigurationServiceGroupTypeId", value);
				this.OnConfigurationServiceGroupTypeIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupTypeId", oldValue, value);
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
		partial void OnConfigurationServiceGroupTypeNameChanging(string newValue);
		partial void OnConfigurationServiceGroupTypeNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceGroupTypeName")]
		[Bindable(true)]
		public string ConfigurationServiceGroupTypeName 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceGroupTypeName");
			}
			set
			{
				this.OnConfigurationServiceGroupTypeNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceGroupTypeName", value);
				string oldValue = this.ConfigurationServiceGroupTypeName;
				SetColumnValue("ConfigurationServiceGroupTypeName", value);
				this.OnConfigurationServiceGroupTypeNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceGroupTypeName", oldValue, value);
			}
		}
		partial void OnReleaseQueryParameterValueChanging(string newValue);
		partial void OnReleaseQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ReleaseQueryParameterValue")]
		[Bindable(true)]
		public string ReleaseQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("ReleaseQueryParameterValue");
			}
			set
			{
				this.OnReleaseQueryParameterValueChanging(value);
				this.OnPropertyChanging("ReleaseQueryParameterValue", value);
				string oldValue = this.ReleaseQueryParameterValue;
				SetColumnValue("ReleaseQueryParameterValue", value);
				this.OnReleaseQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("ReleaseQueryParameterValue", oldValue, value);
			}
		}
		partial void OnCountryQueryParameterValueChanging(string newValue);
		partial void OnCountryQueryParameterValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("CountryQueryParameterValue")]
		[Bindable(true)]
		public string CountryQueryParameterValue 
		{
			get
			{
				return GetColumnValue<string>("CountryQueryParameterValue");
			}
			set
			{
				this.OnCountryQueryParameterValueChanging(value);
				this.OnPropertyChanging("CountryQueryParameterValue", value);
				string oldValue = this.CountryQueryParameterValue;
				SetColumnValue("CountryQueryParameterValue", value);
				this.OnCountryQueryParameterValueChanged(oldValue, value);
				this.OnPropertyChanged("CountryQueryParameterValue", oldValue, value);
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
		partial void OnPublisherLabelValueChanging(string newValue);
		partial void OnPublisherLabelValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("PublisherLabelValue")]
		[Bindable(true)]
		public string PublisherLabelValue 
		{
			get
			{
				return GetColumnValue<string>("PublisherLabelValue");
			}
			set
			{
				this.OnPublisherLabelValueChanging(value);
				this.OnPropertyChanging("PublisherLabelValue", value);
				string oldValue = this.PublisherLabelValue;
				SetColumnValue("PublisherLabelValue", value);
				this.OnPublisherLabelValueChanged(oldValue, value);
				this.OnPropertyChanged("PublisherLabelValue", oldValue, value);
			}
		}
		partial void OnInstallerTypeLabelValueChanging(string newValue);
		partial void OnInstallerTypeLabelValueChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("InstallerTypeLabelValue")]
		[Bindable(true)]
		public string InstallerTypeLabelValue 
		{
			get
			{
				return GetColumnValue<string>("InstallerTypeLabelValue");
			}
			set
			{
				this.OnInstallerTypeLabelValueChanging(value);
				this.OnPropertyChanging("InstallerTypeLabelValue", value);
				string oldValue = this.InstallerTypeLabelValue;
				SetColumnValue("InstallerTypeLabelValue", value);
				this.OnInstallerTypeLabelValueChanged(oldValue, value);
				this.OnPropertyChanged("InstallerTypeLabelValue", oldValue, value);
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
		partial void OnConfigurationServiceApplicationIdChanging(int newValue);
		partial void OnConfigurationServiceApplicationIdChanged(int oldValue, int newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceApplicationId")]
		[Bindable(true)]
		public int ConfigurationServiceApplicationId 
		{
			get
			{
				return GetColumnValue<int>("ConfigurationServiceApplicationId");
			}
			set
			{
				this.OnConfigurationServiceApplicationIdChanging(value);
				this.OnPropertyChanging("ConfigurationServiceApplicationId", value);
				int oldValue = this.ConfigurationServiceApplicationId;
				SetColumnValue("ConfigurationServiceApplicationId", value);
				this.OnConfigurationServiceApplicationIdChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceApplicationId", oldValue, value);
			}
		}
		partial void OnConfigurationServiceApplicationNameChanging(string newValue);
		partial void OnConfigurationServiceApplicationNameChanged(string oldValue, string newValue);
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		[XmlAttribute("ConfigurationServiceApplicationName")]
		[Bindable(true)]
		public string ConfigurationServiceApplicationName 
		{
			get
			{
				return GetColumnValue<string>("ConfigurationServiceApplicationName");
			}
			set
			{
				this.OnConfigurationServiceApplicationNameChanging(value);
				this.OnPropertyChanging("ConfigurationServiceApplicationName", value);
				string oldValue = this.ConfigurationServiceApplicationName;
				SetColumnValue("ConfigurationServiceApplicationName", value);
				this.OnConfigurationServiceApplicationNameChanged(oldValue, value);
				this.OnPropertyChanged("ConfigurationServiceApplicationName", oldValue, value);
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
			
			public static string ConfigurationServiceGroupStatusName = @"ConfigurationServiceGroupStatusName";
			
			public static string PersonName = @"PersonName";
			
			public static string CreatedBy = @"CreatedBy";
			
			public static string CreatedOn = @"CreatedOn";
			
			public static string ModifiedBy = @"ModifiedBy";
			
			public static string ModifiedOn = @"ModifiedOn";
			
			public static string RowStatusId = @"RowStatusId";
			
			public static string Description = @"Description";
			
			public static string ConfigurationServiceGroupStatusId = @"ConfigurationServiceGroupStatusId";
			
			public static string ConfigurationServiceGroupTypeId = @"ConfigurationServiceGroupTypeId";
			
			public static string OwnerId = @"OwnerId";
			
			public static string ConfigurationServiceGroupTypeName = @"ConfigurationServiceGroupTypeName";
			
			public static string ReleaseQueryParameterValue = @"ReleaseQueryParameterValue";
			
			public static string CountryQueryParameterValue = @"CountryQueryParameterValue";
			
			public static string PlatformQueryParameterValue = @"PlatformQueryParameterValue";
			
			public static string BrandQueryParameterValue = @"BrandQueryParameterValue";
			
			public static string PublisherLabelValue = @"PublisherLabelValue";
			
			public static string InstallerTypeLabelValue = @"InstallerTypeLabelValue";
			
			public static string Tags = @"Tags";
			
			public static string TagCount = @"TagCount";
			
			public static string ProductionId = @"ProductionId";
			
			public static string ValidationId = @"ValidationId";
			
			public static string Name = @"Name";
			
			public static string ConfigurationServiceApplicationId = @"ConfigurationServiceApplicationId";
			
			public static string ConfigurationServiceApplicationName = @"ConfigurationServiceApplicationName";
			
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
		// /// <returns>A hash code for the current <see cref="VwMapConfigurationServiceGroup"/>.</returns>
		// [System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		// public override int GetHashCode()
		// {
		// 	//return base.GetHashCode();
		// 	//return this.GetPrimaryKeyValue().GetHashCode();
		// 	return string.Format("VwMapConfigurationServiceGroup#{0}", this.GetPrimaryKeyValue()).GetHashCode();
		// }
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceGroup"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="VwMapConfigurationServiceGroup"/>. </param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is identical to the current <see cref="VwMapConfigurationServiceGroup"/>; otherwise, <c>false</c>.</returns>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public bool IdenticalTo(object obj)
		{
			VwMapConfigurationServiceGroup instance1 = this;
			VwMapConfigurationServiceGroup instance2 = obj as VwMapConfigurationServiceGroup;
			bool isEqual =
				(instance2 != null)
			
				&& (instance1.Id == instance2.Id)
			
				&& (instance1.AppClientId == instance2.AppClientId)
			
				&& (instance1.RowStatusName == instance2.RowStatusName)
			
				&& (instance1.ConfigurationServiceGroupStatusName == instance2.ConfigurationServiceGroupStatusName)
			
				&& (instance1.PersonName == instance2.PersonName)
			
				&& (instance1.CreatedBy == instance2.CreatedBy)
			
				&& (instance1.CreatedOn == instance2.CreatedOn)
			
				&& (instance1.ModifiedBy == instance2.ModifiedBy)
			
				&& (instance1.ModifiedOn == instance2.ModifiedOn)
			
				&& (instance1.RowStatusId == instance2.RowStatusId)
			
				&& (instance1.Description == instance2.Description)
			
				&& (instance1.ConfigurationServiceGroupStatusId == instance2.ConfigurationServiceGroupStatusId)
			
				&& (instance1.ConfigurationServiceGroupTypeId == instance2.ConfigurationServiceGroupTypeId)
			
				&& (instance1.OwnerId == instance2.OwnerId)
			
				&& (instance1.ConfigurationServiceGroupTypeName == instance2.ConfigurationServiceGroupTypeName)
			
				&& (instance1.ReleaseQueryParameterValue == instance2.ReleaseQueryParameterValue)
			
				&& (instance1.CountryQueryParameterValue == instance2.CountryQueryParameterValue)
			
				&& (instance1.PlatformQueryParameterValue == instance2.PlatformQueryParameterValue)
			
				&& (instance1.BrandQueryParameterValue == instance2.BrandQueryParameterValue)
			
				&& (instance1.PublisherLabelValue == instance2.PublisherLabelValue)
			
				&& (instance1.InstallerTypeLabelValue == instance2.InstallerTypeLabelValue)
			
				&& (instance1.Tags == instance2.Tags)
			
				&& (instance1.TagCount == instance2.TagCount)
			
				&& (instance1.ProductionId == instance2.ProductionId)
			
				&& (instance1.ValidationId == instance2.ValidationId)
			
				&& (instance1.Name == instance2.Name)
			
				&& (instance1.ConfigurationServiceApplicationId == instance2.ConfigurationServiceApplicationId)
			
				&& (instance1.ConfigurationServiceApplicationName == instance2.ConfigurationServiceApplicationName)
			
			;
			return isEqual; //return base.Equals(obj);
		}
/*
		/// <summary>
		/// Determines whether the specified <see cref="VwMapConfigurationServiceGroup"/> instances are considered equal.
		/// </summary>
		/// <param name="instance1">The first <see cref="VwMapConfigurationServiceGroup"/> to compare.</param>
		/// <param name="instance2">The second <see cref="VwMapConfigurationServiceGroup"/> to compare.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="instance1"/> is the same instance as <paramref name="instance2"/> 
		/// or if both are null references 
		/// or if <paramref name="instance1"/>.Equals(<paramref name="instance2"/>) returns <c>true</c>; 
		/// otherwise, false.
		/// </returns>
		/// <seealso cref="Equals(object)"/>
		[System.CodeDom.Compiler.GeneratedCodeAttribute("SubSonic", "2.1.0.0")]
		public static bool Equals(VwMapConfigurationServiceGroup instance1, VwMapConfigurationServiceGroup instance2)
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
