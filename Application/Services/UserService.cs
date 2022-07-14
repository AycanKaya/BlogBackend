using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        
        public UserService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Post> SharePost(string userId, PostDTO content)
        {
            var post = new Post();
            post.AuthorID = userId;
            post.Content = content.Content;
            post.Title = content.Title;
            post.CreateTime= DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChanges();
            return post;
        }
        public async Task<int> DeletePost(int PostId)
        {
            var post = _context.Posts.Where(x => x.Id == PostId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
             _context.Posts.Remove(post);

            var commentList= _context.PostWithComments.Where(x => x.PostId == PostId).ToList();
            foreach(var comment in commentList)
            {
                _context.PostWithComments.Remove(comment);
            }

            await _context.SaveChanges();
            return PostId;

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

        public async Task<Comment> ShareComment(string userName,CommentDTO commentContent)
        {
            var comment = new Comment();
            comment.AuthorName = userName;
            comment.Content = commentContent.Content;
            comment.PostID = commentContent.PostID;
            comment.Created= DateTime.Now;
            _context.Comments.Add(comment);

            var commentToPost = new PostWithComments();
            commentToPost.PostId = comment.PostID;
            commentToPost.CommentId= comment.Id;
            _context.PostWithComments.Add(commentToPost);
          
            await _context.SaveChanges();
            return comment;
            
        }


    }
}
