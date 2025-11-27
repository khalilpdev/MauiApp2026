using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MauiApp2026;

public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddLogging(config => 
        {
            config.AddDebug()
                  .SetMinimumLevel(LogLevel.Debug);
        });
        
        // Add repositories
        services.AddSingleton<ProjectRepository>();
        services.AddSingleton<TaskRepository>();
        services.AddSingleton<CategoryRepository>();
        services.AddSingleton<TagRepository>();
        services.AddSingleton<SeedDataService>();
        services.AddSingleton<ModalErrorHandler>();

        // Add view models
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<ProjectsViewModel>();
        services.AddSingleton<ManageMetaViewModel>();
        services.AddSingleton<MainWindowViewModel>();
    }
}
