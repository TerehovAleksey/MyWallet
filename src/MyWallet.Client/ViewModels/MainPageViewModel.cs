namespace MyWallet.Client.ViewModels;
public partial class MainPageViewModel : AppViewModelBase
{
    private readonly IDataService _dataService;
    
    public ObservableCollection<Account> Accounts { get; } = new();
    public ObservableCollection<object> SelectedAccounts { get; } = new();
    
    public ObservableCollection<Record> Records { get; } = new();

    [ObservableProperty]
    private Account? _selectedAccount;

    public MainPageViewModel(IDataService dataService)
    {
        _dataService = dataService;
        Title = "Главная";
    }

    public override async void OnNavigatedTo(object? parameters)
    {
        SetDataLoadingIndicators();
        var response = await _dataService.GetAccountsAsync();
        SetDataLoadingIndicators(false);
        await HandleServiceResponseErrorsAsync(response);
        if (response.Item != null)
        {
            Accounts.AddRange(response.Item);
        }
    }

    [RelayCommand]
    private async void OpenNotificationsPage()
    {
        await PageService.DisplayAlert("Notifications", "Not implemented yet!", "Got it!");
    }

    [RelayCommand]
    private async void OpenAccountsPage()
    {
        await NavigationService.PushAsync(new AccountsPage(), true);
    }
}
