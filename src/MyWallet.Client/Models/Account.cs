namespace MyWallet.Client.Models;

/// <summary>
/// Тип счёта
/// </summary>
/// <param name="Id">ID</param>
/// <param name="Name">Название</param>
public record AccountType(Guid Id, string Name)
{
    public override string ToString() => Name;
}


/// <summary>
/// Счёт
/// </summary>
/// <param name="Id">ID</param>
/// <param name="Name">Название</param>
/// <param name="AccountType">Тип счёта</param>
/// <param name="Balance">Теукщий баланс на счёте</param>
/// <param name="CurrencySymbol">Валюта (BYN, USD, etc.)</param>
/// <param name="IsDisable">Исключён из статистики</param>
/// <param name="IsArhived">В архиве</param>
public record Account(Guid Id, string Name, string AccountType, decimal Balance, string CurrencySymbol, bool IsDisable, bool IsArhived)
{
    /// <summary>
    /// Цвет в HEX-формате
    /// </summary>
    [JsonPropertyName("color")]
    public string ColorString { get; set; }

    /// <summary>
    /// Статус (тип счёта | исключено | исключено, архивировано)
    /// </summary>
    public string Status
    {
        get
        {
            if (IsDisable && IsArhived)
            {
                return "Исключено, Архивировано";
            }
            if (IsDisable)
            {
                return "Исключено";
            }
            if (IsArhived)
            {
                return "Архивировано";
            }
            return AccountType;
        }
    }

    [JsonIgnore]
    public Color Color => Color.FromArgb(ColorString);
}

/// <summary>
/// Новый счёт
/// </summary>
/// <param name="Name">Название</param>
/// <param name="Number">Номер банковского счёта</param>
/// <param name="AccountTypeId">ID типа счёта</param>
/// <param name="Balance">Начальный баланс</param>
/// <param name="CurrencySymbol">Валюта (BYN, USD, etc.)</param>
/// <param name="Color">Цвет в HEX-формате</param>
public record AccountCreate(string Name, string Number, Guid AccountTypeId, decimal Balance, string CurrencySymbol, string Color);

/// <summary>
/// 
/// </summary>
/// <param name="Name">Название</param>
/// <param name="Number">Номер банковского счёта</param>
/// <param name="AccountTypeId">ID типа счётаparam>
/// <param name="Color">Цвет в HEX-формате</param>
/// <param name="IsDisable">Исключён из статистики</param>
/// <param name="IsArhived">В архиве</param>
public record AccountUpdate(string Name, string Number, Guid AccountTypeId, string Color, bool IsDisable, bool IsArhived);