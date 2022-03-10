using FeedbackReporting.Domain.Constants;
using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FeedbackReporting.Presentation.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IJWTService _jwtService;

        public UserController(ILogger<UserController> logger, IJWTService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRessource user)
        {
            _logger.LogDebug($"Authentification request for user {user.UserName}");

            var identifiedUser = await _jwtService.LoginUser(user);

            if (identifiedUser != null)
            {
                _logger.LogDebug($"User {user.UserName} successfully authentificated");
                var token = _jwtService.GenerateJWT(identifiedUser);
                return Ok(token);
            }
            _logger.LogDebug($"User {user.UserName} not found");

            return NotFound(user); // Well not found or bad password...
        }

        //[HttpGet("UserList")]
        //[Authorize(Roles = "User")]
        //public IActionResult getAllUsers()
        //{
        //    var result = _authentication.getUserList<User>();
        //    return Ok(result);
        //}

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create([FromBody] UserRessource user)
        {
            _logger.LogDebug($"Create new user {user.Name}");

            if (await _jwtService.CreateUser(user))
            {
                _logger.LogDebug($"User {user.Name} successfully created");
                return Ok();
            }
            _logger.LogDebug($"An error occur trying to create user {user.Name}");

            return BadRequest(user);
        }

        [HttpDelete]
        [Route("{name}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(string name)
        {
            if (await _jwtService.DeleteUserByEmail(name))
            {
                _logger.LogDebug($"User {name} successfully deleted");
                return Ok();
            }
            _logger.LogDebug($"An error occur trying to delete user {name}");

            return NotFound();
        }
    }
}
