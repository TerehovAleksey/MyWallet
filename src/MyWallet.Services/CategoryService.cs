namespace MyWallet.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        var result = _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name);

        return await _mapper.ProjectTo<CategoryResponseDto>(result).ToListAsync();
    }

    public async Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id)
    {
        var result = _context.Categories
            .AsNoTracking()
            .Where(x => x.Id == id);

        return await _mapper.ProjectTo<CategoryResponseDto>(result)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CategoryDto>> GetSubcategoryByCategoryId(Guid id)
    {
        var result = _context.Subcategories
            .AsNoTracking()
            .Where(x => x.CategoryId == id)
            .OrderBy(x => x.Name);

        return await _mapper.ProjectTo<CategoryDto>(result).ToListAsync();
    }

    public async Task<CategoryDto?> GetSubcategoryById(Guid id)
    {
        var result = _context.Subcategories
            .AsNoTracking()
            .Where(x => x.Id == id);

        return await _mapper.ProjectTo<CategoryDto>(result).FirstOrDefaultAsync();
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);

        if (category is not null)
        {
            category.IsVisible = true;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        var result = _mapper.Map<CategoryResponseDto>(category);
        return result;
    }

    public async Task<CategoryDto> CreateSubcategoryAsync(Guid categoryId, BaseCategoryDto category)
    {
        var subCategory = _mapper.Map<SubCategory>(category);

        if (subCategory is not null)
        {
            subCategory.IsVisible = true;
            subCategory.CategoryId = categoryId;
            await _context.Subcategories.AddAsync(subCategory);
            await _context.SaveChangesAsync();
        }

        var result = _mapper.Map<CategoryDto>(category);
        return result;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        bool result;
        try
        {
            var category = _context.Categories.Attach(new Category { Id = id });
            category.State = EntityState.Deleted;
            result = await _context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            result = false;
        }

        return result;
    }

    public async Task<bool> DeleteSubcategoryAsync(Guid id)
    {
        bool result;
        try
        {
            var category = _context.Subcategories.Attach(new SubCategory { Id = id });
            category.State = EntityState.Deleted;
            result = await _context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            result = false;
        }

        return result;
    }

    public async Task<bool> UpdateCategoryAsync(CategoryUpdateDto category)
    {
        var result = false;
        var exists = await _context.Categories.FindAsync(category.Id);
        if (exists is not null)
        {
            exists.Name = category.Name;
            exists.IsVisible = category.IsVisible;
            exists.ImageName = category.ImageName;
            exists.IsIncome = category.IsIncome;
            await _context.SaveChangesAsync();
            result = true;
        }

        return result;
    }

    public async Task<bool> UpdateSubcategoryAsync(CategoryDto category)
    {
        var result = false;
        var exists = await _context.Subcategories.FindAsync(category.Id);
        if (exists is null)
        {
            return result;
        }
        exists.Name = category.Name;
        exists.IsVisible = category.IsVisible;
        exists.ImageName = category.ImageName;
        await _context.SaveChangesAsync();
        result = true;

        return result;
    }
}
