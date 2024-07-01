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
	/// The non-generated portion of the ConfigurationServiceQueryParameterValueImportController class.
	/// </summary>
	public partial class ConfigurationServiceQueryParameterValueImportController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ConfigurationServiceQueryParameterValueImportCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceQueryParameterValueImportQuerySpecification qs = new ConfigurationServiceQueryParameterValueImportQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceQueryParameterValueImportQuerySpecification qs = new ConfigurationServiceQueryParameterValueImportQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceQueryParameterValueImportCollection Fetch(ConfigurationServiceQueryParameterValueImportQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceQueryParameterValueImportCollection>();
		}

		public static int FetchCount(ConfigurationServiceQueryParameterValueImportQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceQueryParameterValueImportQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceQueryParameterValueImportController.CreateQueryHelper(qs, ConfigurationServiceQueryParameterValueImport.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceQueryParameterValueImportQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

            if (qs.ConfigurationServiceGroupImportId != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ConfigurationServiceGroupImportId", qs.ConfigurationServiceGroupImportId);
            }

            ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceQueryParameterValueImportController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceQueryParameterValueImportQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceQueryParameterValueImportController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceQueryParameterValueImportController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
