using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System.Threading.Tasks;
using Application.DTO;
using Application.DTO.EditorUserDTOs;
using Application.Model;
using Application.Wrappers;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Editor")]
    public class EditorUserController : ControllerBase
    {
        IEditorUserService _editorUserService;
        public EditorUserController(IEditorUserService editorUserService)
        {
            _editorUserService = editorUserService;
        }

        [HttpGet]
        [Route("GetPassivePosts")]
        public async Task<PostsResponseModel> GetPassivePosts()
        {
            var posts = await _editorUserService.GetPassivePosts();
            var response = new PostsResponseModel(posts, true, "All passive posts", 200);
            return response;
        }


        [HttpPost]
        [Route("ActivateControl")]
        public async Task<ResponseBase> ActivatePost(ApproveControlDTO approveControl)
        {
            var changeActivate = await _editorUserService.ActivatePost(approveControl);
            var response = new ResponseBase();
            if (changeActivate)
            {
                response.setResponseMessage(true, "Post approve changed", 200);
                return response;
            }
            response.SetErrorMessage("Not change", false, "Fault", (int)HttpStatusCode.InternalServerError);
            return response;

        }

        [HttpPost]
        [Route("DeleteComment")]
        public async Task<ResponseBase> DeleteComment(int commentID)
        {
            var isDelete = await _editorUserService.DeleteComment(commentID);
            var response = new ResponseBase();
            if (isDelete)
            {
                response.setResponseMessage(true, "Comment deleted!", 200);
                return response;

            }
            response.SetErrorMessage("Comment not found!", false, "Comment did not delete!", (int)HttpStatusCode.BadRequest);
            return response;
        }

    }
}
