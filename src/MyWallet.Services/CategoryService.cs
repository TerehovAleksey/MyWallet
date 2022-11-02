using MyWallet.Services.Dto;
using MyWallet.Core.Dal;
using Microsoft.EntityFrameworkCore;
using MyWallet.Core;

namespace MyWallet.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public CategoryDto CreateCategory(string name)
    {
        var exist = _context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));

        if (exist is not null)
        {
            return new CategoryDto(exist.Id, exist.Name);
        }

        var category = new Category { Id = Guid.NewGuid(), Name = name };

        _context.Categories.Add(category);
        _context.SaveChanges();

        return new CategoryDto(category.Id, category.Name);
    }

    public IEnumerable<CategoryDto> GetCategories()
    {
        var result = _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(x => new CategoryDto(x.Id, x.Name));

        return result;
    }
}
