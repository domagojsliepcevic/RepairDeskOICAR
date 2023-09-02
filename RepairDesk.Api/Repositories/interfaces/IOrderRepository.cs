using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Order;
using RepairDesk.Api.Models.OrderItem;
using RepairDesk.Api.Models.Product;

namespace RepairDesk.Api.Repositories.interfaces
{
    public interface IOrderRepository
    {
        Task<bool> OrderExistsAsync(int orderId);
        Task<OrderDetailsModel> AddOrderAsync(int userId, OrderAddModel model);
        Task<PagedResult<OrderListModel>> GetOrdersAsync(int pageNr = 1, int? userId = null);
        Task<OrderDetailsModel> GetOrderAsync(int orderId, int? userId = null);
        Task<bool> DeleteOrderAsync(int userId, int orderId);
        Task<PagedResult<OrderListModel>> GetOrdersByPOSAsync(int posId, int orderId);
        Task<OrderDetailsModel> GetOrderByPOSAsync(int posId, int orderId);
        Task<OrderDetailsModel> AdminAddOrderAsync(OrderAdminAddModel model);
		Task<OrderDetailsModel> UserAddOrderAsync(int userId, OrderUserAddModel model);

		/* items */
		Task<PagedResult<OrderItemListModel>> GetAllOrderItemsForOrderAsync(int orderId, int pageNr);
        Task<OrderItemDetailsModel> GetOrderItemAsync(int orderItemId);
        Task<OrderItemDetailsModel> AddItemToOrderAsync(int orderId, OrderItemAddModel model, ProductDetailsModel product);
        Task<OrderItemDetailsModel> EditOrderItemAsync(int orderItemId, OrderItemEditModel model, ProductDetailsModel product);
        Task<bool> DeleteOrderItemAsync(int orderItemId);
    }
}
