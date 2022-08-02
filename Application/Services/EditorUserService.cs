using System;
using System.Linq;
using Application.DTO.EditorUserDTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Services
{
    public class EditorUserService : IEditorUserService
    {
        IApplicationDbContext _context;

        public EditorUserService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetPassivePosts()
        {
            var passivePosts = _context.Posts.Where(c => c.IsApprove == false).ToList();
            if (passivePosts == null)
                throw new Exception("No passive post currently");

            return passivePosts;
        }
        public async Task<BaseResponse<string>> ActivatePost(ApproveControlDTO dto)
        {
            var post = _context.Posts.Where(c => c.Id == dto.PostID).FirstOrDefault();
            if (post == null)
                throw new Exception("Post not found !");
            post.IsApprove = dto.isApprove;
            await _context.SaveChanges();
            return new BaseResponse<string> { Message = "Post activated !", Succeeded = true,Errors = null, Data=post.Id.ToString()};
        }
        public async Task<BaseResponse<string>> DeleteComment(int commentID)
        {
            var comment = _context.Comments.Where(c => c.Id == commentID).FirstOrDefault();
            if(comment == null)
                throw new Exception("Comment not found !");
            comment.IsDeleted = true;
            await _context.SaveChanges();
            return new BaseResponse<string> { Message = "Comment Deleted !",Succeeded = true, Errors = null,Data=comment.Id.ToString() };

        }

    }
}

