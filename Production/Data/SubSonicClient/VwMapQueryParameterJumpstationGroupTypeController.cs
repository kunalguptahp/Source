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
	/// Strongly-typed collection for the VwMapQueryParameterJumpstationGroupType class.
	/// </summary>
	public partial class VwMapQueryParameterJumpstationGroupTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapQueryParameterJumpstationGroupTypeCollection ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            
			QueryParameterJumpstationGroupTypeQuerySpecification qs = new QueryParameterJumpstationGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            
			QueryParameterJumpstationGroupTypeQuerySpecification qs = new QueryParameterJumpstationGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapQueryParameterJumpstationGroupTypeCollection Fetch(QueryParameterJumpstationGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapQueryParameterJumpstationGroupTypeCollection>();
		}

        public static DataTable FetchToDataTable(QueryParameterJumpstationGroupTypeQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(QueryParameterJumpstationGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(QueryParameterJumpstationGroupTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return QueryParameterJumpstationGroupTypeController.CreateQueryHelper(qs, VwMapQueryParameterJumpstationGroupType.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="JumpstationGroupSelectorQueryParameterValue"/>s that are related to a specified <see cref="JumpstationGroup"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapQueryParameterJumpstationGroupTypeCollection FetchByJumpstationGroupTypeId(int JumpstationGroupTypeId, int? rowStatusId)
		{
			return VwMapQueryParameterJumpstationGroupTypeController.Fetch(
				new QueryParameterJumpstationGroupTypeQuerySpecification { JumpstationGroupTypeId = JumpstationGroupTypeId, RowStatusId = rowStatusId, Paging = { PageSize = int.MaxValue } });
		}

		#endregion

		#endregion

	}
}
