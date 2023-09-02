using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Image;
using RepairDesk.Api.Models.Product;

namespace RepairDesk.Api.Services.interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductListModel>> GetProductsAsync(int pageNr);
		Task<PagedResult<ProductListModel>> GetProductsByCategoryIdAsync(int categoryId, int pageNr);
		Task<ProductDetailsModel> GetProductAsync(int productId);
        Task<ProductDetailsModel> GetHotDealAsync();
        Task<ProductDetailsModel> AddProductAsync(ProductAddModel model);
        Task<ProductDetailsModel> EditProductAsync(int productId, ProductEditModel model);
        Task<bool> DeleteProductAsync(int productId);
        //Task<PagedResult<string>> GetImagePathsAsync(int productId, int pageNr);
        //Task<bool> AddImageAsync(int productId, ImageAddModel model);
        //Task<bool> RemoveImagePathAsync(int productId, ImageRemoveModel model);
        Task<PagedResult<ProductListModel>> GetProductsWithLowStockNotification(int pageNr);
        Task<PagedResult<ProductListModel>> SearchProductAsync(ProductSearchModel model, int pageNr);

    }

}
