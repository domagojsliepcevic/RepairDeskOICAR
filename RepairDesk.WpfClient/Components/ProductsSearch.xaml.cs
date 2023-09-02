using RepairDesk.WpfClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	/// Interaction logic for ProductsSearch.xaml
	/// </summary>
	public partial class ProductsSearch : UserControl
	{
		public ProductsSearch()
		{
			InitializeComponent();
		}

        private void MoneyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Allow only numbers and single decimal point
            if (e.Text == "." && textBox.Text.Contains("."))
            {
                // Already contains a decimal point, so handle this
                e.Handled = true;
            }
            else
            {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = ((App)Application.Current).MainViewModel;
            if (button != null && mainViewModel != null)
            {
                await mainViewModel.LoadProductsPage();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = ((App)Application.Current).MainViewModel;
            if (button != null && mainViewModel != null)
            {
                mainViewModel.ProductSearch = new ApiClient.ProductSearchModel();
                mainViewModel.SetDefaultCategory();
            }
        }
    }
}
