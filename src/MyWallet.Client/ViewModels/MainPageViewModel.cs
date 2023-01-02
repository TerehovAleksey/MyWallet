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
        Widgets.CurrentAccounts = SelectedAccounts;
        Title = Strings.Home;
        OneTimeInitialized = false;
    }

    public override Task InitializeAsync()
    {
        return IsBusyFor(async () =>
        {
            var accounts = await _dataService.Account.GetAccountsAsync();
            var currentAccounts = accounts.Where(a => !a.IsDisabled && !a.IsArchived);

            Accounts.AddRange(currentAccounts, true);

            await Widgets.LoadingWidgetsAsync();

            //TODO: SelectedAccounts надо сохранять и получать
            SelectedAccounts.AddRange(currentAccounts, true);
        });
    }

    [RelayCommand]
    private Task Navigate(string path) => NavigationService.GoToAsync(path);
    
    [RelayCommand]
    private Task InDevelopment(string path) => DialogService.ShowInDevelopmentMessage();
    
    // [RelayCommand]
    // private Task OpenNotificationsPage() => DialogService.ShowInDevelopmentMessage();
    //
    // [RelayCommand]
    // private Task OpenAccountsPage() => NavigationService.GoToAsync("accounts");
    //
    // [RelayCommand]
    // private Task OpenRecordPage() => NavigationService.GoToAsync("record");
    //
    // [RelayCommand]
    // private Task OpenRecordsPage() => NavigationService.GoToAsync("records");
    //
    // [RelayCommand]
    // private Task OpenWidgetTabs() => AppService.Dialog.ShowInDevelopmentMessage();
    //
    [RelayCommand]
    private Task OpenAdjustWizard() => AppService.Dialog.ShowInDevelopmentMessage();
}