using System.ComponentModel;
using System.Data;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapWorkflowModuleController class.
	/// </summary>
	public partial class VwMapWorkflowModuleController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapWorkflowModuleCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{

			WorkflowModuleQuerySpecification qs = new WorkflowModuleQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount( string serializedQuerySpecificationXml)
		{
			WorkflowModuleQuerySpecification qs = new WorkflowModuleQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapWorkflowModuleCollection Fetch(WorkflowModuleQuerySpecification qs)
		{
			if (qs.Tags != null)
			{
				VwMapWorkflowModuleCollection recordCollection = new VwMapWorkflowModuleCollection();
				string tagFilteringSql = WorkflowModuleController.GetSql_FilterWorkflowModulesByTagNames(VwMapWorkflowModule.Columns.Id, qs.Tags);

				if (string.IsNullOrEmpty(tagFilteringSql) || (tagFilteringSql == SqlUtility.SqlExpressionAlwaysTrue))
				{
					//execute the query normally (i.e. via standard SubSonic Query and without any tag filtering)
				}
				else if (tagFilteringSql == SqlUtility.SqlExpressionAlwaysFalse)
				{
					//return zero immediately to optimize performance, because executing the query would always return 0
					return recordCollection;
				}
				else
				{
					//replace the simple WHERE clause placeholder with the actual WHERE clause(s)
					SqlQuery query = CreateQuery(qs, false);

					//add a simple WHERE clause that will be manually replaced later
					SubSonic.Constraint placeholderConstraint = query.AndWhere(VwMapWorkflowModule.Columns.Id);
					query = placeholderConstraint.IsLessThan(0);

					QueryCommand qc = ReflectionUtility.InvokeMethod<QueryCommand>(query, "BuildCommand"); //query.BuildCommand();
					DataProvider dataProvider = DataService.Providers[query.ProviderName];
					IDbCommand dbCommand = dataProvider.GetCommand(qc);

					string sqlPlaceholder = "[dbo].[vwMapWorkflowModule].[Id] < " + placeholderConstraint.ParameterName; //the generated SQL for the preceding WHERE clause
					int index = dbCommand.CommandText.IndexOf(sqlPlaceholder);
					if (index >= 0)
					{
						dbCommand.CommandText = dbCommand.CommandText.Replace(sqlPlaceholder, tagFilteringSql);
					}

					LogManager.Current.Log(Severity.Debug, typeof(VwMapWorkflowController), "SubSonic Query SQL modified.\nNew SQL:\n" + query);

					//load recordCollection by executing dbCommand directly
					dbCommand.Connection = dataProvider.CreateConnection();
					recordCollection.LoadAndCloseReader(dbCommand.ExecuteReader());
					return recordCollection;
				}
			}

			return CreateQuery(qs, false).ExecuteAsCollection<VwMapWorkflowModuleCollection>();
		}

        public static DataTable FetchToDataTable(WorkflowModuleQuerySpecification qs)
        {
            if (qs.Tags != null)
            {
                DataTable recordCollection = new DataTable();
                string tagFilteringSql = WorkflowModuleController.GetSql_FilterWorkflowModulesByTagNames(VwMapWorkflowModule.Columns.Id, qs.Tags);

                if (string.IsNullOrEmpty(tagFilteringSql) || (tagFilteringSql == SqlUtility.SqlExpressionAlwaysTrue))
                {
                    //execute the query normally (i.e. via standard SubSonic Query and without any tag filtering)
                }
                else if (tagFilteringSql == SqlUtility.SqlExpressionAlwaysFalse)
                {
                    //return zero immediately to optimize performance, because executing the query would always return 0
                    return recordCollection;
                }
                else
                {
                    //replace the simple WHERE clause placeholder with the actual WHERE clause(s)
                    SqlQuery query = CreateQuery(qs, false);

                    //add a simple WHERE clause that will be manually replaced later
                    SubSonic.Constraint placeholderConstraint = query.AndWhere(VwMapWorkflowModule.Columns.Id);
                    query = placeholderConstraint.IsLessThan(0);

                    QueryCommand qc = ReflectionUtility.InvokeMethod<QueryCommand>(query, "BuildCommand"); //query.BuildCommand();
                    DataProvider dataProvider = DataService.Providers[query.ProviderName];
                    IDbCommand dbCommand = dataProvider.GetCommand(qc);

                    string sqlPlaceholder = "[dbo].[vwMapWorkflowModule].[Id] < " + placeholderConstraint.ParameterName; //the generated SQL for the preceding WHERE clause
                    int index = dbCommand.CommandText.IndexOf(sqlPlaceholder);
                    if (index >= 0)
                    {
                        dbCommand.CommandText = dbCommand.CommandText.Replace(sqlPlaceholder, tagFilteringSql);
                    }

                    LogManager.Current.Log(Severity.Debug, typeof(VwMapWorkflowController), "SubSonic Query SQL modified.\nNew SQL:\n" + query);

                    //load recordCollection by executing dbCommand directly
                    dbCommand.Connection = dataProvider.CreateConnection();
                    recordCollection.Load(dbCommand.ExecuteReader());
                    return recordCollection;
                }
            }

            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }
		public static int FetchCount(WorkflowModuleQuerySpecification qs)
		{
			if (qs.Tags != null)
			{
				VwMapWorkflowModuleCollection recordCollection = new VwMapWorkflowModuleCollection();
				string tagFilteringSql = WorkflowModuleController.GetSql_FilterWorkflowModulesByTagNames(VwMapWorkflowModule.Columns.Id, qs.Tags);

				if (string.IsNullOrEmpty(tagFilteringSql) || (tagFilteringSql == SqlUtility.SqlExpressionAlwaysTrue))
				{
					//execute the query normally (i.e. via standard SubSonic Query and without any tag filtering)
				}
				else if (tagFilteringSql == SqlUtility.SqlExpressionAlwaysFalse)
				{
					//return zero immediately to optimize performance, because executing the query would always return 0
					return 0;
				}
				else
				{
					//replace the simple WHERE clause placeholder with the actual WHERE clause(s)
					SqlQuery query = CreateQuery(qs, true);

					//add a simple WHERE clause that will be manually replaced later
					SubSonic.Constraint placeholderConstraint = query.AndWhere(VwMapWorkflowModule.Columns.Id);
					query = placeholderConstraint.IsLessThan(0);

					QueryCommand qc = ReflectionUtility.InvokeMethod<QueryCommand>(query, "BuildCommand"); //query.BuildCommand();
					DataProvider dataProvider = DataService.Providers[query.ProviderName];
					IDbCommand dbCommand = dataProvider.GetCommand(qc);

					string sqlPlaceholder = "[dbo].[vwMapWorkflowModule].[Id] < " + placeholderConstraint.ParameterName; //the generated SQL for the preceding WHERE clause
					int index = dbCommand.CommandText.IndexOf(sqlPlaceholder);
					if (index >= 0)
					{
						dbCommand.CommandText = dbCommand.CommandText.Replace(sqlPlaceholder, tagFilteringSql);
					}

					LogManager.Current.Log(Severity.Debug, typeof(VwMapWorkflowModuleController), "SubSonic Query SQL modified.\nNew SQL:\n" + query);

					//load recordCollection by executing dbCommand directly
					return SubSonicUtility.GetCountInsteadOfRecords(dataProvider, dbCommand);
				}
			}

			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowModuleQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return WorkflowModuleController.CreateQueryHelper(qs, VwMapWorkflowModule.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
