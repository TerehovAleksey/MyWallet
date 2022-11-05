namespace MyWallet.Client.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

	private async void CategoryCell_Tapped(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync(nameof(CategoryPage));
    }
}