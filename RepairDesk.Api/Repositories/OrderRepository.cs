using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Cart;
using RepairDesk.Api.Models.Order;
using RepairDesk.Api.Models.OrderItem;
using RepairDesk.Api.Models.Product;
using RepairDesk.Api.Repositories.interfaces;
using System;
using System.Security.Cryptography;

namespace RepairDesk.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public OrderRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> OrderExistsAsync(int orderId)
        {
            return await _context.Orders.AnyAsync(o => o.Id == orderId);
        }

        public async Task<OrderDetailsModel> AddOrderAsync(int userId, OrderAddModel model)
        {
            var order = new Order
            {
                UserId = model.UserId,
                OrderDate = model.OrderDate,
                PaymentMethod = model.PaymentMethod,
                OrderNumber = Guid.NewGuid().ToString()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return await GetOrderAsync(order.Id, userId);
        }

        public async Task<OrderDetailsModel> GetOrderAsync(int orderId, int? userId = null)
        {
            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Id == orderId)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(o => o.UserId == userId);
            }

            var order = await query.FirstOrDefaultAsync();
            if (order == null)
            {
                throw new ArgumentException($"Order with id {orderId} not found.");
            }

            return new OrderDetailsModel
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderNumber= order.OrderNumber,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                User = $"{order.User.FirstName} {order.User.LastName}",
                StatusId = order.OrderStatusId,
                Status = order.OrderStatus.Name,
                PaymentMethod = order.PaymentMethod,
                OrderItems = order.OrderItems.Select(oi => new OrderItemListModel
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Product = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Price = oi.Price
                }).ToList()
            };
        }

        public async Task<PagedResult<OrderListModel>> GetOrdersAsync(int pageNr = 1, int? userId = null)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            var query = _context.Orders
                .Include(c => c.User)
                .Include(c => c.OrderStatus)
                .AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(o => o.UserId == userId);
            }

            int totalItems = await query.CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var orders = await query
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderListModel
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    User = $"{o.User.FirstName} {o.User.LastName}",
                    TotalAmount = o.TotalAmount,
                    StatusId = o.OrderStatusId,
                    Status = o.OrderStatus.Name,
                    OrderNumber = o.OrderNumber,
                    OrderDate = o.OrderDate,
                    PaymentMethod = o.PaymentMethod
                })
                .ToListAsync();

            return new PagedResult<OrderListModel>
            {
                Items = orders,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }


        public async Task<bool> DeleteOrderAsync(int userId, int orderId)
        {
            var order = await _context.Orders
                .Where(o => o.UserId == userId && o.Id == orderId)
                .FirstOrDefaultAsync();
            if (order != null)
            {
                _context.Orders.Remove(order);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<PagedResult<OrderListModel>> GetOrdersByPOSAsync(int posId, int pageNr = 1)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalItems = await _context.Orders
                .Where(o => o.POSId == posId)
                .CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var orders = await _context.Orders
                .Include(c => c.User)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Where(o => o.POSId == posId)
                .Select(o => new OrderListModel
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    User = $"{o.User.FirstName} {o.User.LastName}",
                    TotalAmount = o.TotalAmount,
                    StatusId = o.OrderStatusId,
                    Status = o.OrderStatus.Name,
                    OrderNumber = o.OrderNumber,
                    OrderDate = o.OrderDate,
                    PaymentMethod = o.PaymentMethod
                })
                .ToListAsync();

            return new PagedResult<OrderListModel>
            {
                Items = orders,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }

        public async Task<OrderDetailsModel> GetOrderByPOSAsync(int posId, int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Id == orderId && o.POSId == posId)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                throw new ArgumentException($"Order with id {orderId} for POS {posId} not found.");
            }

            return new OrderDetailsModel
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                User = $"{order.User.FirstName} {order.User.LastName}",
                StatusId = order.OrderStatusId,
                Status = order.OrderStatus.Name,
                PaymentMethod = order.PaymentMethod,
                OrderItems = order.OrderItems.Select(oi => new OrderItemListModel
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Product = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Price = oi.Price
                }).ToList()
            };
        }

        public async Task<OrderDetailsModel> AdminAddOrderAsync(OrderAdminAddModel model)
        {
            var order = new Order
            {
                UserId = model.UserId,
                OrderDate = model.OrderDate,
                PaymentMethod = model.PaymentMethod,
                OrderNumber = Guid.NewGuid().ToString(),
                OrderStatusId = (int)OrderStatuses.ORDER_RECEIVED,
                POSId = model.POSId
            };
            _context.Orders.Add(order);

            decimal total = 0m;
            foreach (var item in model.OrderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                var orderItem = new OrderItem
                {
                    Order = order,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Price = product.Price * item.Quantity
                };
                total += orderItem.Price;
                _context.OrderItems.Add(orderItem);
            }
            order.TotalAmount = total;

            await _context.SaveChangesAsync();

            return await GetOrderAsync(order.Id);
        }

		public async Task<OrderDetailsModel> UserAddOrderAsync(int userId, OrderUserAddModel model)
		{
			var order = new Order
			{
				UserId = userId,
				OrderDate = model.OrderDate,
				PaymentMethod = model.PaymentMethod,
				OrderNumber = Guid.NewGuid().ToString(),
				OrderStatusId = (int)OrderStatuses.ORDER_RECEIVED,
				POSId = model.POSId
			};
			_context.Orders.Add(order);

			decimal total = 0m;
			foreach (var item in model.OrderItems)
			{
				var product = await _context.Products.FindAsync(item.ProductId);
				var orderItem = new OrderItem
				{
					Order = order,
					ProductId = item.ProductId,
					Quantity = item.Quantity,
					UnitPrice = product.Price,
					Price = product.Price * item.Quantity
				};
				total += orderItem.Price;
				_context.OrderItems.Add(orderItem);
			}
			order.TotalAmount = total;

			await _context.SaveChangesAsync();

			return await GetOrderAsync(order.Id);
		}

		/* items */
		public async Task<PagedResult<OrderItemListModel>> GetAllOrderItemsForOrderAsync(int orderId, int pageNr = 1)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalItems = await _context.OrderItems.Where(oi => oi.OrderId == orderId).CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var orderItems = await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Select(oi => new OrderItemListModel
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Product = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                })
                .ToListAsync();

            return new PagedResult<OrderItemListModel>
            {
                Items = orderItems,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }


        public async Task<OrderItemDetailsModel> GetOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                throw new ArgumentException($"Order item with id {orderItemId} not found.");
            }

            return new OrderItemDetailsModel
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                ProductName = orderItem.Product.Name,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice
            };
        }

        public async Task<OrderItemDetailsModel> AddItemToOrderAsync(int orderId, OrderItemAddModel model, ProductDetailsModel product)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException($"Order with id {orderId} not found.");
            }

            var entity = new OrderItem
            {
                OrderId = orderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                UnitPrice = product.Price
            };

            _context.OrderItems.Add(entity);
            await _context.SaveChangesAsync();

            return await GetOrderItemAsync(entity.Id);
        }

        public async Task<OrderItemDetailsModel> EditOrderItemAsync(int orderItemId, OrderItemEditModel model, ProductDetailsModel product)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                throw new ArgumentException($"Order item with id {orderItemId} not found.");
            }
            if (orderItemId != model.Id)
            {
                throw new ArgumentException($"Id missmatch.");
            }

            orderItem.Quantity = model.Quantity;
            orderItem.UnitPrice = product.Price;
            orderItem.Price = orderItem.Quantity * orderItem.UnitPrice;

            _context.Entry(orderItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await GetOrderItemAsync(orderItemId);
        }

        public async Task<bool> DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                throw new ArgumentException($"Order item with id {orderItemId} not found.");
            }

            _context.OrderItems.Remove(orderItem);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
