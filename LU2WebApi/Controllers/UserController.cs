using DevOne.Security.Cryptography.BCrypt;
using LU2WebApi.Models;
using LU2WebApi.Models.DTO;
using LU2WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace LU2WebApi.Controllers;

[ApiController]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserRepository userRepository, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }


    [HttpGet("{UserName}", Name = "GetUser")]
    public async Task<ActionResult<IEnumerable<User>>> Get(string UserName)
    {

        var users = await _userRepository.GetUser(UserName);
        return Ok(users);
    }


    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> Add(User user)
    {
        user.Password = HashPassword(user.Password); // Hash password before storing

        var createdUser = await _userRepository.AddUser(user);
        return Created();
    }

    public static string HashPassword(string password)
    {
        return BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt(12)); // 12 rounds for security
    }


}

