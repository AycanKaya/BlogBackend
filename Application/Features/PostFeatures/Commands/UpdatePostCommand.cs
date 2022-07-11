using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces;

namespace Application.Features.PostFeatures
{
    public class UpdatePostCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
  
     //   public int AuthorID { get; set; }

        public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, int>
        {
            private readonly IApplicationDbContext _context;
            
            public UpdatePostCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
            {
                var post = _context.Posts.Where(x => x.Id == command.Id).FirstOrDefault();
                if (post == null)
                    return default;
                post.Title = command.Title;
                post.Content = command.Content;
                await _context.SaveChanges();

                return post.Id;

            }

        }

    }
}
