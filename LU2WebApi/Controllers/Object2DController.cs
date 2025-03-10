using LU2WebApi.Models;
using LU2WebApi.Repositories.Users;
using LU2WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LU2WebApi.Repositories.Object;
using LU2WebApi.Models.DTO;

namespace LU2WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class Object2DController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IObject2DRepository _object2DRepository;
        private readonly ILogger<Object2DController> _logger;

        //private readonly IGetUserRepository _getUserRepository;

        public Object2DController(IAuthenticationService authenticationService, IObject2DRepository object2DRepository, ILogger<Object2DController> logger)
        {
            _authenticationService = authenticationService;
            _object2DRepository = object2DRepository;
            //_getUserRepository = getUserRepository;
            _logger = logger;
        }

        //[HttpGet("GetEnvironmentObjects")]
        [HttpGet("GetEnvironmentObjects/{EnvironmentId}", Name = "GetEnvironmentObjects")]


        public async Task<ActionResult<IEnumerable<Object2D>>> GetEnvironmentObjects(Guid environmentId)
        {
            if (environmentId == null)
            {
                return BadRequest("no correct environmentId");
            }
            var environment = await _object2DRepository.GetObject2DOfEnvironment(environmentId);

            return Ok(environment);
        }

        //[HttpPost("CreateObject2D")]
        [HttpPost("CreateObject2D/{EnvironmentId}", Name = "CreateObject2D")]

        public async Task<ActionResult> Create(Object2DDTO object2D, Guid environmentId)
        {
            //string userName = "";
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            //await _getUserRepository.GetUser(userId);
            if (userId == null)
            {
                return BadRequest("Not logged in");
            }
            var createdEnvironment = await _object2DRepository.CreateObject2D(object2D, environmentId);

            return Created();
        }

        [HttpPut("UpdateObject2D")]
        public async Task<ActionResult> Update(Guid objectId, Object2DDTO newObject2D)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return BadRequest("Not logged in");
            }
            

            var existingObject2D = await _object2DRepository.GetObject2D(objectId);

            if (existingObject2D == null)
                return NotFound();

            await _object2DRepository.UpdateObject2D(newObject2D);

            return Ok(newObject2D);
        }



        [HttpDelete("DeleteObject2D")]
        public async Task<IActionResult> Delete(Guid ObjectId)
        {
            var existingObject2D = await _object2DRepository.GetObject2D(ObjectId);

            if (existingObject2D == null)
                return NotFound();

            await _object2DRepository.DeleteObject2D(ObjectId);

            return Ok();
        }
    }
}
