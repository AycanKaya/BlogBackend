using System;
using System.Linq;
using Application.DTO;
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
        public async Task<PostResponseDTO[]> GetPassivePosts()
        {
            var passivePosts = _context.Posts.Where(c => c.IsApprove == false);
            return passivePosts
                .Join(_context.UserInfo,
                post => post.AuthorID,
                userInfo => userInfo.UserID,
                (post, userInfo) => new PostResponseDTO
                {
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
        public async Task<bool> ActivatePost(ApproveControlDTO dto)
        {
            var post = _context.Posts.Where(c => c.Id == dto.PostID).FirstOrDefault();
            post.IsApprove = dto.isApprove;
            if (dto.isApprove == false)
                post.IsDeleted = true;
            await _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteComment(int commentID)
        {
            var comment = _context.Comments.Where(c => c.Id == commentID).FirstOrDefault();
            if (comment == null)
                return false;
            comment.IsDeleted = true;
            await _context.SaveChanges();
            return true;

        }


    }
}

