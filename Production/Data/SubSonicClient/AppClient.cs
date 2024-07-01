using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HP.ElementsCPS.Core.Security;
using HP.ElementsCPS.Data.QuerySpecifications;

namespace HP.ElementsCPS.Data.SubSonicClient
{
   public partial class AppClient
    {
       public AppClientId AppClientId
       {
           get
           {
               return (AppClientId)this.Id;
           }
       }

       public static bool AreValidNames(string name)
       {
           AppClientQuerySpecification qs = new AppClientQuerySpecification();
           qs.Name = name;
           int count = AppClientController.FetchCount(qs);
           return count == 0;
       }

       public static bool AreValidNames(int id, string name)
       {
           AppClient appClient = AppClientController.FetchByID(id)[0];
           if (!appClient.Name.Equals(name))
           {
               AppClientQuerySpecification qs = new AppClientQuerySpecification();
               qs.Name = name;
               int count = AppClientController.FetchCount(qs);
               return count == 0;
           }
           else
           {
               return true;
           }

       }
    }
}
