using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Collections.Generic;
using HP.ElementsCPS.Core.Security;
using System;
using System.Text;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapJumpstationGroup class.
	/// </summary>
	public partial class VwMapJumpstationGroupController
	{

		#region ObjectDataSource Methods

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
           int TenantGroupId=0;
           bool b = int.TryParse(tenantGroupId, out TenantGroupId);
           List<int> appClientIds = TenantController.GetAppClientsInt(TenantGroupId);
           
            JumpstationGroupQuerySpecification qs = new JumpstationGroupQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            qs.AppClientIds = appClientIds;
            //return Fetch(qs);
            return FetchToDataTable(qs);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
        {
            int TenantGroupId = 0;
            bool b = int.TryParse(tenantGroupId, out TenantGroupId);
            List<int> appClientIds = TenantController.GetAppClientsInt(TenantGroupId);
            JumpstationGroupQuerySpecification qs = new JumpstationGroupQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            qs.AppClientIds = appClientIds;
            return FetchCount(qs);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable ODSFetchByQueryParameter(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
            int TenantGroupId = 0;
            bool b = int.TryParse(tenantGroupId, out TenantGroupId);
            List<int> appClientIds = TenantController.GetAppClientsInt(TenantGroupId);
            JumpstationGroupQuerySpecification qs = new JumpstationGroupQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            qs.AppClientIds = appClientIds;
            return FetchByQueryParameter(qs);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCountByQueryParameter(string tenantGroupId, string serializedQuerySpecificationXml)
        {
            int TenantGroupId = 0;
            bool b = int.TryParse(tenantGroupId, out TenantGroupId);
            List<int> appClientIds = TenantController.GetAppClientsInt(TenantGroupId);
            JumpstationGroupQuerySpecification qs = new JumpstationGroupQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            qs.AppClientIds = appClientIds;
            return FetchCountByQueryParameter(qs);
        }

		#endregion

		#region QuerySpecification-related Methods

        public static VwMapJumpstationGroupCollection Fetch( JumpstationGroupQuerySpecification qs)
		{
            VwMapJumpstationGroupCollection recordCollection = null;
			if (qs.Tags != null)
			{
				 recordCollection = new VwMapJumpstationGroupCollection();
				string tagFilteringSql = JumpstationGroupController.GetSql_FilterJumpstationGroupsByTagNames(VwMapJumpstationGroup.Columns.Id, qs.Tags);

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
					SubSonic.Constraint placeholderConstraint = query.AndWhere(VwMapJumpstationGroup.Columns.Id);
					query = placeholderConstraint.IsLessThan(0);

					QueryCommand qc = ReflectionUtility.InvokeMethod<QueryCommand>(query, "BuildCommand"); //query.BuildCommand();
					DataProvider dataProvider = DataService.Providers[query.ProviderName];
					IDbCommand dbCommand = dataProvider.GetCommand(qc);

					string sqlPlaceholder = "[dbo].[vwMapJumpstationGroup].[Id] < " + placeholderConstraint.ParameterName; //the generated SQL for the preceding WHERE clause
					int index = dbCommand.CommandText.IndexOf(sqlPlaceholder);
					if (index >= 0)
					{
						dbCommand.CommandText = dbCommand.CommandText.Replace(sqlPlaceholder, tagFilteringSql);
					}

					LogManager.Current.Log(Severity.Debug, typeof(VwMapJumpstationGroupController), "SubSonic Query SQL modified.\nNew SQL:\n" + query);

					//load recordCollection by executing dbCommand directly
					dbCommand.Connection = dataProvider.CreateConnection();
					recordCollection.LoadAndCloseReader(dbCommand.ExecuteReader());
					return recordCollection;
				}
			}
           
            return CreateQuery(qs, false).ExecuteAsCollection<VwMapJumpstationGroupCollection>();
		}

        /// <summary>
        /// return datatable 
        /// </summary>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static DataTable FetchToDataTable(JumpstationGroupQuerySpecification qs)
        {
            if (qs.Tags != null)
            {

                DataTable recordCollection = new DataTable();

                string tagFilteringSql = JumpstationGroupController.GetSql_FilterJumpstationGroupsByTagNames(VwMapJumpstationGroup.Columns.Id, qs.Tags);

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
                    SubSonic.Constraint placeholderConstraint = query.AndWhere(VwMapJumpstationGroup.Columns.Id);
                    query = placeholderConstraint.IsLessThan(0);

                    QueryCommand qc = ReflectionUtility.InvokeMethod<QueryCommand>(query, "BuildCommand"); //query.BuildCommand();
                    DataProvider dataProvider = DataService.Providers[query.ProviderName];
                    IDbCommand dbCommand = dataProvider.GetCommand(qc);

                    string sqlPlaceholder = "[dbo].[vwMapJumpstationGroup].[Id] < " + placeholderConstraint.ParameterName; //the generated SQL for the preceding WHERE clause
                    int index = dbCommand.CommandText.IndexOf(sqlPlaceholder);
                    if (index >= 0)
                    {
                        dbCommand.CommandText = dbCommand.CommandText.Replace(sqlPlaceholder, tagFilteringSql);
                    }

                    LogManager.Current.Log(Severity.Debug, typeof(VwMapJumpstationGroupController), "SubSonic Query SQL modified.\nNew SQL:\n" + query);

                    //load recordCollection by executing dbCommand directly
                    dbCommand.Connection = dataProvider.CreateConnection();
                    recordCollection.Load(dbCommand.ExecuteReader());
                    return recordCollection;
                }
            }

            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }


		public static int FetchCount(JumpstationGroupQuerySpecification qs)
		{
			if (qs.Tags != null)
			{
				VwMapJumpstationGroupCollection recordCollection = new VwMapJumpstationGroupCollection();
				string tagFilteringSql = JumpstationGroupController.GetSql_FilterJumpstationGroupsByTagNames(VwMapJumpstationGroup.Columns.Id, qs.Tags);

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
					SubSonic.Constraint placeholderConstraint = query.AndWhere(VwMapJumpstationGroup.Columns.Id);
					query = placeholderConstraint.IsLessThan(0);

					QueryCommand qc = ReflectionUtility.InvokeMethod<QueryCommand>(query, "BuildCommand"); //query.BuildCommand();
					DataProvider dataProvider = DataService.Providers[query.ProviderName];
					IDbCommand dbCommand = dataProvider.GetCommand(qc);

					string sqlPlaceholder = "[dbo].[vwMapJumpstationGroup].[Id] < " + placeholderConstraint.ParameterName; //the generated SQL for the preceding WHERE clause
					int index = dbCommand.CommandText.IndexOf(sqlPlaceholder);
					if (index >= 0)
					{
						dbCommand.CommandText = dbCommand.CommandText.Replace(sqlPlaceholder, tagFilteringSql);
					}

					LogManager.Current.Log(Severity.Debug, typeof(VwMapJumpstationGroupController), "SubSonic Query SQL modified.\nNew SQL:\n" + query);

					//load recordCollection by executing dbCommand directly
					return SubSonicUtility.GetCountInsteadOfRecords(dataProvider, dbCommand);
				}
			}

			return CreateQuery(qs, true).GetRecordCount();
		}

        public static DataTable FetchByQueryParameter(JumpstationGroupQuerySpecification qs)
        {
            int pageIndex = qs.Paging.PageIndex ?? 0;
            int pageSize = qs.Paging.PageSize ?? QueryPaginationSpecification.DefaultPageSize;
            int startRowIndex = (pageIndex*pageSize) + 1;

            int statusId = (qs.JumpstationGroupStatusId.HasValue ? qs.JumpstationGroupStatusId.Value : 0);
            int typeId = (qs.JumpstationGroupTypeId.HasValue ? qs.JumpstationGroupTypeId.Value : 0);
            string delimitedIdList = qs.QueryParameterValueIdDelimitedList;

            //if (qs.SortBy.Count > 0)
            //{
            //    foreach (QuerySortDirection qsd in qs.SortBy)
            //    {
            //        string orderByExpression = qsd.SortExpression;
            //        if (qsd.SortDescending != null)
            //        {
            //            orderByExpression += (qsd.SortDescending.Value) ? " DESC" : " ASC";
            //        }
            //        //query.OrderBys.Add(orderByExpression);
            //    }
            //}

            StoredProcedure sp = StoredProcedures.GetJumpstationByQueryParameterValue(0, 0, startRowIndex, pageSize, statusId, typeId, delimitedIdList);
            //Stored Procedure command Is Null... ensure command is created
            string dummy = sp.Command.CommandSql;

            DataTable recordCollection = new DataTable();
            recordCollection.Load(sp.GetReader());
            return recordCollection;
        }

        public static int FetchCountByQueryParameter(JumpstationGroupQuerySpecification qs)
        {
            int statusId = (qs.JumpstationGroupStatusId.HasValue ? qs.JumpstationGroupStatusId.Value : 0);
            int typeId = (qs.JumpstationGroupTypeId.HasValue ? qs.JumpstationGroupTypeId.Value : 0);
            string delimitedIdList = qs.QueryParameterValueIdDelimitedList;

            StoredProcedure sp = StoredProcedures.GetJumpstationByQueryParameterValue(-1, 0, 0, 0, statusId, typeId, delimitedIdList);
            sp.Command.AddReturnParameter();
            sp.Execute();
            return (int)sp.OutputValues[0];
        }

		#region CreateQuery

        private static SqlQuery CreateQuery( JumpstationGroupQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationGroupController.CreateQueryHelper(qs, VwMapJumpstationGroup.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
