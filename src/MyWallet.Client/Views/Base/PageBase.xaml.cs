namespace MyWallet.Client.Views.Base;

public partial class PageBase : ContentPage
{
    public IList<IView> PageContent => PageContentGrid.Children;
    public IList<IView> PageIcons => PageIconsGrid.Children;

    protected bool IsBackButtonEnabled
    {
        set => NavigateBackButton.IsEnabled = value;
    }

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
                Grid.SetRow(PageContentGrid, 2);
                Grid.SetRowSpan(PageContentGrid, 1);
                break;
            case DisplayMode.NoNavigationBar:
                HeaderGrid.IsVisible = false;
                Grid.SetRow(PageContentGrid, 1);
                Grid.SetRowSpan(PageContentGrid, 2);
                break;
        }
    }

    #endregion


    public PageBase()
    {
        InitializeComponent();

        //Hide the Xamarin Forms build in navigation header
        NavigationPage.SetHasNavigationBar(this, false);

        //Set Page Mode
        SetPageMode(PageMode.None);

        //Set Display Mode
        SetContentDisplayMode(DisplayMode.NoNavigationBar);
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

#if ANDROID
        // только так работает цвет стаусбара (в других случаях падает в релизе)
        // https://github.com/MicrosoftDocs/CommunityToolkit/blob/main/docs/maui/behaviors/statusbar-behavior.md
        CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(Color.FromArgb("#21cb87"));
        CommunityToolkit.Maui.Core.Platform.StatusBar.SetStyle(StatusBarStyle.LightContent);
#endif
    }
}