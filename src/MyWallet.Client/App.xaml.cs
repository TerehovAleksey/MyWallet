using Microsoft.Maui.Platform;

namespace MyWallet.Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //MainPage = new NavigationPage(new MainPage());
        MainPage = new NavigationPage(new EntryPage());

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