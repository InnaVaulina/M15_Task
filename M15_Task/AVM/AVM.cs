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
    internal class AVM : INotifyPropertyChanged, IAVMSelectClient<AVMClient>
    {

        BankSystem bankA = new BankSystem();  // банк
        ManagerForNewClient user1;            // вносит данные о новых клиентах
        AccountManager user2;                 // работает со счетами
        DepositManager user3;                 // работает с депозитами
        Consultant user4;                     // консультант

        History history;                      // журналы


        //___представления для функционалов менеджеров

        AVMManagerForNewClient user1view;
        AVMAccountManager user2view;
        AVMDepositManager user3view;
        AVMConsultant user4view;

        AVMUser selectedUser;                 // представление выбранного менеджера
        AVMClient clientView;                 // представление выбранного клиента
        Client selectedClient;                // выбранный клиент


        public event MistakeMessageHendler MistakeMessageNotify;  // если ошибка

        /// <summary>
        /// выбран менеджер
        /// </summary>
        public AVMUser SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
                ClientView = SelectClientAVM(SelectedUser.User, SelectedClient);
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
                ClientView = SelectClientAVM(SelectedUser.User, SelectedClient);
            }
        }

        /// <summary>
        /// представление данных клиента
        /// </summary>
        public AVMClient ClientView
        {
            get { return clientView; }
            set
            {
                clientView = value;
                SelectedUser.Client = clientView;
                OnPropertyChanged("ClientView");
            }
        }

        /// <summary>
        /// список клиентов банка
        /// </summary>
        public ObservableCollection<Client> BankA_Clients
        {
            get { return bankA.Clients; }
        }

        /// <summary>
        /// касса
        /// </summary>
        public Account Cash
        {
            get { return user2.Cash; }
        }


        public AccountManager User2 { get { return user2; } }
        public DepositManager User3 { get { return user3; } }

        /// <summary>
        /// журнал переводов
        /// </summary>
        public ObservableCollection<Log1> History_Transfers
        {
            get { return history.transfers; }
        }

        /// <summary>
        /// журнал изменений данных о клиентах
        /// </summary>
        public ObservableCollection<Log2> History_AddClient
        {
            get { return history.addClient; }
        }


        //__команды

        WeirdCommand finishWorkWithClient;   // завершить работу с клиентом
        public WeirdCommand FinishWorkWithClient { get { return finishWorkWithClient; } }

        WeirdCommand setVievForConsultant;   // установить пользователем консультанта
        public WeirdCommand SetVievForConsultant { get { return setVievForConsultant; } }

        WeirdCommand setVievForAccountManager;   // установить пользователем менеджера по переводам дс
        public WeirdCommand SetVievForAccountManager { get { return setVievForAccountManager; } }

        WeirdCommand setVievForDepositManager;   // установить пользователем менеджера по депозитам
        public WeirdCommand SetVievForDepositManager { get { return setVievForDepositManager; } }

        WeirdCommand setVievForNewClientManager;   // установить пользователем менеджера по клиентам
        public WeirdCommand SetVievForNewClientManager { get { return setVievForNewClientManager; } }


        /// <summary>
        /// основное представление
        /// </summary>
        public AVM() 
        {
            //____
            bankA.MakeCash();   

            user1 = new ManagerForNewClient("Алексей", bankA);           
            user2 = new AccountManager("Андрей", bankA);
            user3 = new DepositManager("Артем", bankA);
            user4 = new Consultant("Антон", bankA);
            user1.ClientAddNotify += user2.OnNewClientAdd2; 

            history = new History(user1, user2, user3, user4); 

            // данные в системе

            user1.MFNewPersonClient("Иванов", "Иван", "Иванович");
            user1.TheClient.Phone = "123456";

            user1.MFNewOrganisationClient("OOO Ромашка", "112", "Романов Роман Романович");
            user1.TheClient.Phone = "978564";
            user1.TheClient = null;
            //__________________________________________________________________________

            user1view = new AVMManagerForNewClient(user1, this);
            user2view = new AVMAccountManager(user2);
            user3view = new AVMDepositManager(user3);
            user4view = new AVMConsultant(user4);

            user2view.MistakeMessageNotify += Mistake;

            SelectedUser = user1view;
            SelectedClient = null;

            //___комманды________________________________________________________________


            setVievForConsultant = new WeirdCommand(o => { SelectedUser = user4view; });

            setVievForAccountManager = new WeirdCommand(o => { SelectedUser = user2view; });

            setVievForDepositManager = new WeirdCommand(o => { SelectedUser = user3view; });

            setVievForNewClientManager = new WeirdCommand(o => { SelectedUser = user1view; });

            finishWorkWithClient = new WeirdCommand(o => { SelectedClient = null; });

        }

        /// <summary>
        /// выбор представления клиента для выбранного менеджера
        /// реализует ковариантный интерфейс IAVMSelectClient<AVMClient>
        /// </summary>
        /// <param name="user">менеджер или консультант</param>
        /// <param name="client">клиент</param>
        /// <returns></returns>
        public AVMClient SelectClientAVM(User user, Client client)
        {
            if (client == null)
            {
                return new AVMClient(user, null);
            }
            else
            {
                string clientType = client.GetType().ToString();
                if (user.GetType() == typeof(ManagerForNewClient))
                {

                    switch (clientType)
                    {
                        case "M13_Library.Organization":
                            return new AVMOrganizationClientChange(user, client as Organization);
                        case "M13_Library.Person":
                            return new AVMPersonClientChange(user, client as Person);
                    }
                }
                else
                {
                    switch (clientType)
                    {
                        case "M13_Library.Organization":
                            return new AVMOrganizationClient(user, client as Organization);
                        case "M13_Library.Person":
                            return new AVMPersonClient(user, client as Person);
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// выбор клиента рализован с помощью событий
        /// </summary>
        /// <param name="client"></param>
        public void SetViewNewClient(Object client) 
        {
            SelectedClient = client as Client;
        }

        /// <summary>
        /// сообщение об ошибке
        /// </summary>
        /// <param name="user"></param>
        /// <param name="e"></param>
        public void Mistake(AVMUser user, MistakeEventArgs e) 
        {
            MistakeMessageNotify?.Invoke(user, e);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
