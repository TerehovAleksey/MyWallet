using Microsoft.Maui.Animations;
using MenuItem = MyWallet.Client.UI.MenuItem;

namespace MyWallet.Client.ViewModels.MenuPages.Settings;

[QueryProperty(nameof(CategoryName), "name")]
[QueryProperty(nameof(IsCategory), "isCategory")]
public partial class CategoryPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private BaseCategory? _category;

    [ObservableProperty]
    private bool _isCategory;
    
    public string? CategoryName { get; set; }
    public ObservableCollection<MenuItem> MenuItems { get; } = new();
    
    public CategoryPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
    }

    public override Task InitializeAsync() => IsBusyFor(async () =>
    {
        Title = CategoryName ?? string.Empty;
        var categories = await _dataService.Categories.GetAllCategoriesAsync().ConfigureAwait(false);
        if (IsCategory)
        {
            Category = categories.First(c => c.Name == CategoryName);
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
            Category = categories.SelectMany(c => c.Subcategories).First(c=>c.Name == CategoryName);
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
    private Task EditName() => AppService.Dialog.ShowInDevelopmentMessage();

    [RelayCommand]
    private Task EditNature() => AppService.Dialog.ShowInDevelopmentMessage();

    [RelayCommand]
    private Task SaveAndReturn() => AppService.Dialog.ShowInDevelopmentMessage();
}