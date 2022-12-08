namespace MyWallet.Client.ViewControls.Common;

public partial class InputText : VerticalStackLayout
{
    #region Bindable properties

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(InputText), string.Empty, propertyChanged: OnTitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(InputText), string.Empty, propertyChanged: OnPlaceholderChanged);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(InputText), string.Empty, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(InputText), false, BindingMode.OneWay, propertyChanged: OnIsPasswordChanged);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(InputText), false, BindingMode.OneWay, propertyChanged: OnIsReadOnlyChanged);

    public new bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    #endregion

    public InputText()
	{
		InitializeComponent();
	}

    #region Handlers

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.TitleLabel.Text = (string)newValue;
        }
    }

    private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.Entry.Placeholder = (string)newValue;
        }
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.Entry.Text = (string)newValue;
        }
    }

    private static void OnIsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.Entry.IsPassword = (bool)newValue;
        }
    }
    private static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.Entry.IsEnabled = !(bool)newValue;
            input.Entry.IsReadOnly = (bool)newValue;
        }
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Value = (sender as Entry)?.Text ?? string.Empty;
    }

    #endregion

}

public class InputTextValidationBehavior : Behavior<InputText>
{
    public static readonly BindableProperty MinimumLengthProperty =
        BindableProperty.Create(nameof(MinimumLength), typeof(int), typeof(InputTextValidationBehavior), 0, propertyChanged: OnValidationPropertyChanged);

    public int MinimumLength
    {
        get => (int)GetValue(MinimumLengthProperty);
        set => SetValue(MinimumLengthProperty, value);
    }

    public static readonly BindableProperty MaximumLengthProperty =
        BindableProperty.Create(nameof(MaximumLength), typeof(int), typeof(InputTextValidationBehavior), int.MaxValue, propertyChanged: OnValidationPropertyChanged);

    public int MaximumLength
    {
        get => (int)GetValue(MaximumLengthProperty);
        set => SetValue(MaximumLengthProperty, value);
    }

    private Entry? _entry;
    private Label? _caption;
    private Grid? _footer;
    private Color? _entryDefaultColor;
    private Color? _captionDefaultColor;

    #region Bindable properties

    #endregion

    protected override void OnAttachedTo(InputText inputText)
    {
        _entry = (Entry)inputText.FindByName("Entry");
        _caption = (Label)inputText.FindByName("TitleLabel");
        _footer = (Grid)inputText.FindByName("ErrorFooter");
        _entryDefaultColor = _entry.TextColor;
        _captionDefaultColor = _caption.TextColor;

        _entry.TextChanged += Entry_TextChanged;

        Validate(_entry.Text ?? string.Empty);

        base.OnAttachedTo(inputText);
    }

    protected override void OnDetachingFrom(InputText inputText)
    {
        if (_entry != null)
        {
            _entry.TextChanged -= Entry_TextChanged;
        }
        base.OnDetachingFrom(inputText);
    }

    private void Entry_TextChanged(object? sender, TextChangedEventArgs e)
    {
        Validate(e.NewTextValue);
    }

    private void Validate(string text)
    {
        if (text.Length > 5)
        {
            _footer.IsVisible = true;
            _entry.TextColor = Colors.Red;
            _caption.TextColor = Colors.Red;
        }
        else
        {
            _footer.IsVisible = false;
            _entry.TextColor = _entryDefaultColor;
            _caption.TextColor = _captionDefaultColor;
        }
    }

    private static void OnValidationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //throw new NotImplementedException();
    }
}