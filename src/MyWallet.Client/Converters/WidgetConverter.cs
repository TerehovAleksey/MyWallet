namespace MyWallet.Client.Converters;

public class WidgetConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var widget = (IWidgetViewModel)value;
        if (widget is null)
        {
            return null;
        }
        var result = CreateWidget(widget);
        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static ContentView CreateWidget(IWidgetViewModel viewModel)
    {
        if (viewModel is WidgetLastRecordsViewModel)
        {
            return new WidgetLastRecords
            {
                BindingContext = viewModel
            };
        }

        throw new NotImplementedException(viewModel.GetType().Name);
    }
}
