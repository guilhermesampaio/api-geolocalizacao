using Geolocalization.Domain.Entities;
using System.Threading.Tasks;

namespace Geolocalization.Domain.Repositories
{
    public interface IPartnersRepository
    {
        Task Create(Partner partner);
        Task<Partner> Get(int id);
        Task<Partner> GetByCoordinates(double latitude, double longitude);
    }
}
