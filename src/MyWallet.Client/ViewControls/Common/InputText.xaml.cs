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

    #endregion

    public InputText()
	{
		InitializeComponent();
	}

    #region Handlers

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var input = bindable as InputText;
        input.TitleLabel.Text = (string)newValue;
    }

    private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var input = bindable as InputText;
        input.Entry.Placeholder = (string)newValue;
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var input = bindable as InputText;
        input.Entry.Text = (string)newValue;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Value = (sender as Entry).Text;
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

    Entry entry = null;
    Label caption = null;
    Grid footer = null;
    Color entryDefaultColor;
    Color captionDefaultColor;

    #region Bindable properties

    #endregion

    protected override void OnAttachedTo(InputText inputText)
    {
        entry = (Entry)inputText.FindByName("Entry");
        caption = (Label)inputText.FindByName("TitleLabel");
        footer = (Grid)inputText.FindByName("ErrorFooter");
        entryDefaultColor = entry.TextColor;
        captionDefaultColor = caption.TextColor;

        entry.TextChanged += Entry_TextChanged;

        Validate(entry.Text ?? string.Empty);

        base.OnAttachedTo(inputText);
    }

    protected override void OnDetachingFrom(InputText inputText)
    {
        entry.TextChanged -= Entry_TextChanged;
        base.OnDetachingFrom(inputText);
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Validate(e.NewTextValue);
    }

    private void Validate(string text)
    {
        if (text.Length > 5)
        {
            footer.IsVisible = true;
            entry.TextColor = Colors.Red;
            caption.TextColor = Colors.Red;
        }
        else
        {
            footer.IsVisible = false;
            entry.TextColor = entryDefaultColor;
            caption.TextColor = captionDefaultColor;
        }
    }

    private static void OnValidationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //throw new NotImplementedException();
    }
}