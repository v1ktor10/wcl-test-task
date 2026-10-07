using Microsoft.Extensions.DependencyInjection;

namespace WCL.App;

public static class AppServiceCollectionExtensions
{
    public static IServiceCollection RegisterAppDependencies(this IServiceCollection services) => services;
}
