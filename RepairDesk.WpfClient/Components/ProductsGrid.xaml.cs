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
	/// Interaction logic for ProductsGrid.xaml
	/// </summary>
	public partial class ProductsGrid : UserControl
	{
		public ProductsGrid()
		{
			InitializeComponent();
		}

        private async void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = ((App)Application.Current).MainViewModel;
            if (button != null && mainViewModel != null)
            {
                await mainViewModel.PreviousProductsPage();
            }
        }

        private async void NextPage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = ((App)Application.Current).MainViewModel;
            if (button != null && mainViewModel != null)
            {
                await mainViewModel.NextProductsPage();
            }
        }
    }
}
