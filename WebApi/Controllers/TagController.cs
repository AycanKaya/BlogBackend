using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System.Threading.Tasks;
using Application.DTO.TagDTOs;

namespace WebApi.Controllers
{
    public class TagController : ControllerBase
    {
        ITagService _tagService;
        
        public  TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpPost]
        [Route("CreateTag")]
        public async Task<IActionResult> CreateTag(CreateTagDTO createTagDTO)
        {
            return Ok(await _tagService.CreateTag(createTagDTO));

        }
        [HttpGet]
        [Route("AllTags")]
        public async Task<IActionResult> GelAllTags()
        {
            return Ok(await _tagService.GetAllTags());
        }
    }
}
