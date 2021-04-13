using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Project.Domain.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Api
{
    [ApiController, Authorize]
    public class BaseApiController : ControllerBase
    {
        public User _loggedUser;
    }
}
