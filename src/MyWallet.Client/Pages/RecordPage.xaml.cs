using MyWallet.Client.ViewModels;

namespace MyWallet.Client.Pages;

public partial class RecordPage : ContentPage
{
    private readonly RecordPageViewModel _vm;
    public RecordPage(RecordPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadCategoriesAsync();
    }
}