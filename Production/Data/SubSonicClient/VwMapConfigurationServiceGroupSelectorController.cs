using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapConfigurationServiceGroupSelector class.
	/// </summary>
	public partial class VwMapConfigurationServiceGroupSelectorController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapConfigurationServiceGroupSelectorCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceGroupSelectorQuerySpecification qs = new ConfigurationServiceGroupSelectorQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceGroupSelectorQuerySpecification qs = new ConfigurationServiceGroupSelectorQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceGroupSelectorCollection Fetch(ConfigurationServiceGroupSelectorQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceGroupSelectorCollection>();
		}

		public static int FetchCount(ConfigurationServiceGroupSelectorQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceGroupSelectorQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceGroupSelectorController.CreateQueryHelper(qs, VwMapConfigurationServiceGroupSelector.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="VwMapConfigurationServiceGroupSelector"/>s that are related to a specified <see cref="ConfigurationServiceGroupSelector"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapConfigurationServiceGroupSelectorCollection FetchByConfigurationServiceGroupId(int configurationServiceGroupId)
		{
			return VwMapConfigurationServiceGroupSelectorController.Fetch(
				new ConfigurationServiceGroupSelectorQuerySpecification { ConfigurationServiceGroupId = configurationServiceGroupId, Paging = { PageSize = int.MaxValue } });
		}

		/// <summary>
		/// Convenience method.
		/// Returns the <see cref="VwMapConfigurationServiceGroupSelector"/> s that are related to a specified <see cref="ConfigurationServiceGroupSelector" />
		/// </summary>
		/// <param name="configurationServiceGroupId"></param>
		/// <param name="name"></param>
		public static VwMapConfigurationServiceGroupSelectorCollection FetchByConfigurationServiceGroupIdName(int configurationServiceGroupId, string name)
		{
			SqlQuery query = DB.Select().From(ConfigurationServiceGroupSelector.Schema);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "ConfigurationServiceGroupId", configurationServiceGroupId);
			ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", name);
			ElementsCPSSubSonicUtility.LogQuerySql(query, false, typeof(VwMapConfigurationServiceGroupSelectorController));
			return query.ExecuteAsCollection<VwMapConfigurationServiceGroupSelectorCollection>();
		}

        /// <summary>
        /// Convenience method.
        /// Returns count by ConfigurationServiceGroupId.
        /// </summary>
        /// <returns></returns>
        public static int FetchCountByConfigurationServiceGroupId(int configurationServiceGroupId)
        {
            return VwMapConfigurationServiceGroupSelectorController.FetchCount(
                new ConfigurationServiceGroupSelectorQuerySpecification { ConfigurationServiceGroupId = configurationServiceGroupId, Paging = { PageSize = int.MaxValue } });
        }


		#endregion

		#endregion

	}
}
