namespace MyWallet.Core.Models;

public class BaseCategory : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Отображается или нет в клиентском приложении
    /// </summary>
    public bool IsVisible { get; set; }

    /// <summary>
    /// Иконка или изображение
    /// </summary>
    public string? ImageName { get; set; }

    /// <summary>
    /// Цвет
    /// </summary>
    public string Color { get; set; } = default!;

    /// <summary>
    /// ID Характера категории
    /// </summary>
    public Guid? CategoryTypeId { get; set; }
    
    /// <summary>
    /// Характер категории
    /// </summary>
    public CategoryType? CategoryType { get; set; }
}