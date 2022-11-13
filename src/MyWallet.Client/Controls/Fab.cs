namespace MyWallet.Client.Controls;

public class Fab : Button
{
	public Fab()
	{
		Margin = new Thickness(0, 0, 20, 20);
		CornerRadius = 30;
		FontSize = 30;
		HeightRequest = 60;
		WidthRequest = 60;
		HorizontalOptions = LayoutOptions.End;
		VerticalOptions = LayoutOptions.End;
		Text = "+";
	}
}
