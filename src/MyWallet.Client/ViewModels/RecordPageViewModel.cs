using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyWallet.Client.DataServices;
using MyWallet.Client.Models;

namespace MyWallet.Client.ViewModels;

public partial class RecordPageViewModel : ObservableObject
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private bool _isIncome = true;

    [ObservableProperty]
    private string _value = string.Empty;

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private TimeSpan _time;

    public ObservableCollection<Category> Categories { get; set; } = new();
    public ObservableCollection<BaseCategory> Subcategories { get; set; } = new();

    private Category? _selectedCategory;
    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory != value)
            {
                SetProperty(ref _selectedCategory, value);
                if (value is not null)
                {
                    LoadSubcategories();
                }
            }
        }
    }

    [ObservableProperty]
    private BaseCategory? _selectedSubcategory;

    [ObservableProperty]
    private string? _description;

    public RecordPageViewModel(IDataService dataService)
    {
        _dataService = dataService;
        Date = DateTime.Now;
        Time = new TimeSpan(Date.Hour, Date.Minute, Date.Second);
    }

    public async Task LoadCategoriesAsync()
    {
        var categories = await _dataService.GetAllCategoriesAsync();
        foreach (var item in categories)
        {
            Categories.Add(item);
        }
    }

    private void LoadSubcategories()
    {
        Subcategories.Clear();
        var sub = SelectedCategory?.Subcategories;
        if (sub is not null)
        {
            foreach (var item in sub)
            {
                Subcategories.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task SaveRecord()
    {
        var record = new RecordCreate
        {
            Value = decimal.Parse(_value.Replace('.',',')),
            DateTime = new DateTime(_date.Year, _date.Month, _date.Day, _time.Hours, _time.Minutes, 0),
            Description = _description,
            SubcategoryId = _selectedSubcategory?.Id ?? Guid.Empty,
        };

        var result = await _dataService.CreateRecordAsync(record);

        if (result is not null)
        {
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", "Запись не создана!", "Закрыть");
        }
    }
}
