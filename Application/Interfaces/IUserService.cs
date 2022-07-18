using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Post> SharePost(string userId, PostDTO content);
        Task<int> DeletePost(int PostId);
        Task<Post> UpdatePost(int PostId, PostDTO content);
        Task<Post> ChangeState(int PostId, string AuthorID, bool state);
        Task<PostWithComments> DeleteComment(int PostId, string AuthorID, int commentId);
        Task<Comment> ShareComment(string userName, string userId, CommentDTO commentContent);
        Task<List<Post>> GetPosts(string userId);
        Task<List<Comment>> GetComments(int postId);



    }
}
