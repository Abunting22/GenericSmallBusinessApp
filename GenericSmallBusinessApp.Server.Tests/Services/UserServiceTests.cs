using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;
using GenericSmallBusinessApp.Server.Services;

using Moq;

namespace GenericSmallBusinessApp.Server.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetAllUsersRequest_Returns_ListOfUsers_When_PrimaryRepositoryCallSucceeds()
        {
            var primeRepoMock = new Mock<IPrimaryRepository<User>>();
            primeRepoMock
                .Setup(x => x.GetAll())
                .ReturnsAsync([]);
            var sut = new UserService(primeRepoMock.Object);

            var actual = await sut.GetAllUsersRequest();

            primeRepoMock.VerifyAll();
        }

        [Fact]
        public async Task GetUserByUserIdRequest_Returns_User_When_PrimaryRepositoryCallSucceeds()
        {
            int userId = 1;
            var primeRepoMock = new Mock<IPrimaryRepository<User>>();
            primeRepoMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<User>());
            var sut = new UserService(primeRepoMock.Object);

            var actual = await sut.GetUserByUserIdRequest(userId);

            primeRepoMock.VerifyAll();
        }

        [Fact]
        public async Task AddUserRequest_Returns_Bool_When_PrimaryRepositoryCallSucceeds()
        {
            var request = new UserDto
            {
                FirstName = "Foo",
                LastName = "Bar",
                EmailAddress = "foo@bar.com",
                Password = "foobar",
                PhoneNumber = "1111111"
            };
            var primeRepoMock = new Mock<IPrimaryRepository<User>>();
            primeRepoMock
                .Setup(x => x.Add(It.IsAny<User>()))
                .ReturnsAsync(true);
            var sut = new UserService(primeRepoMock.Object);

            var actual = await sut.AddUserRequest(request);

            primeRepoMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateUserRequest_Returns_Bool_When_PrimaryRepositoryCallSucceeds()
        {
            var userId = 1;
            var request = new UserDto
            {
                FirstName = "Foo",
                LastName = "Bar",
                EmailAddress = "foo@bar.com",
                Password = "foobar",
                PhoneNumber = "1111111"
            };
            var primeRepoMock = new Mock<IPrimaryRepository<User>>();
            primeRepoMock
                .Setup(x => x.Update(It.IsAny<User>()))
                .ReturnsAsync(true);
            var sut = new UserService(primeRepoMock.Object);

            var actual = await sut.UpdateUserRequest(request, userId);

            primeRepoMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteUserRequest_Returns_Bool_When_PrimaryRepositoryCallSucceeds()
        {
            var userId = 1;
            var primeRepoMock = new Mock<IPrimaryRepository<User>>();
            primeRepoMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);
            var sut = new UserService(primeRepoMock.Object);

            var actual = await sut.DeleteUserRequeset(userId);

            primeRepoMock.VerifyAll();
        }
    }
}
