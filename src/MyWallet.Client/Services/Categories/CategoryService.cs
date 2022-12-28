using MyWallet.Client.Services.Rest;

namespace MyWallet.Client.Services.Categories;

public class CategoryService : RestServiceBase, ICategoryService
{
    private const string KEY = "categories";

    public CategoryService(HttpClient httpClient, IConnectivity connectivity, IStorageService storageService) : base(httpClient, connectivity, storageService)
    {
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await StorageService.LoadFromCache<List<Category>>(KEY);
        if (categories is not null)
        {
            return categories;
        }

        categories = await GetAsync<List<Category>>($"category");

        await StorageService.SaveToCache(KEY, categories, TimeSpan.FromDays(1));
        return categories;
    }
}
