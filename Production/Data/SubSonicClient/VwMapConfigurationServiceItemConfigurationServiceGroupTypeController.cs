using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using HP.HPFx.Data.Utility;
using HP.HPFx.Utility.SubSonic;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapConfigurationServiceItemConfigurationServiceGroupTypeController class.
	/// </summary>
	public partial class VwMapConfigurationServiceItemConfigurationServiceGroupTypeController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapConfigurationServiceItemConfigurationServiceGroupTypeCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceItemConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceItemConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceItemConfigurationServiceGroupTypeQuerySpecification qs = new ConfigurationServiceItemConfigurationServiceGroupTypeQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceItemConfigurationServiceGroupTypeCollection Fetch(ConfigurationServiceItemConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceItemConfigurationServiceGroupTypeCollection>();
		}

		public static int FetchCount(ConfigurationServiceItemConfigurationServiceGroupTypeQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceItemConfigurationServiceGroupTypeQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			SqlQuery q = ConfigurationServiceItemConfigurationServiceGroupTypeController.CreateQueryHelper(qs, VwMapConfigurationServiceItemConfigurationServiceGroupType.Schema, isCountQuery);
			return q;
		}

		#endregion


		#endregion

	}
}
