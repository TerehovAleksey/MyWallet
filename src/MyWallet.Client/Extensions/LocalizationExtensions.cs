namespace MyWallet.Client.Extensions;

public static class LocalizationExtensions
{
    public static string ToShortDisplayString(this TimePeriod timePeriod)
    {
        return timePeriod switch
        {
            TimePeriod.Today => Strings.Today,
            TimePeriod.CurrentWeek => Strings.ThisWeek,
            TimePeriod.CurrentMonth => Strings.ThisMonth,
            TimePeriod.CurrentYear => Strings.ThisYear,
            TimePeriod.Days7 => Strings.Days7,
            TimePeriod.Days30 => Strings.Days30,
            TimePeriod.Week12 => Strings.Week12,
            TimePeriod.Month6 => Strings.Month6,
            TimePeriod.Year1 => Strings.Year1,
            TimePeriod.Year5 => Strings.Year5,
            _ => Strings.NotSpecified,
        };
    }
    
    public static string ToLongDisplayString(this TimePeriod timePeriod)
    {
        return timePeriod switch
        {
            TimePeriod.Today => Strings.Today,
            TimePeriod.CurrentWeek => Strings.ThisWeek,
            TimePeriod.CurrentMonth => Strings.ThisMonth,
            TimePeriod.CurrentYear => Strings.ThisYear,
            TimePeriod.Days7 => Strings.Last7Days,
            TimePeriod.Days30 => Strings.Last30Days,
            TimePeriod.Week12 => Strings.Last12Weeks,
            TimePeriod.Month6 => Strings.Last6Month,
            TimePeriod.Year1 => Strings.Last1Year,
            TimePeriod.Year5 => Strings.Last5Years,
            _ => Strings.NotSpecified,
        };
    }
}
