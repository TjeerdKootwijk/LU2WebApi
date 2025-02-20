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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration["SqlConnectionString"];

            if(string.IsNullOrEmpty(connectionString))
                throw new Exception("Connection string is null or empty");

            builder.Services.AddTransient<IUserRepository, UserRepository>(o => new UserRepository(connectionString));
            builder.Services.AddTransient<AuthRepository, AuthRepository>(o => new AuthRepository(connectionString));


            var app = builder.Build();

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
