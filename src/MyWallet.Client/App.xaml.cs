﻿namespace MyWallet.Client;

public partial class App : Application
{
    private readonly IBarrel _cache;

    public App(IBarrel cache)
    {
        _cache = cache;

        InitializeComponent();
        InitApp();
        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        base.OnStart();
        OnResume();
    }

    protected override void OnResume()
    {
        base.OnResume();
        SetAppTheme(Current!.RequestedTheme);

        // на MAUI 7 в Android не работает
        //https://github.com/dotnet/maui/pull/11200
        RequestedThemeChanged += App_RequestedThemeChanged;
    }

    protected override void OnSleep()
    {
        base.OnSleep();
        SetAppTheme(Current!.RequestedTheme);
        RequestedThemeChanged -= App_RequestedThemeChanged;
    }

    private void App_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        Dispatcher.Dispatch(() => SetAppTheme(e.RequestedTheme));
    }

    private static void SetAppTheme(AppTheme theme)
    {
        ICollection<ResourceDictionary> mergedDictionaries = Current!.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            switch (theme)
            {
                case AppTheme.Dark:
                    var lightTheme = ResourceHelper.GetLightThemeDictionary();
                    if (lightTheme is not null)
                    {
                        mergedDictionaries.Remove(lightTheme);
                    }
                    mergedDictionaries.Add(new DarkTheme());
                    break;
                default:
                    var darkTheme = ResourceHelper.GetDarkThemeDictionary();
                    if (darkTheme is not null)
                    {
                        mergedDictionaries.Remove(darkTheme);
                    }
                    mergedDictionaries.Add(new LightTheme());
                    break;
            }
        }

#if ANDROID
        // только так работает цвет стаусбара (в других случаях падает в релизе)
        // https://github.com/MicrosoftDocs/CommunityToolkit/blob/main/docs/maui/behaviors/statusbar-behavior.md
        var color = ResourceHelper.GetColor("StatusBarColor");
        CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(color);
        CommunityToolkit.Maui.Core.Platform.StatusBar.SetStyle(StatusBarStyle.LightContent);
#endif
    }

    private void InitApp()
    {
        //_cache.EmptyAll();

#if DEBUG
        //очистка кеша, если изменился источник данных
        var source = _cache.Get<string>("ApiServiceUrl");
        if (string.IsNullOrEmpty(source) || source != Constants.ApiServiceUrl)
        {
            _cache.EmptyAll();
        }

        _cache.Add("ApiServiceUrl", Constants.ApiServiceUrl, TimeSpan.MaxValue);
#endif
    }
}