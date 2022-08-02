using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.TagDTOs;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITagService
    {
        Task<BaseResponse<Tag>> CreateTag(CreateTagDTO createTag);
        Task<List<Tag>> GetAllTags();

    }
}
