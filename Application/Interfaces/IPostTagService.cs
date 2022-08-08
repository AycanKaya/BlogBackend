using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.PostTagDTOs;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces
{
    public  interface IPostTagService
    {
        Task<bool> AddTagToPost(AddTagDTO addTag);
        Task<Tag[]> GetPostWithTag(int postID);

    }
}
