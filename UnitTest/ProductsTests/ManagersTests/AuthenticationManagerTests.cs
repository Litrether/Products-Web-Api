using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Products.Managers;
using Xunit;

namespace UnitTests.ProductsTests.ManagersTests
{
    public class AuthenticationManagerTests
    {
        Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
        Mock<UserManager<User>> _userManager;
        AuthenticationManager _authManager;

        public AuthenticationManagerTests()
        {
            SetUserManager();
            _authManager = new AuthenticationManager(_userManager.Object, _configuration.Object);
            _configuration.Setup(x => x.GetSection("SECRET").Value).Returns("testSecret");
            _configuration.Setup(x => x.GetSection("JwtSettings").Value).Returns("\"validIssuer\":\"ProductsApi\",\"validAudience\":\"https://localhost:5001\",\"expires\":30");
        }

        private void SetUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            _userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new User() { UserName = "UserName", Id = "id" });
            _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
            _userManager.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new string[] { "Manager", "User" });
        }

        [Fact]
        public async void ValidateUserReturnsTrue()
        {
            var result = await _authManager.ValidateUser(new UserValidationDto());

            Assert.True(result);
        }

        [Fact]
        public async void ValidateUserReturnsFalseWhenUserNotFound()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(value: null);

            var result = await _authManager.ValidateUser(new UserValidationDto());

            Assert.False(result);
        }

        [Fact]
        public async void ValidateUserReturnsFalseWhenPasswordInvalid()
        {
            _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

            var result = await _authManager.ValidateUser(new UserValidationDto());

            Assert.False(result);
        }

        [Fact]
        public async void CreateTokenReturnsToken()
        {
            _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);
            await _authManager.ValidateUser(new UserValidationDto());
            var result = await _authManager.CreateToken();

            Assert.NotNull(result);
        }
    }
}
