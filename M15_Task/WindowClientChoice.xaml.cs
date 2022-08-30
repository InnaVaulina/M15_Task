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
using System.Windows.Shapes;
using M13_Library;

namespace M15_Task
{
    /// <summary>
    /// Логика взаимодействия для WindowClientCoice.xaml
    /// </summary>
    public partial class WindowClientChoice : Window
    {
        public WindowClientChoice()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void ClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientList.SelectedItem != null)
            {
                AccountList.UnselectAll();
                Client selectedClient = ClientList.SelectedItem as Client;
                AccountList.ItemsSource = selectedClient.Accounts;
               
            }
        }
    }
}
