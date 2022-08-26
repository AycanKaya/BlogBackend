using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.DTO.PostServiceDTOs;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces
{
    public  interface IPostService
    {
        Task<PostResponseDTO> SharePost(string userId, PostDTO content);
        Task<bool> DeletePost(int PostId, string token);
        Task<bool> UpdatePost(UpdatePostDTO updatePostDTO, string token);
        Task<bool> ChangePostState(string token, UpdatePostDTO updatePostDTO);

        Task<PostCommentsDTO[]> GetUserPost(string token);
        
        Task<PostResponseDTO[]> GelAllPosts();
        Task<PostCommentsDTO[]> GetPostWithComments();
        Task<PostResponseDTO[]> WaitingUserPost(string token);
        Task<PostResponseDTO[]> CancelledUserPosts(string token);
        Task<PostResponseDTO> GetPost(int postId);
        Task<PostResponseDTO[]> GetSharedPost(string token);
    }
}
