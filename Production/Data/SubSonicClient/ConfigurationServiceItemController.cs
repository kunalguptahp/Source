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
	/// The non-generated portion of the GroupTypeController class.
	/// </summary>
	public partial class ConfigurationServiceItemController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ConfigurationServiceItemCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceItemQuerySpecification qs = new ConfigurationServiceItemQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceItemQuerySpecification qs = new ConfigurationServiceItemQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceItemCollection Fetch(ConfigurationServiceItemQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceItemCollection>();
		}

		public static int FetchCount(ConfigurationServiceItemQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceItemQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceItemController.CreateQueryHelper(qs, ConfigurationServiceItem.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceItemQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			if (qs.RowStatusId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "RowStatusId", qs.RowStatusId);
			}

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceItemController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceItemQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceItemController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceItemController.Fetch(qs);
			}
		}

		#endregion

		#region Other Querying Methods

		public static ConfigurationServiceGroupTypeCollection GetConfigurationServiceGroupTypeCollection(int configurationServiceItemId)
		{
			SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[ConfigurationServiceGroupType] WITH (NOLOCK) INNER JOIN [ConfigurationServiceItem_ConfigurationServiceGroupType] WITH (NOLOCK) ON [ConfigurationServiceGroupType].[Id] = [ConfigurationServiceItem_ConfigurationServiceGroupType].[ConfigurationServiceGroupTypeId] WHERE [ConfigurationServiceItem_ConfigurationServiceGroupType].[ConfigurationServiceItemId] = @ConfigurationServiceItemId", ConfigurationServiceGroupType.Schema.Provider.Name);
			cmd.AddParameter("@ConfigurationServiceItemId", configurationServiceItemId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			ConfigurationServiceGroupTypeCollection coll = new ConfigurationServiceGroupTypeCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}

		#endregion


		#endregion

	}
}
