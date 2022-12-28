using MenuItem = MyWallet.Client.UI.MenuItem;

namespace MyWallet.Client.ViewModels.MenuPages.Settings;

public partial class CategoriesPageViewModel : ViewModelBase
{
    private readonly IDataService _dataService;

    public ObservableCollection<MenuItem> MenuItems { get; } = new();

    public CategoriesPageViewModel(IAppService appService, IDataService dataService) : base(appService)
    {
        _dataService = dataService;
        OneTimeInitialized = false;
        Title = Strings.EditCategory;
    }

    public override Task InitializeAsync() => IsBusyFor(async () =>
    {
        var categories = await _dataService.Categories.GetAllCategoriesAsync().ConfigureAwait(false);
        var menuItems = categories
            .Select(c => new MenuItem
            {
                Title = c.Name,
                Icon = c.ImageName ?? string.Empty,
                Color = c.Color,
                Link = $"category?name={c.Name}&isCategory=true",
                HasSeparator = true,
            });
        MenuItems.AddRange(menuItems, true);
    });
    
    [RelayCommand]
    private async Task Navigate(string link)
    {
        if (!string.IsNullOrEmpty(link))
        {
            await AppService.Navigation.GoToAsync(link);
        }
    }
}