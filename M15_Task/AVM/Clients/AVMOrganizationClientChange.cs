using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M13_Library;
using M14_Library;

namespace M15_Task
{
    internal class AVMOrganizationClientChange : AVMOrganizationClient
    {
        public AVMOrganizationClientChange(User user, Organization client) : base(user, client) { }

        public override void SaveChanges()
        {
            if (base.selectedClient != null)
            {
                (base.selectedClient as Organization).OrganizationName = name;
                (base.selectedClient as Organization).INN = inn;
                (base.selectedClient as Organization).Representative = representative;
                base.selectedClient.Phone = phone;
            }
            else
            {
                (selectedUser as ManagerForNewClient).MFNewOrganisationClient(name, inn, representative);
                selectedUser.TheClient.Phone = phone;
                selectedClient = selectedUser.TheClient;
            }

        }
    }
}
