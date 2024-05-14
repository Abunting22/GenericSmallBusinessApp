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
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(x => x.Login(It.IsAny<LoginDto>()))
                .ReturnsAsync(It.IsAny<User>());
            var sut = new LoginAuthentication(userRepoMock.Object, jwtStub);

            var actual = await sut.LoginRequest(request);

            userRepoMock.VerifyAll();
        }

        //[Fact]
        //public async Task LoginRequest_Returns_ArgumentException_When_LoginValidationFails()
        //{
        //    var request = new LoginDto
        //    {
        //        EmailAddress = "fizz@buzz.com",
        //        Password = "fizzbuzz"
        //    };
        //    var userRepoMock = new Mock<IUserRepository>();
        //    userRepoMock
        //        .Setup(x => x.Login(It.IsAny<LoginDto>()))
        //        .ThrowsAsync(new ArgumentException());
        //    var sut = new LoginAuthentication(userRepoMock.Object, jwtStub);

        //    await Assert.ThrowsAsync<InvalidOperationException>(); 
        //}
    }
}
