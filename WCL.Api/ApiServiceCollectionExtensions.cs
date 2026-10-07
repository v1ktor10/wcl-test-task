using Microsoft.Extensions.DependencyInjection;
using WCL.Core.Abstractions;

namespace WCL.Api;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection RegisterApiDependencies(this IServiceCollection services, Uri baseAddress)
    {
        services.AddTransient<BearerTokenHandler>();

        services.AddHttpClient<IAuthApi, AuthApi>(client =>
            {
                client.BaseAddress = baseAddress;
                client.Timeout = TimeSpan.FromSeconds(15);
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

        return services;
    }
}
