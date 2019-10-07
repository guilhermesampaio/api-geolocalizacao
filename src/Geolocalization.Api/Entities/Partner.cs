using System.Collections.Generic;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Geolocalization.Api.Entities
{
    public class Partner
    {
        public int Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public MultiPolygon CoverageArea { get; set; }
        public Point Address { get; set; }

    }

    public abstract class GeoPosition
    {
        public string Type { get; set; }
    }

    public class MultiPolygon : GeoPosition
    {
        public IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> Coordinates { get; set; }
    }

    public class Point : GeoPosition
    {
        public IEnumerable<double> Coordinates { get; set; }
    }

    public class PartnerMongoDb
    {
        public int Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public GeoJsonMultiPolygon<GeoJson2DGeographicCoordinates> CoverageArea { get; set; }
        public Point Address { get; set; }

    }
}

