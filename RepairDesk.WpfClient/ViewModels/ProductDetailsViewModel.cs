using Microsoft.Extensions.DependencyInjection;
using RepairDesk.ApiClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RepairDesk.WpfClient.ViewModels
{
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        private readonly RepairDeskApiClient _apiClient;
        //private readonly MainViewModel _mainViewModel;

        private ProductDetailsModel _product;
        public ProductDetailsModel Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged(); }
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

        public ProductDetailsViewModel()
        {
            _apiClient = App.DIHost.Services.GetRequiredService<RepairDeskApiClient>();
            //_mainViewModel = App.DIHost.Services.GetRequiredService<MainViewModel>();
        }

        public async Task LoadProductDetails(int productId)
        {
            try
            {
                IsLoading = true;
                Product = await _apiClient.GetProductAsync(productId);
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
