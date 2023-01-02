namespace MyWallet.Client.ViewControls.Dialog;

public partial class InputDialog : VerticalStackLayout, IDialog
{
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(InputDialog), string.Empty, propertyChanged: OnValueChanged);

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private DialogParameters? _parameters;
    public DialogParameters? Parameters
    {
        get => _parameters;
        set
        {
            if (_parameters != value)
            {
                _parameters = value;
                InputText.Title = _parameters!.Get<string>("Placeholder");
                Value = _parameters!.Get<string>("Value");
            }
        }
    }
    
    public InputDialog()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputDialog { Parameters: { } } input)
        {
           input.Parameters["Value"] = newValue;
        }
    }
}