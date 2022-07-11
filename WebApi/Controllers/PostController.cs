using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.PostFeatures;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseApiController
    {        
        [HttpPost]
        public async Task<IActionResult> Create( CreatePostCommand createPostCommand)
        {
            return Ok(await Mediator.Send(createPostCommand));
        }

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
