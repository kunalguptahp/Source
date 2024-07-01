using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapWorkflowSelector class.
	/// </summary>
	public partial class VwMapWorkflowSelectorController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapWorkflowSelectorCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			WorkflowSelectorQuerySpecification qs = new WorkflowSelectorQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			WorkflowSelectorQuerySpecification qs = new WorkflowSelectorQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapWorkflowSelectorCollection Fetch(WorkflowSelectorQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapWorkflowSelectorCollection>();
		}

		public static int FetchCount(WorkflowSelectorQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowSelectorQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return WorkflowSelectorController.CreateQueryHelper(qs, VwMapWorkflowSelector.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="VwMapWorkflowSelector"/>s that are related to a specified <see cref="WorkflowSelector"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapWorkflowSelectorCollection FetchByWorkflowId(int workflowId)
		{
			return VwMapWorkflowSelectorController.Fetch(
				new WorkflowSelectorQuerySpecification { WorkflowId = workflowId, Paging = { PageSize = int.MaxValue } });
		}

		/// <summary>
		/// Convenience method.
		/// Returns the <see cref="VwMapWorkflowSelector"/> s that are related to a specified <see cref="WorkflowSelector" />
		/// </summary>
		/// <param name="workflowId"></param>
		/// <param name="name"></param>
		public static VwMapWorkflowSelectorCollection FetchByWorkflowIdName(int workflowId, string name)
		{
			SqlQuery query = DB.Select().From(VwMapWorkflowSelector.Schema);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "WorkflowId", workflowId);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
			ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(VwMapWorkflowSelectorController));
			return query.ExecuteAsCollection<VwMapWorkflowSelectorCollection>();
		}

        /// <summary>
        /// Convenience method.
        /// Returns count by workflowId.
        /// </summary>
        /// <returns></returns>
        public static int FetchCountByWorkflowId(int workflowId)
        {
            return VwMapWorkflowSelectorController.FetchCount(
                new WorkflowSelectorQuerySpecification { WorkflowId = workflowId, Paging = { PageSize = int.MaxValue } });
        }

		#endregion

		#endregion

	}
}
