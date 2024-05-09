using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.Repositories
{
    public class UserRepository(IBaseRepository repository) : IUserRepository
    {
        public async Task<User> Login(LoginDto request)
        {
            try
            {
                string sql = $"SELECT * FROM [User] WHERE EmailAddress = @EmailAddress";
                var user = await repository.GetObject<User, dynamic>(sql, new { request.EmailAddress });
                return user;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
