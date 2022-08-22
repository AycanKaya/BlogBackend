using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTO.PostTagDTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.Wrappers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ResponseBase> AddTagToPost(AddTagDTO addTagDTO)
        {
            var isSucceed = await _postTagService.AddTagToPost(addTagDTO);
            if (!isSucceed)
                throw new System.Exception();
            return new ResponseBase()
            {
                Succeeded = isSucceed,
                Message = "This tag added to post.",
                StatusCode = 200
            };
        }
    }
}
