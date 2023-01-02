namespace MyWallet.Client.Services.Widget;

public static class WidgetFabric
{
    public static IWidgetViewModel CreateWidget(WidgetSettings settings)
    {
        IWidgetViewModel? widget = null;
        switch (settings.WidgetType)
        {
            case WidgetType.LastRecords:
                widget = ServiceHelpers.GetService<WidgetLastRecordsViewModel>();
                break;
            case WidgetType.CashFlow:
                widget = ServiceHelpers.GetService<WidgetCashFlowViewModel>();
                break;
        }

        if (widget is null)
        {
            throw new NotImplementedException(nameof(settings.WidgetType));
        }

        widget.Id = settings.Id;
        widget.Filter = settings.Filter;
        widget.WidgetType = settings.WidgetType;

        return widget;
    }
}