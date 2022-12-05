﻿namespace MyWallet.Client.Views.Base;

public class ViewBase<TViewModel> : PageBase where TViewModel : AppViewModelBase
{
    private bool _isLoaded;

    protected TViewModel? ViewModel { get; set; }
    protected object? ViewModelParameters { get; set; }

    protected event EventHandler? ViewModelInitialized;

    protected ViewBase() : base()
    {
    }

    protected ViewBase(object? initParameters) : base() =>
        ViewModelParameters = initParameters;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        //Срабатывает при первом отображении страницы
        if (!_isLoaded)
        {
            BindingContext = ViewModel = ServiceHelpers.GetService<TViewModel>();

            ViewModel.NavigationService = Navigation;
            ViewModel.PageService = this;

            //Raise Event to notify that ViewModel has been Initialized
            ViewModelInitialized?.Invoke(this, EventArgs.Empty);
        }

        //Navigate to View Model's OnNavigatedTo method
        ViewModel?.OnNavigatedTo(ViewModelParameters, _isLoaded);
        
        _isLoaded = true;
    }

}
