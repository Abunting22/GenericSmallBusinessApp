using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenericSmallBusinessApp.Server.AuthenticationAndAuthorization;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

using Moq;

namespace GenericSmallBusinessApp.Server.Tests.AuthenticationAndAuthorization
{
    public class LoginAuthenticationTests
    {
        private readonly IJwtGenerator jwtStub = new Mock<IJwtGenerator>().Object;

        [Fact]
        public async Task LoginRequest_Returns_User_When_LoginValidationSucceeds()
        {
            var request = new LoginDto
            {
                EmailAddress = "foo@bar.com",
                Password = "foobar"
            };
            var user = new User
            {
                UserId = 1,
                RoleId = 3,
                FirstName = "Foo",
                LastName = "Bar",
                EmailAddress = "foo@bar.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                PhoneNumber = "1111111"
            };
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(x => x.Login(It.IsAny<LoginDto>()))
                .ReturnsAsync(user);
            var sut = new LoginAuthentication(userRepoMock.Object, jwtStub);

            var actual = await sut.LoginRequest(request);

            userRepoMock.VerifyAll();
        }

        [Fact]
        public async Task LoginRequest_Returns_InvalidOperationException_When_LoginValidationFails()
        {
            var request = new LoginDto
            {
                EmailAddress = "fizz@buzz.com",
                Password = "fizzbuzz"
            };
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(x => x.Login(It.IsAny<LoginDto>()))
                .ReturnsAsync((User)null);
            var sut = new LoginAuthentication(userRepoMock.Object, jwtStub);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.LoginRequest(request));
        }
    }
}
