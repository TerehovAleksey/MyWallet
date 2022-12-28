namespace MyWallet.Client.ViewModels.Base;

public abstract partial class ViewModelBase : ObservableObject, IViewModelBase, IDisposable
{
    private readonly SemaphoreSlim _isBusyLock = new(1, 1);

    private bool _disposedValue;

    [ObservableProperty]
    private bool _isInitialized;

    [ObservableProperty]
    private bool _isBusy;

    protected IAppService AppService { get; }
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

    protected ViewModelBase(IAppService appService)
    {
        AppService = appService;
        DialogService = appService.Dialog;
        NavigationService = appService.Navigation;
        IsErrorState = false;
        LoadingText = Strings.Loading;
    }

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }

    public virtual Task InitializeAsync() => Task.CompletedTask;

    public virtual Task Reload()
    {
        IsErrorState = false;
        return Task.CompletedTask;
    }

    protected async Task IsBusyFor(Func<Task> unitOfWork)
    {
        await _isBusyLock.WaitAsync();

        try
        {
            IsErrorState = false;
            IsBusy = true;

            await unitOfWork();
        }
        catch (InternetConnectionException)
        {
            IsErrorState = true;
            ErrorMessage = Strings.NoInternetMessagePart1 + Environment.NewLine + Strings.NoInternetMessagePart2;
            ErrorImage = "nointernet.png";
        }
        catch (UnauthorizedAccessException)
        {
            AppService.SetAppState(false);
        }
        // серверная валидация
        catch (ServerResponseException ex)
        {
            IsErrorState = true;
            ErrorMessage = $"{Strings.ErrorMessagePart1} {Constants.Email} {Strings.ErrorMessagePart2}{Environment.NewLine}{Environment.NewLine}{ex.Message}";
            ErrorImage = "error.png";
        }
        // сервер недоступен
        catch (HttpRequestException ex)
        {
            IsErrorState = true;
            ErrorMessage = $"{Strings.ErrorMessagePart1} {Constants.Email} {Strings.ErrorMessagePart2}{Environment.NewLine}{Environment.NewLine}{ex.Message}";
            ErrorImage = "error.png";
        }
        // в Android, если localhost, то сюда попадаем и ex = null
        catch (Exception ex)
        {
            IsErrorState = true;
            ErrorMessage = $"{Strings.ErrorMessagePart1} {Constants.Email} {Strings.ErrorMessagePart2}{Environment.NewLine}{Environment.NewLine}{ex.Message}";
            ErrorImage = "error.png";
        }
        finally
        {
            IsBusy = false;
            LoadingText = string.Empty;
            _isBusyLock.Release();
        }
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
    private async Task NavigateBack()
    {
        if (NavigationService.CanGoBack && NavigationService.Current != nameof(MainPage))
        {
            await NavigationService.GoBackAsync();
        }
        else
        {
            await NavigationService.GoToAsync("//home");
        }
    }

    [RelayCommand]
    private Task CloseModal() => throw new NotImplementedException();
}