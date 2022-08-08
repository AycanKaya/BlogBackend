using Application.Wrappers;
using Domain.Entities;

namespace WebApi.Model
{
    public class AllUserInfoResponseModel :ResponseBase
    {
        public UserInfo[] userInfos { get; set; }

    }
}
