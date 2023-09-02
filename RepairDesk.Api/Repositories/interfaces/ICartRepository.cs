using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Cart;
using RepairDesk.Api.Models.CartItem;
using RepairDesk.Api.Models.Product;

namespace RepairDesk.Api.Repositories.interfaces
{
    public interface ICartRepository
    {
        Task<bool> CartExistsAsync(int cartId);
        Task<CartDetailsModel> GetCartAsync(int userId);
        Task<CartHeadModel> GetCartHeadAsync(int userId);
        //Task<List<CartListModel>> GetCartsAsync();
        Task<bool> ClearCartAsync(int userId);

        /* items */
        Task<CartItemDetailsModel> GetCartItemAsync(int cartItemId);
        Task<PagedResult<CartItemListModel>> GetCartItemsByUserIdAsync(int userId, int pageNr);
        Task<CartItemDetailsModel> AddCartItemAsync(int userId, CartItemAddModel model);
        Task<CartItemDetailsModel> EditCartItemAsync(int userId, CartItemEditModel model);
        Task<bool> DeleteCartItemAsync(int userId, int productId);

        Task RecalculateCartAsync(int userId);

    }

}
