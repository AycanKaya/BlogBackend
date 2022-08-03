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
    public  interface IPostService
    {
        Task<Post> SharePost(string userId, PostDTO content);
        Task<BaseResponse<string>> DeletePost(int PostId, string token);
        Task<Post> UpdatePost(UpdatePostDTO updatePostDTO);
        Task<Post> ChangePostState(string token, UpdatePostDTO updatePostDTO);

        Task<List<Post>> GetUserPost(string token);
        Task<IEnumerable<Post>> GetRecentFivePosts();
        Task<List<Post>> GelAllPosts();

    }
}
