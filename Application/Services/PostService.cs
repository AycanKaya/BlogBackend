using System;
using System.Linq;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Application.Wrappers;
using Application.DTO.PostServiceDTOs;

namespace Application.Services
{
    public class PostService : IPostService
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
            var number = _context.Posts.Where(x => x.AuthorID == userId).Count();
            return number;
        }
        private int GetAccountLevel(string userID)
        {
            var userAccountLevel = _context.UserAccountLevels.Where(x => x.UserID == userID).FirstOrDefault();
            var accountLevel = _context.AccountLevel.Where(x => x.Id == userAccountLevel.AccountLevelID).FirstOrDefault();
            return accountLevel.Level;
        }

        public async Task<PostResponseDTO> SharePost(string accessToken, PostDTO content)
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

                    var author = _context.UserInfo.Where(x => x.UserID == post.AuthorID).FirstOrDefault();
                    return new PostResponseDTO
                    {
                        AuthorName = author.Name,
                        AuthorEmail = author.Email,
                        Title = post.Title,
                        Content = post.Content,
                        IsApprove = post.IsApprove,
                        IsDeleted = post.IsDeleted,
                        CreateTime = post.CreateTime,
                        UpdateTime = post.UpdateTime,
                    };
                }
                throw new ExceptionResponse("You arrived the maximum count of post !");

            }
            throw new SecurityTokenValidationException();
        }



        public async Task<PostCommentsDTO[]> GetUserPost(string token)
        {
            var userId = _jWTService.GetUserIdFromJWT(token);
            var posts = _context.Posts.Where(x => x.AuthorID == userId & x.IsApprove == true & x.isActive == true);
            var postList = await posts.Join(_context.UserInfo,
               post => post.AuthorID,
               userInfo => userInfo.UserID,
               (post, userInfo) => new PostResponseDTO
               {
                   PostId = post.Id,
                   AuthorName = userInfo.Name,
                   AuthorEmail = userInfo.Email,
                   Title = post.Title,
                   Content = post.Content,
                   IsApprove = post.IsApprove,
                   IsDeleted = post.IsDeleted,
                   CreateTime = post.CreateTime,
                   UpdateTime = post.UpdateTime,
               }).ToArrayAsync();
            List<PostCommentsDTO> list = new List<PostCommentsDTO>();
            foreach (var post in postList)
            {
                var comments = await _context.Comments.Where(x => x.PostID == post.PostId).ToArrayAsync();
                var postComments = new PostCommentsDTO();
                postComments.Post = post;
                postComments.Comments = comments;
                list.Add(postComments);
            }
            return list.ToArray();


        }

        public async Task<bool> DeletePost(int PostId, string token)
        {
            var userId = _jWTService.GetUserIdFromJWT(token);
            var post = _context.Posts.Where(x => x.Id == PostId || x.AuthorID == userId).FirstOrDefault();
            if (post == null)
                return false;
            post.IsDeleted = true;
            post.isActive = false;
            await _context.SaveChanges();
            return true;


        }
        public async Task<bool> UpdatePost(UpdatePostDTO updatePostDTO, string token)
        {
            var authorId = _jWTService.GetUserIdFromJWT(token);
            var post = _context.Posts.Where(x => x.Id == updatePostDTO.PostId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            if(authorId == post.AuthorID)
            {
                post.Content = updatePostDTO.Content;
                post.Title = updatePostDTO.Title;
                post.UpdateTime = DateTime.Now;
                await _context.SaveChanges();
                return true;
            }
            return false;
            

        }
        public async Task<bool> ChangePostState(string token, UpdatePostDTO updatePostDTO)
        {
            var authorId = _jWTService.GetUserIdFromJWT(token);
        
            var post = _context.Posts.Where(x => x.Id == updatePostDTO.PostId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            if (post.AuthorID == authorId)
            {
                post.isActive = updatePostDTO.isActive;
                await _context.SaveChanges();
                return true;
            }
            throw new Exception("Only users who share the post can change the post status!");

        }

        public async Task<PostResponseDTO[]> GelAllPosts()
        {
            var postList = _context.Posts;
            return await postList
               .Join(_context.UserInfo,
               post => post.AuthorID,
               userInfo => userInfo.UserID,
               (post, userInfo) => new PostResponseDTO
               {
                   PostId = post.Id,
                   AuthorName = userInfo.Name,
                   AuthorEmail = userInfo.Email,
                   Title = post.Title,
                   Content = post.Content,
                   IsApprove = post.IsApprove,
                   IsDeleted = post.IsDeleted,
                   CreateTime = post.CreateTime,
                   UpdateTime = post.UpdateTime,
               }).ToArrayAsync();
        }

        public async Task<PostCommentsDTO[]> GetPostWithComments()
        {
            var posts = _context.Posts;
            var postList = await PostList(posts);
            List<PostCommentsDTO> list = new List<PostCommentsDTO>();
            foreach (var post in postList)
            {
                var comments = await _context.Comments.Where(x => x.PostID == post.PostId).ToArrayAsync();
                var postComments = new PostCommentsDTO();
                postComments.Post = post;
                postComments.Comments = comments;
                list.Add(postComments);
            }
            return list.ToArray();

        }
      

        private async Task<PostResponseDTO[]> PostList(DbSet<Post> postList)
        {            
            return await postList
               .Join(_context.UserInfo,
               post => post.AuthorID,
               userInfo => userInfo.UserID,
               (post, userInfo) => new PostResponseDTO
               {
                   PostId = post.Id,
                   AuthorName = userInfo.Name,
                   AuthorEmail = userInfo.Email,
                   Title = post.Title,
                   Content = post.Content,
                   IsApprove = post.IsApprove,
                   IsDeleted = post.IsDeleted,
                   CreateTime = post.CreateTime,
                   UpdateTime = post.UpdateTime,
               }).ToArrayAsync();
        }
    }
}
