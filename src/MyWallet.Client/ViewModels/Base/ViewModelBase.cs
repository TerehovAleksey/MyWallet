namespace MyWallet.Client.ViewModels.Base;

public abstract partial class ViewModelBase : ObservableObject, IViewModelBase, IDisposable
{
    private readonly SemaphoreSlim _isBusyLock = new(1, 1);
    
    private bool _disposedValue;
    
    [ObservableProperty]
    private bool _isInitialized;
    
    [ObservableProperty]
    private bool _isBusy;
    
    public IDialogService DialogService { get; }

    public INavigationService NavigationService { get; }

    public bool OneTimeInitialized { get; set; } = true;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _loadingText = string.Empty;

    [ObservableProperty]
    private bool _dataLoaded;

    [ObservableProperty]
    private bool _isErrorState;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _errorImage = string.Empty;

    protected ViewModelBase(IDialogService dialogService, INavigationService navigationService)
    {
        DialogService = dialogService;
        NavigationService = navigationService;
        IsErrorState = false;
    }
    
    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }

    public virtual Task InitializeAsync() => Task.CompletedTask;

    protected async Task IsBusyFor(Func<Task> unitOfWork)
    {
        await _isBusyLock.WaitAsync();

        try
        {
            IsBusy = true;
            LoadingText = "loading...";

            await unitOfWork();
        }
        finally
        {
            IsBusy = false;
            LoadingText = string.Empty;
            _isBusyLock.Release();
        }
    }
    
    protected async Task<bool> HandleServiceResponseErrorsAsync(IResponse response)
    {
        switch (response.State)
        {
            case State.Success:
                return true;
            case State.Error:
                IsErrorState = true;
                ErrorMessage = $"Something went wrong. If the problem persists, plz contact support at email with the error message:{Environment.NewLine}{Environment.NewLine}{response.Errors.FirstOrDefault()}";
                ErrorImage = "error.png";
                break;
            case State.Unauthorized:
                await NavigationService.GoToAsync(nameof(EntryPage));
                break;
            case State.NotFound:
                break;
            case State.NoInternet:
                IsErrorState = true;
                ErrorMessage = $"Slow or no internet connection.{Environment.NewLine}Please check you internet connection and try again.";
                ErrorImage = "nointernet.png";
                break;
        }
        return false;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _isBusyLock?.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    [RelayCommand]
    private Task NavigateBack() => NavigationService.GoBackAsync();

    [RelayCommand]
    private Task CloseModal() => throw new NotImplementedException();
}