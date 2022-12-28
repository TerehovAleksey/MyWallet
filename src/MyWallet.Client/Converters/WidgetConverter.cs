namespace MyWallet.Client.Converters;

public class WidgetConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IWidgetViewModel viewModel)
        {
            var result = CreateWidget(viewModel);
            return result;
        }

        return null;
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

        if (viewModel is WidgetCashFlowViewModel)
        {
            return new WidgetCashFlow
            {
                BindingContext = viewModel
            };
        }

        throw new NotImplementedException(viewModel.GetType().Name);
    }
}
