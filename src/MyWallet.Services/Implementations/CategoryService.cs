namespace MyWallet.Services.Implementations;

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

    public async Task InitCategoriesForUser(Guid userId)
    {
        var types = await _context.CategoryTypes.Select(x => new { x.Id, x.Name }).ToListAsync();
        var mustId = types.First(x => x.Name == "Обязательные").Id;
        var needId = types.First(x => x.Name == "Необходимые").Id;
        var wantId = types.First(x => x.Name == "Желаемые").Id;

        _context.Categories.AddRange(new List<Category>
        {
            new()
            {
                Id = Guid.NewGuid(), Name = "Еда и напитки", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "", Color = "#ff3d00",
                CategoryTypeId = needId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Бар, кафе", Color = "#ff3d00", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Продукты", Color = "#ff3d00", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new()
                    {
                        Id = Guid.NewGuid(), Name = "Ресторан, фаст-фуд", Color = "#ff3d00", ImageName = "", IsVisible = true, CategoryTypeId = wantId
                    }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Покупки", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "", Color = "#4fc3f7",
                CategoryTypeId = wantId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Аптека", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Дети", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Дом и сад", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Домашние животные, питомцы", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Канцелярские принадлежности, инструменты", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Красота и здоровье", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Одежда и обувь", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Отдых", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Подарки", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Электроника, аксессуары", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Ювелирные изделия, аксессуары", Color = "#4fc3f7", ImageName = "", IsVisible = true, CategoryTypeId = wantId }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Жильё", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "", Color = "#ffa726",
                CategoryTypeId = mustId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Аренда", Color = "#ffa726", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Ипотека", Color = "#ffa726", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Страхоание имущества", Color = "#ffa726", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Техобслуживание, ремонт", Color = "#ffa726", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Услуги", Color = "#ffa726", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Электричество, коммунальные услуги", Color = "#ffa726", ImageName = "", IsVisible = true, CategoryTypeId = mustId }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Транспорт", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "", Color = "#78909c",
                CategoryTypeId = needId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Дальние поездки", Color = "#78909c", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Деловые поездки", Color = "#78909c", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Общественный транспорт", Color = "#78909c", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Такси", Color = "#78909c", ImageName = "", IsVisible = true, CategoryTypeId = wantId }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Транспортное средство", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "", Color = "#aa00ff", 
                CategoryTypeId = needId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Аренда", Color = "#aa00ff", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Бензин", Color = "#aa00ff", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Лизинг", Color = "#aa00ff", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Парковка", Color = "#aa00ff", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Страхование транспорта", Color = "#aa00ff", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Техобслуживание, ремонт", Color = "#aa00ff", ImageName = "", IsVisible = true, CategoryTypeId = needId }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Жизнь и развлечения", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "",
                Color = "#64dd17", 
                CategoryTypeId = wantId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Алкоголь, табак", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Благотворительность, подарки", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Здравоохранение, врач", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Значимые события", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Книги, аудио, подписки", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Культура, спортивные мероприятия", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Лотереи, азартные игры", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Образование, развитие", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Оздоровление, красота", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Отпуск, поездки, отели", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Спорт, фитнес", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "ТВ и потоковое вещание", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Хобби", Color = "#64dd17", ImageName = "", IsVisible = true, CategoryTypeId = wantId }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Связь, ПК", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "", Color = "#536dfe",
                CategoryTypeId = needId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Интернет", Color = "#536dfe", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Почтовые услуги", Color = "#536dfe", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Программы, игры", Color = "#536dfe", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Телефон", Color = "#536dfe", ImageName = "", IsVisible = true, CategoryTypeId = needId }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Финансовые расходы", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "",
                Color = "#00bfa5", 
                CategoryTypeId = mustId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Алименты", Color = "#00bfa5", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Займы, проценты", Color = "#00bfa5", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Консультации", Color = "#00bfa5", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Налоги", Color = "#00bfa5", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Сборы, платы", Color = "#00bfa5", ImageName = "", IsVisible = true, CategoryTypeId = mustId },
                    new() { Id = Guid.NewGuid(), Name = "Страхование", Color = "#00bfa5", ImageName = "", IsVisible = true, CategoryTypeId = needId },
                    new() { Id = Guid.NewGuid(), Name = "Штрафы", Color = "#00bfa5", ImageName = "", IsVisible = true, CategoryTypeId = mustId }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Инвестиции", UserId = userId, IsVisible = true, IsIncome = false, ImageName = "", Color = "#ff4081",
                CategoryTypeId = wantId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Коллекции", Color = "#ff4081", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Недвижимость", Color = "#ff4081", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Сбережения", Color = "#ff4081", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Транспортные средства, движимое имущество", Color = "#ff4081", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Финансовые инвестиции", Color = "#ff4081", ImageName = "", IsVisible = true, CategoryTypeId = wantId }
                }
            },
            new()
            {
                Id = Guid.NewGuid(), Name = "Доход", UserId = userId, IsVisible = true, IsIncome = true, ImageName = "", Color = "#fbc02d",
                CategoryTypeId = wantId,
                SubCategories = new List<SubCategory>
                {
                    new() { Id = Guid.NewGuid(), Name = "Алименты", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Возврат денег (налог, покупка)", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Доход от аренды", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Зарплата, счёт-фактуры", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Интересы, дивиденды", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Кредит, аренда", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Лотереи, азартные игры", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Подарки", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Продажа", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Чеки, купоны", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                    new() { Id = Guid.NewGuid(), Name = "Членские взносы и гранты", Color = "#fbc02d", ImageName = "", IsVisible = true, CategoryTypeId = wantId },
                }
            }
        });
        await _context.SaveChangesAsync();
    }
}