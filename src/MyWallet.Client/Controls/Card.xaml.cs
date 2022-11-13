namespace MyWallet.Client.Controls;

public partial class Card : ContentView
{
    public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(Card), string.Empty);

    public string CardTitle
    {
        get => (string)GetValue(CardTitleProperty);
        set => SetValue(CardTitleProperty, value);
    }

    public Card()
    {
        InitializeComponent();
        BindingContext = this;
    }
}