using Application.DTO;
using Application.Model;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.DTO.AccountServiceDTOs;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(RegisterRequest registerRequest);
        Task<DTO.AuthenticationResponseDTO> Login(AuthenticationRequest authenticationRequest, string ipAddress);
         bool isLogged(string token);
        Task<UserInfo> GetUserInfoAsync(string token);
        Task<UserInfo> SettingUserInfo(UserInfoDTO dto, string token);
        Task<UserInfo[]> GetAllUserInfo();
        Task<string> GetCurrentUserRole(string token);
        Task<bool> ResetPassword(ResetPasswordDTO resetPassword);
        AccountLevelResponseDTO GetUserLevel(string id);
        Task<UserInfo> GetUserInfo(string id);

    }
}

