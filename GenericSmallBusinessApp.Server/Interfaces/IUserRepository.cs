using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Login(LoginDto request);
    }
}
