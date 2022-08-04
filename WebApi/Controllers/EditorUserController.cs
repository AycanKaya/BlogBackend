using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System.Threading.Tasks;
using Application.DTO;
using Application.DTO.EditorUserDTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Editor")]
    public class EditorUserController :ControllerBase
    {
        IEditorUserService _editorUserService;
        public EditorUserController(IEditorUserService editorUserService)
        {
            _editorUserService = editorUserService;
        }

        [HttpGet]
        [Route("GetPassivePosts")]
        public async Task<IActionResult> GetPassivePosts()
        {
            return Ok(await _editorUserService.GetPassivePosts());
        }
       

        [HttpPost]
        [Route("ActivateControl")]
        public async Task<IActionResult> ActivatePost(ApproveControlDTO approveControl)
        {
            return Ok(await _editorUserService.ActivatePost(approveControl));
        }

        [HttpDelete]
        [Route("DeletePost")]
        public async Task<IActionResult> DeletePost(int commentID)
        {
            return Ok(await _editorUserService.DeleteComment(commentID));
        }

    }
}
