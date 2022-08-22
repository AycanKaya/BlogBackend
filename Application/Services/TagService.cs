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
            var tagList = _context.Tags.ToArray();
            if (tagList == null)
                throw new ExceptionResponse("There are not exists.");
            return tagList;
        }

        private async Task<Post[]> findPost(Tag[] tagList)
        {
            var tagPost = tagList.Join(
               _context.PostTags,
               tag => tag.Id,
               postTag => postTag.TagID,
               (tag, postTag) => new PostInTagsDTO
               {
                   TagName = tag.TagName,
                   PostID = postTag.PostID,
               }).ToArray();
            List<Post> postList = new List<Post>();
            foreach (var tag in tagPost)
            {
                var post = await _context.Posts.Where(x => x.Id == tag.PostID && x.IsDeleted==false).FirstOrDefaultAsync();
                if (post != null && !postList.Contains(post))  
                postList.Add(post);
            }
            return postList.ToArray();
        }
        private async Task<PostResponseDTO[]> convertPostToPostResponseDTO(Post[] postList)
        {
            var postResponseDto = postList
              .Join(_context.UserInfo,
              post => post.AuthorID,
              userInfo => userInfo.UserID,
              (post, userInfo) => new PostResponseDTO
              {
                  PostId = post.Id,
                  AuthorName = userInfo.Name,
                  AuthorEmail = userInfo.Email,
                  Title = post.Title,
                  Content = post.Content,
                  IsApprove = post.IsApprove,
                  IsDeleted = post.IsDeleted,
                  CreateTime = post.CreateTime,
                  UpdateTime = post.UpdateTime,
              }).ToArray();
            return postResponseDto;
        }
        public async Task<PostCommentsDTO[]> PostsInTags(string tagNames)
        {
            string[] tagNameList = tagNames.Split("-");

            var tagList =new List<Tag>();
            foreach(var tagName in tagNameList)
            {
                var tag = _context.Tags.Where(x => x.TagName == tagName).FirstOrDefault();
                if (tag != null) 
                     tagList.Add(tag);
            }
            var posts = await findPost(tagList.ToArray());
            if (posts == null)
                throw new NullReferenceException("No posts");
            var postResponseDto = await convertPostToPostResponseDTO(posts);

            var list = new List<PostCommentsDTO>();
            foreach (var post in postResponseDto)
            {
                var comments = await _context.Comments.Where(x => x.PostID == post.PostId).ToArrayAsync();
                var postComments = new PostCommentsDTO();
                postComments.Post = post;
                postComments.Comments = comments;
                list.Add(postComments);
            }
          


            return list.ToArray();

        }
      

        public async Task<PostResponseDTO[]> GetPostsInTag(string tagName)
        {
            var tag = _context.Tags.Where(x => x.TagName == tagName).FirstOrDefault();
            var postag = await _context.PostTags.Where(x => x.TagID == tag.Id).ToListAsync();

            var postList = new List<PostResponseDTO>();
            foreach (var postTag in postag)
            {
                var post = _context.Posts.Where(x => x.Id == postTag.PostID && x.IsDeleted==false);
                var postResponse = await post.Join(_context.UserInfo,
              post => post.AuthorID,
              userInfo => userInfo.UserID,
              (post, userInfo) => new PostResponseDTO
              {
                  PostId = post.Id,
                  AuthorName = userInfo.Name,
                  AuthorEmail = userInfo.Email,
                  Title = post.Title,
                  Content = post.Content,
                  IsApprove = post.IsApprove,
                  IsDeleted = post.IsDeleted,
                  CreateTime = post.CreateTime,
                  UpdateTime = post.UpdateTime,
              }).FirstAsync();
                postList.Add(postResponse);
            }
            return postList.ToArray();

        }

        public async Task<Tag[]> GetTagsInPost(int postID)
        {
            var tagPost =await  _context.PostTags.Where(x => x.PostID == postID).ToListAsync();
            var tagsInPost = new List<Tag>();
            foreach(var tagpost in tagPost)
            {
                var tag = await _context.Tags.Where(x=> x.Id == tagpost.TagID).FirstOrDefaultAsync();
                tagsInPost.Add(tag);
            }
            return tagsInPost.ToArray();
        }

     

       



    }
}
