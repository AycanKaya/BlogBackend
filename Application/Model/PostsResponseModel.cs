using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Wrappers;

namespace Application.Model
{
    public class PostsResponseModel :ResponseBase
    {
        public PostResponseDTO[]? Posts { get; set; }
        public PostsResponseModel(PostResponseDTO[] posts, bool succeed, string message,int code)
        {
            Posts = posts;
            Succeeded = succeed;
            Message = message;
            StatusCode = code;
        }
    }
}
