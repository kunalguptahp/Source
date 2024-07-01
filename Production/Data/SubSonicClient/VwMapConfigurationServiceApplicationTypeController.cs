using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapConfigurationServiceApplicationTypeController class.
	/// </summary>
	public partial class VwMapConfigurationServiceApplicationTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapConfigurationServiceApplicationTypeCollection ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceApplicationTypeQuerySpecification qs = new ConfigurationServiceApplicationTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
		{
			ConfigurationServiceApplicationTypeQuerySpecification qs = new ConfigurationServiceApplicationTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;

            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceApplicationTypeCollection Fetch(ConfigurationServiceApplicationTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceApplicationTypeCollection>();
		}

        public static DataTable FetchToDataTable(ConfigurationServiceApplicationTypeQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(ConfigurationServiceApplicationTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceApplicationTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceApplicationTypeController.CreateQueryHelper(qs, VwMapConfigurationServiceApplicationType.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
