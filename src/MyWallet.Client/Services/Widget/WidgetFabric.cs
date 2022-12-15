namespace MyWallet.Client.Services.Widget;

public static class WidgetFabric
{
    public static IWidgetViewModel CreateWidget(WidgetSettings settings)
    {
        var widget = ServiceHelpers.GetService<WidgetLastRecordsViewModel>();
        widget.Title = "Последние записи";
        return widget;
    }
}