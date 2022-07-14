using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.PostFeatures;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : BaseApiController
    {
        IUserService _userService;
        IAuthenticatedUserService _authenticatedUserService;

        public PostController(IUserService userService, IAuthenticatedUserService authenticatedUserService)
        {
            _userService = userService;
            _authenticatedUserService = authenticatedUserService;
        }

        [HttpPost("PostUser")]
        public async Task<IActionResult> Create(PostDTO postDTO)
        {
            return Ok(await _userService.SharePost(_authenticatedUserService.UserId.ToString(), postDTO));
        }

    /*    [HttpPost]
        public async Task<IActionResult> Create( CreatePostCommand createPostCommand)
        {
            return Ok(await Mediator.Send(createPostCommand));
        } */

        [HttpPut("action")]
        public async Task<IActionResult> Update( UpdatePostCommand updatePostCommand)
        {
            return Ok(await Mediator.Send(updatePostCommand));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeletePostCommand {Id = id}));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPosts()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPostById { Id = id}));
        }

    }
}
