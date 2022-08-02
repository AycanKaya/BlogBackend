using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTO.PostTagDTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    public class PostTagController : ControllerBase
    {
        IPostTagService _postTagService;

        public PostTagController(IPostTagService service)
        {
            _postTagService = service;
        }

        [HttpPost]
        [Route("AddTagToPost")]
        public async Task<IActionResult> AddTagToPost(AddTagDTO addTagDTO)
        {
            return Ok(await _postTagService.AddTagToPost(addTagDTO));
        }
    }
}
