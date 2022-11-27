namespace MyWallet.Client.Models;

public class BaseCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string IconName { get; set; } = default!;

    public bool IsVisible { get; set; }
}