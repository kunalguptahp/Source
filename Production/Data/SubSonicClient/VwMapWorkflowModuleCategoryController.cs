using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapWorkflowModuleCategoryController class.
	/// </summary>
	public partial class VwMapWorkflowModuleCategoryController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapWorkflowModuleCategoryCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			WorkflowModuleCategoryQuerySpecification qs = new WorkflowModuleCategoryQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount( string serializedQuerySpecificationXml)
		{
			WorkflowModuleCategoryQuerySpecification qs = new WorkflowModuleCategoryQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));  
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapWorkflowModuleCategoryCollection Fetch(WorkflowModuleCategoryQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapWorkflowModuleCategoryCollection>();
		}
        public static DataTable FetchToDataTable(WorkflowModuleCategoryQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(WorkflowModuleCategoryQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowModuleCategoryQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return WorkflowModuleCategoryController.CreateQueryHelper(qs, VwMapWorkflowModuleCategory.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
