using Microsoft.AspNetCore.Mvc;
using MyWallet.Services;
using MyWallet.Services.Dto;

namespace MyWallet.WebApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получение всех категорий с подкатегориями
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync()
        {
           return _categoryService.GetAllCategoriesAsync();
        }

        /// <summary>
        /// Получение категории по ID c подкатегориями
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        /// <summary>
        /// Получение всех подкатегорий в категории
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("sub/{categoryId:guid}")]
        public Task<IEnumerable<CategoryDto>> GetSubcategoryByCategoryId([FromRoute] Guid categoryId)
        {
            return _categoryService.GetSubcategoryByCategoryId(categoryId);
        }

        /// <summary>
        /// Получение подкатегории по ID
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("sub")]
        public async Task<IActionResult> GetSubcategoryById([FromQuery] Guid id)
        {
            var result = await _categoryService.GetSubcategoryById(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Создание категории с подкатегориями
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction($"api/category/{result.Id}", result);
        }

        /// <summary>
        /// Создание подкатегории в категории
        /// </summary>
        /// <returns></returns>
        [HttpPost("sub/{categoryId:guid}")]
        public async Task<IActionResult> CreateSubcategoryAsync([FromRoute] Guid categoryId, [FromBody] BaseCategoryDto category)
        {
            var result = await _categoryService.CreateSubcategoryAsync(categoryId, category);
            return CreatedAtAction($"api/category/sub?id={result.Id}", result);
        }

        /// <summary>
        /// Изменение категории
        /// </summary>
        /// <param name="category"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Guid id, [FromBody] CategoryUpdateDto category)
        {
            if (category.Id != id)
            {
                return BadRequest();
            }

            var result = await _categoryService.UpdateCategoryAsync(category);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Изменение подкатегории
        /// </summary>
        /// <returns></returns>
        [HttpPut("sub/{id:guid}")]
        public async Task<IActionResult> UpdateSubcategoryAsync([FromRoute] Guid id, [FromBody] CategoryDto category)
        {
            if (category.Id != id)
            {
                return BadRequest();
            }

            var result = await _categoryService.UpdateSubcategoryAsync(category);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Удаление категории с подкатегориями по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (result)
            {
                return NoContent(); 
            }

            return NotFound();
        }

        /// <summary>
        /// Удаление подкатегории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("sub/{id:guid}")]
        public async Task<IActionResult> DeleteSubcategoryAsync([FromRoute] Guid id)
        {
            var result = await _categoryService.DeleteSubcategoryAsync(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}