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

    public Guid AuthorizedUserId { get; set; }
    
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
                var profile = await context.Profiles.AsNoTracking()
                    .Include(x => x.ProfileAttributes)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.ProfileAttributes)
                    .ThenInclude(x => x.DictionaryElement)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                
                var profileViewModel = _mapper.Map<ProfileViewModel>(profile);
                profileViewModel.ProfileLaces = new List<ProfileLaceViewModel>();

                if (profile.UserId != request.AuthorizedUserId)
                {
                    var currentUserProfile = await context.Profiles.AsNoTracking()
                        .Include(x => x.ProfileAttributes)
                        .ThenInclude(x => x.Category)
                        .Include(x => x.ProfileAttributes)
                        .ThenInclude(x => x.DictionaryElement)
                        .FirstOrDefaultAsync(x => x.UserId == request.AuthorizedUserId, cancellationToken);

                    foreach (var profileAttribute in currentUserProfile.ProfileAttributes)
                    {
                        if (profile.ProfileAttributes.Any(x =>
                                x.DictionaryElement.Id == profileAttribute.DictionaryElement.Id))
                        {
                            profileViewModel.ProfileLaces.Add(new ProfileLaceViewModel
                            {
                                Category = profileAttribute.Category.Name,
                                ProfileAttributeValue = profileAttribute.DictionaryElement is null ? profileAttribute.ExternalValue : profileAttribute.DictionaryElement.Name
                            });
                        }
                    }
                }
                
                return profileViewModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}