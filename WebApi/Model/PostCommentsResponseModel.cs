using System.Collections.Generic;
using Application.DTO;
using Application.DTO.PostServiceDTOs;
using Application.Wrappers;
using Domain.Entities;

namespace WebApi.Model
{
    public class PostCommentsResponseModel :ResponseBase 
    {
        public PostCommentsDTO[] Posts { get; set; }

        public PostCommentsResponseModel(PostCommentsDTO[] post, bool succeed, string message, int code)
        {
            Posts = post;
            Succeeded = succeed;
            Message = message;
            StatusCode = code;
        }
    }
}
