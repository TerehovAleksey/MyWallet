using CommunityToolkit.Mvvm.ComponentModel;

namespace MyWallet.Client.Models;

public partial class Category : ObservableObject
{
    public Guid Id { get; set; }

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string iconName;

    [ObservableProperty]
    private bool isVisible;
}
