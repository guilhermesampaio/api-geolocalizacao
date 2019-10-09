using System.Threading;
using System.Threading.Tasks;
using Geolocalization.Application.Command.Commands;
using Geolocalization.Domain.Entities;
using Geolocalization.Domain.Repositories;
using MediatR;

namespace Geolocalization.Application.Command.Handlers
{
    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, Partner>
    {
        private readonly IPartnersRepository _repository;

        public CreatePartnerCommandHandler(IPartnersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Partner> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var coverageArea = new Domain.Entities.MultiPolygon(request.CoverageArea.Coordinates);
            var address = new Domain.Entities.Point(request.Address.Coordinates);

            var partner = new Partner(request.TradingName, 
                request.OwnerName, 
                request.Document,
                coverageArea,
                address);

            var partnerId = await _repository.Create(partner);

            partner.SetId(partnerId);

            return partner;
        }
    }
}
