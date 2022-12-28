namespace MyWallet.Client.ViewModels.MenuPages;

public partial class SettingsPageViewModel : ViewModelBase
{
    public ObservableCollection<UI.MenuItem> Items { get; }

    public SettingsPageViewModel(IAppService appService) : base(appService)
    {
        Title = Strings.Settings;
        Items = new ObservableCollection<UI.MenuItem>(SettingsMenu.Items);
    }

    [RelayCommand]
    private async Task Navigate(string link)
    {
        if (!string.IsNullOrEmpty(link))
        {
            await AppService.Navigation.GoToAsync(link);
        }
    }
}