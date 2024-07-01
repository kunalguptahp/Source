using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceApplicationController class.
	/// </summary>
	public partial class ConfigurationServiceApplicationController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ConfigurationServiceApplicationCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceApplicationQuerySpecification qs = new ConfigurationServiceApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceApplicationQuerySpecification qs = new ConfigurationServiceApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceApplicationCollection Fetch(ConfigurationServiceApplicationQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceApplicationCollection>();
		}

		public static int FetchCount(ConfigurationServiceApplicationQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

        public static ConfigurationServiceApplication FetchByName(string name)
        {
            SqlQuery query = DB.Select().From(ConfigurationServiceApplication.Schema);
            ConfigurationServiceApplicationQuerySpecification qs = new ConfigurationServiceApplicationQuerySpecification();

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(ConfigurationServiceApplicationController));
            ConfigurationServiceApplication instance = query.ExecuteSingle<ConfigurationServiceApplication>();
            return instance;
        }

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceApplicationQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceApplicationController.CreateQueryHelper(qs, ConfigurationServiceApplication.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceApplicationQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);


			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceApplicationController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceApplicationQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceApplicationController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceApplicationController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
