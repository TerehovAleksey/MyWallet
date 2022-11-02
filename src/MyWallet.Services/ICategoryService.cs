using MyWallet.Services.Dto;

namespace MyWallet.Services;

public interface ICategoryService
{
    public CategoryDto CreateCategory(string name);
    public IEnumerable<CategoryDto> GetCategories();
}
