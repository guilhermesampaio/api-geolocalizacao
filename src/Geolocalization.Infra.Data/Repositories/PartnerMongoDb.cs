using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Geolocalization.Infra.Data
{
    internal class PartnerMongoDb
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public GeoJsonMultiPolygon<GeoJson2DGeographicCoordinates> CoverageArea { get; set; }
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Address { get; set; }
    }
}
