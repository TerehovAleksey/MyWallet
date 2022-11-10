namespace MyWallet.Core;

public class BaseEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime DateOfCreation { get; set; }

    /// <summary>
    /// Дата изменения
    /// </summary>
    public DateTime? DateOfChange { get; set; }
}