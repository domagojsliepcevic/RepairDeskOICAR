using RepairDesk.Api.Models.Image;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Category;

namespace RepairDesk.Api.Repositories.interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> CategoryExistsAsync(int categoryId);
        Task<PagedResult<CategoryListModel>> GetCategoriesAsync(int pageNr);
        Task<CategoryDetailsModel> GetCategoryAsync(int categoryId);
		Task<CategoryDetailsModel> GetCategoryByNameAsync(string categoryName);
		Task<CategoryDetailsModel> AddCategoryAsync(CategoryAddModel model);
        Task<CategoryDetailsModel> EditCategoryAsync(int categoryId, CategoryEditModel model);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
