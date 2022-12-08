﻿namespace MyWallet.Client.Services.Navigation;

public interface INavigationService
{
    public Task GoToAsync(string route, IDictionary<string, object>? routeParameters = null);
    public Task GoBackAsync();
}