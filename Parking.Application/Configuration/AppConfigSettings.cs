using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Parking.Application.Configuration;

public static class AppConfigSettings
{
    public static IServiceCollection AppConfigSettingsServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ApplicationKeys>(x =>  config.GetSection("ApplicationKeys").Bind(x));
        return services;
    }
}
