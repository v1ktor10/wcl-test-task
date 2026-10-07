using Microsoft.Extensions.DependencyInjection;
using WCL.Core.Abstractions;
using WCL.Core.Services;

namespace WCL.Core;

public static class CoreServiceCollectionExtensions
{
    public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services) => services
        .AddSingleton<ISessionState, SessionState>()
        .AddSingleton<IAuthService, AuthService>();
}
