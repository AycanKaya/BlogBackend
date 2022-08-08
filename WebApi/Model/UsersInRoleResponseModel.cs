using System.Collections.Generic;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Model
{
    public class UsersInRoleResponseModel :ResponseBase
    {
        public Dictionary<string, IdentityUser[]> RoleUsers { get; set; }

        public UsersInRoleResponseModel(Dictionary<string, IdentityUser[]> roleUsers, string message, bool succeed,int code)
        {
            RoleUsers = roleUsers;
            Message = message;
            Succeeded = succeed;
            StatusCode = code;
        }

    }
}
