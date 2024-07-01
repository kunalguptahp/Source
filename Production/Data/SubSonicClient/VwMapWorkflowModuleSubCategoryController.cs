using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapWorkflowModuleSubCategoryController class.
	/// </summary>
	public partial class VwMapWorkflowModuleSubCategoryController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapWorkflowModuleSubCategoryCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			WorkflowModuleSubCategoryQuerySpecification qs = new WorkflowModuleSubCategoryQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			WorkflowModuleSubCategoryQuerySpecification qs = new WorkflowModuleSubCategoryQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapWorkflowModuleSubCategoryCollection Fetch(WorkflowModuleSubCategoryQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapWorkflowModuleSubCategoryCollection>();
		}
        public static DataTable FetchToDataTable(WorkflowModuleSubCategoryQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(WorkflowModuleSubCategoryQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(WorkflowModuleSubCategoryQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return WorkflowModuleSubCategoryController.CreateQueryHelper(qs, VwMapWorkflowModuleSubCategory.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
