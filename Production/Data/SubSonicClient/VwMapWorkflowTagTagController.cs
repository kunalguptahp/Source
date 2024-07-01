using System.Collections.Generic;
using System.ComponentModel;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapWorkflowTagTagController class.
	/// </summary>
	public partial class VwMapWorkflowTagTagController
	{

		public static VwMapWorkflowTagTagCollection GetActiveTagsByWorkflowId(int WorkflowId)
		{
			const RowStatus.RowStatusId active = RowStatus.RowStatusId.Active;
			return VwMapWorkflowTagTagController.FetchBySearchCriteria(VwMapWorkflowTagTag.Columns.TagId, int.MaxValue, 0, WorkflowId, null, (int)active, (int)active, null);
		}

		/// <summary>
		/// Creates a list of Tag IDs from a collection of <see cref="VwMapWorkflowTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateTagIdList(VwMapWorkflowTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapWorkflowTagTag tag in tags)
			{
				ids.Add(tag.TagId);
			}
			return ids;
		}

		/// <summary>
		/// Creates a list of Workflow IDs from a collection of <see cref="VwMapWorkflowTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateWorkflowIdList(VwMapWorkflowTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapWorkflowTagTag tag in tags)
			{
				ids.Add(tag.WorkflowId);
			}
			return ids;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapWorkflowTagTagCollection FetchBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? WorkflowIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			VwMapWorkflowTagTagCollection recordCollection = new VwMapWorkflowTagTagCollection();
			Query qry = FetchBySearchCriteria_CreateQuery(WorkflowIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			SubSonicUtility.SetPaging(qry, maximumRows, startRowIndex);
			SubSonicUtility.SetOrderBy(qry, sortExpression);
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int FetchCountBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? WorkflowIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			//Note: Object Data Source requires that we pass in sortExpression, maximumRows and startRowIndex.
			return FetchCountBySearchCriteria(WorkflowIdParameter, statusIdParameter, tagIdParameter, tagStatusIdParameter, tagNameParameter);
		}

		public static int FetchCountBySearchCriteria(int? WorkflowIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = FetchBySearchCriteria_CreateQuery(WorkflowIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			return qry.GetCount(VwMapWorkflowTagTag.Columns.CreatedBy);
		}

		private static Query FetchBySearchCriteria_CreateQuery(int? WorkflowIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = new Query(VwMapWorkflowTagTag.Schema);
			if (WorkflowIdParameter != null)
			{
				qry.AddWhere(VwMapWorkflowTagTag.Columns.WorkflowId, WorkflowIdParameter);
			}
			if (tagIdParameter != null)
			{
				qry.AddWhere(VwMapWorkflowTagTag.Columns.TagId, tagIdParameter);
			}
			if (tagStatusIdParameter != null)
			{
				qry.AddWhere(VwMapWorkflowTagTag.Columns.TagRowStatusId, tagStatusIdParameter);
			}

			tagNameParameter = SqlUtility.ValidateSqlLikeComparisonOperand(tagNameParameter, true);
			if (!string.IsNullOrEmpty(tagNameParameter))
			{
				qry.AddWhere(VwMapWorkflowTagTag.Columns.TagName, Comparison.Like, tagNameParameter);
			}

			return qry;
		}
	}
}
