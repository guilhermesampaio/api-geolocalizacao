using Geolocalization.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Geolocalization.Domain.Repositories
{
    public interface IPartnersRepository
    {
        Task<string> Create(Partner partner);
        Task<Partner> Get(string id);
        Task<Partner> GetByCoordinates(double latitude, double longitude);
    }
}
