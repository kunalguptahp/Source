using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Data;
using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the GroupTypeController class.
	/// </summary>
	public partial class ConfigurationServiceGroupTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static ConfigurationServiceGroupTypeCollection ODSFetch( string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{ 
			ConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceGroupTypeCollection Fetch(ConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceGroupTypeCollection>();
		}

        public static DataTable FetchToDataTable(ConfigurationServiceGroupTypeQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(ConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

        public static ConfigurationServiceGroupType FetchByName(string name)
        {
            SqlQuery query = DB.Select().From(ConfigurationServiceGroupType.Schema);
            ConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceGroupTypeQuerySpecification();

            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(ConfigurationServiceGroupTypeController));
            ConfigurationServiceGroupType instance = query.ExecuteSingle<ConfigurationServiceGroupType>();
            return instance;
        }

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceGroupTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceGroupTypeController.CreateQueryHelper(qs, ConfigurationServiceGroupType.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceGroupTypeQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

            ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

            if (qs.ConfigurationServiceApplicationId != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ConfigurationServiceApplicationId", qs.ConfigurationServiceApplicationId);
            }

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceGroupTypeController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceGroupTypeQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceGroupTypeController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceGroupTypeController.Fetch(qs);
			}
		}

		#endregion

        #endregion

    }
}
