using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using WCL.Api;
using WCL.App;
using WCL.Core;
using AvaloniaApp = WCL.App.App;

namespace WCL.Desktop;

internal static class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        using var services = new ServiceCollection()
            .RegisterCoreDependencies()
            .RegisterApiDependencies()
            .RegisterAppDependencies()
            .BuildServiceProvider();

        return BuildAvaloniaApp(services).StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp(IServiceProvider services) =>
        AppBuilder.Configure(() => new AvaloniaApp(services))
            .UsePlatformDetect()
            .LogToTrace();
}
