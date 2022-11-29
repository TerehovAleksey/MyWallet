namespace MyWallet.Client.Converters;

public class DateTimeToDayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var dateTime = (DateTime)value;
        return (DateTime.Today - dateTime.Date).TotalDays switch
        {
            0 => "Сегодня",
            1 => "Вчера",
            _ => dateTime.ToString("dd.MM.yyyy")
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
