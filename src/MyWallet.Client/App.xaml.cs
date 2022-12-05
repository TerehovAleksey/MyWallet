namespace MyWallet.Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // проверяем наличие сохранённого токена и показываем начальную страницу
        var cache = ServiceHelpers.GetService<IBarrel>();
        if (cache is not null)
        {
#if DEBUG
            //очистка кеша, если изменился источник данных
            var source = cache.Get<string>("ApiServiceUrl");
            if (string.IsNullOrEmpty(source) || source != Constants.ApplicationId) 
            {
                cache.EmptyAll();
            }
            cache.Add("ApiServiceUrl", Constants.ApplicationId, TimeSpan.MaxValue);
#endif

            var token = cache.Get<string>(Constants.TOKEN_KEY);
            if (string.IsNullOrEmpty(token))
            {
                MainPage = new NavigationPage(new EntryPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        // Entry курсор можно поменять, бордер всё равно синий
        //        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        //        {
        //#if ANDROID

        //            handler.PlatformView.TextCursorDrawable.SetTint(Color.FromArgb("#21cb87").ToPlatform());

        //#elif IOS || MACCATALYST

        //#elif WINDOWS

        //#endif
        //        });
    }
}