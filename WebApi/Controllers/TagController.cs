using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using System.Threading.Tasks;
using Application.DTO.TagDTOs;
using Application.Wrappers;
using System.Net;
using WebApi.Model;
using Application.Model;
using Domain.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpPost]
        [Route("CreateTag")]
        public async Task<ResponseBase> CreateTag(CreateTagDTO createTagDTO)
        {
            var tag = await _tagService.CreateTag(createTagDTO);
            var response = new ResponseBase();
            if (tag)
            {
                response.setResponseMessage(true, createTagDTO.TagName + " is created.", 200);
                return response;
            }
            response.SetErrorMessage("Already Exists", false, "This tag already created!", (int)HttpStatusCode.Conflict);
            return response;
        }

        [HttpGet]
        [Route("AllTags")]
        public async Task<TagsResponseModel> GelAllTags()
        {
            var tags = await _tagService.GetAllTags();
            return new TagsResponseModel(tags, true, "All tags here", 200);
        }

        [HttpGet]
        [Route("GetPostsInTags")]
        public async Task<PostCommentsResponseModel> GetPostsInTags(string tags)
        {
            var posts = await _tagService.PostsInTags(tags);
            var response = new PostCommentsResponseModel(posts, true, "Posts in tags here", 200);
            return response;

        }
        [HttpGet]
        [Route("GetTagsInPost")]
        public async Task<TagsResponseModel> GetTagsInPost(int PostID)
        {
            var tags= await _tagService.GetTagsInPost(PostID);
            var response = new TagsResponseModel(tags, true, "succeeded", 200);
            return response;
        }

    }
}
