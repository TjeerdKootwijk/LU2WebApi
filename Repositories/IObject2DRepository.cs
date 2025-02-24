using LU2WebApi.Models;
using LU2WebApi.Models.DTO;

namespace LU2WebApi.Repositories
{
    public interface IObject2DRepository
    {
        public Task<Object2D> GetObject2D(int id);
        public Task<int> CreateObject2D(Object2DDTO object2D);
        public Task UpdateObject2D(Object2D object2D);
        public void DeleteObject2D(int id);
    }
}
