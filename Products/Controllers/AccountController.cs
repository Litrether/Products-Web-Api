using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outgoing;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;

namespace Messenger.Controllers
{
    [Route("api/account")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAutenticationManager _authManager;

        public AccountController(IMapper mapper, UserManager<User> userManager,
            IAutenticationManager authManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }


        /// <summary> Get information about current user </summary>
        [HttpGet(Name = "GetCurrentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userDto = _mapper.Map<UserOutgoingDto>(user);

            return Ok(userDto);
        }

        /// <summary> Create a new user account </summary>
        /// <param name="userForRegistration"></param>
        [HttpPost(Name = "RegisterUser")]
        [ServiceFilter(typeof(ValidateAccountAttribute))]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            try
            {
                var result = await _userManager.CreateAsync(user, userForRegistration.Password);
                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                        ModelState.TryAddModelError(error.Code, error.Description);

                    return BadRequest(ModelState);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return Ok();
        }


        /// <summary> Authenticate and autorization user if his exists in the database</summary>
        /// <param name="user"></param>
        /// <returns>Bearer token</returns>
        [HttpPost("login", Name = "Authenticate")]
        [ServiceFilter(typeof(ValidateAccountAttribute))]
        public async Task<IActionResult> Authenticate(
            [FromBody] UserValidationDto user)
        {
            var userForRoles = await _userManager.FindByNameAsync(user.UserName);
            var roles = await _userManager.GetRolesAsync(userForRoles);

            return Ok(new { Token = await _authManager.CreateToken(), roles = roles });
        }

        /// <summary> Delete current user account</summary>
        /// <returns>No content</returns>
        [HttpDelete("delete", Name = "DeleteUser")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var userForDelete = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = await _userManager.DeleteAsync(userForDelete);
            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);

                return BadRequest(ModelState);
            }

            return NoContent();
        }

        /// <summary> Change account password</summary>
        /// <param name="passwords"></param>
        /// <returns>No content</returns>
        [HttpPut("password", Name = "ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordDto passwords)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var result = await _userManager.ChangePasswordAsync(
                user, passwords.OldPassword, passwords.NewPassword);
            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);

                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
