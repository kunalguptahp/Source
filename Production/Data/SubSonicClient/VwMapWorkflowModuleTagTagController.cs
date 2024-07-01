using System.Collections.Generic;
using System.ComponentModel;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapWorkflowModuleTagTagController class.
	/// </summary>
	public partial class VwMapWorkflowModuleTagTagController
	{

		public static VwMapWorkflowModuleTagTagCollection GetActiveTagsByWorkflowModuleId(int WorkflowModuleId)
		{
			const RowStatus.RowStatusId active = RowStatus.RowStatusId.Active;
			return VwMapWorkflowModuleTagTagController.FetchBySearchCriteria(VwMapWorkflowModuleTagTag.Columns.TagId, int.MaxValue, 0, WorkflowModuleId, null, (int)active, (int)active, null);
		}

		/// <summary>
		/// Creates a list of Tag IDs from a collection of <see cref="VwMapWorkflowModuleTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateTagIdList(VwMapWorkflowModuleTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapWorkflowModuleTagTag tag in tags)
			{
				ids.Add(tag.TagId);
			}
			return ids;
		}

		/// <summary>
		/// Creates a list of WorkflowModule IDs from a collection of <see cref="VwMapWorkflowModuleTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateWorkflowModuleIdList(VwMapWorkflowModuleTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapWorkflowModuleTagTag tag in tags)
			{
				ids.Add(tag.WorkflowModuleId);
			}
			return ids;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapWorkflowModuleTagTagCollection FetchBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? WorkflowModuleIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			VwMapWorkflowModuleTagTagCollection recordCollection = new VwMapWorkflowModuleTagTagCollection();
			Query qry = FetchBySearchCriteria_CreateQuery(WorkflowModuleIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			SubSonicUtility.SetPaging(qry, maximumRows, startRowIndex);
			SubSonicUtility.SetOrderBy(qry, sortExpression);
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int FetchCountBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? WorkflowModuleIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			//Note: Object Data Source requires that we pass in sortExpression, maximumRows and startRowIndex.
			return FetchCountBySearchCriteria(WorkflowModuleIdParameter, statusIdParameter, tagIdParameter, tagStatusIdParameter, tagNameParameter);
		}

		public static int FetchCountBySearchCriteria(int? WorkflowModuleIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = FetchBySearchCriteria_CreateQuery(WorkflowModuleIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			return qry.GetCount(VwMapWorkflowModuleTagTag.Columns.CreatedBy);
		}

		private static Query FetchBySearchCriteria_CreateQuery(int? WorkflowModuleIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = new Query(VwMapWorkflowModuleTagTag.Schema);
			if (WorkflowModuleIdParameter != null)
			{
				qry.AddWhere(VwMapWorkflowModuleTagTag.Columns.WorkflowModuleId, WorkflowModuleIdParameter);
			}
			if (tagIdParameter != null)
			{
				qry.AddWhere(VwMapWorkflowModuleTagTag.Columns.TagId, tagIdParameter);
			}
			if (tagStatusIdParameter != null)
			{
				qry.AddWhere(VwMapWorkflowModuleTagTag.Columns.TagRowStatusId, tagStatusIdParameter);
			}

			tagNameParameter = SqlUtility.ValidateSqlLikeComparisonOperand(tagNameParameter, true);
			if (!string.IsNullOrEmpty(tagNameParameter))
			{
				qry.AddWhere(VwMapWorkflowModuleTagTag.Columns.TagName, Comparison.Like, tagNameParameter);
			}

			return qry;
		}
	}
}
