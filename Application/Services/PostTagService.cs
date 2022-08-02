using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.PostTagDTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Services
{
    public class PostTagService : IPostTagService
    {
        IApplicationDbContext _context;
        public PostTagService(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<BaseResponse<string>> AddTagToPost(AddTagDTO addTagDTO)
        {
            var tagPost = new PostTag();
            tagPost.PostID = addTagDTO.PostID;
            tagPost.TagID = addTagDTO.TagID;
            _context.PostTags.Add(tagPost);
            await _context.SaveChanges();
            return new BaseResponse<string> {Message="Tag added to Post", Succeeded=true};

        }

    }
}
