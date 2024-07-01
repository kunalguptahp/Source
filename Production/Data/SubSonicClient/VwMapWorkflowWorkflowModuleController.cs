using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Data;
using HP.ElementsCPS.Data.Utility;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapWorkflowWorkflowModule class.
	/// </summary>
	public partial class VwMapWorkflowWorkflowModuleController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapWorkflowWorkflowModuleCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			WorkflowWorkflowModuleQuerySpecification qs = new WorkflowWorkflowModuleQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			WorkflowWorkflowModuleQuerySpecification qs = new WorkflowWorkflowModuleQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapWorkflowWorkflowModuleCollection Fetch(WorkflowWorkflowModuleQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapWorkflowWorkflowModuleCollection>();
		}

		public static int FetchCount(WorkflowWorkflowModuleQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowWorkflowModuleQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			SqlQuery q = WorkflowWorkflowModuleController.CreateQueryHelper(qs, VwMapWorkflowWorkflowModule.Schema, isCountQuery);
			return q;
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
        /// Returns all <see cref="WorkflowWorkflowModule"/>s that are related to a specified <see cref="WorkflowWorkflowModule"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapWorkflowWorkflowModuleCollection FetchByWorkflowId(int workflowId)
		{
			return VwMapWorkflowWorkflowModuleController.Fetch(
				new WorkflowWorkflowModuleQuerySpecification { WorkflowId = workflowId, Paging = { PageSize = int.MaxValue } });
		}

        /// <summary>
        /// Convenience method.
        /// Returns workflow module by workflowModuleId.
        /// </summary>
        /// <returns></returns>
        public static VwMapWorkflowWorkflowModuleCollection FetchByWorkflowModuleId(int workflowModuleId)
        {
            return VwMapWorkflowWorkflowModuleController.Fetch(
                new WorkflowWorkflowModuleQuerySpecification { WorkflowModuleId = workflowModuleId, Paging = { PageSize = int.MaxValue } });
        }

        /// <summary>
        /// Convenience method.
        /// Returns count by workflowModuleId.
        /// </summary>
        /// <returns></returns>
        public static int FetchCountByWorkflowModuleId(int workflowModuleId)
        {
            return VwMapWorkflowWorkflowModuleController.FetchCount(
                new WorkflowWorkflowModuleQuerySpecification { WorkflowModuleId = workflowModuleId, Paging = { PageSize = int.MaxValue } });
        }

        #endregion

		#endregion

	}
}
