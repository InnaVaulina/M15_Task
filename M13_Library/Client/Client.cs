using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace M13_Library
{
    /// <summary>
    /// клиент
    /// </summary>
    public class Client : IClient, INotifyPropertyChanged
    {
        static int id;
        static Client()
        {
            id = 0;
        }

        int clientId;
        string phone;

        public ObservableCollection<IAccount> Accounts { get; set; }

        public event Action<string, string> PropertyChange;


        /// <summary>
        /// создан клиент
        /// </summary>
        public Client()
        {
            clientId = id;
            id++;
            Accounts = new ObservableCollection<IAccount>();
            phone = "";
        }

        public void SetProperty<T>(ref T property, T val, string change)
        {
            if (!val.Equals(property))
            {
                property = val;
                PropertyChange(change, val.ToString());
                OnPropertyChanged(change);
            }
        }

        public string Phone
        {
            get { return phone; }
            set { SetProperty<string>(ref phone, value, nameof(Phone)); }
        }

        public int ClientId { get { return clientId; } }

        public virtual string Name() { return "Касса"; }



        /// <summary>
        /// клиент открывает счет
        /// </summary>
        /// <returns></returns>
        public void AddAccount(IAccount newAccount)
        {
            this.Accounts.Add(newAccount);
            OnPropertyChanged("Accounts");
        }

        public Account this[string nomber]
        {
            get
            {
                foreach (var account in this.Accounts)
                    if (account.AccountNumber.CompareTo(nomber) == 0)
                        return account as Account;
                return null;
            }
        }

        public override string ToString()
        {
            return $"{this.clientId}:";
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
