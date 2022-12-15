using System.Diagnostics;

namespace MyWallet.Client.ViewControls.Dialog;

public partial class WidgetSettingsDialog : VerticalStackLayout, IDialog
{
    private TimePeriod[] _timePeriodsEnums;
    private string[] _timePeriods;
    //private TimePeriod _period;
    private string _period;

    private DialogParameters? _parameters;
    public DialogParameters? Parameters
    {
        get => _parameters;
        set
        {
            if (_parameters != value)
            {
                _parameters = value;

                // ссылка должна поменяться
                //_period = _parameters!.Get<TimePeriod>("TimePeriod");
                _period = _parameters!.Get<string>("TimePeriod");

                Debug.Write($"--->Hash 2: {_period.GetHashCode()}");

               // var index = Array.FindIndex(_timePeriods, x => x == Enum.GetName(_period));
                var index = Array.FindIndex(_timePeriods, x => x == _period);
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
        TimePeriodPicker.ItemsSource = _timePeriodsEnums.Select(x => x.ToDisplayString()).ToArray();
    }

    private void TimePeriodPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var index = (sender as Picker)!.SelectedIndex;
        //_period = _timePeriodsEnums[index];
        _period = _timePeriods[index];
    }
}