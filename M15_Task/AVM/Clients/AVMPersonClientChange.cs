using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M13_Library;
using M14_Library;

namespace M15_Task
{
    internal class AVMPersonClientChange: AVMPersonClient
    {
        public AVMPersonClientChange(User user, Person client) : base(user, client) { }

        public override void SaveChanges()
        {
            if (base.selectedClient != null)
            {
                (base.selectedClient as Person).FamilyName = familyName;
                (base.selectedClient as Person).FirstName = firstName;
                (base.selectedClient as Person).PatronymicName = patronymicName;
                base.selectedClient.Phone = phone;
            }
            else
            {

                (selectedUser as ManagerForNewClient).MFNewPersonClient(familyName, firstName, patronymicName);
                selectedUser.TheClient.Phone = phone;
                selectedClient = selectedUser.TheClient;
            }

        }
    }
}
