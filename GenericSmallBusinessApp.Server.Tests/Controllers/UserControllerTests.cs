using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using GenericSmallBusinessApp.Server.Controllers;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Moq;

namespace GenericSmallBusinessApp.Server.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetAllUsers_Returns_List_When_ServiceCallSucceeds()
        {
            var users = new List<User>()
            {
                new() { UserId = 1, EmailAddress = "foo@bar.com" },
                new() { UserId = 2, EmailAddress = "bar@foo.com"},
                new() { UserId = 3, EmailAddress = "far@boo.com"}
            };
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(x => x.GetAllUsersRequest())
                .ReturnsAsync(users);
            var sut = new UserController(userServiceMock.Object);

            var actual = await sut.GetAllUsers();

            userServiceMock.VerifyAll();
        }

        [Fact]
        public async Task GetUserById_Returns_User_When_ServiceCalSuccceeds()
        {
            var userId = 1;
            var user = new User();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(x => x.GetUserByUserIdRequest(userId))
                .ReturnsAsync(user);
            var sut = new UserController(userServiceMock.Object);

            var actual = await sut.GetUserById(userId);

            userServiceMock.VerifyAll();
        }

        [Fact]
        public async Task AddUser_Returns_Bool_When_ServiceCallSucceeds()
        {
            var request = new UserDto
            {
                FirstName = "Foo",
                LastName = "Bar",
                EmailAddress = "foo@bar.com",
                Password = "foobar",
                PhoneNumber = "1111111"
            };
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(x => x.AddUserRequest(request))
                .ReturnsAsync(true);
            var sut = new UserController(userServiceMock.Object);

            var actual = await sut.AddUser(request);

            userServiceMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateUser_Returns_Bool_When_ServiceCallSucceeds()
        {
            var request = new UserDto
            {
                FirstName = "Foo",
                LastName = "Bar",
                EmailAddress = "foo@bar.com",
                Password = "foobar",
                PhoneNumber = "1111111"
            };
            var userId = 1;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(x => x.UpdateUserRequest(request, userId))
                .ReturnsAsync(true);
            var mockContext = new ControllerContext();
            mockContext.HttpContext = new DefaultHttpContext();
            mockContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new("id", userId.ToString())
            }));
            
            var sut = new UserController(userServiceMock.Object);
            sut.ControllerContext = mockContext;

            var actual = await sut.UpdateUser(request);

            userServiceMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteUser_Returns_Bool_When_ServiceCallSucceeds()
        {
            var userId = 1;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(x => x.DeleteUserRequeset(userId))
                .ReturnsAsync(true);
            var sut = new UserController(userServiceMock.Object);

            var actual = await sut.DeleteUser(userId);

            userServiceMock.VerifyAll();
        }
    }
}
