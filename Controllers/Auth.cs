using DevOne.Security.Cryptography.BCrypt;
using LU2WebApi.Models;
using LU2WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LU2WebApi.Controllers;

[ApiController]
[Route("Auth")]
public class Auth : ControllerBase
{
    private readonly AuthRepository _authRepository;
    private readonly ILogger<Auth> _logger;
    private static Random random = new Random();


    public Auth(AuthRepository authRepository, ILogger<Auth> logger)
    {
        _authRepository = authRepository;
        _logger = logger;
    }


    [HttpPost(Name = "LoginUser")]
    //public async Task<ActionResult> Add(User user)

    public async Task<ActionResult> Login(User user)
    {
        var loginUser = await _authRepository.Login(user.Username, user.Password);

        if (loginUser == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        // Generate JWT Token
        //string token = GenerateJwtToken(user);
        String token = RandomString(15);

        return Ok(new { Token = token });
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}

