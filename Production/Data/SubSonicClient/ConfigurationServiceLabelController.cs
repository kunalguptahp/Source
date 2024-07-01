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
	/// The non-generated portion of the ConfigurationServiceLabelController class.
	/// </summary>
	public partial class ConfigurationServiceLabelController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ConfigurationServiceLabelCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceLabelQuerySpecification qs = new ConfigurationServiceLabelQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceLabelQuerySpecification qs = new ConfigurationServiceLabelQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceLabelCollection Fetch(ConfigurationServiceLabelQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceLabelCollection>();
		}

		public static int FetchCount(ConfigurationServiceLabelQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceLabelQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceLabelController.CreateQueryHelper(qs, ConfigurationServiceLabel.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceLabelQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

			ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

            if (qs.ConfigurationServiceItemId != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ConfigurationServiceItemId", qs.ConfigurationServiceItemId);
            }

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceLabelController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceLabelQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceLabelController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceLabelController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
