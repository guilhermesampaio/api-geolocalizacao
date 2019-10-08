using Geolocalization.Domain.Entities;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Geolocalization.Infra.Data
{
    internal class PartnerMongoDb
    {
        public int Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public GeoJsonMultiPolygon<GeoJson2DGeographicCoordinates> CoverageArea { get; set; }
        public Point Address { get; set; }

    }
    
}
