using Application.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Model
{
    public class GetUsersResponseModel : ResponseBase
    {
        public IdentityUser[] Users { get; set; }

        public GetUsersResponseModel(IdentityUser[] users, bool succeed, string message, int statusCode)
        {
            Users = users;
            Message = message;
            StatusCode = statusCode;
            Succeeded = succeed;
        }
    }
}
