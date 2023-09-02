using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Image;
using RepairDesk.Api.Models.Product;
using RepairDesk.Api.Repositories.interfaces;

namespace RepairDesk.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; 
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await _context.Products.AnyAsync(p => p.Id == productId);
        }

        public async Task<PagedResult<ProductListModel>> GetProductsAsync(int pageNr = 1)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalProducts = await _context.Products.CountAsync();
            int totalPages = (totalProducts + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductListModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Quantity = p.Quantity,
                    Rating = p.Rating,
                    ImagePath = p.ImagePath
                })
                .ToListAsync();

            return new PagedResult<ProductListModel>
            {
                Items = products,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }

		public async Task<PagedResult<ProductListModel>> GetProductsByCategoryIdAsync(int categoryId, int pageNr = 1)
		{
			int pageSize = int.Parse(_configuration["Pages:Size"]);

            var query = _context.Products
                .Where(c => c.CategoryId == categoryId)
                .OrderBy(p => p.Id)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize);


			int totalProducts = await query.CountAsync();
			int totalPages = (totalProducts + pageSize - 1) / pageSize;

			if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var products = await query
				.Select(p => new ProductListModel
				{
					Id = p.Id,
					Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
					Price = p.Price,
					CategoryId = p.CategoryId,
					CategoryName = p.Category.Name,
					Quantity = p.Quantity,
                    Rating = p.Rating,
                    ImagePath = p.ImagePath
				})
				.ToListAsync();

			return new PagedResult<ProductListModel>
			{
				Items = products,
				CurrentPage = pageNr,
				TotalPages = totalPages
			};
		}

		public async Task<ProductDetailsModel> GetProductAsync(int productId)
        {
            var product = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductDetailsModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Quantity = p.Quantity,
                    Rating = p.Rating,
                    LongDescription = p.LongDescription,
                    ImagePath = p.ImagePath
                }).FirstOrDefaultAsync();

            return product;
        }

        public async Task<ProductDetailsModel> GetHotDealAsync()
        {
            int? productId = await _context.Products
                .OrderBy(r => Guid.NewGuid())
                .Select( p => p.Id)
                .FirstOrDefaultAsync();

            if (productId.HasValue)
            {
                return await GetProductAsync(productId.Value);
            }

            return null;
        }

        public async Task<ProductDetailsModel> AddProductAsync(ProductAddModel model)
        {
            var entity = new Product
            {
                Name = model.Name,
                Brand = model.Brand,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Quantity = model.Quantity,
                Rating = model.Rating,
                LongDescription = model.LongDescription,
                ImagePath = model.ImagePath
            };

            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return await GetProductAsync(entity.Id);
        }

        public async Task<ProductDetailsModel> EditProductAsync(int productId, ProductEditModel model)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ArgumentException($"Product with id {productId} not found.");
            }
            if (productId != model.Id)
            {
                throw new ArgumentException($"Id missmatch.");
            }

            // update data
            product.Name = model.Name;
            product.Brand = model.Brand;
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.Quantity = model.Quantity;
            product.Rating = model.Rating;
            product.LongDescription = model.LongDescription;
            product.ImagePath = model.ImagePath;

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await GetProductAsync(productId);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ArgumentException($"Product with id {productId} not found.");
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync() > 0;
        }

        //public async Task<PagedResult<string>> GetImagePathsAsync(int productId, int pageNr = 1)
        //{
        //    var product = await _context.Products//.Include(p => p.Images)
        //        .FirstOrDefaultAsync(p => p.Id == productId);
        //    if (product == null)
        //    {
        //        throw new ArgumentException($"Product with id {productId} not found.");
        //    }

        //    int pageSize = int.Parse(_configuration["Pages:Size"]);
        //    int totalItems = product.Images.Count();
        //    int totalPages = (totalItems + pageSize - 1) / pageSize;

        //    if (pageNr < 1) pageNr = 1;
        //    if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

        //    var imagePaths = product.Images
        //        .Skip((pageNr - 1) * pageSize)
        //        .Take(pageSize)
        //        .Select(img => img.Path)
        //        .ToList();

        //    return new PagedResult<string>
        //    {
        //        Items = imagePaths,
        //        CurrentPage = pageNr,
        //        TotalPages = totalPages
        //    };
        //}


        //public async Task<bool> AddImageAsync(int productId, ImageAddModel model)
        //{
        //    var product = await _context.Products.FindAsync(productId);
        //    if (product == null)
        //    {
        //        throw new ArgumentException($"Product with id {productId} not found.");
        //    }

        //    var entity = new Image 
        //    { 
        //        Path = model.Path,
        //        Name = model.Name,
        //        Description = model.Description
        //    };
        //    product.Images.Add(entity);

        //    return await _context.SaveChangesAsync() > 0;
        //}

        //public async Task<bool> RemoveImagePathAsync(int productId, ImageRemoveModel model)
        //{
        //    var product = await _context.Products.FindAsync(productId);
        //    if (product == null)
        //    {
        //        throw new ArgumentException($"Product with id {productId} not found.");
        //    }

        //    product.Images = product.Images.Where(img => img.Path != model.Path).ToList();

        //    return await _context.SaveChangesAsync() > 0;
        //}

        public async Task<PagedResult<ProductListModel>> GetProductsWithLowStockNotification(int pageNr)
        {
            int lowStockNotification = 5;
            int.TryParse(_configuration["Products:LowStockNotification"], out lowStockNotification);

            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalProducts = await _context.Products
                .Where(p => p.Quantity <= lowStockNotification)
                .CountAsync();
            int totalPages = (totalProducts + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Where(p => p.Quantity <= lowStockNotification)
                .Select(p => new ProductListModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Quantity = p.Quantity,
                    Rating = p.Rating
                })
                .ToListAsync();

            return new PagedResult<ProductListModel>
            {
                Items = products,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }

        public async Task<PagedResult<ProductListModel>> SearchProductAsync(ProductSearchModel model, int pageNr)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);

            // query
            var query = _context.Products.AsQueryable();

            if (model.Name != null)
            {
                query = query.Where(p => p.Name.Contains(model.Name));
            }
            if (model.Description != null)
            {
                query = query.Where(p => p.Description.Contains(model.Description));
            }
            if (model.Text != null)
            {
                query = query.Where(
                    p => p.Name.Contains(model.Text) 
                    || p.Description.Contains(model.Text) 
                    || p.LongDescription.Contains(model.Text)
               );
            }
            if (model.Brand != null)
            {
                query = query.Where(p => p.Brand.Contains(model.Brand));
            }
            if (model.CategoryId != null)
            {
                query = query.Where(p => p.CategoryId == model.CategoryId);
            }
            if (model.PriceFrom != null)
            {
                query = query.Where(p => p.Price >= model.PriceFrom);
            }
            if (model.PriceTo != null)
            {
                query = query.Where(p => p.Price <= model.PriceTo);
            }
            query = query.OrderBy(p => p.Id);

            // total pages
            int totalProducts = await query.CountAsync();
            int totalPages = (totalProducts + pageSize - 1) / pageSize;
            // current page
            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var products = await query
                .OrderBy(p => p.Id)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductListModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Quantity = p.Quantity,
                    Rating = p.Rating,
                    ImagePath = p.ImagePath
                })
                .ToListAsync();

            return new PagedResult<ProductListModel>
            {
                Items = products,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }

    }

}
