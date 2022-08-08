using Application.DTO;
using Application.Wrappers;

namespace WebApi.Model
{
    public class LoginResponseModel : ResponseBase
    {
        public AuthenticationResponseDTO User { get; set; }
        public LoginResponseModel(AuthenticationResponseDTO user)
        {
            User = user;
        }
            
            
    }
}
