using Dapper;
using LU2WebApi.Models;
using LU2WebApi.Models.DTO;
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

        public async Task<int> CreateObject2D(Object2DDTO object2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "INSERT INTO [Object2D] OUTPUT INSERTED.Id VALUES ( @Name, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer)";

                int lastInsertedId = await sqlConnection.ExecuteScalarAsync<int>(query, object2D);

                return lastInsertedId;
            }
        }

        public async void DeleteObject2D(int id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "DELETE FROM [WeatherForecast] WHERE Id = @Id";

                await sqlConnection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Object2D> GetObject2D(int id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                string query = "SELECT * FROM [Object2D] WHERE Id = @Id";

                return await sqlConnection.ExecuteScalarAsync<Object2D>(query, new { id });
               
            }
        }

        public Task UpdateObject2D(Object2D object2D)
        {
            throw new NotImplementedException();
        }

        //public Task UpdateObject2D(Object2D object2D)
        //{
        //    return true;
        //    //using (var sqlConnection = new SqlConnection(sqlConnectionString))
        //    //{
        //    //    string query = "UPDATE [Object2D] SETVALUES ( @Name, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer)";

        //    //    //int lastInsertedId = await sqlConnection.ExecuteScalarAsync<int>(query, object2D);

        //    //    //return lastInsertedId;
        //    //}
        //}
    }
}
