using MyWallet.Client.Models;

namespace MyWallet.Client.DataServices;

public interface IDataService
{
    public Task<List<Category>> GetAllCategoriesAsync();
    public Task<Category> CreateCategoryAsync(string name, string imageName);
}
