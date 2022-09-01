using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace M13_Library
{
    public class BankSystem : ITransfer<Account>, INotifyPropertyChanged
    {

        public ObservableCollection<Client> Clients { get; set; }           //клиенты
        public Dictionary<string, Account> accounts;        // счета

        public Client manageClient;                         // касса 
        Account cash;

        public Account Cash
        {
            get { return cash; }
            set { cash = value; OnPropertyChanged("Cash"); }
        }


        public BankSystem()
        {

            Clients = new ObservableCollection<Client>();
            accounts = new Dictionary<string, Account>();
            manageClient = null;
            Cash = null;

        }


        /// <summary>
        /// создать кассу (касса это счет типа Account, принадлежащий банку)
        /// </summary>
        /// <param name="cForm">номер клиента</param>
        /// <param name="aForm">номер счета</param>
        public void MakeCash()
        {
            this.manageClient = new Client();
            this.NewAccount(ref this.manageClient);
            this.Cash = this.manageClient.Accounts[0] as Account;

        }


        /// <summary>
        /// добавить клиента физ.лицо
        /// </summary>
        public Person NewPersonClient(
            string familyName,
            string firstName,
            string patronymicName)
        {
            Person person = new Person(familyName, firstName, patronymicName);
            Clients.Add(person);
            OnPropertyChanged("Clients");
            return person;

        }

        /// <summary>
        /// добавить клиента предпринимателя
        /// </summary>
        public Businessman NewBusinessmanClient(
            string familyName,
            string firstName,
            string patronymicName,
            string inn)
        {
            Businessman businessman = new Businessman(
                familyName, firstName, patronymicName, inn);
            Clients.Add(businessman);
            return businessman;
        }

        /// <summary>
        /// добавить клиента организацию
        /// </summary>
        public Organization NewOrganisationClient(
            string name,
            string inn,
            string representative)
        {
            Organization organization = new Organization(name, inn, representative);
            Clients.Add(organization);
            OnPropertyChanged("Clients");
            return organization;

        }


        /// <summary>
        /// новый счет типа Account
        /// </summary>
        public Account NewAccount(ref Client client)
        {
            Account myaccount = new Account();
            client.Accounts.Add(myaccount as IAccount);
            myaccount.Client = client as IClient;
            accounts.Add(myaccount.AccountNumber, myaccount);
            return myaccount;
        }


        /// <summary>
        /// новый счет типа DepositAccount
        /// </summary>
        public DepositAccount NewDepositAccount(ref Client client)
        {
            DepositAccount myaccount = new DepositAccount();
            client.Accounts.Add(myaccount as IAccount);
            myaccount.Client = client as IClient;
            accounts.Add(myaccount.AccountNumber, myaccount);
            return myaccount;
        }

        /// <summary>
        /// закрыть счет
        /// </summary>
        public void CloseAccount(ref Client client, string aForm)
        {
            foreach (Account account in client.Accounts)
                if (account.AccountNumber.CompareTo(aForm) == 0) account.CloseAccount();

        }

        /// <summary>
        /// перевод между счетами с использованием ковариантного интерфейса для определения типа счета
        /// </summary>
        /// <param name="get">счет источник</param>
        /// <param name="put">счет приемник</param>
        /// <param name="amount">сумма</param>
        /// <returns>true в случае успешности перевода</returns>
        public bool TransferCov(VariantCov<Account> get, VariantCov<Account> put, float amount)
        {

            if (get.Account.GetMoney(put.Account.Client, amount))
            {
                if (put.Account.PutMoney(get.Account.Client, amount)) return true;
                else get.Account.PutMoney(put.Account.Client, amount);

            }
            return false;

        }

        /// <summary>
        /// перевод между счетами с использованием контрвариантного интерфейса для определения типа счета
        /// </summary>
        /// <param name="get">счет источник</param>
        /// <param name="put">счет приемник</param>
        /// <param name="amount">сумма</param>
        /// <returns>true в случае успешности перевода</returns>
        public bool TransferContr(Account get, Account put, float amount)
        {

            if (get.GetMoney(put.Client, amount))
            {
                if (put.PutMoney(get.Client, amount)) return true;
                else get.PutMoney(put.Client, amount);

            }
            return false;
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
