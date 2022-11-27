namespace MyWallet.Client.Converters;

public class AccountsCountToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var count = (int)value;
        var str = count switch
        {
            < 1 => string.Empty,
            < 2 => "выбранный счёт",
            < 5 => "выбранных счета",
            _ => "выбранных счетов"
        };

        return $"{count} {str}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
