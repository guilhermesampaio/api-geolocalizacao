using Geolocalization.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;
using System;

namespace Geolocalization.Infra.Data
{
    internal class PartnerMongoDb
    {
        public int Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public GeoJsonMultiPolygon<GeoJson2DGeographicCoordinates> CoverageArea { get; set; }
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Address { get; set; }

    }
    
}
