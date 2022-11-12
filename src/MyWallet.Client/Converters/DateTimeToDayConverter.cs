using CommunityToolkit.Maui.Converters;
using System.Globalization;

namespace MyWallet.Client.Converters;

public class DateTimeToDayConverter : BaseConverter<object, string>
{
    public override string DefaultConvertReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override object DefaultConvertBackReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override object ConvertBackTo(string value, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }

    public override string ConvertFrom(object value, CultureInfo? culture)
    {
        DateTime dateTime = (DateTime)value;
        return (DateTime.Today - dateTime.Date).TotalDays switch
        {
            0 => "Сегодня",
            1 => "Вчера",
            _ => dateTime.ToString("dd.MM.yyyy")
        };
    }
}
