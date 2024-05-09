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

                if (user != null)
                {
                    user.JwtToken = jwt.GetJwt(user);
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        private async Task<User> LoginValidation(LoginDto request)
        {
            try
            {
                User user = await repository.Login(request);

                bool IsValid()
                {
                    if (user != null &&
                        request.EmailAddress == user.EmailAddress &&
                        BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                        return true;
                    return false;
                }

                if (IsValid())
                    return user;
                return null;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
