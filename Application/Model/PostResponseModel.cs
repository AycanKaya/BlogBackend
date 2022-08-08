using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Wrappers;

namespace Application.Model
{
    public class PostResponseModel : ResponseBase
    {   
        public PostResponseDTO? Post { get; set; }
        public PostResponseModel(PostResponseDTO post,bool succeed, string message, int code)
        {
            Post = post;
            Succeeded = succeed;
            Message = message;
            StatusCode = code;
        }

    }
}
