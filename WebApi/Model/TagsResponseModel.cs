using Application.Wrappers;
using Domain.Entities;

namespace WebApi.Model
{
    public class TagsResponseModel :ResponseBase
    {
        public Tag[] Tags { get; set; }
        public TagsResponseModel(Tag[] tags,bool succeed, string message,int code)
        {
            Tags = tags;
            Message = message; 
            Succeeded = succeed;
            StatusCode = code;

        }
    }
}
