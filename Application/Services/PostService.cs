using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<Post> SharePost(string accessToken, PostDTO content)
        {

            var userId = _jWTService.GetUserIdFromJWT(accessToken);

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
            var userId = _jWTService.GetUserIdFromJWT(token);

            var postList = await _context.Posts.Where(x => x.AuthorID == userId).ToListAsync();
            return postList;

        }

        public async Task<Post> DeletePost(int PostId, string token)
        {
            var userId = _jWTService.GetUserIdFromJWT(token);

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
                return post;

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
            post.UpdateTime = DateTime.Now;
            await _context.SaveChanges();
            return post;

        }
        public async Task<Post> ChangePostState(int PostId, string token, bool state)
        {
            var authorId = _jWTService.GetUserIdFromJWT(token);
            if (authorId == null)
                throw new Exception("User not found ");

            var post = _context.Posts.Where(x => x.Id == PostId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            if (post.AuthorID == authorId)
            {
                post.isActive = state;
                await _context.SaveChanges();
                return post;
            }
            throw new Exception("Only users who share the post can change the post status!");

        }
    }
}
