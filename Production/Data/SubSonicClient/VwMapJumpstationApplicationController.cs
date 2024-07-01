using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapJumpstationApplicationController class.
	/// </summary>
	public partial class VwMapJumpstationApplicationController
	{
        
		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapJumpstationApplicationCollection ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{  
            
			JumpstationApplicationQuerySpecification qs = new JumpstationApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
		
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
		{
            
            JumpstationApplicationQuerySpecification qs = new JumpstationApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
            //return FetchCount(qs);    
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapJumpstationApplicationCollection Fetch(JumpstationApplicationQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapJumpstationApplicationCollection>();
		}

        public static DataTable FetchToDataTable(JumpstationApplicationQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }
		public static int FetchCount(JumpstationApplicationQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationApplicationQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationApplicationController.CreateQueryHelper(qs, VwMapJumpstationApplication.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
