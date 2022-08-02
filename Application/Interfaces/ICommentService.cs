using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface  ICommentService
    {
        Task<BaseResponse<string>> DeleteComment(int commentID, string token);
        Task<Comment> ShareComment(string token, CommentDTO commentContent);
        Task<List<Comment>> GetComments(int postId);

    }
}
