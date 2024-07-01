using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapConfigurationServiceLabelConfigurationServiceGroupTypeConfigurationServiceGroupType class.
	/// </summary>
	public partial class VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection Fetch(ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection>();
		}

		public static int FetchCount(ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceLabelConfigurationServiceGroupTypeController.CreateQueryHelper(qs, VwMapConfigurationServiceLabelConfigurationServiceGroupType.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/>s that are related to a specified <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection FetchByConfigurationServiceGroupTypeId(int configurationServiceGroupTypeId)
		{
			return VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController.Fetch(new ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification { ConfigurationServiceGroupTypeId = configurationServiceGroupTypeId, Paging = { PageSize = int.MaxValue }});
		}

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/>s that are related to a specified <see cref="VwMapConfigurationServiceLabelConfigurationServiceGroupType"/> in ordered list.
		/// </summary>
		/// <returns></returns>
		public static VwMapConfigurationServiceLabelConfigurationServiceGroupTypeCollection FetchByConfigurationServiceGroupTypeIdOrdered(int configurationServiceGroupTypeId)
		{
			return VwMapConfigurationServiceLabelConfigurationServiceGroupTypeController.Fetch(new ConfigurationServiceLabelConfigurationServiceGroupTypeQuerySpecification { ConfigurationServiceGroupTypeId = configurationServiceGroupTypeId, Paging = { PageSize = int.MaxValue }, SortBy = { new QuerySortDirection("ConfigurationServiceItemSortOrder, SortOrder ASC") } });
		}

		#endregion

		#endregion

	}
}
