using LU2WebApi.Models;
using LU2WebApi.Models.DTO;

namespace LU2WebApi.Repositories.Object
{
    public interface IObject2DRepository
    {
        public Task<IEnumerable<Object2D>> GetObject2DOfEnvironment(Guid environmentId);
        public Task<Object2D> GetObject2D(Guid id);
        public Task<Object2D> CreateObject2D(Object2DDTO object2D, Guid EnvironmentId);
        public Task UpdateObject2D(Object2D object2D);
        public Task DeleteObject2D(Guid id);
    }
}
