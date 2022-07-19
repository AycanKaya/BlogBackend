using Application.DTO;
using Application.Model;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseModel> Register(RegisterRequest registerRequest);

 //       Task<Model.User> Register(RegsiterDTO dto);
          Task<AuthenticationResponse> Login(AuthenticationRequest authenticationRequest, string ipAddress);
          bool isLogged(string token);
    }
}

