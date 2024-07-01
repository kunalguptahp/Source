using System.Collections.Generic;
using System.ComponentModel;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapJumpstationGroupTagTagController class.
	/// </summary>
	public partial class VwMapJumpstationGroupTagTagController
	{

		public static VwMapJumpstationGroupTagTagCollection GetActiveTagsByJumpstationGroupId(int JumpstationGroupId)
		{
			const RowStatus.RowStatusId active = RowStatus.RowStatusId.Active;
			return VwMapJumpstationGroupTagTagController.FetchBySearchCriteria(VwMapJumpstationGroupTagTag.Columns.TagId, int.MaxValue, 0, JumpstationGroupId, null, (int)active, (int)active, null);
		}

		/// <summary>
		/// Creates a list of Tag IDs from a collection of <see cref="VwMapJumpstationGroupTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateTagIdList(VwMapJumpstationGroupTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapJumpstationGroupTagTag tag in tags)
			{
				ids.Add(tag.TagId);
			}
			return ids;
		}

		/// <summary>
		/// Creates a list of JumpstationGroup IDs from a collection of <see cref="VwMapJumpstationGroupTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateJumpstationGroupIdList(VwMapJumpstationGroupTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapJumpstationGroupTagTag tag in tags)
			{
				ids.Add(tag.JumpstationGroupId);
			}
			return ids;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapJumpstationGroupTagTagCollection FetchBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? JumpstationGroupIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			VwMapJumpstationGroupTagTagCollection recordCollection = new VwMapJumpstationGroupTagTagCollection();
			Query qry = FetchBySearchCriteria_CreateQuery(JumpstationGroupIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			SubSonicUtility.SetPaging(qry, maximumRows, startRowIndex);
			SubSonicUtility.SetOrderBy(qry, sortExpression);
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int FetchCountBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? JumpstationGroupIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			//Note: Object Data Source requires that we pass in sortExpression, maximumRows and startRowIndex.
			return FetchCountBySearchCriteria(JumpstationGroupIdParameter, statusIdParameter, tagIdParameter, tagStatusIdParameter, tagNameParameter);
		}

		public static int FetchCountBySearchCriteria(int? JumpstationGroupIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = FetchBySearchCriteria_CreateQuery(JumpstationGroupIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			return qry.GetCount(VwMapJumpstationGroupTagTag.Columns.CreatedBy);
		}

		private static Query FetchBySearchCriteria_CreateQuery(int? JumpstationGroupIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = new Query(VwMapJumpstationGroupTagTag.Schema);
			if (JumpstationGroupIdParameter != null)
			{
				qry.AddWhere(VwMapJumpstationGroupTagTag.Columns.JumpstationGroupId, JumpstationGroupIdParameter);
			}
			if (tagIdParameter != null)
			{
				qry.AddWhere(VwMapJumpstationGroupTagTag.Columns.TagId, tagIdParameter);
			}
			if (tagStatusIdParameter != null)
			{
				qry.AddWhere(VwMapJumpstationGroupTagTag.Columns.TagRowStatusId, tagStatusIdParameter);
			}

			tagNameParameter = SqlUtility.ValidateSqlLikeComparisonOperand(tagNameParameter, true);
			if (!string.IsNullOrEmpty(tagNameParameter))
			{
				qry.AddWhere(VwMapJumpstationGroupTagTag.Columns.TagName, Comparison.Like, tagNameParameter);
			}

			return qry;
		}
	}
}
