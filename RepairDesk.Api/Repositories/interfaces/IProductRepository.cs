using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Image;
using RepairDesk.Api.Models.Product;

namespace RepairDesk.Api.Repositories.interfaces
{
    public interface IProductRepository
    {
        Task<bool> ProductExistsAsync(int productId);
        Task<PagedResult<ProductListModel>> GetProductsAsync(int pageNr);	
		Task<PagedResult<ProductListModel>> GetProductsByCategoryIdAsync(int categoryId, int pageNr);
		Task<ProductDetailsModel> GetProductAsync(int productId);
        Task<ProductDetailsModel> GetHotDealAsync();
        Task<ProductDetailsModel> AddProductAsync(ProductAddModel model);
        Task<ProductDetailsModel> EditProductAsync(int productId, ProductEditModel model);
        Task<bool> DeleteProductAsync(int productId);
        Task<PagedResult<ProductListModel>> GetProductsWithLowStockNotification(int pageNr);
        Task<PagedResult<ProductListModel>> SearchProductAsync(ProductSearchModel model, int pageNr);


    }
}

