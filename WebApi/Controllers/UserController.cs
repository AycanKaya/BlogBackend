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

        [HttpPost("PostUser")]
        public async Task<IActionResult> CreatePost(PostDTO postDTO)
        {
            return Ok(await _userService.SharePost(_authenticatedUserService.UserId.ToString(), postDTO));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            return Ok(await _userService.DeletePost(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(int PostId, PostDTO post)
        {
            return Ok(await _userService.UpdatePost(PostId, post));
        }

        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeState(int PostId, bool status)
        {
            return  Ok(await _userService.ChangeState(PostId,_authenticatedUserService.UserId, status));
        }

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int CommentId,int PostId)
        {
            return Ok(await _userService.DeleteComment(PostId,_authenticatedUserService.UserId, CommentId));
        }
        [HttpPost("ShareComment")]
        public async Task<IActionResult> ShareComment(CommentDTO commentDTO)
        {
            return Ok(await _userService.ShareComment(_authenticatedUserService.UserName,_authenticatedUserService.UserId,commentDTO));
        }
        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            return Ok(await _userService.GetPosts(_authenticatedUserService.UserId));
        }
        [HttpGet("GetComments")]
        public async Task<IActionResult> GetAllComments(int postId)
        {
            return Ok(await _userService.GetComments(postId));
        }

    }
}
