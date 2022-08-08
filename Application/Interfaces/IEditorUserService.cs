using System;
using Application.DTO;
using Application.DTO.EditorUserDTOs;
using Application.Wrappers;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface IEditorUserService
    {
        Task<bool> DeleteComment(int commentID);
        Task<bool> ActivatePost(ApproveControlDTO dto);
        Task<PostResponseDTO[]> GetPassivePosts();
    }
}
