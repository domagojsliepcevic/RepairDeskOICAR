using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Image;
using RepairDesk.Api.Models.Product;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;

namespace RepairDesk.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductListModel>> GetProductsAsync(int pageNr = 1)
        {
            return await _productRepository.GetProductsAsync(pageNr);
        }

		public async Task<PagedResult<ProductListModel>> GetProductsByCategoryIdAsync(int categoryId, int pageNr)
		{
			return await _productRepository.GetProductsByCategoryIdAsync(categoryId, pageNr);
		}

		public async Task<ProductDetailsModel> GetProductAsync(int id)
        {
            return await _productRepository.GetProductAsync(id);
        }

        public async Task<ProductDetailsModel> GetHotDealAsync()
        {
            return await _productRepository.GetHotDealAsync();
        }

        public async Task<ProductDetailsModel> AddProductAsync(ProductAddModel model)
        {
            return await _productRepository.AddProductAsync(model);
        }

        public async Task<ProductDetailsModel> EditProductAsync(int id, ProductEditModel model)
        {
            // todo -> ProductCategory

            return await _productRepository.EditProductAsync(id, model);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }

        //public async Task<PagedResult<string>> GetImagePathsAsync(int productId, int pageNr = 1)
        //{
        //    return await _productRepository.GetImagePathsAsync(productId, pageNr);
        //}

        //public async Task<bool> AddImageAsync(int productId, ImageAddModel model)
        //{
        //    return await _productRepository.AddImageAsync(productId, model);
        //}

        //public async Task<bool> RemoveImagePathAsync(int productId, ImageRemoveModel model)
        //{
        //    return await _productRepository.RemoveImagePathAsync(productId, model);
        //}

        public async Task<PagedResult<ProductListModel>> GetProductsWithLowStockNotification(int pageNr)
        {
            return await _productRepository.GetProductsWithLowStockNotification(pageNr);
        }

        public async Task<PagedResult<ProductListModel>> SearchProductAsync(ProductSearchModel model, int pageNr)
        {
            return await _productRepository.SearchProductAsync(model, pageNr);
        }

    }

}
