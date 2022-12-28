namespace MyWallet.Client.ViewModels;

public partial class MainPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    public WidgetContainerViewModel Widgets { get; }

    public ObservableCollection<Account> Accounts { get; } = new();
    public ObservableCollection<object> SelectedAccounts { get; } = new();

    public MainPageViewModel(IAppService appService, IDataService dataService, WidgetContainerViewModel widgetContainer) : base(appService)
    {
        _dataService = dataService;
        Widgets = widgetContainer;
        Title = Strings.Home;
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
            var accounts = await _dataService.Account.GetAccountsAsync();
            Accounts.AddRange(accounts, true);
            SelectedAccounts.AddRange(accounts, true);

            await Widgets.LoadingWidgetsAsync();
        });
    }

    [RelayCommand]
    private Task OpenNotificationsPage() => DialogService.ShowInDevelopmentMessage();

    [RelayCommand]
    private Task OpenAccountsPage() => NavigationService.GoToAsync("accounts");

    [RelayCommand]
    private Task OpenRecordPage() => NavigationService.GoToAsync("record");

    [RelayCommand]
    private Task OpenWidgetTabs() => NavigationService.GoToAsync(nameof(WidgetTabs));

    protected override void Dispose(bool disposing)
    {
        SelectedAccounts.CollectionChanged -= SelectedAccounts_CollectionChanged;
        base.Dispose(disposing);
    }
}