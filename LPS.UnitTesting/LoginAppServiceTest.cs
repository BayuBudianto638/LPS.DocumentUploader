using LPS.DocumentUploader.Application.Services.Logins;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using LPS.DocumentUploader.Application.Services.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPS.DocumentUploader.Application.Services.Logins.Dto;

namespace LPS.UnitTesting
{
    public class LoginAppServiceTest: Mock<ILoginAppService>
    {
        [Fact]
        public async Task Login_Success()
        {
            // Arrange
            var loginDto = new LoginDto()
            {
                UserName = "john",
                Password = "Password1!"
            };

            var mockUserAppService = new Mock<ILoginAppService>();
            mockUserAppService.Setup(x => x.Login(loginDto))
                        .ReturnsAsync(loginDto);

            // Act
            var login = await mockUserAppService.Object.Login(loginDto);

            // Assert
            Assert.Matches(loginDto.UserName, login.UserName);
        }
    }
}
