namespace MyWallet.Client.ViewModels;

public partial class RecordPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private Account? _selectedAccount;

    private Category? _category;

    [ObservableProperty]
    private BaseCategory? _selectedSubCategory;

    [ObservableProperty]
    private decimal _value;

    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private DateTime _recordDate = DateTime.Now;

    [ObservableProperty]
    private TimeSpan _recordTime = DateTime.Now.TimeOfDay;

    public Category? SelectedCategory
    {
        get => _category;
        set
        {
           if(value is not null && SetProperty(ref _category, value))
            {
                SubCategories.AddRange(value.Subcategories, true);
                SelectedSubCategory = SubCategories.FirstOrDefault();
            }
        }
    }

    public ObservableCollection<Account> Accounts { get; } = new();
    public ObservableCollection<Category> Categories { get; } = new();
    public ObservableCollection<BaseCategory> SubCategories { get; } = new();

    public RecordPageViewModel(IDataService dataService, IDialogService dialogService, INavigationService navigationService) : base(dialogService, navigationService)
    {
        _dataService = dataService;
        Title = "новая запись";
    }

    public override Task InitializeAsync()
    {
        return IsBusyFor(async () =>
        {
            var accounts = await _dataService.GetAccountsAsync();
            Accounts.AddRange(accounts.Item, true);
            SelectedAccount = Accounts.FirstOrDefault();

            var categories = await _dataService.GetAllCategoriesAsync();
            Categories.AddRange(categories.Item, true);
            SelectedCategory = Categories.FirstOrDefault();
        });
    }

    [RelayCommand]
    private Task SaveAndReturn() => IsBusyFor(async () =>
    {
        if(SelectedAccount is not null && SelectedSubCategory is not null)
        {
            var dateTime = new DateTime(RecordDate.Year, RecordDate.Month, RecordDate.Day, RecordTime.Hours, RecordTime.Minutes, 0);

            var record = new RecordCreate
            {
                DateTime = dateTime,
                Description = Description,
                SubcategoryId = SelectedSubCategory.Id,
                AccountId= SelectedAccount.Id,
                Value = Value
            };
            var result = await _dataService.CreateRecordAsync(record);
            if (result.State == State.Success)
            {
                await NavigationService.GoBackAsync();
            }
        }     
    });
}