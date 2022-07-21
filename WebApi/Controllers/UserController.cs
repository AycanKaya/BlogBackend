using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseApiController
    {
        IUserService _userService;
        IAuthenticatedUserService _authenticatedUserService;

        public UserController(IUserService userService, IAuthenticatedUserService authenticatedUserService)
        {
            _userService = userService;
            _authenticatedUserService = authenticatedUserService;
        }

     

    }
}
