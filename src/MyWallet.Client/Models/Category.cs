namespace MyWallet.Client.Models;

public class CategoryType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class BaseCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ImageName { get; set; } = default!;
    public bool IsVisible { get; set; }

    [JsonPropertyName("color")]
    public string ColorString { get; set; } = default!;

    [JsonIgnore]
    public Color Color => Color.FromArgb(ColorString);

    public CategoryType Type { get; set; } = default!;

    public override string ToString() => Name;
}

public class Category : BaseCategory
{
    public bool IsIncome { get; set; }
    public List<BaseCategory> Subcategories { get; set; } = new();
}