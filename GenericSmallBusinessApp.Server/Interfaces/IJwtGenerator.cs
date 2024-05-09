using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.Interfaces
{
    public interface IJwtGenerator
    {
        public string GetJwt(User user);
    }
}
