namespace MyWallet.Client.ViewModels;
public partial class MainPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    
    public WidgetContainerViewModel Widgets { get; }

    public ObservableCollection<Account> Accounts { get; } = new();
    public ObservableCollection<object> SelectedAccounts { get; } = new();
    
    public MainPageViewModel(IDataService dataService, IDialogService dialogService, INavigationService navigationService, WidgetContainerViewModel widgetContainer) : base(dialogService, navigationService)
    {
        _dataService = dataService;
        Widgets = widgetContainer;
        Title = "Главная";
        OneTimeInitialized = false;

        SelectedAccounts.CollectionChanged += SelectedAccounts_CollectionChanged;
    }

    private void SelectedAccounts_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (IsInitialized)
        {
            //
        }
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
                SelectedAccounts.AddRange(response.Item, true);
            }

            await Widgets.LoadingWidgetsAsync();
        });
    }

    [RelayCommand]
    private async void OpenNotificationsPage()
    {
        await DialogService.ShowAlertAsync("Notifications", "Not implemented yet!", "Got it!");
    }

    [RelayCommand]
    private void OpenAccountsPage() => NavigationService.GoToAsync(nameof(AccountsPage));
    
    [RelayCommand]
    private void OpenRecordPage() => NavigationService.GoToAsync(nameof(RecordPage));

    protected override void Dispose(bool disposing)
    {
        SelectedAccounts.CollectionChanged -= SelectedAccounts_CollectionChanged;
        base.Dispose(disposing);
    }
}
