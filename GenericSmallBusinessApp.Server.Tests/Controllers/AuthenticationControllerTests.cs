using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenericSmallBusinessApp.Server.Controllers;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Moq;

namespace GenericSmallBusinessApp.Server.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public async Task Login_Returns_ActionResultAndUser_When_LoginRequestSucceeds()
        {
            var request = new LoginDto
            {
                EmailAddress = "foo@bar.com",
                Password = "foobar"
            };
            var user = new User();
            var authenticationMock = new Mock<ILoginAuthentication>();
            authenticationMock
                .Setup(x => x.LoginRequest(request))
                .ReturnsAsync(user);
            var sut = new AuthenticationController(authenticationMock.Object);

            var actual = await sut.Login(request);

            authenticationMock.VerifyAll();
        }
    }
}
