using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyWallet.Client.DataServices;
using MyWallet.Client.Models;
using MyWallet.Client.Pages;
using System.Collections.ObjectModel;

namespace MyWallet.Client.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<Record>? _records;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private decimal _totalSum;

    public MainViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task LoadRecordsAsync()
    {
        IsLoading = true;
        Records?.Clear();
        TotalSum = 0;
        var records = await _dataService.GetRecordsAsync(new DateTime(2022, 01, 01), DateTime.Today.AddDays(1));
        foreach (var item in records)
        {
            item.Currency = "BYN";
            item.CurrencyType = "Наличные";
        }
        Records = new(records);
        TotalSum = Records.Where(x => x.IsIncome).Sum(x => x.Value) - Records.Where(x => !x.IsIncome).Sum(x => x.Value);
        IsLoading = false;
    }

    [RelayCommand]
    private async void AddRecord()
    {
        await Shell.Current.GoToAsync(nameof(RecordPage));
    }
}
