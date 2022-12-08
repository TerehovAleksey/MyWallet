namespace MyWallet.Client.Converters;

public class GenderDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var gender = (Gender)Enum.Parse(typeof(Gender), (string)value);

        return gender switch
        {
            Gender.Male => "Мужской",
            Gender.Female => "Женский",
            _ => "Не указано",
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
