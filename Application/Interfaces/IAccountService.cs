using Application.DTO;
using Application.Model;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<string>> Register(RegisterRequest registerRequest);

 //       Task<Model.User> Register(RegsiterDTO dto);
        Task<DTO.AuthenticationResponse> Login(AuthenticationRequest authenticationRequest, string ipAddress);
          bool isLogged(string token);
    }
}

