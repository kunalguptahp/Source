using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.HPFx.Data.Query;
using SubSonic;


namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class VwMapTenantController
    {
        #region ObjectDataSource Methods
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static VwMapTenantCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
        {
            TenantQuerySpecification qs = new TenantQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return Fetch(qs);
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static int ODSFetchCount(string serializedQuerySpecificationXml)
        {
            TenantQuerySpecification qs = new TenantQuerySpecification(QuerySpecification.FromXml(serializedQuerySpecificationXml));
            return FetchCount(qs);
        }
        #endregion
        #region QuerySpecification-related Methods

        public static VwMapTenantCollection Fetch(TenantQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteAsCollection<VwMapTenantCollection>();
        }

        public static int FetchCount(TenantQuerySpecification qs)
        {
            return CreateQuery(qs, true).GetRecordCount();
        }

        #region CreateQuery

        private static SqlQuery CreateQuery(TenantQuerySpecification qs, bool isCountQuery)
        {
            //TODO: Implement: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
#warning Not Implemented: Validate that all of the Query conditions specified by the QuerySpecification are supported by this method.
            return TenantController.CreateQueryHelper(qs, VwMapTenant.Schema, isCountQuery);
        }

        #endregion


        #endregion

    }
}
