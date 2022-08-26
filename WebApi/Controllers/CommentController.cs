using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.DTO;
using Application.Wrappers;
using Domain.Entities;
using System.Net;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        ICommentService _commentService;
       

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
            
        }
        [HttpDelete("DeleteComment")]
        public async Task<ResponseBase> DeleteComment(int commentID)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var isDeleted = await _commentService.DeleteComment(commentID, token);
            var response = new ResponseBase();
            if (isDeleted)
            {
                response.setResponseMessage(true, "Comment deleted successfully.", (int)HttpStatusCode.OK);
                return response;
            }
            response.SetErrorMessage("Bad Request", false, "Only users who share the post or comment can delete the comment !", (int)HttpStatusCode.BadRequest);
            return response;


        }


        [HttpPost("ShareComment")]
        public async Task<ResponseBase> ShareComment(CommentDTO commentDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
        

            var comment = await _commentService.ShareComment(token, commentDTO);
            return new ResponseBase()
            {
                Succeeded = true,
                Message = "Comment shared successfully.",
                StatusCode = (int)HttpStatusCode.OK,
            };

        }


        [HttpGet("GetComments")]
        public async Task<GetCommentsResponeModel> GetAllComments(int postId)
        {
            var comments = await _commentService.GetComments(postId);
            return new GetCommentsResponeModel(comments, true, "Comments belong to post", 200);

        }
        [HttpPut("UpdateComment")]
        public async Task<ResponseBase> UpdateComment(UpdateCommentDTO updateComment)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var isUpdate = await _commentService.UpdateComment(updateComment,token);      
            return new ResponseBase()
            {
                Succeeded = isUpdate,
                Message = "Comment updated successfully.",
                StatusCode = (int)HttpStatusCode.OK,
            };

        }
    }
}
