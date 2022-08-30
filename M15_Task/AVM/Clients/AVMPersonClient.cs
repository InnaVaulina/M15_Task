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
    internal class AVMPersonClient : AVMClient
    {

        protected string familyName;         // фамилия
        protected string firstName;          // имя
        protected string patronymicName;     // отчество


        public AVMPersonClient(User user, Person client) : base(user, client)
        {
            if (client != null) 
            {
                familyName = (base.selectedClient as Person).FamilyName;
                firstName = (base.selectedClient as Person).FirstName;
                patronymicName = (base.selectedClient as Person).PatronymicName;
            }
            else 
            {
                familyName = "";
                firstName = "";
                patronymicName = "";
            }    

        }


        public string FamilyName 
        {
            get { return familyName; }
            set { familyName = value; }
            
        }        
        public string FirstName 
        {
            get { return firstName; }
            set { firstName = value; }
        }          
        public string PatronymicName 
        {
            get { return patronymicName; }
            set { patronymicName = value; }
        }    


        public override void SaveChanges() 
        {
            if(base.selectedClient != null) 
            {
                (base.selectedClient as Person).FamilyName = familyName;
                (base.selectedClient as Person).FirstName = firstName;
                (base.selectedClient as Person).PatronymicName = patronymicName;
                base.selectedClient.Phone = phone;
            }
                 
        }


    }
}
