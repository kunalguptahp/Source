using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapWorkflowSelectorQueryParameterValue class.
	/// </summary>
	public partial class VwMapWorkflowSelectorQueryParameterValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapWorkflowSelectorQueryParameterValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			WorkflowSelectorQueryParameterValueQuerySpecification qs = new WorkflowSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			WorkflowSelectorQueryParameterValueQuerySpecification qs = new WorkflowSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapWorkflowSelectorQueryParameterValueCollection Fetch(WorkflowSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapWorkflowSelectorQueryParameterValueCollection>();
		}

		public static int FetchCount(WorkflowSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowSelectorQueryParameterValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			SqlQuery q = WorkflowSelectorQueryParameterValueController.CreateQueryHelper(qs, VwMapWorkflowSelectorQueryParameterValue.Schema, isCountQuery);
			return q;
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="WorkflowSelectorQueryParameterValue"/>s that are related to a specified <see cref="WorkflowSelector"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapWorkflowSelectorQueryParameterValueCollection FetchByWorkflowSelectorIdQueryParameterId(int WorkflowSelectorId, int queryParameterId)
		{
			return VwMapWorkflowSelectorQueryParameterValueController.Fetch(
				new WorkflowSelectorQueryParameterValueQuerySpecification { WorkflowSelectorId = WorkflowSelectorId, QueryParameterId = queryParameterId, Paging = { PageSize = int.MaxValue } });
		}

		#endregion

		#endregion

	}
}
