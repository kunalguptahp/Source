using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HP.ElementsCPS.Core.Security;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class ApplicationCollection
    {
        public List<RoleApplicationId> ToApplicationList()
        {
            List<RoleApplicationId> Applications = new List<RoleApplicationId>(this.Count);
            foreach (Application client in this)
            {
                Applications.Add((RoleApplicationId)client.Id);
            }
            return Applications;
        }

        public List<int> ToApplicationListInt()
        {
            List<int> Applications = new List<int>(this.Count);
            foreach (Application client in this)
            {
                Applications.Add(client.Id);
            }
            return Applications;
        }
    }
}
