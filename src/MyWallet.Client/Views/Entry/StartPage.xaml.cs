namespace MyWallet.Client.Views.Entry;

public partial class StartPage : ContentPage
{
    public StartPage(StartPageViewModel viewModel)
    {
        //TODO: Баг CarouselView: WinUI HorizontalScrollBar добавляется и залазят соседние элементы в окно
        InitializeComponent();
        BindingContext = viewModel;
    }
}