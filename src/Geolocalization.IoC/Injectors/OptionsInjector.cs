using Geolocalization.CrossCutting.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geolocalization.IoC.Injectors
{
    public static class OptionsInjector
    {
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
        }
    }
}
