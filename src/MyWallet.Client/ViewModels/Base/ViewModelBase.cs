namespace MyWallet.Client.ViewModels.Base;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _isBusy = false;

    [ObservableProperty]
    private string _loadingText = string.Empty;

    [ObservableProperty]
    private bool _dataLoaded = false;

    [ObservableProperty]
    private bool _isErrorState = false;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _errorImage = string.Empty;

    protected ViewModelBase() => IsErrorState = false;

    //Called on Page Appearing
    public virtual async void OnNavigatedTo(object? parameters) => await Task.CompletedTask;

    //Set Loading Indicators for Page
    protected void SetDataLoadingIndicators(bool isStaring = true)
    {
        if (isStaring)
        {
            IsBusy = true;
            DataLoaded = false;
            IsErrorState = false;
            ErrorMessage = string.Empty;
            ErrorImage = string.Empty;
        }
        else
        {
            LoadingText = string.Empty;
            IsBusy = false;
        }
    }
}
