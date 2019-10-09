using Geolocalization.Domain.Entities;
using MediatR;

namespace Geolocalization.Application.Query.Queries
{
    public class GetPartnerByIdQuery : IRequest<Partner>
    {
        public GetPartnerByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
