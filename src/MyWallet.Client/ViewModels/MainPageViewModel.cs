namespace MyWallet.Client.ViewModels;
public partial class MainPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    
    public ObservableCollection<Account> Accounts { get; } = new();
    public ObservableCollection<object> SelectedAccounts { get; } = new();
    
    public ObservableCollection<Record> Records { get; } = new();

    [ObservableProperty]
    private Account? _selectedAccount;

    public MainPageViewModel(IDataService dataService, IDialogService dialogService, INavigationService navigationService) : base(dialogService, navigationService)
    {
        _dataService = dataService;
        Title = "Главная";
    }

    public override Task InitializeAsync()
    {
        return IsBusyFor(async () =>
        {
            var response = await _dataService.GetAccountsAsync();
            await HandleServiceResponseErrorsAsync(response);
            if (response.Item != null)
            {
                Accounts.AddRange(response.Item, true);
            }

            var records = await _dataService.GetRecordsAsync(new DateTime(2020, 01, 01), new DateTime(2023, 01, 01));
            await HandleServiceResponseErrorsAsync(records);
            if (records.Item != null)
            {
                Records.AddRange(records.Item, true);
            }
        });
    }

    [RelayCommand]
    private async void OpenNotificationsPage()
    {
        await DialogService.ShowAlertAsync("Notifications", "Not implemented yet!", "Got it!");
    }

    [RelayCommand]
    private void OpenAccountsPage() => NavigationService.GoToAsync("accounts");
    
    [RelayCommand]
    private void OpenRecordPage() => NavigationService.GoToAsync("record");
}
