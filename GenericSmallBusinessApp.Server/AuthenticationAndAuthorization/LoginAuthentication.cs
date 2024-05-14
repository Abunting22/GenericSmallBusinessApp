using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.AuthenticationAndAuthorization
{
    public class LoginAuthentication(IUserRepository repository, IJwtGenerator jwt) : ILoginAuthentication
    {
        public async Task<User> LoginRequest(LoginDto request)
        {
            try
            {
                var user = await LoginValidation(request);
                user.JwtToken = jwt.GetJwt(user);
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<User> LoginValidation(LoginDto request)
        {
            try
            {
                User user = await repository.Login(request);

                if (user != null &&
                        request.EmailAddress == user.EmailAddress &&
                        BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                    return user;
                throw new InvalidOperationException("Invlaid username or password");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
