using System;
using Application.DTO;
using Application.DTO.EditorUserDTOs;
using Application.Wrappers;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IEditorUserService
    {
        Task<BaseResponse<string>> DeleteComment(int commentID);
        Task<BaseResponse<string>> ActivatePost(ApproveControlDTO dto);
        Task<List<Post>> GetPassivePosts();
    }
}
