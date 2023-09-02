using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Category;
using RepairDesk.Api.Services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RepairDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;


        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        // GET: api/Categories

        [HttpGet("page/{pageNr}", Name = "GetCategoriesPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<CategoryListModel>))]
        public async Task<IActionResult> GetCategories([FromRoute] int pageNr = 1)
        {
            try
            {
                var categories = await _categoryService.GetCategoriesAsync(pageNr);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting categories.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpGet("{categoryId}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsModel))]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            try
            {
                var category = await _categoryService.GetCategoryAsync(categoryId);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting category with id {categoryId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

		[HttpGet("", Name = "GetCategoryByName")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsModel))]
		public async Task<IActionResult> GetCategoryByName([FromQuery][Required] string name)
		{
			try
			{
				var category = await _categoryService.GetCategoryByNameAsync(name);
				if (category == null)
				{
					return NotFound();
				}
				return Ok(category);
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, "Error occurred while getting category.");
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting category with name '{name}'.");
				return StatusCode(500, "Internal server error.");
			}
		}


		// POST: api/Categories
		[Authorize(Roles = "admin")]
        [HttpPost(Name = "AddCategory")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDetailsModel))]
        public async Task<IActionResult> AddCategory([FromBody][Required] CategoryAddModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var category = await _categoryService.AddCategoryAsync(model);
                if (category == null)
                {
                    return BadRequest("Category creation failed.");
                }

                return CreatedAtAction(nameof(GetCategory), new { categoryId = category.Id }, category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new category.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // PUT: api/Categories/5
        [Authorize(Roles = "admin")]
        [HttpPut("{categoryId}", Name = "EditCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsModel))]
        public async Task<IActionResult> EditCategory(int categoryId, [FromBody][Required] CategoryEditModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var category = await _categoryService.EditCategoryAsync(categoryId, model);
                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while editing category with id {categoryId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // DELETE: api/Categories/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{categoryId}", Name= "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(categoryId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting category with id {categoryId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}
