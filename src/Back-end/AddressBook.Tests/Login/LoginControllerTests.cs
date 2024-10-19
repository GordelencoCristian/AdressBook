using AddressBook.API.Controllers;
using AddressBook.DataTrasnferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Tests.Login
{
    public class LoginControllerTests
    {
        private readonly LoginController _loginController = new();

        [Fact]
        public void Login_ShouldReturnJwtToken_WhenCredentialsAreValid()
        {
            var loginDto = new LoginDto() { UserName = "Gordelenco", Password = "Cristian" };

            var result = _loginController.Login(loginDto);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().BeOfType<string>();
        }

        [Fact]
        public void Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            var loginDto = new LoginDto { UserName = "WrongUser", Password = "WrongPass" };

            var result = _loginController.Login(loginDto);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public void Login_ShouldReturnBadRequest_WhenUsernameOrPasswordIsEmpty()
        {
            var loginDto = new LoginDto() { UserName = "", Password = "" };

            var result = _loginController.Login(loginDto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
