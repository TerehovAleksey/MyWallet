namespace MyWallet.Client.Services.Categories;

public interface ICategoryService
{
    public Task<List<Category>> GetAllCategoriesAsync();
    public Task<List<CategoryType>> GetAllCategoryTypes();
}
