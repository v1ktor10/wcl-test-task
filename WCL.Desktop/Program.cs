using Microsoft.Extensions.DependencyInjection;
using WCL.Api;
using WCL.App;
using WCL.Core;

using var services = new ServiceCollection()
    .RegisterCoreDependencies()
    .RegisterApiDependencies()
    .RegisterAppDependencies()
    .BuildServiceProvider();