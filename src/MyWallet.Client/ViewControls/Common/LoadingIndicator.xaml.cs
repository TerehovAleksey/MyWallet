namespace MyWallet.Client.ViewControls.Common;

public partial class LoadingIndicator : Grid
{
    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
        nameof(IsBusy),
        typeof(bool),
        typeof(LoadingIndicator),
        false,
        BindingMode.OneWay,
        null,
        SetIsBusy);

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    private static void SetIsBusy(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is LoadingIndicator control)
        {
            control.IsVisible = (bool)newValue;
            control.Indicator.IsRunning = (bool)newValue;
        }
    }


    public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(
        nameof(LoadingText),
        typeof(string),
        typeof(LoadingIndicator),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetLoadingText);

    public string LoadingText
    {
        get => (string)GetValue(LoadingTextProperty);
        set => SetValue(LoadingTextProperty, value);
    }

    private static void SetLoadingText(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is LoadingIndicator control)
        {
            control.LoadingTextLabel.Text = (string)newValue;
        }
    }

    public LoadingIndicator()
    {
        InitializeComponent();
    }
}