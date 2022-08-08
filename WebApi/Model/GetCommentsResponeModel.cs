using Application.Wrappers;
using Domain.Entities;

namespace WebApi.Model
{
    public class GetCommentsResponeModel :ResponseBase
    {
        public Comment[] Comments { get; set; }
        public GetCommentsResponeModel(Comment[] comments, bool succeed, string message, int statusCode)
        {
            Comments = comments;
            Succeeded = succeed;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
