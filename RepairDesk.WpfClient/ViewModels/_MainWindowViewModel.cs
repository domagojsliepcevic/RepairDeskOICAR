using RepairDesk.ApiClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RepairDesk.WpfClient.ViewModels
{
    public class _MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly RepairDeskApiClient _apiClient;

        // PRODUCTS
        private ObservableCollection<ProductListModel> products;
        public ObservableCollection<ProductListModel> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        // IS LOADING
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


        // commands
        public ICommand LoadDataCommand { get; }


        public _MainWindowViewModel(RepairDeskApiClient apiClient)
        {
            _apiClient = apiClient;
            LoadDataCommand = new RelayCommand1(async () => await LoadDataAsync());
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadDataAsync()
        {
            try
            {
                IsLoading = true;
                //var data = await _apiClient.ProductsAllAsync(); todo
                //Products = new ObservableCollection<ProductListModel>(data);
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
    }
}
