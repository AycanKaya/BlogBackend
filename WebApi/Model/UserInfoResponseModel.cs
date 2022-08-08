using Application.DTO;
using Application.Wrappers;
using Domain.Entities;

namespace WebApi.Model
{
    public class UserInfoResponseModel : ResponseBase
    {
       public UserInfo UserInfo { get; set; }
        public UserInfoResponseModel(UserInfo userInfo, bool succed, string message, int statusCode)
        {
            UserInfo = userInfo;
            Succeeded = succed;
            Message = message;
            StatusCode = statusCode;

        
        }
    }
}
