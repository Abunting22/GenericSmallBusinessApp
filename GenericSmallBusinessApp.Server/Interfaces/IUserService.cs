using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsersRequest();
        public Task<User> GetUserByUserIdRequest(int id);
        public Task<bool> AddUserRequest(UserDto request);
        public Task<bool> UpdateUserRequest(UserDto request);
        public Task<bool> DeleteUserRequeset(int id);
    }
}
