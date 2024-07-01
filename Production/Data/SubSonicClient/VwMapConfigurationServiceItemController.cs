using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapGroupType class.
	/// </summary>
	public partial class VwMapConfigurationServiceItemController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapConfigurationServiceItemCollection ODSFetch( string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceItemQuerySpecification qs = new ConfigurationServiceItemQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml)); 
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceItemQuerySpecification qs = new ConfigurationServiceItemQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceItemCollection Fetch(ConfigurationServiceItemQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceItemCollection>();
		}

        public static DataTable FetchToDataTable(ConfigurationServiceItemQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(ConfigurationServiceItemQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceItemQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceItemController.CreateQueryHelper(qs, VwMapConfigurationServiceItem.Schema, isCountQuery);
		}

		#endregion

		#endregion

	}
}
