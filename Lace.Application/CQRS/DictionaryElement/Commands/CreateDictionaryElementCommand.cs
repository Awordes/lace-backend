using Lace.Application.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.DictionaryElement.Commands;

public class CreateDictionaryElementCommand: IRequest
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }
    
    private class Handler: IRequestHandler<CreateDictionaryElementCommand>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IDbContextFactory<LaceDbContext> _contextFactory;

        public Handler(ILogger<Handler> logger, IDbContextFactory<LaceDbContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public async Task<Unit> Handle(CreateDictionaryElementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

                context.DictionaryElements.Add(new Domain.Entities.DictionaryElement
                {
                    Name = request.Name,
                    Category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId,
                        cancellationToken)
                });

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