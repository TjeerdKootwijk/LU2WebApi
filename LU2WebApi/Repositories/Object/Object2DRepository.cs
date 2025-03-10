﻿using Dapper;
using LU2WebApi.Models;
using LU2WebApi.Models.DTO;
using LU2WebApi.Repositories.Object;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;

namespace LU2WebApi.Repositories
{
    public class Object2DRepository : IObject2DRepository
    {

        private readonly string sqlConnectionString;

        public Object2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Guid> CreateObject2D(Object2DDTO object2D, Guid EnvironmentId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.OpenAsync();

                string query = @"INSERT INTO Environment2D (Id, Name, ScaleX, ScaleY, PositionX, PositionY, SortingLayer, RotationZ) VALUES (@Id, @Name, @MaxHeight, @MaxWidth, @UserUsername,); SELECT @Id;";  // Return the GUID we inserted.

                Guid newId = Guid.NewGuid();
                var parameters = new
                {
                    Id = newId,
                    object2D.Name,
                    object2D.ScaleX,
                    object2D.ScaleY,
                    object2D.RotationZ,
                    object2D.SortingLayer,
                    object2D.PositionX,
                    object2D.PositionY,

                    EnvironmentId
                };
                // Execute the query and fetch the GUID that was inserted
                Guid lastInsertedId = await sqlConnection.ExecuteScalarAsync<Guid>(query, parameters);
                return lastInsertedId;
            }
        }

        public async Task DeleteObject2D(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "DELETE FROM [Object2D] WHERE id = @id";
                await sqlConnection.ExecuteScalarAsync<Object2D>(query, new { id });
            }
        }

        public async Task<IEnumerable<Object2D>> GetObject2DOfEnvironment(Guid environmentId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "SELECT Id, Name, PositionX, PositionY, ScaleY, PositionX, RotationZ, SortingLayer FROM [Object2D] WHERE Environment2DId = @environmentId";

                // Use QueryAsync to return a collection of Object2D
                return await sqlConnection.QueryAsync<Object2D>(query, new { environmentId });
            }
        }

        public async Task<Object2D> GetObject2D(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "SELECT * FROM [Object2D] WHERE Id = @Id";

                // Use QuerySingleOrDefaultAsync instead of ExecuteScalarAsync
                return await sqlConnection.QuerySingleOrDefaultAsync<Object2D>(query, new { id });
            }
        }

        public async Task UpdateObject2D(Object2DDTO object2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Object2D] SET Name = @Name, MaxHeight = @MaxHeight, MaxWidth = @MaxWidth", object2D);
            }
        }
    }
}
