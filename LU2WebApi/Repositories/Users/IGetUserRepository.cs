using LU2WebApi.Models;

namespace LU2WebApi.Repositories.Users
{
    public interface IGetUserRepository
    {
        public Task<User> GetUser(string userId);
    }
}
