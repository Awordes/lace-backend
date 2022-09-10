using AutoMapper;
using Lace.Application.CQRS.User.ViewModels;
using Lace.Application.DbContexts;
using MediatR;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.User.Queries;

public class GetUserQuery: IRequest<UserViewModel>
{
    public Guid Id { get; set; }
    
    private class Handler: IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<LaceDbContext> _contextFactory;

        public Handler(ILogger<Handler> logger, IMapper mapper, IDbContextFactory<LaceDbContext> contextFactory)
        {
            _logger = logger;
            _mapper = mapper;
            _contextFactory = contextFactory;
        }

        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                return _mapper.Map<UserViewModel>(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}