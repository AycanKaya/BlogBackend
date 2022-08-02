using Application.DTO;
using Application.Model;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<string>> Register(RegisterRequest registerRequest);
        Task<DTO.AuthenticationResponse> Login(AuthenticationRequest authenticationRequest, string ipAddress);
         bool isLogged(string token);
        Task<UserInfo> GetUserInfoAsync(string token);
        Task<UserInfo> SettingUserInfo(UserInfoDTO dto, string token);
        Task<List<UserInfo>> GetAllUserInfo();
        Task<string> GetCurrentUserRole(string token);
        Task<BaseResponse<IdentityUser>> ResetPassword(ResetPasswordDTO resetPassword);
      

    }
}

