using Microsoft.Maui.Controls;
using MyWallet.Client.DataServices;

namespace MyWallet.Client;

public partial class MainPage : ContentPage
{
    public MainPage(IDataService dataService)
    {
        InitializeComponent();
    }
}