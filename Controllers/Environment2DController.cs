using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LU2WebApi.Controllers
{
    public class Environment2DController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
