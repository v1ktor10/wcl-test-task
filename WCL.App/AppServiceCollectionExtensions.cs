using Microsoft.Extensions.DependencyInjection;
using WCL.App.Services;
using WCL.App.Views;
using WCL.App.Views.About;
using WCL.App.Views.Main;
using WCL.App.Views.Profile;

namespace WCL.App;

public static class AppServiceCollectionExtensions
{
    public static IServiceCollection RegisterAppDependencies(this IServiceCollection services)
    {
        services.AddSingleton<SectionViewModel, ProfileViewModel>();
        services.AddSingleton<SectionViewModel, AboutViewModel>();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<MainViewModel>();
        return services;
    }
}
