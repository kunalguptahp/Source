using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;
using System.Collections.Generic;
using System.Data;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapConfigurationServiceLabel class.
	/// </summary>
	public partial class VwMapConfigurationServiceLabelController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapConfigurationServiceLabelCollection ODSFetch( string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceLabelQuerySpecification qs = new ConfigurationServiceLabelQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount( string serializedQuerySpecificationXml)
		{
			ConfigurationServiceLabelQuerySpecification qs = new ConfigurationServiceLabelQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));         
            return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceLabelCollection Fetch(ConfigurationServiceLabelQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceLabelCollection>();
		}

        public static DataTable FetchToDataTable(ConfigurationServiceLabelQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteDataSet().Tables[0];
        }

		public static int FetchCount(ConfigurationServiceLabelQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceLabelQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceLabelController.CreateQueryHelper(qs, VwMapConfigurationServiceLabel.Schema, isCountQuery);
		}

		#endregion

		#region Other Querying Methods

		#endregion

		#endregion

	}
}
