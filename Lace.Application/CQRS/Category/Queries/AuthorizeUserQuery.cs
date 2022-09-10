using AutoMapper;
using Lace.Application.CQRS.User.ViewModels;
using Lace.Application.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.Category.Queries;

public class AuthorizeUserQuery: IRequest<UserViewModel>
{
    public string Login { get; set; }

    public string Password { get; set; }
    
    private class Handler: IRequestHandler<AuthorizeUserQuery, UserViewModel>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IDbContextFactory<LaceDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public Handler(ILogger<Handler> logger, IDbContextFactory<LaceDbContext> contextFactory, IMapper mapper)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
                var user = await context.Users.AsNoTracking()
                    .Include(x => x.Profile)
                    .FirstOrDefaultAsync(x => x.Login == request.Login && x.Password == request.Password,
                        cancellationToken);
                return user is not null ? _mapper.Map<UserViewModel>(user) : null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}