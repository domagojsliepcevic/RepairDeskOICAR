using Microsoft.Extensions.DependencyInjection;
using RepairDesk.ApiClient;
using RepairDesk.WpfClient.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairDesk.WpfClient.ViewModels
{
    public class _MainViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, Page> _pages = new Dictionary<string, Page>();
        //public ICommand ShowProductsListCommand { get; }
        //public ICommand ShowProductDetailsCommand { get; }

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }


        public _MainViewModel()
        {
            //ShowProductsListCommand = new RelayCommand1(ShowProductsList);
            //ShowProductDetailsCommand = new RelayCommand1(ShowProductDetails);
        }


        public T GetOrCreatePage<T>(Func<T> pageCreator) where T : Page
        {
            var key = typeof(T).FullName;

            if (!_pages.ContainsKey(key))
            {
                    _pages[key] = pageCreator();
            }

            return _pages[key] as T;
        }

        public void ShowProductsList()
        {
            CurrentPage = GetOrCreatePage(() => new ProductsView());
        }

        public async void ShowProductDetails()
        {
            var productsViewModel = _pages.Values
                .Select(page => page.DataContext as ProductsViewModel)
                .FirstOrDefault(viewModel => viewModel != null && viewModel.SelectedProduct != null);

            if (productsViewModel != null)
            {
                var productDetailsView = GetOrCreatePage(() => new ProductDetailsView());
                ((ProductDetailsViewModel)productDetailsView.DataContext).Product = null;
                CurrentPage = productDetailsView;
                await ((ProductDetailsViewModel)productDetailsView.DataContext).LoadProductDetails(productsViewModel.SelectedProduct.Id);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
