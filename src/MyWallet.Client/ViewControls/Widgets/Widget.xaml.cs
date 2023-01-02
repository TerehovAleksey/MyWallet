namespace MyWallet.Client.ViewControls.Widgets;

public partial class Widget : ContentView
{
    public Widget()
	{
		InitializeComponent();
    }

    protected void LinkButton_Clicked(object sender, EventArgs e) => (BindingContext as IWidgetViewModel)!.OpenDetailsAsync();
    protected void FilterButton_Clicked(object sender, EventArgs e) => (BindingContext as IWidgetViewModel)!.OpenFilterAsync();
}