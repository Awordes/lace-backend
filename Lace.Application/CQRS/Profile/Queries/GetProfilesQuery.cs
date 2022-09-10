using AutoMapper;
using Lace.Application.CQRS.Profile.ViewModels;
using Lace.Application.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.Profile.Queries;

public class GetProfilesQuery: IRequest<ProfileListViewModel>
{
    public string? FirstName { get; set; }
    
    public Guid? DictionaryElementId { get; set; }

    public string? ExternalValue { get; set; }
    
    private class Handler: IRequestHandler<GetProfilesQuery, ProfileListViewModel>
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

        public async Task<ProfileListViewModel> Handle(GetProfilesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
                
                IQueryable<Domain.Entities.Profile> query = context.Profiles.AsNoTracking();

                if (request.FirstName is not null)
                    query = query.Where(x => x.Name.Contains(request.FirstName));

                if (request.DictionaryElementId is not null)
                    query = query
                        .Include(x => x.ProfileAttributes)
                        .ThenInclude(x => x.DictionaryElement)
                        .Where(x => x.ProfileAttributes.Any(y =>
                            y.DictionaryElement.Id == request.DictionaryElementId));
                else if (request.ExternalValue is not null)
                    query = query
                        .Include(x => x.ProfileAttributes)
                        .Where(x => x.ProfileAttributes.Any(y => y.ExternalValue == request.ExternalValue));

                var users = await query.ToListAsync(cancellationToken);
                
                return new ProfileListViewModel()
                {
                    Profiles = users.Select(x => _mapper.Map<ProfileViewModel>(x)).ToList()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}