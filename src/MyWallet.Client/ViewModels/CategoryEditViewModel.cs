using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyWallet.Client.DataServices;
using MyWallet.Client.Models;

namespace MyWallet.Client.ViewModels;

public partial class CategoryEditViewModel : ObservableObject
{
    private readonly IDataService _dataService;
    private bool _isNew;

    [ObservableProperty]
    private string title = "Редактирование категорий";

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private Category _currentCategory;

    public CategoryEditViewModel(IDataService dataService)
	{
        _dataService = dataService;
    }

    public void SetCategory(Category category)
    {
        CurrentCategory = category;
       // Name = category.Name;
        _isNew = category.Id == Guid.Empty;
    }

    public async Task<List<Category>> LoadSubGroups()
    {
        if (_isNew)
        {
            return new List<Category>();
        }

        return new List<Category>
        {
            new Category
            {
               Id = Guid.NewGuid(),
               Name = "Дальние поездки",
               IconName = "cogs.png"
            },
            new Category
            {
               Id = Guid.NewGuid(),
               Name = "Деловые поездки",
               IconName = "cogs.png"
            },
            new Category
            {
               Id = Guid.NewGuid(),
               Name = "Общественный транспорт",
               IconName = "cogs.png"
            },
            new Category
            {
               Id = Guid.NewGuid(),
               Name = "Такси",
               IconName = "cogs.png"
            }
        };
    }

    [RelayCommand]
    private async Task ChangeName()
    {
        var title = _isNew ? "Новая категория" : CurrentCategory.Name;
        var message = _isNew ? "Введите название" : "Измените название";
        string name = await Shell.Current.DisplayPromptAsync(title, message, placeholder: "Название", keyboard: Keyboard.Text, maxLength: 10);
        if (!string.IsNullOrEmpty(name))
        {
            if (_isNew)
            {
               // await _dataService.CreateCategoryAsync(name, null);
            }
            else
            {
                //await _dataService.Update(name, null);
            }
            CurrentCategory.Name = name;
            Name = name;
        }
    }

    [RelayCommand]
    private async void DeleteItem()
    {
        var result = await Shell.Current.DisplayAlert("Категория", "Удалить категорию?", "Да", "Нет");
        if (result)
        {
            //TODO
        }
    }
}
