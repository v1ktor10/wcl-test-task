using Microsoft.Extensions.DependencyInjection;
using ShadUI;
using WCL.App.Services;
using WCL.App.Services.AvatarLoader;
using WCL.App.Services.Dialog;
using WCL.App.Services.Notification;
using WCL.App.Views;
using WCL.App.Views.About;
using WCL.App.Views.Auth;
using WCL.App.Views.Main;
using WCL.App.Views.Profile;
using WCL.App.Views.Register;

namespace WCL.App;

public static class AppServiceCollectionExtensions
{
    public static IServiceCollection RegisterAppDependencies(this IServiceCollection services)
    {
        // ShadUI
        services.AddSingleton<DialogManager>();
        services.AddSingleton<ToastManager>();

        services.AddSingleton<INotificationService, ShadNotificationService>();
        services.AddSingleton<IDialogService, ShadDialogService>();
        services.AddSingleton<IAvatarLoader, AvatarLoader>();

        services.AddSingleton<SectionViewModel, ProfileViewModel>();
        services.AddSingleton<SectionViewModel, AboutViewModel>();

        services.AddSingleton<AuthViewModel>();
        services.AddSingleton<MainViewModel>();

        services.AddTransient<RegisterViewModel>();

        services.AddSingleton(_ =>
        {
            var manager = new DialogManager();
            manager.Register<RegisterView, RegisterViewModel>();
            return manager;
        });

        return services;
    }
}
