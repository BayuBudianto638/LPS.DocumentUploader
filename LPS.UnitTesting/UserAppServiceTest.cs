using LPS.DocumentUploader.Application.Services.Documents.Dto;
using LPS.DocumentUploader.Application.Services.Documents;
using LPS.DocumentUploader.Application.Services.Users;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPS.DocumentUploader.Application.Services.Users.Dto;
using Xunit.Sdk;

namespace LPS.UnitTesting
{
    public class UserAppServiceTest : Mock<IUserAppService>
    {
        [Fact]
        public async Task SaveUser_Success()
        {
            // Arrange
            var userDto = new CreateUserDto
            {
                UserName = "john_travolta",
                Password = "password123",
                SurName = "John",
                LastName = "Travolta",
                Email = "john_travolta@example.com",
                NoHP = 1234567890,
                PasswordSalt = "garamasin"
            };


            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.Create(userDto))
                        .ReturnsAsync((true, "Success"));

            // Act
            var (resultBool, resultString) = await mockUserAppService.Object.Create(userDto);

            // Assert
            Assert.Matches("Success", resultString);
            Assert.True(resultBool);
        }

        [Fact]
        public async Task SaveUser_Error()
        {
            // Arrange
            var userDto = new CreateUserDto
            {
                UserName = "john_travolta",
                Password = "password123",
                SurName = "John",
                LastName = "Travolta",
                Email = "john_travolta@example.com",
                NoHP = 1234567890,
                PasswordSalt = "garamasin"
            };

            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.Create(userDto))
                        .ReturnsAsync((false, "Error"));

            // Act
            var (resultBool, resultString) = await mockUserAppService.Object.Create(userDto);

            // Assert
            Assert.Matches("Error", resultString);
            Assert.False(resultBool);
        }

        [Fact]
        public async Task EditUser_Success()
        {
            // Arrange
            var userDto = new UpdateUserDto
            {
                UserName = "john_travolta",
                Password = "password123",
                SurName = "John",
                LastName = "Travolta",
                Email = "john_travolta@example.com",
                NoHP = 1234567890,
                PasswordSalt = "garamasin"
            };


            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.Update(userDto))
                        .ReturnsAsync((true, "Success"));

            // Act
            var (resultBool, resultString) = await mockUserAppService.Object.Update(userDto);

            // Assert
            Assert.Matches("Success", resultString);
            Assert.True(resultBool);
        }

        [Fact]
        public async Task EditUser_Error()
        {
            // Arrange
            var userDto = new UpdateUserDto
            {
                UserName = "john_travolta",
                Password = "password123",
                SurName = "John",
                LastName = "Travolta",
                Email = "john_travolta@example.com",
                NoHP = 1234567890,
                PasswordSalt = "garamasin"
            };

            var mockUserAppService = new Mock<IUserAppService>();
            mockUserAppService.Setup(x => x.Update(userDto))
                        .ReturnsAsync((false, "Error"));

            // Act
            var (resultBool, resultString) = await mockUserAppService.Object.Update(userDto);

            // Assert
            Assert.Matches("Error", resultString);
            Assert.False(resultBool);
        }
    }
}
