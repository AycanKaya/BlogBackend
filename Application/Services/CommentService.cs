using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Application.Wrappers;
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
        public async Task<bool> DeleteComment(int commentID, string token)
        {
            
            var authorId = _jWTService.GetUserIdFromJWT(token);
            if (authorId == null)
                throw new Exception("User not found ");

            var comment = _context.Comments.Where(x => x.Id == commentID).FirstOrDefault();
           
            if ( comment.AuthorId == authorId)
            {
                comment.IsDeleted = true;
                await _context.SaveChanges();
                return true;
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
            comment.IsDeleted = false;
            _context.Comments.Add(comment);

            await _context.SaveChanges();
            return comment;

        }
        public async Task<Comment[]> GetComments(int postId)
        {
            var commentList = await _context.Comments.Where(x => x.PostID == postId && x.IsDeleted==false).ToArrayAsync();
            return commentList;
        }

        public async Task<bool> UpdateComment(UpdateCommentDTO updateComment, string token)
        {
            var userId = _jWTService.GetUserIdFromJWT(token);
            if (userId == null)
                throw new Exception("User not found ");

            var comment = _context.Comments.Where(x => x.Id == updateComment.CommentID && x.AuthorId==userId).FirstOrDefault();

            if (comment == null)
                throw new BadHttpRequestException("Only the commenter can update the comment");

            comment.Content = updateComment.Content;
            await _context.SaveChanges();
            return true;

        }

    }
}