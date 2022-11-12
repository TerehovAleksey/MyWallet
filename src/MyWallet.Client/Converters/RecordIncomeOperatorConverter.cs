using CommunityToolkit.Maui.Converters;
using MyWallet.Client.Models;
using System.Globalization;

namespace MyWallet.Client.Converters;

public class RecordIncomeOperatorConverter : BaseConverter<object, string>
{
    public override string DefaultConvertReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override object DefaultConvertBackReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override object ConvertBackTo(string value, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }

    public override string ConvertFrom(object value, CultureInfo? culture)
    {
        bool isIncome = (bool)value;
        return isIncome ? string.Empty : "-";
    }
}
