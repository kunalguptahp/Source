using System.Collections.Generic;
using System.ComponentModel;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// The non-generated portion of the VwMapConfigurationServiceGroupTagTagController class.
	/// </summary>
	public partial class VwMapConfigurationServiceGroupTagTagController
	{

		public static VwMapConfigurationServiceGroupTagTagCollection GetActiveTagsByConfigurationServiceGroupId(int ConfigurationServiceGroupId)
		{
			const RowStatus.RowStatusId active = RowStatus.RowStatusId.Active;
			return VwMapConfigurationServiceGroupTagTagController.FetchBySearchCriteria(VwMapConfigurationServiceGroupTagTag.Columns.TagId, int.MaxValue, 0, ConfigurationServiceGroupId, null, (int)active, (int)active, null);
		}

		/// <summary>
		/// Creates a list of Tag IDs from a collection of <see cref="VwMapConfigurationServiceGroupTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateTagIdList(VwMapConfigurationServiceGroupTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapConfigurationServiceGroupTagTag tag in tags)
			{
				ids.Add(tag.TagId);
			}
			return ids;
		}

		/// <summary>
		/// Creates a list of ConfigurationServiceGroup IDs from a collection of <see cref="VwMapConfigurationServiceGroupTagTag"/>.
		/// </summary>
		/// <param name="tags"></param>
		/// <returns></returns>
		public static List<int> CreateConfigurationServiceGroupIdList(VwMapConfigurationServiceGroupTagTagCollection tags)
		{
			if (tags == null)
			{
				return new List<int>(0);
			}

			List<int> ids = new List<int>(tags.Count);
			foreach (VwMapConfigurationServiceGroupTagTag tag in tags)
			{
				ids.Add(tag.ConfigurationServiceGroupId);
			}
			return ids;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapConfigurationServiceGroupTagTagCollection FetchBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? ConfigurationServiceGroupIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			VwMapConfigurationServiceGroupTagTagCollection recordCollection = new VwMapConfigurationServiceGroupTagTagCollection();
			Query qry = FetchBySearchCriteria_CreateQuery(ConfigurationServiceGroupIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			SubSonicUtility.SetPaging(qry, maximumRows, startRowIndex);
			SubSonicUtility.SetOrderBy(qry, sortExpression);
			recordCollection.LoadAndCloseReader(qry.ExecuteReader());
			return recordCollection;
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int FetchCountBySearchCriteria(string sortExpression, int maximumRows, int startRowIndex, int? ConfigurationServiceGroupIdParameter, int? tagIdParameter, int? statusIdParameter,
			int? tagStatusIdParameter, string tagNameParameter)
		{
			//Note: Object Data Source requires that we pass in sortExpression, maximumRows and startRowIndex.
			return FetchCountBySearchCriteria(ConfigurationServiceGroupIdParameter, statusIdParameter, tagIdParameter, tagStatusIdParameter, tagNameParameter);
		}

		public static int FetchCountBySearchCriteria(int? ConfigurationServiceGroupIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = FetchBySearchCriteria_CreateQuery(ConfigurationServiceGroupIdParameter, tagIdParameter, statusIdParameter, tagStatusIdParameter, tagNameParameter);
			return qry.GetCount(VwMapConfigurationServiceGroupTagTag.Columns.CreatedBy);
		}

		private static Query FetchBySearchCriteria_CreateQuery(int? ConfigurationServiceGroupIdParameter, int? tagIdParameter, int? statusIdParameter, int? tagStatusIdParameter, string tagNameParameter)
		{
			Query qry = new Query(VwMapConfigurationServiceGroupTagTag.Schema);
			if (ConfigurationServiceGroupIdParameter != null)
			{
				qry.AddWhere(VwMapConfigurationServiceGroupTagTag.Columns.ConfigurationServiceGroupId, ConfigurationServiceGroupIdParameter);
			}
			if (tagIdParameter != null)
			{
				qry.AddWhere(VwMapConfigurationServiceGroupTagTag.Columns.TagId, tagIdParameter);
			}
			if (tagStatusIdParameter != null)
			{
				qry.AddWhere(VwMapConfigurationServiceGroupTagTag.Columns.TagRowStatusId, tagStatusIdParameter);
			}

			tagNameParameter = SqlUtility.ValidateSqlLikeComparisonOperand(tagNameParameter, true);
			if (!string.IsNullOrEmpty(tagNameParameter))
			{
				qry.AddWhere(VwMapConfigurationServiceGroupTagTag.Columns.TagName, Comparison.Like, tagNameParameter);
			}

			return qry;
		}
	}
}
