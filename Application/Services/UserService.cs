using Application.Interfaces;
using Application.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IJWTService _jWTService;
        public UserService(IApplicationDbContext context, IJWTService jWTService)
        {
            _context = context;
            _jWTService = jWTService;

        }
        public async Task<Post> SharePost(string accessToken, PostDTO content)
        {
            var jwt = _jWTService.ValidateToken(accessToken);
            var userId = (jwt.Claims.First(x => x.Type == "uid").Value); 

            if (userId != null)
            {
                var post = new Post();
                post.AuthorID = userId;
                post.isActive = true;
                post.Content = content.Content;
                post.Title = content.Title;
                post.CreateTime = DateTime.Now;
                _context.Posts.Add(post);
                await _context.SaveChanges();
                return post;
            }
            throw new SecurityTokenValidationException();



        }

        public async Task<List<Post>> GetPosts(string token)
        {
            var jwt = _jWTService.ValidateToken(token);
            var userId = (jwt.Claims.First(x => x.Type == "uid").Value); 

            var postList = await _context.Posts.Where(x => x.AuthorID == userId).ToListAsync();
            return postList;
            
        }

        public async Task<int> DeletePost(int PostId, string token)
        {
            var jwt = _jWTService.ValidateToken(token);
            var userId = (jwt.Claims.First(x => x.Type == "uid").Value);

            if (userId != null)
            {
                var post = _context.Posts.Where(x => x.Id == PostId || x.AuthorID == userId).FirstOrDefault();
                if (post == null)
                    throw new Exception("Post  did not found ! ");
                _context.Posts.Remove(post);

                var commentList = _context.PostWithComments.Where(x => x.PostId == PostId).ToList();
                foreach (var comment in commentList)
                {
                    _context.PostWithComments.Remove(comment);
                }
                await _context.SaveChanges();
                return PostId;

            }
            else
                throw new Exception("User not found !");

        }
        public async Task<Post> UpdatePost(int PostId, PostDTO content)
        {
            var post = _context.Posts.Where(x => x.Id == PostId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            post.Content = content.Content;
            post.Title = content.Title;
            post.UpdateTime= DateTime.Now;
            await _context.SaveChanges();
            return post;

        }
        public async Task<Post> ChangeState(int PostId , string token, bool state)
        {
            var jwt= _jWTService.ValidateToken(token);
            var authorId = (jwt.Claims.First(x => x.Type == "uid").Value);
            if (authorId == null)
                throw new Exception("User not found ");

            var post = _context.Posts.Where(x => x.Id == PostId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            if (post.AuthorID == authorId)
            {
                post.isActive= state;
                await _context.SaveChanges();
                return post;
            }
            throw new Exception("Only users who share the post can change the post status!");
      
        }
        public async Task<PostWithComments> DeleteComment(int PostId,string token, int commentId)
        {
            var jwt = _jWTService.ValidateToken(token);
            var authorId = (jwt.Claims.First(x => x.Type == "uid").Value);
            if (authorId == null)
                throw new Exception("User not found ");

            var post = _context.Posts.Where(x => x.Id == PostId).FirstOrDefault();
            var comment = _context.Comments.Where(x => x.Id == commentId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            if(post.AuthorID == authorId || comment.AuthorId== authorId)
            {
                _context.Comments.Remove(comment);
                var postWithComment= _context.PostWithComments.Where(x => x.CommentId == commentId).FirstOrDefault();
                _context.PostWithComments.Remove(postWithComment);
                await _context.SaveChanges();
                return postWithComment;
            }
            throw new Exception("Only users who share the post or comment can delete the comment !");
        }



        public async Task<Comment> ShareComment(string token,CommentDTO commentContent)
        {
            var jwt = _jWTService.ValidateToken(token);
            var userId = (jwt.Claims.First(x => x.Type == "uid").Value);
            if (userId == null)
                throw new Exception("User not found ");

            var comment = new Comment();
            comment.AuthorName = jwt.Claims.First(x => x.Type == "sub").Value;
            comment.Content = commentContent.Content;
            comment.PostID = commentContent.PostID;
            comment.AuthorId = userId;
            comment.Created= DateTime.Now;
            _context.Comments.Add(comment);

            var commentToPost = new PostWithComments();
            commentToPost.PostId = comment.PostID;
            commentToPost.CommentId= comment.Id;
            _context.PostWithComments.Add(commentToPost);
          
            await _context.SaveChanges();
            return comment;
            
        }
        public async Task<List<Comment>> GetComments(int postId)
        {
            var commentList =await _context.Comments.Where(x => x.PostID == postId).ToListAsync();
            return commentList;
        }



    }
}
