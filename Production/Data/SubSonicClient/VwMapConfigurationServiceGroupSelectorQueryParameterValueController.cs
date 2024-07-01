using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapConfigurationServiceGroupSelectorQueryParameterValue class.
	/// </summary>
	public partial class VwMapConfigurationServiceGroupSelectorQueryParameterValueController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapConfigurationServiceGroupSelectorQueryParameterValueCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs = new ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs = new ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceGroupSelectorQueryParameterValueCollection Fetch(ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceGroupSelectorQueryParameterValueCollection>();
		}

		public static int FetchCount(ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			SqlQuery q = ConfigurationServiceGroupSelectorQueryParameterValueController.CreateQueryHelper(qs, VwMapConfigurationServiceGroupSelectorQueryParameterValue.Schema, isCountQuery);
			return q;
		}

		#endregion

		#region Other Querying Methods

		/// <summary>
		/// Convenience method.
		/// Returns all <see cref="ConfigurationServiceGroupSelectorQueryParameterValue"/>s that are related to a specified <see cref="ConfigurationServiceGroupSelector"/>.
		/// </summary>
		/// <returns></returns>
		public static VwMapConfigurationServiceGroupSelectorQueryParameterValueCollection FetchByConfigurationServiceGroupSelectorIdQueryParameterId(int configurationServiceGroupSelectorId, int queryParameterId)
		{
			return VwMapConfigurationServiceGroupSelectorQueryParameterValueController.Fetch(
				new ConfigurationServiceGroupSelectorQueryParameterValueQuerySpecification { ConfigurationServiceGroupSelectorId = configurationServiceGroupSelectorId, QueryParameterId = queryParameterId, Paging = { PageSize = int.MaxValue } });
		}

		#endregion

		#endregion

	}
}
