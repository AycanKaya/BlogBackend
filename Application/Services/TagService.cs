﻿using System;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Application.DTO.TagDTOs;
using System.Linq;

namespace Application.Services
{
    public class TagService : ITagService
    {
        IApplicationDbContext _context { get; set; }
        public TagService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<Tag>> CreateTag(CreateTagDTO createTag) 
        {
            var tag = new Tag();
            tag.TagName = createTag.TagName;
            var isTagExists = _context.Tags.Contains(tag);
            if (isTagExists) 
                throw new Exception("Already exists");
            _context.Tags.Add(tag);
            await _context.SaveChanges();
            return new BaseResponse<Tag>(tag);  
        }
        public async Task<List<Tag>> GetAllTags()
        {
            var tagList = _context.Tags.ToList();
            if (tagList == null)
                throw new ExceptionResponse("There are not exists.");
            return tagList;
        }
    }
}
