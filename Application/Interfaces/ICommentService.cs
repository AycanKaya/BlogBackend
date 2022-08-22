using Application.DTO;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface  ICommentService
    {
        Task<bool> DeleteComment(int commentID, string token);
        Task<Comment> ShareComment(string token, CommentDTO commentContent);
        Task<Comment[]> GetComments(int postId);
        Task<bool> UpdateComment(UpdateCommentDTO updateComment,string token);

    }
}
