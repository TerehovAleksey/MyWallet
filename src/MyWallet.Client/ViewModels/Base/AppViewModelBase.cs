namespace MyWallet.Client.ViewModels.Base;

public partial class AppViewModelBase : ViewModelBase
{
    public INavigation NavigationService { get; set; }
    public Page PageService { get; set; }

    [Obsolete("Избавиться, создаёт сервис даже на страницах, где этого не требуется")]
    protected IDataService DataService { get; set; }

    protected AppViewModelBase(IDataService dataService) : base()
    {
        DataService = dataService;
    }

    protected async Task HandleServiceResponseErrorsAsync(IResponse response)
    {
        switch (response.State)
        {
            case State.Success:
                break;
            case State.Error:
                IsErrorState = true;
                ErrorMessage = $"Something went wrong. If the problem persists, plz contact support at email with the error message:{Environment.NewLine}{Environment.NewLine}{response.Errors.FirstOrDefault()}";
                ErrorImage = "error.png";
                break;
            case State.Unauthorized:
                await NavigationService.PushAsync(new LoginPage(false));
                break;
            case State.NotFound:
                break;
            case State.NoInternet:
                IsErrorState = true;
                ErrorMessage = $"Slow or no internet connection.{Environment.NewLine}Please check you internet connection and try again.";
                ErrorImage = "nointernet.png";
                break;
            default:
                break;
        }
    }

    [RelayCommand]
    private async Task NavigateBack() => await NavigationService.PopAsync();

    [RelayCommand]
    private async Task CloseModal() => await NavigationService.PopModalAsync();

}
