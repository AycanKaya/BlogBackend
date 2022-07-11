using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Domain.Entities;

namespace Application.Features.PostFeatures
{
    public class GetPostById  : IRequest<Post>
    {
        public int Id { get; set; }
        public class GetPostByIdHandler : IRequestHandler<GetPostById, Post>
        {
            private readonly IApplicationDbContext _context;    
            public GetPostByIdHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Post> Handle(GetPostById query,CancellationToken cancellation)
            {
                var post = _context.Posts.Where(post => post.Id == query.Id).FirstOrDefault();
                if (post == null)
                    throw new Exception("Please check the Id that your entered.");
                return post;
            }
        }
    }
}
