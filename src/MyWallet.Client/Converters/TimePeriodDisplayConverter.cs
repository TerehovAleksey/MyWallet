namespace MyWallet.Client.Converters;

public class TimePeriodDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var timePeriod = (TimePeriod)Enum.Parse(typeof(TimePeriod), (string)value);
        return timePeriod.ToShortDisplayString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}