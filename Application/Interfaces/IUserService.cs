using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {

        string GetUserIdFromJWT(string token);
        string GetUserName(string token);



    }
}
