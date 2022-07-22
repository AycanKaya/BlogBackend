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
         Task<BaseResponse<UserInfo>> SettingUserInfo(UserInfoDTO dto, string token);
         Task<BaseResponse<UserInfo>> GetUserInfoAsync(string token);
    }
}

