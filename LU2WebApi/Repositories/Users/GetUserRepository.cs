using Dapper;
using LU2WebApi.Models;
using Microsoft.Data.SqlClient;

namespace LU2WebApi.Repositories.Users
{
    public class GetUserRepository : IGetUserRepository
    {
        private readonly string sqlConnectionString;

        public GetUserRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<User> GetUser(string userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var FoundUser = await sqlConnection.QuerySingleOrDefaultAsync<User>("select * from auth.AspNetUsers WHERE id = @id", new { userId });
                return FoundUser;
            }
        }

    }
}
