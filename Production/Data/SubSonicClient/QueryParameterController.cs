using System.ComponentModel;
using System.Data;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the QueryParameterController class.
	/// </summary>
	public partial class QueryParameterController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static QueryParameterCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			QueryParameterQuerySpecification qs = new QueryParameterQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			QueryParameterQuerySpecification qs = new QueryParameterQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static QueryParameterCollection Fetch(QueryParameterQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<QueryParameterCollection>();
		}

		public static int FetchCount(QueryParameterQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterController.CreateQueryHelper(qs, QueryParameter.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(QueryParameterQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (qs.ProxyURLTypeId != null)
			{
				query.InnerJoin(Tables.QueryParameterProxyURLType, QueryParameterProxyURLType.Columns.QueryParameterId,
				                Views.VwMapQueryParameter, QueryParameter.Columns.Id);
				query.AndWhere(QueryParameterProxyURLType.Columns.ProxyURLTypeId).IsEqualTo(qs.ProxyURLTypeId);
			}

			if (qs.ConfigurationServiceGroupTypeId != null)
			{
				query.InnerJoin(Tables.QueryParameterConfigurationServiceGroupType, QueryParameterConfigurationServiceGroupType.Columns.QueryParameterId,
								Views.VwMapQueryParameter, QueryParameter.Columns.Id);
				query.AndWhere(QueryParameterConfigurationServiceGroupType.Columns.ConfigurationServiceGroupTypeId).IsEqualTo(qs.ConfigurationServiceGroupTypeId);
			}

			if (qs.WorkflowTypeId != null)
			{
				query.InnerJoin(Tables.QueryParameterWorkflowType, QueryParameterWorkflowType.Columns.QueryParameterId,
								Views.VwMapQueryParameter, QueryParameter.Columns.Id);
				query.AndWhere(QueryParameterWorkflowType.Columns.WorkflowTypeId).IsEqualTo(qs.WorkflowTypeId);
			}

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(QueryParameterController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(QueryParameterQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapQueryParameterController.Fetch(qs);
			}
			else
			{
				return QueryParameterController.Fetch(qs);
			}
		}

		#endregion

		#region Other Querying Methods

		public static ProxyURLTypeCollection GetProxyURLTypeCollection(int queryParameterId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[ProxyURLType] INNER JOIN [QueryParameter_ProxyURLType] ON [ProxyURLType].[Id] = [QueryParameter_ProxyURLType].[ProxyURLTypeId] WHERE [QueryParameter_ProxyURLType].[QueryParameterId] = @QueryParameterId", ProxyURLType.Schema.Provider.Name);
			cmd.AddParameter("@QueryParameterId", queryParameterId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			ProxyURLTypeCollection coll = new ProxyURLTypeCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}

		public static ConfigurationServiceGroupTypeCollection GetConfigurationServiceGroupTypeCollection(int queryParameterId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[ConfigurationServiceGroupType] INNER JOIN [QueryParameter_ConfigurationServiceGroupType] ON [ConfigurationServiceGroupType].[Id] = [QueryParameter_ConfigurationServiceGroupType].[ConfigurationServiceGroupTypeId] WHERE [QueryParameter_ConfigurationServiceGroupType].[QueryParameterId] = @QueryParameterId", ConfigurationServiceGroupType.Schema.Provider.Name);
			cmd.AddParameter("@QueryParameterId", queryParameterId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			ConfigurationServiceGroupTypeCollection coll = new ConfigurationServiceGroupTypeCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}

        public static JumpstationGroupTypeCollection GetJumpstationGroupTypeCollection(int queryParameterId)
        {
            SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[JumpstationGroupType] INNER JOIN [QueryParameter_JumpstationGroupType] ON [JumpstationGroupType].[Id] = [QueryParameter_JumpstationGroupType].[JumpstationGroupTypeId] WHERE [QueryParameter_JumpstationGroupType].[QueryParameterId] = @QueryParameterId", JumpstationGroupType.Schema.Provider.Name);
            cmd.AddParameter("@QueryParameterId", queryParameterId, DbType.Int32);
            IDataReader rdr = SubSonic.DataService.GetReader(cmd);
            JumpstationGroupTypeCollection coll = new JumpstationGroupTypeCollection();
            coll.LoadAndCloseReader(rdr);
            return coll;
        }

		public static WorkflowTypeCollection GetWorkflowTypeCollection(int queryParameterId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[WorkflowType] INNER JOIN [QueryParameter_WorkflowType] ON [WorkflowType].[Id] = [QueryParameter_WorkflowType].[WorkflowTypeId] WHERE [QueryParameter_WorkflowType].[QueryParameterId] = @QueryParameterId", WorkflowType.Schema.Provider.Name);
			cmd.AddParameter("@QueryParameterId", queryParameterId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			WorkflowTypeCollection coll = new WorkflowTypeCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}

		#endregion

		#endregion

	}
}
