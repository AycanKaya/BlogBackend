using System;
using Application.Interfaces;
using MediatR;

namespace Application.Features.PostFeatures
{
    public class DeletePostCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeletePostCommandHandler: IRequestHandler<DeletePostCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeletePostCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }
            public async Task<int> Handle(DeletePostCommand command,  CancellationToken cancellationToken)
            {
                var post = _context.Posts.Where(x => x.Id == command.Id).FirstOrDefault();
                if (post == null)
                    return default;
                _context.Posts.Remove(post);
                await _context.SaveChanges();
                return command.Id;
            }
        }
    }
}
