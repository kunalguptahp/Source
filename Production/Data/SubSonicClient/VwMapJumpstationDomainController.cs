using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using System.Data;
using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapJumpstationDomainController class.
	/// </summary>
	public partial class VwMapJumpstationDomainController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapJumpstationDomainCollection ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            
			JumpstationDomainQuerySpecification qs = new JumpstationDomainQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);

			JumpstationDomainQuerySpecification qs = new JumpstationDomainQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapJumpstationDomainCollection Fetch(JumpstationDomainQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapJumpstationDomainCollection>();
		}

        public static DataTable FetchToDataTable(JumpstationDomainQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }
		public static int FetchCount(JumpstationDomainQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationDomainQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationDomainController.CreateQueryHelper(qs, VwMapJumpstationDomain.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
