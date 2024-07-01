using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using HP.HPFx.Collections;
using HP.HPFx.Diagnostics.Logging;
using System.Transactions;
using HP.ElementsCPS.Core.Security;
using HP.HPFx.Utility;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility.SubSonic;
using SubSonic;


namespace HP.ElementsCPS.Data.Utility.ImportUtility
{
	public class ImportUtility<TRecord>
				where TRecord : ActiveRecord<TRecord>, IActiveRecord, new()
	{

		#region Properties

		protected List<TableSchema.TableColumn> _ImportableColumns;

		/// <summary>
		/// IdColumn doesn't exist in this collection but it will still be handled.
		/// </summary>
		public virtual List<TableSchema.TableColumn> ImportableColumns
		{
			get
			{
				//_ImportableColumns.RemoveAll(c=>c.ColumnName == IdColumnName);
				return this._ImportableColumns;
			}
			set
			{
				this._ImportableColumns = new List<TableSchema.TableColumn>(value);
			}
		}

		public string IdColumnName { get; set; }

		public Func<TRecord> NewInstanceFactory { get; set; }

		#endregion

		#region Constructors

		public ImportUtility()
			: this(SubSonicUtility.GetDefaultImportableColumns<TRecord>(false), SubSonicUtility.GetPrimaryKey<TRecord>().ColumnName)
		{
		}

		public ImportUtility(IEnumerable<TableSchema.TableColumn> importableColumns, string idColumnName)
		{
			ImportableColumns = importableColumns.ToList();
			IdColumnName = idColumnName;
		}

		#endregion

		#region Methods

		public List<TRecord> Import(IEnumerable<DataRow> rows, int? estimatedRowCount)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(rows, "rows");

			try
			{
				List<TRecord> instances = (estimatedRowCount == null) ? new List<TRecord>() : new List<TRecord>(estimatedRowCount.Value);
				using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					foreach (DataRow row in rows)
					{
						TRecord instance = this.Import(row);
						if (instance != null)
						{
							instances.Add(instance);
						}
					}

					scope.Complete(); // transaction complete
				}
				return instances;
			}
			catch (Exception ex)
			{
				LogManager.Current.Log(Severity.Info, typeof(TRecord), "Data import failed.", ex);

				throw;
			}
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		public List<TRecord> Import(DataRowCollection rows)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(rows, "rows");
			IEnumerable<DataRow> dataRows = new Iterator(rows.GetEnumerator()).Cast<DataRow>();
			return this.Import(dataRows, rows.Count);
		}

		public TRecord Import(DataRow row)
		{
			ExceptionUtility.ArgumentNullEx_ThrowIfNull(row, "row");

			try
			{
				TRecord instance;

				using (TransactionScope scope = new TransactionScope()) //using (SharedDbConnectionScope sharedConnectionScope = new SharedDbConnectionScope())
				{
					instance = GetInstance(row);
					this.ImportRowValues(row, instance);
					scope.Complete(); // transaction complete
				}
				return instance;
			}
			catch (Exception ex)
			{
				LogManager.Current.Log(Severity.Info, typeof(TRecord), "Data import failed.", ex);
				throw;
			}
		}

		protected virtual bool ImportRowValues(DataRow row, TRecord instance)
		{
			int importedValueCount = 0;
			foreach (TableSchema.TableColumn columnToImport in ImportableColumns)
			{
				object value = row.Table.Columns.Contains(columnToImport.ColumnName) ? row[columnToImport.ColumnName] : null;
				bool valueWasImported = this.ImportColumnValue(instance, columnToImport, value);
				if (valueWasImported)
				{
					importedValueCount++;
				}
			}
			if (importedValueCount <= 0)
			{
				return false;
			}

			this.SaveInstance(instance);
			return true;
		}

		protected TRecord DefaultNewInstanceFactory()
		{
			TRecord instance = new TRecord();
			return instance;
		}

		protected TRecord GetInstance(DataRow row)
		{
			TRecord instance;
			object rowId = row[IdColumnName];

			int? instanceId = ((rowId == null) || (rowId == DBNull.Value)) ? null : rowId.ToString().TryParseInt32();

			if (instanceId == null)
			{
				//create a new instance (using either a specified factory method, or the default implementation)
				Func<TRecord> newInstanceFactory = this.NewInstanceFactory ?? this.DefaultNewInstanceFactory;
				instance = newInstanceFactory();
			}
			else
			{
				//get the existing instance with the specified ID in order to update it
				instance = ActiveRecord<TRecord>.FetchByID(instanceId.Value);
			}

			if (instance == null)
			{
				throw new ArgumentException(
					string.Format("Unable to update existing data. There is no existing data item with the specified ID value ({0}).", instanceId.Value));
			}
			return instance;
		}

		protected bool ImportColumnValue(TRecord instance, TableSchema.TableColumn columnToImport, object value)
		{
			if (ElementsCPSDataUtility.IsImportableValue(value))
			{
				value = ParseImportValue(columnToImport, value);
				instance.SetColumnValue(columnToImport.ColumnName, value);
				return true;
			}
			return false;
		}

		protected void SaveInstance(TRecord instance)
		{
			//if ((ImportableValueCount > 1) || instance.IsNew || instance.IsDirty)
			//{
			instance.Save(SecurityManager.CurrentUserIdentityName);
			//}
		}

		#region Import Utility Methods

		public static object ParseImportValue(TableSchema.TableColumn columnToImport, object value)
		{
			switch (columnToImport.DataType)
			{
				case DbType.String:
					value = columnToImport.IsNullable ? ElementsCPSDataUtility.ParseImportValueAsString(value) : ElementsCPSDataUtility.ParseImportValueAsString(value);
					break;
				case DbType.Int16:
				case DbType.Int32:
					value = columnToImport.IsNullable ? ElementsCPSDataUtility.ParseImportValueAsNullableInt32(value) : ElementsCPSDataUtility.ParseImportValueAsInt32(value);
					break;
				case DbType.Int64:
					value = columnToImport.IsNullable ? ElementsCPSDataUtility.ParseImportValueAsNullableInt64(value) : ElementsCPSDataUtility.ParseImportValueAsNullableInt64(value);
					break;
				case DbType.Double:
					value = columnToImport.IsNullable ? ElementsCPSDataUtility.ParseImportValueAsNullableDouble(value) : ElementsCPSDataUtility.ParseImportValueAsDouble(value);
					break;
				case DbType.Decimal:
					value = columnToImport.IsNullable ? ElementsCPSDataUtility.ParseImportValueAsNullableDecimal(value) : ElementsCPSDataUtility.ParseImportValueAsDecimal(value);
					break;
				case DbType.DateTime:
				case DbType.DateTime2:
				case DbType.Date:
					value = columnToImport.IsNullable ? ElementsCPSDataUtility.ParseImportValueAsNullableDateTime(value) : ElementsCPSDataUtility.ParseImportValueAsNullableDateTime(value);
					break;
				case DbType.Boolean:
					value = columnToImport.IsNullable ? ElementsCPSDataUtility.ParseImportValueAsNullableBoolean(value) : ElementsCPSDataUtility.ParseImportValueAsBoolean(value);
					break;
				default:
					throw new NotImplementedException(string.Format("Type {0:G} can't be parsed. Please implement in the code.", columnToImport.DataType));
			}
			return value;
		}

		#endregion

		#endregion

	}
}
