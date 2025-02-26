using Dapper;
using DevOne.Security.Cryptography.BCrypt;
using LU2WebApi.Models;
using Microsoft.Data.SqlClient;

namespace LU2WebApi.Repositories
{
    public class AuthRepository: IAuthRepository
    {

        private readonly string sqlConnectionString;

        public AuthRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<User> Login(string username, string password)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.OpenAsync();

                // Retrieve user from database
                var user = await sqlConnection.QuerySingleOrDefaultAsync<User>("SELECT * FROM [Users] WHERE UserName = @UserName", new { UserName = username });

                if (user == null)
                {
                    return null; // Username not found
                }

                // FIX: Correct parameter order for BCryptHelper.CheckPassword
                if (!BCryptHelper.CheckPassword(password, user.Password))
                {
                    return null; // Password incorrect
                }

                return user; // Successful login
            }
        }

    }
}
