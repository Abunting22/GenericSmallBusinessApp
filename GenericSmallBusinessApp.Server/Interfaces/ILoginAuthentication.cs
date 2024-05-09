using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.Interfaces
{
    public interface ILoginAuthentication
    {
        public Task<User> LoginRequest(LoginDto request);
    }
}
