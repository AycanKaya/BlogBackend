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
        Task<bool> CreateTag(CreateTagDTO createTag);
        Task<Tag[]> GetAllTags();

    }
}
