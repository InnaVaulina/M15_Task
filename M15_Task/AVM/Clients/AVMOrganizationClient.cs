using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M13_Library;
using M14_Library;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace M15_Task
{
    internal class AVMOrganizationClient : AVMClient
    {

        protected string name;               // Название
        protected string inn;                // налоговый номер
        protected string representative;     // представитель


        public AVMOrganizationClient(User user, Organization client): base(user, client)
        {
            if (client != null) 
            {
                name = (base.selectedClient as Organization).OrganizationName;
                representative = (base.selectedClient as Organization).Representative;
                inn = (base.selectedClient as Organization).INN;
            }
            else 
            {
                name = "";
                representative = "";
                inn = "";
            }
        }


        public string OrganizationName
        {
            get { return name; }
            set { name = value; }

        }
        public string INN
        {
            get { return inn; }
            set { inn = value; }
        }
        public string Representative
        {
            get { return representative; }
            set { representative = value; }
        }


        public override void SaveChanges()
        {
            if (base.selectedClient != null) 
            {
                (base.selectedClient as Organization).OrganizationName = name;
                (base.selectedClient as Organization).INN = inn;
                (base.selectedClient as Organization).Representative = representative;
                base.selectedClient.Phone = phone;
            }
 
        }



    }
}
