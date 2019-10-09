using Geolocalization.Application.Query.Queries;
using Geolocalization.Domain.Entities;
using Geolocalization.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalization.Application.Query.Handlers
{
    public class GetPartnerByCoordinatesQueryHandler : IRequestHandler<GetPartnerByCoordinatesQuery, Partner>
    {
        private readonly IPartnersRepository _repository;

        public GetPartnerByCoordinatesQueryHandler(IPartnersRepository repository)
        {
            _repository = repository;
        }
        public async Task<Partner> Handle(GetPartnerByCoordinatesQuery request, CancellationToken cancellationToken)
        {
            var partner = await _repository.GetByCoordinates(request.Latitude, request.Longitude);

            return partner;
        }
    }
}
