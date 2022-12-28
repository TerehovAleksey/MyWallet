namespace MyWallet.Client.ViewControls.Common;

public partial class ErrorIndicator : Grid
{
    public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
        nameof(IsError),
        typeof(bool),
        typeof(ErrorIndicator),
        false,
        BindingMode.OneWay,
        null,
        SetIsError);

    public bool IsError
    {
        get => (bool)GetValue(IsErrorProperty);
        set => SetValue(IsErrorProperty, value);
    }

    private static void SetIsError(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ErrorIndicator control)
        {
            control.IsVisible = (bool)newValue;
        }
    }

    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText),
        typeof(string),
        typeof(ErrorIndicator),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetErrorText);

    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    private static void SetErrorText(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ErrorIndicator control)
        {
            control.ErrorTextLabel.Text = (string)newValue;
        }
    }

    public static readonly BindableProperty ErrorImageProperty = BindableProperty.Create(
        nameof(ErrorImage),
        typeof(ImageSource),
        typeof(ErrorIndicator),
        null,
        BindingMode.OneWay,
        null,
        SetErrorImage);

    public ImageSource ErrorImage
    {
        get => (ImageSource)GetValue(ErrorImageProperty);
        set => SetValue(ErrorImageProperty, value);
    }

    private static void SetErrorImage(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ErrorIndicator control)
        {
            control.ErrorIcon.Source = (ImageSource)newValue;
        }
    }

    public ErrorIndicator()
    {
        InitializeComponent();
    }

    private void RefreshButton_Clicked(object sender, EventArgs e)
    {
        var pageBase = Parent.Parent as PageBase;
        if (pageBase?.BindingContext is IViewModelBase vm)
        {
            vm.Reload();
        }
    }
}