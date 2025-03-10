using Dapper;
using LU2WebApi.Models;
using LU2WebApi.Models.DTO;
using LU2WebApi.Repositories.Environment;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LU2WebApi.Repositories
{
    public class Environment2DRepository : IEnvironment2DRepository
    {

        private readonly string sqlConnectionString;

        public Environment2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task DeleteEnvironment2D(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                // First, delete all Object2D records related to the Environment2D
                string deleteObjectsQuery = "DELETE FROM [Object2D] WHERE Environment2DId = @id";
                await sqlConnection.ExecuteAsync(deleteObjectsQuery, new { id });

                // Then, delete the Environment2D record
                string deleteEnvironmentQuery = "DELETE FROM [Environment2D] WHERE id = @id";
                await sqlConnection.ExecuteAsync(deleteEnvironmentQuery, new { id });
            }
        }

        public async Task<IEnumerable<Environment2D>> GetEnvironment2DOfUser(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "SELECT * FROM [Environment2D] WHERE UserUsername = @userId";

                // Use QueryAsync to return a collection of Environment2D
                return await sqlConnection.QueryAsync<Environment2D>(query, new { userId });
            }
        }


        public async Task<Guid> CreateEnvironment2D(Environment2DDTO object2D, string UserUsername)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.OpenAsync();

                string query = @"
                    INSERT INTO Environment2D (Name, MaxHeight, MaxWidth, UserUsername, Id)
                    VALUES (@Name, @MaxHeight, @MaxWidth, @UserUsername, @Id);
                    SELECT @Id;";  // Return the GUID we inserted.

                // Generate a new GUID
                Guid newId = Guid.NewGuid();

                // Set up the parameters
                var parameters = new
                {
                    Id = newId,
                    Name = object2D.Name,
                    MaxHeight = object2D.MaxHeight,
                    MaxWidth = object2D.MaxWidth,
                    UserUsername = UserUsername
                };

                // Execute the query and fetch the GUID that was inserted
                Guid lastInsertedId = await sqlConnection.ExecuteScalarAsync<Guid>(query, parameters);

                return lastInsertedId;
            }
        }





        public async Task<Environment2D> GetEnvironment2D(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "SELECT * FROM [Environment2D] WHERE Id = @Id";

                // Use QuerySingleOrDefaultAsync instead of ExecuteScalarAsync
                return await sqlConnection.QuerySingleOrDefaultAsync<Environment2D>(query, new { id });
            }
        }

        public async Task UpdateEnvironment2D(Environment2DDTO environment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Environment2D] SET Name = @Name, MaxHeight = @MaxHeight, MaxWidth = @MaxWidth", environment);
            }
        }
    }
}
