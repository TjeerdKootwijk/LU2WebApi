using LU2WebApi.Models;

namespace LU2WebApi.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUser(string username);
        public Task<int> AddUser(User user);
        public Task<User> UpdateUser(User user);
        public void DeleteUser(string username);
    }
}
