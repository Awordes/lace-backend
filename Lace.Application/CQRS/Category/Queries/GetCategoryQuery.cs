using AutoMapper;
using Lace.Application.CQRS.Category.ViewModels;
using Lace.Application.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.Category.Queries;

public class GetCategoryQuery: IRequest<CategoryViewModel>
{
    public Guid Id { get; set; }
    
    private class Handler: IRequestHandler<GetCategoryQuery, CategoryViewModel>
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

        public async Task<CategoryViewModel> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
                var category = await context.Categories.AsNoTracking()
                    .Include(x => x.DictionaryElements)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}