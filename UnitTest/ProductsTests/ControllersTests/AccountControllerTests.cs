using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outgoing;
using Entities.Models;
using Messenger.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using Xunit;

namespace UnitTests.ProductsTests.ControllersTests
{
    public class AccountControllerTests
    {
        Mock<IMapper> _mapper = new Mock<IMapper>();
        Mock<IAutenticationManager> _authManager = new Mock<IAutenticationManager>();
        Mock<UserManager<User>> _userManager;
        private ControllerContext _controllerContext;
        private AccountController _controller;
        private User _user;

        public AccountControllerTests()
        {
            _user = new User() { UserName = "UserName", Id = "id" };
            SetUserManager();
            var identity = new GenericIdentity("UserName", "test");
            var contextUser = new ClaimsPrincipal(identity);
            _controllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext { User = contextUser } };
            _controller = new AccountController(_mapper.Object, _userManager.Object, _authManager.Object) { ControllerContext = _controllerContext, };
        }

        private void SetUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            _userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _userManager.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(x => x.GetRolesAsync(_user)).ReturnsAsync(new string[] { "Administrator", "User" });
        }

        [Fact]
        public async void GetCurrentUserReturnsOkObjectResult()
        {
            _mapper.Setup(map => map.Map<UserOutgoingDto>(It.IsAny<User>()))
                .Returns(new UserOutgoingDto());

            var result = await _controller.GetCurrentUser();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void RegisterUserReturnsBadRequestObjectResultWhenException()
        {
            var registrDto = new UserRegistrationDto()
            {
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                UserName = "test",
                Password = "test",
                Roles = new string[] { "test1", "test2" },
            };

            _mapper.Setup(map => map.Map<User>(It.IsAny<UserRegistrationDto>())).Returns(new User());
            _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).Throws(new System.Exception("test"));
            var result = await _controller.RegisterUser(registrDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void RegisterUserReturnsBadRequestResultWhenError()
        {
            var registrDto = new UserRegistrationDto()
            {
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                UserName = "test",
                Password = "test",
                Roles = new string[] { "test1", "test2" },
            };

            _mapper.Setup(map => map.Map<User>(It.IsAny<UserRegistrationDto>())).Returns(new User());
            _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

            var result = await _controller.RegisterUser(registrDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void RegisterUserReturnsOkResultMOCK()
        {
            var registrDto = new UserRegistrationDto()
            {
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                UserName = "test",
                Password = "test",
                Roles = new string[] { "test1", "test2" },
            };

            _mapper.Setup(map => map.Map<User>(It.IsAny<UserRegistrationDto>()))
                .Returns(new User());

            var result = await _controller.RegisterUser(registrDto);

            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async void RegisterUserReturnsOkResult()
        {
            var registrDto = new UserRegistrationDto()
            {
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                UserName = "test",
                Password = "test",
                Roles = new string[] { "test1", "test2" },
            };

            _mapper.Setup(map => map.Map<User>(It.IsAny<UserRegistrationDto>()))
                .Returns(new User());

            var result = await _controller.RegisterUser(registrDto);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void AuthenticateReturnsOkObjectResult()
        {
            _authManager.Setup(auth => auth.CreateToken()).ReturnsAsync(value: null);
            var user = new UserValidationDto()
            {
                UserName = "UserName",
                Password = "111"
            };

            var result = await _controller.Authenticate(user);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void DeleteUserReturnsNoContentResult()
        {
            var user = new UserValidationDto()
            {
                UserName = "UserName",
                Password = "111"
            };

            var result = await _controller.DeleteUser(user);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteUserReturnsBadRequestObjectResult()
        {
            var user = new UserValidationDto()
            {
                UserName = "UserName",
                Password = "111"
            };
            _userManager.Setup(x => x.DeleteAsync(_user)).ReturnsAsync(IdentityResult.Failed());

            var result = await _controller.DeleteUser(user);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ChangePasswordReturnsNoContentResult()
        {
            var changePass = new ChangePasswordDto()
            {
                NewPassword = "oldPass",
                OldPassword = "newPass",
            };

            var result = await _controller.ChangePassword(changePass);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void ChangePasswordReturnsBadRequestObjectResultWhenError()
        {
            var changePass = new ChangePasswordDto()
            {
                NewPassword = "oldPass",
                OldPassword = "newPass",
            };

            _userManager.Setup(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            var result = await _controller.ChangePassword(changePass);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
