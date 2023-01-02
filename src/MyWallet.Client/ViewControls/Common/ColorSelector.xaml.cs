namespace MyWallet.Client.ViewControls.Common;

public partial class ColorSelector : Grid
{
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorSelector), Colors.White, BindingMode.TwoWay, propertyChanged: OnColorChanged);

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public ColorSelector()
	{
		InitializeComponent();
        Collection.ItemsSource = UserColors.GetColors().Select(c => Color.FromArgb(c));
	}

    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is ColorSelector collorSelector)
        {
            collorSelector.SelectedColor.BackgroundColor = (Color)newValue;
        }
    }

    private void SelectColor_Tapped(object sender, TappedEventArgs e)
    {
        if(sender is BindableObject control && control.BindingContext is Color color)
        {
            Color = color;
        }
        Selector.IsVisible = false;
        Selector.Focus();
        SelectedColor.IsVisible = true;
    }

    private void OpenCollection_Tapped(object sender, TappedEventArgs e)
    {
        Selector.IsVisible = true;
        SelectedColor.IsVisible = false;
    }
}