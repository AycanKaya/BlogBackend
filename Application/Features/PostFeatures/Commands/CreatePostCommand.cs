using System;
using MediatR;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.PostFeatures
{
    public class CreatePostCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string AuthorID { get; set; }

        public class CreatePostHandler : IRequestHandler<CreatePostCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreatePostHandler(IApplicationDbContext dbContext)
            {
                _context = dbContext;
            }

            public async Task<int> Handle(CreatePostCommand request,CancellationToken cancellationToken)
            {
                var post = new Post();
                post.Title = request.Title;
                post.Content = request.Content;
                post.CreateTime = request.CreateTime;
                post.UpdateTime = request.UpdateTime;
                post.AuthorID = request.AuthorID;
                _context.Posts.Add(post);
                await _context.SaveChanges();
                return post.Id;
            }
        }

    }
}
