using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Utility;

namespace HP.ElementsCPS.Data.Utility
{
	/// <summary>
	/// This class provides the basic data access services for the ElementsCPS system.
	/// All classes that need to access ElementsCPS database should be derived from this
	/// class.  Within the class are standard methods to execute stored
	/// procedures and retrieve the results.
	///
	/// If successful the Reader (or DSet) member will be contain the resulting
	/// recordset. 
	///
	/// When using the SqlDataReader: after use, the developer should call the
	/// Close() method which will release the connection.  Using the DataSet will
	/// automatically close the connection.
	///	</summary>
	public class DataAccess
	{
		private SqlConnection connection;
		public DataSet DSet;
		public SqlDataReader Reader;


		// flags to set result type
		// default is to use the DataSet and not SqlDataReader
		public bool UseDataSet = true;
		public bool UseXmlReader = false;
		public XmlReader XmlReader;

		#region Contructions

		public DataAccess()
		{
			this.CreateConnection();
		}

		public DataAccess(bool useDataSet)
		{
			this.UseDataSet = useDataSet;
			this.CreateConnection();
		}

		public DataAccess(string connectionString)
		{
			this.CreateConnection(connectionString);
		}

		public DataAccess(bool useDataSet, string connectionString)
		{
			this.UseDataSet = useDataSet;
			this.CreateConnection(connectionString);
		}

		#endregion

		#region Methods


		/// <summary>
		/// Returns the default connection string from the configuration settings.
		/// </summary>
		/// <returns></returns>
		private static string GetDefaultConnectionString()
		{
			return SqlUtility.GetConnectionString("ElementsCPSDB");
		}



		private void CreateConnection()
		{
			// Getting connection string to ElementsCPS database
			this.connection = new SqlConnection(GetDefaultConnectionString());
		}


		private void CreateConnection(string connectionString)
		{
			this.connection = new SqlConnection(connectionString);
		}

		public void Close()
		{
			this.connection.Close();
		}

		public void ExecuteStoredProcedure(string procedureName)
		{
			this.ExecuteStoredProcedure(procedureName, null);
		}

		public void ExecuteStoredProcedure(string procedureName,
		                                   SqlParameter[] parameters)
		{
			// Create Sql Command
			var command = new SqlCommand(procedureName, this.connection);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = 180; //default to 3 minutes

			// Add parameters to the stored procedure
			if (parameters != null)
			{
				foreach (SqlParameter p in parameters)
				{
					command.Parameters.Add(p);
				}
			}
			this.ExecuteCommand(command);
		}

		/// <summary>
		/// This method executes stored procedures with output parameters
		/// </summary>
		/// <param name="procedureName">name of the stored procedure to execute</param>
		/// <param name="parameters">intput and output parameter collection (called by reference) </param>
		public void ExecuteStoredProcedure(string procedureName,
		                                   ref SqlParameter[] parameters)
		{
			// Create Sql Command
			var command = new SqlCommand(procedureName, this.connection);
			command.CommandType = CommandType.StoredProcedure;

			// Add parameters to the stored procedure
			if (parameters != null)
			{
				foreach (SqlParameter p in parameters)
				{
					command.Parameters.Add(p);
				}
			}

			//this must be true to get the output parameters back
			this.UseDataSet = true;

			this.ExecuteCommand(command);

			// Get output parameter values back
			if (parameters != null)
			{
				foreach (SqlParameter p in parameters)
				{
					if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput)
						p.Value = command.Parameters[p.ParameterName].Value;
				}
			}
		}

		public void ExecuteQuery(string query)
		{
			// Create SQL Command
			var command = new SqlCommand(query, this.connection);
			command.CommandType = CommandType.Text;
			this.ExecuteCommand(command);
		}


		/// <summary>
		/// This method executes SQL query with one variable. Developer need to user @Parameter in their sql
		/// </summary>
		/// <param name="query">SQL query string</param>
		/// <param name="parameter">Value for @Parameter</param>
		public void ExecuteQuery(string query, string parameter)
		{
			// Create SQL Command
			var command = new SqlCommand(query, this.connection);
			command.CommandType = CommandType.Text;

			// Add parameters
			if (! string.IsNullOrEmpty(parameter))
			{
				var para = new SqlParameter
				           	{
				           		ParameterName = "@Parameter",
				           		SqlDbType = SqlDbType.VarChar,
				           		Value = parameter
				           	};

				command.Parameters.Add(para);
			}

			this.ExecuteCommand(command);
		}


		/// <summary>
		/// This method executes SQL query with one variable. Developer need to user @Parameter in their sql
		/// </summary>
		/// <param name="query">SQL query string</param>
		/// <param name="parameter">Value for @Parameter</param>
		public void ExecuteQuery(string query, int parameter)
		{
			// Create SQL Command
			var command = new SqlCommand(query, this.connection);
			command.CommandType = CommandType.Text;

			
			var para = new SqlParameter
			{
				ParameterName = "@Parameter",
				SqlDbType = SqlDbType.Int,
				Value = parameter
			};

			command.Parameters.Add(para);
			

			this.ExecuteCommand(command);
		}



		private void ExecuteCommand(SqlCommand command)
		{
			// Clear results
			this.Reader = null;
			this.DSet = null;

			// Execute the SqlCommand (with SqlCommandLogging and with DeadlockRedundancy)
			ElementsCPSSubSonicUtility.InvokeWithDeadlockRedundancy(() => ElementsCPSSqlUtility.InvokeWithSqlCommandLogging(() => this.ExecuteCommandHelper(command), this, command), this);
		}

		private void ExecuteCommandHelper(SqlCommand command)
		{
			try
			{
				if (this.connection.State != ConnectionState.Open)
					this.connection.Open();

				if (this.UseDataSet)
				{
					var da = new SqlDataAdapter(command);
					this.DSet = new DataSet();
					da.Fill(this.DSet);
					this.Close();
				}
				else if (this.UseXmlReader)
				{
					this.XmlReader = command.ExecuteXmlReader();
				}
				else
				{
					this.Reader = command.ExecuteReader();
				}
			}
			catch (Exception e)
			{
				//throw the error so the application error handler can catch it to handle
				throw (e);
			}
		}

		#endregion

	}
}