using CommunityToolkit.Maui.Converters;
using MyWallet.Client.Models;
using System.Globalization;

namespace MyWallet.Client.Converters;

public class RecordToColorConverter : BaseConverter<object, Color>
{
    public override Color DefaultConvertReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override object DefaultConvertBackReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override object ConvertBackTo(Color value, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }

    public override Color ConvertFrom(object value, CultureInfo? culture)
    {
        bool isIncome = (bool)value;
        return isIncome ? Colors.Green : Colors.Red;
    }
}
