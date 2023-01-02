using MenuItem = MyWallet.Client.UI.MenuItem;

namespace MyWallet.Client.ViewModels.MenuPages.Settings;

[QueryProperty(nameof(CategoryName), "name")]
[QueryProperty(nameof(IsCategory), "isCategory")]
public partial class CategoryPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    private IEnumerable<CategoryType>? _categoryTypes;

    [ObservableProperty]
    private BaseCategory? _category;

    [ObservableProperty]
    private bool _isCategory;

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set
        {
            if (SetProperty(ref _name, value) && Category is not null)
            {
                Category.Name = value;
            }
        }
    }

    public string? CategoryName { get; set; }
    public ObservableCollection<MenuItem> MenuItems { get; } = new();

    public CategoryPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
    }

    public override Task InitializeAsync() => IsBusyFor(async () =>
    {
        Title = CategoryName ?? string.Empty;
        _categoryTypes = await _dataService.Categories.GetAllCategoryTypes();
        var categories = await _dataService.Categories.GetAllCategoriesAsync().ConfigureAwait(false);
        if (IsCategory)
        {
            Category = categories.First(c => c.Name == CategoryName);
            Name = Category.Name;

            var menuItems = categories.First(c => c.Name == CategoryName).Subcategories
                .Select(c => new MenuItem
                {
                    Title = c.Name,
                    Icon = c.ImageName ?? string.Empty,
                    Color = c.Color,
                    Link = $"category?name={c.Name}",
                    HasSeparator = true,
                });
            MenuItems.AddRange(menuItems, true);
        }
        else
        {
            Category = categories.SelectMany(c => c.Subcategories).First(c => c.Name == CategoryName);
        }
    });

    [RelayCommand]
    private async Task Navigate(string link)
    {
        if (!string.IsNullOrEmpty(link))
        {
            await AppService.Navigation.GoToAsync(link);
        }
    }

    [RelayCommand]
    private Task EditImage() => AppService.Dialog.ShowInDevelopmentMessage();

    [RelayCommand]
    private async Task EditName()
    {
        var (success, value) = await AppService.Dialog.ShowInputTextAsync(Strings.EditCategory, Strings.Name, Category?.Name ?? string.Empty);
        if (success && Category is not null)
        {
            Name = value;
        }
    }

    [RelayCommand]
    private async Task EditNature()
    {
        var values = _categoryTypes.Select(x => x.Name).ToArray();
        var selected = _categoryTypes.First().Name;
        var result = await AppService.Dialog.ShowRadioInputAsync("Type", values, selected);
        if (result.Sucsess)
        {

        }
    }

    [RelayCommand]
    private Task SaveAndReturn() => AppService.Dialog.ShowInDevelopmentMessage();
}