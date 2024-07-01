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
	/// Strongly-typed collection for the VwMapConfigurationServiceGroupType class.
	/// </summary>
	public partial class VwMapConfigurationServiceGroupTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapConfigurationServiceGroupTypeCollection ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            
			ConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceGroupTypeCollection Fetch(ConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceGroupTypeCollection>();
		}

        public static DataTable FetchToDataTable(ConfigurationServiceGroupTypeQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(ConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
        }

        #endregion

        #region CreateQuery

        private static SqlQuery CreateQuery(ConfigurationServiceGroupTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceGroupTypeController.CreateQueryHelper(qs, VwMapConfigurationServiceGroupType.Schema, isCountQuery);
		}

		#endregion

        #region Other Querying Methods

        /// <summary>
        /// Convenience method.
        /// Returns all <see cref="VwMapConfigurationServiceGroupType"/>s that are related to a specified <see cref="ConfigurationServiceGroupType"/>.
        /// </summary>
        /// <returns></returns>
        public static VwMapConfigurationServiceGroupTypeCollection FetchByConfigurationServiceApplicationId(int configurationServiceApplicationId)
        {
            return VwMapConfigurationServiceGroupTypeController.Fetch(new ConfigurationServiceGroupTypeQuerySpecification { ConfigurationServiceApplicationId = configurationServiceApplicationId, Paging = { PageSize = int.MaxValue } });
        }

        /// <summary>
        /// Convenience method.
        /// Returns all <see cref="VwMapConfigurationServiceGroupType"/>s that are related to a specified <see cref="ConfigurationServiceGroupType"/>.
        /// </summary>
        /// <returns></returns>
        public static VwMapConfigurationServiceGroupType FetchByElementsKeyGroupTypeName(string elementsKey, string groupTypeName)
        {
            SqlQuery query = DB.Select().From(VwMapConfigurationServiceGroupType.Schema);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ElementsKey", elementsKey);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", groupTypeName);
            VwMapConfigurationServiceGroupType instance = query.ExecuteSingle<VwMapConfigurationServiceGroupType>();
            return instance;
        }

		#endregion

    }

}
