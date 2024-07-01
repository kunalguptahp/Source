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
	/// Strongly-typed collection for the VwMapQueryParameterWorkflowType class.
	/// </summary>
	public partial class VwMapQueryParameterWorkflowTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapQueryParameterWorkflowTypeCollection ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			QueryParameterWorkflowTypeQuerySpecification qs = new QueryParameterWorkflowTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
		{
			QueryParameterWorkflowTypeQuerySpecification qs = new QueryParameterWorkflowTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapQueryParameterWorkflowTypeCollection Fetch(QueryParameterWorkflowTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapQueryParameterWorkflowTypeCollection>();
		}

        public static DataTable FetchToDataTable(QueryParameterWorkflowTypeQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(QueryParameterWorkflowTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterWorkflowTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterWorkflowTypeController.CreateQueryHelper(qs, VwMapQueryParameterWorkflowType.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="WorkflowSelectorQueryParameterValue"/>s that are related to a specified <see cref="Workflow"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapQueryParameterWorkflowTypeCollection FetchByWorkflowTypeId(int configurationServiceGroupTypeId, int? rowStatusId)
		{
			return VwMapQueryParameterWorkflowTypeController.Fetch(
				new QueryParameterWorkflowTypeQuerySpecification { WorkflowTypeId = configurationServiceGroupTypeId, RowStatusId = rowStatusId, Paging = { PageSize = int.MaxValue } });
		}

		#endregion

		#endregion

	}
}
