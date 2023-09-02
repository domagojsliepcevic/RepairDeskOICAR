using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using RepairDesk.ApiClient;
using RepairDesk.WpfClient.Components;
using RepairDesk.WpfClient.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RepairDesk.WpfClient.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private bool _isLoading;
		public bool IsLoading
		{
			get => _isLoading;
			set
			{
				_isLoading = value;
				OnPropertyChanged();
			}
		}

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private TokenModel _token;
        public TokenModel Token
        {
            get { return _token; }
            set
            {
                _token = value;
                OnPropertyChanged();
            }
        }


        private UserControl _currentView;
		public UserControl CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<ProductListModel> _products;
		public ObservableCollection<ProductListModel> Products
		{
			get { return _products; }
			set
			{
				_products = value;
				OnPropertyChanged();
			}
		}

        private ProductDetailsModel _product;
        public ProductDetailsModel Product
        {
            get { return _product; }
            set 
			{
				_product = value; 
				OnPropertyChanged(); 
			}
        }

        private ObservableCollection<CartItemListModel> _cartItems;
        public ObservableCollection<CartItemListModel> CartItems
        {
            get { return _cartItems; }
            set
            {
                _cartItems = value;
                OnPropertyChanged();
            }
        }

        private RepairAddModel _ticket = new RepairAddModel { RequestDate = DateTime.Now };
        public RepairAddModel Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;
                OnPropertyChanged();
            }
        }

        /* users list */
        private ObservableCollection<UserListModel> _users;
        public ObservableCollection<UserListModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        private UserListModel _selectedUser;
        public UserListModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }
        private UserListModel _dummyUser = new UserListModel { Id = 0, Email = "- Select User -" };


        private ProductSearchModel _productSearch = new ProductSearchModel();
        public ProductSearchModel ProductSearch
        {
            get { return _productSearch; }
            set
            {
                _productSearch = value;
                OnPropertyChanged();
            }
        }

        /* categories list */
        private ObservableCollection<CategoryListModel> _categories;
        public ObservableCollection<CategoryListModel> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }
        private CategoryListModel _selectedCategory;
        public CategoryListModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                ProductSearch.CategoryId = _selectedCategory?.Id;
                if (ProductSearch.CategoryId == 0)
                {
                    ProductSearch.CategoryId = null;
                }
                OnPropertyChanged();
            }
        }
        private CategoryListModel _dummyCategory = new CategoryListModel { Id = 0, Name = "- Select Category -" };

        /* paging */
        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set
            {
                _totalPages = value;
                OnPropertyChanged();
            }
        }


        // views
        private Login loginView;
        private Logout logoutView;
        private ProductsGrid productsGridView;
		private ProductDetails productDetailsView;
		private OrderDetails orderDetailsView;
		private TicketAddNew ticketAddNewView;

		private readonly RepairDeskApiClient _apiClient;

        // commands
        public ICommand ShowLoginCommand { get; }
        public ICommand ShowProductsCommand { get; }
		public ICommand ShowProductCommand { get; }
		public ICommand ShowOrderCommand { get; }
		public ICommand ShowTicketCommand { get; }
		public ICommand LoadProductsPageCommand { get; }
        public ICommand LoadProductDetailsCommand { get; }



        public MainViewModel()
		{
            loginView = new Login() { DataContext = this };
            logoutView = new Logout() { DataContext = this };
            productsGridView = new ProductsGrid() { DataContext = this };
			productDetailsView = new ProductDetails() { DataContext = this };
			orderDetailsView = new OrderDetails() { DataContext = this };
			ticketAddNewView = new TicketAddNew() { DataContext = this };

            Products = new ObservableCollection<ProductListModel>();
			CartItems = new ObservableCollection<CartItemListModel>();
            Users = new ObservableCollection<UserListModel>();
            Categories = new ObservableCollection<CategoryListModel>();

            ShowLoginCommand = new RelayCommand(o =>
            {
                if (Token != null && !string.IsNullOrWhiteSpace(Token.Token))
                {
                    CurrentView = logoutView;
                    return;
                }
                CurrentView = loginView;
            });
            ShowProductsCommand = new AsyncCommand(async() => 
			{
                if (Token == null || string.IsNullOrWhiteSpace(Token.Token))
                {                    
                    CurrentView = loginView;
                    return;
                }
                await LoadCategoriesList();
                await LoadProductsPage();
				CurrentView = productsGridView;
			});
			ShowProductCommand = new AsyncCommand<int>(async (productId) =>
			{
                if (Token == null || string.IsNullOrWhiteSpace(Token.Token))
                {
                    CurrentView = loginView;
                    return;
                }
                await LoadProductDetails(productId);
                CurrentView = productDetailsView;
                productDetailsView.DataContext = Product;
            });
			ShowOrderCommand = new RelayCommand(async o => 
			{
                if (Token == null || string.IsNullOrWhiteSpace(Token.Token))
                {
                    CurrentView = loginView;
                    return;
                }
                await LoadUsersList();
                CurrentView = orderDetailsView;
            });
			ShowTicketCommand = new RelayCommand(async o => 
            {
                if (Token == null || string.IsNullOrWhiteSpace(Token.Token))
                {
                    CurrentView = loginView;
                    return;
                }
                await LoadUsersList();
                CurrentView = ticketAddNewView; 
            });

            _apiClient = App.DIHost.Services.GetRequiredService<RepairDeskApiClient>();

			// start view
			CurrentView = loginView;
        }


		public async Task LoadProductsPage(int pageNr = 1)
		{
			try
			{
				IsLoading = true;

                Products.Clear();
                var pagedResult = IsProductSearchEmpty() 
                    ? await _apiClient.GetProductsPageAsync(pageNr) 
                    : await _apiClient.SearchProductsAsync(pageNr, ProductSearch);
                if (pagedResult != null)
                {
                    TotalPages = pagedResult.TotalPages;
                    CurrentPage = TotalPages == 0 ? 0 : pagedResult.CurrentPage;
                    if (!pagedResult.Items.IsNullOrEmpty())
                    {
                        foreach (var product in pagedResult.Items)
                        {
                            Products.Add(product);
                        }
                    }
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

        public async Task PreviousProductsPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadProductsPage(CurrentPage);
            }
        }

        public async Task NextProductsPage()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                await LoadProductsPage(CurrentPage);
            }
        }


        public void SetDefaultCategory()
        {
            SelectedCategory = _dummyCategory;
        }

        private bool IsProductSearchEmpty()
        {
            if (string.IsNullOrWhiteSpace(ProductSearch.Name) 
                && string.IsNullOrWhiteSpace(ProductSearch.Description) 
                && string.IsNullOrWhiteSpace(ProductSearch.Brand) 
                && string.IsNullOrWhiteSpace(ProductSearch.Text) 
                && !ProductSearch.CategoryId.HasValue
                && !ProductSearch.PriceFrom.HasValue
                && !ProductSearch.PriceTo.HasValue)
            {
                return true;
            }

            return false;
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

		private async Task ManageCart(int productId, int quantity, bool keepExisting = true)
		{
            try
            {
                IsLoading = true;

                var item = CartItems.Where(ci => ci.ProductId == productId).FirstOrDefault();
                if (item == null)
				{
                    var product = await _apiClient.GetProductAsync(productId);
					item = new CartItemListModel
					{
						ProductId = productId,
						ProductName = product.Name,
						ProductImagePath = product.ImagePath,
						UnitPrice = product.Price,
                        Quantity = quantity,
						Price = product.Price * quantity
                    };

					if (quantity > 0)
					{
						CartItems.Add(item);
					}
                }
				else
				{
					if (keepExisting)
					{
						item.Quantity += quantity;
					}
					else
					{
						item.Quantity = quantity;
					}
                    item.Price = item.UnitPrice * item.Quantity;

					if (item.Quantity <= 0)
					{
						CartItems.Remove(item);
					}
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

        public async Task AddOneToCart(int productId)
        {
			await ManageCart(productId, 1);
        }

        public async Task RemoveOneFromCart(int productId)
        {
            await ManageCart(productId, -1);
        }

        public async Task RemoveAllFromCart(int productId)
        {
            await ManageCart(productId, 0, false);
        }

        public async Task AddNewTicket(int productId)
        {
            await ManageCart(productId, 0, false);
        }

		public async Task<bool> Login(string username, string password)
		{
            try
            {
                IsLoading = true;

                var ulm = new UserLoginModel
				{
					Email = username,
					Password = password
				};
                var token = await _apiClient.UserLoginAsync(ulm);
                if (token != null)
                {
                    var roleClaims = GetRoleClaimFromToken(token.Token);
                    if (roleClaims.Where(rc => rc.Value == "admin").Any())
                    {
                        Token = token;
                        CurrentView = null;
                        ShowProductsCommand.Execute(null);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private List<Claim> GetRoleClaimFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var roleClaims = jwtToken?.Claims.Where(claim => claim.Type == "role").ToList();

            return roleClaims;
        }

        public void Logout()
        {
            try
            {
                IsLoading = true;

                Token = null;
                Username = null;

                ShowLoginCommand.Execute(null);
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

        public async Task LoadUsersList()
        {
            try
            {
                IsLoading = true;

                if (Users.Count == 0)
                {
                    var pagedResult = await _apiClient.GetUsersPageAsync(1);
                    Users.Clear();
                    if (pagedResult != null && !pagedResult.Items.IsNullOrEmpty())
                    {
                        Users.Add(_dummyUser);
                        SelectedUser = _dummyUser;
                        foreach (var user in pagedResult.Items)
                        {
                            Users.Add(user);
                        }
                    }
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

        public async Task LoadCategoriesList()
        {
            try
            {
                IsLoading = true;

                if (Categories.Count == 0)
                {
                    var pagedResult = await _apiClient.GetCategoriesPageAsync(1);
                    Categories.Clear();
                    if (pagedResult != null && !pagedResult.Items.IsNullOrEmpty())
                    {
                        Categories.Add(_dummyCategory);
                        SelectedCategory = _dummyCategory;
                        foreach (var category in pagedResult.Items)
                        {
                            Categories.Add(category);
                        }
                    }
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

        public async Task<bool> Buy()
        {
            try
            {
                IsLoading = true;

                if (!CartItems.IsNullOrEmpty())
                {
                    if (SelectedUser?.Id != null && SelectedUser?.Id > 0)
                    {
                        var orderItems = CartItems.Select(ci => new OrderItemAddModel
                        {
                            ProductId = ci.ProductId,
                            Quantity = ci.Quantity
                        }).ToList();

                        var order = new OrderAdminAddModel
                        {
                            OrderDate = DateTime.Now,
                            PaymentMethod = "CASH",
                            UserId = SelectedUser.Id,
                            PosId = 2, // todo
                            OrderItems = orderItems
                        };

                        await _apiClient.AdminAddOrderAsync(order);
                    }
                    CartItems.Clear();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                IsLoading = false;
            }

        }

        public async Task<bool> SaveTicketForRepair()
        {
            try
            {
                IsLoading = true;

                if (Ticket != null && SelectedUser != null && SelectedUser.Id > 0)
                {
                    Ticket.UserId = SelectedUser.Id;
                    Ticket.PosId = 2; // todo
                    await _apiClient.AddRepairAsync(Ticket);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                IsLoading = false;
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
