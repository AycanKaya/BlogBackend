using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.PostTagDTOs;
using Application.Wrappers;

namespace Application.Interfaces
{
    public  interface IPostTagService
    {
        Task<BaseResponse<string>> AddTagToPost(AddTagDTO addTagDTO);

    }
}
