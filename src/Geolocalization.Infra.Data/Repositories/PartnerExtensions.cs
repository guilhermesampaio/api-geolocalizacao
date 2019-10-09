using Geolocalization.Domain.Entities;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Geolocalization.Infra.Data
{
    public static class PartnerExtensions
    {
        internal static Partner ToPartnerEntity(this PartnerMongoDb partnerDb)
        {
            var coverageArea = GetCoverageArea(partnerDb);
            var address = GetAddress(partnerDb);

            return new Partner(partnerDb.Id.ToString(), partnerDb.TradingName, partnerDb.OwnerName, partnerDb.Document, coverageArea, address);
        }

        internal static PartnerMongoDb ToPartnerMongoDb(this Partner partner)
        {
            var polygonsCoodinates = new List<GeoJsonPolygonCoordinates<GeoJson2DGeographicCoordinates>>();

            foreach (var multipolygon in partner.CoverageArea.Coordinates)
            {
                foreach (var polygon in multipolygon)
                {
                    var points = new List<GeoJson2DGeographicCoordinates>();
                    foreach (var coordinates in polygon)
                    {
                        var coordinatesArray = coordinates.ToArray();
                        var longitude = coordinatesArray[0];
                        var latitude = coordinatesArray[1];

                        var point = new GeoJson2DGeographicCoordinates(longitude, latitude);
                        points.Add(point);
                    }

                    var linearRings = new GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>(points);
                    var polygonCoordinates = new GeoJsonPolygonCoordinates<GeoJson2DGeographicCoordinates>(linearRings);
                    polygonsCoodinates.Add(polygonCoordinates);
                }
            }

            var coverageArea = GeoJson.MultiPolygon(polygonsCoodinates.ToArray());
            var addressCoordinates = partner.Address.Coordinates.ToArray();
            var address = GeoJson.Point(new GeoJson2DGeographicCoordinates(addressCoordinates[0], addressCoordinates[1]));

            var partnerDb = new PartnerMongoDb()
            {
                Document = partner.Document,
                OwnerName = partner.OwnerName,
                TradingName = partner.TradingName,
                CoverageArea = coverageArea,
                Address = address
            };

            return partnerDb;
        }

        private static Point GetAddress(PartnerMongoDb partnerDb)
        {
            var pointCoordinates = partnerDb.Address.Coordinates;
            var coordinates = new List<double>() { pointCoordinates.Longitude, pointCoordinates.Latitude };

            return new Point(coordinates);
        }

        private static MultiPolygon GetCoverageArea(PartnerMongoDb partnerDb)
        {
            var multipolygon = new List<List<List<List<double>>>>();

            foreach (var polygonDb in partnerDb.CoverageArea.Coordinates.Polygons)
            {
                var polygon = new List<List<List<double>>>();
                var polygonCoordinates = new List<List<double>>();

                foreach (var position in polygonDb.Exterior.Positions)
                {
                    var coordinates = new List<double>
                    {
                        position.Longitude,
                        position.Latitude
                    };

                    polygonCoordinates.Add(coordinates);
                }

                polygon.Add(polygonCoordinates);
                multipolygon.Add(polygon);
            }

            return new MultiPolygon(multipolygon); ;
        }
    }
}
