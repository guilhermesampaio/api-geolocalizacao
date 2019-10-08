using Geolocalization.Application.Query.Queries;
using Geolocalization.Domain.Entities;
using Geolocalization.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalization.Application.Query.Handlers
{
    public class GetPartnerByIdQueryHandler : IRequestHandler<GetPartnerByIdQuery, Partner>
    {
        private readonly IPartnersRepository _repository;

        public GetPartnerByIdQueryHandler(IPartnersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Partner> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var partner = await _repository.Get(request.Id);
            return partner;
        }
    }
}
