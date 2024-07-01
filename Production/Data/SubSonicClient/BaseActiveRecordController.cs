using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using HP.HPFx.Data.Utility;
using HP.HPFx.Security;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	public abstract class BaseActiveRecordController<TRecord, TRecordCollection, TController> : BaseRecordController<TRecord, TRecordCollection, TController>, IActiveRecordController 
		where TRecord : ActiveRecord<TRecord>, IActiveRecord, new()
		where TRecordCollection : ActiveList<TRecord, TRecordCollection>, new()
		where TController : BaseActiveRecordController<TRecord, TRecordCollection, TController>, new()
	{
		protected static string UserName
		{
			get
			{
				return SimpleSecurityManager.CurrentUserIdentityName;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public bool Delete(object Id)
		{
			return (ActiveRecord<TRecord>.Delete(Id) == 1);
		}

		[DataObjectMethod(DataObjectMethodType.Delete, false)]
		public bool Destroy(object Id)
		{
			return (ActiveRecord<TRecord>.Destroy(Id) == 1);
		}

		/// <summary>
		/// Returns a <see cref="DataSet"/> that contains the distinct values within a specific table column.
		/// </summary>
		/// <param name="columnName">The name of the DB column.</param>
		/// <param name="columnValuePrefix">Optional. If not null, only column values that begin with the specified string will be returned.</param>
		/// <param name="rowStatusFilter">If not null, filters the results by the specified RowStatus.</param>
		/// <param name="pageIndex">The 0-based index of the page of values to return.</param>
		/// <param name="pageSize">The maximum number of values to return.</param>
		/// <returns>
		/// A <see cref="DataSet"/> (containing a single <see cref="DataTable"/> with a single <see cref="DataColumn"/>) 
		/// containing the distinct values within the specified table column.
		/// </returns>
		public static DataSet FetchDistinctColumnValues(string columnName, string columnValuePrefix, int? rowStatusFilter, int pageIndex, int pageSize)
		{
			return SubSonicUtility.CreateQuery_DistinctColumnValues(GetTableSchema().TableName, columnName, columnValuePrefix, rowStatusFilter, pageIndex, pageSize).ExecuteDataSet();
		}

		/// <summary>
		/// Returns a <see cref="DataSet"/> that contains the distinct values within a specific table column.
		/// </summary>
		/// <param name="columnName">The name of the DB column.</param>
		/// <param name="columnValuePrefix">Optional. If not null, only column values that begin with the specified string will be returned.</param>
		/// <param name="rowStatusFilter">If not null, filters the results by the specified RowStatus.</param>
		/// <returns>
		/// A <see cref="DataSet"/> (containing a single <see cref="DataTable"/> with a single <see cref="DataColumn"/>) 
		/// containing the distinct values within the specified table column.
		/// </returns>
		public static DataSet FetchDistinctColumnValues(string columnName, string columnValuePrefix, int? rowStatusFilter)
		{
			//return NewDistinctColumnValuesQuery(columnName, columnValuePrefix, rowStatusFilter).ExecuteDataSet();
			return NewDistinctColumnValuesQuery2(columnName, columnValuePrefix, rowStatusFilter).ExecuteDataSet();
		}

		/// <summary>
		/// Returns a list that contains the distinct values within a specific table column.
		/// </summary>
		/// <param name="columnName">The name of the DB column.</param>
		/// <param name="columnValuePrefix">Optional. If not null, only column values that begin with the specified string will be returned.</param>
		/// <param name="rowStatusFilter">If not null, filters the results by the specified RowStatus.</param>
		/// <param name="pageIndex">The 0-based index of the page of values to return.</param>
		/// <param name="pageSize">The maximum number of values to return.</param>
		/// <returns>A list of the distinct column values.</returns>
		public static List<string> GetDistinctColumnValues(string columnName, string columnValuePrefix, int? rowStatusFilter, int pageIndex, int pageSize)
		{
			DataSet ds = FetchDistinctColumnValues(columnName, columnValuePrefix, rowStatusFilter, pageIndex, pageSize);
			return SqlUtility.CreateListOfColumnValues<string>(ds.Tables[0], 0);
		}

		/// <summary>
		/// Returns a list that contains the distinct values within a specific table column.
		/// </summary>
		/// <param name="columnName">The name of the DB column.</param>
		/// <param name="columnValuePrefix">Optional. If not null, only column values that begin with the specified string will be returned.</param>
		/// <param name="rowStatusFilter">If not null, filters the results by the specified RowStatus.</param>
		/// <returns>A list of the distinct column values.</returns>
		public static List<string> GetDistinctColumnValues(string columnName, string columnValuePrefix, int? rowStatusFilter)
		{
			DataSet ds = FetchDistinctColumnValues(columnName, columnValuePrefix, rowStatusFilter);
			return SqlUtility.CreateListOfColumnValues<string>(ds.Tables[0], 0);
		}

        public static List<int> GetDistinctColumnValueInts(string columnName, string columnValuePrefix, int? rowStatusFilter)
        {
            DataSet ds = FetchDistinctColumnValues(columnName, columnValuePrefix, rowStatusFilter);
            return SqlUtility.CreateListOfColumnValues<int>(ds.Tables[0], 0);
        }
	}
}