namespace MyWallet.Client.ViewModels;

public class NotificationsPageViewModel : ViewModelBase
{
    public NotificationsPageViewModel(IAppService appService) : base(appService)
    {
        Title = Strings.Notifications;
    }
}
