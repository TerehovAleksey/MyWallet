using MyWallet.Client.Services.Rest;

namespace MyWallet.Client.Services.Categories;

public class CategoryService : RestServiceBase, ICategoryService
{
    public const string KEY_CATEGORIES = "categories";
    public const string KEY_TYPES = "category_types";

    public CategoryService(HttpClient httpClient, IConnectivity connectivity, IStorageService storageService) : base(httpClient, connectivity,
        storageService)
    {
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await StorageService.LoadFromCache<List<Category>>(KEY_CATEGORIES);
        if (categories is not null)
        {
            return categories;
        }

        categories = await GetAsync<List<Category>>($"category");

        await StorageService.SaveToCache(KEY_CATEGORIES, categories, TimeSpan.FromDays(30));
        return categories;
    }

    public async Task<List<CategoryType>> GetAllCategoryTypes()
    {
        var types = await StorageService.LoadFromCache<List<CategoryType>>(KEY_TYPES);
        if (types is not null)
        {
            return types;
        }

        types = await GetAsync<List<CategoryType>>($"category/types");

        await StorageService.SaveToCache(KEY_TYPES, types, TimeSpan.FromDays(30));
        return types;
    }
}