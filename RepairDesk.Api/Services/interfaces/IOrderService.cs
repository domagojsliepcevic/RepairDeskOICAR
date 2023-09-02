using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Order;
using RepairDesk.Api.Models.OrderItem;

namespace RepairDesk.Api.Services.interfaces
{
    public interface IOrderService
    {
        Task<OrderDetailsModel> AddOrderAsync(int userId, OrderAddModel model);
        Task<PagedResult<OrderListModel>> GetOrdersByUserAsync(int userId, int pageNr);
        Task<OrderDetailsModel> GetOrderByUserAsync(int userId, int orderId);
        Task<PagedResult<OrderListModel>> GetOrdersAsync(int pageNr);
        Task<OrderDetailsModel> GetOrderAsync(int orderId);

        Task<bool> DeleteOrderAsync(int userId, int orderId);
        Task<PagedResult<OrderListModel>> GetOrdersByPOSAsync(int posId, int orderId);
        Task<OrderDetailsModel> GetOrderByPOSAsync(int posId, int orderId);
        Task<OrderDetailsModel> AdminAddOrderAsync(OrderAdminAddModel model);
		Task<OrderDetailsModel> UserAddOrderAsync(int userId, OrderUserAddModel model);


		/* items */
		Task<PagedResult<OrderItemListModel>> GetAllOrderItemsForOrderAsync(int orderId, int pageNr);
        Task<OrderItemDetailsModel> GetOrderItemAsync(int orderItemId);
        Task<OrderItemDetailsModel> AddItemToOrderAsync(int orderId, OrderItemAddModel model);
        Task<OrderItemDetailsModel> EditOrderItemAsync(int orderItemId, OrderItemEditModel model);
        Task<bool> DeleteOrderItemAsync(int orderItemId);

    }
}
