using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.SubSonicClient;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Utility;
using MbUnit.Framework;
using SubSonic;

namespace HP.ElementsCPS.Data.Tests.Utility
{
	public static class DataUtility
	{

		#region Constants

		private const string DBScript_CleanSchema = @"Utility\DB.Clean.Schema.sql";
		private const string DBScript_CleanData = @"Utility\DB.Clean.Data.sql";
		//private const string DBScript_SetupSchema = @"Setup\ElementsCPSDB.Setup.Schema.sql";
		private const string DBScript_BootstrapData = @"Setup\ElementsCPSDB.Setup.Data.TestData.001.BootstrapData.sql";
		private const string DBScript_RepresentativeData = @"Setup\ElementsCPSDB.Setup.Data.TestData.002b.RepresentativeTestData.sql";
		private const string DBScript_SimpleData = @"Setup\ElementsCPSDB.Setup.Data.TestData.002a.SimpleTestData.sql";
		private const string DBScript_UnitTestData = @"Setup\ElementsCPSDB.Setup.Data.TestData.002e.UnitTestData.sql";
		private const string DBScript_SortingPagingData = @"Setup\ElementsCPSDB.Setup.Data.TestData.001b.SortingPagingTestData.sql";
		private const string DBScript_FetchMethodTestData = @"Setup\ElementsCPSDB.Setup.Data.TestData.002f.FetchMethodTestData.sql";

		#endregion

		#region Properties

		/// <summary>
		/// This field is used to cache scripts for performance.
		/// </summary>
		private static Dictionary<string, string> _ScriptCache = new Dictionary<string, string>();

		#endregion

		#region RestoreDB Methods

		/// <summary>
		/// Restores/initializes the DB to a "known" state.
		/// </summary>
		public static void RestoreDBSchema_NoSchema()
		{
			LogManager.Current.Log(Severity.Debug, typeof(DataUtility), "Entering DataUtility.RestoreDBSchema_NoSchema().");
			ExecuteSqlScriptFile(DBScript_CleanSchema);
		}

		/// <summary>
		/// Restores/initializes the DB to a "known" state.
		/// </summary>
		public static void RestoreDB_NoData()
		{
			LogManager.Current.Log(Severity.Debug, typeof(DataUtility), "Entering DataUtility.RestoreDB_NoData().");
			ExecuteSqlScriptFile(DBScript_CleanData);
		}

		/// <summary>
		/// Restores/initializes the DB to a "known" state.
		/// </summary>
		public static void RestoreDB_BootstrapData()
		{
			LogManager.Current.Log(Severity.Debug, typeof(DataUtility), "Entering DataUtility.RestoreDB_BootstrapData().");
			RestoreDB_NoData();
			ExecuteSqlScriptFile(DBScript_BootstrapData);
		}

		/// <summary>
		/// Restores/initializes the DB to a "known" state.
		/// </summary>
		public static void RestoreDB_RepresentativeData()
		{
			LogManager.Current.Log(Severity.Debug, typeof(DataUtility), "Entering DataUtility.RestoreDB_RepresentativeData().");
			RestoreDB_BootstrapData();
			ExecuteSqlScriptFile(DBScript_RepresentativeData);
		}

		/// <summary>
		/// Restores/initializes the DB to a "known" state.
		/// </summary>
		public static void RestoreDB_SimpleData()
		{
			LogManager.Current.Log(Severity.Debug, typeof(DataUtility), "Entering DataUtility.RestoreDB_SimpleData().");
			RestoreDB_BootstrapData();
			ExecuteSqlScriptFile(DBScript_SimpleData);
		}

		/// <summary>
		/// Restores/initializes the DB to a "known" state.
		/// </summary>
		public static void RestoreDB_UnitTestData()
		{
			LogManager.Current.Log(Severity.Debug, typeof(DataUtility), "Entering DataUtility.RestoreDB_UnitTestData().");
			RestoreDB_BootstrapData();
			ExecuteSqlScriptFile(DBScript_UnitTestData);
		}

		/// <summary>
		/// Restores/initializes the DB to a "known" state.
		/// </summary>
		public static void RestoreDB_SortingPagingData()
		{
			LogManager.Current.Log(Severity.Debug, typeof(DataUtility), "Entering DataUtility.RestoreDB_SortingPagingData().");
			RestoreDB_NoData();
			ExecuteSqlScriptFile(DBScript_SortingPagingData);
		}

		/// <summary>
		/// Restores/initializes the DB to a "known" state.
		/// </summary>
		public static void RestoreDB_FetchMethodTestData()
		{
			LogManager.Current.Log(Severity.Debug, typeof(DataUtility), "Entering DataUtility.RestoreDB_FetchMethodTestData().");
			RestoreDB_BootstrapData();
			ExecuteSqlScriptFile(DBScript_FetchMethodTestData);
		}

		#endregion

		#region SetUserRoles Methods

		/// <summary>
		/// Sets/updates a specified user's roles.
		/// </summary>
		/// <param name="windowsId">The WindowsId of the user to update.</param>
		/// <param name="roles">The new set of roles for the user.</param>
		public static void SetUserRoles(string windowsId, List<UserRoleId> roles)
		{
			Person person = PersonController.GetByWindowsID(windowsId);
			person.SetRoles(roles);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		/// <param name="windowsId"></param>
		/// <param name="roles"></param>
		public static void SetUserRoles(string windowsId, params UserRoleId[] roles)
		{
			SetUserRoles(windowsId, new List<UserRoleId>(roles));
		}

		#endregion

		#region SetTestUserRoles Methods

		/// <summary>
		/// Sets/updates the roles of the "test user" account.
		/// </summary>
		/// <param name="roles">The new set of roles for the user.</param>
		public static void SetTestUserRoles(List<UserRoleId> roles)
		{
			string windowsId = GetTestUserWindowsId();
			SetUserRoles(windowsId, roles);
		}

		/// <summary>
		/// Convenience overload.
		/// </summary>
		public static void SetTestUserRoles(params UserRoleId[] roles)
		{
			SetTestUserRoles(new List<UserRoleId>(roles));
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Executes a specified SQL script file (from the project's existing DB scripts) on the DB.
		/// </summary>
		/// <param name="scriptFile">The name (or relative path) of the script file to execute.</param>
		private static void ExecuteSqlScriptFile(string scriptFile)
		{
			string sql = GetSqlScript(scriptFile);

			string connectionString = GetTestDBConnectionString();
			int rowsAffected = SqlUtility.ExecuteAsNonQuery(connectionString, sql);
		}

		/// <summary>
		/// Gets a specified script from the cache (updating the cache as needed).
		/// </summary>
		/// <param name="scriptFile">The name (or relative path) of the script file to execute.</param>
		private static string GetSqlScript(string scriptFile)
		{
			string sql;
			if (_ScriptCache.ContainsKey(scriptFile))
			{
				sql = _ScriptCache[scriptFile];
			}
			else
			{
				//cache miss. need to update the cache
				sql = CacheScript(scriptFile);
			}
			return sql;
		}

		/// <summary>
		/// Loads a specified key (and it's value) into the _ScriptCache .
		/// </summary>
		/// <param name="scriptFile">The name (or relative path) of the script file to execute.</param>
		/// <returns>the key's value</returns>
		private static string CacheScript(string scriptFile)
		{
			string sql;
			lock (_ScriptCache)
			{
				//check again
				if (_ScriptCache.ContainsKey(scriptFile))
				{
					//cache was updated before the lock was obtained
					sql = _ScriptCache[scriptFile];
				}
				else
				{
					//update the cache
					sql = GetSqlScriptFromFile(scriptFile);
					_ScriptCache.Add(scriptFile, sql);
				}
			}
			return sql;
		}

		/// <summary>
		/// Gets a specified script file's contents.
		/// </summary>
		/// <param name="scriptFile">The name (or relative path) of the script file to execute.</param>
		private static string GetSqlScriptFromFile(string scriptFile)
		{
			string sql = File.ReadAllText(Path.Combine(GetScriptFolderPath(), scriptFile));

			//convert all GO statements into semicolons
			sql = SqlUtility.ConvertSql_ReplaceGoWithSemicolon("\n" + sql + "\n");

			return sql;
		}

		/// <summary>
		/// Returns the connection string that tests should use to connect to the DB.
		/// </summary>
		/// <returns></returns>
		private static string GetTestDBConnectionString()
		{
			return ElementsCPSSqlUtility.GetDefaultConnectionString();
		}

		/// <summary>
		/// Returns the file system path of the ElementsCPS DB scripts root folder.
		/// </summary>
		/// <returns></returns>
		private static string GetScriptFolderPath()
		{
			return ConfigurationManager.AppSettings["DBScriptPath"]; // "C:\...\ElementsCPS\trunk\Source\Production\DB\ElementsCPSDB\Scripts\";
		}

		/// <summary>
		/// Returns the Windows ID of the user account that autotests use to authenticate during testing.
		/// </summary>
		/// <returns></returns>
		public static string GetTestUserWindowsId()
		{
			return ConfigurationManager.AppSettings["TestUserWindowsId"];
		}

		public static void AssertColumnPropertyWorks(IActiveRecord instance, string columnName)
		{
			AssertColumnPropertyWorksHelper(instance, columnName);
		}

		private static void AssertColumnPropertyWorksHelper(object instance, string columnName)
		{
			TableSchema.TableColumn column = GetColumn(instance, columnName);
			if (column != null)
			{
				Assert.AreEqual(columnName, string.Format(CultureInfo.InvariantCulture, "{0}Column", column.ColumnName));
			}
		}

		private static TableSchema.TableColumn GetColumn(object instance, string name)
		{
			return ReflectionUtility.GetPropertyValue<TableSchema.TableColumn>(instance, name);
		}

		#endregion

		/// <summary>
		/// Returns the file system path of the ElementsCPS source code root folder.
		/// </summary>
		/// <returns></returns>
		public static string GetElementsCPSSourceRootPath()
		{
			return ConfigurationManager.AppSettings["SourceRootPath"]; // "C:\...\ElementsCPS\trunk\Source\";
		}

		#region Test Data Factory Methods

		internal static IEnumerable<UserRoleId[]> UserRoleFactoryHelper_SingleRolesAndSpecialRolePairs()
		{
			//single roles
			yield return new[] { UserRoleId.AccountLocked };
			yield return new[] { UserRoleId.Administrator };
			yield return new[] { UserRoleId.Coordinator };
			yield return new[] { UserRoleId.DataAdmin };
			yield return new[] { UserRoleId.Editor };
			yield return new[] { UserRoleId.Everyone };
			yield return new[] { UserRoleId.None };
			yield return new[] { UserRoleId.RestrictedAccess };
			yield return new[] { UserRoleId.TechSupport };
			yield return new[] { UserRoleId.UserAdmin };
			yield return new[] { UserRoleId.Validator };
			yield return new[] { UserRoleId.Viewer };

			//role pairs: AccountLocked + (OtherRole)
			//yield return new[] { UserRoleId.AccountLocked, UserRoleId.AccountLocked };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.Administrator };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.Coordinator };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.DataAdmin };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.Editor };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.Everyone };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.None };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.RestrictedAccess };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.TechSupport };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.UserAdmin };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.Validator };
			yield return new[] { UserRoleId.AccountLocked, UserRoleId.Viewer };

			//role pairs: RestrictedAccess + (OtherRole)
			//yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.AccountLocked };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.Administrator };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.Coordinator };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.DataAdmin };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.Editor };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.Everyone };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.None };
			//yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.RestrictedAccess };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.TechSupport };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.UserAdmin };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.Validator };
			yield return new[] { UserRoleId.RestrictedAccess, UserRoleId.Viewer };
		}

		internal static IEnumerable<UserRoleId[]> UserRoleFactoryHelper_EachIndividualRole()
		{
			//single roles
			yield return new[] { UserRoleId.AccountLocked };
			yield return new[] { UserRoleId.Administrator };
			yield return new[] { UserRoleId.Coordinator };
			yield return new[] { UserRoleId.DataAdmin };
			yield return new[] { UserRoleId.Editor };
			yield return new[] { UserRoleId.Everyone };
			yield return new[] { UserRoleId.None };
			yield return new[] { UserRoleId.RestrictedAccess };
			yield return new[] { UserRoleId.TechSupport };
			yield return new[] { UserRoleId.UserAdmin };
			yield return new[] { UserRoleId.Validator };
			yield return new[] { UserRoleId.Viewer };
		}

		#endregion

		#region Test Data Helper Methods

		public static string GenerateArrayDeclaration(UserRoleId[] userRoleIds)
		{
			string userRoleSetParam = "";
			foreach (UserRoleId userRoleId in userRoleIds)
			{
				userRoleSetParam += " UserRoleId." + userRoleId.ToString();
			}
			userRoleSetParam = string.Join(", ", userRoleSetParam.Trim().Split(" ".ToCharArray()));
			userRoleSetParam = string.Format(CultureInfo.InvariantCulture, "new[]{{{0}}}", userRoleSetParam);
			return userRoleSetParam;
		}

		#endregion

	}
}
