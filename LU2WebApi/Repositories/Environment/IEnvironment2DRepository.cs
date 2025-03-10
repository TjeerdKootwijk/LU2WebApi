using LU2WebApi.Models;
using LU2WebApi.Models.DTO;

namespace LU2WebApi.Repositories.Environment
{
    public interface IEnvironment2DRepository
    {
        public Task<IEnumerable<Environment2D>> GetEnvironment2DOfUser(Guid userId);
        public Task<Environment2D> GetEnvironment2D(Guid id);
        public Task<Guid> CreateEnvironment2D(Environment2DDTO object2D, string userName);
        public Task UpdateEnvironment2D(Environment2DDTO object2D);
        public Task DeleteEnvironment2D(Guid id);
    }
}
