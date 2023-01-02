namespace MyWallet.Client.Models.Settings;

public record WidgetSettings(Guid Id, WidgetType WidgetType, WidgetFilter Filter);
public record WidgetFilter(TimePeriod TimePeriod = TimePeriod.Days7);

public enum TimePeriod
{
    Today,
    CurrentWeek,
    CurrentMonth,
    CurrentYear,
    Days7,
    Days30,
    Week12,
    Month6,
    Year1,
    Year5
}