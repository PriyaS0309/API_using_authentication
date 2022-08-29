using API_using_authentication;

namespace API_using_authentication.Services
{
   public interface IJWTManagerRepo
    {
        Tokens Authenticate(Users users);
    }
}
