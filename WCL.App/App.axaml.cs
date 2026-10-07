using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using WCL.App.Views.Main;
using WCL.App.Windows.MainWindow;

namespace WCL.App;

public class App(IServiceProvider services) : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow { DataContext = services.GetRequiredService<MainViewModel>() };

        base.OnFrameworkInitializationCompleted();
    }
}
