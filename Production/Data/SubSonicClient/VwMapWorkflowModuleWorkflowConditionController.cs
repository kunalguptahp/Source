using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapWorkflowModuleWorkflowCondition class.
	/// </summary>
	public partial class VwMapWorkflowModuleWorkflowConditionController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapWorkflowModuleWorkflowConditionCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			WorkflowModuleWorkflowConditionQuerySpecification qs = new WorkflowModuleWorkflowConditionQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			WorkflowModuleWorkflowConditionQuerySpecification qs = new WorkflowModuleWorkflowConditionQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapWorkflowModuleWorkflowConditionCollection Fetch(WorkflowModuleWorkflowConditionQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapWorkflowModuleWorkflowConditionCollection>();
		}

		public static int FetchCount(WorkflowModuleWorkflowConditionQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowModuleWorkflowConditionQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			SqlQuery q = WorkflowModuleWorkflowConditionController.CreateQueryHelper(qs, VwMapWorkflowModuleWorkflowCondition.Schema, isCountQuery);
			return q;
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="WorkflowModuleWorkflowCondition"/>s that are related to a specified <see cref="ConfigurationServiceGroupSelector"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapWorkflowModuleWorkflowConditionCollection FetchByWorkflowModuleId(int workflowModuleId)
		{
			return VwMapWorkflowModuleWorkflowConditionController.Fetch(
				new WorkflowModuleWorkflowConditionQuerySpecification { WorkflowModuleId = workflowModuleId, Paging = { PageSize = int.MaxValue } });
		}

		#endregion

		#endregion

	}
}
