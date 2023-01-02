using MenuItem = MyWallet.Client.UI.MenuItem;

namespace MyWallet.Client.ViewModels.MenuPages.Settings;

public partial class DevicesPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    public ObservableCollection<MenuItem> MenuItems { get; } = new();

    public DevicesPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
        Title = Strings.UserDevices;
    }

    public override Task InitializeAsync() => IsBusyFor(async () =>
    {
        var devices = await _dataService.Auth.GetUserDevicesAsync().ConfigureAwait(false);
        var menuItems = devices
            .Select(c => new MenuItem
            {
                Title = c.Name,
                Description = c.ToString(),
                HasSeparator = true,
            });
        MenuItems.AddRange(menuItems, true);
    });

    [RelayCommand]
    private Task Navigate(string link) => AppService.Dialog.ShowInDevelopmentMessage();
}