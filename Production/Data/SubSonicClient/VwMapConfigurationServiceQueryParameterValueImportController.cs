using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
	/// <summary>
	/// Strongly-typed collection for the VwMapConfigurationServiceQueryParameterValueImport class.
	/// </summary>
    public partial class VwMapConfigurationServiceQueryParameterValueImportController
	{

		#region ObjectDataSource Methods

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static VwMapConfigurationServiceQueryParameterValueImportCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
		{
			ConfigurationServiceQueryParameterValueImportQuerySpecification qs = new ConfigurationServiceQueryParameterValueImportQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return Fetch(qs);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static int ODSFetchCount(string serializedQuerySpecificationXml)
		{
			ConfigurationServiceQueryParameterValueImportQuerySpecification qs = new ConfigurationServiceQueryParameterValueImportQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
			return FetchCount(qs);
		}

		#endregion

		#region QuerySpecification-related Methods

		public static VwMapConfigurationServiceQueryParameterValueImportCollection Fetch(ConfigurationServiceQueryParameterValueImportQuerySpecification qs)
		{
			return CreateQuery(qs, false).ExecuteAsCollection<VwMapConfigurationServiceQueryParameterValueImportCollection>();
		}

		public static int FetchCount(ConfigurationServiceQueryParameterValueImportQuerySpecification qs)
		{
			return CreateQuery(qs, true).GetRecordCount();
		}

		#region CreateQuery

		private static SqlQuery CreateQuery(ConfigurationServiceQueryParameterValueImportQuerySpecification qs, bool isCountQuery)
		{
			//TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
			return ConfigurationServiceQueryParameterValueImportController.CreateQueryHelper(qs, VwMapConfigurationServiceQueryParameterValueImport.Schema, isCountQuery);
		}

		#endregion

        #region Other Querying Methods

		#endregion

    }

        #endregion

}
