namespace MyWallet.Client.Views.Entry;

public partial class StartPage : ContentPage
{
    public StartPage(StartPageViewModel viewModel)
    {
        //TODO: ��� CarouselView: WinUI HorizontalScrollBar ����������� � ������� �������� �������� � ����
        InitializeComponent();
        BindingContext = viewModel;
    }
}