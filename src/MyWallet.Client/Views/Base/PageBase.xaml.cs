namespace MyWallet.Client.Views.Base;

public partial class PageBase : ContentPage
{
    private readonly INavigationService _navigationService;

    public IList<IView> PageContent => PageContentGrid.Children;
    public IList<IView> PageIcons => PageIconsGrid.Children;

    #region Bindable properties

    public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(
        nameof(PageTitle),
        typeof(string),
        typeof(PageBase),
        string.Empty,
        defaultBindingMode:
        BindingMode.OneWay,
        propertyChanged: OnPageTitleChanged);

    public string PageTitle
    {
        get => (string)GetValue(PageTitleProperty);
        set => SetValue(PageTitleProperty, value);
    }

    private static void OnPageTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PageBase basePage)
        {
            basePage.TitleLabel.Text = (string)newValue;
            basePage.TitleLabel.IsVisible = true;
        }
    }


    public static readonly BindableProperty PageModeProperty = BindableProperty.Create(
        nameof(PageMode),
        typeof(PageMode),
        typeof(PageBase),
        PageMode.None,
        propertyChanged: OnPageModePropertyChanged);

    public PageMode PageMode
    {
        get => (PageMode)GetValue(PageModeProperty);
        set => SetValue(PageModeProperty, value);
    }

    private static void OnPageModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PageBase basePage)
        {
            basePage.SetPageMode((PageMode)newValue);
        }
    }

    private void SetPageMode(PageMode pageMode)
    {
        switch (pageMode)
        {
            case PageMode.Menu:
                HamburgerButton.IsVisible = true;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = false;
                break;
            case PageMode.Navigate:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = true;
                CloseButton.IsVisible = false;
                break;
            case PageMode.Modal:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = true;
                break;
            default:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = false;
                break;
        }
    }


    public static readonly BindableProperty DisplayModeProperty = BindableProperty.Create(
        nameof(DisplayMode),
        typeof(DisplayMode),
        typeof(PageBase),
        DisplayMode.NoNavigationBar,
        propertyChanged: OnContentDisplayModePropertyChanged);

    public DisplayMode DisplayMode
    {
        get => (DisplayMode)GetValue(DisplayModeProperty);
        set => SetValue(DisplayModeProperty, value);
    }

    private static void OnContentDisplayModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PageBase basePage)
        {
            basePage.SetContentDisplayMode((DisplayMode)newValue);
        }
    }

    private void SetContentDisplayMode(DisplayMode displayMode)
    {
        switch (displayMode)
        {
            case DisplayMode.NavigationBar:
                HeaderGrid.IsVisible = true;
                Grid.SetRow(PageContentGrid, 1);
                Grid.SetRowSpan(PageContentGrid, 1);
                break;
            case DisplayMode.NoNavigationBar:
                HeaderGrid.IsVisible = false;
                Grid.SetRow(PageContentGrid, 0);
                Grid.SetRowSpan(PageContentGrid, 2);
                break;
        }
    }

    #endregion

    public PageBase()
    {
        InitializeComponent();

        NavigationPage.SetBackButtonTitle(this, string.Empty);
        NavigationPage.SetHasNavigationBar(this, false);

        SetPageMode(PageMode.None);
        SetContentDisplayMode(DisplayMode.NoNavigationBar);

        _navigationService = ServiceHelpers.GetService<INavigationService>();
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        
        // скрываем меню
        // в OnAppearing() Width еще неизвестна
        MenuGrid.TranslationX = -Window.Width;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // скрываем меню
        ContentMaskGrid.IsVisible = false;
        MenuGrid.IsVisible = false;

        // инициализируем ViewModel, если еще не инициализирована
        if (BindingContext is IViewModelBase viewModel && (!viewModel.IsInitialized || !viewModel.OneTimeInitialized))
        {
            await viewModel.InitializeAsync();
            viewModel.IsInitialized = true;
        }
    }

    protected async void MenuGrid_Tapped(object sender, TappedEventArgs e)
    {
        await CloseMenu();
    }

    protected async void HamburgerButton_Clicked(object sender, EventArgs e)
    {
        ContentMaskGrid.IsVisible = true;
        MenuGrid.IsVisible = true;
        _ = ContentMaskGrid.FadeTo(0.5, 800);
        await MenuGrid.TranslateTo(0, 0, 800, Easing.CubicOut);
    }

    protected async void OpenUserPage_Tapped(object sender, TappedEventArgs e)
    {
        _ = CloseMenu();
        await _navigationService.GoToAsync(nameof(UserPage));
    }

    private async Task CloseMenu()
    {
        _ = ContentMaskGrid.FadeTo(0, 800);
        await MenuGrid.TranslateTo(-Width, 0, 800, Easing.CubicOut);
        ContentMaskGrid.IsVisible = false;
        MenuGrid.IsVisible = false;
    }

    protected async void MenuItem_Settings_Tapped(object sender, TappedEventArgs e)
    {
        await _navigationService.GoToAsync(nameof(SettingsPage));
    }
}