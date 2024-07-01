using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HP.ElementsCPS.Core.Security;

namespace HP.ElementsCPS.Data.SubSonicClient
{
    public partial class AppClientCollection
    {
        public List<AppClientId> ToAppClientList()
        {
            List<AppClientId> appClients = new List<AppClientId>(this.Count);
            foreach (AppClient client in this)
            {
                appClients.Add((AppClientId)client.Id);
            }
            return appClients;
        }

        public List<int> ToAppClientListInt()
        {
            List<int> appClients = new List<int>(this.Count);
            foreach (AppClient client in this)
            {
                appClients.Add(client.Id);
            }
            return appClients;
        }
    }
}
