using Microsoft.Extensions.DependencyInjection;
using RepairDesk.ApiClient;
using RepairDesk.WpfClient.Helpers;
using RepairDesk.WpfClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace RepairDesk.WpfClient.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        private readonly RepairDeskApiClient _apiClient;
        private readonly MainViewModel _mainViewModel;

        private ObservableCollection<ProductListModel> _products;
        public ObservableCollection<ProductListModel> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }

        private ProductListModel _selectedProduct;
        public ProductListModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                    ShowProductDetails();
                }
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand LoadProductsCommand { get; }
        public ICommand ShowProductDetailsCommand { get; }


        public ProductsViewModel()
        {
            _apiClient = App.DIHost.Services.GetRequiredService<RepairDeskApiClient>();
            _mainViewModel = ((App)Application.Current).MainViewModel;

            LoadProductsCommand = new RelayCommand1(async () => await LoadProducts());
            ShowProductDetailsCommand = new RelayCommand1(ShowProductDetails);
            Products = new ObservableCollection<ProductListModel>();
        }

        private async Task LoadProducts()
        {
            try
            {
                IsLoading = true;

                var pagedResult = await _apiClient.GetProductsPageAsync(1);
                Products.Clear();
                if (pagedResult != null && !pagedResult.Items.IsNullOrEmpty()) { }
                foreach (var proizvod in pagedResult.Items)
                {
                    Products.Add(proizvod);
                }
            }
            catch (Exception ex)
            {
                // todo
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ShowProductDetails()
        {
            if (SelectedProduct != null)
            {
                ///////////////////////////_mainViewModel.ShowProductDetails();
            }
        }


        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
