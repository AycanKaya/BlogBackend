using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        
        public UserService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Post> SharePost(string userId, PostDTO content)
        {
            var post = new Post();
            post.AuthorID = userId;
            post.Content = content.Content;
            post.Title = content.Title;
            post.CreateTime= DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChanges();
            return post;
        }


    }
}
