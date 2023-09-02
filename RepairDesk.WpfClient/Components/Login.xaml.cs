using RepairDesk.WpfClient.Converters;
using RepairDesk.WpfClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RepairDesk.WpfClient.Components
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = DataContext as MainViewModel;
            if (button != null && mainViewModel != null && !string.IsNullOrWhiteSpace(Username.Text)
                && !string.IsNullOrWhiteSpace(Password.Password))
            {
                if (!await mainViewModel.Login(Username.Text, Password.Password))
                {
                    MessageBox.Show("Invalid username or password.");
                }
                else
                {
                    Username.Text = null;
                    Password.Password = null;
                }
            }
        }
    }
}
