namespace MyWallet.Client.ViewModels.Base;

public interface IViewModelBase : IQueryAttributable
{
    public IDialogService DialogService { get; }
    public INavigationService NavigationService { get; }

    /// <summary>
    /// Инициализация будет происходить только первый раз при отображении
    /// или каждый раз 
    /// </summary>
    public bool OneTimeInitialized { get; set; }

    /// <summary>
    /// Признак того, что идёт получение данных
    /// </summary>
    public bool IsBusy { get; }

    /// <summary>
    /// Признак, что инициализация была выполнена
    /// </summary>
    public bool IsInitialized { get; set; }

    Task InitializeAsync();

    Task Reload();
}