using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;


namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class VwMapApplicationController
    {
        #region ObjectDataSource Methods
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapApplicationCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
            ApplicationQuerySpecification qs = new ApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
        {
            ApplicationQuerySpecification qs = new ApplicationQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
        }
        #endregion
        #region QuerySpecification-related Methods

        public static VwMapApplicationCollection Fetch(ApplicationQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteAsCollection<VwMapApplicationCollection>();
        }

        public static int FetchCount(ApplicationQuerySpecification qs)
        {
            return CreateQuery(qs, true).GetRecordCount();
        }

        #region CreateQuery

        private static SqlQuery CreateQuery(ApplicationQuerySpecification qs, bool isCountQuery)
        {
            //TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
            return ApplicationController.CreateQueryHelper(qs, VwMapApplication.Schema, isCountQuery);
        }

        #endregion


        #endregion

    }
}
