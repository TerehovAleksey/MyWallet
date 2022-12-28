namespace MyWallet.Client.ViewModels.Entry;

public partial class StartPageViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public List<StartScreenCarouselItem> Items { get; }

    public StartPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        Items = StartScreenCarousel.GetItems();
    }

    [RelayCommand]
    private Task OpenEntryPage() => _navigationService.GoToAsync(nameof(EntryPage));
}