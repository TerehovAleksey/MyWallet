namespace MyWallet.Services.Interfaces;

public interface ICategoryService
{
    public Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync(Guid userId);
    public Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id);
    public Task<IEnumerable<CategoryDto>> GetSubcategoryByCategoryId(Guid id);
    public Task<CategoryDto?> GetSubcategoryById(Guid id);
    public Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto category);
    public Task<CategoryDto> CreateSubcategoryAsync(Guid categoryId, BaseCategoryDto category);
    public Task<bool> DeleteCategoryAsync(Guid id);
    public Task<bool> DeleteSubcategoryAsync(Guid id);
    public Task<bool> UpdateCategoryAsync(CategoryUpdateDto category); 
    public Task<bool> UpdateSubcategoryAsync(CategoryDto category);
    public Task<IEnumerable<CategoryTypeDto>> GetCategoryTypesAsync();

    public Task InitCategoriesForUser(Guid userId);
}
