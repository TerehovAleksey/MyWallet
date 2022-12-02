namespace MyWallet.Common.Dto;

public class BaseCategoryDto
{
    public string Name { get; set; } = default!;
    public string? ImageName { get; set; }
    public bool IsVisible { get; set; }
}

public class CategoryDto : BaseCategoryDto
{
    public Guid Id { get; set; } 
}

public class CategoryCreateDto : BaseCategoryDto
{
    public bool IsIncome { get; set; }
    public IEnumerable<BaseCategoryDto>? Subcategories { get; set; }
}

public class CategoryUpdateDto : CategoryDto
{
    public bool IsIncome { get; set; }
}

public class CategoryResponseDto : CategoryDto
{
    public bool IsIncome { get; set; }
    public IEnumerable<CategoryDto>? Subcategories { get; set; }
}