using RepairDesk.ApiClient;
using RepairDesk.WpfClient.Converters;
using RepairDesk.WpfClient.Helpers;
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
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : UserControl
	{
        public OrderDetails()
		{
			InitializeComponent();
            Loaded += OrderDetails_Loaded;
        }

        private void OrderDetails_Loaded(object sender, RoutedEventArgs e)
        {
            var mainViewModel = DataContext as MainViewModel;
            if (mainViewModel != null)
            {
                CartItemsView.ItemsSource = null;
                CartItemsView.ItemsSource = mainViewModel.CartItems;
                var newBinding = new Binding("CartItems") { Converter = new PriceSumConverter() };
                CartItemsTotal.SetBinding(TextBlock.TextProperty, newBinding);
            }
        }

        private async void Plus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = DataContext as MainViewModel;
            if (button != null && mainViewModel != null && button.CommandParameter != null && button.CommandParameter is int)
            {
                await mainViewModel.AddOneToCart((int)button.CommandParameter); // id
                CartItemsView.ItemsSource = null;
                CartItemsView.ItemsSource = mainViewModel.CartItems;
                var newBinding = new Binding("CartItems") { Converter = new PriceSumConverter() };
                CartItemsTotal.SetBinding(TextBlock.TextProperty, newBinding);
            }
        }

        private async void Minus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = DataContext as MainViewModel;
            if (button != null && mainViewModel != null && button.CommandParameter != null && button.CommandParameter is int)
            {
                await mainViewModel.RemoveOneFromCart((int)button.CommandParameter); // id
                CartItemsView.ItemsSource = null;
                CartItemsView.ItemsSource = mainViewModel.CartItems;
                var newBinding = new Binding("CartItems") { Converter = new PriceSumConverter() };
                CartItemsTotal.SetBinding(TextBlock.TextProperty, newBinding);
            }
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = DataContext as MainViewModel;
            if (button != null && mainViewModel != null && button.CommandParameter != null && button.CommandParameter is int)
            {
                await mainViewModel.RemoveAllFromCart((int)button.CommandParameter); // id
                mainViewModel.CartItems = new ObservableCollection<CartItemListModel>(mainViewModel.CartItems);
                CartItemsView.ItemsSource = null;
                CartItemsView.ItemsSource = mainViewModel.CartItems;
                var newBinding = new Binding("CartItems"){ Converter = new PriceSumConverter()};
                CartItemsTotal.SetBinding(TextBlock.TextProperty, newBinding);
            }
        }

        private async void Pay_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel mainViewModel = ((App)Application.Current).MainViewModel;
            if (button != null && mainViewModel != null)
            {
                // validation
                //if (mainViewModel.SelectedUser == null || mainViewModel.SelectedUser.Id == 0)
                //{
                //    MessageBox.Show("Please select a user.");
                //    return;
                //}

                if (mainViewModel.CartItems.IsNullOrEmpty()) 
                {
                    MessageBox.Show("Order is empty.");
                    return;
                }

                // save order
                if (await mainViewModel.Buy())
                {
                    var newBinding = new Binding("CartItems") { Converter = new PriceSumConverter() };
                    CartItemsTotal.SetBinding(TextBlock.TextProperty, newBinding);
                    MessageBox.Show("Order saved.");
                }
                else
                {
                    MessageBox.Show("Order NOT saved.");
                }
            }
        }
    }
}
