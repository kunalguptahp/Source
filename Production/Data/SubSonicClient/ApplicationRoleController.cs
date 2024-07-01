using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HP.ElementsCPS.Core.Security;
using System.ComponentModel;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class ApplicationRoleController
    { 
        public static List<UserRoleId> GetCurrentRoleListByApp(int personId, RoleApplicationId application)
        {
            ApplicationRoleCollection apps = GetApplicationRoleCollection(personId);
            List<UserRoleId> defaultAppRole = new List<UserRoleId>();
            List<UserRoleId> JSAppRole = new List<UserRoleId>();
            List<UserRoleId> CFSAppRole = new List<UserRoleId>();
            List<UserRoleId> ReaAppRole = new List<UserRoleId>();
            // Dictionary<RoleApplicationId, UserRoleId> dic = new Dictionary<RoleApplicationId, UserRoleId>();
            foreach (ApplicationRole app in apps)
            {
                //dic.Add((RoleApplicationId)app.ApplicationId, (UserRoleId)app.RoleId);
                if ((RoleApplicationId)app.ApplicationId == RoleApplicationId.Default)
                {
                    defaultAppRole.Add((UserRoleId)app.RoleId);
                }
                if ((RoleApplicationId)app.ApplicationId == RoleApplicationId.Jumpstation)
                {
                    JSAppRole.Add((UserRoleId)app.RoleId);
                }
                if ((RoleApplicationId)app.ApplicationId == RoleApplicationId.ConfigureService)
                {
                    CFSAppRole.Add((UserRoleId)app.RoleId);
                }
                if ((RoleApplicationId)app.ApplicationId == RoleApplicationId.Reach)
                {
                    ReaAppRole.Add((UserRoleId)app.RoleId);
                }
            }
            if (application == RoleApplicationId.Default)
            {
                return defaultAppRole;
            }

            if (application == RoleApplicationId.Jumpstation)
            {
                return JSAppRole;
            }
            if (application == RoleApplicationId.ConfigureService)
            {
                return CFSAppRole;
            }
            if (application == RoleApplicationId.Reach)
            {
                return ReaAppRole;
            }
            return null;
        }


        public static ApplicationCollection GetCurrentAppColByRole(int personId, UserRoleId role)
        {
            ApplicationCollection appCol = new ApplicationCollection();
            List<RoleApplicationId> apps = GetCurrentAppListByRole(personId, role);
            foreach (RoleApplicationId app in apps)
            {
                appCol.Add(Application.FetchByID((int)app));
            }
            return appCol;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static ApplicationRoleCollection GetApplicationRoleCollection(int personId)
        {
            return new ApplicationRoleCollection().Where(ApplicationRole.PersonIdColumn.ColumnName, personId).Load();
        }

        public static List<RoleApplicationId> GetCurrentAppListByRole(int personId, UserRoleId role)
        {
            ApplicationRoleCollection apps = GetApplicationRoleCollection(personId);
            List<RoleApplicationId> viewApp = new List<RoleApplicationId>();
            List<RoleApplicationId> editApp = new List<RoleApplicationId>();
            List<RoleApplicationId> validateApp = new List<RoleApplicationId>();
            List<RoleApplicationId> dataAdminApp = new List<RoleApplicationId>();

            foreach (ApplicationRole app in apps)
            {
                //dic.Add((RoleApplicationId)app.ApplicationId, (UserRoleId)app.RoleId);
                if ((UserRoleId)app.RoleId == UserRoleId.Viewer)
                {
                    viewApp.Add((RoleApplicationId)app.ApplicationId);
                }
                if ((UserRoleId)app.RoleId == UserRoleId.Editor)
                {
                    editApp.Add((RoleApplicationId)app.ApplicationId);
                }
                if ((UserRoleId)app.RoleId == UserRoleId.Validator)
                {
                    validateApp.Add((RoleApplicationId)app.ApplicationId);
                }
                if ((UserRoleId)app.RoleId == UserRoleId.DataAdmin)
                {
                    dataAdminApp.Add((RoleApplicationId)app.ApplicationId);
                }
            }

            if (role == UserRoleId.Viewer)
            {
                return viewApp;
            }
            if (role == UserRoleId.Editor)
            {
                return editApp;
            }
            if (role == UserRoleId.Validator)
            {
                return validateApp;
            }
            if (role == UserRoleId.DataAdmin)
            {
                return dataAdminApp;
            }
            return null;
        }
    }
}
