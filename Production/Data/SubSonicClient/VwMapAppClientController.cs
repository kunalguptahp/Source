using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;


namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class VwMapAppClientController
    {
        #region ObjectDataSource Methods
        [DataObjectMethod(DataObjectMethodType.Select,true)]
        public static VwMapAppClientCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
            AppClientQuerySpecification qs = new AppClientQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
        {
            AppClientQuerySpecification qs = new AppClientQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
        }
#endregion
        #region QuerySpecification-related Methods

        public static VwMapAppClientCollection Fetch(AppClientQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteAsCollection<VwMapAppClientCollection>();
        }

        public static int FetchCount(AppClientQuerySpecification qs)
        {
            return CreateQuery(qs, true).GetRecordCount();
        }

        #region CreateQuery

        private static SqlQuery CreateQuery( AppClientQuerySpecification qs, bool isCountQuery)
        {
            //TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
            return AppClientController.CreateQueryHelper(qs, VwMapAppClient.Schema, isCountQuery);
        }

        #endregion

       
        #endregion

    }
}
