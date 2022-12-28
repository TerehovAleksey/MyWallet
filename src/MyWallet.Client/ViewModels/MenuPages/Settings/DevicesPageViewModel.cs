namespace MyWallet.Client.ViewModels.MenuPages.Settings;

public partial class DevicesPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    public ObservableCollection<UserDeviceDto> UserDevices { get; } = new();

    public DevicesPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
    }

    public override Task InitializeAsync() => IsBusyFor(async () =>
    {
        var devices = await _dataService.Auth.GetUserDevicesAsync().ConfigureAwait(false);
        UserDevices.AddRange(devices);
    });

    [RelayCommand]
    private Task Delete(UserDeviceDto userDevice) => IsBusyFor(async () =>
    {
        await _dataService.Auth.DeleteUserDevice(userDevice.Name);
        var devices = await _dataService.Auth.GetUserDevicesAsync().ConfigureAwait(false);
        UserDevices.AddRange(devices, true);
    });
}