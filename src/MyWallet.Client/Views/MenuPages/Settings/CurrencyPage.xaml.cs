namespace MyWallet.Client.Views.MenuPages.Settings;

public partial class CurrencyPage : PageBase
{
	public CurrencyPage(CurrencyPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

#if WINDOWS
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        CurrencyPicker.WidthRequest = CurrencyPickerLayout.Width;
    }
#endif
}