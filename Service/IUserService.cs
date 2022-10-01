using Alfa.Models.Response;
using Alfa.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Alfa.Service
{
    public interface IUserService
    {

        UserResponse Auth(AuthRequest model);

    }
}
