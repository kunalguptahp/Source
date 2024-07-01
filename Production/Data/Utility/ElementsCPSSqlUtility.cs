using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.Text.StringManipulation;
using HP.HPFx.Utility;

namespace HP.ElementsCPS.Data.Utility
{
	/// <summary>
	/// Contains utility methods related to SQL, SQL Server, etc.
	/// </summary>
	public static class ElementsCPSSqlUtility
	{

		#region Connection-related Utility Methods

		/// <summary>
		/// Returns the default connection string from the configuration settings.
		/// </summary>
		/// <returns></returns>
		public static string GetDefaultConnectionString()
		{
			return SqlUtility.GetConnectionString("ElementsCPSDB");
		}

		/// <summary>
		/// Returns a DB connection using the system's default connection string.
		/// </summary>
		/// <returns></returns>
		public static SqlConnection CreateDefaultConnection()
		{
			return new SqlConnection(GetDefaultConnectionString());
		}

		public static DataTable GetDataSource(bool distinct, bool withNoLock, string fromTable, string joinClauseSql, IEnumerable<string> selectExpressions, string whereClauseSql, IEnumerable<string> orderByExpressions)
		{
			DataTable dt = new DataTable();

			const string sqlStatementTemplate = "SELECT {0} FROM {1} {2} {3} {4}";
			string[] orderBy = (orderByExpressions == null) ? new string[] { } : orderByExpressions.ToArray();
			string sqlStatement = string.Format(sqlStatementTemplate
				, (distinct ? "DISTINCT " : "") + string.Join(", ", selectExpressions.ToArray())
				, fromTable + (withNoLock ? " WITH (NOLOCK)" : "")
				, string.IsNullOrEmpty(joinClauseSql) ? "" : joinClauseSql
				, string.IsNullOrEmpty(whereClauseSql) ? "" : ("WHERE " + whereClauseSql)
				, (orderBy.Length < 1) ? "" : ("ORDER BY " + string.Join(", ", orderBy))
				);
			var da = new DataAccess();
			da.ExecuteQuery(sqlStatement);
			dt = da.DSet.Tables[0];

			return dt;
		}

		public static DataTable GetListControlDataSource(string fromTable, string valueColumn, string textColumn, string whereClauseSql)
		{
			return GetDataSource(false, true, fromTable, "", new[] { valueColumn, textColumn }, whereClauseSql, new[] { textColumn });
		}

		public static DataTable GetListControlDataSource(string fromTable, string valueColumn, string textColumn, string whereClauseSql, string orderByColumn)
		{
			return GetDataSource(false, true, fromTable, "", new[] { valueColumn, textColumn }, whereClauseSql, new[] { orderByColumn });
		}

        public static DataTable GetTenantListControlDataSource(string fromTable, string valueColumn, string textColumn, string filterColumn ,string whereClauseSql)
        {
            return GetDataSource(false, true, fromTable, "", new[] { valueColumn, textColumn, filterColumn }, whereClauseSql, new[] { textColumn });
        }

		public static DataTable GetListControlDataSource_Id_Name(string fromTable)
		{
			return GetListControlDataSource(fromTable, "Id", "Name", null);
		}

		public static DataTable GetListControlDataSource_Id_Name(string fromTable, RowStatus.RowStatusId? rowStatusId)
		{
			string whereClauseSql = (rowStatusId == null) ? null : string.Format(" RowStatusId = {0:D}", rowStatusId);
			return GetListControlDataSource(fromTable, "Id", "Name", whereClauseSql);
		}

		public static DataTable GetListControlDataSource_Id_Name(string fromTable, string whereClauseSql)
		{
			return GetListControlDataSource(fromTable, "Id", "Name", whereClauseSql);
		}

        public static DataTable GetTenantListControlDataSource_Id_Name(string fromTable, RowStatus.RowStatusId? rowStatusId, int tenantGroupId)
        {
            string whereClauseSql = (rowStatusId == null) ? null : string.Format(" RowStatusId = {0:D}", rowStatusId);
           
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(tenantGroupId);
            DataTable dataTable = GetTenantListControlDataSource(fromTable, "Id", "Name", "CreatedBy", whereClauseSql);
            string strs = "'" + string.Join("','", windowsIds.ToArray()) + "'";
            //DataRow[] rows = dataTable.Select("CreatedBy in ( " + strs + ")");
            //DataTable newdataTable = new DataTable();
            //newdataTable = dataTable.Clone();
            //foreach (DataRow row in rows)
            //{
            //    newdataTable.Rows.Add(row.ItemArray);
            //}
            //newdataTable.Columns.Remove("CreatedBy");
            //int i = newdataTable.Rows.Count;
            //return newdataTable;
            dataTable.DefaultView.RowFilter="CreatedBy in ( " + strs + ")";
            DataTable dt = dataTable.DefaultView.ToTable(false, new string[] { "Id", "Name"});
            
            return dt;
        }

        public static List<int> GetTenantListControlDataSource_Id(string fromTable, RowStatus.RowStatusId? rowStatusId, int tenantGroupId)
        {
            string whereClauseSql = (rowStatusId == null) ? null : string.Format(" RowStatusId = {0:D}", rowStatusId);
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(tenantGroupId);
            DataTable dataTable = GetTenantListControlDataSource(fromTable, "Id", "Name", "CreatedBy", whereClauseSql);
            string strs = "'" + string.Join("','", windowsIds.ToArray()) + "'";
            DataRow[] rows = dataTable.Select("CreatedBy in ( " + strs + ")");
            DataTable newdataTable = new DataTable();
            newdataTable = dataTable.Clone();
            List<int> list = new List<int>();
            foreach (DataRow row in rows)
            {
                newdataTable.Rows.Add(row.ItemArray);
            }
            newdataTable.Columns.Remove("CreatedBy");
            foreach(DataRow row in rows)
            {
                list.Add((int)row["Id"]);
            }
            return list;
        }
      
        //for tenant_appclient
        //public static DataTable GetListControlDataSource_TenantGroupId_AppClientId(string fromTable, string whereClauseSql)
        //{


        //    return GetListControlDataSource(fromTable, "TenantGroupId", "AppClientId", whereClauseSql);
        //}

		public static DataTable GetListControlDataSource_Id_Name(string fromTable, string whereClauseSql, string orderByColumn)
		{
			return GetListControlDataSource(fromTable, "Id", "Name", whereClauseSql, orderByColumn);
		}

		public static DataTable GetListControlDataSource_Id_UniqueName(string fromTable)
		{
			return GetListControlDataSource(fromTable, "Id", "UniqueName", null);
		}

		public static DataTable GetListControlDataSource_Id_UniqueName(string fromTable, string whereClauseSql)
		{
			return GetListControlDataSource(fromTable, "Id", "UniqueName", whereClauseSql);
		}

		public static DataTable GetListControlDataSource_Id_ViewUniqueName(string fromTable)
		{
			return GetListControlDataSource(fromTable, "Id", "ViewUniqueName", null);
		}

		public static DataTable GetListControlDataSource_Id_ViewUniqueName(string fromTable, string whereClauseSql)
		{
			return GetListControlDataSource(fromTable, "Id", "ViewUniqueName", whereClauseSql);
		}

        public static DataTable GetListControlDataSource_Id_ElementsKey(string fromTable, RowStatus.RowStatusId? rowStatusId)
        {
            string whereClauseSql = (rowStatusId == null) ? null : string.Format(" RowStatusId = {0:D}", rowStatusId);
            return GetListControlDataSource(fromTable, "Id", "ElementsKey", whereClauseSql);
        }

		public static object GetColumnValue(string fromTable, string whereClauseSql, string valueColumn)
		{
			DataTable dt = GetDataSource(false, true, fromTable, "", new[] { valueColumn }, whereClauseSql, null);
			if (dt.Rows.Count > 0)
			{
				return dt.Rows[0].ItemArray[0];
			}
			return null;
		}

		public static object GetColumnValue(string fromTable, string idColumn, string valueColumn, int recordId)
		{
			return GetColumnValue(fromTable, string.Format("({0} = {1})", idColumn, recordId), valueColumn);
		}

		public static string GetName(string fromTable, int recordId)
		{
			return GetColumnValue(fromTable, "Id", "Name", recordId) as string;
		}

		public static string GetUniqueName(string fromTable, int recordId)
		{
			return GetColumnValue(fromTable, "Id", "UniqueName", recordId) as string;
		}

		public static string GetViewUniqueName(string fromTable, int recordId)
		{
			return GetColumnValue(fromTable, "Id", "ViewUniqueName", recordId) as string;
		}

		#endregion

		#region InvokeWithSqlCommandLogging Method

		/// <seealso cref="ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(System.Action,object,SubSonic.QueryCommand)"/>
		internal static void InvokeWithSqlCommandLogging(Action action, object logEventSource, SqlCommand query)
		{
			HpfxUtility.InvokeWithCallback(action,
				(startTime, duration, ex) => LogManager.Current.Log(Severity.Debug, logEventSource, () => string.Format("SQL Executed: SqlCommand: Executed in {0}. Query SQL:\n{1}", duration, query.CommandText), ex)
				);
		}

		/// <seealso cref="ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging{TFuncResult}"/>
		internal static TFuncResult InvokeWithSqlCommandLogging<TFuncResult>(Func<TFuncResult> action, object logEventSource, SqlCommand query)
		{
			return HpfxUtility.InvokeWithCallback(action,
				(startTime, duration, ex, results) => LogManager.Current.Log(Severity.Debug, logEventSource, () => string.Format("SQL Executed: SqlCommand: Executed in {0}. Query SQL:\n{1}", duration, query.CommandText), ex)
				);
		}

		#endregion

	}
}
