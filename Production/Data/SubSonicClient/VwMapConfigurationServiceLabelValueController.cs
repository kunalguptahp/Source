using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapGroupType class.
	/// </summary>
	public partial class VwMapConfigurationServiceLabelValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapConfigurationServiceLabelValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceLabelValueQuerySpecification qs = new ConfigurationServiceLabelValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceLabelValueQuerySpecification qs = new ConfigurationServiceLabelValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceLabelValueCollection Fetch(ConfigurationServiceLabelValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceLabelValueCollection>();
		}

		public static int FetchCount(ConfigurationServiceLabelValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceLabelValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceLabelValueController.CreateQueryHelper(qs, VwMapConfigurationServiceLabelValue.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="VwMapConfigurationServiceLabelValue"/>s that are related to a specified <see cref="ConfigurationServiceLabelValue"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapConfigurationServiceLabelValueCollection FetchByConfigurationServiceGroupId(int configurationServiceGroupId)
		{
			return VwMapConfigurationServiceLabelValueController.Fetch(
				new ConfigurationServiceLabelValueQuerySpecification { ConfigurationServiceGroupId = configurationServiceGroupId, Paging = { PageSize = int.MaxValue } });
		}

		#endregion

		#endregion

	}
}
