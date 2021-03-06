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
    public class PostController :BaseApiController
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("PostUser")]
        public async Task<BaseResponse<Post>> CreatePost(PostDTO postDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var res= await _postService.SharePost(token, postDTO);
            return new BaseResponse<Post>(res);
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse<Post>> DeletePost(int id)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return new BaseResponse<Post>(await _postService.DeletePost(id, token));
        }

        [HttpPut]
        public async Task<BaseResponse<Post>> UpdatePost(UpdatePostDTO updatePostDTO){
            return new BaseResponse<Post>(await _postService.UpdatePost(updatePostDTO));
        }
        
        [HttpPost("ChangeStatus")]
        public async Task<BaseResponse<Post>> ChangeState(UpdatePostDTO updatePostDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return new BaseResponse<Post>(await _postService.ChangePostState(token, updatePostDTO));
        }
        [HttpGet("GetAllPosts")]
        public async Task<BaseResponse<List<Post>>> GetAllPosts()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var postList= await _postService.GetPosts(token);
            return new BaseResponse<List<Post>>(postList);
        }


    }
}



