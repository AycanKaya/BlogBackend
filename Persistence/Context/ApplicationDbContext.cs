﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<RoleWithPermissions> RoleWithPermissions { get; set; }
        public DbSet<PostWithComments> PostWithComments { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<AccountLevel> AccountLevel { get; set; }
        public DbSet<UserAccountLevel> UserAccountLevels { get; set; }
        public DbSet<Tag> Tags { get; set; }   
        public DbSet<PostTag> PostTags { get; set; }
      




        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.ToTable(name: "Post");

            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.ToTable(name: "Comment");

            });
            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.HasKey(c => c.ID);
                entity.ToTable(name: "Permissions");

            });
        

            modelBuilder.Entity<RoleWithPermissions>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable(name: "RoleWithPermissions");

            });
            modelBuilder.Entity<PostWithComments>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable(name: "PostWithComments");
             
                
            });
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(c => c.UserID);
                entity.ToTable(name: "UserInfo");

            });

            modelBuilder.Entity<AccountLevel>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.ToTable(name: "AccountLevel");

            });
            modelBuilder.Entity<UserAccountLevel>(entity =>
            {
                entity.HasKey(c => c.UserID);
                entity.ToTable(name: "UserAccountLevel");
               

            });
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.ToTable(name: "Tag");

            });
            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.ToTable(name: "PostTag");
                

            });

           


        }



    }
}
