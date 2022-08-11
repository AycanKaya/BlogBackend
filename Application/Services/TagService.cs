using System;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Application.DTO.TagDTOs;
using System.Linq;
using Application.DTO.PostServiceDTOs;
using Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class TagService : ITagService
    {
        IApplicationDbContext _context { get; set; }
        public TagService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTag(CreateTagDTO createTag) 
        {
            var tag = new Tag();
            tag.TagName = createTag.TagName;
            var isTagExists = _context.Tags.Contains(tag);
            if (isTagExists)
                return false;
            _context.Tags.Add(tag);
            await _context.SaveChanges();
            return true;
        }
        public async Task<Tag[]> GetAllTags()
        {
            var tagList =  _context.Tags.ToArray();
            if (tagList == null)
                throw new ExceptionResponse("There are not exists.");
            return tagList;
        }

        /*
        public async Task<PostAndTagsDTO[]> PostsInTags(string[] tagName)
        {
            var tagList = _context.Tags.Where(x => tagName.Contains(x.TagName));
            var tagPost = tagList.Join(
                _context.PostTags,
                tag => tag.Id,
                postTag => postTag.TagID,
                (tag, postTag) => new PostInTagsDTO
                {
                    TagName = tag.TagName,
                    PostID = postTag.PostID,
                }).ToArray();
               List<Post> postList =new List<Post>();

            foreach(var tag in tagPost)
            {
                var post = _context.Posts.Where(x => x.Id == tag.PostID).FirstOrDefault();
                postList.Add(post);
            }
            var posts = await PostList(postList);        
                  
         }  */

    

    }
}
