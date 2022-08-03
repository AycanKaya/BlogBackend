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
       
        private UserInfo FindUserInfos(string userID)
        {
            var userInfo= _context.UserInfo.Where(x => x.UserID == userID).FirstOrDefault();
            return userInfo;
        }
        private List<PostResponse> CreateResponse(List<Post> passivePosts)
        {
            var postResponse = new List<PostResponse>();
            foreach (var post in passivePosts)
            {
                var userInfo = FindUserInfos(post.AuthorID);
                var response = new PostResponse();
                response.Post = post;
                response.AuthorName = userInfo.Name;
                response.AuthorEmail = userInfo.Email;
                postResponse.Add(response);
            }
            return postResponse;
        }

        public async Task<List<PostResponse>> GetPassivePosts()
        {
            var passivePosts = _context.Posts.Where(c => c.IsApprove == false).ToList();
            if (passivePosts == null)
                throw new Exception("No passive post currently");
            var responseList = CreateResponse(passivePosts);

            return responseList;
        }
        public async Task<BaseResponse<string>> ActivatePost(ApproveControlDTO dto)
        {
            var post = _context.Posts.Where(c => c.Id == dto.PostID).FirstOrDefault();
            if (post == null)
                throw new Exception("Post not found !");
            post.IsApprove = dto.isApprove;
            if(dto.isApprove == false)
                post.IsDeleted= true;
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

