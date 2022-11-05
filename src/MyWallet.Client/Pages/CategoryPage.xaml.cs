using MyWallet.Client.DataServices;
using MyWallet.Client.Models;
using System.Diagnostics;

namespace MyWallet.Client.Pages;

public partial class CategoryPage : ContentPage
{
    private readonly IDataService _dataService;
    private List<Category> categories;

    public CategoryPage(IDataService dataService)
    {
        InitializeComponent();
        _dataService = dataService;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Loader.IsRunning = true;
        categories = await _dataService.GetAllCategoriesAsync();
        TableSection.Clear();
        foreach (var category in categories)
        {
            if (string.IsNullOrEmpty(category.IconName))
            {
                category.IconName = "cogs.png";
            }

            var cell = new ImageCell
            {
                Text = category.Name,
                ImageSource = "cogs.png"
            };
            cell.Tapped += Cell_Tapped;
            TableSection.Add(cell);
        }
        Loader.IsRunning = false;
    }

    private async void Cell_Tapped(object sender, EventArgs e)
    {
        var name = (sender as ImageCell).Text;
        var category = categories.Find(x => x.Name == name);

        var navigationParameters = new Dictionary<string, object>
        {
            {nameof(Category), category}
        };
        await Shell.Current.GoToAsync(nameof(CategoryEditPage), navigationParameters);
    }

    private async void fabBtn_Clicked(object sender, EventArgs e)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {nameof(Category), new Category()}
        };
        await Shell.Current.GoToAsync(nameof(CategoryEditPage), navigationParameters);
    }
}