using RepairDesk.Api.Models.Image;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Category;

namespace RepairDesk.Api.Services.interfaces
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryListModel>> GetCategoriesAsync(int pageNr);
        Task<CategoryDetailsModel> GetCategoryAsync(int categoryId);
        Task<CategoryDetailsModel> GetCategoryByNameAsync(string categoryName);
		Task<CategoryDetailsModel> AddCategoryAsync(CategoryAddModel model);
        Task<CategoryDetailsModel> EditCategoryAsync(int categoryId, CategoryEditModel model);
        Task<bool> DeleteCategoryAsync(int categoryId);

    }
}
