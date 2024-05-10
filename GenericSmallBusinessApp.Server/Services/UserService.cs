using System.Security.Claims;

using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

using Microsoft.AspNetCore.Mvc.Filters;

namespace GenericSmallBusinessApp.Server.Services
{
    public class UserService(IPrimaryRepository<User> repository) : IUserService
    {
        public async Task<List<User>> GetAllUsersRequest()
        {
            var users = await repository.GetAll();
            return users;
        }

        public async Task<User> GetUserByUserIdRequest(int id)
        {
            var user = await repository.GetById(id);
            return user;
        }

        public async Task<bool> AddUserRequest(UserDto request)
        {
            var user = ConvertDtoRequest(request);
            var result = await repository.Add(user);
            return result;
        }

        public async Task<bool> UpdateUserRequest(UserDto request, int id)
        {
            var user = ConvertDtoRequest(request);
            user.UserId = id;
            var result = await repository.Update(user);
            return result;
        }

        public async Task<bool> DeleteUserRequeset(int id)
        {
            var result = await repository.Delete(id);
            return result;
        }

        private static User ConvertDtoRequest(UserDto request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                PhoneNumber = request.PhoneNumber
            };

            return user;
        }
    }
}
