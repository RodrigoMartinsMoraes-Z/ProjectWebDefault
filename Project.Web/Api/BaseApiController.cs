using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Users;

namespace Project.Web.Api
{
    [ApiController, Authorize]
    public class BaseApiController : ControllerBase
    {
        public UserManager<User> _loggedUser;
    }
}
