using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        IApplicationDbContext _context;
        IJWTService _jWTService;

        public CommentService(IApplicationDbContext context, IJWTService service)
        {
            _jWTService = service;
            _context = context;

        }
        public async Task<PostWithComments> DeleteComment(PostWithCommentsDTO postWithComments, string token)
        {
            
            var authorId = _jWTService.GetUserIdFromJWT(token);
            if (authorId == null)
                throw new Exception("User not found ");

            var post = _context.Posts.Where(x => x.Id == postWithComments.PostId).FirstOrDefault();
            var comment = _context.Comments.Where(x => x.Id == postWithComments.CommentId).FirstOrDefault();
            if (post == null)
                throw new Exception("Post  did not found ! ");
            if (post.AuthorID == authorId || comment.AuthorId == authorId)
            {
                _context.Comments.Remove(comment);
                var postWithComment = _context.PostWithComments.Where(x => x.CommentId == postWithComments.CommentId).FirstOrDefault();
                _context.PostWithComments.Remove(postWithComment);
                await _context.SaveChanges();
                return postWithComment;
            }
            throw new Exception("Only users who share the post or comment can delete the comment !");
        }



        public async Task<Comment> ShareComment(string token, CommentDTO commentContent)
        {
            var userId = _jWTService.GetUserIdFromJWT(token);
            if (userId == null)
                throw new Exception("User not found ");

            var comment = new Comment();
            comment.AuthorName = _jWTService.GetUserName(token);
            comment.Content = commentContent.Content;
            comment.PostID = commentContent.PostID;
            comment.AuthorId = userId;
            comment.Created = DateTime.Now;
            _context.Comments.Add(comment);

            var commentToPost = new PostWithComments();
            commentToPost.PostId = comment.PostID;
            commentToPost.CommentId = comment.Id;
            _context.PostWithComments.Add(commentToPost);

            await _context.SaveChanges();
            return comment;

        }
        public async Task<List<Comment>> GetComments(int postId)
        {
            var commentList = await _context.Comments.Where(x => x.PostID == postId).ToListAsync();
            return commentList;
        }

    }
}