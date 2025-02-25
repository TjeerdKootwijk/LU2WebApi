using LU2WebApi.Repositories;

namespace LU2WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration["SqlConnectionString"];

            var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(connectionString);

            builder.Services.AddTransient<IUserRepository, UserRepository>(o => new UserRepository(connectionString));
            builder.Services.AddTransient<AuthRepository, AuthRepository>(o => new AuthRepository(connectionString));


            var app = builder.Build();
            app.MapGet("/", () => $"The API is up . Connection string found: {(sqlConnectionStringFound ? "Yes" : "NO")}");


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
