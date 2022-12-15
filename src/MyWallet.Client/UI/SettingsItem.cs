namespace MyWallet.Client.UI;

public class SettingsItem
{
    public string Icon { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Link { get; set; } = default!;

    public static List<SettingsItem> GetItems()
    {
        return new List<SettingsItem>()
        {
            new()
            {
                Icon = "user.png",
                Title = "Профиль пользователя",
                Description = "Изменить изображение профиля, имя или пароль, выйти из системы или удалить данные",
                Link = nameof(UserPage)
            },
            new()
            {
                Icon = "bitcoin.png",
                Title = "Валюты",
                Description = "Добавить другие валюты, скорректировать обменные курсы",
                Link = nameof(CurrenciesPage)
            }
        };
    }
}