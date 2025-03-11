using LU2WebApi.Models;
using LU2WebApi.Repositories.Environment;
using LU2WebApi.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;


namespace LU2WebApi.Controllers
{
    [ApiController]
    //[Authorize]

    public class Environment2DController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly IEnvironment2DRepository _environment2DRepository;
        private readonly ILogger<Environment2DController> _logger;

        private readonly IGetUserRepository _getUserRepository;

        public Environment2DController(IAuthenticationService authenticationService, IEnvironment2DRepository environment2DRepository, ILogger<Environment2DController> logger, IGetUserRepository getUserRepository)
        {
            _authenticationService = authenticationService;
            _environment2DRepository = environment2DRepository;
            _getUserRepository = getUserRepository;
            _logger = logger;
        }

        [HttpGet("GetEnvironment2DOfUser")]
        public async Task<ActionResult<IEnumerable<Environment2D>>> GetUserEnvironment()
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();

            if (userId == null)
            {
                return BadRequest("no correct userId");
            }
            var environment = await _environment2DRepository.GetEnvironment2DOfUser(Guid.Parse(userId));

            return Ok(environment);
        }

        [HttpPost("CreateEnvironment2D")]
        public async Task<ActionResult> Create(Environment2DDTO environment)
        {
            //string userName = "";
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            //await _getUserRepository.GetUser(userId);
            if (userId == null)
            {
                return BadRequest("Not logged in");
            }

            if (environment.Name.Length < 1 || environment.Name.Length > 25)
            {
                return BadRequest("Incorrect environment name, must be 1 to 25 characters.");
            }
            if (environment.MaxWidth < 20 || environment.MaxWidth > 200)
            {
                return BadRequest("Incorrect environment width, must be between 20 and 200");

            }
            if (environment.MaxHeight < 10 || environment.MaxHeight > 100)
            {
                return BadRequest("Incorrect environment width, must be between 10 and 100");

            }

            IEnumerable<Environment2D> userEnvironments = await _environment2DRepository.GetEnvironment2DOfUser(Guid.Parse(userId));
            if (userEnvironments.Count() >= 5) // Correct operator and use Count()
            {
                return BadRequest("You have reached the max worlds");
            }
            foreach(Environment2D environment2D in userEnvironments)
            {
                if(environment.Name == environment2D.Name)
                {
                    return BadRequest("Cannot create a world with a elready existing world name");
                }
            }

            var createdEnvironment = await _environment2DRepository.CreateEnvironment2D(environment, userId);

            return Created();
        }

        [HttpPut("UpdateEnvironment2D")]
        public async Task<ActionResult> Update(Guid environmentId, Environment2DDTO newEnvironment2D)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return BadRequest("Not logged in");
            }
            if (newEnvironment2D.Name.Length < 1 || newEnvironment2D.Name.Length > 25)
            {
                return BadRequest("Incorrect environment name, must be 1 to 25 characters.");
            }
            if (newEnvironment2D.MaxWidth < 20 || newEnvironment2D.MaxWidth > 200)
            {
                return BadRequest("Incorrect environment width, must be between 20 and 200");

            }
            if (newEnvironment2D.MaxHeight < 10 || newEnvironment2D.MaxHeight > 100)
            {
                return BadRequest("Incorrect environment width, must be between 10 and 100");

            }

            var existingEnvironment2D = await _environment2DRepository.GetEnvironment2D(environmentId);

            if (existingEnvironment2D == null)
                return NotFound();

            await _environment2DRepository.UpdateEnvironment2D(newEnvironment2D);

            return Ok(newEnvironment2D);
        }



        [HttpDelete("DeleteEnvironment2D/{EnvironmentId}", Name = "DeleteEnvironment2D")]
        public async Task<IActionResult> Delete(Guid EnvironmentId)
        {
            //delete objects first


            var existingEnvironment2D = await _environment2DRepository.GetEnvironment2DOfUser(EnvironmentId);

            if (existingEnvironment2D == null)
                return NotFound();

            await _environment2DRepository.DeleteEnvironment2D(EnvironmentId);

            return Ok();
        }
    }
}
