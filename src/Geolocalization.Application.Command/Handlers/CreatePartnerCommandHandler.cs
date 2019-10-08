using Geolocalization.Application.Command.Commands;
using Geolocalization.Domain.Entities;
using Geolocalization.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalization.Application.Command.Handlers
{
    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand>
    {
        private readonly IPartnersRepository _repository;

        public CreatePartnerCommandHandler(IPartnersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var coverageArea = new Domain.Entities.MultiPolygon(request.CoverageArea.Coordinates);
            var address = new Domain.Entities.Point(request.Address.Coordinates);

            var entity = new Partner(request.Id, 
                request.TradingName, 
                request.OwnerName, 
                request.Document,
                coverageArea,
                address);


            await _repository.Create(entity);

            return Unit.Value;
        }
    }
}
