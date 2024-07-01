using System.ComponentModel;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.HPFx.Data.Query;
using HP.HPFx.Extensions.SubSonic;
using HP.HPFx.Utility.SubSonic;
using SubSonic;
using System.Collections.Generic;
using HP.ElementsCPS.Core.Security;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    /// <summary>
    /// The non-generated portion of the GroupTypeController class.
    /// </summary>
    public partial class TenantController
    {

        #region ObjectDataSource Methods

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static TenantCollection ODSFetch(string serializedQuerySpecificationXml, int maximumRows, int startRowIndex, string sortExpression)
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

        public static TenantCollection Fetch(TenantQuerySpecification qs)
        {
            return CreateQuery(qs, false).ExecuteAsCollection<TenantCollection>();
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
            return TenantController.CreateQueryHelper(qs, Tenant.Schema, isCountQuery);
        }

        #endregion

        #region CreateQueryHelper

        internal static SqlQuery CreateQueryHelper(TenantQuerySpecification qs, TableSchema.Table schema, bool isCountQuery)
        {
            SqlQuery query = DB.Select().From(schema);

            if (qs.Name != null)
            {
                ElementsCPSSubSonicUtility.AddStandardQueryCondition_AndWhereIsEqualTo(query, "Name", qs.Name);
            }

            ElementsCPSSubSonicUtility.AddGenericConditionsToQuery(query, qs);

            if (!isCountQuery)
            {
                SubSonicUtility.SetPaging(query, qs.Paging);
                SubSonicUtility.SetOrderBy(query, qs.SortBy);
            }

            ElementsCPSSubSonicUtility.LogQuerySql(query, isCountQuery, typeof(TenantController));
            return query;
        }

        #endregion

        #region SmartFetch Method

        public static IRecordCollection SmartFetch(TenantQuerySpecification qs, bool includeRowStatusNameInOutput)
        {
            if (includeRowStatusNameInOutput)
            {
                return VwMapTenantController.Fetch(qs);
            }
            else
            {
                return TenantController.Fetch(qs);
            }
        }

        #endregion

        #endregion

        #region  appclient related method
        public static List<AppClientId> GetAppClients(int TenantGroupId)
        {
            List<AppClientId> AppClientIds = Tenant.GetAppClientCollection(TenantGroupId).ToAppClientList();
            return AppClientIds;
        }

        public static List<int> GetAppClientsInt(int TenantGroupId)
        {
            List<int> AppClientIds = Tenant.GetAppClientCollection(TenantGroupId).ToAppClientListInt();
            return AppClientIds;
        }
        #endregion

    }
}
