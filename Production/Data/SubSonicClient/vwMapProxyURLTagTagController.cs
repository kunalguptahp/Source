using System.Collections.Generic;
using System.ComponentModel;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapProxyURLTagTagController class.
	/// </summary>
	public partial class VwMapProxyURLTagTagController
	{

		public static VwMapProxyURLTagTagCollection GetActiveTagsByProxyURLId(int ProxyURLId)
		{
			const RowStatus.RowStatusId active = RowStatus.RowStatusId.Active;
			return VwMapProxyURLTagTagController.FetchBySearchCriteria(VwMapProxyURLTagTag.Columns.TagId, int.MaxValue, 0, ProxyURLId, null, (int)active, (int)active, null);
		}

		/// <summary>
		/// Creates a list of Tag IDs from a collection of <see cref="VwMapProxyURLTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateTagIdList(VwMapProxyURLTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapProxyURLTagTag tag in tags)
			{
				ids.Add(tag.TagId);
			}
			return ids;
		}

		/// <summary>
		/// Creates a list of ProxyURL IDs from a collection of <see cref="VwMapProxyURLTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateProxyURLIdList(VwMapProxyURLTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapProxyURLTagTag tag in tags)
			{
				ids.Add(tag.ProxyURLId);
			}
			return ids;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapProxyURLTagTagCollection FetchBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? ProxyURLIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			VwMapProxyURLTagTagCollection recordCollection = new VwMapProxyURLTagTagCollection();
			Query qry = FetchBySearchCriteria_CreateQuery(ProxyURLIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			SubSonicUtility.SetPaging(qry, maximumRows, startRowIndex);
			SubSonicUtility.SetOrderBy(qry, sortExpression);
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int FetchCountBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? ProxyURLIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			//Note: Object Data Source requires that we pass in sortExpression, maximumRows and startRowIndex.
			return FetchCountBySearchCriteria(ProxyURLIdParameter, statusIdParameter, tagIdParameter, tagStatusIdParameter, tagNameParameter);
		}

		public static int FetchCountBySearchCriteria(int? ProxyURLIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = FetchBySearchCriteria_CreateQuery(ProxyURLIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			return qry.GetCount(VwMapProxyURLTagTag.Columns.CreatedBy);
		}

		private static Query FetchBySearchCriteria_CreateQuery(int? ProxyURLIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = new Query(VwMapProxyURLTagTag.Schema);
			if (ProxyURLIdParameter != null)
			{
				qry.AddWhere(VwMapProxyURLTagTag.Columns.ProxyURLId, ProxyURLIdParameter);
			}
			if (tagIdParameter != null)
			{
				qry.AddWhere(VwMapProxyURLTagTag.Columns.TagId, tagIdParameter);
			}
			if (tagStatusIdParameter != null)
			{
				qry.AddWhere(VwMapProxyURLTagTag.Columns.TagRowStatusId, tagStatusIdParameter);
			}

			tagNameParameter = SqlUtility.ValidateSqlLikeComparisonOperand(tagNameParameter, true);
			if (!string.IsNullOrEmpty(tagNameParameter))
			{
				qry.AddWhere(VwMapProxyURLTagTag.Columns.TagName, Comparison.Like, tagNameParameter);
			}

			return qry;
		}
	}
}
