using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.DTO;
using Application.Wrappers;
using Domain.Entities;
using System.Collections.Generic;

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
        public async Task<BaseResponse> DeleteComment(int CommentId, int PostId)
        {
            return Ok(await _commentService.DeleteComment(PostId, _authenticatedUserService.UserId, CommentId));
        }
        [HttpPost("ShareComment")]
        public async Task<BaseResponse<Comment>> ShareComment(CommentDTO commentDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return new BaseResponse<Comment>(await _commentService.ShareComment(token, commentDTO));
        }
        
     
        [HttpGet("GetComments")]
        public async Task<BaseResponse<List<Comment>>> GetAllComments(int postId)
        {
            return new BaseResponse<List<Comment>>(List<Comment>(await _commentService.GetComments(postId)));
        }
    }
}
