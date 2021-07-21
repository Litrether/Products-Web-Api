using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;
using System.Threading.Tasks;

namespace Messenger.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAutenticationManager _authManager;

        public AccountController(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IAutenticationManager authManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        /// <summary> Create a new user account </summary>
        /// <param name="userForRegistration"></param>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);

                return BadRequest(ModelState);
            }

            try
            {
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            }
            catch
            {
                return BadRequest($"One or all roles doesn't exist: {string.Join(' ', userForRegistration.Roles)} \n\r" +
                    $"Possible roles: Administrator, Manager and User");
            }

            return StatusCode(201);
            }


        /// <summary> Authenticate and autorization user if his exists in the database</summary>
        /// <param name="user"></param>
        /// <returns>Bearer token</returns>
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate(
            [FromBody] UserForManipulationDto user)
        {
            if (await _authManager.ValidateUser(user) == false)
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong username or password.");
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken() });
        }

        /// <summary> Delete user after authenticate </summary>
        /// <param name="user"></param>
        /// <returns>Bearer token</returns>
        [HttpDelete("delete")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteUser(
            [FromBody] UserForManipulationDto user)
        {

            if (await _authManager.ValidateUser(user) == false)
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Delete failed. Wrong username or password.");
                return Unauthorized();
            }

            var userForDelete = await _userManager.FindByNameAsync(user.UserName);

            var result = await _userManager.DeleteAsync(userForDelete);
            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);

                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
