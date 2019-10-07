using Geolocalization.Api.Entities;
using Geolocalization.Api.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalization.Api.Repositories
{
    public interface IPartnersRepository
    {
        Task Create(Partner partner);
        Task<PartnerMongoDb> Get(int id);
        Task<PartnerMongoDb> GetByCoordinates(double latitude, double longitude);
    }

    public class PartnersRepository : IPartnersRepository
    {
        private readonly IMongoCollection<PartnerMongoDb> _collection;
        private const string CollectionName = "Partners";
        public PartnersRepository(IOptions<DatabaseSettings> options)
        {
            var databaseSettings = options?.Value;
            var client = new MongoClient(databaseSettings?.ConnectionString);
            var database = client.GetDatabase(databaseSettings?.DatabaseName);
            _collection = database.GetCollection<PartnerMongoDb>(CollectionName);
        }

        public async Task Create(Partner partner)
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

            var partnerDb = new PartnerMongoDb()
            {
                Id = partner.Id,
                CoverageArea = coverageArea,
                Document = partner.Document,
                OwnerName = partner.OwnerName,
                TradingName = partner.TradingName,
                Address = partner.Address
            };

            await _collection.InsertOneAsync(partnerDb);

        }

        public async Task<PartnerMongoDb> Get(int id)
        {
            var partner = await _collection.Find(it => it.Id == id).FirstOrDefaultAsync();
            return partner;
        }

        public async Task<PartnerMongoDb> GetByCoordinates(double latitude, double longitude)
        {
            var coordinates = new GeoJson2DGeographicCoordinates(longitude, latitude);
            var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(coordinates);
            var filter = Builders<PartnerMongoDb>.Filter.GeoIntersects(x => x.CoverageArea, point);

            var partner = await _collection.Find(filter).FirstOrDefaultAsync();

            return partner;
        }
    }
}
