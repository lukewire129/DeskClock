using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.DeskApp.Services;
using Avalonia.DeskApp.ViewModels;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Avalonia.DeskApp;

public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices ();
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    public new static App Current => (App)Application.Current;

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
    public IServiceProvider Services { get; }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection ();

        services.AddSingleton<IWeatherService, TestWeatherService> ();
        services.AddTransient<MainViewModel> ();

        return services.BuildServiceProvider ();
    }
}