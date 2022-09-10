using AutoMapper;
using Lace.Application.CQRS.Category.ViewModels;
using Lace.Application.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.Category.Queries;

public class GetCategoriesQuery: IRequest<CategoryListViewModel>
{
    public string? Name { get; set; }
    
    private class Handler: IRequestHandler<GetCategoriesQuery, CategoryListViewModel>
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

        public async Task<CategoryListViewModel> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
                var categories = await context.Categories.AsNoTracking().ToListAsync(cancellationToken);
                return new CategoryListViewModel
                {
                    Categories = categories.Select(x => _mapper.Map<CategoryViewModel>(x)).ToList()
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