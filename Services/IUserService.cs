using SIGED_API.Entity;
using SIGED_API.Models.Request;
using SIGED_API.Models.Response;

namespace SIGED_API.Services
{
    public interface IUserService
    {
        UserResponse Auth(Login login);

        UserResponse Authadmin(Login login);
    }
}
