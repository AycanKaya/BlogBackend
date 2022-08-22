using Application.DTO.AccountServiceDTOs;
using Application.Wrappers;

namespace WebApi.Model
{
    public class AccountLevelResponseModel :ResponseBase
    {
        public AccountLevelResponseDTO Level { get; set; }

        public AccountLevelResponseModel(AccountLevelResponseDTO level, bool succeed, string message,int code)
        {
            Level = level;
            Succeeded = succeed;
            Message = message;
            StatusCode = code;
        }
    }
}
