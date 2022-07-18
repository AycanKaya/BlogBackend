using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Post> SharePost(string userId, PostDTO content);
        Task<int> DeletePost(int PostId, string token);
        Task<Post> UpdatePost(int PostId, PostDTO content);
        Task<Post> ChangeState(int PostId, string token, bool state);
        Task<PostWithComments> DeleteComment(int PostId, string token, int commentId);
        Task<Comment> ShareComment(string token, CommentDTO commentContent);
        Task<List<Post>> GetPosts(string token);
        Task<List<Comment>> GetComments(int postId);



    }
}
