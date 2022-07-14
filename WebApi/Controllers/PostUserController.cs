/*
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System.Threading.Tasks;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostUserController : ControllerBase
    {
         IUserService _userService;
         IAuthenticatedUserService _authenticatedUserService;

        public PostUserController(IUserService userService, IAuthenticatedUserService authenticatedUserService)
        {
            _userService = userService;
            _authenticatedUserService = authenticatedUserService;
        }
        [HttpPost("PostUser")]
        public async Task<IActionResult> Post([FromBody] PostDTO postDTO)
        {
            return Ok(await _userService.SharePost(_authenticatedUserService.UserId, postDTO));
        }
    }
}
*/