using Microsoft.Extensions.DependencyInjection;
using WCL.Core.Abstractions;

namespace WCL.Api;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddWclApi(this IServiceCollection services, Uri baseAddress)
    {
        services.AddTransient<BearerTokenHandler>();

        services.AddHttpClient<IAuthApi, AuthApi>(client =>
            {
                client.BaseAddress = baseAddress;
                client.Timeout = TimeSpan.FromSeconds(15);
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IImageApi, ImageApi>(c => c.Timeout = TimeSpan.FromSeconds(15));

        return services;
    }
}
