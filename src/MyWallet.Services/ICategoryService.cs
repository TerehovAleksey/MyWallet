using MyWallet.Services.Dto;

namespace MyWallet.Services;

public interface ICategoryService
{
    public Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto category);
    public Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    public Task<CategoryDto?> UpdateCategoryAsync(CategoryDto category);
    public Task<CategoryDto?> DeleteCategoryAsync(Guid id);
}
