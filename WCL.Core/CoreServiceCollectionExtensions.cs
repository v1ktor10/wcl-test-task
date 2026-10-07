using Microsoft.Extensions.DependencyInjection;
using WCL.Core.Abstractions;
using WCL.Core.Services;

namespace WCL.Core;

public static class CoreServiceCollectionExtensions
{
    public static IServiceCollection AddWclCore(this IServiceCollection services) =>
        services.AddSingleton<Session>().AddSingleton<ISessionState>(sp => sp.GetRequiredService<Session>())
            .AddSingleton<ISessionStore>(sp => sp.GetRequiredService<Session>())
            .AddSingleton<IAuthService, AuthService>();
}
