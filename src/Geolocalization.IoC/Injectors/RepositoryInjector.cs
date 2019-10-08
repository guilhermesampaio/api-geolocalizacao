using Geolocalization.Domain.Repositories;
using Geolocalization.Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Geolocalization.IoC.Injectors
{
    public static class RepositoryInjector
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IPartnersRepository, PartnersRepository>();

        }
    }
}
