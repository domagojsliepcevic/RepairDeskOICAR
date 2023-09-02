using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Order;
using RepairDesk.Api.Models.OrderItem;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;
using System;

namespace RepairDesk.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }


        public async Task<OrderDetailsModel> AddOrderAsync(int userId, OrderAddModel model)
        {
            return await _orderRepository.AddOrderAsync(userId, model);
        }

        public async Task<OrderDetailsModel> GetOrderByUserAsync(int userId, int orderId)
        {
            return await _orderRepository.GetOrderAsync(orderId, userId);
        }

        public async Task<PagedResult<OrderListModel>> GetOrdersByUserAsync(int userId, int pageNr)
        {
            return await _orderRepository.GetOrdersAsync(pageNr, userId);
        }

        public async Task<OrderDetailsModel> GetOrderAsync(int orderId)
        {
            return await _orderRepository.GetOrderAsync(orderId: orderId);
        }

        public async Task<PagedResult<OrderListModel>> GetOrdersAsync(int pageNr)
        {
            return await _orderRepository.GetOrdersAsync(pageNr: pageNr);
        }

        public async Task<bool> DeleteOrderAsync(int userId, int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(userId, orderId);
        }

        public async Task<PagedResult<OrderListModel>> GetOrdersByPOSAsync(int posId, int pageNr)
        {
            return await _orderRepository.GetOrdersByPOSAsync(posId, pageNr);
        }

        public async Task<OrderDetailsModel> GetOrderByPOSAsync(int posId, int orderId)
        {
            return await _orderRepository.GetOrderByPOSAsync(posId, orderId);
        }

        public async Task<OrderDetailsModel> AdminAddOrderAsync(OrderAdminAddModel model)
        {
            return await _orderRepository.AdminAddOrderAsync(model);
        }

		public async Task<OrderDetailsModel> UserAddOrderAsync(int userId, OrderUserAddModel model)
		{
			return await _orderRepository.UserAddOrderAsync(userId, model);
		}

		/* items */

		public async Task<PagedResult<OrderItemListModel>> GetAllOrderItemsForOrderAsync(int orderId, int pageNr = 1)
        {
            return await _orderRepository.GetAllOrderItemsForOrderAsync(orderId, pageNr);
        }

        public async Task<OrderItemDetailsModel> GetOrderItemAsync(int orderItemId)
        {
            return await _orderRepository.GetOrderItemAsync(orderItemId);
        }

        public async Task<OrderItemDetailsModel> AddItemToOrderAsync(int orderId, OrderItemAddModel model)
        {
            if (!await _productRepository.ProductExistsAsync(model.ProductId))
            {
                throw new ArgumentException($"Product with id {model.ProductId} not found");
            }
            var product = await _productRepository.GetProductAsync(model.ProductId);

            return await _orderRepository.AddItemToOrderAsync(orderId, model, product);
        }

        public async Task<OrderItemDetailsModel> EditOrderItemAsync(int orderItemId, OrderItemEditModel model)
        {
            if (!await _productRepository.ProductExistsAsync(model.ProductId))
            {
                throw new ArgumentException($"Product with id {model.ProductId} not found");
            }

            var product = await _productRepository.GetProductAsync(model.ProductId);

            return await _orderRepository.EditOrderItemAsync(orderItemId, model, product);
        }

        public async Task<bool> DeleteOrderItemAsync(int orderItemId)
        {
            return await _orderRepository.DeleteOrderItemAsync(orderItemId);
        }
    }
}
