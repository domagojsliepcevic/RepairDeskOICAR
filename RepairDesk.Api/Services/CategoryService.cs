using RepairDesk.Api.Models.Image;
using RepairDesk.Api.Models;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Models.Category;
using RepairDesk.Api.Services.interfaces;

namespace RepairDesk.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResult<CategoryListModel>> GetCategoriesAsync(int pageNr = 1)
        {
            return await _categoryRepository.GetCategoriesAsync(pageNr);
        }

        public async Task<CategoryDetailsModel> GetCategoryAsync(int id)
        {
            return await _categoryRepository.GetCategoryAsync(id);
        }

		public async Task<CategoryDetailsModel> GetCategoryByNameAsync(string categoryName)
		{
			return await _categoryRepository.GetCategoryByNameAsync(categoryName);
		}

		public async Task<CategoryDetailsModel> AddCategoryAsync(CategoryAddModel model)
        {
            return await _categoryRepository.AddCategoryAsync(model);
        }

        public async Task<CategoryDetailsModel> EditCategoryAsync(int id, CategoryEditModel model)
        {
            return await _categoryRepository.EditCategoryAsync(id, model);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }

	}
}
