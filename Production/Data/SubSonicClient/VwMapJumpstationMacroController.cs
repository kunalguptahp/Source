using System.ComponentModel;
using System.Data;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Diagnostics.Logging;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Collections.Generic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapJumpstationMacro class.
	/// </summary>
	public partial class VwMapJumpstationMacroController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapJumpstationMacroCollection ODSFetch(string tenantGroupId, string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            
			JumpstationMacroQuerySpecification qs = new JumpstationMacroQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string tenantGroupId, string serializedQuerySpecificationXml)
		{
            List<string> windowsIds = PersonController.GetWindowsIdListByTenantGroupId(PersonController.GetCurrentUser().TenantGroupId);
            
			JumpstationMacroQuerySpecification qs = new JumpstationMacroQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            string strs = string.Join(",", windowsIds.ToArray());
            qs.CreatedBy = strs;
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapJumpstationMacroCollection Fetch(JumpstationMacroQuerySpecification qs)
		{

			return CreateQuery(qs, false).ExecuteAsCollection<VwMapJumpstationMacroCollection>();
		}

        public static DataTable FetchToDataTable(JumpstationMacroQuerySpecification qs)
        {

            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }
		public static int FetchCount(JumpstationMacroQuerySpecification qs)
		{

			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(JumpstationMacroQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return JumpstationMacroController.CreateQueryHelper(qs, VwMapJumpstationMacro.Schema, isCountQuery);
		}

		#endregion


        #region Other Querying Methods

        /// <summary>
        /// Convenience method.
        /// Returns the <see cref="VwMapJumpstationMacro"/> s that are related to a specified <see cref="JumpstationMacro" />
        /// </summary>
        /// <param name="name"></param>
        public static VwMapJumpstationMacroCollection FetchByName(string name)
        {
            SqlQuery query = DB.Select().From(VwMapJumpstationMacro.Schema);
            ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
            ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(VwMapJumpstationMacroController));
            return query.ExecuteAsCollection<VwMapJumpstationMacroCollection>();
        }

        #endregion

		#endregion

	}
}
