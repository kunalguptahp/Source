using System;
using System.Data;
using System.Collections.Generic;
using SubSonic;

namespace HP.ElementsCPS.Data.Utility.ImportUtility
{
	public class ImportUtilityForStatefulEntity<TRecord> : ImportUtility<TRecord>
		where TRecord : ActiveRecord<TRecord>, IActiveRecord, new()
	{

		#region Properties

		/// <summary>
		/// StateColumn doesn't exist in this collection but it will still be handled.
		/// </summary>
		public override List<TableSchema.TableColumn> ImportableColumns
		{
			get
			{
				//this._ImportableColumns = base.ImportableColumns;
				//this._ImportableColumns.RemoveAll(c => c.ColumnName == StateColumnName);
				//return this._ImportableColumns;
				return base.ImportableColumns;
			}
			set
			{
				base.ImportableColumns = value;
				this.RemoveStateColumnFromImportableColumns();
			}
		}

		private string _StateColumnName;
		protected string StateColumnName
		{
			get { return this._StateColumnName; }
			set
			{
				this._StateColumnName = value;
				this.RemoveStateColumnFromImportableColumns();
			}
		}

		private void RemoveStateColumnFromImportableColumns()
		{
			this._ImportableColumns.RemoveAll(c => c.ColumnName == this.StateColumnName);
		}

		#endregion

		#region Events

		public Action<TRecord, object> ModifyLifecycleStateCallback { get; set; }

		#endregion

		#region Constructors

		public ImportUtilityForStatefulEntity(string stateColumnName)
			: base()
		{
			StateColumnName = stateColumnName;
		}

		public ImportUtilityForStatefulEntity(List<TableSchema.TableColumn> importableColumns, string idColumnName, string stateColumnName)
			: base(importableColumns, idColumnName)
		{
			StateColumnName = stateColumnName;
		}

		#endregion

		#region Methods

		protected override bool ImportRowValues(DataRow row, TRecord instance)
		{
			bool instanceModified = base.ImportRowValues(row, instance);

			object newInstanceState = row.Table.Columns.Contains(this.StateColumnName) ? row[this.StateColumnName] : null;
			if (newInstanceState == null)
			{
				return instanceModified;
			}

			//perform the state transition
			if (this.ModifyLifecycleStateCallback != null)
			{
				this.ModifyLifecycleStateCallback(instance, newInstanceState);
			}

			//NOTE: DO NOT use or save the original instance, as the state transition may have made it out of date

			return true;
		}

		#endregion

	}
}
