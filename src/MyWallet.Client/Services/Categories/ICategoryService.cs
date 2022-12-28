namespace MyWallet.Client.Services.Categories;

public interface ICategoryService
{
    public Task<List<Category>> GetAllCategoriesAsync();
}
