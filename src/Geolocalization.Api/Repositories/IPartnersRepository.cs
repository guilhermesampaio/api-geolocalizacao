using Geolocalization.Api.Entities;
using Geolocalization.Api.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalization.Api.Repositories
{
    public interface IPartnersRepository
    {
        Task Create(Partner partner);
        Task<Partner> Get(int id);
    }

    public class PartnersRepository : IPartnersRepository
    {
        private readonly IMongoCollection<Partner> _collection;
        private const string CollectionName = "Partners";
        public PartnersRepository(IOptions<DatabaseSettings> options)
        {
            var databaseSettings = options?.Value;
            var client = new MongoClient(databaseSettings?.ConnectionString);
            var database = client.GetDatabase(databaseSettings?.DatabaseName);
            _collection = database.GetCollection<Partner>(CollectionName);
        }

        public async Task Create(Partner partner)
        {
            var point = new GeoJson2DGeographicCoordinates(20, 20);
            var points = new List<GeoJson2DGeographicCoordinates>() { point };


            var x = new GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>(points);

            var polygon = new GeoJsonPolygonCoordinates<GeoJson2DGeographicCoordinates>(x);

            var multipolygon = GeoJson.MultiPolygon(polygon);

            await _collection.InsertOneAsync(partner);

        }

        public async Task<Partner> Get(int id)
        {
            var partner = await _collection.Find(it => it.Id == id).FirstOrDefaultAsync();
            return partner;
        }
    }
}
