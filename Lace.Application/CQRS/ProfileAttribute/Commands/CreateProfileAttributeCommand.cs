using Lace.Application.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.ProfileAttribute.Commands;

public class CreateProfileAttributeCommand: IRequest
{
    public Guid ProfileId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid? DictionaryElementId { get; set; }

    public string? ExternalValue { get; set; }

    private class Handler: IRequestHandler<CreateProfileAttributeCommand>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IDbContextFactory<LaceDbContext> _contextFactory;

        public Handler(ILogger<Handler> logger, IDbContextFactory<LaceDbContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public async Task<Unit> Handle(CreateProfileAttributeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

                var profileAttribute = new Domain.Entities.ProfileAttribute
                {
                    ExternalValue = request.ExternalValue,
                    Profile = await context.Profiles.FirstOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken),
                    Category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken),
                    DictionaryElement = await context.DictionaryElements.FirstOrDefaultAsync(x => x.Id == request.DictionaryElementId, cancellationToken)
                };

                context.ProfileAttributes.Add(profileAttribute);

                await context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}