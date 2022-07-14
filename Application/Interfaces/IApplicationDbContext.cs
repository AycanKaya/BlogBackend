using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Comment> Comments { get; set; }
        DbSet<Post> Posts { get; set; }
   
        DbSet<Permissions>  Permissions { get; set; }
        DbSet<RoleWithPermissions> RoleWithPermissions { get; set; }

        Task<int> SaveChanges();


    }
}
