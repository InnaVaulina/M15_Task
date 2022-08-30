using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using M13_Library;
using M14_Library;

namespace M15_Task
{


    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AVM myAVM;
        WindowClientChoice windowClientChoice;
        WindowLogs windowLogs;

        public MainWindow()
        {
            InitializeComponent();
            myAVM = new AVM();
            windowClientChoice = new WindowClientChoice();
            windowLogs = new WindowLogs();
            DataContext = myAVM;
            windowClientChoice.DataContext = myAVM;
            windowLogs.DataContext = myAVM;
            windowClientChoice.Show();
            windowLogs.Show();

            myAVM.MistakeMessageNotify += OnMistake;
            
        }

        private void OnClosed(object sender, EventArgs e)
        {
            windowClientChoice.Close();
            windowLogs.Close();
        }


        private void ClientListShow_Click(object sender, RoutedEventArgs e)
        {
            windowClientChoice.Show();
        }
 

        private void ClientListHide_Click(object sender, RoutedEventArgs e)
        {
            windowClientChoice.Hide();
        }

        private void LogShow_Click(object sender, RoutedEventArgs e)
        {
            windowLogs.Show();
        }

        private void LogHide_Click(object sender, RoutedEventArgs e)
        {
            windowLogs.Hide();
        }

        /// <summary>
        /// выбор нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientChoice_Click(object sender, RoutedEventArgs e)
        {           
                myAVM.SetViewNewClient(windowClientChoice.ClientList.SelectedItem);
        }

        /// <summary>
        /// выбор счета для работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (accountList.SelectedItem != null)
            {
                if(accountList.SelectedItem.GetType() == typeof(Account)) 
                {
                    Account x = accountList.SelectedItem as Account;
                    if (myAVM.SelectedUser.User.GetType() == typeof(AccountManager))
                        myAVM.User2.AccountInWork = x;
                    if (myAVM.SelectedUser.User.GetType() == typeof(DepositManager))
                        myAVM.User3.AccountInWork = x;

                }
                if (accountList.SelectedItem.GetType() == typeof(DepositAccount)) 
                {
                    DepositAccount x = accountList.SelectedItem as DepositAccount;
                    if (myAVM.SelectedUser.User.GetType() == typeof(DepositManager))
                        myAVM.User3.DepositInWork = x;
                }
            }

        }

        /// <summary>
        /// сообщения при обработке ошибок ввода данных
        /// </summary>
        /// <param name="user"></param>
        /// <param name="e"></param>
        public void OnMistake(AVMUser user, MistakeEventArgs e)
        {
            MessageBox.Show(e.MistakeMessage);
        }



    }
}
