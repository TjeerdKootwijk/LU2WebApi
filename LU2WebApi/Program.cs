using LU2WebApi.Controllers;
using LU2WebApi.Repositories;
using LU2WebApi.Repositories.Environment;
using LU2WebApi.Repositories.Object;
using LU2WebApi.Repositories.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace LU2WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration["SqlConnectionString"];

            var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(connectionString);

            // Add services to the container.

            builder.Services.AddAuthorization();

            builder.Services
                .AddIdentityApiEndpoints<IdentityUser>()
                .AddDapperStores(options =>
                {
                    options.ConnectionString = connectionString;
                });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();

            builder.Services.AddTransient<IGetUserRepository, GetUserRepository>(o => new GetUserRepository(connectionString));
            builder.Services.AddTransient<IEnvironment2DRepository, Environment2DRepository>(o => new Environment2DRepository(connectionString));
            builder.Services.AddTransient<IObject2DRepository, Object2DRepository>(o => new Object2DRepository(connectionString));



            var app = builder.Build();

            app.MapGet("/", () => $"The API is up . Connection string found: {(sqlConnectionStringFound ? "Yes" : "NO")}");

            app.UseAuthorization();

            app.MapGroup(prefix: "/account")

                .MapIdentityApi<IdentityUser>();

            app.MapPost(pattern: "/account/logout",

            async (SignInManager<IdentityUser> signInManager,

            [FromBody] object empty) => {

                if (empty != null)
                {
                    await signInManager.SignOutAsync();
                    return Results.Ok();
                }

                return Results.Unauthorized();

            })
            .RequireAuthorization();

            app.MapControllers();


            // Configure the HTTP request pipeline.

            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers().RequireAuthorization();
            app.Run();
        }
    }
}