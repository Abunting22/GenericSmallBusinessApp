using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using GenericSmallBusinessApp.Server.AuthenticationAndAuthorization;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GenericSmallBusinessApp.Server.Tests.AuthenticationAndAuthorization
{
    public class JwtGeneratorTests
    {
        [Fact]
        public void GetJwt_Returns_String_When_CreateJwtSucceeds()
        {
            var password = "foobar";
            var user = new User
            {
                UserId = 1,
                RoleId = 3,
                FirstName = "Foo",
                LastName = "Bar",
                EmailAddress = "foo@bar.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                PhoneNumber = "1111111"
            };

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x.GetSection("JwtSettings:Issuer").Value).Returns("TestIssuer");
            configMock.Setup(x => x.GetSection("JwtSettings:Audience").Value).Returns("TestAudience");
            configMock.Setup(x => x.GetSection("JwtSettings:Key").Value).Returns("FooBarNotSuperDuperSecretTestKeyBarFoo");

            var sut = new JwtGenerator(configMock.Object);

            var actual = sut.GetJwt(user);

            var handler = new JwtSecurityTokenHandler();
            var mockJwt = handler.ReadToken(actual) as JwtSecurityToken;

            Assert.NotNull(mockJwt);
            Assert.Contains(mockJwt.Claims, claim =>
                (claim.Type == ClaimTypes.Name && claim.Value == "foo@bar.com") ||
                (claim.Type == "id" && claim.Value == "1") ||
                (claim.Type == ClaimTypes.Role && claim.Value == "User") ||
                (claim.Type == ClaimTypes.SerialNumber));
        }
    }
}
