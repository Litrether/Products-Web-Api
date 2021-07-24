using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;

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
        [ServiceFilter(typeof(ValidateAccountAttribute))]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserRegistrationDto userForRegistration)
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
                return BadRequest($"One or all roles doesn't exist: {string.Join(' ', userForRegistration.Roles)}/ \n\r" +
                    $"Possible roles: Administrator, Manager and User/");
            }

            return StatusCode(201);
        }


        /// <summary> Authenticate and autorization user if his exists in the database</summary>
        /// <param name="user"></param>
        /// <returns>Bearer token</returns>
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidateAccountAttribute))]
        public async Task<IActionResult> Authenticate(
            [FromBody] UserValidationDto user)
        {
            return Ok(new { Token = await _authManager.CreateToken() });
        }

        /// <summary> Delete user after authenticate </summary>
        /// <param name="user"></param>
        /// <returns>Bearer token</returns>
        [HttpDelete("delete")]
        [ServiceFilter(typeof(ValidateAccountAttribute))]
        public async Task<IActionResult> DeleteUser(
            [FromBody] UserValidationDto user)
        {
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
