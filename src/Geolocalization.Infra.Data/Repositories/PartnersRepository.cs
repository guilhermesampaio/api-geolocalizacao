using Geolocalization.CrossCutting.Options;
using Geolocalization.Domain.Entities;
using Geolocalization.Domain.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Threading.Tasks;

namespace Geolocalization.Infra.Data
{
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

        public async Task<string> Create(Partner partner)
        {
            var partnerDb = partner.ToPartnerMongoDb();

            await _collection.InsertOneAsync(partnerDb);

            return partnerDb.Id.ToString();
        }

        public async Task<Partner> Get(string id)
        {
            var partnerDb = await _collection
                .Find(it => it.Id == ObjectId.Parse(id))
                .FirstOrDefaultAsync();

            if (partnerDb == null)
                return default(Partner);

            var partnerEntity = partnerDb.ToPartnerEntity();

            return partnerEntity;
        }

        public async Task<Partner> GetByCoordinates(double latitude, double longitude)
        {
            var coordinates = new GeoJson2DGeographicCoordinates(longitude, latitude);
            var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(coordinates);
            var filter = Builders<PartnerMongoDb>.Filter.GeoIntersects(x => x.CoverageArea, point);
            var partnerDb = await _collection.Find(filter).FirstOrDefaultAsync();

            if (partnerDb == null)
                return default(Partner);

            var partner = partnerDb.ToPartnerEntity();

            return partner;
        }
    }

}
