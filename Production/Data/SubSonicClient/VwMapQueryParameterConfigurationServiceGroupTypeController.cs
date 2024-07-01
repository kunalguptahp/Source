using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapQueryParameterConfigurationServiceGroupType class.
	/// </summary>
	public partial class VwMapQueryParameterConfigurationServiceGroupTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapQueryParameterConfigurationServiceGroupTypeCollection ODSFetch( string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            
			QueryParameterConfigurationServiceGroupTypeQuerySpecification qs = new QueryParameterConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount( string serializedQuerySpecificationXml)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            
			QueryParameterConfigurationServiceGroupTypeQuerySpecification qs = new QueryParameterConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapQueryParameterConfigurationServiceGroupTypeCollection Fetch(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapQueryParameterConfigurationServiceGroupTypeCollection>();
		}
        public static DataTable FetchToDataTable(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterConfigurationServiceGroupTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterConfigurationServiceGroupTypeController.CreateQueryHelper(qs, VwMapQueryParameterConfigurationServiceGroupType.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/>s that are related to a specified <see cref="ConfigurationServiceGroup"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapQueryParameterConfigurationServiceGroupTypeCollection FetchByConfigurationServiceGroupTypeId(int configurationServiceGroupTypeId, int? rowStatusId)
		{
			return VwMapQueryParameterConfigurationServiceGroupTypeController.Fetch(
				new QueryParameterConfigurationServiceGroupTypeQuerySpecification { ConfigurationServiceGroupTypeId = configurationServiceGroupTypeId, RowStatusId = rowStatusId, Paging = { PageSize = int.MaxValue } });
		}

		#endregion

		#endregion

	}
}
