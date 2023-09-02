using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Image;
using RepairDesk.Api.Models.Product;
using RepairDesk.Api.Services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RepairDesk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;


        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        // GET: api/Products     
        [HttpGet("page/{pageNr}", Name= "GetProductsPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ProductListModel>))]
        public async Task<IActionResult> GetProducts([FromRoute]int pageNr = 1)
        {
            try
            {
                var products = await _productService.GetProductsAsync(pageNr);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("category/{categoryId}/page/{pageNr}", Name = "GetProductsByCategoryPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ProductListModel>))]
        public async Task<IActionResult> GetProductsByCategoryId([FromRoute] int categoryId, [FromRoute] int pageNr = 1)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryIdAsync(categoryId, pageNr);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpGet("{productId}", Name="GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsModel))]
        public async Task<IActionResult> GetProduct(int productId)
        {
            try
            {
                var product = await _productService.GetProductAsync(productId);
                return Ok(product);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting product.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting product.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // POST: api/Products
        [Authorize(Roles = "admin")]
        [HttpPost(Name="AddProduct")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDetailsModel))]
        public async Task<IActionResult> AddProduct([FromBody][Required] ProductAddModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = await _productService.AddProductAsync(model);
                if (product == null)
                {
                    return BadRequest("Product creation failed.");
                }

                return CreatedAtAction(nameof(GetProduct), new { productId = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new product.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // PUT: api/Products/5
        [Authorize(Roles = "admin")]
        [HttpPut("{productId}",Name="EditProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsModel))]
        public async Task<IActionResult> EditProduct(int productId, [FromBody][Required] ProductEditModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = await _productService.EditProductAsync(productId, model);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while editing product with id {productId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // DELETE: api/Products/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{productId}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                await _productService.DeleteProductAsync(productId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting product with id {productId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        //// GET: api/Products/5/images
        //[HttpGet("{productId}/images/page/{pageNr}", Name = "GetProductImagePathsPage")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<string>))]
        //public async Task<IActionResult> GetImagePaths([FromRoute] int productId, [FromRoute] int pageNr=1)
        //{
        //    try
        //    {
        //        var imagePaths = await _productService.GetImagePathsAsync(productId, pageNr);
        //        return Ok(imagePaths);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error occurred while getting image paths for product with id {productId}.");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //}


        //// POST: api/Products/5/images
        //[Authorize(Roles = "admin")]
        //[HttpPost("{productId}/images")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> AddImage(int productId, [FromBody][Required] ImageAddModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var success = await _productService.AddImageAsync(productId, model);
        //        if (!success)
        //        {
        //            _logger.LogWarning($"Failed to add image for product with id {productId}.");
        //            return BadRequest("Failed to add image.");
        //        }

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error occurred while adding image for product with id {productId}.");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //}


        //[Authorize(Roles = "admin")]
        //[HttpDelete("{productId}/images")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> RemoveImagePath(int productId, [FromQuery][Required] ImageRemoveModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        await _productService.RemoveImagePathAsync(productId, model);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error occurred while removing image path {model.Path} for product with id {productId}.");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //}

        [Authorize(Roles = "admin")]
        [HttpGet("page/{pageNr}/lowStockNotification", Name = "GetLowStockNotificationProductsPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ProductListModel>))]
        public async Task<IActionResult> GetProductsWithLowStockNotification([FromRoute] int pageNr = 1)
        {
            try
            {
                var products = await _productService.GetProductsWithLowStockNotification(pageNr);
                return Ok(products);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while getting products.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // POST: api/Products
        [HttpPost("search/page/{pageNr}", Name = "SearchProducts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ProductListModel>))]
        public async Task<IActionResult> SearchProducts([FromBody][Required] ProductSearchModel model, [FromRoute] int pageNr = 1)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var products = await _productService.SearchProductAsync(model, pageNr);
                return Ok(products);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while getting products.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("hotDeal", Name = "HotDeal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsModel))]
        public async Task<IActionResult> HotDeal()
        {
            try
            {
                var product = await _productService.GetHotDealAsync();
                return Ok(product);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting hot deal product.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting hot deal product.");
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}
