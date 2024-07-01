using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;

namespace HP.ElementsCPS.Data.SubSonicClient
{
   public partial class Tenant
    {
       public void AddAppClient(AppClientId ClientId)
       {
           TenantAppClient newClient = new TenantAppClient(true);
           newClient.TenantGroupId = this.Id;
           newClient.AppClientId = (int)ClientId;
           newClient.Save();
       }
       public void RemoveAppClient(AppClientId ClientId)
       {
           TenantAppClient.Destroy(this.Id, (int)ClientId);
       }

       public void SetAppClients(List<AppClientId> newAppClients)
       {
           List<AppClientId> oldAppClients = new List<AppClientId>();
           AppClientCollection oldAppCols = Tenant.GetAppClientCollection(this.Id);
           foreach (AppClient app in oldAppCols)
           {
               oldAppClients.Add((AppClientId)app.Id);
           }
           IEnumerable<AppClientId> removedClients = oldAppClients.Except(newAppClients);
           IEnumerable<AppClientId> addedClients = newAppClients.Except(oldAppClients);

           foreach (AppClientId clientId in removedClients)
           {
               this.RemoveAppClient(clientId);
           }

           foreach (AppClientId clientId in addedClients)
           {
               this.AddAppClient(clientId);
           }

       }

       public static bool AreValidNames(string name)
       {
           TenantQuerySpecification qs =new TenantQuerySpecification();
           qs.Name = name;
           int count = TenantController.FetchCount(qs);
           return count == 0;
       }

       public static bool AreValidNames(int id,string name)
       {
           Tenant tenant = TenantController.FetchByID(id)[0];
           if (!tenant.Name.Equals(name))
           {
               TenantQuerySpecification qs = new TenantQuerySpecification();
               qs.Name = name;
               int count = TenantController.FetchCount(qs);
               return count == 0;
           }
           else
           {
               return true;
           }
           
       }

    }
}
