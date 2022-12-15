namespace MyWallet.Client.Extensions;

public static class EnumExtensions
{
    public static string ToDisplayString(this TimePeriod timePeriod)
    {
        return timePeriod switch
        {
            TimePeriod.Today => "Сегодня",
            TimePeriod.CurrentWeek => "Текущая неделя",
            TimePeriod.CurrentMonth => "Текущий месяц",
            TimePeriod.CurrentYear => "Текущий год",
            TimePeriod.Days7 => "7 дней",
            TimePeriod.Days30 => "30 дней",
            TimePeriod.Week12 => "12 недель",
            TimePeriod.Month6 => "6 месяцев",
            TimePeriod.Year1 => "1 год",
            TimePeriod.Year5 => "5 лет",
            _ => "Не указано",
        };
    }
}
