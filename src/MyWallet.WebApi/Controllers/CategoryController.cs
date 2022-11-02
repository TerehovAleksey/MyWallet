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
        public IEnumerable<CategoryDto> GetCategories() => _categoryService.GetCategories();

        [HttpPost]
        public CategoryDto CreateCategory([FromBody]string name) => _categoryService.CreateCategory(name);
    }
}
