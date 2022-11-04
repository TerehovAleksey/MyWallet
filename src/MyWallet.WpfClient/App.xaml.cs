using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWallet.WpfClient.Http;

namespace MyWallet.WpfClient;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient<MyWalletHttpClient>(c => 
                {
                    c.BaseAddress = new Uri("https://mywalletapi.azurewebsites.net/api/");
                });

                services.AddSingleton<MainWindow>();

                // так будет открываться только одно дочернее окно
                //services.AddTransient<ChildWindow>();

                // если нужно несколько, то через фабрику
                //services.AddFormFactory<ChildWindow>();

                //services.AddTransient<IDataAccess, DataAccess>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();

        base.OnExit(e);
    }
}
