using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Category;
using RepairDesk.Api.Repositories;
using RepairDesk.Api.Repositories.interfaces;

namespace RepairDesk.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CategoryRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<CategoryDetailsModel> AddCategoryAsync(CategoryAddModel model)
        {
            var entity = new ProductCategory
            {
                Name = model.Name,
                Description = model.Description,
                IsSpecial = model.IsSpecial,
                ImagePath = model.ImagePath,
            };

            _context.ProductCategories.Add(entity);
            await _context.SaveChangesAsync();

            return await GetCategoryAsync(entity.Id);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.ProductCategories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<bool> CategoryExistsByNameAsync(string categoryName)
        {
            return await _context.ProductCategories
                .AnyAsync(c => c.Name.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.ProductCategories.FindAsync(categoryId);
            if (category == null)
            {
                throw new ArgumentException($"Product category with id {categoryId} not found.");
            }

            _context.ProductCategories.Remove(category);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CategoryDetailsModel> EditCategoryAsync(int categoryId, CategoryEditModel model)
        {
            var category = await _context.ProductCategories.FindAsync(categoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category with id {categoryId} not found.");
            }
            if (categoryId != model.Id)
            {
                throw new ArgumentException($"Id missmatch.");
            }

            // update data
            category.Name = model.Name;
            category.Description = model.Description;
            category.IsSpecial = model.IsSpecial;
            category.ImagePath = model.ImagePath;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await GetCategoryAsync(categoryId);
        }

        public async Task<CategoryDetailsModel> GetCategoryAsync(int categoryId)
        {
            var category = await _context.ProductCategories
                .Where(pc => pc.Id == categoryId)
                .Select(pc => new CategoryDetailsModel
                {
                    Id = pc.Id,
                    Name = pc.Name,
                    Description = pc.Description,
                    IsSpecial = pc.IsSpecial,
                    ImagePath = pc.ImagePath
                }).FirstOrDefaultAsync();

            if (category == null)
            {
                throw new ArgumentException($"Category with id {categoryId} not found.");
            }

            return category;
        }

        public async Task<CategoryDetailsModel> GetCategoryByNameAsync(string categoryName)
        {
            //var category = await _context.ProductCategories
            //    .Where(pc => string.Equals(pc.Name, categoryName, StringComparison.CurrentCultureIgnoreCase))
            //    .Select(pc => new CategoryDetailsModel
            //    {
            //        Id = pc.Id,
            //        Name = pc.Name,
            //        Description = pc.Description,
            //        IsSpecial = pc.IsSpecial,
            //        ImagePath = pc.ImagePath
            //    }).FirstOrDefaultAsync();

            var categories = await _context.ProductCategories.ToListAsync();
            var matchedCategory = categories.FirstOrDefault(pc => pc.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

            var category = matchedCategory != null ? new CategoryDetailsModel
            {
                Id = matchedCategory.Id,
                Name = matchedCategory.Name,
                Description = matchedCategory.Description,
                IsSpecial = matchedCategory.IsSpecial,
                ImagePath = matchedCategory.ImagePath
            } : null;

            return category;
        }

        public async Task<PagedResult<CategoryListModel>> GetCategoriesAsync(int pageNr)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalItems = await _context.ProductCategories.CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var categories = await _context.ProductCategories
                .OrderBy(pc => pc.Id)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new CategoryListModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    IsSpecial = p.IsSpecial,
                    ImagePath = p.ImagePath
                })
                .ToListAsync();

            return new PagedResult<CategoryListModel>
            {
                Items = categories,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }


    }
}






