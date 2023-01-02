namespace MyWallet.Client.ViewControls.Dialog;

public partial class WidgetSettingsDialog : VerticalStackLayout, IDialog
{
    private readonly TimePeriod[] _timePeriodsEnums;
    private readonly string[] _timePeriods;

    private DialogParameters? _parameters;
    public DialogParameters? Parameters
    {
        get => _parameters;
        set
        {
            if (value != null && _parameters != value)
            {
                _parameters = value;
                var index = Array.FindIndex(_timePeriods, x => x == _parameters.Get<string>("TimePeriod"));
                TimePeriodPicker.SelectedIndex = index;
            }
        }
    }

#if WINDOWS
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        TimePeriodPicker.WidthRequest = width;
        FilterPicker.WidthRequest = width;
    }
#endif

    public WidgetSettingsDialog()
    {
        InitializeComponent();

        _timePeriodsEnums = Enum.GetValues<TimePeriod>();
        _timePeriods = Enum.GetNames<TimePeriod>();
        TimePeriodPicker.ItemsSource = _timePeriodsEnums.Select(x => x.ToShortDisplayString()).ToArray();
    }

    private void TimePeriodPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var index = (sender as Picker)!.SelectedIndex;
        if(_parameters is not null)
        {
            _parameters["TimePeriod"] = _timePeriods[index];
        }
    }
}