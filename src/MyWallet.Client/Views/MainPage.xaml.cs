﻿namespace MyWallet.Client.Views;

public partial class MainPage : PageBase
{
    public MainPage(MainPageViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}