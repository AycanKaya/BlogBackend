using Application.DTO;
using Application.DTO.PostServiceDTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<PostResponseDTO> GetPost(int postId)
        {
            var post = _context.Posts.Where(x => x.Id == postId);
            return await post
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
              }).FirstOrDefaultAsync();

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
                        PostId=post.Id,
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

        public async Task<PostResponseDTO[]> GetSharedPost(string email)
        {
            //  var userId = _jWTService.GetUserIdFromJWT(token);
            var user = _context.UserInfo.Where(c => c.Email == email).FirstOrDefault();
            var posts = _context.Posts.Where(c => c.AuthorID == user.UserID & c.IsDeleted == false & c.IsApprove == true);
            return await posts.Join(_context.UserInfo,
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
            var post = _context.Posts.Where(x => x.Id == PostId && x.AuthorID == userId).FirstOrDefault();

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

            if (authorId == post.AuthorID)
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
            var postList = _context.Posts.Where(x=> x.isActive==true && x.IsApprove==true && x.IsDeleted == false);
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
            var posts = _context.Posts.Where(c => c.IsDeleted == false & c.IsApprove == true & c.isActive == true);
            var postList = await PostList(posts);
            List<PostCommentsDTO> list = new List<PostCommentsDTO>();
            foreach (var post in postList)
            {
                var comments = await _context.Comments.Where(x => x.PostID == post.PostId && x.IsDeleted==false).ToArrayAsync();
                var postComments = new PostCommentsDTO();
                postComments.Post = post;
                postComments.Comments = comments;
                list.Add(postComments);
            }
            return list.ToArray();

        }
        public async Task<PostResponseDTO[]> WaitingUserPost(string email)
        {
            // var userId = _jWTService.GetUserIdFromJWT(token);
            var user = _context.UserInfo.Where(c => c.Email == email).FirstOrDefault();
            var postList = _context.Posts.Where(c => c.AuthorID == user.UserID & c.IsDeleted == false & c.IsApprove == false);
            return postList
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
               }).ToArray();
        }
        public async Task<PostResponseDTO[]> CancelledUserPosts(string token)
        {
            var userId = _jWTService.GetUserIdFromJWT(token);
            var postList = _context.Posts.Where(c => c.AuthorID == userId & c.IsDeleted == true & c.IsApprove == false);
            return await PostList(postList);
        }

        private async Task<PostResponseDTO[]> PostList(IQueryable<Post> postList)
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
