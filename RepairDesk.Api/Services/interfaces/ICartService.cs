using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Cart;
using RepairDesk.Api.Models.CartItem;
using RepairDesk.Api.Models.Product;

namespace RepairDesk.Api.Services.interfaces
{

    public interface ICartService
    {
        Task<CartDetailsModel> GetCartAsync(int userId);
        Task<CartHeadModel> GetCartHeadAsync(int userId);
        Task<bool> ClearCartAsync(int userId);

        /* items */
        Task<PagedResult<CartItemListModel>> GetCartItemsForCartAsync(int userId, int pageNr);
        Task<CartItemDetailsModel> GetCartItemAsync(int cartItemId);
        Task<CartItemDetailsModel> AddItemToCartAsync(int userId, CartItemAddModel model);
        Task<CartItemDetailsModel> EditCartItemAsync(int cartItemId, CartItemEditModel model);
        Task<bool> DeleteCartItemAsync(int userId, int productId);
    }

}
