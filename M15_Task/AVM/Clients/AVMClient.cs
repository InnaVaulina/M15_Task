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
    public class AVMClient : INotifyPropertyChanged
    {
        protected User selectedUser;
        protected Client selectedClient;

        protected string phone;

        public AVMClient(User user, Client client)
        {
      
            selectedUser = user;
            selectedUser.TheClient = client;
            if (client != null) 
            {
                this.selectedClient = client;
                phone = selectedClient.Phone;
            }
            else 
            {
                phone = "";
            }
                
        }

        /// <summary>
        /// выбран менеджер
        /// </summary>
        public User SelectedUser 
        { 
            get { return selectedUser; }
            set 
            {
                selectedUser.TheClient = null;
                selectedUser = value;
                selectedUser.TheClient = selectedClient;

            }
        }

        /// <summary>
        /// выбран клиент
        /// </summary>
        public Client SelectedClient 
        { 
            get { return selectedClient; }
            set 
            { 
                selectedClient = value;
                selectedUser.TheClient = selectedClient;

            }
        }

        

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }


        public ObservableCollection<IAccount> Accounts
        {
            get { return selectedClient.Accounts; }
        }


        public virtual void SaveChanges(){}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
