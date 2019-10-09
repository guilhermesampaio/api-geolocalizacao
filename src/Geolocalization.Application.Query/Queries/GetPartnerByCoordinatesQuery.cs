using Geolocalization.Domain.Entities;
using MediatR;

namespace Geolocalization.Application.Query.Queries
{
    public class GetPartnerByCoordinatesQuery : IRequest<Partner>
    {
        public GetPartnerByCoordinatesQuery(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; }
        public double Longitude { get; }
    }
}
