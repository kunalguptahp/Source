using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using HP.ElementsCPS.Data.QuerySpecifications;
using HP.ElementsCPS.Data.Utility;
using HP.ElementsCPS.Core.Security;
namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class ApplicationRole
    {
        public static void DestroyByRoleId(int roleId)
        {
            Query query = ApplicationRole.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(RoleIdColumn.ColumnName, roleId);
            ApplicationRoleController.DestroyByQuery(query);
        }

        public static void Destroy(int personId ,int roleId, int appId)
        {
            Query query = ApplicationRole.CreateQuery();
            query.QueryType = QueryType.Delete;
            query = query.WHERE(PersonIdColumn.ColumnName, personId);
            query = query.WHERE(ApplicationIdColumn.ColumnName, appId);
            query = query.WHERE(RoleIdColumn.ColumnName, roleId);
            ApplicationRoleController.DestroyByQuery(query);
        }

       
        
    }
}
