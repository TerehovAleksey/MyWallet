namespace MyWallet.Client.Converters;

public class AccountsCounterVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var count = (int)value;
        if (count < 1)
        {
            return false;
        }

        //if(parameter is not null)
        //{
        //    var totalCount = (int)parameter;
        //    if (count == totalCount)
        //    {
        //        return false;
        //    }
        //}

        return true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
