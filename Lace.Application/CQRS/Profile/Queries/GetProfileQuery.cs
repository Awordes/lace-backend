using AutoMapper;
using Lace.Application.CQRS.Profile.ViewModels;
using Lace.Application.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.Profile.Queries;

public class GetProfileQuery: IRequest<ProfileViewModel>
{
    public Guid Id { get; set; }
    
    private class Handler: IRequestHandler<GetProfileQuery, ProfileViewModel>
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

        public async Task<ProfileViewModel> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
                var profile = await context.Profiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                return _mapper.Map<ProfileViewModel>(profile);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}