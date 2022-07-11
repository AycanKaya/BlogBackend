using Application.DTO;
using Application.Model;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseModel> Register(RegisterRequest registerRequest);

 //       Task<Model.User> Register(RegsiterDTO dto);
        Task<ResponseModel> Login(AuthenticationRequest authenticationRequest);
    }
}

