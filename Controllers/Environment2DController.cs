using LU2WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LU2WebApi.Controllers
{
    public class Environment2DController : Controller
    {
        //private readonly IUserRepository _userRepository;
        private readonly ILogger<Environment2DController> _logger;

        public IActionResult Index()
        {
            return View();
        }
    }
}
