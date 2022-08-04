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


        }   public async Task<List<PostResponseDTO>> GetPassivePosts()
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
                    }).ToList();
            }
            public async Task<BaseResponse<string>> ActivatePost(ApproveControlDTO dto)
            {
                  var post = _context.Posts.Where(c => c.Id == dto.PostID).FirstOrDefault();
                    post.IsApprove = dto.isApprove;
                    if (dto.isApprove == false)
                        post.IsDeleted = true;
                    await _context.SaveChanges();
                  return new BaseResponse<string> { Message = "Comment Deleted !", Succeeded = true, Errors = null, Body = post.Id.ToString() };
                }
    
               
            
            public async Task<BaseResponse<string>> DeleteComment(int commentID)
            {
                var comment = _context.Comments.Where(c => c.Id == commentID).FirstOrDefault();
                if (comment == null)
                    throw new Exception("Comment not found !");
                comment.IsDeleted = true;
                await _context.SaveChanges();
                return new BaseResponse<string> { Message = "Comment Deleted !", Succeeded = true, Errors = null, Body = comment.Id.ToString() };

            }

        }
    }

