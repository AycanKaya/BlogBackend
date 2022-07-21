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
    public class CommentController : BaseApiController
    {
        ICommentService _commentService;
        IAuthenticatedUserService _authenticatedUserService;

        public CommentController(ICommentService commentService, IAuthenticatedUserService authenticatedUserService)
        {
            _commentService = commentService;
            _authenticatedUserService = authenticatedUserService;
        }
        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int CommentId, int PostId)
        {
            return Ok(await _commentService.DeleteComment(PostId, _authenticatedUserService.UserId, CommentId));
        }
        [HttpPost("ShareComment")]
        public async Task<IActionResult> ShareComment(CommentDTO commentDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return Ok(await _commentService.ShareComment(token, commentDTO));
        }
        
     
        [HttpGet("GetComments")]
        public async Task<IActionResult> GetAllComments(int postId)
        {
            return Ok(await _commentService.GetComments(postId));
        }
    }
}
