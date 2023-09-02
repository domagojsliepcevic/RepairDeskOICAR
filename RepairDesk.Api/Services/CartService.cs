using RepairDesk.Api.Data;
using RepairDesk.Api.Extensions;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Cart;
using RepairDesk.Api.Models.CartItem;
using RepairDesk.Api.Repositories;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;
using static RepairDesk.Api.Services.CartService;

namespace RepairDesk.Api.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;


        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<CartDetailsModel> GetCartAsync(int userId)
        {
            if (!await _userRepository.UserExistsAsync(userId))
            {
                throw new ArgumentException($"User with id {userId} not found");
            }

            var item = await _cartRepository.GetCartAsync(userId);
            if (item.IsNull())
            {
                return new CartDetailsModel();
            }

            return item;
        }

        public async Task<CartHeadModel> GetCartHeadAsync(int userId)
        {
            if (!await _userRepository.UserExistsAsync(userId))
            {
                throw new ArgumentException($"User with id {userId} not found");
            }

            var item = await _cartRepository.GetCartHeadAsync(userId);
            if (item.IsNull())
            {
                return new CartHeadModel();
            }

            return item;
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            if (!await _userRepository.UserExistsAsync(userId))
            {
                throw new ArgumentException($"User with id {userId} not found");
            }

            return await _cartRepository.ClearCartAsync(userId);
        }

        public async Task<PagedResult<CartItemListModel>> GetCartItemsForCartAsync(int userId, int pageNr = 1)
        {
            if (!await _userRepository.UserExistsAsync(userId))
            {
                throw new ArgumentException($"User with id {userId} not found");
            }

            return await _cartRepository.GetCartItemsByUserIdAsync(userId, pageNr);
        }

        public async Task<CartItemDetailsModel> GetCartItemAsync(int cartItemId)
        {
            return await _cartRepository.GetCartItemAsync(cartItemId);
        }

        public async Task<CartItemDetailsModel> AddItemToCartAsync(int userId, CartItemAddModel model)
        {
            if (!await _userRepository.UserExistsAsync(userId))
            {
                throw new ArgumentException($"User with id {userId} not found");
            }

            var item = await _cartRepository.AddCartItemAsync(userId, model);
            if (item != null)
            {
                await _cartRepository.RecalculateCartAsync(userId);
            }

            return item;
        }

        public async Task<CartItemDetailsModel> EditCartItemAsync(int userId, CartItemEditModel model)
        {
            if (!await _userRepository.UserExistsAsync(userId))
            {
                throw new ArgumentException($"User with id {userId} not found");
            }

            var item = await _cartRepository.EditCartItemAsync(userId, model);
            if (item != null)
            {
                await _cartRepository.RecalculateCartAsync(userId);
            }

            return item;
        }

        public async Task<bool> DeleteCartItemAsync(int userId, int productId)
        {
            bool result =  await _cartRepository.DeleteCartItemAsync(userId, productId);
            if (result)
            {
                await _cartRepository.RecalculateCartAsync(userId);
            }

            return result;
        }


    }

}
