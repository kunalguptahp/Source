using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class TenantAppClient
    {
        public static void DestroyByTenantGroupId(int TenantGroupId)
        {
            Query query = PersonRole.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(TenantGroupIdColumn.ColumnName, TenantGroupId);
            PersonRoleController.DestroyByQuery(query);
        }
        public static void Destroy(int tenantGroupId, int appClientId)
        {
            Query query = TenantAppClient.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(TenantGroupIdColumn.ColumnName, tenantGroupId);
            query = query.WHERE(AppClientIdColumn.ColumnName, appClientId);
            TenantAppClientController.DestroyByQuery(query);

        }
    }
}
