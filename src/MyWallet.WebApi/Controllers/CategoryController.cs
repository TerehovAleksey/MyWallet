namespace MyWallet.WebApi.Controllers
{
    [ApiController]   
    [Authorize]
    [Route("api/category")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, UserManager<User> userManager) : base(userManager)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получение всех категорий с подкатегориями
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var user = await GetUserAsync();
            if (user is not null)
            {
                var result = await _categoryService.GetAllCategoriesAsync(user.Id);
                return Ok(result);
            }
            return Unauthorized();
        }

        /// <summary>
        /// Получение категории по ID c подкатегориями
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IEnumerable<CategoryDto>> GetSubcategoryByCategoryId([FromRoute] Guid categoryId)
        {
            return _categoryService.GetSubcategoryByCategoryId(categoryId);
        }

        /// <summary>
        /// Получение подкатегории по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("sub")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);
            return Created($"api/category/{result.Id}", result);
        }

        /// <summary>
        /// Создание подкатегории в категории
        /// </summary>
        /// <returns></returns>
        [HttpPost("sub/{categoryId:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSubcategoryAsync([FromRoute] Guid categoryId, [FromBody] BaseCategoryDto category)
        {
            var result = await _categoryService.CreateSubcategoryAsync(categoryId, category);
            return Created($"api/category/sub?id={result.Id}", result);
        }

        /// <summary>
        /// Изменение категории
        /// </summary>
        /// <param name="category"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSubcategoryAsync([FromRoute] Guid id)
        {
            var result = await _categoryService.DeleteSubcategoryAsync(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        /// <summary>
        /// Получение типов категорий
        /// </summary>
        /// <returns></returns>
        [HttpGet("types")]
        [ProducesResponseType(typeof(IEnumerable<CategoryTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCategoryTypesAsync()
        {
            var user = await GetUserAsync();
            if (user is not null)
            {
                var result = await _categoryService.GetCategoryTypesAsync();
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}