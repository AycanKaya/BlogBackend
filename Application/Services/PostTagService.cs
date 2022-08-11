using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.PostServiceDTOs;
using Application.DTO.PostTagDTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PostTagService : IPostTagService
    {
        IApplicationDbContext _context;
   
        public PostTagService(IApplicationDbContext dbContext)
        {
            _context = dbContext;
           
        }

      
        public async Task<bool> AddTagToPost(AddTagDTO addTag)
        {
            var isTagExists= _context.Tags.Where(x => x.TagName == addTag.TagName).FirstOrDefault();
            if(isTagExists == null)
            {
                var tag = new Tag();
                tag.TagName = addTag.TagName;
                _context.Tags.Add(tag);
                await _context.SaveChanges();

                var tagPost = new PostTag();
                tagPost.TagID = tag.Id;
                tagPost.PostID=addTag.PostID;
                _context.PostTags.Add(tagPost);
                await _context.SaveChanges();
                return true;

            }
            var tag_post = new PostTag();
            tag_post.PostID = addTag.PostID;
            tag_post.TagID= isTagExists.Id;
            _context.PostTags.Add(tag_post);
            await _context.SaveChanges();
            return true;

        }
        public async Task<Tag[]> GetPostWithTag(int postID)
        {
            
            var tagPost= _context.PostTags.Where(c => c.PostID==postID).ToArray();
            var tagList = new List<Tag>();
            foreach (var tag in tagPost)
            {
                tagList.Add(_context.Tags.Where(c => c.Id == tag.TagID).FirstOrDefault());
            }
            return tagList.ToArray();
           
        }
       
        // public async Task<> GetPostsInTag()
     
    }
}
