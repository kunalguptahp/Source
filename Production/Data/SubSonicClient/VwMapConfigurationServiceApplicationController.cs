using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using HP.ElementsCPS.Data.Utility;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapConfigurationServiceApplicationController class.
	/// </summary>
	public partial class VwMapConfigurationServiceApplicationController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapConfigurationServiceApplicationCollection ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);   
			ConfigurationServiceApplicationQuerySpecification qs = new ConfigurationServiceApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
           
			ConfigurationServiceApplicationQuerySpecification qs = new ConfigurationServiceApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
           
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceApplicationCollection Fetch(ConfigurationServiceApplicationQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceApplicationCollection>();
		}
        public static DataTable FetchToDataTable(ConfigurationServiceApplicationQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(ConfigurationServiceApplicationQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceApplicationQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceApplicationController.CreateQueryHelper(qs, VwMapConfigurationServiceApplication.Schema, isCountQuery);
		}

		#endregion

		#endregion


        #region Other Querying Methods

        /// <summary>
        /// Convenience method.
        /// Returns the <see cref="VwMapConfigurationServiceApplication"/> s that are related to a specified <see cref="ConfigurationServiceApplication" />
        /// </summary>
        /// <param name="elementsKey"></param>
        public static VwMapConfigurationServiceApplication FetchByElementsKey(string elementsKey)
        {
            SqlQuery query = DB.Select().From(VwMapConfigurationServiceApplication.Schema);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ElementsKey", elementsKey);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(VwMapConfigurationServiceApplicationController));
            VwMapConfigurationServiceApplication instance = query.ExecuteSingle<VwMapConfigurationServiceApplication>();
            return instance;
        }

        #endregion
    }
}
