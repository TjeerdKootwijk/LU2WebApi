using LU2WebApi.Models;

namespace LU2WebApi.Repositories
{
    public interface IAuthRepository
    {
        public Task<User> Login(string username, string password);
    }
}
