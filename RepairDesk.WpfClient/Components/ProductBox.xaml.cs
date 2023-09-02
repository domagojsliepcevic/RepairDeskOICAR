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
	/// Interaction logic for ProductBox.xaml
	/// </summary>
	public partial class ProductBox : UserControl
	{
        public ProductBox()
		{
			InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = ((App)Application.Current).MainViewModel;
            if (button != null && mainViewModel != null && button.CommandParameter != null && button.CommandParameter is int)
            {
                mainViewModel.ShowProductCommand.Execute((int)button.CommandParameter);
            }
        }

        private async void Buy_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = ((App)Application.Current).MainViewModel;
            if (button != null && mainViewModel != null && button.CommandParameter != null && button.CommandParameter is int)
            {
                await mainViewModel.AddOneToCart((int)button.CommandParameter); // id
            }
        }
    }
}
