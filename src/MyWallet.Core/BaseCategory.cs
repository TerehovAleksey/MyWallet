namespace MyWallet.Core;

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
}