namespace MyWallet.Client.Services.Widget;

public static class WidgetFabric
{
    public static IWidgetViewModel CreateWidget(WidgetSettings settings)
    {
        IWidgetViewModel? widget = null;
        switch (settings.Widgets)
        {
            case Widgets.LastRecords:
                widget = ServiceHelpers.GetService<WidgetLastRecordsViewModel>();
                widget.Title = "Последние записи";
                break;
            case Widgets.CashFlow:
                widget = ServiceHelpers.GetService<WidgetCashFlowViewModel>();
                widget.Title = "Денежный поток";
                break;
        }

        if (widget is null)
        {
            throw new NotImplementedException(nameof(settings.Widgets));
        }
        return widget;
    }
}