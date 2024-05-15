using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenericSmallBusinessApp.Server.AuthenticationAndAuthorization;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;
using GenericSmallBusinessApp.Server.Repositories;

using Moq;

namespace GenericSmallBusinessApp.Server.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task Login_Returns_User_When_BaseRepositoryCallSucceeds()
        {
            var sql = $"SELECT * FROM User WHERE EmailAddress = @EmailAddress";
            var request = new LoginDto
            {
                EmailAddress = "foo@bar.com",
            };
            var user = new User();
            var baseRepMock = new Mock<IBaseRepository>();
            baseRepMock
                .Setup(x => x.GetObject<User, object>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(user);
            var sut = new UserRepository(baseRepMock.Object);

            var actual = await sut.Login(request);

            baseRepMock.VerifyAll();
        }

        [Fact]
        public async Task Login_Returns_Exception_When_DependenciesFail()
        {
            var sql = $"SELECT * FROM User WHERE EmailAddress = @EmailAddress";
            var request = new LoginDto
            {
                EmailAddress = "fizz@buzz.com",
            };
            var baseRepoMock = new Mock<IBaseRepository>();
            baseRepoMock
                .Setup(x => x.GetObject<User, object>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync((User)null);
            var sut = new UserRepository(baseRepoMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => await sut.Login(request));
        }
    }
}
