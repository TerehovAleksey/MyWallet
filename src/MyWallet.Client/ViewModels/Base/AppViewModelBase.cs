namespace MyWallet.Client.ViewModels.Base;

public partial class AppViewModelBase : ViewModelBase
{
    public INavigation NavigationService { get; set; }
    public Page PageService { get; set; }

    protected IDataService DataService { get; set; }

    public AppViewModelBase(IDataService dataService) : base()
    {
        DataService = dataService;
    }

    [RelayCommand]
    private async Task NavigateBack() => await NavigationService.PopAsync();

    [RelayCommand]
    private async Task CloseModal() => await NavigationService.PopModalAsync();

}
