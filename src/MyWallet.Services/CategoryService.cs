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

    public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto)
    {
        var exist = await _context.Categories.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(dto.Name.ToLower()));

        if (exist is not null)
        {
            return new CategoryDto(exist.Id, exist.Name, exist.IsVisible, exist.ImageName);
        }

        var category = new Category
        {
            Id = Guid.NewGuid(), 
            Name = dto.Name, 
            IsVisible = true,
            ImageName = dto.ImageName
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return new CategoryDto(category.Id, category.Name, category.IsVisible, category.ImageName);
    }

    public async Task<CategoryDto?> DeleteCategoryAsync(Guid id)
    {
        var result = await _context.Categories.FindAsync(id);
        if (result is null)
        {
            return null;
        }
        _context.Categories.Remove(result);
        await _context.SaveChangesAsync();
        return new CategoryDto(result.Id, result.Name, result.IsVisible, result.ImageName);
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var result = await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(x => new CategoryDto(x.Id, x.Name, x.IsVisible, x.ImageName ))
            .ToListAsync();

        return result;
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(CategoryDto category)
    {
        var result = await _context.Categories.FindAsync(category.Id);
        if (result is null)
        {
            return null;
        }
        result.Name = category.Name;
        result.IsVisible = category.IsVisible;
        result.ImageName = category.ImageName;
        await _context.SaveChangesAsync();
        return category;
    }
}
