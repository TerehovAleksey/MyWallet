using Microsoft.AspNetCore.Mvc;
using MyWallet.Services;
using MyWallet.Services.Dto;

namespace MyWallet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync() => _categoryService.GetCategoriesAsync();

        [HttpPost]
        public Task<CategoryDto> CreateCategory([FromBody] CategoryCreateDto category) => _categoryService.CreateCategoryAsync(category);

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryDto category, [FromRoute] Guid id)
        {
            if (category.Id != id)
            {
                return BadRequest();
            }

            var result = await _categoryService.UpdateCategoryAsync(category);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}