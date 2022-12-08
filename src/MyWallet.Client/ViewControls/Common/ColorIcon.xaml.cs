namespace MyWallet.Client.ViewControls.Common;

public partial class ColorIcon : Frame
{
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorIcon), propertyChanged: OnColorChanged);

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(ColorIcon), string.Empty);

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public ColorIcon()
	{
		InitializeComponent();
	}

    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ColorIcon input)
        {
            input.ColorIconFrame.BackgroundColor = (Color)newValue;
        }
    }
}