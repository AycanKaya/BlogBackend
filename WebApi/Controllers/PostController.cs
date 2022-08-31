using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.DTO;
using Application.Wrappers;
using Domain.Entities;

using Microsoft.Extensions.Logging;
using Application.Model;
using System.Net;
using WebApi.Model;
using System;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]


    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;
        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpPost("PostUser")]
        public async Task<PostResponseModel> CreatePost(PostDTO postDTO)
        {
            if(postDTO.Content.Equals("") || postDTO.Title.Equals(""))
            {
                throw new  Exception("Values cannot be null!");
            }
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var post = await _postService.SharePost(token, postDTO);
            return new PostResponseModel(post, true, "succeed", 200);
        }

        [HttpGet("id")]
        public async Task<PostResponseModel> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            return new PostResponseModel(post, true, "succeed" , 200);
        }

        [HttpDelete]
        [Route("DeletePost")]
        public async Task<ResponseBase> DeletePost(int id)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var isSucced = await _postService.DeletePost(id, token);
            var response = new ResponseBase();
            response.Succeeded = isSucced;
            if (isSucced)
            {

                response.Message = "Post Deleted.";
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Error = null;
                return response;
            }
            response.SetErrorMessage("BAD REQUEST", false,
                "Only author can deleted this post !",
                (int)HttpStatusCode.BadRequest);
            return response;
        }

        [HttpPut("UpdatePost")]
        public async Task<ResponseBase> UpdatePost(UpdatePostDTO updatePostDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var isUpdate = await _postService.UpdatePost(updatePostDTO, token);
            var response = new ResponseBase();
            if (isUpdate)
            {
                response.setResponseMessage(true, "Post Updated!", 200);
                return response;
            }
            response.SetErrorMessage("Not authorize", false,
                                    "Only posts author can be change on this post.",
                                    (int)HttpStatusCode.BadRequest);
            return response;
        }

        [HttpPost("ChangeStatus")]
        public async Task<ResponseBase> ChangeState(UpdatePostDTO updatePostDTO)
        {
            _logger.LogInformation("cahnge Method Call");
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var isChange = await _postService.ChangePostState(token, updatePostDTO);
            var response = new ResponseBase();
            if (isChange)
            {
                response.setResponseMessage(true, "State changed.", 200);
                return response;
            }
            response.SetErrorMessage("Not authorize", false,
                "Only author can be change",
                (int)HttpStatusCode.BadRequest);
            return response;
        }

        [HttpGet("GetUserPosts")]
        public async Task<PostCommentsResponseModel> GetUserPosts()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var postList = await _postService.GetUserPost(token);
            return new PostCommentsResponseModel(postList, true, "successful", 200);
        }

        [HttpGet("AllPosts")]
        public async Task<PostsResponseModel> GetAllPosts()
        {
            var posts = (await _postService.GelAllPosts());
            return new PostsResponseModel(posts, true, "successful", 200);
        }

        [HttpGet("PostComments")]
        public async Task<PostCommentsResponseModel> GetPostComments()
        {
            var posts = await _postService.GetPostWithComments();
            return new PostCommentsResponseModel(posts, true, "All posts and comments", 200);
        }

        [HttpGet("SharedPosts")]
        public async Task<PostsResponseModel> SharedPosts(string email)
        {
           // var token = HttpContext.Request.Headers.Authorization.ToString();
            var posts = await _postService.GetSharedPost(email);
            return new PostsResponseModel(posts, true, "Shared posts here.", 200);
        }


        [HttpGet("WaitingPosts")]
        public async Task<PostsResponseModel> WaitingPosts(string email)
        {
          //   var token = HttpContext.Request.Headers.Authorization.ToString();
            var posts = await _postService.WaitingUserPost(email);
            return new PostsResponseModel(posts, true, "Waiting posts here.", 200);
        }
        [HttpGet("CancelledPosts")]
        public async Task<PostsResponseModel> CancelledPosts()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var posts = await _postService.CancelledUserPosts(token);
            return new PostsResponseModel(posts, true, "Cancelled posts here.", 200);
        }
    }


}





