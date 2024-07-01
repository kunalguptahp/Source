using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Utility;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{

	/// <summary>
	/// A customized SubSonic <see cref="DataProvider"/> that extends the generic <see cref="SqlDataProvider"/> with application-specific functionality.
	/// </summary>
	/// <remarks>
	/// The most significant aspect of this class is that it overrides many of the base class' DB command-related methods in order to wrap the inherited functionality 
	/// with app-specific error handling logic (primarily for handling DB deadlock exceptions).
	/// </remarks>
	public class ElementsCPSSqlDataProvider : SqlDataProvider
	{

		//TODO: Refactoring: Migrate to HPFx
#warning Refactoring: Migrate to HPFx

		#region QueryCommand-related Method Overrides

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <param name="qry"></param>
		/// <returns></returns>
		public override int ExecuteQuery(QueryCommand qry)
		{
			return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.ExecuteQuery(qry), this, qry), this);
		}

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <param name="qry"></param>
		/// <returns></returns>
		public override object ExecuteScalar(QueryCommand qry)
		{
			return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.ExecuteScalar(qry), this, qry), this);
		}

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <param name="commands"></param>
		public override void ExecuteTransaction(QueryCommandCollection commands)
		{
			ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.ExecuteTransaction(commands), this, commands), this);
		}

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <param name="qry"></param>
		/// <returns></returns>
		public override System.Data.IDbCommand GetCommand(QueryCommand qry)
		{
			return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetCommand(qry), this, qry), this);
		}

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <param name="qry"></param>
		/// <returns></returns>
		public override System.Data.Common.DbCommand GetDbCommand(QueryCommand qry)
		{
			return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetDbCommand(qry), this, qry), this);
		}

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="qry"></param>
		/// <returns></returns>
		public override T GetDataSet<T>(QueryCommand qry)
		{
			return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetDataSet<T>(qry), this, qry), this);
		}

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <param name="qry"></param>
		/// <returns></returns>
		public override System.Data.DataSet GetDataSet(QueryCommand qry)
		{
			return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetDataSet(qry), this, qry), this);
		}

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <param name="qry"></param>
		/// <returns></returns>
		public override System.Data.IDataReader GetReader(QueryCommand qry)
		{
			
			return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetReader(qry), this, qry), this);
		}

		/// <summary>
		/// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		/// </summary>
		/// <param name="qry"></param>
		/// <returns></returns>
		public override System.Data.IDataReader GetSingleRecordReader(QueryCommand qry)
		{
			return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetSingleRecordReader(qry), this, qry), this);
		}

		#endregion

		#region Query-related Method Overrides

		///// <summary>
		///// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		///// </summary>
		///// <param name="qry"></param>
		///// <param name="table"></param>
		///// <param name="updateSql"></param>
		///// <returns></returns>
		//protected override string AdjustUpdateSql(Query qry, TableSchema.Table table, string updateSql)
		//{
		//   return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.AdjustUpdateSql(qry, table, updateSql), this, qry), this);
		//}

		///// <summary>
		///// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		///// </summary>
		///// <param name="qry"></param>
		///// <returns></returns>
		//public override string GetInsertSql(Query qry)
		//{
		//   return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetInsertSql(qry), this, qry), this);
		//}

		///// <summary>
		///// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		///// </summary>
		///// <param name="qry"></param>
		///// <returns></returns>
		//public override int GetRecordCount(Query qry)
		//{
		//   return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetRecordCount(qry), this, qry), this);
		//}

		///// <summary>
		///// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		///// </summary>
		///// <param name="qry"></param>
		///// <returns></returns>
		//public override string GetSelectSql(Query qry)
		//{
		//   return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetSelectSql(qry), this, qry), this);
		//}

		#endregion

		#region SqlQuery-related Method Overrides

		///// <summary>
		///// Overridden. Enhances the inherited implementation with query logging and improved deadlock error handling.
		///// </summary>
		///// <param name="sqlQuery"></param>
		///// <returns></returns>
		//public override ISqlGenerator GetSqlGenerator(SqlQuery sqlQuery)
		//{
		//   return ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSubSonicUtility.InvokeWithSubSonicQueryLogging(() => base.GetSqlGenerator(sqlQuery), this, qry), this);
		//}

		#endregion

		#region Other Method Overrides

		#endregion


	}
}