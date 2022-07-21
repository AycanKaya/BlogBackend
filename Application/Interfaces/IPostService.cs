using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public  interface IPostService
    {
        Task<Post> SharePost(string userId, PostDTO content);
        Task<int> DeletePost(int PostId, string token);
        Task<Post> UpdatePost(int PostId, PostDTO content);
        Task<Post> ChangePostState(int PostId, string token, bool state);

        Task<List<Post>> GetPosts(string token);

    }
}
