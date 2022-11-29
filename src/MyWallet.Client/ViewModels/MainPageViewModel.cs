namespace MyWallet.Client.ViewModels;
public partial class MainPageViewModel : AppViewModelBase
{
    public ObservableCollection<Account> Accounts { get; } = new();
    public ObservableCollection<object> SelectedAccounts { get; }
    public ObservableCollection<Record> Records { get; } = new();

    [ObservableProperty]
    private Account? _selectedAccount;

    public MainPageViewModel(IDataService dataService) : base(dataService)
    {
        Title = "Главная";

        //Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Карта", Balance = -80, Color = Color.FromArgb("#ad1457"), CurrencySymbol = "BYN" });
        //Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Наличные", Balance = 158.40M, Color = Color.FromArgb("#039be5"), CurrencySymbol = "BYN" });
        //Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Наличные (USD)", Balance = 0, Color = Color.FromArgb("#43a047"), CurrencySymbol = "USD" });

        SelectedAccounts = new ObservableCollection<object>(Accounts);
    }

    public override async void OnNavigatedTo(object? parameters)
    {
        SetDataLoadingIndicators();

        LoadingText = "Hold on...";

        try
        {
            var accounts = await DataService.GetAccountsAsync();
            Accounts.AddRange(accounts);
           // var records = await _dataService.GetRecordsAsync(new DateTime(2020, 1, 1), new DateTime(2023, 1, 1));
           // Records.AddRange(records);
        }
        catch (InternetConnectionException)
        {
            IsErrorState = true;
            ErrorMessage = $"Slow or no internet connection.{Environment.NewLine}Please check you internet connection and try again.";
            ErrorImage = "nointernet.png";
        }
        catch (Exception ex)
        {
            IsErrorState = true;
            ErrorMessage =
                $"Something went wrong. If the problem persists, plz contact support at email with the error message:{Environment.NewLine}{Environment.NewLine}{ex.Message}";
            ErrorImage = "error.png";
        }
        finally
        {
            SetDataLoadingIndicators(false);
        }
    }

    [RelayCommand]
    private async void OpenNotificationsPage()
    {
        await PageService.DisplayAlert("Notifications", "Not implemented yet!", "Got it!");
    }

    [RelayCommand]
    private async void OpenAccountsPage()
    {
        await NavigationService.PushAsync(new AccountsPage(), true);
    }
}
