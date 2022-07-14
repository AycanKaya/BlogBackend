using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Post> SharePost(string userId, PostDTO content);
    }
}
