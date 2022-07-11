using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PostFeatures
{
    public class GetAllPosts : IRequest<IEnumerable<Post>>
    {
        public class GetAllPostsHandler : IRequestHandler<GetAllPosts, IEnumerable<Post>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPostsHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Post>> Handle(GetAllPosts request, CancellationToken  cancellationToken)
            {
                var postList = await _context.Posts.ToListAsync();
                if (postList == null)
                    throw new ArgumentNullException(nameof(postList));

                return postList.AsReadOnly();

            }
        }
    }
}
