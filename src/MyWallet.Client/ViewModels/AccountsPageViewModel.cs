﻿namespace MyWallet.Client.ViewModels;

public partial class AccountsPageViewModel : AppViewModelBase
{
    private readonly IDataService _dataService;
    
    private Account? _selectedAccount;
    public Account? SelectedAccount
    {
        get => _selectedAccount;
        set
        {
            if (SetProperty(ref _selectedAccount, value) && value is not null) 
            {
                NavigationService.PushAsync(new AccountPage(_selectedAccount));
            }
        }
    }

    public ObservableCollection<Account> Accounts { get; } = new();

    public AccountsPageViewModel(IDataService dataService)
	{
        _dataService = dataService;
        Title = "Настройки счетов";
    }

    public override async void OnNavigatedTo(object parameters, bool wasLoaded)
    {
        var accounts = await _dataService.GetAccountsAsync();
        Accounts.AddRange(accounts.Item, true);
        SelectedAccount = null;
    }

    #region Commands

    [RelayCommand]
    private async Task OpenAccountPage()
    {
        await NavigationService.PushAsync(new AccountPage(null));
    }

    #endregion
}
