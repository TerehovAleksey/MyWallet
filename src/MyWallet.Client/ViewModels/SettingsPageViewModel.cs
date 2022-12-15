namespace MyWallet.Client.ViewModels;

public class SettingsPageViewModel : ViewModelBase
{
    private SettingsItem? _selectedItem;
    public List<SettingsItem> Items { get; }

    public SettingsItem? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (value is null)
            {
                return;
            }
            _selectedItem = value;
            OnPropertyChanged();
            NavigationService.GoToAsync(value.Link);
        }
    }

    public SettingsPageViewModel(IDialogService dialogService, INavigationService navigationService) : base(dialogService, navigationService)
    {
        Title = "Настройки";
        Items = SettingsItem.GetItems();
        OneTimeInitialized = false;
    }

    public override Task InitializeAsync()
    {
        _selectedItem = null;
        OnPropertyChanged(nameof(SelectedItem));
        return base.InitializeAsync();
    }
}
