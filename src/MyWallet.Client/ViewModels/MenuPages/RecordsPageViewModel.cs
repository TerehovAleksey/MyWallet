namespace MyWallet.Client.ViewModels.MenuPages;

public class RecordsPageViewModel : ViewModelBase
{
    public RecordsPageViewModel(IAppService appService) : base(appService)
    {
        Title = Strings.Records;
    }
}
