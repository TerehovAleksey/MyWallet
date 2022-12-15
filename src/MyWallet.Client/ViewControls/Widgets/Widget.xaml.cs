namespace MyWallet.Client.ViewControls.Widgets;

public partial class Widget : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(Widget), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public Widget()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        Title = (BindingContext as IWidgetViewModel)!.Title;
    }

    protected void LinkButton_Clicked(object sender, EventArgs e)
    {
        //
    }

    protected void FilterButton_Clicked(object sender, EventArgs e) => (BindingContext as IWidgetViewModel)!.OpenFilterAsync();
}