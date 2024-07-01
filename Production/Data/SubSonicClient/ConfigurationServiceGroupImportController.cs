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
	/// The non-generated portion of the GroupImportController class.
	/// </summary>
	public partial class ConfigurationServiceGroupImportController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ConfigurationServiceGroupImportCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceGroupImportQuerySpecification qs = new ConfigurationServiceGroupImportQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceGroupImportQuerySpecification qs = new ConfigurationServiceGroupImportQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static ConfigurationServiceGroupImportCollection Fetch(ConfigurationServiceGroupImportQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<ConfigurationServiceGroupImportCollection>();
		}

		public static int FetchCount(ConfigurationServiceGroupImportQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceGroupImportQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceGroupImportController.CreateQueryHelper(qs, ConfigurationServiceGroupImport.Schema, isCountQuery);
		}

		#endregion

		#region CreateQueryHelper

		internal static SqlQuery CreateQueryHelper(ConfigurationServiceGroupImportQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
		{
			SqlQuery query = DB.Select().From(schema);

            ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

            if (qs.ConfigurationServiceApplicationName != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "ConfigurationServiceApplicationName", string.Format("%{0}%", qs.ConfigurationServiceApplicationName), true);
            }

            if (qs.ConfigurationServiceGroupTypeName != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "ConfigurationServiceGroupTypeName", string.Format("%{0}%", qs.ConfigurationServiceGroupTypeName), true);
            }

            if (qs.LabelValue != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "LabelValue", string.Format("%{0}%", qs.LabelValue), true);
            }

            if (qs.ImportMessage != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "ImportMessage", string.Format("%{0}%", qs.ImportMessage), true);
            }

            if (qs.ImportStatus != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereLike(query, "ImportStatus", string.Format("%{0}%", qs.ImportStatus), true);
            }

			if (!isCountQuery)
			{
				SubSonicUtility.SetPaging(query, qs.Paging);
				SubSonicUtility.SetOrderBy(query, qs.SortBy);
			}

			ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(ConfigurationServiceGroupImportController));
			return query;
		}

		#endregion

		#region SmartFetch Method

		public static IRecordCollection SmartFetch(ConfigurationServiceGroupImportQuerySpecification qs, bool includeRowStatusNameInOutput)
		{
			if (includeRowStatusNameInOutput)
			{
				return VwMapConfigurationServiceGroupImportController.Fetch(qs);
			}
			else
			{
				return ConfigurationServiceGroupImportController.Fetch(qs);
			}
		}

		#endregion

		#endregion

	}
}
