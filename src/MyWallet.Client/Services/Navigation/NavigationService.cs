﻿using System.Diagnostics;

namespace MyWallet.Client.Services.Navigation;

public class NavigationService : INavigationService
{
    public Task GoBackAsync() => Shell.Current.GoToAsync("..");

    public Task GoToAsync(string route, IDictionary<string, object>? routeParameters = null)
    {
        var shellNavigation = new ShellNavigationState(route);

#if DEBUG
        Debug.WriteLine($"---> Current: {Shell.Current.CurrentState.Location}; GoTo: {shellNavigation.Location}");
#endif
        
        if (shellNavigation.Location == Shell.Current.CurrentState.Location)
        {
            return Task.CompletedTask;
        }

        return routeParameters != null ? Shell.Current.GoToAsync(route, routeParameters) : Shell.Current.GoToAsync(route);
    }

    public bool CanGoBack => Shell.Current.Navigation.NavigationStack.Count > 1;
    public string Current => string.Empty;
}