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
	/// The non-generated portion of the ConfigurationServiceGroupSelectorQueryParameterValueController class.
	/// </summary>
	public partial class ConfigurationServiceGroupSelectorQueryParameterValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ConfigurationServiceGroupSelectorQueryParameterValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs = new ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs = new ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceGroupSelectorQueryParameterValueCollection Fetch(ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceGroupSelectorQueryParameterValueCollection>();
		}

		public static int FetchCount(ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceGroupSelectorQueryParameterValueController.CreateQueryHelper(qs, ConfigurationServiceGroupSelectorQueryParameterValue.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			if (qs.ConfigurationServiceGroupSelectorId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ConfigurationServiceGroupSelectorId", qs.ConfigurationServiceGroupSelectorId);
			}

			if (qs.QueryParameterId != null)
			{
				ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "QueryParameterId", qs.QueryParameterId);
			}

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceGroupSelectorQueryParameterValueController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceGroupSelectorQueryParameterValueController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceGroupSelectorQueryParameterValueController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
