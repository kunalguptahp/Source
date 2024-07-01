using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the ConfigurationServiceApplicationTypeController class.
	/// </summary>
	public partial class ConfigurationServiceApplicationTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static ConfigurationServiceApplicationTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceApplicationTypeQuerySpecification qs = new ConfigurationServiceApplicationTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount( string serializedQuerySpecificationXml)
		{
			ConfigurationServiceApplicationTypeQuerySpecification qs = new ConfigurationServiceApplicationTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceApplicationTypeCollection Fetch(ConfigurationServiceApplicationTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceApplicationTypeCollection>();
		}

        public static DataTable FetchToDataTable(ConfigurationServiceApplicationTypeQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(ConfigurationServiceApplicationTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

        public static ConfigurationServiceApplicationType FetchByName(string name)
        {
            SqlQuery query = DB.Select().From(ConfigurationServiceApplicationType.Schema);
            ConfigurationServiceApplicationTypeQuerySpecification qs = new ConfigurationServiceApplicationTypeQuerySpecification();

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(ConfigurationServiceApplicationTypeController));
            ConfigurationServiceApplicationType instance = query.ExecuteSingle<ConfigurationServiceApplicationType>();
            return instance;
        }

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceApplicationTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceApplicationTypeController.CreateQueryHelper(qs, ConfigurationServiceApplicationType.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceApplicationTypeQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceApplicationTypeController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceApplicationTypeQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceApplicationTypeController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceApplicationTypeController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
