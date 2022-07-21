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
    public class PostController :BaseApiController
    {
        IPostService _postService;
        IAuthenticatedUserService _authenticatedUserService;
        public PostController(IPostService postService, IAuthenticatedUserService authenticatedUserService)
        {
            _postService = postService;
            _authenticatedUserService = authenticatedUserService;
        }

        [HttpPost("PostUser")]
        public async Task<IActionResult> CreatePost(PostDTO postDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return Ok(await _postService.SharePost(token, postDTO));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return Ok(await _postService.DeletePost(id, token));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(int PostId, PostDTO post)
        {
            return Ok(await _postService.UpdatePost(PostId, post));
        }
        /*
                HTTPPOST ChangePostStatus
                    public ChangePostStatusResponse ChangePostStatus(ChangePostStatusRequest request) 
        */
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeState(int PostId, bool status)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return Ok(await _postService.ChangePostState(PostId, token, status));
        }
        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return Ok(await _postService.GetPosts(token));
        }


    }
}
