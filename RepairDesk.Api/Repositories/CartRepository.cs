using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models.Cart;
using RepairDesk.Api.Models.CartItem;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;
using RepairDesk.Api.Extensions;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Product;
using System.Diagnostics.Eventing.Reader;

namespace RepairDesk.Api.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CartRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> CartExistsAsync(int userId)
        {
            return await _context.Carts.AnyAsync(c => c.UserId == userId);
        }

        public async Task<CartDetailsModel> GetCartAsync(int userId)
        {
            var entity = await _context.Carts
                .Include(c => c.CartItems).ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (entity.IsNull())
            {
                return null;
            }

            return new CartDetailsModel
            {
                Id = entity.Id,
                Total = entity.Total,
                UserId = entity.UserId,
                Quantity = entity.Quantity,
                CartItems = entity.CartItems.Select(ci => new CartItemListModel
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    UnitPrice = ci.UnitPrice,
                    Quantity = ci.Quantity,
                    Price = ci.Price,
                }).ToList()
            };
        }



        public async Task<CartHeadModel> GetCartHeadAsync(int userId)
        {
            var entity = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
            if (entity.IsNull())
            {
                return null;
            }

            return new CartHeadModel
            {
                Id = entity.Id,
                Total = entity.Total,
                UserId = entity.UserId,
                Quantity = entity.Quantity
            };
        }

        //public async Task<List<CartListModel>> GetCartsAsync()
        //{
        //    var entities = await _context.Carts.Include(c => c.CartItems).ToListAsync();

        //    return entities.Select(c => new CartListModel
        //    {
        //        Id = c.Id,
        //        UserId = c.UserId,
        //        TotalAmount = c.TotalAmount
        //    }).ToList();
        //}


        public async Task<bool> ClearCartAsync(int userId)
        {
            var entity = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
            if (entity == null)
            {
                throw new ArgumentException($"Cart for user with id {userId} cannot be found.");
            }

            entity.Total = 0m;
            _context.CartItems.RemoveRange(entity.CartItems);

            //await RecalculateCartAsync(userId);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CartItemDetailsModel> GetCartItemAsync(int cartItemId)
        {
            var entity = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.Id == cartItemId)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new ArgumentException($"Cart item with id {cartItemId} cannot be found.");
            }

            return new CartItemDetailsModel
            {
                Id = entity.Id,
                CartId = entity.CartId,
                ProductId = entity.ProductId,
                ProductName = entity.Product.Name,
                Price = entity.Price,
                UnitPrice = entity.UnitPrice,
                Quantity = entity.Quantity
            };
        }

        public async Task<PagedResult<CartItemListModel>> GetCartItemsByUserIdAsync(int userId, int pageNr = 1)
        {
            var query = _context.CartItems
                .Join(_context.Carts, cartItem => cartItem.CartId, cart => cart.Id, (cartItem, cart) => new { cartItem, cart })
                .Where(joined => joined.cart.UserId == userId)
                .Select(joined => new CartItemListModel
                {
                    Id = joined.cartItem.Id,
                    CartId = joined.cartItem.CartId,
                    ProductId = joined.cartItem.ProductId,
                    Price = joined.cartItem.Price,
                    ProductName = joined.cartItem.Product.Name,
                    ProductImagePath = joined.cartItem.Product.ImagePath,
                    Quantity = joined.cartItem.Quantity,
                    UnitPrice = joined.cartItem.UnitPrice
                });

            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalItems = await query.CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var cartItems = await query.ToListAsync();

            return new PagedResult<CartItemListModel>
            {
                Items = cartItems,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }


        public async Task<CartItemDetailsModel> AddCartItemAsync(int userId, CartItemAddModel model)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, AddedAt = DateTime.Now };
				_context.Carts.Add(cart);
				await _context.SaveChangesAsync();
			}

			var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.ProductId);
            if (product == null)
            {
                throw new ArgumentException($"Product with id {model.ProductId} cannot be found.");
            }

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == model.ProductId && ci.CartId == cart.Id);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    UnitPrice = product.Price,
                    Price = product.Price * model.Quantity
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity+= model.Quantity;
            }


            await _context.SaveChangesAsync();

            var modul = await GetCartItemAsync(cartItem.Id);

            return modul;
        }

        public async Task<CartItemDetailsModel> EditCartItemAsync(int userId, CartItemEditModel model)
        {
            var cart = await _context.Carts.Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();
            if (cart == null)
            {
                throw new ArgumentException($"Cart cannot be found.");
            }
            var item = await _context.CartItems.Where(ci => ci.ProductId == model.ProductId && ci.CartId == cart.Id)
                .FirstOrDefaultAsync();
            if (item == null)
            {
                throw new ArgumentException($"Cart item cannot be found.");
            }

            // update data
            item.Quantity = model.Quantity;

            //_context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return await GetCartItemAsync(item.Id);
        }

        public async Task<bool> DeleteCartItemAsync(int userId, int productId)
        {
            var item = await _context.CartItems
                .Include(ci => ci.Cart)
                .Where(ci => ci.ProductId == productId && ci.Cart.UserId == userId)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                throw new ArgumentException($"Cart item for user {userId} and product {productId} cannot be found.");
            }

            _context.CartItems.Remove(item);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task RecalculateCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart.IsNotNull())
            {
                cart.Total = 0m;
                cart.Quantity = 0;

                if (!cart.CartItems.IsNullOrEmpty())
                {
                    foreach (var item in cart.CartItems)
                    {
                        item.Price = item.Quantity * item.UnitPrice;
                        cart.Total += item.Price;
                        cart.Quantity += item.Quantity;
                        if (item.Quantity <= 0)
                        {
                            _context.Entry(item).State = EntityState.Deleted;
                        }
                    }
                }
                _context.Entry(cart).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        //private async Task<Cart> GetCartAsyncById(int cartId)
        //{
        //    var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.Id == cartId);

        //    return cart;
        //}

        //private async Task<CartDetailsModel> GetCartAsyncById(int cartId)
        //{
        //    var entity = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.Id == cartId);
        //    if (entity == null)
        //    {
        //        throw new ArgumentException($"Cart with id {cartId} cannot be found.");
        //    }

        //    return GetCartDetailsModel(entity);
        //}

        //private CartDetailsModel GetCartDetailsModel(Cart cart)
        //{
        //    return new CartDetailsModel
        //    {
        //        Id = cart.Id,
        //        Total = cart.TotalAmount,
        //        UserId = cart.UserId,
        //        Quantity = cart.Quantity,
        //        CartItems = cart.CartItems.Select(ci => new CartItemListModel
        //        {
        //            Id = ci.Id,
        //            ProductId = ci.ProductId,
        //            Quantity = ci.Quantity
        //        }).ToList()
        //    };
        //}

        //private async Task RecalculateCartAsync(int userId)
        //{
        //    var cart = await GetCartAsync(userId);
        //    await RecalculateCartDetailsModelAsync(cart);

        //}

        //private async Task RecalculateCartAsyncById(int cartId)
        //{
        //    var cart = await GetCartAsyncById(cartId);
        //    await RecalculateCartDetailsModelAsync(cart);
        //}

        //private async Task RecalculateCartDetailsModelAsync(CartDetailsModel cart)
        //{
        //    if (cart.IsNotNull())
        //    {
        //        cart.Total = 0m;
        //        cart.Quantity = 0m;

        //        if (!cart.CartItems.IsNullOrEmpty())
        //        {
        //            foreach (var item in cart.CartItems)
        //            {
        //                cart.Total += item.Price;
        //                cart.Quantity += item.Quantity;
        //            }
        //        }

        //        _context.Entry(cart).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //    }
        //}


    }
}
