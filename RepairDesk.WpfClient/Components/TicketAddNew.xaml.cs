using RepairDesk.WpfClient.ViewModels;
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

namespace RepairDesk.WpfClient.Components
{
	/// <summary>
	/// Interaction logic for TicketAddNew.xaml
	/// </summary>
	public partial class TicketAddNew : UserControl
	{
		public TicketAddNew()
		{
			InitializeComponent();
		}

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = ((App)Application.Current).MainViewModel;
            if (button != null && mainViewModel != null)
            {
                // validation
                if (mainViewModel.SelectedUser == null || mainViewModel.SelectedUser.Id == 0) 
                { 
                    MessageBox.Show("Please select a user."); 
                    return; 
                }
                if (!RequestDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Set request date");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Description.Text))
                {
                    MessageBox.Show("Add description for repair.");
                    return;
                }

                // save repair ticket
                if (await mainViewModel.SaveTicketForRepair())
                {
                    mainViewModel.Ticket = new ApiClient.RepairAddModel { RequestDate = DateTime.Now };
                    MessageBox.Show("Ticket for repair saved.");
                }
                else
                {
                    MessageBox.Show("Ticket for repair NOT saved.");
                }
            }
        }
    }
}
