using Dapper;
using LU2WebApi.Models;
using Microsoft.Data.SqlClient;

namespace LU2WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly string sqlConnectionString;

        public UserRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        //public async Task<User?> ReadAsync(Guid id)
        //{
        //    using (var sqlConnection = new SqlConnection(sqlConnectionString))
        //    {
        //        return await sqlConnection.QuerySingleOrDefaultAsync<User>("SELECT * FROM [Users] WHERE Id = @Id", new { id });
        //    }
        //}

        //public async Task<IEnumerable<User>> ReadAsync()
        //{
        //    using (var sqlConnection = new SqlConnection(sqlConnectionString))
        //    {
        //        return await sqlConnection.QueryAsync<User>("SELECT * FROM [Users]");
        //    }
        //}

        public async Task<int> AddUser(User user)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "INSERT INTO [Users] (UserName, Password) VALUES (@UserName, @Password); SELECT SCOPE_IDENTITY();";

                return await sqlConnection.ExecuteScalarAsync<int>(query, new { user.Username, user.Password });
            }
        }

        public void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(string username)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var FoundUser = await sqlConnection.QuerySingleOrDefaultAsync<User>("SELECT * FROM [Users] WHERE UserName = @UserName", new { username });
                return FoundUser;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
