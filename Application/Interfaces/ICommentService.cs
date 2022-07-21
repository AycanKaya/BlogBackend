using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface  ICommentService
    {
        Task<PostWithComments> DeleteComment(int PostId, string token, int commentId);
        Task<Comment> ShareComment(string token, CommentDTO commentContent);
        Task<List<Comment>> GetComments(int postId);

    }
}
