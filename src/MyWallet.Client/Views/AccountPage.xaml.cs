namespace MyWallet.Client.Views;

public partial class AccountPage : ViewBase<AccountPageViewModel>
{
	/// <summary>
	/// Account new/edit page
	/// </summary>
	/// <param name="param">Account for editing or null</param>
	public AccountPage(object? param) : base(param)
	{
		InitializeComponent();
	}
}