using Geolocalization.Application.Command.Commands;
using Geolocalization.Application.Query.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Geolocalization.IoC.Injectors
{
    public static class MediatrInjector
    {
        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(CreatePartnerCommand).Assembly, typeof(GetPartnerByIdQuery).Assembly);

        }

    }
}
