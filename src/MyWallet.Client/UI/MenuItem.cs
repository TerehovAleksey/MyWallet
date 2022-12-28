namespace MyWallet.Client.UI;

public class MenuItem
{
    public string Icon { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Link { get; set; } = default!;
    public bool HasSeparator { get; set; }
    public Color Color { get; set; } = Colors.Transparent;
}

public static class SettingsMenu
{
    public static List<MenuItem> Items => new()
        {
            new()
            {
                Icon = "settings_user.png",
                Title = Strings.UserProfile,
                Description = Strings.UserProfileDescription,
                Link = "user"
            },
            new()
            {
                Icon = "settings_devices.png",
                Title = Strings.UserDevices,
                Description = Strings.UserDevicesDescription,
                Link = "devices"
            },
            new()
            {
                Icon = "settings_accounts.png",
                Title = Strings.Accounts,
                Description = Strings.AccountsDescription,
                Link = "accounts"
            },
            new()
            {
                Icon = "settings_categories.png",
                Title = Strings.Categories,
                Description = Strings.CategoriesDescription,
                Link = "categories"
            },
            new()
            {
                Icon = "settings_currencies.png",
                Title = Strings.Currencies,
                Description = Strings.CurrenciesDescription,
                Link = "currencies"
            }
        };
}