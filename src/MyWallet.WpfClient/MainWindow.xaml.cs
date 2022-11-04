using MyWallet.WpfClient.ViewModels;
using System.Windows;

namespace MyWallet.WpfClient;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
