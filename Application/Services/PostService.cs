using System;
using System.Linq;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Application.Wrappers;
namespace Application.Services
{
    public class PostService :IPostService
    {
         IApplicationDbContext _context;
         IJWTService _jWTService;
   
        public PostService(IApplicationDbContext context, IJWTService service)
        {
            _jWTService = service;
            _context = context;
            
        }
        private int GetTotalPostNumber(string userId)
        {
            var number= _context.Posts.Where(x => x.AuthorID == userId).Count();
            return number;
        }
        private int GetAccountLevel(string userID)
        {
            var userAccountLevel = _context.UserAccountLevels.Where(x => x.UserID == userID).FirstOrDefault();
            var accountLevel = _context.AccountLevel.Where(x => x.Id == userAccountLevel.AccountLevelID).FirstOrDefault();
            return accountLevel.Level;
        }
        public async Task<Post> SharePost(string accessToken, PostDTO content)
        {

            var userId = _jWTService.GetUserIdFromJWT(accessToken);

            if (userId != null)
            {
               
                if (GetAccountLevel(userId) > GetTotalPostNumber(userId))
                {
                    var post = new Post();
                    post.AuthorID = userId;
                    post.isActive = true;
                    post.Content = content.Content;
                    post.Title = content.Title;
                    post.CreateTime = DateTime.Now;
                    post.IsApprove = false;
                    post.IsDeleted = false;
                    _context.Posts.Add(post);
                    await _context.SaveChanges();
                    return post;

                }
                throw new ExceptionResponse("You arrived the maximum count of post !");
               
            }
            throw new SecurityTokenValidationException();
        }
        
        public async Task<List<Post>> GetRecentFivePosts()
        {
           var postList = _context.Posts.OrderBy(post => post.CreateTime).TakeLast(5);
            if (postList == null)
                throw new ExceptionResponse("Post not found !");
            return postList.ToList();                                          

        }

        public async Task<List<Post>> GetUserPost(string token)
        {
            var userId = _jWTService.GetUserIdFromJWT(token);
            var postList = await _context.Posts.Where(x => x.AuthorID == userId & x.IsApprove == true & x.isActive==true).ToListAsync();
            return postList;

        }

        public async Task<BaseResponse<string>> DeletePost(int PostId, string token)
        {
            var userId = _jWTService.GetUserIdFromJWT(token);

            if (userId != null)
            {
                var post = _context.Posts.Where(x => x.Id == PostId || x.AuthorID == userId).FirstOrDefault();
                if (post == null)
                    throw new Exception("Post did not found ! ");
                post.IsDeleted = true;
                _context.SaveChanges();
                return new BaseResponse<string> { Message= "Post deleted " };

            }
            else
                throw new Exception("User not found !");

        }
        public async Task<Post> UpdatePost(UpdatePostDTO updatePostDTO)
        {
            var post = _context.Posts.Where(x => x.Id == updatePostDTO.PostId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            post.Content = updatePostDTO.Content;
            post.Title = updatePostDTO.Title;
            post.UpdateTime = DateTime.Now;
            await _context.SaveChanges();
            return post;

        }
        public async Task<Post> ChangePostState(string token, UpdatePostDTO updatePostDTO)
        {
            var authorId = _jWTService.GetUserIdFromJWT(token);
            if (authorId == null)
                throw new Exception("User not found ");

            var post = _context.Posts.Where(x => x.Id == updatePostDTO.PostId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            if (post.AuthorID == authorId)
            {
                post.isActive = updatePostDTO.isActive;
                await _context.SaveChanges();
                return post;
            }
            throw new Exception("Only users who share the post can change the post status!");

        }

        public async Task<List<Post>> GelAllPosts()
        {
            var postList = _context.Posts.ToList();
            return postList;
        }
    }
}
